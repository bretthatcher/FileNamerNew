''Imports System.IO

Imports System.Drawing.Text

Public Class form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call PopulateListBoxRecursively(ShowFolderChooser("g:\"), ListBox1)
        'Dim aboutform As New About()
        'aboutform.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call ShowFolderChooser("g:\")
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutform As New About()
        aboutform.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim settingsform As New Settings()
        settingsform.Show()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim settingsform As New Settings()
        settingsform.Show()
    End Sub
    Private Sub GetMediaInfo(filepath As String)
        Dim mywidth As Long
        Dim myheight As Long
        Dim MI As MediaInfo
        MI = New MediaInfo

        MI.Open(filepath)

        mediadict.Add("VidWidth", MI.Get_(StreamKind.Visual, 0, "Width"))
        mediadict.Add("VidHeight", MI.Get_(StreamKind.Visual, 0, "Height"))
        mediadict.Add("VideoCodec", MI.Get_(StreamKind.Visual, 0, "Format"))
        mediadict.Add("AudioChannels", MI.Get_(StreamKind.Audio, 0, "Channel(s)"))

        MI.Close()

        'If the VideoCodec contains XviD then set the VideoCodec to XVID
        If mediadict.ContainsKey("VideoCodec") = True Then
            If InStr(1, mediadict("VideoCodec"), "XviD") > 0 Then
                mediadict("VideoCodec") = "XVID"
            End If
        End If

        'Set the VidResolution based off of the Width and Height of the video stream
        If mediadict.ContainsKey("VidWidth") And mediadict.ContainsKey("VidHeight") Then

            mywidth = Int(mediadict("VidWidth"))
            myheight = Int(mediadict("VidHeight"))

            Select Case True

           'Set to 4K
                Case myheight = 2160
                    mediadict("VidResolution") = "2160p"

           'Set to 1080p
                Case (myheight > 720 And myheight <= 1080)
                    mediadict("VidResolution") = "1080p"

           'Set to 720p
                Case (myheight > 480 And myheight <= 720)
                    mediadict("VidResolution") = "720p"

           'Set to 480p
                Case myheight = 480
                    mediadict("VidResolution") = "480p"

           'Set to SD if less than 480
                Case myheight < 480
                    mediadict("VidResolution") = "SD"

            End Select

        End If

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GetMediaInfo("G:\Movies\2 Fast 2 Furious (2003)\2 Fast 2 Furious (2003) 1080p HEVC 6CH.mp4")
    End Sub

    Private Sub GetMediaInfo2()
        Dim To_Display As String
        Dim Temp As String
        Dim MI As MediaInfo

        MI = New MediaInfo
        To_Display = MI.Option_("Info_Version", "0.7.0.0;MediaInfoDLL_Example_MSVB;0.7.0.0")

        If (To_Display.Length() = 0) Then
            RichTextBox1.Text = "MediaInfo.Dll: this version of the DLL is not compatible"
            Return
        End If

        'Information about MediaInfo
        To_Display += vbCrLf + vbCrLf + "Info_Parameters" + vbCrLf
        To_Display += MI.Option_("Info_Parameters")

        To_Display += vbCrLf + vbCrLf + "Info_Capacities" + vbCrLf
        To_Display += MI.Option_("Info_Capacities")

        To_Display += vbCrLf + vbCrLf + "Info_Codecs" + vbCrLf
        To_Display += MI.Option_("Info_Codecs")

        'An example of how to use the library
        To_Display += vbCrLf + vbCrLf + "Open" + vbCrLf
        MI.Open("G:\Movies\2 Fast 2 Furious (2003)\2 Fast 2 Furious (2003) 1080p HEVC 6CH.mp4")

        To_Display += vbCrLf + vbCrLf + "Inform with Complete=false" + vbCrLf
        MI.Option_("Complete")
        To_Display += MI.Inform()

        To_Display += vbCrLf + vbCrLf + "Inform with Complete=true" + vbCrLf
        MI.Option_("Complete", "1")
        To_Display += MI.Inform()

        To_Display += vbCrLf + vbCrLf + "Custom Inform" + vbCrLf
        MI.Option_("Inform", "General;Example : FileSize=%FileSize%")
        To_Display += MI.Inform()

        To_Display += vbCrLf + vbCrLf + "Get with Stream=General and Parameter='FileSize'" + vbCrLf
        To_Display += MI.Get_(StreamKind.General, 0, "FileSize")

        To_Display += vbCrLf + vbCrLf + "GetI with Stream=General and Parameter=46" + vbCrLf
        To_Display += MI.Get_(StreamKind.General, 0, 46)

        To_Display += vbCrLf + vbCrLf + "Count_Get with StreamKind=Stream_Audio" + vbCrLf
        Temp = MI.Count_Get(StreamKind.Audio)
        To_Display += Temp

        To_Display += vbCrLf + vbCrLf + "Get with Stream=General and Parameter='AudioCount'" + vbCrLf
        To_Display += MI.Get_(StreamKind.General, 0, "AudioCount")

        To_Display += vbCrLf + vbCrLf + "Get with Stream=Audio and Parameter='StreamCount'" + vbCrLf
        To_Display += MI.Get_(StreamKind.Audio, 0, "StreamCount")

        To_Display += vbCrLf + vbCrLf + "Get with Stream=Audio and Parameter='Width'" + vbCrLf
        To_Display += MI.Get_(StreamKind.Visual, 0, "Width")

        To_Display += vbCrLf + vbCrLf + "Get with Stream=Audio and Parameter='Height'" + vbCrLf
        To_Display += MI.Get_(StreamKind.Visual, 0, "Height")

        To_Display += vbCrLf + vbCrLf + "Get with Stream=Audio and Parameter='Codec/String'" + vbCrLf
        To_Display += MI.Get_(StreamKind.Visual, 0, "Format")

        To_Display += vbCrLf + vbCrLf + "Get with Stream=Audio and Parameter='Codec/String'" + vbCrLf
        To_Display += MI.Get_(StreamKind.Audio, 0, "Channel(s)")

        To_Display += RichTextBox1.Text + vbCrLf + vbCrLf + "Close" + vbCrLf
        MI.Close()

        'Displaying the text
        RichTextBox1.Text = To_Display
    End Sub
End Class