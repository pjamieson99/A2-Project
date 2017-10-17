Public Class CBody
    Private Bxpos1
    Private Bxpos2
    Public Bypos1
    Public Bypos2
    Public CoM As PointF
    Public yspeed As Integer
    Public connections(2) As PointF
    Public Hypotenuse As Double
    Private AngleIncrease As Integer = 0
    Public Bpy1 As Double
    Public Bpy2 As Double
    Private px1 As Double
    Private px2 As Double
    Private p1 As PointF
    Private p2 As PointF
    Public Sub New(x1, y1, x2, y2)

        Bypos1 = y1
        Bxpos1 = x1
        Bypos2 = y2
        Bxpos2 = x2
        CoM = New Point((x1 + x2) / 2, (y1 + y2) / 2)
        yspeed = 0
        p1 = New Point(x1, y1)
        p2 = New Point(x2, y2)
        px1 = x1
        px2 = x2
        Bpy1 = y1
        Bpy2 = y2


    End Sub

    Sub BodyPoints(P1Legs() As PointF, GroundLegs As Integer)
        For x = 0 To GroundLegs
            connections(x) = P1Legs(x)
        Next
    End Sub

    Sub FallRight(pivot As PointF, g As Graphics)
        AngleIncrease += 1
        For x = 0 To 2
            connections(x).X = ((connections(x).X - pivot.X) * Math.Cos((10) * AngleIncrease * Math.PI / 180)) + ((connections(x).Y - pivot.Y) * Math.Sin((10) * Math.PI / 180)) + pivot.X
            connections(x).Y = ((connections(x).X - pivot.X) * (-Math.Sin((10) * AngleIncrease * Math.PI / 180))) + ((connections(x).Y - pivot.Y) * Math.Cos((10) * Math.PI / 180)) + pivot.Y
        Next
        px1 = ((Bxpos1 - pivot.X) * Math.Cos((10) * AngleIncrease * Math.PI / 180)) + ((Bypos1 - pivot.Y) * Math.Sin((10) * AngleIncrease * Math.PI / 180)) + pivot.X
        Bpy1 = ((Bxpos1 - pivot.Y) * (-Math.Sin((10) * AngleIncrease * Math.PI / 180))) + ((Bypos1 - pivot.Y) * Math.Cos((10) * AngleIncrease * Math.PI / 180)) + pivot.Y
        px2 = ((Bxpos2 - pivot.X) * Math.Cos((10) * AngleIncrease * Math.PI / 180)) + ((Bypos2 - pivot.Y) * Math.Sin((10) * AngleIncrease * Math.PI / 180)) + pivot.X
        Bpy2 = ((Bxpos2 - pivot.Y) * (-Math.Sin((10) * AngleIncrease * Math.PI / 180))) + ((Bypos2 - pivot.Y) * Math.Cos((10) * AngleIncrease * Math.PI / 180)) + pivot.Y


    End Sub



    Public Sub draw(g As Graphics)
        p1.Y = Bpy1
        p1.X = px1
        p2.Y = Bpy2
        p2.X = px2
        g.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y)
        p1.X = Bxpos1
        p1.Y = Bypos1
        p2.X = Bxpos2
        p2.Y = Bypos2

    End Sub

    Public Sub drop()
        'add speed to fall
        Bypos1 += yspeed
        Bypos2 += yspeed

        For x = 0 To 2
            connections(x).Y += yspeed
        Next
        Bpy1 += yspeed
        Bpy2 += yspeed
        yspeed += 1
    End Sub




End Class
