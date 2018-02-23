﻿
#Region "Copyright"
' Copyright 2014 Fred Beckhusen for Outworldz.com
' https://opensource.org/licenses/AGPL

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

Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports IWshRuntimeLibrary
Imports IniParser
Imports System.Threading
Imports System.Runtime.InteropServices


Public Class Form1

#Region "Declarations"


    Dim MyVersion As String = "2.11"
    Dim DebugPath As String = "C:\Opensim\Outworldz Source"  ' no slash at end
    Public Domain As String = "http://www.outworldz.com"
    Public prefix As String ' Holds path to Opensim folder

    Public MyFolder As String   ' Holds the current folder that we are running in
    Dim gCurSlashDir As String '  holds the current directory info in Unix format
    Public isRunning As Boolean = False
    Dim Arnd As Object = New Random()
    Public gChatTime As Integer
    Dim client As New System.Net.WebClient

    ' Processes
    Dim pMySqlDiag As Process = New Process()
    Dim ProcessUpnp As Process = New Process()
    Public Shared ActualForm As AdvancedForm

    ' with events
    Private pMySql As Process = New Process()
    Public Event Exited As EventHandler

    Dim Data As IniParser.Model.IniData
    Private randomnum As New Random
    Dim parser As IniParser.FileIniDataParser
    Dim gINI As String  ' the name of the current INI file we are writing

    ' robust errors and startup
    Public gRobustProcID As Integer
    Private WithEvents RobustProcess As New Process()

    Public Event RobustExited As EventHandler
    Private images As List(Of Image) = New List(Of Image) From {My.Resources.tangled, My.Resources.wp_habitat, My.Resources.wp_Mooferd,
                             My.Resources.wp_To_Piers_Anthony,
                             My.Resources.wp_wavy_love_of_animals, My.Resources.wp_zebra,
                             My.Resources.wp_Que, My.Resources.wp_1, My.Resources.wp_2,
                             My.Resources.wp_3, My.Resources.wp_4, My.Resources.wp_5,
                             My.Resources.wp_6, My.Resources.wp_7, My.Resources.wp_8,
                             My.Resources.wp_9, My.Resources.wp_10, My.Resources.wp_11,
                             My.Resources.wp_12, My.Resources.wp_13, My.Resources.wp_14,
                             My.Resources.wp_15, My.Resources.wp_16, My.Resources.wp_17,
                             My.Resources.wp_18, My.Resources.wp_19, My.Resources.wp_20,
                             My.Resources.wp_21, My.Resources.wp_22, My.Resources.wp_23,
                             My.Resources.wp_24, My.Resources.wp_25, My.Resources.wp_26,
                             My.Resources.wp_27, My.Resources.wp_28, My.Resources.wp_29,
                             My.Resources.wp_30, My.Resources.wp_31, My.Resources.wp_32,
                             My.Resources.wp_33, My.Resources.wp_34, My.Resources.wp_35,
                             My.Resources.wp_36, My.Resources.wp_37, My.Resources.wp_38,
                             My.Resources.wp_39, My.Resources.wp_40, My.Resources.wp_41,
                             My.Resources.wp_42, My.Resources.wp_43, My.Resources.wp_44,
                             My.Resources.wp_45, My.Resources.wp_46, My.Resources.wp_47,
                             My.Resources.wp_48, My.Resources.wp_49, My.Resources.wp_50,
                             My.Resources.wp_51, My.Resources.wp_52, My.Resources.wp_53,
                             My.Resources.wp_54, My.Resources.wp_55, My.Resources.wp_56,
                             My.Resources.wp_57, My.Resources.fairy
                            }
    Dim gDebug As Boolean = False       ' toggled by -debug flag on command line
    Dim gContentAvailable As Boolean = False ' assume there is no OAR and IAR data available
    Public MyUPnpMap As UPnp
    Dim ws As NetServer
    Public RegionClass As RegionMaker
    Dim RegionHandles(50) As Boolean
    Dim gStopping As Boolean = False
    Dim Timertick As Integer        ' counts the seconds uintil wallpaper changes
    Public Shared MysqlConn As Mysql    ' object lets us query Mysql database
    Private Diagsrunning As Boolean = False
    Dim gDNSSTimer As Integer = 0
    Dim gUseIcons As Boolean = True
    Dim gIPv4Address As String
    Public MySetting As New MySettings

    <CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId:="1")>
    <CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible")>
    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")>
    <DllImport("user32.dll")>
    Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal windowName As String) As Boolean
    End Function

#End Region

#Region "Events"
    Private WithEvents MyProcess1 As New Process()
    Private WithEvents MyProcess2 As New Process()
    Private WithEvents MyProcess3 As New Process()
    Private WithEvents MyProcess4 As New Process()
    Private WithEvents MyProcess5 As New Process()
    Private WithEvents MyProcess6 As New Process()
    Private WithEvents MyProcess7 As New Process()
    Private WithEvents MyProcess8 As New Process()
    Private WithEvents MyProcess9 As New Process()
    Private WithEvents MyProcess10 As New Process()
    Private WithEvents MyProcess11 As New Process()
    Private WithEvents MyProcess12 As New Process()
    Private WithEvents MyProcess13 As New Process()
    Private WithEvents MyProcess14 As New Process()
    Private WithEvents MyProcess15 As New Process()
    Private WithEvents MyProcess16 As New Process()
    Private WithEvents MyProcess17 As New Process()
    Private WithEvents MyProcess18 As New Process()
    Private WithEvents MyProcess19 As New Process()
    Private WithEvents MyProcess20 As New Process()
    Private WithEvents MyProcess21 As New Process()
    Private WithEvents MyProcess22 As New Process()
    Private WithEvents MyProcess23 As New Process()
    Private WithEvents MyProcess24 As New Process()
    Private WithEvents MyProcess25 As New Process()
    Private WithEvents MyProcess26 As New Process()
    Private WithEvents MyProcess27 As New Process()
    Private WithEvents MyProcess28 As New Process()
    Private WithEvents MyProcess29 As New Process()
    Private WithEvents MyProcess30 As New Process()
    Private WithEvents MyProcess31 As New Process()
    Private WithEvents MyProcess32 As New Process()
    Private WithEvents MyProcess33 As New Process()
    Private WithEvents MyProcess34 As New Process()
    Private WithEvents MyProcess35 As New Process()
    Private WithEvents MyProcess36 As New Process()
    Private WithEvents MyProcess37 As New Process()
    Private WithEvents MyProcess38 As New Process()
    Private WithEvents MyProcess39 As New Process()
    Private WithEvents MyProcess40 As New Process()
    Private WithEvents MyProcess41 As New Process()
    Private WithEvents MyProcess42 As New Process()
    Private WithEvents MyProcess43 As New Process()
    Private WithEvents MyProcess44 As New Process()
    Private WithEvents MyProcess45 As New Process()
    Private WithEvents MyProcess46 As New Process()
    Private WithEvents MyProcess47 As New Process()
    Private WithEvents MyProcess48 As New Process()
    Private WithEvents MyProcess49 As New Process()
    Private WithEvents MyProcess50 As New Process()


#End Region

#Region "Properties"

    Public Property IPv4Address() As String
        Get
            Return gIPv4Address
        End Get
        Set(ByVal Value As String)
            gIPv4Address = Value
        End Set
    End Property
    Public Property Splashpage() As String
        Get
            Return MySetting.SplashPage
        End Get
        Set(ByVal Value As String)
            MySetting.SplashPage = Value

            MySetting.SaveMyINI()
        End Set
    End Property

    ' Save a random machine ID - we don't want any data to be sent that's personal or identifiable,  but it needs to be unique
    Public Property Machine() As String
        Get
            Return MySetting.MachineID
        End Get
        Set(ByVal Value As String)
            If (MySetting.MachineID = "") Then
                MySetting.MachineID = Value
                MySetting.SaveMyINI()
            End If
        End Set
    End Property

    Public Property Running() As Boolean
        Get
            Return isRunning
        End Get
        Set(ByVal Value As Boolean)
            isRunning = Value
        End Set
    End Property

#End Region

