''Imports System.IO

Imports System.Drawing.Text
Imports System.IO
Imports System.Net.Http.Headers
Imports System.Reflection.Emit

Public Class Main
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If textOriginalFolder.Text = "" Then
            MsgBox("Please select your original files folder")
            Exit Sub
        End If

        If textNewFolder.Text = "" Then
            MsgBox("Please select your new files folder")
            Exit Sub
        End If

        If (cbMovies.Checked = False And cbTV.Checked = False) And (mediaop = 0 Or mediaop = 1) Then
            MsgBox("Please select Movies or TV Shows")
            Exit Sub
        End If

        If cbMovies.Checked = True Then mediatype = "movie"
        If cbTV.Checked = True Then mediatype = "tvshow"

        Select Case mediaop
            Case 0, 1
                Call ProcessFiles()
            Case 2, 3
                Call Move_Copy_Files()
        End Select

        'Clear both original and new listboxes - re-populate original listbox
        MsgBox(btnProcess.Text & " complete")

        lbOriginal.Items.Clear()
        lbNew.Items.Clear()
        Call PopulateListBoxRecursively(textOriginalFolder.Text, lbOriginal)

        Call cbSelectAll_CheckedChanged(Nothing, Nothing)

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutform As New About()
        aboutform.Show()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim settingsform As New Settings()
        settingsform.Show()
    End Sub

    Private Sub btnOriginalFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(textOriginalFolder.Text)
        If myfolder <> "" Then
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
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
    Public Sub Move_Copy_Files()
        For loopcount = 0 To lbOriginal.Items.Count - 1

            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict.Clear()

                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
                mediadict("OriginalFullName") = Path.GetFileName(mediadict("OriginalFullPath"))

                mediadict("NewFilePath") = textNewFolder.Text
                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("OriginalFullName")

                If cbMakeChanges.Checked = True Then
                    Select Case mediaop
                        Case 2
                            My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                        Case 3
                            My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)
                    End Select
                End If

                lbNew.Items.Add(mediadict("NewFullPath"))

                If loopcount > 17 Then
                    lbOriginal.TopIndex = loopcount - 17
                    lbNew.TopIndex = loopcount - 17
                End If
            Else
                lbNew.Items.Add("")
            End If
            lbOriginal.Update()
            lbNew.Update()
        Next
    End Sub

    Public Sub ProcessFiles()
        'Dim mediatype = "movie"
        Dim lasttvshow As String = ""
        Dim media_unknown As String = ""
        Dim url_search As String
        Dim loopcount As Long

        For loopcount = 0 To lbOriginal.Items.Count - 1

            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict.Clear()

                mediadict("NewFilePath") = textNewFolder.Text
                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
                mediadict("OriginalFullName") = Path.GetFileName("OriginalFullPath")
                mediadict("OriginalFileName") = Path.GetFileNameWithoutExtension(mediadict("OriginalFullPath"))
                mediadict("OriginalFileExt") = Path.GetExtension(mediadict("OriginalFullPath"))
                mediadict("OriginalFilePath") = Path.GetDirectoryName(mediadict("OriginalFullPath"))

                'Pull out important information from the filename like show name, season and episode numbers, year etc
                Call Parse_Name(mediadict("OriginalFileName"), mediatype)

                If goodfile() = True Then

                    'Build the search URL and call TMDB to search for the media
                    'url_search = Build_TMDB_URL(mediatype)
                    Call TMDB_Search_Media(mediatype)

                    'Parse the returned JSON and populate the movies, tvshows, tvseasons or tvepisodes lists
                    Call JSON_Parse(mediatype)

                    Dim url_tryagain As String

                    Do While mediadict("totalresults") = 0

                        If media_unknown = mediadict("searchname") Then
                            If mediatype = "tvshow" Then
                                mediadict("searchname") = media_unknown
                                Call TMDB_Search_Media(mediatype)
                                Call JSON_Parse(mediatype)
                            End If
                        Else

                            'Try again with a different search name
                            url_tryagain = InputBox("Couldn't find a match for " & mediadict("searchname") & vbCrLf & "Please try a different name:", "No Matches Found", mediadict("searchname"))
                            If url_tryagain <> "" Then
                                media_unknown = mediadict("searchname")
                                mediadict("searchname") = url_tryagain

                                Call TMDB_Search_Media(mediatype)
                                Call JSON_Parse(mediatype)
                            Else
                                Exit Do
                            End If
                        End If
                    Loop

                    If mediadict("totalresults") > 0 Then

                        'This will take all the movies returned and determine if there is an exact match


                        'Dim mediaexact As New List(Of Movie)
                        Dim mediaarray As New List(Of Long)
                        'Dim position As Long

                        Select Case mediatype
                            Case "movie"
                                For Each movie In movies
                                    If mediadict("searchname").ToString.ToLower() = movie.title.ToLower() Then
                                        If mediadict.ContainsKey("searchyear") AndAlso mediadict("searchyear") <> "" Then
                                            If mediadict("searchyear") = movie.release_date Then
                                                mediaarray.Add(movies.IndexOf(movie))
                                            End If
                                        Else
                                            mediaarray.Add(movies.IndexOf(movie))
                                        End If
                                    End If
                                Next
                                If mediaarray.Count = 1 Or movies.Count = 1 Then
                                    If mediaarray.Count = 1 Then
                                        mediadict("title") = movies(mediaarray(0)).title
                                        mediadict("release_date") = movies(mediaarray(0)).release_date
                                        mediadict("id") = movies(mediaarray(0)).id
                                    End If
                                    If movies.Count = 1 Then
                                        mediadict("title") = movies(0).title
                                        mediadict("release_date") = movies(0).release_date
                                        mediadict("id") = movies(0).id
                                    End If
                                Else

                                    Dim duplicateform As New Duplicate()
                                    duplicateform.ShowDialog()

                                End If


                            Case "tvshow"

                                If lasttvshow = mediadict("searchname") Then
                                    mediadict("title") = tvdict("title")
                                    mediadict("id") = tvdict("id")
                                    mediadict("release_date") = tvdict("release_date")
                                Else
                                    For Each tvshow In tvshows
                                        If mediadict("searchname").ToString.ToLower() = tvshow.title.ToLower() Then
                                            mediaarray.Add(tvshows.IndexOf(tvshow))
                                        End If
                                    Next
                                    If mediaarray.Count = 1 Or tvshows.Count = 1 Then
                                        If mediaarray.Count = 1 Then
                                            mediadict("title") = tvshows(mediaarray(0)).title
                                            mediadict("release_date") = tvshows(mediaarray(0)).release_date
                                            mediadict("id") = tvshows(mediaarray(0)).id
                                        End If
                                        If tvshows.Count = 1 Then
                                            mediadict("title") = tvshows(0).title
                                            mediadict("release_date") = tvshows(0).release_date
                                            mediadict("id") = tvshows(0).id
                                        End If
                                    Else
                                        Dim duplicateform As New Duplicate()
                                        duplicateform.ShowDialog()

                                    End If

                                    'this store the last tvshow name for use when renaming multiple episodes of the same show
                                    lasttvshow = mediadict("title")
                                    tvdict("title") = mediadict("title")
                                    tvdict("id") = mediadict("id")
                                    tvdict("release_date") = mediadict("release_date")
                                End If

                        End Select

                        Call GetMediaInfo(mediadict("OriginalFullPath"))

                        Select Case mediatype
                            Case "movie"
                                'Build the new movie name and add options on the name as needed
                                mediadict("NewFileName") = mediadict("title") & " (" & mediadict("release_date") & ")"
                                mediadict("NewFileName") = mediadict("NewFileName") & " " & mediadict("VideoResolution") & " " & mediadict("VideoCodec") & " " & mediadict("AudioChannels") & mediadict("OriginalFileExt")

                                If My.Settings.IndividualFolders = True Then
                                    mediadict("NewFilePath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                End If

                                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("NewFileName")

                                If cbMakeChanges.Checked = True Then
                                    Select Case mediaop
                                        Case 0
                                            My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                            If mediadict("OriginalFilePath") <> textOriginalFolder.Text Then
                                                'If Directory.Exists(mediadict("OriginalFilePath")) And Directory.GetFiles(mediadict("OriginalFilePath").Length = 0) And Directory.GetDirectories(mediadict("OriginalFilePath").Length = 0) Then
                                                'Directory.Delete(mediadict("OriginalFilePath"))
                                                'End If
                                            End If
                                        Case 1
                                            My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

                                    End Select
                                    'My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                    'File.Copy(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                End If

                                lbNew.Items.Add(mediadict("NewFullPath"))

                            Case "tvshow"
                                'Check to see if the tvshow season and episode actually exists

                                mediatype = "tvexactshow"
                                Call ValidTVSeasonEpisode()
                                mediatype = "tvshow"

                                If mediadict("totalresults") <> 0 Then
                                    'Build the new tv show name and add options on the name as needed
                                    mediadict("NewFileName") = mediadict("title") & " S" & mediadict("season") & "E" & mediadict("episode") & " - " & mediadict("episodename") & mediadict("OriginalFileExt")

                                    If My.Settings.IndividualFolders = True Then
                                        mediadict("NewFilePath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                        mediadict("NewSeasonPath") = mediadict("NewFilePath") & "\Season " & mediadict("season")
                                        mediadict("NewFullPath") = mediadict("NewSeasonPath") & "\" & mediadict("NewFileName")
                                    Else
                                        mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("NewFileName")
                                    End If

                                    If cbMakeChanges.Checked = True Then
                                        Select Case mediaop
                                            Case 0
                                                My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                            Case 1
                                                My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                        End Select

                                        'File.Copy(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                    End If

                                    lbNew.Items.Add(mediadict("NewFullPath"))
                                Else
                                    lbNew.Items.Add(mediadict("title") & " (" & mediadict("release_date") & ") Season " & mediadict("searchseason") & " Episode " & mediadict("searchepisode") & " Not Found")
                                End If
                        End Select
                    Else
                        lbNew.Items.Add("Not Found")
                    End If
                Else
                    lbNew.Items.Add("Not Found")
                End If
                If loopcount > 17 Then
                    lbOriginal.TopIndex = loopcount - 17
                    lbNew.TopIndex = loopcount - 17
                End If
            Else
                lbNew.Items.Add("")
                If loopcount > 17 Then
                    lbOriginal.TopIndex = loopcount - 17
                    lbNew.TopIndex = loopcount - 17
                End If
            End If
            lbOriginal.Update()
            lbNew.Update()

        Next

    End Sub

    Private Sub cbMovies_CheckedChanged(sender As Object, e As EventArgs) Handles cbMovies.CheckedChanged
        If cbMovies.Checked = True Then
            cbTV.Checked = False
            mediatype = "movie"
        End If

    End Sub

    Private Sub cbTV_CheckedChanged(sender As Object, e As EventArgs) Handles cbTV.CheckedChanged
        If cbTV.Checked = True Then
            cbMovies.Checked = False
            mediatype = "tvshow"
        End If
    End Sub

    Private Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbSelectAll.CheckedChanged
        lbOriginal.BeginUpdate()
        If cbSelectAll.Checked = True Then

            For i As Integer = 0 To lbOriginal.Items.Count - 1
                lbOriginal.SetSelected(i, True)
            Next
        Else
            lbOriginal.ClearSelected()
        End If

        lbOriginal.TopIndex = 0
        lbOriginal.EndUpdate()
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmbOperation.SelectedIndex = 0
        mediaop = cmbOperation.SelectedIndex

    End Sub

    Private Sub cmbOperation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperation.SelectedIndexChanged
        mediaop = cmbOperation.SelectedIndex
        btnProcess.Text = cmbOperation.SelectedItem.ToString & " Files"
    End Sub

    Private Sub UseDefaultMovieFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultMovieFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedMovieFolder) Then
            textNewFolder.Text = My.Settings.RenamedMovieFolder
        Else
            My.Settings.RenamedMovieFolder = ""
        End If

        If Directory.Exists(My.Settings.OriginalMovieFolder) Then
            textOriginalFolder.Text = My.Settings.OriginalMovieFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(textOriginalFolder.Text, lbOriginal)
        Else
            My.Settings.OriginalMovieFolder = ""
        End If

    End Sub

    Private Sub UseDefaultTVFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultTVFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedTVFolder) Then
            textNewFolder.Text = My.Settings.RenamedTVFolder
        Else
            My.Settings.RenamedTVFolder = ""
        End If

        If Directory.Exists(My.Settings.OriginalTVFolder) Then
            textOriginalFolder.Text = My.Settings.OriginalTVFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(textOriginalFolder.Text, lbOriginal)
        Else
            My.Settings.OriginalTVFolder = ""
        End If
    End Sub

End Class