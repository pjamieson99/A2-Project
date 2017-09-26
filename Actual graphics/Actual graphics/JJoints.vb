Public Class JJoints
    Private Xpos As Integer
    Private Ypos As Integer
    Private width As Integer
    Private diameter As Integer

    Public Sub New(x, y, w, d)
        Xpos = x
        Ypos = y
        width = w
        diameter = d
    End Sub

    Public Sub draw(g As Graphics)
        g.FillEllipse(Brushes.Black, Xpos - 5, Ypos - 5, width, diameter)
    End Sub
End Class
