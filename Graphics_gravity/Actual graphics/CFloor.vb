Public Class CFloor
    Private ypos As Integer
    Public Sub New(y As Integer)
        ypos = y
    End Sub

    Public Sub draw(g As Graphics)
        g.DrawLine(Pens.Black, 0, ypos, 1000, ypos)
    End Sub
End Class
