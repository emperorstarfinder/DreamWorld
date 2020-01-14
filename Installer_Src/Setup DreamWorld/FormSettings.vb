#Region "Copyright"

' Copyright 2014 Fred Beckhusen for www.Outworldz.com https://opensource.org/licenses/AGPL

'Permission Is hereby granted, free Of charge, to any person obtaining a copy of this software
' And associated documentation files (the "Software"), to deal in the Software without restriction,
'including without limitation the rights To use, copy, modify, merge, publish, distribute, sublicense,
'And/Or sell copies Of the Software, And To permit persons To whom the Software Is furnished To
'Do so, subject To the following conditions:

'The above copyright notice And this permission notice shall be included In all copies Or '
'substantial portions Of the Software.

'THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or IMPLIED,
' INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY, FITNESS For A PARTICULAR
'PURPOSE And NONINFRINGEMENT.In NO Event SHALL THE AUTHORS Or COPYRIGHT HOLDERS BE LIABLE
'For ANY CLAIM, DAMAGES Or OTHER LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or
'OTHERWISE, ARISING FROM, OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER
'DEALINGS IN THE SOFTWARE.Imports System

#End Region

Public Class AdvancedForm
    Implements IDisposable

#Region "Declarations"

    Private Backups As New FormAutoBackups
    Private Bird As New BirdForm
    Dim FormApache As New FormApache
    Dim FormCache As New FormCaches
    Dim FormDatabase As New FormDatabase
    Dim FormDiva As New FormDiva
    Dim FormDNSName As New FormDNSName
    Dim FormPermissions As New FormPermissions
    Dim FormPhysics As New FormPhysics
    Dim FormPorts As New FormPorts
    Dim FormPublicity As New FormPublicity
    Dim FormRegions As New FormRegions
    Dim FormRestart As New FormRestart
    Dim FormServerType As New FormServerType
    Dim Gloebits As New Gloebits
    Dim Icecast As New Icecast
    Dim Logging As New FormLogging
    Dim Maps As New FormMaps
    Dim Scripts As New FormScripts
    Dim Tide As New Tides
    Dim Tos As New TosForm
    Dim Voice As New FormVoice

#End Region

#Region "ScreenSize"

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

#Region "Private Methods"

    Private Sub Advanced_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetScreen()

        Dim x = Threading.Thread.CurrentThread.CurrentUICulture

        Me.Visible = True
        Me.ToolTip1.SetToolTip(Me.TOSButton, My.Resources.Setup_TOS)

    End Sub

#End Region

