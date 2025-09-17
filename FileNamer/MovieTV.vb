Imports System.IO
Imports System.Net.Http
Imports System.Security.Policy
Imports System.Text.RegularExpressions

Public Class FileChange
    Public Property sourcefile As String
    Public Property destinationfile As String
    Public Property fileoperation As String
End Class

Module MovieTV
    Public mediatype As String
    Public mediaop As Integer
    Public tvdict As New Dictionary(Of String, String)
    Public mediadict As New Dictionary(Of String, String)
    Public filechanges As New List(Of FileChange)

    Private apikey As String = "27a8df932b72b4ceb2fed7c4a3cec29d"

    Public Sub TMDB_Search_Media(mediatype As String)
        Dim media_url As String = Build_TMDB_URL(mediatype)

        Task.Run(Function() GetMedia_JSON_Async(media_url)).Wait()

    End Sub

    Async Function GetMedia_JSON_Async(myurl As String) As Task
        'Dim baseUrl As String = "https://api.themoviedb.org/3/movie/"

        jsonstring = ""

        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(myurl)
                response.EnsureSuccessStatusCode()

                jsonstring = Await response.Content.ReadAsStringAsync()

            Catch ex As Exception
                'Console.WriteLine($"Error: {ex.Message}")
            End Try


        End Using



    End Function
    Public Sub ValidTVSeasonEpisode()
        Call TMDB_Search_Media(mediatype)
        Call JSON_Parse(mediatype)
    End Sub
    Public Function Build_TMDB_URL(mediatype As String) As String

        'Replace spaces with %20 for URL encoding

        'Dim searchterm As String = Replace(mediadict("searchname"), " ", "%20")
        If mediadict.ContainsKey("searchname") AndAlso Not String.IsNullOrEmpty("searchname") Then
            'build the searchterm for the URL
            'add the search name
            Dim searchterm As String = "&query=" & Replace(mediadict("searchname"), " ", "%20")

            'add the year if it exists
            If mediadict.ContainsKey("searchyear") AndAlso Not String.IsNullOrEmpty("searchyear") Then
                searchterm = searchterm & "&year=" & mediadict("searchyear")
            End If

            Select Case mediatype
                Case "movie"

                    Return "https://api.themoviedb.org/3/search/movie?api_key=" & apikey & searchterm

                Case "tvshow"

                    Return "https://api.themoviedb.org/3/search/tv?api_key=" & apikey & searchterm

                Case "tvexactshow"

                    Return "https://api.themoviedb.org/3/tv/" & mediadict("id") & "/season/" & mediadict("searchseason") & "/episode/" & mediadict("searchepisode") & "?api_key=" & apikey


            End Select
        End If
    End Function
    Public Function goodfile() As Boolean

        If mediatype = "tvshow" Then
            If mediadict.ContainsKey("searchseason") And mediadict.ContainsKey("searchepisode") Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If

    End Function
    Public Function duplicatetv() As Boolean
        If tvdict("searchname") = mediadict("searchname") Then
            Return True
        Else
            Return vbFalse
        End If
    End Function
    Public Sub Parse_Name(filename As String, mediatype As String)
        'This function will parse the filename to extract the title and year

        mediadict("searchname") = ""
        mediadict("searchyear") = ""

        Select Case mediatype

            Case "movie"

                Dim minYear As Integer = 1888
                Dim maxYear As Integer = DateTime.Now.Year + 1

                Dim allYearMatches As MatchCollection = Regex.Matches(filename, "\b\d{4}\b")

                Dim plausibleYears As New List(Of Match)()
                For Each m As Match In allYearMatches
                    Dim y As Integer
                    If Integer.TryParse(m.Value, y) Then
                        If y >= minYear AndAlso y <= maxYear Then
                            plausibleYears.Add(m)
                        End If
                    End If
                Next

                Dim releaseYear As String = ""
                Dim titlePart As String = filename

                If plausibleYears.Count >= 2 Then
                    ' Multiple years: last one is release year
                    mediadict("searchyear") = plausibleYears(plausibleYears.Count - 1).Value
                    Dim first = plausibleYears(0)
                    Dim second = plausibleYears(1)
                    If first.Index = 0 And second.Index < 7 Then
                        titlePart = first.Value ' title is a year (e.g. "1984.1984")
                    Else
                        titlePart = filename.Substring(0, second.Index)
                    End If

                ElseIf plausibleYears.Count = 1 Then
                    Dim first = plausibleYears(0)
                    If first.Index = 0 AndAlso filename.Trim() = first.Value Then
                        ' Only one token and it’s the whole name → title only
                        titlePart = first.Value
                    ElseIf first.Index = 0 Then
                        ' Starts with year but more content → title is that year, release year = ""
                        titlePart = first.Value
                    Else
                        ' Year later in filename → it's the release year
                        mediadict("searchyear") = first.Value
                        titlePart = filename.Substring(0, first.Index)
                    End If
                Else
                    ' No years at all
                    titlePart = filename
                End If

                ' Cleanup: remove dangling symbols
                titlePart = Regex.Replace(titlePart, "[\(\[\{]+$", "")
                'titlePart = Regex.Replace(titlePart, "^[\s._\-]+|[\s._\-]+$", "")
                titlePart = Regex.Replace(titlePart, "^[\s_]+|[\s_]+$", "")
                mediadict("searchname") = Regex.Replace(titlePart, "[\s._]+", " ").Trim()
                mediadict("searchname") = RemoveIllegalFilenameChars(mediadict("searchname"))


            Case "tvshow"
                'For TV episodes we will try to extract the show name, year season and episode number

                Dim pattern As String = "^(?<show>.+?)(?:[\s._-]*\(?(?<year>(?:19|20)\d{2})\)?)?[\s._-]*(?:S?(?<season>\d{1,2})[\s._-]*E(?<episode>\d{1,2})|Season[\s._-]*(?<season2>\d{1,2})[\s._-]*Episode[\s._-]*(?<episode2>\d{1,2})|(?<season3>\d{1,2})x(?<episode3>\d{1,2}))"

                Dim match As Match = Regex.Match(filename, pattern, RegexOptions.IgnoreCase)

                If match.Success Then

                    mediadict("searchname") = Regex.Replace(match.Groups("show").Value, "[\s._-]+", " ").Trim()
                    mediadict("searchname") = RemoveIllegalFilenameChars(mediadict("searchname"))

                    'tvdict("currentsearchname") = mediadict("searchname")

                    mediadict("searchyear") = If(match.Groups("year").Success, match.Groups("year").Value, "")

                    ' Handle special case: show name is a year
                    If mediadict("searchyear") = "" AndAlso Regex.IsMatch(mediadict("searchname"), "^\d{4}$") Then
                        ' Treat it as show name, not release year
                        mediadict("searchyear") = ""
                    End If
                    ' pick the right capture group
                    mediadict("searchseason") = If(match.Groups("season").Success, match.Groups("season").Value,
                                                If(match.Groups("season2").Success, match.Groups("season2").Value,
                                                If(match.Groups("season3").Success, match.Groups("season3").Value, "")))

                    mediadict("searchepisode") = If(match.Groups("episode").Success, match.Groups("episode").Value,
                                                If(match.Groups("episode2").Success, match.Groups("episode2").Value,
                                                If(match.Groups("episode3").Success, match.Groups("episode3").Value, "")))

                End If
        End Select

    End Sub
    Public Sub GetMediaInfo(filepath As String)
        Dim mywidth As Long
        Dim myheight As Long
        Dim MI As MediaInfo
        MI = New MediaInfo

        MI.Open(filepath)

        mediadict.Add("VidWidth", MI.Get_(StreamKind.Visual, 0, "Width"))
        mediadict.Add("VidHeight", MI.Get_(StreamKind.Visual, 0, "Height"))
        mediadict.Add("VideoCodec", MI.Get_(StreamKind.Visual, 0, "Format"))
        mediadict.Add("AudioChannels", MI.Get_(StreamKind.Audio, 0, "Channel(s)") + "CH")

        MI.Close()

        'If the VideoCodec contains XviD then set the VideoCodec to XVID
        If mediadict.ContainsKey("VideoCodec") = True Then
            If InStr(1, mediadict("VideoCodec"), "XviD") > 0 Then
                mediadict("VideoCodec") = "XVID"
            End If
        End If

        'Set the VidResolution based off of the Width and Height of the video stream
        If (mediadict.ContainsKey("VidWidth") AndAlso mediadict("VidWidth") > 0) And (mediadict.ContainsKey("VidHeight") AndAlso mediadict("VidHeight") > 0) Then

            mywidth = Int(mediadict("VidWidth"))
            myheight = Int(mediadict("VidHeight"))

            Select Case True

           'Set to 4K
                Case myheight = 2160
                    mediadict("VideoResolution") = "2160p"

           'Set to 1080p
                Case (myheight > 720 And myheight <= 1080)
                    mediadict("VideoResolution") = "1080p"

           'Set to 720p
                Case (myheight > 480 And myheight <= 720)
                    mediadict("VideoResolution") = "720p"

           'Set to 480p
                Case myheight = 480
                    mediadict("VideoResolution") = "480p"

           'Set to SD if less than 480
                Case myheight < 480
                    mediadict("VideoResolution") = "SD"

            End Select

        End If

    End Sub
    Public Sub GetMediaName(filename As String)


    End Sub
    Private Sub GetMediaInfo_Test()
        Dim To_Display As String
        Dim Temp As String
        Dim MI As MediaInfo

        MI = New MediaInfo
        To_Display = MI.Option_("Info_Version", "0.7.0.0;MediaInfoDLL_Example_MSVB;0.7.0.0")

        If (To_Display.Length() = 0) Then
            MsgBox("MediaInfo.Dll: this version of the DLL is not compatible")
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

        'To_Display += RichTextBox1.Text + vbCrLf + vbCrLf + "Close" + vbCrLf
        MI.Close()

    End Sub
End Module
