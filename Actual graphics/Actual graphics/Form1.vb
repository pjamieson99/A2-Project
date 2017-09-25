Public Class Form1
    Dim line(2) As LLegs
    Dim P1 As Integer = 100
    Dim up As Boolean = True
    Dim down As Boolean = False
    Dim increase As Integer = 1
    Dim speed As Integer = 1
    Dim angle As Integer
    Dim count As Integer = 0
    Dim clock(2) As Integer
    Dim CheckStatic As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        Dim g As Graphics
        g = e.Graphics


        For x = 0 To 2
            clock(x) = 50
            line(x) = New LLegs(P1, 100, 90, speed, clock(x))
            P1 += 100
        Next
        P1 = 100

        'For x = 0 To 2
        '    line(x).draw(g)
        'Next

        For x = 0 To 2
            If (speed * increase) > 45 Then
                up = False
                down = False
                If count <> clock(x) Then
                    CheckStatic = True
                    count += 1
                Else
                    count = 0
                    down = True
                End If
            ElseIf (speed * increase) < -45 Then
                up = False
                down = False
                If count <> clock(x) Then
                    CheckStatic = True
                    count += 1
                Else
                    count = 0
                    up = True
                End If
            End If
                If count = clock(x) + 1 Then
                count = 0
            End If

            line(x).rotate(g, up, down, increase)

            If up = True Then
                increase += 1
            ElseIf down = True Then
                increase -= 1
            End If

        Next




    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Display.Refresh()
    End Sub
End Class
