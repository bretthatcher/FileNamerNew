''Imports System.IO

Imports System.Drawing.Text
Imports System.IO
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Reflection.Emit

Public Class Main

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If lblOriginalFolder.Text = "" Then
            MsgBox("Please select your original files folder")
            Exit Sub
        End If

        If lblNewFolder.Text = "" Then
            MsgBox("Please select your new files folder")
            Exit Sub
        End If

        If (cbMovies.Checked = False And cbTV.Checked = False) And (mediaop = 0 Or mediaop = 1) Then
            MsgBox("Please select Movies or TV Shows")
            Exit Sub
        End If

        If lbOriginal.SelectedItems.Count = 0 Then
            MsgBox("Please select at least 1 original file to process")
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
        Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)

        Call cbSelectAll_CheckedChanged(Nothing, Nothing)

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutform As New About()
        aboutform.Show()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim settingsform As New Settings()
        AddHandler settingsform.FormClosed, AddressOf Settingsform_Closed
        settingsform.Show()

    End Sub

    Private Sub Settingsform_Closed(sender As Object, e As FormClosedEventArgs)
        cbMakeChanges.Checked = My.Settings.MakeChanges
    End Sub

    Private Sub btnOriginalFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(lblOriginalFolder.Text)
        If myfolder <> "" Then
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            lblOriginalFolder.Text = myfolder
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
        End If
    End Sub

    Private Sub btnNewFolder_Click(sender As Object, e As EventArgs) Handles btnNewFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(lblNewFolder.Text)
        If myfolder <> "" Then
            lblNewFolder.Text = myfolder
        End If
    End Sub
    Public Sub Move_Copy_Files()
        For loopcount = 0 To lbOriginal.Items.Count - 1

            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict.Clear()

                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
                mediadict("OriginalFilePath") = Path.GetDirectoryName(mediadict("OriginalFullPath"))
                mediadict("OriginalFullName") = Path.GetFileName(mediadict("OriginalFullPath"))

                mediadict("NewFilePath") = lblNewFolder.Text
                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("OriginalFullName")

                If cbMakeChanges.Checked = True Then
                    Select Case mediaop
                        Case 2
                            My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)

                            'This adds the operation to the filechanges list in case we need to undo the change
                            If My.Settings.WriteResults = True Then
                                filechanges.Add(New FileChange() With {
                                .sourcefile = mediadict("OriginalFullPath"),
                                .destinationfile = mediadict("NewFullPath"),
                                .fileoperation = "move"
                                })
                            End If

                            'this will delete empty folders back up to the root folder if they are empty as files are moved
                            If My.Settings.RemoveEmptyFolders = True Then
                                Call DeleteEmptyParentFolders(mediadict("OriginalFilePath"), lblOriginalFolder.Text)
                            End If

                        Case 3
                            My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)
                            If My.Settings.WriteResults = True Then
                                filechanges.Add(New FileChange() With {
                                .sourcefile = mediadict("OriginalFullPath"),
                                .destinationfile = mediadict("NewFullPath"),
                                .fileoperation = "copy"
                                })
                            End If
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
        Dim name_unknown As String = ""
        Dim loopcount As Long

        For loopcount = 0 To lbOriginal.Items.Count - 1

            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict.Clear()

                mediadict("NewFilePath") = lblNewFolder.Text.TrimEnd("\"c)
                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
                mediadict("OriginalFullName") = Path.GetFileName(mediadict("OriginalFullPath"))
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

                    Dim new_searchname As String = ""

                    Do While mediadict("totalresults") = 0

                        If name_unknown = mediadict("searchname") Then
                            If mediatype = "tvshow" Then
                                mediadict("totalresults") = 1
                                Exit Do
                                'mediadict("searchname") = tvdict("title")
                                'lasttvshow = name_unknown
                                'Call TMDB_Search_Media(mediatype)
                                'Call JSON_Parse(mediatype)
                            End If
                        Else
                            'Try again with a different search name
                            new_searchname = InputBox("Couldn't find a match for " & mediadict("searchname") & vbCrLf & "Please try a different name:", "No Matches Found", mediadict("searchname"))
                            If new_searchname <> "" Then
                                name_unknown = mediadict("searchname")
                                mediadict("searchname") = new_searchname

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

                                If lasttvshow = mediadict("searchname") Or name_unknown = mediadict("searchname") Then
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

                                    'this stores the last tvshow name for use when renaming multiple episodes of the same show
                                    lasttvshow = mediadict("searchname")
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

                                mediadict("NewFileName") = BuildMovieName()

                                mediadict("NewFileName") = mediadict("NewFileName") & mediadict("OriginalFileExt")

                                If My.Settings.IndividualFolders = True Then
                                    mediadict("NewFilePath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                End If

                                mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("NewFileName")

                                If cbMakeChanges.Checked = True Then
                                    Select Case mediaop
                                        Case 0
                                            My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)

                                            If My.Settings.WriteResults = True Then
                                                filechanges.Add(New FileChange() With {
                                                .sourcefile = mediadict("OriginalFullPath"),
                                                .destinationfile = mediadict("NewFullPath"),
                                                .fileoperation = "move"
                                                })
                                            End If

                                            If My.Settings.RemoveEmptyFolders = True Then
                                                'this will delete empty folders back up to the root folder if they are empty as files are moved
                                                Call DeleteEmptyParentFolders(mediadict("OriginalFilePath"), lblOriginalFolder.Text)
                                            End If

                                        Case 1
                                            My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

                                            If My.Settings.WriteResults = True Then
                                                filechanges.Add(New FileChange() With {
                                                .sourcefile = mediadict("OriginalFullPath"),
                                                .destinationfile = mediadict("NewFullPath"),
                                                .fileoperation = "copy"
                                                })
                                            End If
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
                                    mediadict("NewFileName") = BuildTVName()

                                    mediadict("NewFileName") = mediadict("NewFileName") & mediadict("OriginalFileExt")

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

                                                If My.Settings.WriteResults = True Then
                                                    filechanges.Add(New FileChange() With {
                                                    .sourcefile = mediadict("OriginalFullPath"),
                                                    .destinationfile = mediadict("NewFullPath"),
                                                    .fileoperation = "move"
                                                    })
                                                End If

                                                If My.Settings.RemoveEmptyFolders = True Then
                                                    'this will delete empty folders back up to the root folder if they are empty as files are moved
                                                    Call DeleteEmptyParentFolders(mediadict("OriginalFilePath"), lblOriginalFolder.Text)
                                                End If

                                            Case 1
                                                My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)

                                                If My.Settings.WriteResults = True Then
                                                    filechanges.Add(New FileChange() With {
                                                    .sourcefile = mediadict("OriginalFullPath"),
                                                    .destinationfile = mediadict("NewFullPath"),
                                                    .fileoperation = "move"
                                                    })
                                                End If
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
            lblExample.Visible = True
            lblExtras.Visible = True
            btnChangeExample.Visible = True

            Call BuildMovieExample()
        Else
            mediatype = ""
            lblExample.Visible = False
            lblExtras.Visible = False
            btnChangeExample.Visible = False
        End If

    End Sub

    Private Sub cbTV_CheckedChanged(sender As Object, e As EventArgs) Handles cbTV.CheckedChanged
        If cbTV.Checked = True Then
            cbMovies.Checked = False
            mediatype = "tvshow"
            lblExample.Visible = True
            lblExtras.Visible = True
            btnChangeExample.Visible = True

            Call BuildTVExample()
        Else
            mediatype = ""
            lblExample.Visible = False
            lblExtras.Visible = False
            btnChangeExample.Visible = False

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
        cbMakeChanges.Checked = My.Settings.MakeChanges

        Dim tooltip As New ToolTip()
        ' Set properties for the ToolTip (optional)
        tooltip.AutoPopDelay = 5000  ' Time in milliseconds the tooltip remains visible
        tooltip.InitialDelay = 1000 ' Time in milliseconds before the tooltip appears
        tooltip.ReshowDelay = 500   ' Time in milliseconds before reappearing
        tooltip.ShowAlways = True   ' Ensures the tooltip is displayed even if the form is inactive

        ' Add a tooltip to a control (e.g., a Button)
        tooltip.SetToolTip(cbMakeChanges, "If this is checked changes will be made to the files." & vbCrLf & "If not, it is a dry run with no changes.")
        tooltip.SetToolTip(cbMovies, "Determines what extras to add to the movie when it is renamed")

    End Sub

    Private Sub cmbOperation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperation.SelectedIndexChanged
        mediaop = cmbOperation.SelectedIndex
        btnProcess.Text = cmbOperation.SelectedItem.ToString & " Files"
    End Sub

    Private Sub UseDefaultMovieFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultMovieFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedMovieFolder) Then
            lblNewFolder.Text = My.Settings.RenamedMovieFolder
        Else
            My.Settings.RenamedMovieFolder = ""
        End If

        If Directory.Exists(My.Settings.OriginalMovieFolder) Then
            lblOriginalFolder.Text = My.Settings.OriginalMovieFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            cbMovies.Checked = True
            cbMovies_CheckedChanged(cbMovies, EventArgs.Empty)
        Else
            My.Settings.OriginalMovieFolder = ""
        End If

    End Sub

    Private Sub UseDefaultTVFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultTVFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedTVFolder) Then
            lblNewFolder.Text = My.Settings.RenamedTVFolder
        Else
            My.Settings.RenamedTVFolder = ""
        End If

        If Directory.Exists(My.Settings.OriginalTVFolder) Then
            lblOriginalFolder.Text = My.Settings.OriginalTVFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            cbTV.Checked = True
            cbTV_CheckedChanged(cbTV, EventArgs.Empty)

        Else
            My.Settings.OriginalTVFolder = ""
        End If
    End Sub

    Private Sub btnChangeExample_Click(sender As Object, e As EventArgs) Handles btnChangeExample.Click
        If cbTV.Checked Then
            Dim tvextrasform As New TVExtras
            AddHandler tvextrasform.FormClosed, AddressOf tvextrasform_Closed
            tvextrasform.Show()

        End If
        If cbMovies.Checked Then
            Dim movieextrasform As New MovieExtras
            AddHandler movieextrasform.FormClosed, AddressOf movieextrasform_Closed
            movieextrasform.Show()
        End If
    End Sub
    Private Sub tvextrasform_Closed(sender As Object, e As FormClosedEventArgs)
        Call BuildTVExample()
    End Sub
    Private Sub movieextrasform_Closed(sender As Object, e As FormClosedEventArgs)
        Call BuildMovieExample()
    End Sub
    Private Function BuildTVName() As String
        'mediadict("NewFileName") = mediadict("title") & " S" & mediadict("season") & "E" & mediadict("episode") & " - " & mediadict("episodename") & mediadict("OriginalFileExt")
        Dim tvtitle As String = mediadict("title")
        Dim tvdescription As String = mediadict("episodename")
        Dim tvtitleseperator As String = ""
        Dim seasontext As String = ""
        Dim seasonnumber As String = ""
        Dim seasonepisodeseperator As String = ""
        Dim episodetext As String = ""
        Dim episodenumber As String = ""
        Dim tvdescriptionseperator = ""

        If My.Settings.TVTitleSeperator <> "" Then
            Select Case My.Settings.TVTitleSeperator
                Case "<space>"
                    tvtitleseperator = " "
                Case "<dash>"
                    tvtitleseperator = "-"
                Case "<space><dash><space>"
                    tvtitleseperator = " - "
            End Select
        End If

        If My.Settings.TVSeasonText <> "" And My.Settings.TVSeasonText <> "<none>" Then
            seasontext = My.Settings.TVSeasonText
        End If

        If My.Settings.TVSeasonNumber <> "" Then
            seasonnumber = My.Settings.TVSeasonNumber
            Select Case seasonnumber
                Case "Leading Zero"
                    If CInt(mediadict("season")) < 10 Then
                        seasonnumber = "0" & mediadict("season")
                    End If
                Case "No Leading Zero"
                    seasonnumber = mediadict("season")

            End Select
        End If

        If My.Settings.TVSeasonEpisodeSeperator <> "" And My.Settings.TVSeasonEpisodeSeperator <> "<none>" Then
            seasonepisodeseperator = My.Settings.TVSeasonEpisodeSeperator
        End If

        If My.Settings.TVEpisodeText <> "" And My.Settings.TVEpisodeText <> "<none>" Then
            episodetext = My.Settings.TVEpisodeText
        End If

        If My.Settings.TVEpisodeNumber <> "" Then
            episodenumber = My.Settings.TVSeasonNumber
            Select Case episodenumber
                Case "Leading Zero"
                    If CInt(mediadict("episode")) < 10 Then
                        episodenumber = "0" & mediadict("episode")
                    End If
                Case "No Leading Zero"
                    episodenumber = mediadict("episode")
            End Select
        End If

        If My.Settings.TVDescriptionSeperator <> "" Then
            Select Case My.Settings.TVDescriptionSeperator
                Case "<space>"
                    tvdescriptionseperator = " "
                Case "<dash>"
                    tvdescriptionseperator = "-"
                Case "<space><dash><space>"
                    tvdescriptionseperator = " - "
            End Select
        End If

        If My.Settings.TVDescription <> "" Then
            If My.Settings.TVDescription = "Don't Show" Then tvdescription = ""
        End If

        Return tvtitle & tvtitleseperator & seasontext & seasonnumber & seasonepisodeseperator & episodetext & episodenumber & tvdescriptionseperator & tvdescription
    End Function

    Private Sub BuildTVExample()
        Dim tvtitle As String = "TVShow"
        Dim tvdescription As String = ""
        Dim tvtitleseperator As String = ""
        Dim seasontext As String = ""
        Dim seasonnumber As String = ""
        Dim seasonepisodeseperator As String = ""
        Dim episodetext As String = ""
        Dim episodenumber As String = ""
        Dim tvdescriptionseperator = ""

        If My.Settings.TVTitleSeperator <> "" Then
            Select Case My.Settings.TVTitleSeperator
                Case "<space>"
                    tvtitleseperator = " "
                Case "<dash>"
                    tvtitleseperator = "-"
                Case "<space><dash><space>"
                    tvtitleseperator = " - "
            End Select
        End If

        If My.Settings.TVSeasonText <> "" And My.Settings.TVSeasonText <> "<none>" Then
            seasontext = My.Settings.TVSeasonText
        End If

        If My.Settings.TVSeasonNumber <> "" Then
            seasonnumber = My.Settings.TVSeasonNumber
            Select Case seasonnumber
                Case "Leading Zero"
                    seasonnumber = "01"
                Case "No Leading Zero"
                    seasonnumber = "1"
            End Select
        End If

        If My.Settings.TVSeasonEpisodeSeperator <> "" And My.Settings.TVSeasonEpisodeSeperator <> "<none>" Then
            seasonepisodeseperator = My.Settings.TVSeasonEpisodeSeperator
        End If

        If My.Settings.TVEpisodeText <> "" And My.Settings.TVEpisodeText <> "<none>" Then
            episodetext = My.Settings.TVEpisodeText
        End If

        If My.Settings.TVEpisodeNumber <> "" Then
            episodenumber = My.Settings.TVSeasonNumber
            Select Case episodenumber
                Case "Leading Zero"
                    episodenumber = "01"
                Case "No Leading Zero"
                    episodenumber = "1"
            End Select
        End If

        If My.Settings.TVDescriptionSeperator <> "" Then
            Select Case My.Settings.TVDescriptionSeperator
                Case "<space>"
                    tvdescriptionseperator = " "
                Case "<dash>"
                    tvdescriptionseperator = "-"
                Case "<space><dash><space>"
                    tvdescriptionseperator = " - "
            End Select
        End If

        If My.Settings.TVDescription <> "" Then
            If My.Settings.TVDescription = "Show" Then tvdescription = "Description"
        End If

        lblExtras.Text = tvtitle & tvtitleseperator & seasontext & seasonnumber & seasonepisodeseperator & episodetext & episodenumber & tvdescriptionseperator & tvdescription
    End Sub


    Private Function BuildMovieName() As String
        Dim completemoviename As String = mediadict("NewFileName")

        'mediadict("NewFileName") = mediadict("NewFileName") & " " & mediadict("VideoResolution") & " " & mediadict("VideoCodec") & " " & mediadict("AudioChannels") & mediadict("OriginalFileExt")
        If My.Settings.MovieExtras IsNot Nothing Then
            For Each item As String In My.Settings.MovieExtras
                Select Case item
                    Case "Video Resolution"
                        completemoviename = completemoviename & " " & mediadict("VideoResolution")
                    Case "Video Codec"
                        completemoviename = completemoviename & " " & mediadict("VideoCodec")
                    Case "Audio Channels"
                        completemoviename = completemoviename & " " & mediadict("AudioChannels")
                End Select
            Next
        End If

        Return completemoviename

    End Function
    Private Sub BuildMovieExample()
        If My.Settings.MovieExtras IsNot Nothing Then
            lblExtras.Text = "Movie (2025)"
            For Each item As String In My.Settings.MovieExtras
                Select Case item
                    Case "Video Resolution"
                        lblExtras.Text = lblExtras.Text & " 1080p"
                    Case "Video Codec"
                        lblExtras.Text = lblExtras.Text & " HVEC"
                    Case "Audio Channels"
                        lblExtras.Text = lblExtras.Text & " 6CH"
                End Select
            Next
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim undochangesform As New UndoChanges
        undochangesform.Show()


    End Sub
End Class
