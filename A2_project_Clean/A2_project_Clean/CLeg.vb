Public Class CLeg
    Public Speed As Integer
    Public Clock As Integer
    Public Angle As Integer
    Public GoThroughClock As Integer

    Public LXpos As Double
    Public LYpos As Double

    Public LPx1 As Double
    Public LPx2 As Double
    Public LPy1 As Double
    Public LPy2 As Double

    Public LP1 As PointF
    Public LP2 As PointF

    Private increase As Integer = 1
    Public Down As Boolean
    Public Up As Boolean

    Public LeftSide As Boolean
    Public rightSide As Boolean
    Public Pivot As PointF

    Public OldPoint1 As PointF
    Public OldPoint2 As PointF


    Public Sub New(x As Integer, y As Integer, rnd As Random)
        'set properties
        LXpos = x
        LYpos = y
        Lpx1 = LXpos
        Lpy1 = LYpos
        Lpx2 = LXpos
        Lpy2 = LYpos + 100
        Speed = rnd.Next(1, 10)
        clock = rnd.Next(5, 80)
        Angle = rnd.Next(20, 60)
        GoThroughClock = 0
        Lp1 = New Point(LXpos, LYpos)
        Lp2 = New Point(LXpos, LYpos + 100)
    End Sub

    Sub AngleLock(floor As CFloor)
        If Angle / 2 <= Speed * increase Then
            GoThroughClock += 1
            If GoThroughClock >= Clock Then
                GoThroughClock = 0
                Down = True
                Up = False
            Else
                Down = False
                Up = False
            End If
        ElseIf Angle / 2 <= Speed * -increase And increase < 0 Then
            GoThroughClock += 1
            If GoThroughClock >= Clock Then
                GoThroughClock = 0
                down = False
                up = True
            Else
                down = False
                up = False
            End If
        End If
    End Sub

    Sub NewPoints(angle As Double)

        If Up = True Then
            'rotate points using rotation matrix
            increase += 1
            LPx1 = ((LP1.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((LP1.Y - LYpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)))) + LXpos
            LPy1 = ((LP1.X - LXpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180) + ((LP1.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos
            LPx2 = ((LP2.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((LP2.Y - LYpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)))) + LXpos
            LPy2 = ((LP2.X - LXpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180) + ((LP2.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos

        ElseIf Down = True Then
            increase -= 1
            LPx1 = ((LP1.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((LP1.Y - LYpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)))) + LXpos
            LPy1 = ((LP1.X - LXpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180) + ((LP1.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos
            LPx2 = ((LP2.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((LP2.Y - LYpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)))) + LXpos
            LPy2 = ((LP2.X - LXpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180) + ((LP2.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos

        End If

    End Sub

    Sub AttachBottomLegs(x As Integer, Connection As PointF, Line2 As CLeg)

        LPx2 += Line2.LPx2 - LPx1
        LPx1 = Line2.LPx2
        LPy2 += Line2.LPy2 - LPy1
        LPy1 = Line2.LPy2

        LYpos = Connection.Y
        LYpos = Line2.LYpos + 100
        LXpos = Connection.X
        LXpos = Line2.LXpos

    End Sub

    Sub FindLowestLeg(Line2 As CLeg, ByRef BodyRise As Double)

        If BodyRise <= LPy2 Then
            BodyRise = LPy2
        End If
        If BodyRise <= LPy1 Then
            BodyRise = LPy1
        End If
        If BodyRise <= Line2.LPy1 Then
            BodyRise = Line2.LPy1
        End If
    End Sub

    Sub FindSideLegisOn(Body As CBody, Floor As CFloor)
        Body.Rightside = False
        Body.Leftside = False

        'need to put this function in body instead
        If Form1.CheckFloor(Floor, LPx1) = True And LPx1 < Body.CoM.X Then
            Body.Leftside = True
            Body.LeftMomentum = 0.1

        ElseIf form1.CheckFloor(Floor, LPx1) = True And LPx1 > Body.CoM.X Then
            Body.Rightside = True
            Body.RightMomentum = 0.1

        ElseIf form1.CheckFloor(Floor, LPx1) = True And LPx1 = Body.CoM.X Then
            Body.Rightside = True
            Body.Leftside = True
            Body.RightMomentum = 0.1
            Body.LeftMomentum = 0.1

        End If

        If Form1.CheckFloor(Floor, LPy1) = True Then
            Pivot.X = LPx1
            Pivot.Y = LPy1
            Body.Pivot = Pivot

            If Form1.CheckFloor(Floor, LPx2) = True And LPx2 < Body.CoM.X Then
                Body.Leftside = True
                Body.LeftMomentum = 0.1
                If LPx2 > Pivot.X Then
                    Pivot.X = LPx2
                    Pivot.Y = LPy2
                    Body.Pivot = Pivot
                End If
            ElseIf Form1.CheckFloor(Floor, LPx2) = True And LPx2 > Body.CoM.X Then
                Body.Rightside = True
                Body.RightMomentum = 0.1
                If LPx2 < Pivot.X Then
                    Pivot.X = LPx2
                    Pivot.Y = LPy2
                    Body.Pivot = Pivot
                End If
            ElseIf Form1.CheckFloor(Floor, LPx2) = True And LPx2 = Body.CoM.X Then
                Body.Rightside = True
                Body.Leftside = True
                Body.RightMomentum = 0.1
                Body.LeftMomentum = 0.1
            Else
                Body.Leftside = False
                Body.Rightside = False
            End If

        Else
            Pivot.X = LPx2
            Pivot.Y = LPy2
        End If



    End Sub

    Sub StopFalling(Body As CBody, Floor As CFloor)

        If Form1.CheckFloor(Floor, LPy1) = True Or Form1.CheckFloor(Floor, LPy2) Then
            Body.Yspeed = 0
        End If

    End Sub
End Class
