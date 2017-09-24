Public Class Form1
    Dim p1 As PointF
    Dim p2 As PointF
    Dim line(2) As Legs
    Dim Joint(2) As Joints
    Dim check As Integer = 0
    Dim angle As Integer = 10
    Dim lowerangle As Boolean = False
    Dim counter As Integer = 0
    Dim contin As Boolean = True

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        check = 0

        If angle > 45 Or angle < -45 Then
            counter += 1
            contin = False
        End If

        If counter = 10 Then
            counter = 0
            contin = True
        End If
        Dim g As Graphics
        If contin = True Then

            If angle >= 45 Then
                angle -= 10
                lowerangle = True

            ElseIf lowerangle = False Or angle <= -45 Then
                angle += 10
                lowerangle = False
            ElseIf lowerangle = True Then
                angle -= 10
            End If

            g = e.Graphics
            p1 = New Point(100, 100)
            p2 = New Point(100, 200)
            For x = 0 To 2
                Joint(x) = New Joints(p1.X - 5, p1.Y - 5, 10, 10, 90)
                line(x) = New Legs(p1.X, p1.Y, p2.X, p2.Y)
                p1.X += 100
                p2.X += 100
                check += 100

                line(x).draw(g, check, angle)
                Joint(x).draw(g)
            Next
        End If
        '       rotate(Joint, g, line)

    End Sub

    'Sub rotate(ByVal joint() As Joints, g As Graphics, ByVal line() As Legs)


    'End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Display.Refresh()
    End Sub
End Class





