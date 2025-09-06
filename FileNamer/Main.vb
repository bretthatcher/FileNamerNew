''Imports System.IO

Imports System.Drawing.Text
Imports System.IO
Imports System.Net.Http.Headers

Public Class Main
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If cbMovies.Checked = False And cbTV.Checked = False Then
            MsgBox("Please select Movies or TV Shows")
            Exit Sub
        End If
        If cbMovies.Checked = True Then mediatype = "movie"
        If cbTV.Checked = True Then mediatype = "tvshow"
        Call ProcessFiles()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Call TMDB_Search_Media()
        Call PopulateListBoxRecursively(textOriginalFolder.Text, lbOriginal)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutform As New About()
        aboutform.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call Test_Parse_Name()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim settingsform As New Settings()
        settingsform.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        GetMediaInfo("G:\Movies\2 Fast 2 Furious (2003)\2 Fast 2 Furious (2003) 1080p HEVC 6CH.mp4")
    End Sub


    Private Sub btnOriginalFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(textOriginalFolder.Text)
        If myfolder <> "" Then
            lbOriginal.Items.Clear()
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

    Public Sub ProcessFiles()
        'Dim mediatype = "movie"
        Dim lasttvshow As String = ""
        Dim media_unknown As String = ""
        Dim url_search As String
        Dim loopcount As Long

        For loopcount = 0 To lbOriginal.Items.Count - 1
            mediadict.Clear()
            mediadict("NewFilePath") = textNewFolder.Text

            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
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

                        'Check to see if the tvshow season and episode actually exists
                        If mediatype = "tvshow" Then
                            mediatype = "tvexactshow"
                            Call ValidTVSeasonEpisode()
                            mediatype = "tvshow"
                        End If

                        Call GetMediaInfo(mediadict("OriginalFullPath"))

                        Select Case mediatype
                            Case "movie"
                                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                mediadict("NewFullPath") = mediadict("NewFullPath") & " " & mediadict("VideoResolution") & mediadict("VideoCodec") & mediadict("AudioChannels") & mediadict("OriginalFileExt")

                            Case "tvshow"
                                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("title") & " S" & mediadict("season") & "E" & mediadict("episode") & " - " & mediadict("episodename") & mediadict("OriginalFileExt")

                        End Select
                        If cbMakeChanges.Checked = True Then
                            File.Copy(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                        End If
                        lbNew.Items.Add(mediadict("NewFullPath"))
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

        Next

    End Sub

    Private Sub cbMovies_CheckedChanged(sender As Object, e As EventArgs) Handles cbMovies.CheckedChanged
        If True Then
            cbTV.Checked = False
            mediatype = "movie"
        End If
    End Sub

    Private Sub cbTV_CheckedChanged(sender As Object, e As EventArgs) Handles cbTV.CheckedChanged
        If True Then
            cbMovies.Checked = False
            mediatype = "tvshow"
        End If
    End Sub

    Private Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbSelectAll.CheckedChanged
        If True Then
            lbOriginal.BeginUpdate()

            For i As Integer = 0 To lbOriginal.Items.Count - 1
                lbOriginal.SetSelected(i, True)
            Next

            lbOriginal.TopIndex = 0

            lbOriginal.EndUpdate()

        End If
    End Sub
End Class