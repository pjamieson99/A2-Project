Public Class Joints
    Private Width As Integer
    Private Diameter As Integer
    Private Xpos As Integer
    Private Ypos As Integer
    Private angle As Integer
    Public Sub New(x As Integer, y As Integer, D As Integer, W As Integer, a As Integer)
        Width = W
        Diameter = D
        Xpos = x
        Ypos = y
        angle = a
    End Sub
    Public Sub draw(g As Graphics)
        g.FillEllipse(Brushes.Black, Xpos, Ypos, Diameter, Width)
    End Sub
End Class
