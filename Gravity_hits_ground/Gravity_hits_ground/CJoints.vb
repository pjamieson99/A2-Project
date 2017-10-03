Public Class CJoints

    Private Xpos As Integer
    Private Ypos As Integer
    Private width As Integer
    Private diameter As Integer
    Public JYpos As Integer
    Public JXpos As Integer
    Public Yspeed As Integer

        Public Sub New(x As Integer, w As Integer, d As Integer, sp As Integer)
            width = w
            diameter = d
        JYpos = 100
        JXpos = x
        Yspeed = sp
        End Sub

        Public Sub draw(g As Graphics)
            g.FillEllipse(Brushes.Black, JXpos - 5, JYpos - 5, width, diameter)
        End Sub

    Public Sub drop()
        JYpos += Yspeed
    End Sub


End Class
