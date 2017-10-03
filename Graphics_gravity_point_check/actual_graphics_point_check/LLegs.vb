Public Class CLegs

    Public Xpos As Integer
    Public Ypos As Integer
    Public Angle As Integer
    Public Speed As Integer
    Public clock As Integer
    Public p1 As PointF
    Public p2 As PointF

    Public Sub New(x As Integer, rnd As Random)
        Xpos = x
        Ypos = 100
        p1 = New Point(Xpos, Ypos)
        p2 = New Point(Xpos, Ypos + 100)




        Speed = rnd.Next(2, 5)
        clock = rnd.Next(10, 70)
        Angle = rnd.Next(45, 180)

    End Sub

    Public Sub draw(g As Graphics)
        g.DrawLine(Pens.Black, 0, 0, 0, 100)
    End Sub

    Public Sub rotate(g As Graphics, Up As Boolean, down As Boolean, increase As Integer)
        If Up = True Then


            g.TranslateTransform(Xpos, Ypos)
            g.RotateTransform(Speed * increase)
            draw(g)
            g.RotateTransform(-Speed * increase)
            g.TranslateTransform(-Xpos, -Ypos)

            '          g.RotateTransform(-Speed)
            'ElseIf down = True Then
            '    g.TranslateTransform(Xpos, Ypos)
            '    g.RotateTransform(-Speed)

        ElseIf down = True Then

            g.TranslateTransform(Xpos, Ypos)
            g.RotateTransform(Speed * increase)
            draw(g)
            g.RotateTransform(-Speed * increase)
            g.TranslateTransform(-Xpos, -Ypos)

        Else
            g.TranslateTransform(Xpos, Ypos)
            g.RotateTransform(Speed * increase)
            draw(g)
            g.RotateTransform(-Speed * increase)
            g.TranslateTransform(-Xpos, -Ypos)
        End If

    End Sub

    Sub NewPoints(increase As Integer, floor As CFloor)
        p1.X = ((p1.X - Xpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p1.Y - Ypos) * Math.Sin(Speed * increase * Math.PI / 180)) + Xpos
        p1.Y = ((p1.X - Xpos) * -Math.Sin(Speed * increase * Math.PI / 180)) + ((p1.Y - Ypos) * Math.Cos(Speed * increase * Math.PI / 180)) + Ypos
        p2.X = ((p2.X - Xpos) * Math.Cos(Speed * increase * Math.PI / 180)) + ((p2.Y - Ypos) * Math.Sin(Speed * increase * Math.PI / 180)) + Xpos
        p2.Y = ((p2.X - Xpos) * -Math.Sin(Speed * increase * Math.PI / 180)) + ((p2.Y - Ypos) * Math.Cos(Speed * increase * Math.PI / 180)) + Ypos
        If floor.ypos < (p1.Y + 50) Or floor.ypos < (p2.Y - 50) Then
            Console.ReadLine()
        End If
    End Sub



End Class
