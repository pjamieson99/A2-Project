Public Class CLeg
    Public LXpos As Integer
    Public LYpos As Integer
    Public Angle As Integer
    Public Speed As Integer
    Public clock As Integer
    Public Lp1 As PointF
    Public Lp2 As PointF
    Public Lpx1 As Double
    Public Lpx2 As Double
    Public Lpy1 As Double
    Public Lpy2 As Double
    Public increase As Integer
    Public GoThroughClock As Integer
    Public up As Boolean = True
    Public down As Boolean = False
    Public LeftSidex1 As Boolean
    Public RightSidex1 As Boolean
    Public LeftSidex2 As Boolean
    Public RightSidex2 As Boolean


    Public Sub New(x As Integer, y As Integer, rnd As Random)
        'set properties
        LXpos = x
        LYpos = y
        Lpx1 = LXpos
        Lpy1 = LYpos
        Lpx2 = LXpos
        Lpy2 = LYpos + 100
        Lp1 = New Point(LXpos, LYpos)
        Lp2 = New Point(LXpos, LYpos + 100)
        Speed = rnd.Next(1, 10)
        clock = rnd.Next(5, 80)
       Angle = rnd.Next(180, 190)
        GoThroughClock = 0

    End Sub

    Public Sub draw(g As Graphics, floor As CFloor)
        'draw object
        Lp1.X = Lpx1
        Lp1.Y = Lpy1
        Lp2.X = Lpx2
        Lp2.Y = Lpy2
        g.DrawLine(Pens.Black, Lp1.X, Lp1.Y, Lp2.X, Lp2.Y)
        'reset points to what the original dshape is but in its current position to allow the rotation matrix to rotate from original angle





    End Sub

    Sub NewPoints(angle As Double)

        Lp1.X = LXpos
        Lp1.Y = LYpos
        Lp2.X = LXpos
        Lp2.Y = LYpos + 100

        If up = True Then
            'rotate points using rotation matrix
            increase += 1
            Lpx1 = ((Lp1.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((Lp1.Y - LYpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180))) + LXpos
            Lpy1 = ((Lp1.X - LXpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)) + ((Lp1.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos
            Lpx2 = ((Lp2.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((Lp2.Y - LYpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180))) + LXpos
            Lpy2 = ((Lp2.X - LXpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)) + ((Lp2.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos

        ElseIf down = True Then
            increase -= 1
            Lpx1 = ((Lp1.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((Lp1.Y - LYpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180))) + LXpos
            Lpy1 = ((Lp1.X - LXpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)) + ((Lp1.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos
            Lpx2 = ((Lp2.X - LXpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180) + ((Lp2.Y - LYpos) * Math.Sin((Speed * increase + angle) * Math.PI / 180))) + LXpos
            Lpy2 = ((Lp2.X - LXpos) * (-Math.Sin((Speed * increase + angle) * Math.PI / 180)) + ((Lp2.Y - LYpos) * Math.Cos((Speed * increase + angle) * Math.PI / 180))) + LYpos

        End If

    End Sub

    Sub AngleLock(floor As CFloor)
        If Angle / 2 <= Speed * increase Then
            GoThroughClock += 1
            If GoThroughClock >= clock Then
                GoThroughClock = 0
                down = True
                up = False
            Else
                down = False
                up = False
            End If
        ElseIf Angle / 2 <= Speed * -increase And increase < 0 Then
            GoThroughClock += 1
            If GoThroughClock >= clock Then
                GoThroughClock = 0
                down = False
                up = True
            Else
                down = False
                up = False
            End If
        End If
    End Sub


    Function CheckFloor(floor As CFloor)
        If floor.ypos - 4 <= Lpy1 Or floor.ypos - 4 <= Lpy2 Then
            Return True
        Else
            Return False

        End If
    End Function

End Class
