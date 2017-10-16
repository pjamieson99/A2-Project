Public Class Form1
    Dim GroundLegs As Integer = 2
    Dim Layers As Integer = 1
    Dim Line(GroundLegs, Layers) As CLeg
    Dim floor As CFloor
    Dim Rnd As New Random
    Dim joint(GroundLegs, Layers) As CJoint
    Dim P1 As Integer = 100
    Dim down As Boolean = False
    Dim Up As Boolean = True
    Dim Body As CBody
    Dim P1Legs(GroundLegs) As PointF
    Dim LineDiff As Double


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        floor = New CFloor(500)
        Body = New CBody(100, 0, 300, 0)
        'create legs and joints
        For y = 0 To Layers
            If y = 1 Then
                P1 -= (GroundLegs + 1) * 100
            End If
            For x = 0 To GroundLegs
                If y = 1 Then
                    Line(x, y) = New CLeg(P1, 0, Rnd)
                    joint(x, y) = New CJoint(P1, 0, 10, 10, 1)
                    P1Legs(x) = Line(x, y).Lp1
                Else
                    Line(x, y) = New CLeg(P1, 100, Rnd)
                    joint(x, y) = New CJoint(P1, 100, 10, 10, 1)
                End If

                P1 += 100
            Next



        Next
        P1 -= 300
        Body.BodyPoints(P1Legs, GroundLegs)
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
        Body.drop()
        For y = 0 To Layers
            For x = 0 To GroundLegs
                Line(x, y).AngleLock(floor)
                Line(x, y).NewPoints()
            Next
        Next


        For x = 0 To 2

            If floor.ypos <= Line(x, 0).Lpy1 Or floor.ypos <= Line(x, 0).Lpy2 Then


                Line(x, 1).Lpy1 -= (Line(x, 0).Lpy2 - floor.ypos)
                Line(x, 1).LYpos -= (Line(x, 0).Lpy2 - floor.ypos)
                Line(x, 1).Lpy2 -= (Line(x, 0).Lpy2 - floor.ypos)
                Body.Bypos1 -= (Line(x, 0).Lpy2 - floor.ypos)
                Body.Bypos2 -= (Line(x, 0).Lpy2 - floor.ypos)
                For y = 0 To GroundLegs
                    Body.connections(y).Y -= (Line(x, 0).Lpy2 - floor.ypos)
                Next

            End If
        Next
        For y = 0 To Layers
            For x = 0 To GroundLegs
                Line(x, y).HitFloor(floor)
            Next
        Next




            For y = 0 To Layers
            For x = 0 To GroundLegs
                '  Line(x, y).HitFloor(floor)
                If Line(x, y).CheckFloor(floor) = True Then
                    Body.yspeed = 0
                    End If


            Next
        Next

        'check which side leg is on when on floor
        For x = 0 To GroundLegs
            If Line(x, 0).CheckFloor(floor) = True And Line(x, 0).Lpx1 < Body.CoM.X Then
                Line(x, 0).LeftSide = True
            ElseIf Line(x, 0).CheckFloor(floor) = True And Line(x, 0).lpx1 > Body.CoM.X Then
                Line(x, 0).RightSide = True
            ElseIf Line(x, 0).CheckFloor(floor) = True And Line(x, 0).lpx1 = Body.CoM.X Then
                Line(x, 0).RightSide = True
                Line(x, 0).LeftSide = True
            Else
                Line(x, 0).LeftSide = False
                Line(x, 0).RightSide = False
            End If
        Next

        'find pivot point and fall of it
        For x = GroundLegs To 0 Step -1
            If Line(x, 0).LeftSide = True And Line(x, 0).RightSide = False Then
                ' Body.FallRight(Line(x, 1).Lp1, g)
            ElseIf Line(x, 0).LeftSide = True And Line(x, 0).RightSide = True Then

            End If
        Next

        'find pivot point and fall of it
        For x = 0 To GroundLegs
            If Line(x, 0).LeftSide = False And Line(x, 0).RightSide = True Then

            ElseIf Line(x, 0).LeftSide = True And Line(x, 0).RightSide = True Then

            End If
        Next



        'set points to body 
        For x = 0 To GroundLegs
            Line(x, 1).Lp2.Y += Body.connections(x).Y - Line(x, 1).Lp1.Y
            Line(x, 1).Lpy2 += Body.connections(x).Y - Line(x, 1).Lp1.Y
            Line(x, 1).Lp1 = Body.connections(x)
            Line(x, 1).LYpos = Line(x, 1).Lp1.Y
            Line(x, 1).Lpy1 = Line(x, 1).Lp1.Y
        Next
        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next
        For y = 0 To Layers
            For x = 0 To GroundLegs

                Line(x, y).draw(g, floor)
                joint(x, y).StickToLeg(Line(x, y).Lp1)
                joint(x, y).draw(g)
            Next
        Next
        Body.draw(g)


    End Sub




End Class
