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
    Dim BodyRise As Double
    Dim Friction As Integer = 0
    Dim OldPoints(2) As Double
    Dim NegativeFriction As Integer
    Dim Checking As Boolean
    Dim Escape As Boolean
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




        For y = 0 To Layers
            For x = 0 To GroundLegs
                Line(x, y).AngleLock(floor)

                Line(x, y).NewPoints(Body.AngleIncrease)
            Next
        Next

        For x = 0 To 2
            If Line(x, 0).Lpy2 > (floor.ypos - 10) Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If

        Checking = False

        'set points to body 
        For x = 0 To GroundLegs
            Body.SetPoints(Line(x, 1), x)
        Next

        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next

        For x = 0 To 2
            Line(x, 1).LYpos = Body.connections(x).Y
            Line(x, 0).LYpos = Line(x, 1).LYpos + 100
            Line(x, 1).LXpos = Body.connections(x).X
            Line(x, 0).LXpos = Line(x, 1).LXpos
        Next


        BodyRise = 0



        For x = 0 To 2
            If Line(x, 0).Lpy2 > (floor.ypos - 10) Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If
        Checking = False


        'find furthest line past floor
        BodyRise = Line(0, 0).Lpy2
        For x = 0 To GroundLegs
            If BodyRise <= Line(x, 0).Lpy2 Then
                BodyRise = Line(x, 0).Lpy2
            End If
            If BodyRise <= Line(x, 0).Lpy1 Then
                BodyRise = Line(x, 0).Lpy1
            End If
            If BodyRise <= Line(x, 1).Lpy1 Then
                BodyRise = Line(x, 1).Lpy1
            End If
        Next


        'raise body by distance between floor and line
        If floor.ypos <= BodyRise Then
            Body.Bpy1 -= (BodyRise - floor.ypos)
            Body.Bpy2 -= (BodyRise - floor.ypos)
            For x = 0 To 2
                Body.connections(x).Y -= (BodyRise - floor.ypos)
                Body.StaticConnections(x).Y -= (BodyRise - floor.ypos)
            Next
            Body.Bypos1 -= (BodyRise - floor.ypos)
            Body.Bypos2 -= (BodyRise - floor.ypos)
            Body.ResetCoM()

        End If

        'set points to body 
        For x = 0 To GroundLegs
            Body.SetPoints(Line(x, 1), x)
        Next

        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next



        For x = 0 To 2
            Line(x, 1).LYpos = Body.connections(x).Y
            Line(x, 0).LYpos = Line(x, 1).LYpos + 100
            Line(x, 1).LXpos = Body.connections(x).X
            Line(x, 0).LXpos = Line(x, 1).LXpos
        Next

        For x = 0 To 2
            If Line(x, 0).Lpy2 > (floor.ypos - 10) Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If
        Checking = False

        'check which side leg is on when on floor
        For y = 0 To 1
            For x = 0 To GroundLegs
                If Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx2 < Body.CoM.X Then
                    Line(x, y).LeftSidex2 = True
                    Body.LeftMomentum = 0.1
                ElseIf Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx2 > Body.CoM.X Then
                    Line(x, y).RightSidex2 = True
                    Body.RightMomentum = 0.1
                ElseIf Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx2 = Body.CoM.X Then
                    Line(x, y).RightSidex2 = True
                    Line(x, y).LeftSidex2 = True
                    Body.RightMomentum = 0.1
                    Body.LeftMomentum = 0.1
                Else
                    Line(x, y).LeftSidex2 = False
                    Line(x, y).RightSidex2 = False
                End If
            Next
        Next

        For y = 0 To 1
            For x = 0 To GroundLegs
                If Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx1 < Body.CoM.X Then
                    Line(x, y).LeftSidex1 = True
                    Body.LeftMomentum = 0.1
                ElseIf Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx1 > Body.CoM.X Then
                    Line(x, y).RightSidex1 = True
                    Body.RightMomentum = 0.1
                ElseIf Line(x, y).CheckFloor(floor) = True And Line(x, y).Lpx1 = Body.CoM.X Then
                    Line(x, y).RightSidex1 = True
                    Line(x, y).LeftSidex1 = True
                    Body.RightMomentum = 0.1
                    Body.LeftMomentum = 0.1
                Else
                    Line(x, y).LeftSidex1 = False
                    Line(x, y).RightSidex1 = False
                End If
            Next
        Next



        'find pivot point and fall of it
        Escape = False
        For y = 0 To 1
            If Escape = True Then
                Exit For
            End If
            For x = GroundLegs To 0 Step -1
                If Line(x, y).LeftSidex2 = True And Line(x, y).RightSidex2 = False Then
                    Body.FallRight(Line(x, y).Lpx2, Line(x, y).Lpy2, g)
                    Escape = True
                    Exit For
                End If
            Next
        Next
        For y = 0 To 1
            For x = 0 To GroundLegs
                If Line(x, 0).LeftSidex2 = False And Line(x, y).RightSidex2 = True Then
                    Body.FallLeft(Line(x, y).Lpx2, Line(x, y).Lpy2, g)
                    Escape = True
                    Exit For

                End If
            Next
        Next
        For y = 0 To 1
            If Escape = True Then
                Exit For
            End If
            For x = GroundLegs To 0 Step -1
                If Line(x, y).LeftSidex1 = True And Line(x, y).RightSidex1 = False Then
                    Body.FallRight(Line(x, y).Lpx1, Line(x, y).Lpy1, g)
                    Escape = True
                    Exit For
                End If
            Next
        Next

        For y = 0 To 1
            If Escape = True Then
                Exit For
            End If
            For x = 0 To GroundLegs
                If Line(x, 0).LeftSidex1 = False And Line(x, y).RightSidex1 = True Then
                    Body.FallLeft(Line(x, y).Lpx1, Line(x, y).Lpy1, g)
                    Escape = True
                    Exit For

                End If
            Next
        Next


        Body.ResetCoM()


        For x = 0 To 2
            If Line(x, 0).Lpy2 > (floor.ypos - 10) Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If
        Checking = False

        'set points to body 
        For x = 0 To GroundLegs

            Body.SetPoints(Line(x, 1), x)

        Next

        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next

        'check if hit floor and if so stop falling
        For y = 0 To Layers
            For x = 0 To GroundLegs
                If Line(x, y).CheckFloor(floor) = True Then
                    Body.yspeed = 0
                End If
            Next
        Next

        Body.drop()

        'set points to body 
        For x = 0 To GroundLegs

            Body.SetPoints(Line(x, 1), x)

        Next

        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next

        'find furthest line past floor
        BodyRise = Line(0, 0).Lpy2
        For x = 0 To GroundLegs
            If BodyRise <= Line(x, 0).Lpy2 Then
                BodyRise = Line(x, 0).Lpy2
            End If
            If BodyRise <= Line(x, 0).Lpy1 Then
                BodyRise = Line(x, 0).Lpy1
            End If
            If BodyRise <= Line(x, 1).Lpy1 Then
                BodyRise = Line(x, 1).Lpy1
            End If


        Next

        For x = 0 To 2
            If Line(x, 0).Lpy2 > (floor.ypos - 10) Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If
        Checking = False

        'raise body by distance between floor and line
        If floor.ypos - 4 <= BodyRise Then
            Body.Bpy1 -= (BodyRise - floor.ypos)
            Body.Bpy2 -= (BodyRise - floor.ypos)
            For x = 0 To 2
                Body.connections(x).Y -= (BodyRise - floor.ypos)
                Body.StaticConnections(x).Y -= (BodyRise - floor.ypos)
            Next
            Body.Bypos1 -= (BodyRise - floor.ypos)
            Body.Bypos2 -= (BodyRise - floor.ypos)


            For x = 0 To 2
                Line(x, 1).LYpos = Body.connections(x).Y
                Line(x, 0).LYpos = Line(x, 1).LYpos + 100
                Line(x, 1).LXpos = Body.connections(x).X
                Line(x, 0).LXpos = Line(x, 1).LXpos
            Next

            Body.ResetCoM()
        End If

        For x = 0 To 2
            If Line(x, 0).Lpy2 > floor.ypos - 10 Then
                Checking = True
            End If


        Next
        If Checking = False Then
            Console.Read()
        End If
        Checking = False
        Friction = 0
        NegativeFriction = 0
        For x = 0 To 2
            If OldPoints(x) - Line(x, 0).Lpx2 > Friction And Line(x, 0).CheckFloor(floor) = True Then
                Friction = (OldPoints(x) - Line(x, 0).Lpx2)
            End If
            If OldPoints(x) - Line(x, 0).Lpx2 < NegativeFriction And Line(x, 0).CheckFloor(floor) = True Then
                NegativeFriction = (OldPoints(x) - Line(x, 0).Lpx2)
            End If
        Next

        Body.Bpx1 += Friction + NegativeFriction
        Body.Bpx2 += Friction + NegativeFriction
        For x = 0 To 2
            Body.connections(x).X += Friction + NegativeFriction
            Body.StaticConnections(x).X += Friction + NegativeFriction
        Next
        Body.Bxpos1 += Friction + NegativeFriction
        Body.Bxpos2 += Friction + NegativeFriction


        For x = 0 To 2
            Line(x, 1).LXpos = Body.connections(x).X
            Line(x, 0).LXpos = Line(x, 1).LXpos
        Next

        'NegativeFriction = 0
        'Friction = 0
        'For x = 0 To 2
        '    If Line(x, 0).Lpx2 - OldPoints(x) < -Friction And Line(x, 0).CheckFloor(floor) = True Then
        '        '  Friction = -(Line(x, 0).Lpx2 - OldPoints(x)) / 2
        '    End If
        '    'If Line(x, 0).Lpx2 - OldPoints(x) > -NegativeFriction And Line(x, 0).CheckFloor(floor) = True Then
        '    '    NegativeFriction = -(Line(x, 0).Lpx2 - OldPoints(x)) / 2
        '    'End If
        'Next
        ''Friction += NegativeFriction
        'For x = 0 To 2
        '    Body.connections(x).X += Friction
        '    Body.StaticConnections(x).X += Friction
        'Next
        'Body.Bxpos1 += Friction
        'Body.Bxpos2 += Friction
        'Body.Bpx1 += Friction
        'Body.Bpx2 += Friction

        'set points to body 
        For x = 0 To GroundLegs
            Body.SetPoints(Line(x, 1), x)
        Next

        'attach the bottom legs to the top legs and move with them
        For x = 0 To 2
            Line(x, 0).Lpx2 += Line(x, 1).Lpx2 - Line(x, 0).Lpx1
            Line(x, 0).Lpx1 = Line(x, 1).Lpx2
            Line(x, 0).Lpy2 += Line(x, 1).Lpy2 - Line(x, 0).Lpy1
            Line(x, 0).Lpy1 = Line(x, 1).Lpy2
        Next

        For x = 0 To 2
            Line(x, 1).LYpos = Body.connections(x).Y
            Line(x, 0).LYpos = Line(x, 1).LYpos + 100
            Line(x, 1).LXpos = Body.connections(x).X
            Line(x, 0).LXpos = Line(x, 1).LXpos
        Next

        For x = 0 To 2
            OldPoints(x) = Line(x, 0).Lpx2
        Next

        If Body.AngleIncrease > 80 Or Body.AngleIncrease < -80 Then
            Console.ReadLine()
        End If
        'draw everything and set joint = to end of line
        For y = 0 To Layers
            For x = 0 To GroundLegs

                Line(x, y).draw(g, floor)
                joint(x, y).StickToLeg(Line(x, y).Lp1)
                joint(x, y).draw(g)
            Next
        Next
        Body.draw(g)


    End Sub

    Private Sub Display_Click(sender As Object, e As EventArgs) Handles Display.Click

    End Sub
End Class
