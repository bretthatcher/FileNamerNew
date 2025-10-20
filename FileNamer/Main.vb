''Imports System.IO

Imports System.Drawing.Text
Imports System.IO
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class Main

    Private cancelprocess As Boolean = False
    Private syncingScroll As Boolean = False ' To prevent infinite loop during sync

    Private isTMDB As Boolean = True
    Private TMDBImage As Image = My.Resources.tmdb
    Private TVDBImage As Image = My.Resources.tvdb

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If lblOriginalFolder.Text = "" Then
            MsgBox("Please select your Original files folder")
            Exit Sub
        End If

        If lblNewFolder.Text = "" Then
            MsgBox("Please select your New files folder")
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

        If isTMDB Then
            medialibrary = "TMDB"
        Else
            medialibrary = "TVDB"
        End If

        Dim msgresult As MsgBoxResult

        If cbMakeChanges.Checked = True Then
            msgresult = MsgBox("You are about to make changes to the original files.  Continue?", MsgBoxStyle.YesNo)
            If msgresult = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        If cbMakeChanges.Checked = False Then
            msgresult = MsgBox("Make Changes is not checked so this is a dry run. Continue?", MsgBoxStyle.YesNo)
            If msgresult = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        lblProcessingText.Visible = True
        lblProcessingOfText.Visible = True
        lblCurrentItemText.Visible = True
        lblTotalItemsText.Visible = True
        btnCancel.Visible = True

        Select Case mediaop
            Case 0, 1
                Call ProcessFiles()
            Case 2, 3
                Call Move_Copy_Files()
        End Select

        btnCancel.Visible = False


        'This form will show the results of the process
        Dim myData As New List(Of MyResults)()


        For i As Integer = 0 To lbNew.Items.Count - 1
            If lbNew.Items(i).ToString() <> "" Then
                'notFoundItems.Add((lbOriginal.Items(i).ToString(), lbNew.Items(i).ToString()))
                Dim myitem As New MyResults() With {
                        .Original = lbOriginal.Items(i).ToString(),
                        .NewName = lbNew.Items(i).ToString()
                    }
                myData.Add(myitem)
            End If
        Next

        If cancelprocess = True Then
            msgresult = MsgBox("Process canceled by the user.  Would you like to see the partial results?", MsgBoxStyle.YesNo)
            cancelprocess = False
        Else
            msgresult = MsgBox(btnProcess.Text & " complete.  Would you like to see the results?", MsgBoxStyle.YesNo)
        End If

        If msgresult = MsgBoxResult.Yes Then
            ' Show the new form
            Dim frm As New Results()
            frm.MyList = myData
            frm.Show()
        End If


        'Clear both original and new listboxes - re-populate original listbox
        lblProcessingText.Visible = False
        lblProcessingOfText.Visible = False
        lblCurrentItemText.Visible = False
        lblTotalItemsText.Visible = False


        lbOriginal.Items.Clear()
        lbNew.Items.Clear()

        If Directory.Exists(lblOriginalFolder.Text) Then
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            Call cbSelectAll_CheckedChanged(Nothing, Nothing)
        Else
            lblOriginalFolder.Text = ""
        End If

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
        cmbOperation.SelectedIndex = My.Settings.DefaultAction
        mediaop = cmbOperation.SelectedIndex
        cbMakeChanges.Checked = My.Settings.MakeChanges
        If lblOriginalFolder.Text <> "" Then
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
        End If
    End Sub

    Private Sub btnOriginalFolder_Click(sender As Object, e As EventArgs) Handles btnOriginalFolder.Click
        Dim myfolder As String

        myfolder = ShowFolderChooser(lblOriginalFolder.Text)
        If myfolder <> "" Then
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            lblOriginalFolder.Text = myfolder
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)

            cbSelectAll_CheckedChanged(cbSelectAll, EventArgs.Empty)
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

        Call RemoveUnselectedItems()

        If lbOriginal.Items.Count > 0 Then
            lblTotalItemsText.Text = lbOriginal.Items.Count
            lbOriginal.TopIndex = 0
        End If

        For loopcount As Long = 0 To lbOriginal.Items.Count - 1

            If cancelprocess = True Then
                Exit For
            End If

            lblCurrentItemText.Text = loopcount + 1

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
                            Try
                                My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

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

                                lbNew.Items.Add(mediadict("NewFullPath"))

                            Catch ex As Exception
                                MsgBox("Error - " & ex.Message.ToString)
                                lbNew.Items.Add("Error - Moving File")
                            End Try

                        Case 3
                            Try
                                My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)
                                If My.Settings.WriteResults = True Then
                                    filechanges.Add(New FileChange() With {
                                    .sourcefile = mediadict("OriginalFullPath"),
                                    .destinationfile = mediadict("NewFullPath"),
                                    .fileoperation = "copy"
                                    })
                                End If
                                lbNew.Items.Add(mediadict("NewFullPath"))

                            Catch ex As Exception
                                MsgBox("Error - " & ex.Message.ToString)
                                lbNew.Items.Add("Error - Copying File")
                            End Try
                    End Select
                Else

                    lbNew.Items.Add(mediadict("NewFullPath"))
                End If

                If loopcount > 17 Then
                    lbOriginal.TopIndex = loopcount - 17
                    lbNew.TopIndex = loopcount - 17
                End If
            Else
                lbNew.Items.Add("")
            End If

            lbOriginal.Update()
            lbNew.Update()
            lblCurrentItemText.Refresh()
            Application.DoEvents()

        Next
    End Sub
    Public Sub RemoveUnselectedItems()

        For loopcount As Long = lbOriginal.Items.Count - 1 To 0 Step -1
            If Not lbOriginal.GetSelected(loopcount) Then
                lbOriginal.Items.RemoveAt(loopcount)
            End If
        Next
    End Sub

    Public Sub ProcessFiles()
        Dim subtitlefile As Boolean = False
        Dim lasttvshow As String = ""
        Dim name_unknown As String = ""

        Call RemoveUnselectedItems()
        If lbOriginal.Items.Count > 0 Then
            lblTotalItemsText.Text = lbOriginal.Items.Count
            lbOriginal.TopIndex = 0
        End If

        For loopcount As Long = 0 To lbOriginal.Items.Count - 1

            If cancelprocess = True Then
                Exit For
            End If

            lblCurrentItemText.Text = loopcount + 1
            If lbOriginal.GetSelected(loopcount) = True Then

                mediadict.Clear()
                subtitlefile = False

                mediadict("NewFilePath") = lblNewFolder.Text.TrimEnd("\"c)
                mediadict("OriginalFullPath") = lbOriginal.Items(loopcount).ToString
                mediadict("OriginalFullName") = Path.GetFileName(mediadict("OriginalFullPath"))
                mediadict("OriginalFileName") = Path.GetFileNameWithoutExtension(mediadict("OriginalFullPath"))
                mediadict("OriginalFileExt") = Path.GetExtension(mediadict("OriginalFullPath"))
                mediadict("OriginalFilePath") = Path.GetDirectoryName(mediadict("OriginalFullPath"))

                If My.Settings.IncludeSubtitleFiles = True Then
                    If ValidSubTitleExtension(mediadict("OriginalFullName")) = True Then
                        subtitlefile = True
                    End If
                End If
                'Pull out important information from the filename like show name, season and episode numbers, year etc
                Call Parse_Name(mediadict("OriginalFileName"), mediatype)

                If goodfile() = True Then

                    'Build the search URL and call TMDB to search for the media
                    'url_search = Build_TMDB_URL(mediatype)
                    'Call TMDB_Search_Media(mediatype)

                    'Parse the returned JSON and populate the movies, tvshows, tvseasons or tvepisodes lists
                    'Call JSON_Parse(mediatype)

                    Call Search_Media(mediatype)

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
                            If My.Settings.SuggestFileName = True Then
                                new_searchname = InputBox("Couldn't find a match for " & mediadict("searchname") & vbCrLf & "Please try a different name:", "No Matches Found", mediadict("searchname"))
                                If String.IsNullOrEmpty(new_searchname) Then
                                    Exit Do
                                End If

                                If new_searchname <> mediadict("searchname") Then

                                    name_unknown = mediadict("searchname")
                                    mediadict("searchname") = new_searchname

                                    'Call TMDB_Search_Media(mediatype)
                                    'Call JSON_Parse(mediatype)

                                    Call Search_Media(mediatype)

                                Else
                                    Exit Do
                                End If
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
                                        If mediadict.ContainsKey("title") = False Then
                                            Exit Select
                                        End If

                                    End If

                                    'this stores the last tvshow name for use when renaming multiple episodes of the same show
                                    lasttvshow = mediadict("searchname")
                                    tvdict("title") = mediadict("title")
                                    tvdict("id") = mediadict("id")
                                    tvdict("release_date") = mediadict("release_date")
                                End If

                        End Select

                        'This checks to see if a valid media file was found - if not skip to end of loop
                        If mediadict.ContainsKey("title") = True Then

                            'Grab media info if not a subtitle file
                            If subtitlefile = False Then
                                Call GetMediaInfo(mediadict("OriginalFullPath"))
                            End If

                            'Build the new file name and path based off the type of media
                            Select Case mediatype
                                Case "movie"
                                    'Build the new movie name and add options on the name as needed
                                    mediadict("NewFileName") = mediadict("title") & " (" & mediadict("release_date") & ")"

                                    If subtitlefile = False Then
                                        mediadict("NewFileName") = BuildMovieName()
                                    End If

                                    mediadict("NewFileName") = mediadict("NewFileName") & mediadict("OriginalFileExt")

                                    'Then if creating individual folders - then add in the movie folder
                                    If My.Settings.IndividualFolders = True Then
                                        mediadict("NewFilePath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                    End If

                                    mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("NewFileName")

                                    'Make changes in the file system if Make Changes is checked
                                    If cbMakeChanges.Checked = True Then
                                        Select Case mediaop
                                            Case 0
                                                Try
                                                    'This will move the file to the new location and create any needed folders
                                                    My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

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

                                                    lbNew.Items.Add(mediadict("NewFullPath"))

                                                Catch ex As Exception
                                                    MsgBox("Error - " & ex.Message.ToString)
                                                    lbNew.Items.Add("Error - Moving File")
                                                End Try


                                            Case 1
                                                Try
                                                    My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

                                                    If My.Settings.WriteResults = True Then
                                                        filechanges.Add(New FileChange() With {
                                                        .sourcefile = mediadict("OriginalFullPath"),
                                                        .destinationfile = mediadict("NewFullPath"),
                                                        .fileoperation = "copy"
                                                        })
                                                    End If

                                                    lbNew.Items.Add(mediadict("NewFullPath"))

                                                Catch ex As Exception
                                                    MsgBox("Error - " & ex.Message.ToString)
                                                    lbNew.Items.Add("Error - Moving File")
                                                End Try
                                        End Select
                                        'My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                        'File.Copy(mediadict("OriginalFullPath"), mediadict("NewFullPath"), True)
                                    Else
                                        lbNew.Items.Add(mediadict("NewFullPath"))
                                    End If


                                Case "tvshow"
                                    'Check to see if the tvshow season and episode actually exists

                                    mediatype = "tvexactshow"
                                    'Call ValidTVSeasonEpisode()
                                    Call Search_Media(mediatype)
                                    mediatype = "tvshow"

                                    If mediadict("totalresults") <> 0 Then
                                        'Build the new tv show name and add options on the name as needed
                                        mediadict("NewFileName") = BuildTVName()

                                        mediadict("NewFileName") = mediadict("NewFileName") & mediadict("OriginalFileExt")

                                        'If creating individual folders - then add in the tv show folder and the season folder
                                        If My.Settings.IndividualFolders = True Then
                                            mediadict("NewFilePath") = mediadict("NewFilePath") & "\" & mediadict("title") & " (" & mediadict("release_date") & ")"
                                            mediadict("NewSeasonPath") = mediadict("NewFilePath") & "\Season " & mediadict("season")
                                            mediadict("NewFullPath") = mediadict("NewSeasonPath") & "\" & mediadict("NewFileName")
                                        Else
                                            'if not then just add the tv show episode to the root new folder
                                            mediadict("NewFullPath") = mediadict("NewFilePath") & "\" & mediadict("NewFileName")
                                        End If

                                        'Make changes in the file system if Make Changes is checked
                                        If cbMakeChanges.Checked = True Then
                                            Select Case mediaop
                                                Case 0
                                                    Try
                                                        My.Computer.FileSystem.MoveFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

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

                                                        lbNew.Items.Add(mediadict("NewFullPath"))

                                                    Catch ex As Exception
                                                        MsgBox("Error - " & ex.Message.ToString)
                                                        lbNew.Items.Add("Error - Moving File")
                                                    End Try



                                                Case 1
                                                    Try
                                                        My.Computer.FileSystem.CopyFile(mediadict("OriginalFullPath"), mediadict("NewFullPath"), FileIO.UIOption.AllDialogs)

                                                        If My.Settings.WriteResults = True Then
                                                            filechanges.Add(New FileChange() With {
                                                            .sourcefile = mediadict("OriginalFullPath"),
                                                            .destinationfile = mediadict("NewFullPath"),
                                                            .fileoperation = "move"
                                                            })
                                                        End If

                                                        lbNew.Items.Add(mediadict("NewFullPath"))

                                                    Catch ex As Exception
                                                        MsgBox("Error - " & ex.Message.ToString)
                                                        lbNew.Items.Add("Error - Copying File")
                                                    End Try

                                            End Select

                                        Else
                                            lbNew.Items.Add(mediadict("NewFullPath"))
                                        End If

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
                Else
                    lbNew.Items.Add("Not Found")
                End If
                If loopcount > 21 Then
                    lbOriginal.TopIndex = loopcount - 21
                    lbNew.TopIndex = loopcount - 21
                End If
            Else
                lbNew.Items.Add("")
                If loopcount > 21 Then
                    lbOriginal.TopIndex = loopcount - 21
                    lbNew.TopIndex = loopcount - 21
                End If
            End If
            lbOriginal.Update()
            lbNew.Update()
            lblCurrentItemText.Refresh()
            Application.DoEvents()

        Next

    End Sub

    Private Sub cbMovies_CheckedChanged(sender As Object, e As EventArgs) Handles cbMovies.CheckedChanged
        If cbMovies.Checked = True Then
            cbTV.Checked = False
            mediatype = "movie"

            lblLibraryText.Visible = True
            pbLibrary.Visible = True

            If My.Settings.DefaultMovieLibrary = 0 Then
                isTMDB = True
            Else
                isTMDB = False
            End If
            pbLibrary.Image = If(isTMDB, TMDBImage, TVDBImage)

            lblExample.Visible = True
            lblExtras.Visible = True
            btnChangeExample.Visible = True

            Call BuildMovieExample()
        Else
            mediatype = ""
            lblExample.Visible = False
            lblExtras.Visible = False
            btnChangeExample.Visible = False

            lblLibraryText.Visible = False
            pbLibrary.Visible = False

        End If


    End Sub

    Private Sub cbTV_CheckedChanged(sender As Object, e As EventArgs) Handles cbTV.CheckedChanged
        If cbTV.Checked = True Then

            cbMovies.Checked = False
            mediatype = "tvshow"

            lblLibraryText.Visible = True
            pbLibrary.Visible = True

            If My.Settings.DefaultTVLibrary = 0 Then
                isTMDB = True
            Else
                isTMDB = False
            End If
            pbLibrary.Image = If(isTMDB, TMDBImage, TVDBImage)

            lblExample.Visible = True
            lblExtras.Visible = True
            btnChangeExample.Visible = True

            If My.Settings.TVExtras = 0 Then
                Dim msgresult As MsgBoxResult = MsgBox("Since you don't have a default tv show format set, you will now be asked to set it.")

                Dim tvextrasform As New TVExtras
                AddHandler tvextrasform.FormClosed, AddressOf tvextrasform_Closed
                tvextrasform.Show()

            End If

            Call BuildTVExample()
        Else
            mediatype = ""
            lblExample.Visible = False
            lblExtras.Visible = False
            btnChangeExample.Visible = False

            lblLibraryText.Visible = False
            pbLibrary.Visible = False

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

        If My.Settings.DefaultAction < 0 Or My.Settings.DefaultAction > 3 Then
            My.Settings.DefaultAction = 0
        End If
        cmbOperation.SelectedIndex = My.Settings.DefaultAction
        mediaop = cmbOperation.SelectedIndex
        cbMakeChanges.Checked = My.Settings.MakeChanges

        pbLibrary.Image = TMDBImage
        pbLibrary.SizeMode = PictureBoxSizeMode.StretchImage

        lblLibraryText.Visible = False
        pbLibrary.Visible = False

        ' Attach event handlers for scrolling
        'AddHandler lbOriginal.MouseWheel, AddressOf ListBox_MouseWheel
        'AddHandler lbNew.MouseWheel, AddressOf ListBox_MouseWheel

        'Check to see if we have a valid TVDB token, if not then get one
        If My.Settings.JWTToken = "" Then
            Dim mytoken As String = GetJWTToken()
            My.Settings.JWTToken = mytoken
        Else
            Dim mytokenexpired As Boolean = IsJwtExpired(My.Settings.JWTToken)
            If mytokenexpired = True Then
                Dim mytoken As String = GetJWTToken()
                My.Settings.JWTToken = mytoken
            End If
        End If

        Dim tooltip As New ToolTip()
        ' Set properties for the ToolTip (optional)
        tooltip.AutoPopDelay = 5000  ' Time in milliseconds the tooltip remains visible
        tooltip.InitialDelay = 750 ' Time in milliseconds before the tooltip appears
        tooltip.ReshowDelay = 500   ' Time in milliseconds before reappearing
        tooltip.ShowAlways = True   ' Ensures the tooltip is displayed even if the form is inactive

        ' Add a tooltip to a control (e.g., a Button)
        tooltip.SetToolTip(cbMakeChanges, "If this is checked changes will be made to the files." & vbCrLf & "If not, it is a dry run with no changes.")
        tooltip.SetToolTip(cbMovies, "Sets the type of media to be Movies")
        tooltip.SetToolTip(cbTV, "Sets the type of media to be TV Shows")
        tooltip.SetToolTip(btnChangeExample, "Allows the user to set the defaul template for Movie or TV Show titles when renaming")
        tooltip.SetToolTip(btnOriginalFolder, "Sets the root folder for original files for the rename, copy or move process")
        tooltip.SetToolTip(btnNewFolder, "Sets the root folder for the new files after the rename, copy or move process")
        tooltip.SetToolTip(pbLibrary, "Sets the current library used to gather media information. Click to toggle.")
    End Sub

    Private Sub cmbOperation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOperation.SelectedIndexChanged
        mediaop = cmbOperation.SelectedIndex
        btnProcess.Text = cmbOperation.SelectedItem.ToString & " Files"
        Select Case mediaop
            Case 0, 1

                cbMovies.Enabled = True
                cbTV.Enabled = True

            Case 2, 3

                cbMovies.Checked = False
                cbTV.Checked = False
                cbMovies_CheckedChanged(cbMovies, EventArgs.Empty)
                cbTV_CheckedChanged(cbTV, EventArgs.Empty)
                cbMovies.Enabled = False
                cbTV.Enabled = False

        End Select
    End Sub

    Private Sub UseDefaultMovieFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultMovieFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedMovieFolder) Then
            lblNewFolder.Text = My.Settings.RenamedMovieFolder
        Else
            My.Settings.RenamedMovieFolder = ""
            lblNewFolder.Text = ""
        End If

        If Directory.Exists(My.Settings.OriginalMovieFolder) Then
            lblOriginalFolder.Text = My.Settings.OriginalMovieFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            cbSelectAll_CheckedChanged(cbSelectAll, EventArgs.Empty)
        Else
            My.Settings.OriginalMovieFolder = ""
            lblOriginalFolder.Text = ""
            lbOriginal.Items.Clear()

        End If

        If mediaop = 0 Or mediaop = 1 Then
            cbMovies.Enabled = True
            cbTV.Enabled = True
            cbMovies.Checked = True
            cbMovies_CheckedChanged(cbMovies, EventArgs.Empty)
        End If
    End Sub

    Private Sub UseDefaultTVFoldersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseDefaultTVFoldersToolStripMenuItem.Click
        If Directory.Exists(My.Settings.RenamedTVFolder) Then
            lblNewFolder.Text = My.Settings.RenamedTVFolder
        Else
            My.Settings.RenamedTVFolder = ""
            lblNewFolder.Text = ""
        End If

        If Directory.Exists(My.Settings.OriginalTVFolder) Then
            lblOriginalFolder.Text = My.Settings.OriginalTVFolder
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            cbSelectAll_CheckedChanged(cbSelectAll, EventArgs.Empty)
        Else
            My.Settings.OriginalTVFolder = ""
            lblOriginalFolder.Text = ""
            lbOriginal.Items.Clear()
        End If

        If mediaop = 0 Or mediaop = 1 Then
            cbTV.Enabled = True
            cbMovies.Enabled = True
            cbTV.Checked = True
            cbTV_CheckedChanged(cbTV, EventArgs.Empty)
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
                    Else
                        seasonnumber = mediadict("season")
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
            episodenumber = My.Settings.TVEpisodeNumber
            Select Case episodenumber
                Case "Leading Zero"
                    If CInt(mediadict("episode")) < 10 Then
                        episodenumber = "0" & mediadict("episode")
                    Else
                        episodenumber = mediadict("episode")
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
            If My.Settings.TVDescription = "Don't Show" Then
                tvdescription = ""
            Else
                tvdescription = RemoveIllegalFilenameChars(tvdescription)
            End If
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
            episodenumber = My.Settings.TVEpisodeNumber
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
                        If mediadict.ContainsKey("VideoResolution") AndAlso mediadict("VideoResolution") <> "" Then
                            completemoviename = completemoviename & " " & mediadict("VideoResolution")
                        End If
                    Case "Video Codec"
                        If mediadict.ContainsKey("VideoCodec") AndAlso mediadict("VideoCodec") <> "" Then
                            completemoviename = completemoviename & " " & mediadict("VideoCodec")
                        End If
                    Case "Audio Channels"
                        If mediadict.ContainsKey("AudioChannels") AndAlso mediadict("AudioChannels") <> "" Then
                            completemoviename = completemoviename & " " & mediadict("AudioChannels")
                        End If
                End Select
            Next
        End If

        Return completemoviename

    End Function
    Private Sub BuildMovieExample()
        lblExtras.Text = "Movie (" & CStr(DateTime.Now.Year) & ")"
        If My.Settings.MovieExtras IsNot Nothing Then

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
        If filechanges.Count = 0 Then
            MsgBox("There are no file renames, moves or copies to undo")
            Exit Sub
        End If
        Dim undochangesform As New UndoChanges
        AddHandler undochangesform.FormClosed, AddressOf undochangesform_Closed
        undochangesform.Show()

    End Sub

    Private Sub undochangesform_Closed(sender As Object, e As FormClosedEventArgs)
        If lblOriginalFolder.Text <> "" Then
            lbOriginal.Items.Clear()
            lbNew.Items.Clear()
            Call PopulateListBoxRecursively(lblOriginalFolder.Text, lbOriginal)
            Call cbSelectAll_CheckedChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        cancelprocess = True
    End Sub


    Private Sub pbLibrary_Click(sender As Object, e As EventArgs) Handles pbLibrary.Click
        isTMDB = Not isTMDB
        pbLibrary.Image = If(isTMDB, TMDBImage, TVDBImage)
    End Sub
End Class
