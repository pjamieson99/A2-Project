Public Class Form1
    Dim GroundLegs As Integer = 2
    Dim Layers As Integer = 1
    Dim Line(GroundLegs, Layers) As CLegs
    Dim floor As CFloor
    Dim Rnd As New Random
    Dim joint(GroundLegs, Layers) As CJoints
    Dim P1 As Integer = 100
    Dim Wait As Integer = 0
    Dim WaitTime As Integer = 5
    Dim down As Boolean = False
    Dim Up As Boolean = True
    Dim ConnectJoint As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        floor = New CFloor(500)

        'create legs and joints
        For y = 0 To Layers
            If y = 1 Then
                P1 -= (GroundLegs + 1) * 100
            End If
            For x = 0 To GroundLegs
                If y = 1 Then
                    Line(x, y) = New CLegs(P1, 0, Rnd, 1, 5)
                    joint(x, y) = New CJoints(P1, 0, 10, 10, 1)
                Else
                    Line(x, y) = New CLegs(P1, 100, Rnd, 1, 5)
                    joint(x, y) = New CJoints(P1, 100, 10, 10, 1)
                End If


                P1 += 100
            Next
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

        'check if angle limit has been broken, if so stop rotation but continue movement for however long the line clock is
        For y = 0 To Layers
            For X = 0 To GroundLegs
                If Line(X, y).Angle / 2 <= Line(X, y).Speed * Line(X, y).increase Then
                    Line(X, y).GoThroughClock += 1
                    If Line(X, y).GoThroughClock >= Line(X, y).clock Then
                        Line(X, y).GoThroughClock = 0
                        Line(X, y).down = True
                        Line(X, y).up = False
                    Else
                        Line(X, y).down = False
                        Line(X, y).up = False
                    End If
                ElseIf Line(X, y).Angle / 2 <= Line(X, y).Speed * -Line(X, y).increase And Line(X, y).increase < 0 Then
                    Line(X, y).GoThroughClock += 1
                    If Line(X, y).GoThroughClock >= Line(X, y).clock Then
                        Line(X, y).GoThroughClock = 0
                        Line(X, y).down = False
                        Line(X, y).up = True
                    Else
                        Line(X, y).down = False
                        Line(X, y).up = False
                    End If
                End If

                'rotate each line and check if it has hit the floor, now make the jkoint follow the line point 1, draw line and joint, drop them, then increasew the fall speed
                If Line(X, y).py2 >= floor.ypos Then
                    Line(X, y).Friction()
                Else
                    Line(X, y).NewPoints()
                End If


            Next
        Next
        For y = 0 To Layers
            For x = 0 To GroundLegs
                If y = 0 Then
                    Line(x, y).px2 += Line(x, y + 1).px2 - Line(x, y).px1
                    Line(x, y).px1 = Line(x, y + 1).px2
                    Line(x, y).py2 += Line(x, y + 1).py2 - Line(x, y).py1
                    Line(x, y).py1 = Line(x, y + 1).py2
                End If
                If y = 0 Then
                    If floor.ypos <= Line(x, y).py1 Or floor.ypos <= Line(x, y).py2 Then
                        Line(x, y).Yspeed = 0

                        Line(x, y + 1).px1 += Line(x, y).px1 - Line(x, y + 1).px2
                        Line(x, y + 1).px2 = Line(x, y).px1
                        Line(x, y + 1).py1 += Line(x, y).py1 - Line(x, y + 1).py2
                        Line(x, y + 1).py2 = Line(x, y).py1
                    End If

                    If Line(x, y).Yspeed = 0 Then
                        Line(x, y + 1).Yspeed = 0
                        'If Line(x, y).py1 >= floor.ypos Then
                        '    Line(x, y + 1).py2 -= (Line(x, y).py1 - floor.ypos)
                        '    Line(x, y + 1).LYpos -= (Line(x, y).py1 - floor.ypos)
                        '    Line(x, y + 1).py1 -= (Line(x, y).py1 - floor.ypos)
                        'End If

                        If Line(x, y).py2 >= floor.ypos Then
                            Line(x, y + 1).py1 -= (Line(x, y).py2 - floor.ypos)
                            Line(x, y + 1).LYpos -= (Line(x, y).py2 - floor.ypos)
                            Line(x, y + 1).py2 -= (Line(x, y).py2 - floor.ypos)
                        End If
                    End If
                End If


                Line(x, y).HitFloor(floor)

                joint(x, y).rise(Line(x, y).p1)
                Line(x, y).draw(g, floor)
                joint(x, y).draw(g)
                Line(x, y).drop()
                joint(x, y).drop(floor)
                Line(x, y).Yspeed += 1
                joint(x, y).Yspeed += 1
            Next
        Next

    End Sub

    Private Sub Display_Click(sender As Object, e As EventArgs) Handles Display.Click

    End Sub

    'TODO: make joints always spawn one one side of leg, make sure that there is an end to the leg, make it so legs can connect to joint, 2 legs to one joint etc. make end legs have friction to move forwards. make body and centre of mass and if centre of mass is away from pivot that it falls - whole object rotates 



End Class
