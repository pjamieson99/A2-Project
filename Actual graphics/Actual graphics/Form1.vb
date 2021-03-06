﻿Public Class Form1
    Dim line(2) As LLegs
    Dim P1 As Integer = 100
    Dim up(2) As Boolean
    Dim down(2) As Boolean
    Dim increase(2) As Integer
    Dim speed(2) As Integer

    Dim count(2) As Integer
    Dim clock(2) As Integer
    Dim CheckStatic(2) As Boolean
    Dim joint(2) As JJoints
    Dim rnd As New Random
    Dim RunOnce As Integer = 0
    Dim angle(2) As Integer


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Display_Paint(sender As Object, e As PaintEventArgs) Handles Display.Paint
        If RunOnce = 0 Then
            For x = 0 To 2
                up(x) = True
                down(x) = False
                count(x) = 0
                increase(x) = 1
                speed(x) = rnd.Next(2, 5)
                '  speed(x) = 1
                clock(x) = rnd.Next(10, 70)
                angle(x) = rnd.Next(80, 180)
            Next
        End If
        RunOnce += 1
        Dim g As Graphics
        g = e.Graphics


        For x = 0 To 2

            joint(x) = New JJoints(P1, 100, 10, 10)
            line(x) = New LLegs(P1, 100, angle(x), speed(x), clock(x))
            P1 += 100
        Next
        P1 = 100

        'For x = 0 To 2
        '    line(x).draw(g)
        'Next

        For x = 0 To 2

            joint(x).draw(g)
            If (speed(x) * increase(x)) > (angle(x) / 2) Then
                up(x) = False
                down(x) = False
                If count(x) <> clock(x) Then
                    CheckStatic(x) = True
                    count(x) += 1
                Else
                    count(x) = 0
                    down(x) = True
                End If
            ElseIf (speed(x) * increase(x)) < -(angle(x) / 2) Then
                up(x) = False
                down(x) = False
                If count(x) <> clock(x) Then
                    CheckStatic(x) = True
                    count(x) += 1
                Else
                    count(x) = 0
                    up(x) = True
                End If
            End If
            If count(x) = clock(x) + 1 Then
                count(x) = 0
            End If

            line(x).rotate(g, up(x), down(x), increase(x))

            If up(x) = True Then
                increase(x) += 1
            ElseIf down(x) = True Then
                increase(x) -= 1
            End If

        Next




    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Display.Refresh()
    End Sub
End Class
