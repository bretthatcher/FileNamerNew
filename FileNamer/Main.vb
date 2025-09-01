''Imports System.IO

Imports System.Drawing.Text

Public Class Main
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call PopulateListBoxRecursively(ShowFolderChooser("g:\"), lbOriginal)
        'Dim aboutform As New About()
        'aboutform.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call ParseMovie()

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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GetMediaInfo("G:\Movies\2 Fast 2 Furious (2003)\2 Fast 2 Furious (2003) 1080p HEVC 6CH.mp4")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles textOriginalFolder.TextChanged

    End Sub

    Private Sub btnOriginalFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(textOriginalFolder.Text)
        If myfolder <> "" Then
            textOriginalFolder.Text = myfolder
            Call PopulateListBoxRecursively(textOriginalFolder.Text, lbOriginal)
        End If
    End Sub

    Private Sub btnNewFolder_Click(sender As Object, e As EventArgs) Handles btnNewFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(textNewFolder.Text)
        If myfolder <> "" Then
            textNewFolder.Text = myfolder
        End If
    End Sub
End Class