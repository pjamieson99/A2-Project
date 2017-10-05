Public Class Form1


    Dim floor As CFloor
    Dim Rnd As New Random
    Dim count As Integer = 0
    Dim P1 As Integer = 100
    Dim Wait As Integer = 0
    Dim WaitTime As Integer = 5
    Dim down As Boolean = False
    Dim Up As Boolean = True
    Dim ConnectJoint As Boolean
    Dim GroundLegs As Integer = 1
    Dim joint(5, GroundLegs) As CJoints
    Dim Line(5, GroundLegs) As CLegs
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox2_Click(sender, e)
        'create floor object
        floor = New CFloor(500)

        'create legs and joints
        For y = 0 To 1
            For x = 0 To GroundLegs
                If y = 1 Then
                    count += 1
                    If count = 1 Then
                        P1 = 100
                    End If
                End If
                Line(y, x) = New CLegs(P1, 100 - y * 100, Rnd, 1, 5)
                joint(y, x) = New CJoints(P1, 100 - y * 100, 10, 10, 1)

                P1 += 100

            Next
        Next


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

        'check if angle limit has been broken, if so stop rotation but continue movement for however long the line clock is
        For y = 0 To 1
            For X = 0 To GroundLegs
                If Line(y, X).Angle / 2 <= Line(y, X).Speed * Line(y, X).increase Then
                    Line(y, X).GoThroughClock += 1
                    If Line(y, X).GoThroughClock >= Line(y, X).clock Then
                        Line(y, X).GoThroughClock = 0
                        Line(y, X).down = True
                        Line(y, X).up = False
                    Else
                        Line(y, X).down = False
                        Line(y, X).up = False
                    End If
                ElseIf Line(y, X).Angle / 2 <= Line(y, X).Speed * -Line(y, X).increase And Line(y, X).increase < 0 Then
                    Line(y, X).GoThroughClock += 1
                    If Line(y, X).GoThroughClock >= Line(y, X).clock Then
                        Line(y, X).GoThroughClock = 0
                        Line(y, X).down = False
                        Line(y, X).up = True
                    Else
                        Line(y, X).down = False
                        Line(y, X).up = False
                    End If
                End If

                'rotate each line and check if it has hit the floor, now make the jkoint follow the line point 1, draw line and joint, drop them, then increasew the fall speed
                If Line(y, X).py2 >= floor.ypos Then
                    Line(y, X).Friction()
                Else
                    Line(y, X).NewPoints()
                End If

                If y = 1 Then
                    Line(0, X).LYpos += Line(y, X).LYpos + 100 - Line(y, X).py2
                End If


                Line(y, X).HitFloor(floor)
                joint(y, X).rise(Line(y, X).p1)
                Line(y, X).draw(g, floor)
                joint(y, X).draw(g)
                Line(y, X).drop()
                joint(y, X).drop(floor)
                Line(y, X).Yspeed += 1
                joint(y, X).Yspeed += 1
            Next
        Next
    End Sub


    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.Top = 515
    End Sub

    'TODO: make joints always spawn on one side of leg, make sure that there is an end to the leg, make it so legs can connect to joint, 2 legs to one joint etc. make end legs have friction to move forwards. make body and centre of mass and if centre of mass is away from pivot that it falls - whole object rotates 



End Class
