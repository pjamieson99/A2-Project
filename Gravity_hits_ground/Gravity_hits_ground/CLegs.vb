﻿Public Class CLegs
    Public LXpos As Integer
    Public LYpos As Integer
    Public Angle As Integer
    Public Speed As Integer
    Public clock As Integer
    Public p1 As PointF
    Public p2 As PointF
    Public px1 As Double
    Public px2 As Double
    Public py1 As Double
    Public py2 As Double
    Public Yspeed As Integer
    Public increase As integer
    Public Sub New(x As Integer, rnd As Random, sp As Integer)
        LXpos = x
        LYpos = 100
        px1 = LXpos
        py1 = LYpos
        px2 = LXpos
        py2 = LYpos + 100
        p1 = New Point(LXpos, LYpos)
        p2 = New Point(LXpos, LYpos + 100)
        Speed = rnd.Next(2, 5)
        clock = rnd.Next(10, 70)
        Angle = rnd.Next(45, 180)

        Yspeed = sp
    End Sub

    Public Sub draw(g As Graphics)
        '     g.TranslateTransform(LXpos, LYpos)
        g.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y)
        '    g.TranslateTransform(-LXpos, -LYpos)
        p1.X = LXpos
        p1.Y = LYpos
        p2.X = LXpos
        p2.Y = LYpos + 100

    End Sub

    Public Sub drop()
        LYpos += Yspeed
        p1.Y += Yspeed
        p2.Y += Yspeed
    End Sub

    Sub NewPoints(floor As CFloor)
        'p1.X = ((p1.X - LXpos) * Math.Cos(Speed * Math.PI / 180)) + ((p1.Y - LYpos) * Math.Sin(Speed * Math.PI / 180)) + LXpos
        'p1.Y = ((p1.X - LXpos) * (-Math.Sin(Speed * Math.PI / 180))) + ((p1.Y - LYpos) * Math.Cos(Speed * Math.PI / 180)) + LYpos
        'p2.X = ((p2.X - LXpos) * Math.Cos(Speed * Math.PI / 180)) + ((p2.Y - LYpos) * Math.Sin(Speed * Math.PI / 180)) + LXpos
        'p2.Y = ((p2.X - LXpos) * (-Math.Sin(Speed * Math.PI / 180))) + ((p2.Y - LYpos) * Math.Cos(Speed * Math.PI / 180)) + LYpos

        px1 = ((p1.X - LXpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p1.Y - LYpos) * Math.Sin(Speed * increase * Math.PI / 180)) + LXpos
        py1 = ((p1.X - LXpos) * (-Math.Sin(Speed * increase * Math.PI / 180))) + ((p1.Y - LYpos) * Math.Cos(Speed * increase * Math.PI / 180)) + LYpos
        px2 = ((p2.X - LXpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p2.Y - LYpos) * Math.Sin(Speed * increase * Math.PI / 180)) + LXpos
        py2 = ((p2.X - LXpos) * (-Math.Sin(Speed * increase * Math.PI / 180))) + ((p2.Y - LYpos) * Math.Cos(Speed * increase * Math.PI / 180)) + LYpos

        p1.X = px1
        p1.Y = py1
        p2.X = px2
        p2.Y = py2
        If floor.ypos < (p1.Y + 20) Or floor.ypos < (py2 + 20) Then
            Yspeed = 0

        End If
        increase += 1
    End Sub
End Class
