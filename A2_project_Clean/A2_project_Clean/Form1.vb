Public Class Form1
    Dim Floor As CFloor
    Dim Body As CBody

    Public NumOfLegs As Integer = 3
    Dim NumOfLayers As Integer = 2
    Dim Line(NumOfLegs - 1, NumOfLayers - 1) As CLeg
    Dim Joint(NumOfLegs - 1, NumOfLayers - 1) As CJoint

    Dim Rnd As New Random




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create floor object
        Floor = New CFloor(500)
        Body = New CBody(100, 0, 300, 0)

        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1
                Line(x, y) = New CLeg(50 + 100 * x, 50 + 100 * y, Rnd)
                Joint(x, y) = New CJoint(50 + 100 * x, 50 + 100 * y, 10, 10, 1)
            Next
        Next

        For x = 0 To NumOfLegs - 1
            Body.BodyPoints(Line(x, 0).LP1, NumOfLegs)
        Next
    End Sub




    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'keep display refreshing, loop through display
        Display.Refresh()
    End Sub




    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        Dim g As Graphics
        g = e.Graphics

        'draw the floor
        Floor.draw(g)


        'check if max angle of legs has been reached then rotate legs.
        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1
                Line(x, y).OldPoint1 = Line(x, y).LP1
                Line(x, y).OldPoint2 = Line(x, y).LP2
                Line(x, y).AngleLock(Floor)

                Line(x, y).NewPoints(Body.AngleIncrease)
            Next
        Next


        'attach the legs to the body
        AttachLegs()


        'Find lowest leg past the floor
        Body.BodyRise = 0
        For x = 0 To NumOfLegs - 1
            Line(x, 0).FindLowestLeg(Line(x, 1), Body.BodyRise)
        Next


        'raise the body by the distance between the furthest leg and the floor
        Body.RaiseBody(Floor)


        'attach the legs to the body
        AttachLegs()


        'Find the side of the point touching the floor compared to the centre of mass
        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1

                Line(x, y).FindSideLegisOn(Body, Floor)

            Next
        Next


        'Find the pivot point for the body
        Body.Pivot.Y = 0
        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1

                Body.FindPivot(Line(x, y), Floor)

            Next
        Next


        'make the body fall over based on the pivot
        Body.FallOver(g)


        'attach the legs
        AttachLegs()


        'check if hit floor and if so stop falling
        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1
                Line(x, y).StopFalling(Body, Floor)
            Next
        Next

        Body.Drop()


        'attach the legs
        AttachLegs()


        'raise the body by the distance between the furthest leg and the floor
        Body.RaiseBody(Floor)


        'attach the legs to the body
        AttachLegs()


        'Find the frictional value based on the different frictions from all legs
        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs
                Body.FindFriction(Line(x, y), Floor)
            Next
        Next


        'add the friction to the body that moves it by the distance moved by the leg in sum.
        Body.AddFriction()


        'attach the legs to the body
        AttachLegs()
    End Sub




    Sub AttachLegs()
        'attach top legs to body
        For x = 0 To NumOfLegs - 1
            Body.SetPoints(Line(x, 1), x)
        Next

        'attach bottom legs to top legs
        For x = 0 To NumOfLegs - 1
            Line(x, 0).AttachBottomLegs(x, Body.Connections(x), Line(x, 1))
        Next
    End Sub


    Function CheckFloor(floor As CFloor, point As Double)

        If floor.ypos - 4 <= point Then
            Return True
        Else
            Return False
        End If

    End Function
End Class
