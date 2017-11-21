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
                Line(x, y).AngleLock(Floor)

                Line(x, y).NewPoints(Body.AngleIncrease)
            Next
        Next


        'attach the legs to the body
        AttachLegs()


        'Find lowest leg past the floor
        Body.BodyRise = 0
        For x = 0 To NumOfLegs
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


        For y = 0 To NumOfLayers - 1
            For x = 0 To NumOfLegs - 1

                Body.FindPivot(Line(x, y), Floor)

            Next
        Next

    End Sub




    Sub AttachLegs()
        'attach top legs to body
        For x = 0 To NumOfLegs - 1
            Body.SetPoints(Line(x, 1), x)
        Next

        'attach bottom legs to top legs
        For x = 0 To NumOfLegs
            Line(x, 0).AttachBottomLegs(x, Body.Connections(x), Line(x, 1))
        Next
    End Sub


End Class
