Public Class Form1
    Dim Line(2) As CLegs
    Dim floor As CFloor
    Dim Rnd As New Random
    Dim joint(2) As CJoints
    Dim P1 As Integer = 100
    Dim Wait As Integer = 0
    Dim WaitTime As Integer = 5
    Dim down As Boolean = False
    Dim Up As Boolean = True
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        floor = New CFloor(500)

        'create legs and joints
        For x = 0 To 2
            Line(x) = New CLegs(P1, Rnd, 1, 5)
            joint(x) = New CJoints(P1, 10, 10, 1)
            P1 += 100
        Next
        P1 -= 300
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'keep display refreshing, loop through display
        Display.Refresh()
    End Sub

    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        Dim g As Graphics
        g = e.Graphics

        'draw the floor
        floor.draw(g)


        'rotate each line and check if it has hit the floor, now make the jkoint follow the line point 1, draw line and joint, drop them, then increasew the fall speed
        For X = 0 To 2
            If Line(X).Angle / 2 <= Line(X).Speed * Line(X).increase Then
                Line(X).GoThroughClock += 1
                If Line(X).GoThroughClock >= Line(X).clock Then
                    Line(X).GoThroughClock = 0
                    down = True
                    Up = False
                Else
                    down = False
                    Up = False
                End If
            ElseIf Line(X).Angle / 2 <= Line(X).Speed * -Line(X).increase And Line(X).increase < 0 Then
                Line(X).GoThroughClock += 1
                If Line(X).GoThroughClock >= Line(X).clock Then
                    Line(X).GoThroughClock = 0
                    down = False
                    Up = True
                Else
                    down = False
                    Up = False
                End If
            End If

            Line(X).NewPoints(floor, down, Up)
            Line(X).HitFloor(floor)
            joint(X).rise(Line(X).p1)
            Line(X).draw(g)
            joint(X).draw(g)
            Line(X).drop()
            joint(X).drop(floor)
            Line(X).Yspeed += 1
            joint(X).Yspeed += 1

        Next
    End Sub


End Class
