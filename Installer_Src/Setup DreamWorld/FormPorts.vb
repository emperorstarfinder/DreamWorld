﻿Imports System.Text.RegularExpressions

Public Class FormPorts

    Dim initted As Boolean

#Region "FormPos"

    Private _screenPosition As ScreenPos
    Private Handler As New EventHandler(AddressOf Resize_page)

    Public Property ScreenPosition As ScreenPos
        Get
            Return _screenPosition
        End Get
        Set(value As ScreenPos)
            _screenPosition = value
        End Set
    End Property

    'The following detects  the location of the form in screen coordinates
    Private Sub Resize_page(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Private Sub Loaded(sender As Object, e As EventArgs) Handles Me.Load

        SetScreen()

        FirstRegionPort.Text = Form1.pMySetting.FirstRegionPort()
        MaxP.Text = "Highest used: " + Form1.pMaxPortUsed.ToString(Form1.Usa)
        uPnPEnabled.Checked = Form1.pMySetting.UPnPEnabled

        'ports
        DiagnosticPort.Text = Form1.pMySetting.DiagnosticPort
        PrivatePort.Text = Form1.pMySetting.PrivatePort
        HTTPPort.Text = Form1.pMySetting.HttpPort

        ExternalHostName.Text = Form1.pMySetting.ExternalHostName

        Form1.HelpOnce("Ports")
        initted = True

    End Sub

    Private Sub UPnPEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles uPnPEnabled.CheckedChanged

        If Not initted Then Return
        Form1.pMySetting.UPnPEnabled = uPnPEnabled.Checked
        Form1.pMySetting.SaveSettings()

    End Sub

#Region "Ports"
    Private Sub ExternalHostName_TextChanged(sender As Object, e As EventArgs) Handles ExternalHostName.LostFocus

        If Not initted Then Return

        Form1.pMySetting.ExternalHostName = ExternalHostName.Text

    End Sub

    Private Sub PrivatePort_TextChanged(sender As Object, e As EventArgs) Handles PrivatePort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        PrivatePort.Text = digitsOnly.Replace(PrivatePort.Text, "")
        Form1.pMySetting.PrivatePort = PrivatePort.Text
        Form1.pMySetting.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub HTTPPort_TextChanged(sender As Object, e As EventArgs) Handles HTTPPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        HTTPPort.Text = digitsOnly.Replace(HTTPPort.Text, "")
        Form1.pMySetting.HttpPort = HTTPPort.Text
        Form1.pMySetting.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub DiagnosticPort_TextChanged(sender As Object, e As EventArgs) Handles DiagnosticPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        DiagnosticPort.Text = digitsOnly.Replace(DiagnosticPort.Text, "")

        Form1.pMySetting.DiagnosticPort = DiagnosticPort.Text
        Form1.pMySetting.SaveSettings()
        Form1.CheckDefaultPorts()

    End Sub

    Private Sub FirstRegionPort_TextChanged_1(sender As Object, e As EventArgs) Handles FirstRegionPort.TextChanged

        If Not initted Then Return

        Dim digitsOnly As Regex = New Regex("[^\d]")
        FirstRegionPort.Text = digitsOnly.Replace(FirstRegionPort.Text, "")
        Form1.pMySetting.FirstRegionPort() = FirstRegionPort.Text
        Form1.pMySetting.SaveSettings()
        Form1.pRegionClass.UpdateAllRegionPorts()

    End Sub

    Private Sub Upnp_Click(sender As Object, e As EventArgs) Handles Upnp.Click

        Form1.Help("Ports")

    End Sub



#End Region

End Class