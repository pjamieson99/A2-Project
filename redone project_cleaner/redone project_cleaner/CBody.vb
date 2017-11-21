Public Class CBody
    Public Bxpos1
    Public Bxpos2
    Public Bypos1
    Public Bypos2
    Public CoM As PointF
    Public yspeed As Integer
    Public connections(2) As PointF
    Public StaticConnections(2) As PointF
    Public Hypotenuse As Double
    Public AngleIncrease As Integer = 0
    Public Bpy1 As Double
    Public Bpy2 As Double
    Public Bpx1 As Double
    Public Bpx2 As Double
    Private p1 As PointF
    Private p2 As PointF

    Public LeftMomentum As Double = 0.1
    Public RightMomentum As Double = 0.1
    Public Sub New(x1, y1, x2, y2)

        Bypos1 = y1
        Bxpos1 = x1
        Bypos2 = y2
        Bxpos2 = x2
        CoM = New Point((x1 + x2) / 2, (y1 + y2) / 2)
        yspeed = 0
        p1 = New Point(x1, y1)
        p2 = New Point(x2, y2)
        Bpx1 = x1
        Bpx2 = x2
        Bpy1 = y1
        Bpy2 = y2


    End Sub

    Sub BodyPoints(P1Legs() As PointF, GroundLegs As Integer)
        For x = 0 To GroundLegs
            connections(x) = P1Legs(x)
            StaticConnections(x) = P1Legs(x)
        Next
    End Sub

    Sub FallRight(pivotx As Double, pivoty As Double, g As Graphics)
        AngleIncrease -= 1 * RightMomentum
        RightMomentum += 0.2
        For x = 0 To 2
            connections(x).X = ((StaticConnections(x).X - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
            connections(x).Y = ((StaticConnections(x).X - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((StaticConnections(x).Y - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty
        Next
        Bpx1 = ((Bxpos1 - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos1 - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
        Bpy1 = ((Bxpos1 - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((Bypos1 - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty
        Bpx2 = ((Bxpos2 - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos2 - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
        Bpy2 = ((Bxpos2 - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((Bypos2 - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty


    End Sub

    Sub FallLeft(pivotx As Double, pivoty As Double, G As Graphics)
        AngleIncrease += 1 * LeftMomentum
        LeftMomentum += 0.2
        For x = 0 To 2
            connections(x).X = ((StaticConnections(x).X - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((StaticConnections(x).Y - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
            connections(x).Y = ((StaticConnections(x).X - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((StaticConnections(x).Y - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty
        Next
        Bpx1 = ((Bxpos1 - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos1 - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
        Bpy1 = ((Bxpos1 - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((Bypos1 - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty
        Bpx2 = ((Bxpos2 - pivotx) * Math.Cos(AngleIncrease * Math.PI / 180)) + ((Bypos2 - pivoty) * Math.Sin(AngleIncrease * Math.PI / 180)) + pivotx
        Bpy2 = ((Bxpos2 - pivotx) * (-Math.Sin(AngleIncrease * Math.PI / 180))) + ((Bypos2 - pivoty) * Math.Cos(AngleIncrease * Math.PI / 180)) + pivoty

    End Sub

    Public Sub draw(g As Graphics)
        p1.Y = Bpy1
        p1.X = Bpx1
        p2.Y = Bpy2
        p2.X = Bpx2

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
            StaticConnections(x).Y += yspeed
        Next
        Bpy1 += yspeed
        Bpy2 += yspeed
        yspeed += 1
        ResetCoM()
    End Sub

    Public Sub SetPoints(line As CLeg, x As Integer)
        'set points to body 
        line.Lpx2 += connections(x).X - line.Lpx1
        line.Lpy2 += connections(x).Y - line.Lpy1
        line.Lp1 = connections(x)
        ' line.LYpos = line.Lp1.Y
        line.Lpy1 = line.Lp1.Y
        line.Lpx1 = line.Lp1.X

    End Sub

    Sub ResetCoM()
        CoM = New Point((Bpx1 + Bpx2) / 2, (Bpy1 + Bpy2) / 2)
    End Sub

End Class
