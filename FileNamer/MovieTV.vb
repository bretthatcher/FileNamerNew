Imports System.IO
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Security.Policy
Imports System.Text
Imports System.Text.Json
Imports System.Text.RegularExpressions
Public Class Movie
    Public Property id As Long
    Public Property overview As String
    Public Property popularity As Double
    Public Property poster_path As String
    Public Property release_date As String
    Public Property title As String
    Public Property vote_average As Double
    Public Property vote_count As Long
End Class

Public Class TVShow
    Public Property id As Long
    Public Property overview As String
    Public Property popularity As Double
    Public Property poster_path As String
    Public Property release_date As String
    Public Property title As String
    Public Property vote_average As Double
    Public Property vote_count As Long
End Class

Public Class TVSeason
    Public Property id As Long
    Public Property air_date As String
    Public Property name As String
    Public Property overview As String
    Public Property season_number As Long
    Public Property episode_count As Long
    Public Property poster_path As String
    Public Property vote_average As Long
End Class

Public Class TVEpisode
    Public Property id As Long
    Public Property air_date As String
    Public Property name As String
    Public Property overview As String
    Public Property episode_number As Long
    Public Property season_number As Long
    Public Property still_path As String
    Public Property runtime As Long
    Public Property vote_average As Double
    Public Property vote_count As Long
End Class

Public Class FileChange
    Public Property sourcefile As String
    Public Property destinationfile As String
    Public Property fileoperation As String
End Class

Public Class AuthResponse
    Public Property Data As AuthData
End Class

Public Class AuthData
    Public Property Token As String
End Class