#Region "StartStop"

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        MyFolder = My.Application.Info.DirectoryPath

        If MyFolder.Contains("Setup DreamWorld\bin") Then
            ' for debugging when compiling
            gDebug = False ' set to true to fail all kinds of tests :-)
            MyFolder = DebugPath ' for testing, as the compiler buries itself in ../../../debug
        End If
        gCurSlashDir = MyFolder.Replace("\", "/")    ' because Mysql uses unix like slashes, that's why
        prefix = MyFolder & "\OutworldzFiles\Opensim\"

        MySetting.Init()

        'hide progress
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = 0

        If MySetting.MyX > 2000 Or MySetting.MyY > 2000 Then
            Me.CenterToScreen()
        ElseIf MySetting.MyX < 0 Or MySetting.MyY < 0 Then
            Me.CenterToScreen()
        ElseIf MySetting.MyX = 0 And MySetting.MyY = 0 Then
            Me.CenterToScreen()
        Else
            Me.Location = New Point(MySetting.MyX, MySetting.MyY)
        End If

        TextBox1.BackColor = Me.BackColor
        TextBox1.AllowDrop = True

        PictureBox1.Image = My.Resources.ResourceManager.GetObject("fairy")
        PictureBox1.Enabled = True
        PictureBox1.AllowDrop = True

        LogButton.Hide()
        IgnoreButton.Hide()
        Buttons(BusyButton)

        ' hide updater
        UpdaterGo.Visible = False
        UpdaterCancel.Visible = False

        ' WebUI
        ViewWebUI.Visible = MySetting.WifiEnabled

        Me.Text = "Outworldz Dreamgrid V" + MyVersion

        gChatTime = MySetting.ChatTime

        Running = False ' true when opensim is running
        Me.Show()

        RegionClass = RegionMaker.Instance

        SaySomething()

        ClearLogFiles() ' clear log fles

        MyUPnpMap = New UPnp(MyFolder)

#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        MySetting.PublicIP = MyUPnpMap.LocalIP
        MySetting.PrivateURL = MySetting.PublicIP
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

        MySetting.SaveMyINI()

        If (MySetting.SplashPage = "") Then
            MySetting.SplashPage = Domain + "/Outworldz_installer/Welcome.htm"
            MySetting.SaveMyINI()

		end If

        ProgressBar1.Value = 100
        ProgressBar1.Value = 0

        If Not MySetting.SkipUpdateCheck Then
            CheckForUpdates()
        End If

        CheckDefaultPorts()

        ' must start after region Class is instantiated
        ws = NetServer.GetWebServer
        Log("Info:Starting Web Server ")

        ws.StartServer(MyFolder, MySetting.PrivateURL, MySetting.DiagnosticPort)

        ' Run diagnostics, maybe
        If Not MySetting.RanAllDiags Or gDebug Then
            DoDiag()
            MySetting.RanAllDiags = True
            MySetting.SaveMyINI()
        End If

        If Not SetIniData() Then Return

        mnuSettings.Visible = True
        SetIAROARContent() ' load IAR and OAR web content

        If MySetting.Password = "secret" Then
            BumpProgress10()
            Dim Password = New PassGen
            MySetting.Password = Password.GeneratePass()
            MySetting.SaveMyINI()
        End If

        ' Find out if the viewer is installed
        If System.IO.File.Exists(MyFolder & "\OutworldzFiles\Init.txt") Then

            Buttons(StartButton)
            ProgressBar1.Value = 100

            If MySetting.Autostart Then
                Print("Auto Startup")
                Startup()
            Else
                Print("Ready to Launch! Click 'Start' to begin your adventure in Opensimulator.")
            End If

        Else

            Print("Installing Desktop icon clicky thingy")
            Create_ShortCut(MyFolder & "\Start.exe")
            BumpProgress10()

            If SetPublicIP() Then
                OpenPorts()
            End If

            Try
                ' mark the system as ready
                Using outputFile As New StreamWriter(MyFolder & "\OutworldzFiles\Init.txt", True)
                    outputFile.WriteLine("This file lets Outworldz know it has been installed")
                End Using
            Catch ex As Exception
                Log("Error:Could not create Init.txt - no permissions to write it:" + ex.Message)
            End Try

            Print("Ready to Launch!")
            Buttons(StartButton)
        End If

        ProgressBar1.Value = 100
        Application.DoEvents()


    End Sub

    Private Sub StartButton_Click(sender As System.Object, e As System.EventArgs) Handles StartButton.Click

        Startup()

    End Sub

    Private Sub Startup()

        ProgressBar1.Value = 0
        ProgressBar1.Visible = True
        Buttons(BusyButton)
        Running = True
        MapDelete()
        RegionClass.GetAllRegions()

        If SetPublicIP() Then
            OpenPorts()
        End If


        If Not SetIniData() Then Return   ' set up the INI files

        If Not StartMySQL() Then Return

        If Not Start_Robust() Then
            Return
        End If

        If Not MySetting.RunOnce Then
            MsgBox("Please type 'create user<ret>' to make the system owner's account in the ROBUST console, and then answer any questions.", vbInformation, "Info")
            Sleep(10000)
            MySetting.RunOnce = True

            MySetting.SaveMyINI()
        End If

        If Not Start_Opensimulator() Then ' Launch the rockets
            Return
        End If

        ' show the IAR and OAR menu when we are up 
        If gContentAvailable Then
            IslandToolStripMenuItem.Visible = True
            ClothingInventoryToolStripMenuItem.Visible = True
        End If

        Buttons(StopButton)
        ProgressBar1.Value = 100
        Print("Outworldz is almost ready for you to log in.  Wait for INITIALIZATION COMPLETE - LOGINS ENABLED to appear in the console, and you can log in." + vbCrLf _
              + " Hypergrid address is http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

        ' done with bootup
        ProgressBar1.Visible = False

        Timer1.Interval = 1000
        Timer1.Start() 'Timer starts functioning

        Me.AllowDrop = True

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        Dim p As Point
        p = Me.Location

        Try
            ws.StopWebServer()
        Catch
        End Try

        MySetting.MyX = p.X
        MySetting.MyY = p.Y

        ProgressBar1.Value = 90

        Print("Hold fast to your dreams ...")
        KillAll()
        ProgressBar1.Value = 10
        Print("I'll tell you my next dream when I wake up.")
        StopMysql()
        ProgressBar1.Value = 5
        Print("Zzzz...")
        ProgressBar1.Value = 0

    End Sub

    Private Sub MnuExit_Click(sender As System.Object, e As System.EventArgs) Handles mnuExit.Click
        Log("Info:Exiting")
        End
    End Sub

    Private Sub ShutdownNowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Print("Stopping")
        Application.DoEvents()
        KillAll()
        Buttons(StartButton)
        Print("")
    End Sub

    Private Sub KillAll()

        Timer1.Stop()
        gStopping = True
        ProgressBar1.Value = 100
        ProgressBar1.Visible = True
        ' close everything as gracefully as possible.

        Application.DoEvents()

        Dim n As Integer = RegionClass.RegionCount()
        Debug.Print("N=" + n.ToString())


        Dim counter = 50
        While counter
            RegionHandles(counter) = False
            counter = counter - 1
        End While

        Dim ctr = 0
        For Each X As Integer In RegionClass.RegionNumbers
            Dim PID As Integer = RegionClass.ProcessID(ctr)
            If PID Then
                RegionClass.ShuttingDown(ctr) = True
                RegionClass.Booted(ctr) = False
                RegionClass.WarmingUp(ctr) = False
                Print("Shutting down " + RegionClass.RegionName(ctr))
                ConsoleCommand(PID, "quit{ENTER}")
            End If
            Application.DoEvents()
            ctr = ctr + 1
        Next

        ' only wait if the port 8001 is working
        If gUseIcons Then
            Print("Waiting for all regions to exit")

            counter = 300 ' 5 minutes to quit all regions
            While (counter)
                ' decrement progress bar according to the ratio of what we had / what we have now
                Dim n2 As Integer = RegionClass.RegionCount()
                'Debug.Print("N2=" + n2.ToString())
                If n Then
                    ProgressBar1.Value = n2 / n * 100
                    'Debug.Print("V=" + ProgressBar1.Value.ToString)
                End If
                Sleep(1000)

                counter = counter - 1
                Dim isRunning As Boolean = False

                For Each X In RegionClass.RegionNumbers
                    If RegionClass.ProcessID(X) Then
                        isRunning = True
                    End If
                    Application.DoEvents()
                Next
                If Not isRunning Then counter = 0
            End While
        End If

        For Each X As Integer In RegionClass.RegionNumbers
            RegionClass.ShuttingDown(X) = False
            RegionClass.Booted(X) = False
            RegionClass.WarmingUp(X) = False
        Next

        If gRobustProcID Then
            ConsoleCommand(gRobustProcID, "quit{ENTER}")
            Me.Focus()
        End If

        ' cannot load OAR or IAR, either
        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False

        Running = False
        Me.AllowDrop = False

        ProgressBar1.Value = 0
        Application.DoEvents()

        gStopping = False

    End Sub

    Private Function Zap(processName As String) As Boolean
        ' Kill process by name
        For Each P As Process In System.Diagnostics.Process.GetProcessesByName(processName)
            Try
                Log("Info:Stopping process " + processName)
                P.Kill()
                Return True
            Catch ex As Exception
                Log("Info:failed to stop " + processName)
                Return False
            End Try
        Next
        Zap = False
    End Function

#End Region

#Region "Menus"

    Private Sub ConsoleCOmmandsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConsoleCOmmandsToolStripMenuItem1.Click
        Dim webAddress As String = "http://opensimulator.org/wiki/Server_Commands"
        Process.Start(webAddress)
    End Sub


    Private Sub Busy_Click(sender As System.Object, e As System.EventArgs)
        ' Busy click shuts us down
        Dim result As Integer = MessageBox.Show("Do you want to Abort?", "caption", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Print("Stopping")

            Buttons(StartButton)
            Print("Stopped")
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Function Buttons(button As System.Object) As Boolean
        ' Turns off all 4 stacked buttons, then enables one of them
        BusyButton.Visible = False
        StopButton.Visible = False
        StartButton.Visible = False
        InstallButton.Visible = False
        button.Visible = True
        Buttons = True
    End Function

    Private Sub Create_ShortCut(ByVal sTargetPath As String)
        ' Requires reference to Windows Script Host Object Model
        Dim WshShell As WshShellClass = New WshShellClass
        Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
        Log("Info:creating shortcut on desktop")
        ' The shortcut will be created on the desktop
        Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\Outworldz.lnk"), IWshRuntimeLibrary.IWshShortcut)
        MyShortcut.TargetPath = sTargetPath
        MyShortcut.IconLocation = WshShell.ExpandEnvironmentStrings(MyFolder & "\Start.exe")
        MyShortcut.WorkingDirectory = MyFolder
        MyShortcut.Save()

    End Sub

    Public Sub Print(Value As String)

        Log("Info:" + Value)
        PictureBox1.Visible = False
        TextBox1.Text = Value
        TextBox1.Visible = True
        Application.DoEvents()
        Sleep(gChatTime)  ' time to read
        Application.DoEvents()

    End Sub
    Public Sub PrintFast(Value As String)

        Log("Info:" + Value)
        PictureBox1.Visible = False
        TextBox1.Visible = True
        TextBox1.Text = Value
        Application.DoEvents()

    End Sub

    Private Sub MnuAbout_Click(sender As System.Object, e As System.EventArgs) Handles mnuAbout.Click
        Print("(c) 2017 Outworldz,LLC")
        Dim webAddress As String = Domain + "/Outworldz_Installer"
        Process.Start(webAddress)

    End Sub

    Private Sub StopButton_Click_1(sender As System.Object, e As System.EventArgs) Handles StopButton.Click

        Print("Stopping")
        ProgressBar1.Value = 100
        Buttons(BusyButton)
        KillAll()
        Buttons(StartButton)
        Print("Stopped")
        ProgressBar1.Value = 0
        ProgressBar1.Visible = 0
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mnuShow.Click

        Print("The Opensimulator Console will be shown when Opensim is running.")
        mnuShow.Checked = True
        mnuHide.Checked = False

        MySetting.ConsoleShow = mnuShow.Checked

        MySetting.SaveMyINI()
        If Running Then
            Print("The Opensimulator Console will be shown the next time the system is started.")
        End If

    End Sub

    Private Sub MnuHide_Click(sender As System.Object, e As System.EventArgs) Handles mnuHide.Click
        Print("The Opensimulator Console will not be shown. You can still interact with it with Help->Opensim Console")
        mnuShow.Checked = False
        mnuHide.Checked = True

        MySetting.ConsoleShow = mnuShow.Checked
        MySetting.SaveINI()
        If Running Then
            Print("The Opensimulator Console will not be shown. Change will occur when Opensim is restarted")
        End If

    End Sub

    Public Function Random() As String
        Dim value As Integer = CInt(Int((600000000 * Rnd()) + 1))
        Random = System.Convert.ToString(value)
    End Function

    Private Sub WebUIToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Print("The Web UI lets you add or view settings for the default avatar. ")
        If Running Then
            Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort
            Process.Start(webAddress)
        End If
    End Sub

#End Region

#Region "INI"


    Private Sub SetDefaultSims()

        Dim reader As System.IO.StreamReader
        Dim line As String
        Dim DefaultName As String = ""

        Try
            ' add this sim name as a default to the file as HG regions, and add the other regions as fallback

            ' it may have been deleted
            Dim o As Integer = RegionClass.FindRegionByName(MySetting.WelcomeRegion)

            If o < 0 Then
                o = 0
            End If
            ' save to disk
            DefaultName = RegionClass.RegionName(o)
            MySetting.WelcomeRegion = DefaultName
            MySetting.SaveINI()


            '(replace spaces with underscore)
            DefaultName = DefaultName.Replace(" ", "_")    ' because this is a screwy thing they did in the INI file

            Dim onceflag As Boolean = False ' only do the DefaultName
            Dim counter As Integer = 0

            Try
                My.Computer.FileSystem.DeleteFile(prefix + "bin\Robust.tmp")
            Catch ex As Exception
                'Nothing to do, this was just cleanup
            End Try

            Using outputFile As New StreamWriter(prefix + "bin\Robust.tmp")
                reader = System.IO.File.OpenText(prefix + "bin\Robust.HG.ini")
                'now loop through each line
                While reader.Peek <> -1
                    line = reader.ReadLine()

                    If line.Contains("DefaultHGRegion, FallbackRegion") Then
                        ' flag lets us skip multi-lines
                        If onceflag = False Then
                            onceflag = True
                            line = "Region_" + DefaultName + " = " + """" + "DefaultRegion, DefaultHGRegion, FallbackRegion" + """"
                            outputFile.WriteLine(line)
                        End If
                    Else
                        outputFile.WriteLine(line)
                    End If
                    Application.DoEvents()
                End While
            End Using
            'close your reader
            reader.Close()

            Try
                Try
                    My.Computer.FileSystem.DeleteFile(prefix + "bin\Robust.HG.ini.bak")
                Catch ex As Exception
                    'Nothing to do, this was just cleanup
                End Try
                My.Computer.FileSystem.RenameFile(prefix + "bin\Robust.HG.ini", "Robust.HG.ini.bak")
                My.Computer.FileSystem.RenameFile(prefix + "bin\Robust.tmp", "Robust.HG.ini")
            Catch ex As Exception
                Log("Error:SetDefault sims could not rename the file:" + ex.Message)
                My.Computer.FileSystem.RenameFile(prefix + "bin\Robust.HG.ini.bak", "Robust.HG.ini")
            End Try

        Catch ex As Exception
            MsgBox("Could not set default sim for visitors. Check the Common Settings panel.", vbInformation, "Settings")
        End Try

    End Sub

    Private Function SetIniData() As Boolean

        'mnuShow shows the DOS box for Opensimulator
        mnuShow.Checked = MySetting.ConsoleShow
        mnuHide.Checked = Not MySetting.ConsoleShow

        If MySetting.ConsoleShow Then
            Log("Info:Console will be shown")
        Else
            Log("Info:Console will not be shown")
        End If

        Print("Saving all settings")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' set the defaults in the INI for the viewer to use. Painful to do as it's a Left hand side edit 

        SetDefaultSims()
        ''''''''''''''''''''''''''''''''''''''''''''''''
        ' Robust 
        ' Grid regions need GridDBName
        MySetting.SaveINI()

        MySetting.LoadIni(prefix + "bin\config-include\Gridcommon.ini", ";")
        Dim ConnectionString = """" _
            + "Data Source=" + "127.0.0.1" _
            + ";Database=" + MySetting.RegionDBName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RegionDBUsername _
            + ";Password=" + MySetting.RegionDbPassword _
            + ";Old Guids=True;Allow Zero Datetime=True;" _
            + """"
        MySetting.SetIni("DatabaseService", "ConnectionString", ConnectionString)
        MySetting.SaveINI()

        ''''''''''''''''''''''''''''''''''''''''''
        ' Robust Process
        MySetting.LoadIni(prefix + "bin\Robust.HG.ini", ";")

        ConnectionString = """" _
            + "Data Source=" + MySetting.RobustServer _
            + ";Database=" + MySetting.RobustMySqlName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RobustMySqlUsername _
            + ";Password=" + MySetting.RobustMySqlPassword _
            + ";Old Guids=True;Allow Zero Datetime=True;" _
            + """"

        MySetting.SetIni("DatabaseService", "ConnectionString", ConnectionString)
        MySetting.SetIni("Const", "GridName", MySetting.SimName)
        MySetting.SetIni("Const", "BaseURL", "http://" + MySetting.PublicIP)
        MySetting.SetIni("Const", "PublicPort", MySetting.HttpPort) ' 8002
        MySetting.SetIni("Const", "PrivatePort", MySetting.PrivatePort) ' 8003
        MySetting.SetIni("Const", "http_listener_port", MySetting.HttpPort)
        MySetting.SetIni("GridInfoService", "welcome", Splashpage)

        If MySetting.WebStats Then
            MySetting.SetIni("WebStats", "enabled", "True")
        Else
            MySetting.SetIni("WebStats", "enabled", "False")
        End If

        MySetting.SaveINI()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Opensim.ini
        MySetting.LoadIni(prefix + "bin\Opensim.proto", ";")


        If (MySetting.Region_owner_is_god Or MySetting.Region_manager_is_god) Then
            MySetting.SetIni("Permissions", "allow_grid_gods", "true")
        Else
            MySetting.SetIni("Permissions", "allow_grid_gods", "false")
        End If

        If (MySetting.Region_owner_is_god) Then
            MySetting.SetIni("Permissions", "region_owner_is_god", "true")
        Else
            MySetting.SetIni("Permissions", "region_owner_is_god", "false")
        End If

        If (MySetting.Region_manager_is_god) Then
            MySetting.SetIni("Permissions", "region_manager_is_god", "true")
        Else
            MySetting.SetIni("Permissions", "region_manager_is_god", "false")
        End If

        ' Physics
        ' choices for meshmerizer, where Ubit's ODE requires a special one
        ' mesging = ZeroMesher
        ' meshing = Meshmerizer
        ' meshing = ubODEMeshmerizer

        ' 0 = physics = none
        ' 1 = OpenDynamicsEngine
        ' 2 = physics = BulletSim
        ' 3 = physics = BulletSim with threads
        ' 4 = physics = ubODE

        Select Case MySetting.Physics
            Case 0
                MySetting.SetIni("Startup", "meshing", "ZeroMesher")
                MySetting.SetIni("Startup", "physics", "basicphysics")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "false")
            Case 1
                MySetting.SetIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetIni("Startup", "physics", "OpenDynamicsEngine")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "false")
            Case 2
                MySetting.SetIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetIni("Startup", "physics", "BulletSim")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "false")
            Case 3
                MySetting.SetIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetIni("Startup", "physics", "BulletSim")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "true")
            Case 4
                MySetting.SetIni("Startup", "meshing", "ubODEMeshmerizer")
                MySetting.SetIni("Startup", "physics", "ubODE")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "false")
            Case Else
                MySetting.SetIni("Startup", "meshing", "Meshmerizer")
                MySetting.SetIni("Startup", "physics", "BulletSim")
                MySetting.SetIni("Startup", "UseSeparatePhysicsThread", "true")
        End Select

        MySetting.SetIni("Const", "DiagnosticsPort", MySetting.DiagnosticPort)
        MySetting.SetIni("Const", "GridName", MySetting.SimName)

        If MySetting.MapType = "None" Then
            MySetting.SetIni("Map", "GenerateMaptiles", "false")
        ElseIf MySetting.MapType = "Simple" Then
            MySetting.SetIni("Map", "GenerateMaptiles", "true")
            MySetting.SetIni("Map", "MapImageModule", "MapImageModule")  ' versus Warp3DImageModule
            MySetting.SetIni("Map", "TextureOnMapTile", "false")         ' versus true
            MySetting.SetIni("Map", "DrawPrimOnMapTile", "false")
            MySetting.SetIni("Map", "TexturePrims", "false")
            MySetting.SetIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Good" Then
            MySetting.SetIni("Map", "GenerateMaptiles", "true")
            MySetting.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetIni("Map", "TextureOnMapTile", "false")         ' versus true
            MySetting.SetIni("Map", "DrawPrimOnMapTile", "false")
            MySetting.SetIni("Map", "TexturePrims", "false")
            MySetting.SetIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Better" Then
            MySetting.SetIni("Map", "GenerateMaptiles", "true")
            MySetting.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetIni("Map", "TextureOnMapTile", "true")         ' versus true
            MySetting.SetIni("Map", "DrawPrimOnMapTile", "true")
            MySetting.SetIni("Map", "TexturePrims", "false")
            MySetting.SetIni("Map", "RenderMeshes", "false")
        ElseIf MySetting.MapType = "Best" Then
            MySetting.SetIni("Map", "GenerateMaptiles", "true")
            MySetting.SetIni("Map", "MapImageModule", "Warp3DImageModule")  ' versus MapImageModule
            MySetting.SetIni("Map", "TextureOnMapTile", "true")      ' versus true
            MySetting.SetIni("Map", "DrawPrimOnMapTile", "true")
            MySetting.SetIni("Map", "TexturePrims", "true")
            MySetting.SetIni("Map", "RenderMeshes", "true")
        End If


        ' Autobackup
        If MySetting.AutoBackup Then
            Log("Info:Autobackup is On")
            MySetting.SetIni("AutoBackupModule", "AutoBackup", "true")
        Else
            Log("Info:Autobackup is Off")
            MySetting.SetIni("AutoBackupModule", "AutoBackup", "false")
        End If

        MySetting.SetIni("AutoBackupModule", "AutoBackupInterval", MySetting.AutobackupInterval)
        MySetting.SetIni("AutoBackupModule", "AutoBackupKeepFilesForDays", MySetting.KeepForDays)
        MySetting.SetIni("AutoBackupModule", "AutoBackupDir", BackupPath())


        ' Voice
        If MySetting.VivoxEnabled Then
            MySetting.SetIni("VivoxVoice", "enabled", "true")
        Else
            MySetting.SetIni("VivoxVoice", "enabled", "false")
        End If
        MySetting.SetIni("VivoxVoice", "vivox_admin_user", MySetting.Vivox_UserName)
        MySetting.SetIni("VivoxVoice", "vivox_admin_password", MySetting.Vivox_password)

        MySetting.SaveINI()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Wifi Settings

        DoWifi("bin\Wifi.ini")
        'DoWifi("addins-registry\addins\Diva.Wifi.0.9.0.0.13\Wifi.ini")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        DoGloebits(prefix + "bin\Gloebit.ini")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Regions - write all region.ini files with public IP and Public port

        For Each X In RegionClass.RegionNumbers
            If RegionClass.RegionName(X) <> "Robust" Then
                Dim simName = RegionClass.RegionName(X)

                MySetting.LoadIni(RegionClass.RegionPath(X), ";")
                MySetting.SetIni(simName, "InternalPort", Convert.ToString(RegionClass.RegionPort(X)))
                MySetting.SetIni(simName, "ExternalHostName", Convert.ToString(MySetting.PublicIP))

                ' not a standard INI, only use by the Dreamers
                If RegionClass.RegionEnabled(X) Then
                    MySetting.SetIni(simName, "Enabled", "true")
                Else
                    MySetting.SetIni(simName, "Enabled", "false")
                End If

                ' Extended in v 2.1
                MySetting.SetIni(simName, "NonPhysicalPrimMax", Convert.ToString(RegionClass.NonPhysicalPrimMax(X)))
                MySetting.SetIni(simName, "PhysicalPrimMax", Convert.ToString(RegionClass.PhysicalPrimMax(X)))
                MySetting.SetIni(simName, "MaxPrims", Convert.ToString(RegionClass.MaxPrims(X)))
                MySetting.SetIni(simName, "MaxAgents", Convert.ToString(RegionClass.MaxAgents(X)))
                MySetting.SetIni(simName, "ClampPrimSize", Convert.ToString(RegionClass.ClampPrimSize(X)))

                MySetting.SaveINI()
            End If

        Next

        Return CopyOpensimProto()

    End Function

    Function CopyOpensimProto() As Boolean

        ' COPY OPENSIM.INI prototype to all region folders and set the Sim Name

        For Each X As Integer In RegionClass.RegionNumbers
            Debug.Print("Count: " + X.ToString)
            Dim regionName = RegionClass.RegionName(X)
            Dim pathname = RegionClass.IniPath(X)
            Debug.Print(regionName)

            Try
                MySetting.LoadIni(prefix + "bin\Opensim.proto", ";")
                MySetting.SetIni("Const", "BaseURL", "http://" + MySetting.PublicIP)
                MySetting.SetIni("Const", "PrivURL", "http://" + MySetting.PrivateURL)
                MySetting.SetIni("Const", "PublicPort", MySetting.HttpPort) ' 8002
                MySetting.SetIni("Const", "http_listener_port", RegionClass.RegionPort(X)) ' varies with region
                MySetting.SetIni("Const", "PrivatePort", MySetting.PrivatePort) '8003
                MySetting.SetIni("Const", "RegionFolderName", RegionClass.GroupName(X))
                MySetting.SetIni("Const", "PrivatePort", MySetting.PrivatePort) '8003
                MySetting.SaveINI()
                My.Computer.FileSystem.CopyFile(prefix + "bin\Opensim.proto", pathname + "Opensim.ini", True)

            Catch ex As Exception
                Print("Error:Failed to set the Opensim.ini for sim " + regionName + ":" + ex.Message)
                Return False
            End Try

        Next

        Return True

    End Function

    Public Sub DoGloebits(path As String)

        'Gloebits.ini

        MySetting.LoadIni(path, ";")
        If MySetting.GloebitsEnable Then
            MySetting.SetIni("Gloebit", "Enabled", "true")
        Else
            MySetting.SetIni("Gloebit", "Enabled", "false")
        End If

        If MySetting.GloebitsMode Then
            MySetting.SetIni("Gloebit", "GLBEnvironment", "production")
            MySetting.SetIni("Gloebit", "GLBKey", MySetting.GLProdKey)
            MySetting.SetIni("Gloebit", "GLBSecret", MySetting.GLProdSecret)
        Else
            MySetting.SetIni("Gloebit", "GLBEnvironment", "sandbox")
            MySetting.SetIni("Gloebit", "GLBKey", MySetting.GLSandKey)
            MySetting.SetIni("Gloebit", "GLBSecret", MySetting.GLSandSecret)
        End If

        MySetting.SetIni("Gloebit", "GLBOwnerName", MySetting.GLBOwnerName)
        MySetting.SetIni("Gloebit", "GLBOwnerEmail", MySetting.GLBOwnerEmail)


        Dim ConnectionString = """" _
            + "Data Source=" + "127.0.0.1" _
            + ";Database=" + MySetting.RegionDBName _
            + ";Port=" + MySetting.MySqlPort _
            + ";User ID=" + MySetting.RegionDBUsername _
            + ";Password=" + MySetting.RegionDbPassword _
            + ";Old Guids=True;Allow Zero Datetime=True;" _
            + """"
        MySetting.SetIni("Gloebit", "GLBSpecificConnectionString", ConnectionString)

        MySetting.SaveINI()

    End Sub

    Private Sub DoWifi(param As String)


        MySetting.LoadIni(prefix + param, ";")

        Dim ConnectionString = """" _
                + "Data Source=" + "127.0.0.1" _
                + ";Database=" + MySetting.RobustMySqlName _
                + ";Port=" + MySetting.MySqlPort _
                + ";User ID=" + MySetting.RobustMySqlUsername _
                + ";Password=" + MySetting.RobustMySqlPassword _
                + """"

        MySetting.SetIni("DatabaseService", "ConnectionString", ConnectionString)

        ' Wifi Section

        If (MySetting.WifiEnabled) Then
            MySetting.SetIni("WifiService", "Enabled", "true")
        Else
            MySetting.SetIni("WifiService", "Enabled", "false")
        End If

        MySetting.SetIni("WifiService", "GridName", MySetting.SimName)
        MySetting.SetIni("WifiService", "LoginURL", "http: //" + MySetting.PublicIP + ":" + MySetting.HttpPort)
        MySetting.SetIni("WifiService", "WebAddress", "http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

        ' Wifi Admin'
        MySetting.SetIni("WifiService", "AdminFirst", MySetting.AdminFirst)
        MySetting.SetIni("WifiService", "AdminPassword", MySetting.Password)
        MySetting.SetIni("WifiService", "AdminLast", MySetting.AdminLast)

        'Gmail
        MySetting.SetIni("WifiService", "AdminPassword", MySetting.Password)
        MySetting.SetIni("WifiService", "SmtpUsername", MySetting.SmtpUsername)
        MySetting.SetIni("WifiService", "SmtpPassword", MySetting.SmtpPassword)


        If MySetting.AccountConfirmationRequired Then
            MySetting.SetIni("WifiService", "AccountConfirmationRequired", "true")
        Else
            MySetting.SetIni("WifiService", "AccountConfirmationRequired", "false")
        End If

        MySetting.SaveINI()
    End Sub

#End Region

#Region "Ports"

    Function PortTests() As Boolean

        Dim Passfail As Boolean = True
        ' Do some tests
        Dim ports(1) As Object
        ports(0) = Nothing

        Dim n = 0
        For Each X As Integer In RegionClass.RegionNumbers
            If ports.Length <= RegionClass.RegionPort(n) Then
                ReDim ports(RegionClass.RegionPort(n) + 1)
            End If
            Try
                If ports(RegionClass.RegionPort(n)) Is Nothing Then
                    ports(RegionClass.RegionPort(n)) = RegionClass.RegionName(n)
                Else
                    MsgBox(RegionClass.RegionName(n) + " has a duplicated port with " + ports(RegionClass.RegionPort(n)) + ". Skipping boot of " + RegionClass.RegionName(n), vbInformation, "Error")
                    RegionClass.RegionEnabled(n) = False
                    Passfail = False
                End If
            Catch ex As Exception
                Log("Error:" + ex.Message)
                ports(RegionClass.RegionPort(n)) = 0
            End Try
            n = n + 1
        Next

        Return Passfail

    End Function

    Public Sub CheckDefaultPorts()

        If MySetting.DiagnosticPort = MySetting.HttpPort _
            Or MySetting.DiagnosticPort = MySetting.PrivatePort _
            Or MySetting.HttpPort = MySetting.PrivatePort Then
            MySetting.DiagnosticPort = 8001
            MySetting.HttpPort = 8002
            MySetting.PrivatePort = 8003

            MsgBox("Port conflict detected. Sim Ports have been reset to the defaults", vbInformation, "Error")
        End If

    End Sub


#End Region

#Region "ToolBars"

    Private Sub ClearCachesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCachesToolStripMenuItem.Click

        If Not Running Then
            Try
                My.Computer.FileSystem.DeleteDirectory(prefix & "bin\bakes\", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.DeleteDirectory(prefix & "bin\\ScriptEngines\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch
            End Try
        End If

        Try
            My.Computer.FileSystem.DeleteDirectory(prefix & "bin\assetcache\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteDirectory(prefix & "bin\\j2kDecodeCache\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteDirectory(prefix & "bin\\MeshCache\", FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch
        End Try

        If Not Running Then
            Print("All Server Caches cleared")
        Else
            Print("All Server Caches except scripts and bakes were cleared. Opensim must be stopped to clear script and bake caches.")
        End If


    End Sub


    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Print("Starting UPnp Control Panel")
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = ""
        pi.FileName = MyFolder & "\UPnpPortForwardManager.exe"
        pi.WindowStyle = ProcessWindowStyle.Normal
        ProcessUpnp.StartInfo = pi
        Try
            ProcessUpnp.Start()
        Catch ex As Exception
            Log("Error:UPnP failed to launch:" + ex.Message)
        End Try
    End Sub

    Private Sub BusyButton_Click(sender As Object, e As EventArgs) Handles BusyButton.Click

        If gStopping = True Then
            gStopping = False
            Print("Stopped")
            Buttons(StartButton)
            Print("Opensim Is Stopped")
            ProgressBar1.Value = 0
            ProgressBar1.Visible = False
            Return
        End If

        Print("Stopping")
        Application.DoEvents()
        KillAll()
        Buttons(StartButton)
        Print("Opensim Is Stopped")
        ProgressBar1.Value = 0
        ProgressBar1.Visible = False

    End Sub

    Private Sub AdminUIToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewWebUI.Click
        If (Running) Then
            Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort
            Process.Start(webAddress)
            Print("Log in as '" + MySetting.AdminFirst + " " + MySetting.AdminLast + "' with a password of '" + MySetting.Password + "' to add user accounts.")
        Else
            Print("Opensim is not running. Cannot open the Web Interface.")
        End If
    End Sub

    Private Sub LoopBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoopBackToolStripMenuItem.Click
        Dim webAddress As String = Domain + "/Outworldz_Installer/Loopback.htm"
        Process.Start(webAddress)
    End Sub

    Private Sub MoreContentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MoreContentToolStripMenuItem.Click
        Dim webAddress As String = Domain + "/cgi/freesculpts.plx"
        Process.Start(webAddress)
        Print("Drag and drop Backup.Oar, or any OAR or IAR files to load into your Sim")
    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedSettingsToolStripMenuItem.Click

        ActualForm = New AdvancedForm
        ' Set the new form's desktop location so it appears below and
        ' to the right of the current form.
        ActualForm.SetDesktopLocation(300, 200)
        ActualForm.Activate()
        ActualForm.Visible = True

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim webAddress As String = Domain + "/Outworldz_Installer/PortForwarding.htm"
        Process.Start(webAddress)
    End Sub
#End Region

#Region "Robust"

    Public Sub RobustCommand(command As String)

        Try
            AppActivate(gRobustProcID)
            SendKeys.SendWait(command)
        Catch ex As Exception
            Log("Warn:" + ex.Message)
        End Try

    End Sub

    ' Handle Exited event and display process information.
    Private Sub RobustProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles RobustProcess.Exited

        gRobustProcID = Nothing

    End Sub

    Public Function Start_Robust() As Boolean

        If IsRobustRunning() Then
            'Print("Robust is already running")
            Return True
        End If

        gRobustProcID = Nothing
        Print("Starting Robust")

        Try
            RobustProcess.EnableRaisingEvents = True
            RobustProcess.StartInfo.UseShellExecute = True ' so we can redirect streams
            RobustProcess.StartInfo.FileName = prefix + "bin\robust.exe"

            RobustProcess.StartInfo.CreateNoWindow = False
            RobustProcess.StartInfo.WorkingDirectory = prefix + "bin"

            If mnuShow.Checked Then
                RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Else
                RobustProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End If

            RobustProcess.StartInfo.Arguments = "-inifile Robust.HG.ini"
            RobustProcess.Start()
            gRobustProcID = RobustProcess.Id

            Thread.Sleep(1000)
            SetWindowText(RobustProcess.MainWindowHandle, "Robust")

        Catch ex As Exception
            Print("Error: Robust did not start: " + ex.Message)
            KillAll()
            Buttons(StartButton)
            Return False
        End Try

        ' Wait for Opensim to start listening 

        Dim counter = 0
        While Not IsRobustRunning() And Running
            Application.DoEvents()
            BumpProgress(1)
            counter = counter + 1
            ' wait a minute for it to start
            If counter > 100 Then
                Print("Error:Robust failed to start")
                KillAll()
                Buttons(StartButton)
                Dim yesno = MsgBox("Robust did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim Log As String = """" + MyFolder + "\OutworldzFiles\Opensim\bin\Robust.log" + """"
                    System.Diagnostics.Process.Start("wordpad.exe", Log)
                End If
                Buttons(StartButton)
                Return False
            End If
            Application.DoEvents()
            Sleep(100)

        End While
        Log("Info:Robust is running")
        Return True

    End Function


#End Region

#Region "Opensimulator"

    Private Function Start_Opensimulator() As Boolean

        If Running = False Then Return True

        PortTests()

        Try
            ' Boot them up
            Dim n = 0
            For Each x In RegionClass.RegionNumbers
                If RegionClass.RegionEnabled(n) And Running Then '
                    If Not Boot(RegionClass.RegionName(n)) Then
                        Print("Boot skipped for " + RegionClass.RegionName(n))
                    End If
                End If
                n = n + 1
            Next
        Catch ex As Exception
            Debug.Print(ex.Message)
            Print("Unable to boot some regions")
        End Try


        Return True

    End Function

#End Region

#Region "Exited"
    Private Sub OpensimProcess01_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess1.Exited
        RegionHandles(1) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess02_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess2.Exited
        RegionHandles(2) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess03_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess3.Exited
        RegionHandles(3) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess04_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess4.Exited
        RegionHandles(4) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess05_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess5.Exited
        RegionHandles(5) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess06_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess6.Exited
        RegionHandles(6) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess07_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess7.Exited
        RegionHandles(7) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess08_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess8.Exited
        RegionHandles(8) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess09_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess9.Exited
        RegionHandles(9) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess10_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess10.Exited
        RegionHandles(10) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess11_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess11.Exited
        RegionHandles(11) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess12_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess12.Exited
        RegionHandles(12) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess13_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess13.Exited
        RegionHandles(13) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess14_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess14.Exited
        RegionHandles(14) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess15_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess15.Exited
        RegionHandles(15) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess16_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess16.Exited
        RegionHandles(16) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess17_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess17.Exited
        RegionHandles(17) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess18_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess18.Exited
        RegionHandles(18) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess19_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess19.Exited
        RegionHandles(19) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess20_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess20.Exited
        RegionHandles(20) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess21_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess21.Exited
        RegionHandles(21) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess22_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess22.Exited
        RegionHandles(22) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess23_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess23.Exited
        RegionHandles(23) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess24_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess24.Exited
        RegionHandles(24) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess25_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess25.Exited
        RegionHandles(25) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess26_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess26.Exited
        RegionHandles(26) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess27_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess27.Exited
        RegionHandles(27) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess28_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess28.Exited
        RegionHandles(28) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess29_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess29.Exited
        RegionHandles(29) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess30_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess30.Exited
        RegionHandles(30) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess31_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess31.Exited
        RegionHandles(31) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess32_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess32.Exited
        RegionHandles(32) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess33_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess33.Exited
        RegionHandles(33) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess34_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess34.Exited
        RegionHandles(34) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess35_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess35.Exited
        RegionHandles(35) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess36_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess36.Exited
        RegionHandles(36) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess37_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess37.Exited
        RegionHandles(37) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess38_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess38.Exited
        RegionHandles(38) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess39_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess39.Exited
        RegionHandles(39) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess40_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess40.Exited
        RegionHandles(40) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess41_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess41.Exited
        RegionHandles(41) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess42_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess42.Exited
        RegionHandles(42) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess43_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess43.Exited
        RegionHandles(43) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess44_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess44.Exited
        RegionHandles(44) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess45_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess45.Exited
        RegionHandles(45) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess46_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess46.Exited
        RegionHandles(46) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess47_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess47.Exited
        RegionHandles(47) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess48_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess48.Exited
        RegionHandles(48) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess49_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess49.Exited
        RegionHandles(49) = False
        DoExit(sender)
    End Sub
    Private Sub OpensimProcess50_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyProcess50.Exited
        RegionHandles(50) = False
        DoExit(sender)
    End Sub

#End Region

#Region "ExitHandlers"

    Private Sub DoExit(ByVal sender As Object)
        ' Handle Opensim Exited

        Dim n As Integer = RegionClass.FindRegionByProcessID(sender.Id)
        If n < 0 Then Return

        If RegionClass.WarmingUp(n) = True Then
            Dim yesno = MsgBox(RegionClass.RegionName(n) + " did not start. Do you want to see the log file?", vbYesNo, "Error")
            If (yesno = vbYes) Then
                System.Diagnostics.Process.Start("notepad.exe", RegionClass.IniPath(n) + "Opensim.log")
            End If
        End If

        For Each num In RegionClass.RegionListByGroupNum(RegionClass.GroupName(n))
            Log(RegionClass.RegionName(n) + " crashed")
            RegionClass.Booted(num) = False
            RegionClass.WarmingUp(num) = False
            RegionClass.ShuttingDown(num) = False
            RegionClass.ProcessID(num) = 0
        Next

    End Sub

    Private Function GetNewProcess() As Process

        ' find a empty regionhandle
        Dim ProcessCount As Integer = 0
        While ProcessCount < 50
            If Not RegionHandles(ProcessCount) Then
                RegionHandles(ProcessCount) = True
                Exit While
            End If
            ProcessCount = ProcessCount + 1
        End While
        If ProcessCount = 50 Then
            Return Nothing
        End If

        If ProcessCount = 0 Then Return MyProcess1
        If ProcessCount = 1 Then Return MyProcess2
        If ProcessCount = 2 Then Return MyProcess3
        If ProcessCount = 3 Then Return MyProcess4
        If ProcessCount = 4 Then Return MyProcess5
        If ProcessCount = 5 Then Return MyProcess6
        If ProcessCount = 6 Then Return MyProcess7
        If ProcessCount = 7 Then Return MyProcess8
        If ProcessCount = 8 Then Return MyProcess9
        If ProcessCount = 9 Then Return MyProcess10
        If ProcessCount = 10 Then Return MyProcess11
        If ProcessCount = 11 Then Return MyProcess12
        If ProcessCount = 12 Then Return MyProcess13
        If ProcessCount = 13 Then Return MyProcess14
        If ProcessCount = 14 Then Return MyProcess15
        If ProcessCount = 15 Then Return MyProcess16
        If ProcessCount = 16 Then Return MyProcess17
        If ProcessCount = 17 Then Return MyProcess18
        If ProcessCount = 18 Then Return MyProcess19
        If ProcessCount = 19 Then Return MyProcess20
        If ProcessCount = 20 Then Return MyProcess21
        If ProcessCount = 21 Then Return MyProcess22
        If ProcessCount = 22 Then Return MyProcess23
        If ProcessCount = 23 Then Return MyProcess24
        If ProcessCount = 24 Then Return MyProcess25
        If ProcessCount = 25 Then Return MyProcess26
        If ProcessCount = 26 Then Return MyProcess27
        If ProcessCount = 27 Then Return MyProcess28
        If ProcessCount = 28 Then Return MyProcess29
        If ProcessCount = 29 Then Return MyProcess30
        If ProcessCount = 30 Then Return MyProcess31
        If ProcessCount = 31 Then Return MyProcess32
        If ProcessCount = 32 Then Return MyProcess33
        If ProcessCount = 33 Then Return MyProcess34
        If ProcessCount = 34 Then Return MyProcess35
        If ProcessCount = 35 Then Return MyProcess36
        If ProcessCount = 36 Then Return MyProcess37
        If ProcessCount = 37 Then Return MyProcess38
        If ProcessCount = 38 Then Return MyProcess39
        If ProcessCount = 39 Then Return MyProcess40
        If ProcessCount = 40 Then Return MyProcess41
        If ProcessCount = 41 Then Return MyProcess42
        If ProcessCount = 42 Then Return MyProcess43
        If ProcessCount = 43 Then Return MyProcess44
        If ProcessCount = 44 Then Return MyProcess45
        If ProcessCount = 45 Then Return MyProcess46
        If ProcessCount = 46 Then Return MyProcess47
        If ProcessCount = 47 Then Return MyProcess48
        If ProcessCount = 48 Then Return MyProcess49
        If ProcessCount = 49 Then Return MyProcess50

        Return Nothing

    End Function

    Public Function Boot(BootName As String) As Boolean

        Running = True
        Buttons(StopButton)

        Dim n = RegionClass.FindRegionByName(BootName)
        If RegionClass.ProcessID(n) Or RegionClass.Booted(n) Or RegionClass.WarmingUp(n) Or RegionClass.ShuttingDown(n) Then
            Return True
        End If

        Environment.SetEnvironmentVariable("OSIM_LOGPATH", prefix + "bin\Regions\" + RegionClass.GroupName(n))

        Dim myProcess As Process = GetNewProcess()

        If myProcess Is Nothing Then
            Print("Exceeded max number of processes: could not start " + RegionClass.RegionName(n))
            Return False
        End If

        Print("Starting Region " + BootName)

        Try
            myProcess.EnableRaisingEvents = True
            myProcess.StartInfo.UseShellExecute = False ' so we can redirect streams
            myProcess.StartInfo.WorkingDirectory = prefix + "bin"

            Dim permanent = True
            myProcess.StartInfo.FileName = """" + prefix + "bin\OpenSim.exe" + """"
            myProcess.StartInfo.CreateNoWindow = False
            myProcess.StartInfo.Arguments = " -inidirectory=" & """" & "./Regions/" & RegionClass.GroupName(n) + """"

            If mnuShow.Checked Then
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Else
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            End If

            Try
                My.Computer.FileSystem.DeleteFile(prefix + "bin\Regions\" & RegionClass.GroupName(n) & "\Opensim.log")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(prefix + "bin\Regions\" & RegionClass.GroupName(n) & "\PID.pid")
            Catch
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(prefix + "bin\regions\" & RegionClass.GroupName(n) & "\OpensimConsole.log")
            Catch ex As Exception
            End Try

            Try
                My.Computer.FileSystem.DeleteFile(prefix + "bin\regions\" & RegionClass.GroupName(n) & "\OpenSimStats.log")
            Catch ex As Exception
            End Try

            RegionClass.ProcessID(n) = 0
            myProcess.Start()
            RegionClass.ProcessID(n) = myProcess.Id

            If myProcess.Id Then

                For Each num In RegionClass.RegionListByGroupNum(RegionClass.GroupName(n))
                    RegionClass.WarmingUp(num) = True
                    RegionClass.Booted(num) = False
                    RegionClass.ShuttingDown(num) = False
                Next

                Thread.Sleep(1500)

                SetWindowText(myProcess.MainWindowHandle, RegionClass.GroupName(n))
                Return True
            End If

        Catch ex As Exception
            If ex.Message.Contains("Process has exited") Then Return False
            Print("Oops! " + BootName + " did Not start")
            Log(ex.Message)
            Dim yesno = MsgBox("Oops! " + BootName + " did Not start. Do you want to see the log file?", vbYesNo, "Error")
            If (yesno = vbYes) Then
                System.Diagnostics.Process.Start("notepad.exe", RegionClass.IniPath(n) + "Opensim.log")
            End If

            Return False
        End Try

        Return False

    End Function

    Private Function IsRobustRunning() As Boolean

        Dim Up As String = String.Empty
        Try
            Up = client.DownloadString("http://127.0.0.1:" + MySetting.HttpPort + "/?_Opensim=" + Random())
        Catch ex As Exception
            If ex.Message.Contains("404") Then Return True
            Return False
        End Try
        If Up.Length = 0 And Running Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Logging"

    Private Sub ClearLogFiles()

        Dim Logfiles = New List(Of String) From {
            MyFolder + "\OutworldzFiles\Outworldz.log",
            MyFolder + "\OutworldzFiles\Opensim\bin\OpenSimConsoleHistory.txt",
            MyFolder + "\OutworldzFiles\Diagnostics.log",
            MyFolder + "\OutworldzFiles\UPnp.log",
            MyFolder + "\OutworldzFiles\Opensim\bin\Robust.log",
            MyFolder + "\OutworldzFiles\http.log",
            MyFolder + "\http.log"      ' an old mistake
        }

        For Each thing As String In Logfiles
            ' clear out the log files
            Try
                My.Computer.FileSystem.DeleteFile(thing)
            Catch ex As Exception
            End Try
        Next

    End Sub

    Public Sub Log(message As String)

        Try
            Using outputFile As New StreamWriter(MyFolder & "\OutworldzFiles\Outworldz.log", True)
                outputFile.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + message)
                Debug.Print(message)
            End Using
        Catch
        End Try

    End Sub

    Private Sub ShowLog()

        LogButton.Show()
        IgnoreButton.Show()

    End Sub

    Private Sub ShowLogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click

        System.Diagnostics.Process.Start("wordpad.exe", """" + MyFolder + "/OutworldzFiles/Outworldz.log" + """")

        LogButton.Hide()
        IgnoreButton.Hide()

    End Sub

    Private Sub IgnoreButton_Click(sender As Object, e As EventArgs) Handles IgnoreButton.Click

        LogButton.Hide()
        IgnoreButton.Hide()

    End Sub

#End Region

#Region "Subs"
    Public Sub ConsoleCommand(ProcessID As Integer, command As String)

        Try
            'plus sign(+), caret(^), percent sign (%), tilde (~), And parentheses ()
            command.Replace("+", "{+}")
            command.Replace("^", "{^}")
            command.Replace("%", "{%}")
            command.Replace("(", "{(}")
            command.Replace(")", "{)}")

            AppActivate(ProcessID)

            SendKeys.SendWait(command)
        Catch ex As Exception
            Log("Warn:" + ex.Message)
        End Try

    End Sub


    Private Sub SaySomething()
        Dim Prefix() As String = {
                                  "Mmmm?  Yawns ...",
                                  "Yawns, and stretches ...",
                                  "Wakes up and rolls over ...",
                                  "You look more beautiful every time I wake up.",
                                  "Zzzz...  Ooooh, I need coffee before I go to work.",
                                  "Nooo... is it already time to wake up?",
                                  "Mmmm, I was sleeping...",
                                  "What a dream that was!",
                                  "Do you ever dream of better worlds? I just did.",
                                  "Huh?",
                                  "Mumbles...",
                                  "Hello beautiful",
                                  "Act on dreams with an open mind.",
                                  "Believe in the beauty of your dreams.",
                                  "A dream doesn't become reality through magic"
                                }

        Dim Array() As String = {
                 "I dreamt we were both flying a dragon in the Outworldz. You flamed me. I tried to get even.  I lost LOL ",
                 "I dreamt we were chatting at OsGrid.org. It's the largest hypergrid-enabled virtual world.",
                 "I dreamt some friends and you were riding a rollercoaster in the Great Canadian Grid.",
                 "I dreamt I was watching a pretty particle exhibit with you on the Metropolis grid.",
                 "I dreamt we walked into a bar discussing politics in Hebrew and Arabic using a free translator.",
                 "I dreamt you took the hypergrid safari to visit the mountains of Africa in the Virunga sim.",
                 "I dreamt you won a race while riding a silly cow at the Outworldz 'Frankie' sim.",
                 "I dreamt you are a wonderful singer. I loved to hear your voice singing into the voice-chat system.",
                 "I remember in my dream that the spaceport at Gravity sim in OsGrid was really hopping. And floating. And then I fell. ",
                 "I was dreaming that you were a mermaid in the Lost Worlds.",
                 "I deamt that you made a pile of prims that you simply will not believe!",
                 "I dreamed that I asked when you were going to straighten out the castle. You said, 'Why? Is it tilted?'",
                 "I saw a dog in my dream. I dreamt you made a 'mesh' of it.",
                 "I dreamt I saw a man without any pants firmly attached to an eagle flying in the air. Always rez before you attach!",
                 "I forgot the dream already. I remember I woke up in it.",
                 "I was thinking I had no clothes on. No shirt, shoes, or hair. The worst part was there was no facelight! I looked hideous!",
                 "I dreamt that I was floating in a river and a scripted mesh crocodile chased me.",
                 "I dreamt I drove our car into the ocean. You found a pose ball, and we both grabbed onto it and were saved.",
                 "I dreamed that there was a animated mesh zebra in my bathtub.",
                 "I had dreamed a fairy was my best friend.",
                 "I dreamed that there were non-player characters living in my house, so I decided to fly away. ",
                 "I had a dream that there were pimples all over my face. So I switched skins and looked perfect!",
                 "I had a dream where I had lost my free mesh boots, so I was asking everybody where I got them on the hypergrid.",
                 "I had a dream that we were sitting on my roof and we stood up and both fell off. But I hit Page Up and flew away."
                  }

        Randomize()

        Dim value1 As Integer = CInt(Int((Prefix.Length - 1) * Rnd()))
        Dim value2 As Integer = CInt(Int((Array.Length - 1) * Rnd()))
        Dim whattosay = Prefix(value1) + vbCrLf + vbCrLf + Array(value2) + " ... and then I woke up."
        Print(whattosay)

    End Sub

    Sub Sleep(value As Integer)

        ' value is in milliseconds, but we do it in 10 passes so we can doevents() to free up console

        Dim sleeptime = value / 10  ' now in tenths
        Dim counter = 10
        While counter
            Application.DoEvents()
            Thread.Sleep(sleeptime)
            counter = counter - 1
        End While

    End Sub

    Public Sub PaintImage()

        Timertick = Timertick + 1
        If MySetting.TimerInterval > 0 Then  ' is it enabled?

            If Timertick >= MySetting.TimerInterval And Not Diagsrunning Then
                Dim randomFruit = images(Arnd.Next(0, images.Count))
                ProgressBar1.Visible = False
                TextBox1.Visible = False
                PictureBox1.Enabled = True
                PictureBox1.Image = randomFruit
                PictureBox1.Visible = True
                Timertick = 0   ' rest for next pass
            End If

        Else
            PictureBox1.Visible = False
        End If
        Application.DoEvents()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        PaintImage()

        ScanAgents()

        If gDNSSTimer Mod 3600 = 0 Then
            RegisterDNS()
        End If

        gDNSSTimer = gDNSSTimer + 1

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim randomFruit = images(Arnd.Next(0, images.Count))
        ProgressBar1.Visible = False
        TextBox1.Visible = False
        PictureBox1.Enabled = True
        PictureBox1.Image = randomFruit
        PictureBox1.Visible = True
        Timertick = 0   ' rest for next pass

    End Sub

    Private Sub ShowHyperGridAddressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHyperGridAddressToolStripMenuItem.Click

        Print("Hypergrid address is http://" + MySetting.PublicIP + ":" + MySetting.HttpPort)

    End Sub

    Private Sub WebStatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebStatsToolStripMenuItem.Click

        If (Running) Then
            Dim webAddress As String = "http://127.0.0.1:" + MySetting.HttpPort + "\bin\data\sim.html"
            Process.Start(webAddress)
        Else
            Print("Opensim is not running. Cannot open the Statistics web page.")
        End If

    End Sub

    Private Sub BumpProgress(bump As Integer)

        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value = ProgressBar1.Value + bump
        End If

    End Sub

    Private Sub BumpProgress10()

        If ProgressBar1.Value < 90 Then
            ProgressBar1.Value = ProgressBar1.Value + 10
        Else
            ProgressBar1.Value = 100
        End If
        Application.DoEvents()
    End Sub

    Private Function Stripqq(input As String) As String

        Return Replace(input, """", "")

    End Function

#End Region

#Region "IAROAR"

    Private Sub SaveRegionOARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveRegionOARToolStripMenuItem.Click

        If (Running) Then

            Dim chosen = ChooseRegion(True)
            Dim n As Integer = RegionClass.FindRegionByName(chosen)


            Dim Message, title, defaultValue As String
            Dim myValue As Object
            ' Set prompt.
            Message = "Enter a name for your backup:"
            title = "Backup to OAR"
            defaultValue = chosen + ".oar"   ' Set default value.

            ' Display message, title, and default value.
            myValue = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If myValue.length = 0 Then Return

            If RegionClass.Booted(n) Then
                Dim Group = RegionClass.GroupName(n)
                For Each Y In RegionClass.RegionListByGroupNum(Group)
                    ConsoleCommand(RegionClass.ProcessID(Y), "alert CPU Intensive Backup Started{ENTER}")
                    ConsoleCommand(RegionClass.ProcessID(Y), "change region " + """" + chosen + """" + "{ENTER}")
                    ConsoleCommand(RegionClass.ProcessID(Y), "save oar " + """" + BackupPath() + RegionClass.RegionName(Y) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{Enter}")
                    Application.DoEvents()
                Next
            End If
            Me.Focus()
            Print("Saving " + myValue + " to " + BackupPath())
        Else
            Print("Opensim is not running. Cannot make a backup now.")
        End If

    End Sub

    Private Sub LoadRegionOarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadRegionOarToolStripMenuItem.Click

        If (Running) Then
            Dim chosen = ChooseRegion(True)
            Dim n As Integer = RegionClass.FindRegionByName(chosen)

            ' Create an instance of the open file dialog box.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

            ' Set filter options and filter index.
            openFileDialog1.InitialDirectory = BackupPath()
            openFileDialog1.Filter = "Opensim OAR(*.OAR,*.GZ,*.TGZ)|*.oar;*.gz;*.tgz;*.OAR;*.GZ;*.TGZ|All Files (*.*)|*.*"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.Multiselect = False

            ' Call the ShowDialog method to show the dialogbox.
            Dim UserClickedOK As Boolean = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = True Then
                Dim backMeUp = MsgBox("Make a backup first and then load the new content?", vbYesNo, "Backup?")
                Dim thing = openFileDialog1.FileName
                If thing.Length Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why


                    Dim Group = RegionClass.GroupName(n)
                    For Each Y In RegionClass.RegionListByGroupNum(Group)

                        ConsoleCommand(RegionClass.ProcessID(Y), "change region " + chosen + "{Enter}")
                        If backMeUp = vbYes Then
                            ConsoleCommand(RegionClass.ProcessID(Y), "alert CPU Intensive Backup Started{ENTER}")
                            ConsoleCommand(RegionClass.ProcessID(Y), "save oar  " + """" + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{Enter}")
                        End If
                        ConsoleCommand(RegionClass.ProcessID(Y), "alert New content Is loading..{ENTER}")
                        ConsoleCommand(RegionClass.ProcessID(Y), "load oar --force-terrain --force-parcels " + """" + thing + """" + "{ENTER}")
                        ConsoleCommand(RegionClass.ProcessID(Y), "alert New content just loaded." + "{ENTER}")
                        Me.Focus()
                    Next
                End If
            End If
        Else
            Print("Opensim Is Not running. Cannot load the OAR file.")
        End If

    End Sub
    Private Function BackupPath() As String

        If MySetting.BackupFolder = "AutoBackup" Then
            BackupPath = gCurSlashDir + "/OutworldzFiles/Autobackup/"
        Else
            BackupPath = MySetting.BackupFolder + "/"
            BackupPath = BackupPath.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why
        End If

    End Function

    Private Sub AllRegionsOARsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllTheRegionsOarsToolStripMenuItem.Click

        If Not Running Then
            Print("Opensim Is Not running. Cannot save an OAR at this time.")
            Return
        End If

        Dim n As Integer = 0
        Dim L As New List(Of String)
        ' L.Add("Xyzzy")

        For Each X In RegionClass.RegionNumbers
            If RegionClass.Booted(n) Then

                Dim Group = RegionClass.GroupName(n)

                For Each Y In RegionClass.RegionListByGroupNum(Group)
                    If Not L.Contains(RegionClass.RegionName(Y)) Then
                        ConsoleCommand(RegionClass.ProcessID(n), "change region " + """" + RegionClass.RegionName(Y) + """" + "{Enter}")
                        ConsoleCommand(RegionClass.ProcessID(n), "save oar  " + """" + BackupPath() + RegionClass.RegionName(Y) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{Enter}")
                        Application.DoEvents()
                        L.Add(RegionClass.RegionName(Y))
                    End If
                Next

            End If

            n = n + 1

        Next

    End Sub

    Private Sub LoadInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadInventoryIARToolStripMenuItem.Click

        If Running Then
            ' Create an instance of the open file dialog box.
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

            ' Set filter options and filter index.
            openFileDialog1.InitialDirectory = """" + MyFolder + "/" + """"
            openFileDialog1.Filter = "Inventory IAR (*.iar)|*.iar|All Files (*.*)|*.*"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.Multiselect = False

            ' Call the ShowDialog method to show the dialogbox.
            Dim UserClickedOK As Boolean = openFileDialog1.ShowDialog

            ' Process input if the user clicked OK.
            If UserClickedOK = True Then
                Dim thing = openFileDialog1.FileName
                If thing.Length Then
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why
                    LoadIARContent(thing)
                End If
            End If
        Else
            Print("Opensim Is Not running. Cannot load an IAR at this time.")
        End If

    End Sub

    Private Sub SaveInventoryIARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveInventoryIARToolStripMenuItem.Click

        If (Running) Then
            Dim Message, title, defaultValue As String

            '''''''''''''''''''''''
            ' Object Name to back up
            Dim itemName As String
            ' Set prompt.
            Message = "Enter the Object name ('/' will  backup everything, and '/Objects/box' will back up box in folder Objects) :"
            title = "Backup Name?"
            defaultValue = "/"   ' Set default value.

            ' Display message, title, and default value.
            itemName = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If itemName.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Name of the IAR
            Dim backupName As String
            Message = "Backup name? :"
            title = "Backup Name?"
            defaultValue = "backup.iar"   ' Set default value.

            ' Display message, title, and default value.
            backupName = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If itemName.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Avatar
            Dim Name As String
            Message = "Avatar FirstName and Lastname:"
            title = "FirstName LastName"
            defaultValue = ""   ' Set default value.

            ' Display message, title, and default value.
            Name = InputBox(Message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue 
            If Name.Length = 0 Then Return

            '''''''''''''''''''''''
            ' Password
            Dim Password As String
            Message = "Avatar's Password?:"
            title = "Password needed"
            defaultValue = ""   ' Set default value.

            ' Display message, title, and default value.
            Password = InputBox(Message, title, defaultValue)

            Dim r As Integer = -1
            For Each d As Integer In RegionClass.RegionNumbers
                If RegionClass.Booted(d) Then
                    r = d
                End If
            Next

            If r < 0 Then
                Print("No regions are running")
                Return
            End If

            ConsoleCommand(RegionClass.ProcessID(r), "save iar " + Name + " " + """" + itemName + """" + " " + Password + " " + """" + BackupPath() + backupName + """" + "{ENTER}")
            Me.Focus()
            Print("Saving " + backupName + " to " + BackupPath())

        Else
            Print("Opensim is not running. Cannot make a backup now.")
        End If

    End Sub

    Public Function ChooseRegion(Optional JustRunning As Boolean = False) As String

        Dim Chooseform As New Choice ' form for choosing a set of regions
        ' Show testDialog as a modal dialog and determine if DialogResult = OK.

        Chooseform.FillGrid("Region", JustRunning)  ' populate the grid with either Group or RegionName

        Dim chosen As String
        Chooseform.ShowDialog()
        Try
            ' Read the chosen sim name
            chosen = Chooseform.DataGridView.CurrentCell.Value.ToString()
        Catch ex As Exception
            Log("Warn:Could not chose a displayed region. " + ex.Message)
            chosen = ""
        End Try
        Return chosen

    End Function

    Private Sub LoadOARContent(thing As String)

        If Not isRunning Then
            Print("Opensim has to be started to load an OAR file.")
            Return
        End If

        Dim region = ChooseRegion(True)

        If region.Length Then
            Dim backMeUp = MsgBox("Make a backup first?", vbYesNo, "Backup?")

            For Each Y In RegionClass.RegionListByGroupNum(region)
                Try
                    Print("Opensimulator will load  " + thing + ".  This may take some time.")
                    thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    ConsoleCommand(RegionClass.ProcessID(Y), "change region " + region + "{ENTER}")
                    If backMeUp = vbYes Then
                        ConsoleCommand(RegionClass.ProcessID(Y), "alert CPU Intensive Backup Started {ENTER}")
                        ConsoleCommand(RegionClass.ProcessID(Y), "save oar " + """" + BackupPath() + "Backup_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".oar" + """" + "{Enter}")
                    End If
                    ConsoleCommand(RegionClass.ProcessID(Y), "alert New content Is loading ...{ENTER}")
                    ConsoleCommand(RegionClass.ProcessID(Y), "load oar --force-terrain --force-parcels " + """" + thing + """" + "{ENTER}")
                    ConsoleCommand(RegionClass.ProcessID(Y), "alert New content just loaded. {ENTER}")

                Catch ex As Exception
                    Log("Error: " + ex.Message)
                End Try
            Next

            Me.Focus()

        End If

    End Sub

    Private Sub LoadIARContent(thing As String)

        If Not Running Then
            Print("Opensim is not running. Cannot save an OAR at this time.")
            Return
        End If

        Dim num As Integer = -1

        ' find one that is running
        For Each RegionNum In RegionClass.RegionNumbers
            If RegionClass.Booted(RegionNum) Then
                num = RegionNum
            End If
        Next
        If num = -1 Then
            MsgBox("No regions are ready, so cannot load the IAR", vbInformation, "Info")
            Return
        End If

        Dim user = InputBox("User name that will get this IAR?")
        Dim password = InputBox("Password for user " + user + "?")
        If user.Length And password.Length Then
            For Each Y In RegionClass.RegionListByGroupNum(RegionClass.RegionName(num))   ' a booted region,'s grroup            
                ConsoleCommand(RegionClass.ProcessID(Y), "load iar --merge " + user + " /Objects " + password + " " + """" + thing + """" + "{ENTER}")
                ConsoleCommand(RegionClass.ProcessID(Y), "alert IAR content Is loaded{ENTER}")
            Next
            Print("Opensim is loading your item. You will find it in in Inventory in /Objects soon.")
        Else
            Print("Load IAR cancelled - must use the full user name and password.")
        End If
        Me.Focus()
    End Sub

    Private Sub TextBox1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragDrop

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each pathname As String In files
            pathname.Replace("\", "/")
            Dim extension = Path.GetExtension(pathname)
            extension = Mid(extension, 2, 5)
            If extension.ToLower = "iar" Then
                LoadIARContent(pathname)
            ElseIf extension.ToLower = "oar" Or extension.ToLower = "gz" Or extension.ToLower = "tgz" Then
                LoadOARContent(pathname)
            Else
                Print("Unrecognized file type:" + extension + ".  Drag and drop any OAR, GZ, TGZ, or IAR files to load them when the sim starts")
            End If
        Next

    End Sub

    Private Sub TextBox1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub PictureBox1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop

        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each pathname As String In files
            pathname.Replace("\", "/")
            Dim extension = Path.GetExtension(pathname)
            extension = Mid(extension, 2, 5)
            If extension.ToLower = "iar" Then
                LoadIARContent(pathname)
            ElseIf extension.ToLower = "oar" Or extension.ToLower = "gz" Or extension.ToLower = "tgz" Then
                LoadOARContent(pathname)
            Else
                Print("Unrecognized file type:" + extension + ".  Drag and drop any OAR, GZ, TGZ, or IAR files to load them when the sim starts")
            End If
        Next

    End Sub

    Private Sub PictureBox1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub SetIAROARContent()

        IslandToolStripMenuItem.Visible = False
        ClothingInventoryToolStripMenuItem.Visible = False

        Print("Dreaming up new content for your sim")
        Dim oars As String = ""
        Try
            oars = client.DownloadString(Domain + "/Outworldz_Installer/Content.plx?type=OAR&r=" + Random())
        Catch ex As Exception
            Log("No Oars, dang, something is wrong with the Internet :-(")
            Return
        End Try
        Application.DoEvents()
        Dim oarreader = New System.IO.StringReader(oars)
        Dim line As String = ""
        Dim ContentSeen As Boolean = False
        While Not ContentSeen
            line = oarreader.ReadLine()
            If line <> Nothing Then
                Log("Info:" + line)
                Dim OarMenu As New ToolStripMenuItem
                OarMenu.Text = line
                OarMenu.ToolTipText = "Cick to load this content"
                OarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler OarMenu.Click, New EventHandler(AddressOf OarClick)
                IslandToolStripMenuItem.Visible = True
                IslandToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OarMenu})
                gContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While
        BumpProgress10()

        Print("Dreaming up some clothes and items for your avatar")
        Dim iars As String = ""
        Try
            iars = client.DownloadString(Domain + "/Outworldz_Installer/Content.plx?type=IAR&r=" + Random())
        Catch ex As Exception
            Log("Info:No IARS, dang, something is wrong with the Internet :-(")
            Return
        End Try

        Dim iarreader = New System.IO.StringReader(iars)
        ContentSeen = False
        While Not ContentSeen
            line = iarreader.ReadLine()
            If line <> Nothing Then
                Log("Info:" + line)
                Dim IarMenu As New ToolStripMenuItem
                IarMenu.Text = line
                IarMenu.ToolTipText = "Click to load this content the next time the simulator is started"
                IarMenu.DisplayStyle = ToolStripItemDisplayStyle.Text
                AddHandler IarMenu.Click, New EventHandler(AddressOf IarClick)
                ClothingInventoryToolStripMenuItem.Visible = True
                ClothingInventoryToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {IarMenu})
                gContentAvailable = True
            Else
                ContentSeen = True
            End If
        End While
        BumpProgress10()

    End Sub

    Private Sub OarClick(sender As Object, e As EventArgs)

        Dim File = Mid(sender.text, 1, InStr(sender.text, "|") - 2)
        File = Domain + "/Outworldz_Installer/OAR/" + File 'make a real URL
        LoadOARContent(File)
        sender.checked = True

    End Sub

    Private Sub IarClick(sender As Object, e As EventArgs)

        Dim file = Mid(sender.text, 1, InStr(sender.text, "|") - 2)
        file = Domain + "/Outworldz_Installer/IAR/" + file 'make a real URL
        LoadIARContent(file)
        sender.checked = True
        Print("Opensimulator will load " + file + ".  This may take time to load.")

    End Sub

#End Region

#Region "Updates"

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CHeckForUpdatesToolStripMenuItem.Click
        CheckForUpdates()
    End Sub

    Private Sub UpdaterCancel_Click(sender As Object, e As EventArgs) Handles UpdaterCancel.Click

        UpdaterGo.Visible = False
        UpdaterCancel.Visible = False
        MySetting.SkipUpdateCheck = True
        Print("Update scan is off. You can still check for updates in the Help Menu.")

    End Sub

    Private Sub UpdaterGo_Click(sender As Object, e As EventArgs) Handles UpdaterGo.Click

        UpdaterGo.Enabled = False
        UpdaterCancel.Visible = False
        Dim fileloaded As String = Download()
        If (fileloaded.Length) Then
            Dim pUpdate As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo()
            pi.Arguments = ""
            pi.FileName = """" + MyFolder + "\" + fileloaded + """"
            pUpdate.StartInfo = pi
            Try
                Print("I'll see you again when I wake up all fresh and new!")
                Log("Info:Launch Updater and exiting")
                pUpdate.Start()
            Catch ex As Exception
                Print("Error: Could not launch " + fileloaded + ". Perhaps you can can exit this program and launch it manually.")
                Log("Error: installer failed to launch:" + ex.Message)
            End Try
            End ' program
        Else
            Print("Uh Oh!  The files I need could not be found online. The gnomes have absconded with them!   Please check later.")
            UpdaterGo.Visible = False
            UpdaterGo.Enabled = True
        End If

    End Sub

    Private Function Download() As String

        Dim fileName As String = "UpdateGrid.exe"

        Try
            My.Computer.FileSystem.DeleteFile(MyFolder + "\" + fileName)
        Catch
            Log("Warn:Could not delete " + MyFolder + "\" + fileName)
        End Try

        Try
            fileName = client.DownloadString(Domain + "/Outworldz_Installer/GetUpdaterGrid.plx?r" + Random())
        Catch
            MsgBox("Could not fetch an update. Please try again, later", vbInformation, "Info")
            Return ""
        End Try

        Try
            Dim myWebClient As New WebClient()
            Print("Downloading new updater, this will take a moment")
            ' The DownloadFile() method downloads the Web resource and saves it into the current file-system folder.
            myWebClient.DownloadFile(Domain + "/Outworldz_Installer/" + fileName, fileName)
        Catch e As Exception
            MsgBox("Could not fetch an update. Please try again, later", vbInformation, "Info")
            Log("Warn:" + e.Message)
            Return ""
        End Try
        Return fileName

    End Function

    Sub CheckForUpdates()

        Dim Update As String = ""
        Dim isPortOpen As String = ""
        Dim Data As String = GetPostData()

        MySetting.SkipUpdateCheck = False
        MySetting.SaveMyINI()

        Try
            Update = client.DownloadString(Domain + "/Outworldz_Installer/UpdateGrid.plx?Ver=" + Str(MyVersion) + Data)
        Catch ex As Exception
            Log("Dang:The Outworld web site is down")
        End Try
        If (Update = "") Then Update = "0"

        Try
            If Convert.ToSingle(Update) > Convert.ToSingle(MyVersion) Then
                Print("A dreamier version " + Update + " is available.")
                UpdaterGo.Visible = True
                UpdaterGo.Enabled = True
                UpdaterCancel.Visible = True
            Else
                Print("I am the dreamiest version available, at V " + MyVersion)
            End If
        Catch
        End Try
        BumpProgress10()

    End Sub

#End Region

#Region "Diagnostics"

    Private Function CheckPort(ServerAddress As String, Port As String) As Boolean

        Dim iPort As Integer = Convert.ToInt16(Port)
        Dim ClientSocket As New TcpClient

        Try
            ClientSocket.Connect(ServerAddress, iPort)
        Catch ex As Exception
            Log("Info:Unable to connect to server:" + ex.Message)
            Return False
        End Try

        If ClientSocket.Connected Then
            Log("Info: port probe success on port " + Convert.ToString(iPort))
            ClientSocket.Close()
            Return True
        End If
        CheckPort = False

    End Function

    Public Function SetPublicIP() As Boolean

        If Not MySetting.EnableHypergrid Then
            BumpProgress10()
            If MySetting.DNSName.Length Then
                BumpProgress10()
                MySetting.PublicIP = MySetting.DNSName
                MySetting.SaveMyINI()
            End If

#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

            MySetting.SaveMyINI()
            Return False
        End If

        If MySetting.DNSName.Length Then
            BumpProgress10()
            MySetting.PublicIP = MySetting.DNSName
            MySetting.SaveMyINI()

            If MySetting.DNSName.ToLower.Contains("outworldz.net") Then
                Print("Registering Dyn DNS")
            End If

            If RegisterDNS() Then
                ProbePublicPort()
            End If
            Return True
        End If


        ' Set Public IP
        Try
            Dim ip As String = client.DownloadString("http://api.ipify.org/?r=" + Random())
            BumpProgress10()
            Log("Info:Public IP=" + MySetting.PublicIP)
            MySetting.PublicIP = ip
            MySetting.SaveMyINI()
            Return True

        Catch ex As Exception
            Print("Hmm, I cannot reach the Internet? Uh. Okay, continuing." + ex.Message)
            MySetting.DiagFailed = True
            Log("Info:Public IP=" + "127.0.0.1")
        End Try

        MySetting.PublicIP = "127.0.0.1"
        MySetting.SaveMyINI()

        BumpProgress10()
        Return False

    End Function

    Private Sub TestLoopback()

        If MySetting.DNSName = "localhost" Or MySetting.DNSName = "127.0.0.1" Then
            Return
        End If

        Print("Running Loopback Test")
        Dim result As String = ""
        Dim loopbacktest As String = "http://" + MySetting.PublicIP + ":" + MySetting.DiagnosticPort + "/?_TestLoopback=" + Random()
        Try
            Log(loopbacktest)
            result = client.DownloadString(loopbacktest)
        Catch ex As Exception
            Log("Err:Loopback fail:" + result + ":" + ex.Message)
        End Try

        BumpProgress10()

        If result = "Test Completed" And Not gDebug Then
            Log("Passed:" + result)
            MySetting.LoopBackDiag = True
            MySetting.SaveMyINI()
        Else
            Log("Failed:" + result)
            Print("Router Loopback failed. See the Help section for 'Loopback' and how to enable it in Windows. Continuing...")
            MySetting.LoopBackDiag = False
            MySetting.DiagFailed = True
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP()
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

            MySetting.SaveMyINI()
        End If

    End Sub

    Private Sub DiagnosticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiagnosticsToolStripMenuItem.Click

        If Running Then
            Print("Cannot run dignostics while Opensimulator is running. Click 'Stop' and try again.")
            Return
        End If
        ProgressBar1.Value = 0
        DoDiag()
        If MySetting.DiagFailed = True Then
            Print("Hypergrid Diagnostics failed. These can be re-run at any time. See Help->Network Diagnostics', 'Loopback', and 'Port Forwards'")
        Else
            Print("Tests passed, Hypergrid should be working.")
        End If
        ProgressBar1.Value = 100

    End Sub

    Private Function ProbePublicPort() As Boolean

        If MySetting.DNSName = "localhost" Or MySetting.DNSName = "127.0.0.1" Then
            Return True
        End If
        Print("Checking Router")

        Dim isPortOpen As String = ""
        Try
            ' collect some stats and test loopback with a HTTP_ GET to the webserver.
            ' Send unique, anonymous random ID, both of the versions of Opensim and this program, and the diagnostics test results 
            ' See my privacy policy at https://www.outworldz.com/privacy.htm

            Dim Data As String = GetPostData()
            Dim Url = Domain + "/cgi/probetest.plx?IP=" + MySetting.PublicIP + "&Port=" + MySetting.DiagnosticPort + Data
            Log(Url)
            isPortOpen = client.DownloadString(Url)
        Catch ex As Exception
            Log("Dang:The Outworldz web site cannot find a path back")
            MySetting.DiagFailed = True
        End Try

        BumpProgress10()

        If isPortOpen = "yes" And Not gDebug Then
            MySetting.PublicIP = MySetting.PublicIP
            Log("Public IP set to " + MySetting.PublicIP)
            MySetting.SaveMyINI()
            Return True
        Else
            Log("Failed:" + isPortOpen)
            MySetting.DiagFailed = True
            Print("Internet address " + MySetting.PublicIP + ":" + MySetting.DiagnosticPort + " appears to not be forwarded to this machine in your router, so Hypergrid is not available. This can possibly be fixed by 'Port Forwards' in your router.  See Help->Port Forwards.")
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.PublicIP = MyUPnpMap.LocalIP() ' failed, so try the machine address
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
            MySetting.SaveMyINI()
            Log("IP set to " + MySetting.PublicIP)
            Return False
        End If

    End Function

    Private Sub DoDiag()

        If MySetting.DNSName = "127.0.0.1" Or MySetting.DNSName = "localhost" Then Return

        Print("Running Network Diagnostics, please wait")
        Diagsrunning = True
        MySetting.DiagFailed = False

        OpenPorts() ' Open router ports with UPnp
        CheckDiagPort()
        ProbePublicPort()
        TestLoopback()
        If MySetting.DiagFailed Then
            ShowLog()
        Else
            NewDNSName()
        End If
        Log("Diagnostics set the Hypergrid address to " + MySetting.PublicIP)

        Diagsrunning = False

    End Sub


    Private Sub CheckDiagPort()
        gUseIcons = True

        Dim wsstarted = CheckPort(MyUPnpMap.LocalIP, MySetting.DiagnosticPort)
        If wsstarted = False Then
            MsgBox("Diagnostics port " + MySetting.DiagnosticPort + " is not working or blocked by firewall or anti virus, icons disabled.", vbInformation, "Cannot HG")
            gUseIcons = False
            MySetting.DiagFailed = True

            MySetting.SaveMyINI()
        End If

    End Sub

    Private Shared Function IsPrivateIP(CheckIP As String) As Boolean

        ''' <summary>
        ''' Checks to see if an IP address is a local IP address.
        ''' </summary>
        ''' <param name="CheckIP">The IP address to check.</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        ''' 

        Dim Quad1, Quad2 As Integer

        Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
        Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Select Case Quad1
            Case 10
                Return True
            Case 172
                If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function

#End Region

#Region "UPnP"

    Function OpenRouterPorts() As Boolean


        If Not MyUPnpMap.UPnpEnabled Then
            Log("UPnP is not enabled in the router")
            Return False
        End If

        If Not MySetting.UPnPEnabled Then
            Log("UPnP is not enabled in the menu")
            Return True
        End If

        Log("Local ip seems to be " + MyUPnpMap.LocalIP)
        Print("Puny human is instructed to wait while I check out the router ...")
        Try
            'diagnostics 8001
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.DiagnosticPort), UPnp.Protocol.TCP, "Opensim TCP Public " + MySetting.DiagnosticPort)
            PrintFast("Diagnostic Port is set to " + MySetting.DiagnosticPort)
            BumpProgress10()

            ' 8002 for TCP and UDP
            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.TCP, "Opensim TCP Grid " + MySetting.HttpPort)
            PrintFast("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            If MyUPnpMap.Exists(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP) Then
                MyUPnpMap.Remove(Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP)
            End If
            MyUPnpMap.Add(MyUPnpMap.LocalIP, Convert.ToInt16(MySetting.HttpPort), UPnp.Protocol.UDP, "Opensim UDP Grid " + MySetting.HttpPort)
            PrintFast("Grid Port is set to " + MySetting.HttpPort)
            BumpProgress10()

            '8004-whatever

            ' !!!regions = regions.OrderBy(Function(x) x.RegionPort).ToList()

            For Each X In RegionClass.RegionNumbers
                Dim R As Int16 = RegionClass.RegionPort(X)

                If MyUPnpMap.Exists(R, UPnp.Protocol.UDP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.UDP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.UDP, "Opensim UDP Region " & RegionClass.RegionName(X) & " ")
                PrintFast("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)

                If MyUPnpMap.Exists(R, UPnp.Protocol.TCP) Then
                    MyUPnpMap.Remove(R, UPnp.Protocol.TCP)
                End If
                MyUPnpMap.Add(MyUPnpMap.LocalIP, R, UPnp.Protocol.TCP, "Opensim TCP Region " & RegionClass.RegionName(X) & " ")
                PrintFast("Region " + RegionClass.RegionName(X) + " is set to " + Convert.ToString(R))
                BumpProgress(1)
            Next

        Catch e As Exception
            Log("UPnp: UPnp Exception caught:  " + e.Message)
            Return False
        End Try
        Return True 'successfully added

    End Function


    Private Function GetPostData() As String

        Dim SimVersion = "0.9.0"

        Dim UPnp As String = "Fail"
        If MySetting.UPnpDiag Then
            UPnp = "Pass"
        End If
        Dim Loopb As String = "Fail"
        If MySetting.LoopBackDiag Then
            Loopb = "Pass"
        End If


        Dim data As String = "&Machine=" + Machine _
            + "&V=" + MyVersion _
            + "&OV=" + SimVersion _
            + "&uPnp=" + UPnp _
            + "&Loop=" + Loopb _
            + "&Type=Grid" _
            + "&r=" + Random()
        Return data

    End Function

    Private Function OpenPorts() As Boolean

        'If Running = False Then Return True

        Try
            If OpenRouterPorts() Then ' open UPnp port
                Log("UPnpOk")
                MySetting.UPnpDiag = True
                MySetting.SaveMyINI()
                BumpProgress10()
                Return True
            Else
                Log("UPnp: fail")
                MySetting.UPnpDiag = False
                MySetting.SaveMyINI()

                BumpProgress10()
                Return False
            End If
        Catch e As Exception
            Log("Error: UPnp Exception: " + e.Message)
            MySetting.UPnpDiag = False
            MySetting.SaveMyINI()
            BumpProgress10()
            Return False
        End Try

    End Function

#End Region

#Region "MySQl"

    Private Sub RestoreDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RestoreDatabaseToolStripMenuItem1.Click

        If Running Then
            Print("Cannot restore when Opensim is running. Click [Stop] and try again.")
            Return
        End If

        If Not StartMySQL() Then Return

        ' Create an instance of the open file dialog box.
        Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog

        ' Set filter options and filter index.
        openFileDialog1.InitialDirectory = BackupPath()
        openFileDialog1.Filter = "MySqlDump (*.sql)|*.sql|All Files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.Multiselect = False

        ' Call the ShowDialog method to show the dialogbox.
        Dim UserClickedOK As Boolean = openFileDialog1.ShowDialog

        ' Process input if the user clicked OK.
        If UserClickedOK = True Then
            Dim thing = openFileDialog1.FileName
            If thing.Length Then

                Dim yesno = MsgBox("Are you sure? Your database will re-loaded from the backup and all existing content replaced. Avatars, sims, inventory, all of it.", vbYesNo, "Restore?")
                If yesno = vbYes Then
                    ' thing = thing.Replace("\", "/")    ' because Opensim uses unix-like slashes, that's why

                    Try
                        My.Computer.FileSystem.DeleteFile(MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat")
                    Catch
                    End Try
                    Try
                        Dim filename As String = MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                        Using outputFile As New StreamWriter(filename, True)
                            outputFile.WriteLine("@REM A program to restore Mysql from a backup" + vbCrLf _
                                    + "mysql -u root opensim <  " + """" + thing + """" _
                                    + vbCrLf + "@pause" + vbCrLf)
                        End Using
                    Catch ex As Exception
                        Print("Failed to create restore file:" + ex.Message)
                        Return
                    End Try

                    Print("Starting restore - do not interrupt!")
                    Dim pMySqlRestore As Process = New Process()
                    Dim pi As ProcessStartInfo = New ProcessStartInfo()

                    ' pi.Arguments = thing
                    pi.WindowStyle = ProcessWindowStyle.Normal
                    pi.WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\"

                    pi.FileName = MyFolder & "\OutworldzFiles\mysql\bin\RestoreMysql.bat"
                    pMySqlRestore.StartInfo = pi
                    pMySqlRestore.Start()
                End If
            Else
                Print("Restore cancelled")
            End If
        End If
    End Sub

    Private Sub BackupDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackupDatabaseToolStripMenuItem.Click

        If Running Then
            Print("Cannot backup safely when Opensim is running. Click [Stop] and try again.")
            Return
        End If

        If Not StartMySQL() Then Return

        Print("Starting a slow but extensive Database Backup => Autobackup folder")
        Dim pMySqlBackup As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = ""
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.WorkingDirectory = MyFolder & "\OutworldzFiles\mysql\bin\"
        pi.FileName = MyFolder & "\OutworldzFiles\mysql\bin\BackupMysql.bat"
        pMySqlBackup.StartInfo = pi

        pMySqlBackup.Start()

        Print("")
    End Sub

    Public Function StartMySQL() As Boolean

        ' Check for MySql operation
        Dim MysqlOk As Boolean

        MysqlConn = New Mysql   ' makes a new connection for us to send SQL statements on

        MysqlOk = CheckMysql()
        If MysqlOk Then Return True
        ' Start MySql in background.

        BumpProgress10()
        Dim StartValue = ProgressBar1.Value
        Print("Starting Database")

        ' SAVE INI file
        MySetting.LoadIni(MyFolder & "\OutworldzFiles\mysql\my.ini", "#")
        MySetting.SetIni("mysqld", "basedir", """" + gCurSlashDir + "/OutworldzFiles/Mysql" + """")
        MySetting.SetIni("mysqld", "datadir", """" + gCurSlashDir + "/OutworldzFiles/Mysql/Data" + """")
        MySetting.SetIni("mysqld", "port", MySetting.MySqlPort)
        MySetting.SetIni("client", "port", MySetting.MySqlPort)

        MySetting.SaveMyINI()

        ' create test program 
        ' slants the other way:
        Dim testProgram As String = MyFolder & "\OutworldzFiles\Mysql\bin\StartManually.bat"
        Try
            My.Computer.FileSystem.DeleteFile(testProgram)
        Catch ex As Exception
            Log("DeleteFile: " + ex.Message)
        End Try
        Try
            Using outputFile As New StreamWriter(testProgram, True)
                outputFile.WriteLine("@REM A program to run Mysql manually for troubleshooting." + vbCrLf _
                                     + "mysqld.exe --defaults-file=" + """" + gCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """")
            End Using
        Catch ex As Exception
            Log("Error:StartManually" + ex.Message)
        End Try

        BumpProgress(5)

        ' Mysql was not running, so lets start it up.
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = "--defaults-file=" + """" + gCurSlashDir + "/OutworldzFiles/mysql/my.ini" + """"
        pi.WindowStyle = ProcessWindowStyle.Hidden
        pi.FileName = """" + MyFolder & "\OutworldzFiles\mysql\bin\mysqld.exe" + """"
        pMySql.StartInfo = pi
        pMySql.Start()

        ' wait for MySql to come up
        While Not MysqlOk And Running

            BumpProgress(1)
            Application.DoEvents()

            Dim MysqlLog As String = MyFolder + "\OutworldzFiles\mysql\data"
            If ProgressBar1.Value = 100 Then ' about 30 seconds when it fails

                Dim yesno = MsgBox("The database did not start. Do you want to see the log file?", vbYesNo, "Error")
                If (yesno = vbYes) Then
                    Dim files() As String
                    files = Directory.GetFiles(MysqlLog, "*.err", SearchOption.TopDirectoryOnly)
                    For Each FileName As String In files
                        System.Diagnostics.Process.Start("notepad.exe", FileName)
                    Next
                End If
                Buttons(StartButton)
                Return False
            End If

            ' check again
            Sleep(2000)
            MysqlOk = CheckMysql()
        End While

        Return True

    End Function

    Function CheckMysql() As Boolean

        Dim version = MysqlConn.IsMySqlRunning()

        If version Is Nothing Then
            Return False
        End If
        Return True

    End Function

    Private Sub StopMysql()

        Print("Stopping MySql")

        Try
            MysqlConn.Dispose()
        Catch
        End Try


        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = "-u root shutdown"
        pi.FileName = """" + MyFolder + "\OutworldzFiles\mysql\bin\mysqladmin.exe" + """"
        pi.WindowStyle = ProcessWindowStyle.Minimized
        p.StartInfo = pi
        Try
            p.Start()
            Sleep(1)
            p.WaitForExit()
            p.Close()
        Catch ex As Exception
            Log("Error:mysqladmin failed to stop mysql:" + ex.Message)
        End Try

        Dim Mysql = CheckPort("127.0.0.1", MySetting.MySqlPort)
        If Mysql Then
            Sleep(4000)
            Try
                pMySql.Close()
            Catch ex2 As Exception
                Log("Error:Process pMySql.Close() " + ex2.Message)
            End Try
        End If

        Return

        ' no zapping any more, too dangerous
        Mysql = CheckPort("127.0.0.1", MySetting.MySqlPort)
        If Mysql Then
            Zap("mysqld")
        End If

    End Sub

#End Region

#Region "DNS"

    Public Function RegisterDNS() As Boolean

        If MySetting.DNSName = String.Empty Then
            Return True
        End If

        Dim client As New System.Net.WebClient
        Dim Checkname As String = String.Empty


        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx/?GridName=" + MySetting.DNSName + "&r=" + Random())
        Catch ex As Exception
            Log("Warn:Cannot check the DNS Name" + ex.Message)
            Return False
        End Try
        Return True

    End Function

    Public Function DoGetHostAddresses(hostName As [String]) As String

        Try
            Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

            For Each IPaddress In IPList.AddressList
                If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then
                    Dim ip = IPaddress.ToString()
                    Return ip
                End If
            Next
            Return String.Empty

        Catch ex As Exception
            Log("Warn:Unable to resolve name:" + ex.Message)
        End Try
        Return String.Empty

    End Function

    Public Function GetNewDnsName() As String

        Dim client As New System.Net.WebClient
        Dim Checkname As String = String.Empty
        Try
            Checkname = client.DownloadString("http://outworldz.net/getnewname.plx/?r=" + Random())
        Catch ex As Exception
            Log("Warn:Cannot get new name:" + ex.Message)
        End Try
        Return Checkname

    End Function

    Public Function RegisterName(name As String) As String

        Dim Checkname As String = String.Empty

        Try
            Checkname = client.DownloadString("http://outworldz.net/dns.plx/?GridName=" + name _
                                              + "&ID=" + MySetting.MachineID _
                                              + "&Port=" + MySetting.HttpPort _
                                              + "&r=" + Random())
        Catch ex As Exception
            Log("Warn: Cannot check the DNS Name" + ex.Message)
        End Try
        If Checkname = "NEW" Or Checkname = "UPDATED" Then
            Return name
        End If
        Return ""

    End Function

    Private Sub NewDNSName()

        If MySetting.DNSName = "" Then
            Dim newname = GetNewDnsName()
            If newname <> "" Then
                If RegisterName(newname) <> "" Then
                    BumpProgress10()
                    MySetting.DNSName = newname

                    MySetting.SaveMyINI()
                    MsgBox("Your system's name has been set to " + newname + ". You can change the name in the Advanced menu at any time", vbInformation, "Info")
                End If
            End If
            BumpProgress10()
        End If

    End Sub

    Private Sub CheckDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckDatabaseToolStripMenuItem.Click

        If Not StartMySQL() Then Return

        Dim pi As ProcessStartInfo = New ProcessStartInfo()

        ChDir(MyFolder & "\OutworldzFiles\mysql\bin")
        pi.WindowStyle = ProcessWindowStyle.Normal
        pi.Arguments = MySetting.MySqlPort
        pi.FileName = "CheckAndRepair.bat"
        pMySqlDiag.StartInfo = pi
        pMySqlDiag.Start()
        pMySqlDiag.WaitForExit()
        ChDir(MyFolder)

    End Sub

#End Region


#Region "Regions"

    Private Sub RegionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegionsToolStripMenuItem.Click

        If RegionList.InstanceExists = False Then
            Dim RegionForm As New RegionList
            RegionForm.Show()
        End If

    End Sub


    Private Sub RegionClick(sender As ToolStripMenuItem, e As EventArgs)

        Dim num As Integer = RegionClass.FindRegionByName(sender.Text)

        ' Handle robust clicking
        If sender.Text = "Robust" Then
            If gRobustProcID Then
                ConsoleCommand(gRobustProcID, "quit{ENTER}")
                Me.Focus()
                Log("Region:Stopped Robust")
                sender.Checked = True
                Return
            Else
                Print("Starting Robust")
                If Not StartMySQL() Then Return
                Start_Robust()
                sender.Checked = False
            End If

            Return

        Else ' had to be a region that was clicked

            Debug.Print("Region:Clicked region " & sender.Text)

            ' Running, stop it
            If RegionClass.RegionEnabled(num) And (RegionClass.Booted(num) Or RegionClass.WarmingUp(num)) Then

                ' if enabled and running, even partly up, stop it.
                sender.Checked = False
                If Running Then
                    Try
                        ConsoleCommand(RegionClass.ProcessID(num), "quit{ENTER}")
                        Me.Focus()

                        RegionClass.RegionEnabled(num) = False
                        RegionClass.Booted(num) = False
                        RegionClass.WarmingUp(num) = False
                        RegionClass.ShuttingDown(num) = True

                        MySetting.LoadIni(RegionClass.RegionPath(num), ";")
                        MySetting.SetIni(sender.Text, "Enabled", "false")

                        MySetting.SaveMyINI()

                        Debug.Print("Region:Stopped Region " + RegionClass.RegionName(num))
                    Catch ex As Exception
                        Debug.Print("Region:Could not stop Opensim")
                    End Try
                End If

            ElseIf Not (RegionClass.Booted(num) Or RegionClass.WarmingUp(num) Or RegionClass.ShuttingDown(num)) Then
                ' it was stopped, and disabled, so we toggle the enable, and start up
                sender.Checked = True

                ' and region file on disk
                MySetting.LoadIni(RegionClass.RegionPath(num), ";")
                MySetting.SetIni(sender.Text, "Enabled", "true")

                MySetting.SaveMyINI()

                Boot(RegionClass.RegionName(num))
                Debug.Print("Region:Started Region " + RegionClass.RegionName(num))
            End If
        End If

    End Sub

    Private Sub ScanAgents()


        ' Scan all the regions

        For Each X As Integer In RegionClass.RegionNumbers
            If RegionClass.Booted(X) Then
                RegionClass.AvatarCount(X) = MysqlConn.IsUserPresent(RegionClass.UUID(X))
                'Debug.Print(RegionClass.AvatarCount(X).ToString + " avatars in region " + RegionClass.RegionName(X))
            Else
                RegionClass.AvatarCount(X) = 0
            End If
        Next

        If MySetting.AutoLoad Then
            For Each X As Integer In RegionClass.RegionNumbers
                If RegionClass.Timer(X) > 0 Then RegionClass.Timer(X) = RegionClass.Timer(X) - 1

                ' if enabled and running, stopit
                If RegionClass.AvatarCount(X) = 0 Then
                    If Running And RegionClass.RegionEnabled(X) And RegionClass.Booted(X) Then
                        Debug.Print("AutoLoad is shutting down " + RegionClass.RegionName(X))
                        ConsoleCommand(RegionClass.ProcessID(X), "quit{ENTER}")
                        Me.Focus()
                        RegionClass.Booted(X) = False
                        RegionClass.WarmingUp(X) = False
                        RegionClass.ShuttingDown(X) = True
                    End If
                Else
                    RegionClass.Timer(X) = 60 ' 60 seconds and we will shut it off
                End If
            Next
        End If

    End Sub

#End Region



#Region "Maps"

    Private Sub MapDelete()

        If Running And MySetting.MapType <> "None" Then
            Try
                My.Computer.FileSystem.DeleteDirectory(prefix & "bin\maptiles\", FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch
            End Try
        End If


    End Sub




#End Region


End Class
