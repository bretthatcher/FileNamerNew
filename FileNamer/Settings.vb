Public Class Settings
    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization code here
        Me.txtOriginalMovieFolder.Text = My.Settings.OriginalMovieFolder
        Me.txtRenamedMovieFolder.Text = My.Settings.RenamedMovieFolder
        Me.txtOriginalTVFolder.Text = My.Settings.OriginalTVFolder
        Me.txtRenamedTVFolder.Text = My.Settings.RenamedTVFolder
        Me.cbWriteResults.Checked = My.Settings.WriteResults
        Me.cbSuggestFileName.Checked = My.Settings.SuggestFileName
        Me.cbIndividualFolders.Checked = My.Settings.IndividualFolders
        Me.cbIncludeSubtitleFiles.Checked = My.Settings.IncludeSubtitleFiles
        Me.cbMakeChanges.Checked = My.Settings.MakeChanges
    End Sub

    Private Sub cbWriteResults_CheckedChanged(sender As Object, e As EventArgs) Handles cbWriteResults.CheckedChanged
        If Me.cbWriteResults.Checked = True Then
            My.Settings.WriteResults = True
        Else
            My.Settings.WriteResults = False
        End If
    End Sub

    Private Sub cbSuggestFileName_CheckedChanged(sender As Object, e As EventArgs) Handles cbSuggestFileName.CheckedChanged
        If Me.cbSuggestFileName.Checked = True Then
            My.Settings.SuggestFileName = True
        Else
            My.Settings.SuggestFileName = False
        End If
    End Sub

    Private Sub cbIndividualFolders_CheckedChanged(sender As Object, e As EventArgs) Handles cbIndividualFolders.CheckedChanged
        If cbIndividualFolders.Checked = True Then
            My.Settings.IndividualFolders = True
        Else
            My.Settings.IndividualFolders = False
        End If
    End Sub

    Private Sub cbIncludeSubtitleFiles_CheckedChanged(sender As Object, e As EventArgs) Handles cbIncludeSubtitleFiles.CheckedChanged
        If cbIncludeSubtitleFiles.Checked = True Then
            My.Settings.IncludeSubtitleFiles = True
        Else
            My.Settings.IncludeSubtitleFiles = False
        End If
    End Sub

    Private Sub cbMakeChanges_CheckedChanged(sender As Object, e As EventArgs) Handles cbMakeChanges.CheckedChanged
        If cbMakeChanges.Checked = True Then
            My.Settings.MakeChanges = True
        Else
            My.Settings.MakeChanges = False
        End If
    End Sub

    Private Sub btnOriginalMovieFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalMovieFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(My.Settings.OriginalMovieFolder)
        If myfolder <> "" Then
            My.Settings.OriginalMovieFolder = myfolder
            Me.txtOriginalMovieFolder.Text = My.Settings.OriginalMovieFolder
        End If
    End Sub

    Private Sub btnRenamedMovieFolder_Click(sender As Object, e As EventArgs) Handles btnRenamedMovieFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(My.Settings.RenamedMovieFolder)
        If myfolder <> "" Then
            My.Settings.RenamedMovieFolder = myfolder
            Me.txtRenamedMovieFolder.Text = My.Settings.RenamedMovieFolder
        End If
    End Sub

    Private Sub btnOriginalTVFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalTVFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(My.Settings.OriginalTVFolder)
        If myfolder <> "" Then
            My.Settings.OriginalTVFolder = myfolder
            Me.txtOriginalTVFolder.Text = My.Settings.OriginalTVFolder
        End If
    End Sub

    Private Sub btnRenamedTVFolder_Click(sender As Object, e As EventArgs) Handles btnRenamedTVFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(My.Settings.RenamedTVFolder)
        If myfolder <> "" Then
            My.Settings.RenamedTVFolder = myfolder
            Me.txtRenamedTVFolder.Text = My.Settings.RenamedTVFolder
        End If
    End Sub
End Class