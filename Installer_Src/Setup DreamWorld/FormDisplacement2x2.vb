﻿Public Class FormDisplacement2x2

#Region "ScreenSize"
    Public ScreenPosition As ScreenPos
        Private Handler As New EventHandler(AddressOf resize_page)

        'The following detects  the location of the form in screen coordinates
        Private Sub resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
            'Me.Text = "Form screen position = " + Me.Location.ToString
            ScreenPosition.SaveXY(Me.Left, Me.Top)
        End Sub
        Private Sub SetScreen()
            Me.Show()
            ScreenPosition = New ScreenPos(Me.Name)
            AddHandler ResizeEnd, Handler
            Dim xy As List(Of Integer) = ScreenPosition.GetXY()
            Me.Left = xy.Item(0)
            Me.Top = xy.Item(1)
        End Sub

#End Region
        Private Sub FormDisplacement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            SetScreen()
            Form1.gSelectedBox = ""

        End Sub

        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.gSelectedBox = " --displacement <0,256,0> "
        Me.Close()
        End Sub

        Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Form1.gSelectedBox = " --displacement <256,256,0> "
        Me.Close()
        End Sub

        Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Form1.gSelectedBox = " --displacement <0,0,0> "
        Me.Close()
        End Sub

        Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Form1.gSelectedBox = " --displacement <256,0,0> "
        Me.Close()
        End Sub

End Class