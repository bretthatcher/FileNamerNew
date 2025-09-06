Imports System.Net.Http
Imports System.Text.Json
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports FileNamer.JSON

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

Module JSON
    Public jsonstring As String
    Public movies As New List(Of Movie)
    Public tvshows As New List(Of TVShow)
    Public tvseasons As New List(Of TVSeason)
    Public tvepisodes As New List(Of TVEpisode)



    Public Sub JSON_Parse(mediatype As String)
        Dim validJSON As Boolean = True

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
                                        .poster_path = result.GetProperty("poster_path").GetString(),
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
                                        .poster_path = result.GetProperty("poster_path").GetString(),
                                        .release_date = Left(result.GetProperty("first_air_date").GetString(), 4),
                                        .title = result.GetProperty("name").GetString(),
                                        .vote_average = result.GetProperty("vote_average").GetDouble(),
                                        .vote_count = result.GetProperty("vote_count").GetInt32()
                                    })
                        Next

                        For Each tvshow In tvshows
                            tvshow.title = RemoveIllegalFileNameChars(tvshow.title)
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
                                        .poster_path = result.GetProperty("poster_path").GetString(),
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
                                        .still_path = result.GetProperty("still_path").GetString(),
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

    End Sub
    Public Sub Test_Parse_Name()
        Dim filenames As String() = {
            "1883 S01E01 1080p WEB H264-MEMENTO",
            "The.Office.2005.S02E14.HDTV.mkv",
            "The Office S02E14.mkv",
            "1984.S01E01.mkv",
            "1984.2019.S01E01.mkv",
            "The_Office_(2005)_Season 2 Episode 14.avi",
            "The Office 1x01 Pilot.mp4"
        }
        For Each filename In filenames
            Call Parse_Name(filename, "tv episode")
        Next
    End Sub



End Module
