Public Class Settings
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization code here
        Me.lblOriginalMovieFolder.Text = My.Settings.OriginalMovieFolder
        Me.lblRenamedMovieFolder.Text = My.Settings.RenamedMovieFolder
        Me.lblOriginalTVFolder.Text = My.Settings.OriginalTVFolder
        Me.lblRenamedTVFolder.Text = My.Settings.RenamedTVFolder
        Me.cbWriteResults.Checked = My.Settings.WriteResults
        Me.cbSuggestFileName.Checked = My.Settings.SuggestFileName
        Me.cbIndividualFolders.Checked = My.Settings.IndividualFolders
        Me.cbIncludeSubtitleFiles.Checked = My.Settings.IncludeSubtitleFiles
        Me.cbMakeChanges.Checked = My.Settings.MakeChanges
        Me.cbRemoveEmptyFolders.Checked = My.Settings.RemoveEmptyFolders

        Dim tooltip As New ToolTip()
        ' Set properties for the ToolTip (optional)
        tooltip.AutoPopDelay = 5000  ' Time in milliseconds the tooltip remains visible
        tooltip.InitialDelay = 1000 ' Time in milliseconds before the tooltip appears
        tooltip.ReshowDelay = 500   ' Time in milliseconds before reappearing
        tooltip.ShowAlways = True   ' Ensures the tooltip is displayed even if the form is inactive

        tooltip.SetToolTip(cbRemoveEmptyFolders, "For example: You have one episode of the TV Show Banshee in the following folder structure: " _
            & vbCrLf & "c:\tv\banshee\season 1\.  If you move the one episode to another folder this setting will remove" _
            & vbCrLf & "the season 1 folder and the banshee folder leaving you with c:\tv")
    End Sub

    Private Sub cbWriteResults_CheckedChanged(sender As Object, e As EventArgs) Handles cbWriteResults.CheckedChanged
        'If Me.cbWriteResults.Checked = True Then
        'My.Settings.WriteResults = True
        'Else
        'My.Settings.WriteResults = False
        'End If
    End Sub

    Private Sub cbSuggestFileName_CheckedChanged(sender As Object, e As EventArgs) Handles cbSuggestFileName.CheckedChanged
        'If Me.cbSuggestFileName.Checked = True Then
        'My.Settings.SuggestFileName = True
        'Else
        'My.Settings.SuggestFileName = False
        'End If
    End Sub

    Private Sub cbIndividualFolders_CheckedChanged(sender As Object, e As EventArgs) Handles cbIndividualFolders.CheckedChanged
        'If cbIndividualFolders.Checked = True Then
        'My.Settings.IndividualFolders = True
        'Else
        'My.Settings.IndividualFolders = False
        'End If
    End Sub

    Private Sub cbIncludeSubtitleFiles_CheckedChanged(sender As Object, e As EventArgs) Handles cbIncludeSubtitleFiles.CheckedChanged
        'If cbIncludeSubtitleFiles.Checked = True Then
        'My.Settings.IncludeSubtitleFiles = True
        'Else
        'My.Settings.IncludeSubtitleFiles = False
        'End If
    End Sub

    Private Sub cbMakeChanges_CheckedChanged(sender As Object, e As EventArgs) Handles cbMakeChanges.CheckedChanged
        'If cbMakeChanges.Checked = True Then
        'My.Settings.MakeChanges = True
        'Else
        'My.Settings.MakeChanges = False
        'End If
    End Sub

    Private Sub cbRemoveEmptyFolders_CheckedChanged(sender As Object, e As EventArgs) Handles cbRemoveEmptyFolders.CheckedChanged
        'If cbRemoveEmptyFolders.Checked = True Then
        'My.Settings.RemoveEmptyFolders = True
        'Else
        'My.Settings.RemoveEmptyFolders = False
        'End If
    End Sub

    Private Sub btnOriginalMovieFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalMovieFolder.Click
        Dim myfolder As String = ShowFolderChooser(My.Settings.OriginalMovieFolder)
        If myfolder <> "" Then
            Me.lblOriginalMovieFolder.Text = myfolder
        End If
    End Sub

    Private Sub btnRenamedMovieFolder_Click(sender As Object, e As EventArgs) Handles btnRenamedMovieFolder.Click
        Dim myfolder As String = ShowFolderChooser(My.Settings.RenamedMovieFolder)
        If myfolder <> "" Then
            Me.lblRenamedMovieFolder.Text = myfolder
        End If
    End Sub

    Private Sub btnOriginalTVFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalTVFolder.Click
        Dim myfolder As String = ShowFolderChooser(My.Settings.OriginalTVFolder)
        If myfolder <> "" Then
            Me.lblOriginalTVFolder.Text = myfolder
        End If
    End Sub

    Private Sub btnRenamedTVFolder_Click(sender As Object, e As EventArgs) Handles btnRenamedTVFolder.Click
        Dim myfolder As String = ShowFolderChooser(My.Settings.RenamedTVFolder)
        If myfolder <> "" Then
            Me.lblRenamedTVFolder.Text = myfolder
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        My.Settings.OriginalMovieFolder = Me.lblOriginalMovieFolder.Text
        My.Settings.RenamedMovieFolder = Me.lblRenamedMovieFolder.Text
        My.Settings.OriginalTVFolder = Me.lblOriginalTVFolder.Text
        My.Settings.OriginalTVFolder = Me.lblOriginalTVFolder.Text
        My.Settings.RenamedTVFolder = Me.lblRenamedTVFolder.Text
        My.Settings.WriteResults = Me.cbWriteResults.Checked
        My.Settings.SuggestFileName = Me.cbSuggestFileName.Checked
        My.Settings.IndividualFolders = Me.cbIndividualFolders.Checked
        My.Settings.IncludeSubtitleFiles = Me.cbIncludeSubtitleFiles.Checked
        My.Settings.MakeChanges = Me.cbMakeChanges.Checked
        My.Settings.RemoveEmptyFolders = Me.cbRemoveEmptyFolders.Checked

        Me.Close()
    End Sub
End Class