Public Class LLegs

    Private Xpos As Integer
    Private Ypos As Integer
    Private Angle As Integer
    Private Speed As Integer
    Private clock As Integer
    Public Sub New(x As Integer, y As Integer, a As Integer, s As Integer, c As Integer)
        Xpos = x
        Ypos = y
        Angle = a
        Speed = s
        clock = c
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
