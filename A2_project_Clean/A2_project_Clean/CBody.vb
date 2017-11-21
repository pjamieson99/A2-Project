Public Class CBody
    Public Connections(Form1.NumOfLegs) As PointF
    Public StaticConnections(Form1.NumOfLegs) As PointF
    Public CoM As PointF

    Public Bypos1 As Double
    Public Bypos2 As Double
    Public Bxpos1 As Double
    Public Bxpos2 As Double

    Public Yspeed As Integer = 0

    Public BPx1 As Double
    Public BPx2 As Double
    Public BPy1 As Double
    Public BPy2 As Double

    Public AngleIncrease As Integer = 0

    Public BodyRise As Double = 0

    Public LeftMomentum As Double = 0.1
    Public RightMomentum As Double = 0.1

    Public Pivot As PointF

    Public Leftside As Boolean
    Public Rightside As Boolean

    Public Sub New(x1, y1, x2, y2)
        Bypos1 = y1
        Bxpos1 = x1
        Bypos2 = y2
        Bxpos2 = x2
        CoM = New Point((x1 + x2) / 2, (y1 + y2) / 2)
        BPx1 = x1
        Bpx2 = x2
        Bpy1 = y1
        Bpy2 = y2
    End Sub

    Sub BodyPoints(P1Legs As PointF, NumOfLegs As Integer)
        Connections(NumOfLegs - 1) = P1Legs
        StaticConnections(NumOfLegs - 1) = P1Legs
    End Sub

    Public Sub SetPoints(line As CLeg, x As Integer)
        'set points to body 
        line.LPx2 += Connections(x).X - line.LPx1
        line.LPy2 += Connections(x).Y - line.LPy1
        line.LP1 = Connections(x)
        line.LYpos = line.LP1.Y
        line.LPy1 = line.LP1.Y
        line.LPx1 = line.LP1.X

    End Sub

    Sub RaiseBody(Floor As CFloor)
        If Floor.ypos <= BodyRise Then
            BPy1 -= (BodyRise - Floor.ypos)
            BPy2 -= (BodyRise - Floor.ypos)
            For x = 0 To 2
                Connections(x).Y -= (BodyRise - Floor.ypos)
                StaticConnections(x).Y -= (BodyRise - Floor.ypos)
            Next
            Bypos1 -= (BodyRise - Floor.ypos)
            Bypos2 -= (BodyRise - Floor.ypos)
            ResetCoM()

        End If
    End Sub

    Sub ResetCoM()
        CoM = New Point((BPx1 + BPx2) / 2, (BPy1 + BPy2) / 2)
    End Sub

    Sub FindPivot(Line As CLeg, floor As CFloor)
        If Leftside = True And Rightside = False And Line.CheckFloor(floor, Line.Pivot.Y) = True Then

            If Pivot.X < Line.Pivot.X Then
                Pivot = Line.Pivot
            End If

        ElseIf Leftside = False And Rightside = True T And line.checkfloor(floor, line.pivot.y) = True Then

            If Pivot.X > Line.Pivot.X Then
                Pivot = Line.Pivot
            End If

        End If
    End Sub
End Class
