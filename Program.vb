Imports System
Imports System.Runtime.CompilerServices

Module Program
    Sub Main(args As String())
        Dim i As Long, j As Integer, k As Integer, n As Integer, asker As Integer, trump As Integer
        Dim str_hand As String
        Dim suits()() As String
        Dim arr_leads() As String
        Dim arr_leaders() As Integer
        Dim info() As Integer
        Dim info_min() As Integer
        Dim info_max() As Integer
        Dim honors() As Integer
        ReDim hand(3)
        ReDim suits(3)
        ReDim arr_leads(63)
        ReDim arr_leaders(51)

        For k = 0 To 3
            ReDim hand(k).suit(3)
        Next
        For k = 0 To 3
            Player(k) = New EPBot86.EPBot
            'Player(k) = New EPBot64.EPBot
            'Player(k) = New EPBotARM64.EPBot
        Next k

        set_board()
        set_dealers()
        set_vulnerability()
        set_strain_mark()



        vulnerable = 3
        deal = 1
        dealer = 0

        ReDim arr_bids(63)
        arr_bids(0) = "00"
        arr_bids(1) = "08"
        arr_bids(2) = "00"
        arr_bids(3) = "14*Jacoby 2NT"
        arr_bids(4) = "00"
        arr_bids(5) = "16*shortness  !D"
        arr_bids(6) = "01"
        arr_bids(7) = "20*Cue bid, a !C stopper"
        arr_bids(8) = "00"
        arr_bids(9) = "24*Blackwood 1430, for !S"
        arr_bids(10) = "00"
        arr_bids(11) = "25"
        arr_bids(12) = "00"
        arr_bids(13) = "26"
        arr_bids(14) = "00"
        arr_bids(15) = "27"
        arr_bids(16) = "00"
        arr_bids(17) = "33"
        arr_bids(18) = "00"
        arr_bids(19) = "00"
        arr_bids(20) = "00"

        'arr_bids(13) = "33"
        'arr_bids(14) = "00"
        'arr_bids(15) = "00"
        'arr_bids(16) = "00"

        For k = 0 To 3
            With Player(k)
                asker = -1
                trump = C_NT
                'IMPORTANT - it is required to establish a system for both lines
                .system_type(0) = T_21GF
                .system_type(1) = T_21GF
                .conventions(0, "Blackwood 1430") = True
                .conventions(1, "Blackwood 1430") = True
                .conventions(0, "Blackwood without K And Q") = False
                .conventions(1, "Blackwood without K And Q") = False

                'set hand
                '.new_hand(k, hand(k).suit, dealer, vulnerable)
                ''set the entire auction
                .set_arr_bids(arr_bids)
                Console.WriteLine("Player " & k)
                For n = 0 To 3
                    Console.WriteLine("Position " & n)
                    info = .info_feature(n)
                    Console.WriteLine("HCP " & info(402) & "-" & info(403))
                    info_min = .info_min_length(n)
                    info_max = .info_max_length(n)
                    For i = 3 To 0 Step -1
                        Console.WriteLine("Length " & strain_mark(i) & " " & info_min(i) & "-" & info_max(i))
                    Next i
                    If info(425) > 0 Then
                        asker = n
                        Console.WriteLine("asker=" & asker)
                        trump = info(424)
                        Console.WriteLine("trump=" & trump)
                    End If

                Next n
                If asker >= 0 Then
                    info = .info_feature((asker + 2) Mod 4)
                    Console.WriteLine("A=" & info(406))
                    Console.WriteLine("K=" & info(407))

                    If trump <> 4 Then
                        honors = .info_honors((asker + 2) Mod 4)
                        Console.WriteLine("Q=" & (honors(trump) And 4) \ 4)
                    End If
                End If

                Console.WriteLine("")
            End With
            Console.WriteLine("")
        Next k
        Console.ReadKey()
        Console.WriteLine("")
    End Sub
End Module
