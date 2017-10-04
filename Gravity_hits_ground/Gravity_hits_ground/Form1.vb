Public Class Form1
    Dim Line(2) As CLegs
    Dim floor As CFloor
    Dim Rnd As New Random
    Dim joint(2) As CJoints
    Dim P1 As Integer = 100
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        floor = New CFloor(500)
        For x = 0 To 2
            Line(x) = New CLegs(P1, Rnd, 1)
            joint(x) = New CJoints(P1, 10, 10, 1)
            P1 += 100
        Next
        P1 -= 300
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Display.Refresh()
    End Sub

    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        Dim g As Graphics
        g = e.Graphics


        floor.draw(g)



        For X = 0 To 2
            Line(X).NewPoints(floor)
            Line(X).draw(g)
            joint(X).draw(g)
            Line(X).drop()
            joint(X).drop(floor)
            Line(X).Yspeed += 1
            joint(X).Yspeed += 1

        Next
    End Sub


End Class
