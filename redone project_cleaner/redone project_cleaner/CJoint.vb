Public Class CJoint

    Private width As Integer
    Private diameter As Integer
    Public JYpos As Integer
    Public JXpos As Integer
    Public Yspeed As Integer

    Public Sub New(x As Integer, y As Integer, w As Integer, d As Integer, sp As Integer)
        'set properties
        width = w
        diameter = d
        JYpos = y
        JXpos = x
        Yspeed = sp
    End Sub

    Public Sub draw(g As Graphics)
        'draw
        g.FillEllipse(Brushes.Black, JXpos - 5, JYpos - 5, width, diameter)
    End Sub

    'rise the joint towards the line point 1
    Sub StickToLeg(p1 As PointF)
        JYpos = p1.Y
        JXpos = p1.X
    End Sub

End Class
