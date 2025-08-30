''Imports System.IO

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
End Class


