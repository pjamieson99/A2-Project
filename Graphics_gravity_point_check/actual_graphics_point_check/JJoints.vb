Public Class CJoints
    Private Xpos As Integer
    Private Ypos As Integer
    Private width As Integer
    Private diameter As Integer
    Public JYpos As Integer
    Public JXpos As Integer

    Public Sub New(x, w, d)
        width = w
        diameter = d
        JYpos = 100
        JXpos = x
    End Sub

    Public Sub draw(g As Graphics)
        g.FillEllipse(Brushes.Black, JXpos - 5, JYpos - 5, width, diameter)
    End Sub

    Public Sub drop(g As Graphics)
        Ypos += 10

    End Sub
End Class
