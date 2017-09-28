Public Class CLegs

    Public Xpos As Integer
    Public Ypos As Integer
    Public Angle As Integer
    Public Speed As Integer
    Public clock As Integer
    Public rnd As New Random
    Public Sub New(x As Integer)
        Xpos = x





        Ypos = 100
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

End Class
