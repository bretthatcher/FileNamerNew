Public Class TVExtras
    Private Sub cmbTitleSeperator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTitleSeperator.SelectedIndexChanged

        Call BuildTVExtra()

    End Sub
    Private Sub BuildStandardExtras(myoption As Integer)
        My.Settings.TVTitleSeperator = "<space><dash><space>"
        My.Settings.TVSeasonNumber = "Leading Zero"
        My.Settings.TVEpisodeNumber = "Leading Zero"
        My.Settings.TVDescriptionSeperator = "<space><dash><space>"
        My.Settings.TVDescription = "Show"

        Select Case myoption
            Case 1
                My.Settings.TVSeasonText = "S"
                My.Settings.TVSeasonEpisodeSeperator = "<none>"
                My.Settings.TVEpisodeText = "E"
            Case 2
                My.Settings.TVSeasonText = "<none>"
                My.Settings.TVSeasonEpisodeSeperator = "x"
                My.Settings.TVEpisodeText = "<none>"
        End Select
    End Sub
    Private Sub BuildTVExtra()
        Dim tvtitle As String = "TVShow"
        Dim tvdescription As String = ""
        Dim tvtitleseperator As String = ""
        Dim seasontext As String = ""
        Dim seasonnumber As String = ""
        Dim seasonepisodeseperator As String = ""
        Dim episodetext As String = ""
        Dim episodenumber As String = ""
        Dim tvdescriptionseperator = ""

        If cmbTitleSeperator.SelectedIndex <> -1 Then
            Select Case cmbTitleSeperator.SelectedItem.ToString
                Case "<space>"
                    tvtitleseperator = " "
                Case "<dash>"
                    tvtitleseperator = "-"
                Case "<space><dash><space>"
                    tvtitleseperator = " - "
            End Select
        End If

        If cmbSeasonText.SelectedIndex <> -1 Then
            If cmbSeasonText.SelectedItem.ToString <> "<none>" Then
                seasontext = cmbSeasonText.SelectedItem.ToString
            End If
        End If

        If cmbSeasonNumber.SelectedIndex <> -1 Then
            seasonnumber = cmbSeasonNumber.SelectedItem.ToString
            Select Case seasonnumber
                Case "Leading Zero"
                    seasonnumber = "01"
                Case "No Leading Zero"
                    seasonnumber = "1"
            End Select
        End If

        If cmbSeasonEpisodeSeperator.SelectedIndex <> -1 Then
            If cmbSeasonEpisodeSeperator.SelectedItem.ToString <> "<none>" Then
                seasonepisodeseperator = cmbSeasonEpisodeSeperator.SelectedItem.ToString
            End If
        End If

        If cmbEpisodeText.SelectedIndex <> -1 Then
            If cmbEpisodeText.SelectedItem.ToString <> "<none>" Then
                episodetext = cmbEpisodeText.SelectedItem.ToString
            End If
        End If

        If cmbEpisodeNumber.SelectedIndex <> -1 Then
            episodenumber = cmbEpisodeNumber.SelectedItem.ToString
            Select Case episodenumber
                Case "Leading Zero"
                    episodenumber = "01"
                Case "No Leading Zero"
                    episodenumber = "1"
            End Select
        End If

        If cmbDescriptionSeperator.SelectedIndex <> -1 Then
            Select Case cmbDescriptionSeperator.SelectedItem.ToString
                Case "<space>"
                    tvdescriptionseperator = " "
                Case "<dash>"
                    tvdescriptionseperator = "-"
                Case "<space><dash><space>"
                    tvdescriptionseperator = " - "
            End Select
        End If

        If cmbDescription.SelectedIndex <> -1 Then
            If cmbDescription.SelectedItem.ToString = "Show" Then tvdescription = "Description"
        End If

        lblTVExtras.Text = tvtitle & tvtitleseperator & seasontext & seasonnumber & seasonepisodeseperator & episodetext & episodenumber & tvdescriptionseperator & tvdescription

    End Sub
    Private Sub EnableCustomOptions(turnon As Boolean)
        If turnon = True Then
            cmbTitleSeperator.Enabled = True
            cmbSeasonText.Enabled = True
            cmbSeasonNumber.Enabled = True
            cmbSeasonEpisodeSeperator.Enabled = True
            cmbEpisodeText.Enabled = True
            cmbEpisodeNumber.Enabled = True
            cmbDescriptionSeperator.Enabled = True
            cmbDescription.Enabled = True
        Else
            cmbTitleSeperator.Enabled = False
            cmbSeasonText.Enabled = False
            cmbSeasonNumber.Enabled = False
            cmbSeasonEpisodeSeperator.Enabled = False
            cmbEpisodeText.Enabled = False
            cmbEpisodeNumber.Enabled = False
            cmbDescriptionSeperator.Enabled = False
            cmbDescription.Enabled = False
        End If
    End Sub
    Private Sub SetcmbValues()
        If My.Settings.TVTitleSeperator <> "" Then
            cmbTitleSeperator.SelectedItem = My.Settings.TVTitleSeperator
        End If

        If My.Settings.TVSeasonText <> "" Then
            cmbSeasonText.SelectedItem = My.Settings.TVSeasonText
        End If

        If My.Settings.TVSeasonNumber <> "" Then
            cmbSeasonNumber.SelectedItem = My.Settings.TVSeasonNumber
        End If

        If My.Settings.TVSeasonEpisodeSeperator <> "" Then
            cmbSeasonEpisodeSeperator.SelectedItem = My.Settings.TVSeasonEpisodeSeperator
        End If

        If My.Settings.TVEpisodeText <> "" Then
            cmbEpisodeText.SelectedItem = My.Settings.TVEpisodeText
        End If

        If My.Settings.TVEpisodeNumber <> "" Then
            cmbEpisodeNumber.SelectedItem = My.Settings.TVEpisodeNumber
        End If

        If My.Settings.TVDescriptionSeperator <> "" Then
            cmbDescriptionSeperator.SelectedItem = My.Settings.TVDescriptionSeperator
        End If

        If My.Settings.TVDescription <> "" Then
            cmbDescription.SelectedItem = My.Settings.TVDescription
        End If

        Call BuildTVExtra()
    End Sub
    Private Sub TVExtras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case My.Settings.TVExtras
            Case 0, 1
                EnableCustomOptions(False)
                Call BuildStandardExtras(1)
                rbOption1.Checked = True
            Case 2
                EnableCustomOptions(False)
                Call BuildStandardExtras(2)
                rbOption2.Checked = True
            Case 3
                EnableCustomOptions(True)
                rbOption3.Checked = True

        End Select

        Call SetcmbValues()

    End Sub

    Private Sub btnSaveExit_Click(sender As Object, e As EventArgs) Handles btnSaveExit.Click
        If cmbTitleSeperator.SelectedIndex <> -1 Then
            My.Settings.TVTitleSeperator = cmbTitleSeperator.SelectedItem.ToString
        Else
            My.Settings.TVTitleSeperator = ""
        End If

        If cmbSeasonText.SelectedIndex <> -1 Then
            My.Settings.TVSeasonText = cmbSeasonText.SelectedItem.ToString
        Else
            My.Settings.TVSeasonText = ""
        End If

        If cmbSeasonNumber.SelectedIndex <> -1 Then
            My.Settings.TVSeasonNumber = cmbSeasonNumber.SelectedItem.ToString
        Else
            My.Settings.TVSeasonNumber = ""
        End If

        If cmbSeasonEpisodeSeperator.SelectedIndex <> -1 Then
            My.Settings.TVSeasonEpisodeSeperator = cmbSeasonEpisodeSeperator.SelectedItem.ToString
        Else
            My.Settings.TVSeasonEpisodeSeperator = ""
        End If

        If cmbEpisodeText.SelectedIndex <> -1 Then
            My.Settings.TVEpisodeText = cmbEpisodeText.SelectedItem.ToString
        Else
            My.Settings.TVEpisodeText = ""
        End If

        If cmbEpisodeNumber.SelectedIndex <> -1 Then
            My.Settings.TVEpisodeNumber = cmbEpisodeNumber.SelectedItem.ToString
        Else
            My.Settings.TVEpisodeNumber = ""
        End If

        If cmbDescriptionSeperator.SelectedIndex <> -1 Then
            My.Settings.TVDescriptionSeperator = cmbDescriptionSeperator.SelectedItem.ToString
        Else
            My.Settings.TVDescriptionSeperator = ""
        End If

        If cmbDescription.SelectedIndex <> -1 Then
            My.Settings.TVDescription = cmbDescription.SelectedItem.ToString
        Else
            My.Settings.TVDescription = ""
        End If

        If rbOption1.Checked = True Then My.Settings.TVExtras = 1
        If rbOption2.Checked = True Then My.Settings.TVExtras = 2
        If rbOption3.Checked = True Then My.Settings.TVExtras = 3

        Me.Close()

    End Sub

    Private Sub cmbSeasonText_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSeasonText.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbSeasonNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSeasonNumber.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbSeasonEpisodeSeperator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSeasonEpisodeSeperator.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbEpisodeText_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEpisodeText.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbEpisodeNumber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEpisodeNumber.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbDescriptionSeperator_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDescriptionSeperator.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub cmbDescription_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDescription.SelectedIndexChanged
        Call BuildTVExtra()
    End Sub

    Private Sub rbOption1_CheckedChanged(sender As Object, e As EventArgs) Handles rbOption1.CheckedChanged
        If rbOption1.Checked = True Then
            Call BuildStandardExtras(1)
            EnableCustomOptions(False)
            Call SetcmbValues()
        End If
    End Sub

    Private Sub rbOption2_CheckedChanged(sender As Object, e As EventArgs) Handles rbOption2.CheckedChanged
        If rbOption2.Checked = True Then
            Call BuildStandardExtras(2)
            EnableCustomOptions(False)
            Call SetcmbValues()
        End If
    End Sub

    Private Sub rbOption3_CheckedChanged(sender As Object, e As EventArgs) Handles rbOption3.CheckedChanged
        If rbOption3.Checked = True Then
            EnableCustomOptions(True)
            Call SetcmbValues()
        End If
    End Sub
End Class