Module MovieTV

    Public jsonstring As String
    Public movies As New List(Of Movie)
    Public tvshows As New List(Of TVShow)
    Public tvseasons As New List(Of TVSeason)
    Public tvepisodes As New List(Of TVEpisode)

    Public mediatype As String
    Public mediaop As Integer
    Public medialibrary As String

    Public tvdict As New Dictionary(Of String, String)
    Public mediadict As New Dictionary(Of String, String)
    Public filechanges As New List(Of FileChange)

    Private client As HttpClient = New HttpClient()
    Private tmdb_apikey As String = "27a8df932b72b4ceb2fed7c4a3cec29d"
    Private tvdb_apikey As String = "f9c787a9-b8f7-47bf-9ae5-05d9f259aa34"
    Private tmdb_posterbaseurl As String = "https://image.tmdb.org/t/p/w500"


    Public Sub JSON_Parse(mediatype As String)
        Dim validJSON As Boolean = True
        If jsonstring <> "" Then
            Using document As JsonDocument = JsonDocument.Parse(jsonstring)

                'This is a check to see if the call to retrieve an exact match based off of ID was successful
                'This will fail if the ID is incorrect or if an invalid season or episode number is requested for the ID
                Dim successelement As JsonElement
                If document.RootElement.TryGetProperty("success", successelement) Then
                    Dim success As Boolean = successelement.GetBoolean
                    If success = False Then validJSON = False
                End If

                'This is a check to see if the search for a movie or tv show retruned any results
                Dim totalresultselement As JsonElement
                If document.RootElement.TryGetProperty("total_results", totalresultselement) Then
                    Dim totalresults As Integer = totalresultselement.GetInt32()
                    If totalresults = 0 Then
                        validJSON = False
                    Else
                        mediadict("totalresults") = totalresults
                    End If
                End If

                If validJSON Then

                    Select Case mediatype
                        Case "movie"
                            Dim results = document.RootElement.GetProperty("results")
                            movies.Clear()
                            For Each result In results.EnumerateArray()

                                'This code will add any of the properties requested in the result to the movie list that is returned
                                movies.Add(New Movie With {
                                        .id = result.GetProperty("id").GetInt32(),
                                        .overview = result.GetProperty("overview").GetString(),
                                        .popularity = result.GetProperty("popularity").GetDouble(),
                                        .poster_path = tmdb_posterbaseurl & result.GetProperty("poster_path").GetString(),
                                        .release_date = Left(result.GetProperty("release_date").GetString(), 4),
                                        .title = result.GetProperty("title").GetString(),
                                        .vote_average = result.GetProperty("vote_average").GetDouble(),
                                        .vote_count = result.GetProperty("vote_count").GetInt32()
                                    })

                            Next
                            For Each movie In movies
                                movie.title = RemoveIllegalFilenameChars(movie.title)
                            Next

                        'Use this when we are looking for a TV show
                        Case "tvshow"
                            Dim results = document.RootElement.GetProperty("results")
                            tvshows.Clear()
                            For Each result In results.EnumerateArray()
                                'This code will add any of the properties requested in the result to the tvshows list that is returned
                                tvshows.Add(New TVShow With {
                                        .id = result.GetProperty("id").GetInt32(),
                                        .overview = result.GetProperty("overview").GetString(),
                                        .popularity = result.GetProperty("popularity").GetDouble(),
                                        .poster_path = tmdb_posterbaseurl & result.GetProperty("poster_path").GetString(),
                                        .release_date = Left(result.GetProperty("first_air_date").GetString(), 4),
                                        .title = result.GetProperty("name").GetString(),
                                        .vote_average = result.GetProperty("vote_average").GetDouble(),
                                        .vote_count = result.GetProperty("vote_count").GetInt32()
                                    })
                            Next

                            For Each tvshow In tvshows
                                tvshow.title = RemoveIllegalFilenameChars(tvshow.title)
                            Next

                    'Use this to get details of a specific episode of a specific season of a specific show
                        Case "tvexactshow"
                            Dim result = document.RootElement
                            mediadict("episodename") = result.GetProperty("name").GetString()
                            mediadict("season") = result.GetProperty("season_number").GetInt32()
                            mediadict("episode") = result.GetProperty("episode_number").GetInt32()

                    'Use this when we don't know exactly which season we want
                        Case "tvseasons"
                            Dim results = document.RootElement.GetProperty("seasons")
                            Dim tvseasons As New List(Of TVSeason)
                            tvseasons.Clear()
                            For Each result In results.EnumerateArray()
                                'This code will add any of the properties requested in the result to the tvseasons list that is returned
                                tvseasons.Add(New TVSeason With {
                                        .id = result.GetProperty("id").GetInt32(),
                                        .air_date = result.GetProperty("air_date").GetString(),
                                        .name = result.GetProperty("name").GetString(),
                                        .overview = result.GetProperty("overview").GetString(),
                                        .season_number = result.GetProperty("season_number").GetInt32(),
                                        .episode_count = result.GetProperty("episode_count").GetInt32(),
                                        .poster_path = tmdb_posterbaseurl & result.GetProperty("poster_path").GetString(),
                                        .vote_average = result.GetProperty("vote_average").GetDouble()
                                    })
                            Next

                    'Use this when we don't know exactly which episode we want
                        Case "tvepisodes"
                            Dim results = document.RootElement.GetProperty("episodes")
                            tvepisodes.Clear()
                            For Each result In results.EnumerateArray()
                                'This code will add any of the properties requested in the result to the tvspisodes list that is returned
                                tvepisodes.Add(New TVEpisode With {
                                        .id = result.GetProperty("id").GetInt32(),
                                        .air_date = result.GetProperty("air_date").GetString(),
                                        .name = result.GetProperty("name").GetString(),
                                        .overview = result.GetProperty("overview").GetString(),
                                        .episode_number = result.GetProperty("episode_number").GetInt32(),
                                        .season_number = result.GetProperty("season_number").GetInt32(),
                                        .still_path = tmdb_posterbaseurl & result.GetProperty("still_path").GetString(),
                                        .runtime = result.GetProperty("runtime").GetInt32(),
                                        .vote_average = result.GetProperty("vote_average").GetDouble(),
                                        .vote_count = result.GetProperty("vote_count").GetInt32()
                                    })
                            Next
                    End Select

                Else
                    mediadict("totalresults") = 0
                End If

            End Using

        Else
            mediadict("totalresults") = 0
        End If
    End Sub

    Public Sub Search_Media(mediatype As String)

        If mediadict.ContainsKey("searchname") AndAlso Not String.IsNullOrEmpty(mediadict("searchname")) Then

            Select Case medialibrary
                Case "TMDB"
                    Call TMDB_Search_Media(mediatype)
                    Call JSON_Parse(mediatype)
                Case "TVDB"
                    Call TVDB_Search_Media(mediatype)
            End Select

        Else
            'Return ""
        End If

    End Sub

    Public Sub TVDB_Search_Media(mediatype As String)

        Select Case mediatype
            Case "movie"
                If TVDB_GetMovieName(mediadict("searchname")) Then
                    mediadict("totalresults") = movies.Count
                Else
                    mediadict("totalresults") = 0
                End If

            Case "tvshow"
                If mediadict.ContainsKey("searchname") AndAlso Not String.IsNullOrEmpty(mediadict("searchname")) Then

                    If TVDB_GetTVSeriesName(mediadict("searchname")) Then
                        mediadict("totalresults") = tvshows.Count
                    Else
                        mediadict("totalresults") = 0
                    End If

                End If

            Case "tvexactshow"
                If TVDB_GetTVEpisode(mediadict("id"), mediadict("searchseason"), mediadict("searchepisode")) Then
                    mediadict("totalresults") = 1
                Else
                    mediadict("totalresults") = 0
                End If
        End Select
    End Sub
    Public Function TVDB_GetMovieName(movieName As String) As Boolean

        client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", My.Settings.JWTToken)

        Dim propvalue As JsonElement

        Dim searchResponse = client.GetAsync($"https://api4.thetvdb.com/v4/search?query={Uri.EscapeDataString(movieName)}").Result
        Dim searchJson = searchResponse.Content.ReadAsStringAsync().Result
        Using document As JsonDocument = JsonDocument.Parse(searchJson)

            Dim results = document.RootElement.GetProperty("data")
            movies.Clear()
            For Each result In results.EnumerateArray()

                Dim myyear As String = ""
                Dim myoverview As String = ""
                Dim myposterpath As String = ""

                If result.TryGetProperty("year", propvalue) Then
                    myyear = Left(propvalue.GetString(), 4)
                Else
                    myyear = "NULL"
                End If

                If result.TryGetProperty("overview", propvalue) Then
                    myoverview = propvalue.GetString()
                End If

                If result.TryGetProperty("image_url", propvalue) Then
                    myposterpath = propvalue.GetString()
                End If

                'This code will add any of the properties requested in the result to the tvshows list that is returned
                movies.Add(New Movie With {
                                        .id = result.GetProperty("tvdb_id").GetString(),
                                        .overview = myoverview,
                                        .poster_path = myposterpath,
                                        .release_date = myyear,
                                        .title = result.GetProperty("name").GetString()
                                })

            Next

        End Using

        For Each movie In movies
            movie.title = RemoveIllegalFilenameChars(movie.title)
        Next

        If movies.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function TVDB_GetTVEpisode(tvshowid As String, showseason As Integer, showepisode As Integer) As Boolean

        client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", My.Settings.JWTToken)

        Dim page As Integer = 0

        Do
            Dim episodeResponse = client.GetAsync($"https://api4.thetvdb.com/v4/series/{tvshowid}/episodes/default?page={page}").Result
            Dim episodeJson = episodeResponse.Content.ReadAsStringAsync().Result
            Dim episodeDoc = JsonDocument.Parse(episodeJson)
            Dim dataObj = episodeDoc.RootElement.GetProperty("data")
            Dim episodes = dataObj.GetProperty("episodes")

            If episodes.ValueKind <> JsonValueKind.Array OrElse episodes.GetArrayLength() = 0 Then Exit Do

            For Each ep In episodes.EnumerateArray()
                Dim season = ep.GetProperty("seasonNumber").GetInt32()
                Dim number = ep.GetProperty("number").GetInt32()
                If season = showseason AndAlso number = showepisode Then
                    mediadict("episodename") = ep.GetProperty("name").GetString()
                    mediadict("season") = season
                    mediadict("episode") = number

                    mediadict("episodename") = RemoveIllegalFilenameChars(mediadict("episodename"))

                    Return True
                End If
            Next

            page += 1
        Loop

        Return False

    End Function
    Public Function TVDB_GetTVSeriesName(seriesName As String) As Boolean

        client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", My.Settings.JWTToken)

        Dim propvalue As JsonElement

        ' Step 2: Search for series
        Dim searchResponse = client.GetAsync($"https://api4.thetvdb.com/v4/search?query={Uri.EscapeDataString(seriesName)}&type=series").Result
        Dim searchJson = searchResponse.Content.ReadAsStringAsync().Result
        Using document As JsonDocument = JsonDocument.Parse(searchJson)

            Dim results = document.RootElement.GetProperty("data")
            tvshows.Clear()
            For Each result In results.EnumerateArray()

                Dim myyear As String = ""
                Dim myoverview As String = ""
                Dim myposterpath As String = ""
                Dim myname As String = ""

                If result.TryGetProperty("year", propvalue) Then
                    myyear = Left(propvalue.GetString(), 4)
                Else
                    myyear = "NULL"
                End If

                If result.TryGetProperty("overview", propvalue) Then
                    myoverview = propvalue.GetString()
                End If

                If result.TryGetProperty("image_url", propvalue) Then
                    myposterpath = propvalue.GetString()
                End If

                If result.TryGetProperty("name", propvalue) Then
                    myname = propvalue.GetString()
                    myname = System.Text.RegularExpressions.Regex.Replace(myname, "\s*\(\d{4}\)", "")
                End If

                'This code will add any of the properties requested in the result to the tvshows list that is returned
                tvshows.Add(New TVShow With {
                                        .id = result.GetProperty("tvdb_id").GetString(),
                                        .overview = myoverview,
                                        .poster_path = myposterpath,
                                        .release_date = myyear,
                                        .title = myname
                                })

            Next

        End Using

        For Each tvshow In tvshows
            tvshow.title = RemoveIllegalFilenameChars(tvshow.title)
        Next

        If tvshows.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
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


            Select Case mediatype
                Case "movie"

                    'add the year if it exists for movies
                    If mediadict.ContainsKey("searchyear") AndAlso Not String.IsNullOrEmpty("searchyear") Then
                        searchterm = searchterm & "&year=" & mediadict("searchyear")
                    End If

                    Return "https://api.themoviedb.org/3/search/movie?api_key=" & tmdb_apikey & searchterm

                Case "tvshow"

                    Return "https://api.themoviedb.org/3/search/tv?api_key=" & tmdb_apikey & searchterm

                Case "tvexactshow"

                    Return "https://api.themoviedb.org/3/tv/" & mediadict("id") & "/season/" & mediadict("searchseason") & "/episode/" & mediadict("searchepisode") & "?api_key=" & tmdb_apikey


            End Select
        Else
            Return ""
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

        If mediadict("AudioChannels") = "CH" Then
            mediadict("AudioChannels") = ""
        End If

        'If the VideoCodec contains XviD then set the VideoCodec to XVID
        If mediadict.ContainsKey("VideoCodec") = True Then
            If InStr(1, mediadict("VideoCodec"), "XviD") > 0 Then
                mediadict("VideoCodec") = "XVID"
            End If
        End If

        'Set the VidResolution based off of the Width and Height of the video stream if available
        'If (mediadict.ContainsKey("VidWidth") AndAlso mediadict("VidWidth") > 0) And (mediadict.ContainsKey("VidHeight") AndAlso mediadict("VidHeight") > 0) Then
        If (mediadict.ContainsKey("VidWidth") AndAlso Not String.IsNullOrEmpty(mediadict("VidWidth"))) And (mediadict.ContainsKey("VidHeight") AndAlso Not String.IsNullOrEmpty(mediadict("VidHeight"))) Then
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
    Private Function Base64UrlDecode(input As String) As Byte()
        Dim base64 = input.Replace("-", "+").Replace("_", "/")
        Select Case base64.Length Mod 4
            Case 2 : base64 &= "=="
            Case 3 : base64 &= "="
        End Select
        Return Convert.FromBase64String(base64)
    End Function

    ' Get the expiration time from JWT
    Public Function GetJwtExpiration(jwtToken As String) As DateTime?
        Try
            Dim parts() As String = jwtToken.Split("."c)
            If parts.Length <> 3 Then
                Throw New ArgumentException("Invalid JWT token format.")
            End If

            ' JWT payload is the second part
            Dim payloadBytes() As Byte = Base64UrlDecode(parts(1))
            Dim payloadJson As String = Encoding.UTF8.GetString(payloadBytes)

            ' Parse JSON payload
            Dim payloadDoc As JsonDocument = JsonDocument.Parse(payloadJson)
            Dim expProp As JsonElement ' Declare the variable here

            If payloadDoc.RootElement.TryGetProperty("exp", expProp) Then
                ' 'exp' is in Unix time seconds
                Dim expUnix As Long = expProp.GetInt64()
                Dim expDate As DateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime
                Return expDate
            Else
                Return Nothing ' 'exp' claim not found
            End If

        Catch ex As Exception
            Console.WriteLine("Error decoding JWT: " & ex.Message)
            Return Nothing
        End Try
    End Function

    ' Check if a JWT token is expired
    Public Function IsJwtExpired(jwtToken As String) As Boolean
        Dim exp As DateTime? = GetJwtExpiration(jwtToken)
        If exp.HasValue Then
            Return DateTime.UtcNow > exp.Value
        Else
            ' If no expiration found, consider it invalid/expired depending on your logic
            Return True
        End If
    End Function
    Public Function GetJWTToken() As String
        Try
            Dim authPayload = New With {.apikey = tvdb_apikey}
            Dim authJson = JsonSerializer.Serialize(authPayload)
            Dim authContent = New StringContent(authJson, Encoding.UTF8, "application/json")
            Dim authResponse = client.PostAsync("https://api4.thetvdb.com/v4/login", authContent).Result
            Dim authResult = authResponse.Content.ReadAsStringAsync().Result

            Dim options As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}
            Dim authObj = JsonSerializer.Deserialize(Of AuthResponse)(authResult, options)
            Return authObj.Data.Token
        Catch ex As Exception
            Return "Unable to get token"
        End Try

    End Function
End Module