#Region "Help"

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

        Dim webAddress As String = Form1.SecureDomain + "/Outworldz_installer/technical.htm#Regions"
        Try
            Process.Start(webAddress)
        Catch ex As ObjectDisposedException
        Catch ex As InvalidOperationException
        Catch ex As System.ComponentModel.Win32Exception
        End Try

    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>Dispose!</summary>
    ''' <remarks></remarks>

#End Region

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Visible = False

    End Sub

#Region "Clicks"

    Private Sub ApacheButton_Click(sender As Object, e As EventArgs) Handles ApacheButton.Click

        FormApache.Close()
        FormApache.Dispose()
        FormApache = New FormApache
        FormApache.Activate()
        FormApache.Visible = True
        FormApache.Select()
        FormApache.BringToFront()

    End Sub

    Private Sub BackupButton1_Click(sender As Object, e As EventArgs) Handles BackupButton1.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Backups.Close()
        Backups.Dispose()
        Backups = New FormAutoBackups
        Backups.Activate()
        Backups.Visible = True
        Backups.Select()
        Backups.BringToFront()

    End Sub

    Private Sub Birds_Click(sender As Object, e As EventArgs) Handles Birds.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Bird.Close()
        Bird.Dispose()
        Bird = New BirdForm
        Bird.Activate()
        Bird.Visible = True
        Bird.Select()
        Bird.BringToFront()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles GloebitsButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Gloebits.Close()
        Gloebits.Dispose()
        Gloebits = New Gloebits
        Gloebits.Activate()
        Gloebits.Visible = True
        Gloebits.Select()
        Gloebits.BringToFront()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles PortsButton1.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormPorts.Close()
        FormPorts.Dispose()
        FormPorts = New FormPorts
        FormPorts.Activate()
        FormPorts.Visible = True
        FormPorts.Select()
        FormPorts.BringToFront()

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click

        FormServerType.Close()
        FormServerType.Dispose()
        FormServerType = New FormServerType
        FormCaches.Activate()
        FormServerType.Visible = True
        FormServerType.Select()
        FormServerType.BringToFront()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormPermissions.Close()
        FormPermissions.Dispose()
        FormPermissions = New FormPermissions
        FormPermissions.Activate()
        FormPermissions.Visible = True
        FormPermissions.Select()
        FormPermissions.BringToFront()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormRestart.Close()
        FormRestart.Dispose()
        FormRestart = New FormRestart
        FormRestart.Activate()
        FormRestart.Visible = True
        FormRestart.Select()
        FormRestart.BringToFront()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormPublicity.Close()
        FormPublicity.Dispose()
        FormPublicity = New FormPublicity
        FormPublicity.Activate()
        FormPublicity.Visible = True
        FormPublicity.Select()
        FormPublicity.BringToFront()

    End Sub

    Private Sub CacheButton1_Click(sender As Object, e As EventArgs) Handles CacheButton1.Click

        FormCache.Close()
        FormCache.Dispose()
        FormCache = New FormCaches
        FormCache.Activate()
        FormCache.Visible = True
        FormCache.Select()
        FormCache.BringToFront()

    End Sub

    Private Sub DatabaseButton2_Click(sender As Object, e As EventArgs) Handles DatabaseButton2.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormDatabase.Close()
        FormDatabase.Dispose()
        FormDatabase = New FormDatabase
        FormDatabase.Activate()
        FormDatabase.Visible = True
        FormDatabase.Select()
        FormDatabase.BringToFront()

    End Sub

    Private Sub DivaButton1_Click(sender As Object, e As EventArgs) Handles DivaButton1.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormDiva.Close()
        FormDiva.Dispose()
        FormDiva = New FormDiva
        FormDiva.Activate()
        FormDiva.Visible = True
        FormDiva.Select()
        FormDiva.BringToFront()

    End Sub

    Private Sub DNSButton_Click(sender As Object, e As EventArgs) Handles DNSButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormDNSName.Close()
        FormDNSName.Dispose()
        FormDNSName = New FormDNSName
        FormDNSName.Activate()
        FormDNSName.Visible = True
        FormDNSName.Select()
        FormDNSName.BringToFront()

    End Sub

    Private Sub LoggingButton_Click(sender As Object, e As EventArgs) Handles LoggingButton.Click

        Logging.Close()
        Logging.Dispose()
        Logging = New FormLogging With {
            .Visible = True
        }
        Logging.Activate()
        Logging.Select()
        Logging.BringToFront()
    End Sub

    Private Sub MapsButton_Click(sender As Object, e As EventArgs) Handles MapsButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Maps.Close()
        Maps.Dispose()
        Maps = New FormMaps
        Maps.Activate()
        Maps.Select()
        Maps.Visible = True
        Maps.BringToFront()

    End Sub

    Private Sub PhysicsButton1_Click(sender As Object, e As EventArgs) Handles PhysicsButton1.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        FormPhysics.Close()
        FormPhysics.Dispose()
        FormPhysics = New FormPhysics
        FormPhysics.Activate()
        FormPhysics.Visible = True
        FormPhysics.Select()
        FormPhysics.BringToFront()

    End Sub

    Private Sub RegionsButton1_Click(sender As Object, e As EventArgs) Handles RegionsButton1.Click

        FormRegions.Close()
        FormRegions.Dispose()
        FormRegions = New FormRegions
        FormRegions.Activate()
        FormRegions.Visible = True
        FormRegions.Select()
        FormRegions.BringToFront()

    End Sub

    Private Sub ScriptButton_Click(sender As Object, e As EventArgs) Handles ScriptButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Scripts.Close()
        Scripts.Dispose()
        Scripts = New FormScripts
        Scripts.Activate()
        Scripts.Visible = True
        Scripts.Select()
        Scripts.BringToFront()

    End Sub

    Private Sub Shoutcast_Click(sender As Object, e As EventArgs) Handles Shoutcast.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Icecast.Close()
        Icecast.Dispose()
        Icecast = New Icecast With {
            .Visible = True
        }
        Icecast.Activate()
        Icecast.Select()
        Icecast.BringToFront()

    End Sub

    Private Sub TideButton_Click(sender As Object, e As EventArgs) Handles TideButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Tide.Close()
        Tide.Dispose()
        Tide = New Tides
        Tide.Activate()
        Tide.Visible = True
        Tide.Select()
        Tide.BringToFront()

    End Sub

    Private Sub TOSButton_Click(sender As Object, e As EventArgs) Handles TOSButton.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Tos.Close()
        Tos.Dispose()
        Tos = New TosForm
        Tos.Activate()
        Tos.Visible = True
        Tos.Select()
        Tos.BringToFront()

    End Sub

    Private Sub VoiceButton1_Click(sender As Object, e As EventArgs) Handles VoiceButton1.Click

        ' Set the new form's desktop location so it appears below and to the right of the current form.
        Voice.Close()
        Voice.Dispose()
        Voice = New FormVoice
        Voice.Activate()
        Voice.Visible = True
        Voice.Select()
        Voice.BringToFront()

    End Sub

#End Region

End Class
