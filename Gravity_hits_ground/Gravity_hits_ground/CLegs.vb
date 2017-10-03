Public Class CLegs
    Public LXpos As Integer
    Public LYpos As Integer
    Public Angle As Integer
    Public Speed As Integer
    Public clock As Integer
    Public p1 As PointF
    Public p2 As PointF
    Public Yspeed As Integer
    Public Sub New(x As Integer, rnd As Random, sp As Integer)
        LXpos = x
        LYpos = 100
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
    End Sub

    Public Sub drop()
        '  LYpos += Yspeed
        p1.Y += Yspeed
        p2.Y += Yspeed
    End Sub

    Sub NewPoints(increase As Integer, floor As CFloor)
        p1.X = ((p1.X - LXpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p1.Y - LYpos) * Math.Sin(Speed * increase * Math.PI / 180)) + LXpos
        p1.Y = ((p1.X - LXpos) * -Math.Sin(Speed * increase * Math.PI / 180)) + ((p1.Y - LYpos) * Math.Cos(Speed * increase * Math.PI / 180)) + LYpos
        p2.X = ((p2.X - LXpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p2.Y - LYpos) * Math.Sin(Speed * increase * Math.PI / 180)) + LXpos
        p2.Y = ((p2.X - LXpos) * -Math.Sin(Speed * increase * Math.PI / 180)) + ((p2.Y - LYpos) * Math.Cos(Speed * increase * Math.PI / 180)) + LYpos
        If floor.ypos < (p1.Y + 50) Or floor.ypos < (p2.Y - 50) Then
            Console.ReadLine()
        End If
    End Sub
End Class
