Public Class Legs

    Private Xpos1 As Integer
    Private Ypos1 As Integer
    Private Xpos2 As Integer
    Private Ypos2 As Integer
    Dim check As Integer = 0

    Public Sub New(x As Integer, y As Integer, x2 As Integer, y2 As Integer)

        Xpos1 = x
        Ypos1 = y
        Xpos2 = x2
        Ypos2 = y2
    End Sub
    Public Sub draw(g As Graphics, ByVal check As Integer, ByVal angle As Integer)



        g.TranslateTransform(check, Ypos1)
        g.RotateTransform(angle)
        g.DrawLine(Pens.Black, 0, 0, 0, 100)
        g.RotateTransform(-angle)
        g.TranslateTransform(-check, -Ypos1)


    End Sub

End Class

