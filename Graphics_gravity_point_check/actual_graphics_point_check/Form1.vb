Public Class Form1
    Dim line(2) As CLegs
    Dim p1 As Integer = 100
    Dim up(2) As Boolean
    Dim down(2) As Boolean
    Dim increase(2) As Integer
    Public floor As CFloor
    Dim count(2) As Integer
    Dim CheckStatic(2) As Boolean
    Dim joint(2) As CJoints
    Dim RunOnce As Integer = 0
    Dim rnd As New Random
    '  Dim gravity As Integer = 2
    Dim yspeed(2) As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        Dim g As Graphics
        g = e.Graphics
        floor = New CFloor(500)
        floor.draw(g)
        If RunOnce = 0 Then
            For x = 0 To 2
                up(x) = True
                down(x) = False
                count(x) = 0
                increase(x) = 1
                yspeed(x) = 0
                line(x) = New CLegs(P1, rnd)
                joint(x) = New CJoints(P1, 10, 10)
                P1 += 100
            Next
            P1 -= 300
        End If
        RunOnce += 1
        For x = 0 To 2
            yspeed(x) += 1
            line(x).Ypos += yspeed(x)

            joint(x).JYpos += yspeed(x)

            line(x).p1.Y += yspeed(x)
            line(x).p2.Y += yspeed(x)
            line(x).Xpos = line(x).Xpos

        Next
        'For x = 0 To 2
        '    line(x).draw(g)
        'Next
        For x = 0 To 2
            joint(x).draw(g)
            joint(x).drop(g)
            If (line(x).Speed * increase(x)) > (line(x).Angle / 2) Then
                up(x) = False
                down(x) = False
                If count(x) <> line(x).clock Then
                    CheckStatic(x) = True
                    count(x) += 1
                Else
                    count(x) = 0
                    down(x) = True
                End If
            ElseIf (line(x).Speed * increase(x)) < -(line(x).Angle / 2) Then
                up(x) = False
                down(x) = False
                If count(x) <> line(x).clock Then
                    CheckStatic(x) = True
                    count(x) += 1
                Else
                    count(x) = 0
                    up(x) = True
                End If
            End If
            If count(x) = line(x).clock + 1 Then
                count(x) = 0
            End If
            line(x).NewPoints(increase(x), floor)

            line(x).rotate(g, up(x), down(x), increase(x))
            If up(x) = True Then
                increase(x) += 1
            ElseIf down(x) = True Then
                increase(x) -= 1
            End If

        Next
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Display.Refresh()
    End Sub


End Class
