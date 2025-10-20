Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.Json

Public Class AuthResponse
    Public Property Data As AuthData
End Class

Public Class AuthData
    Public Property Token As String
End Class

Module TVDB
    Private client As HttpClient = New HttpClient()
    Private jwtToken As String = ""

    Public Function GetJWTToken(apiKey As String) As String
        ' Step 1: Authenticate
        Try
            Dim authPayload = New With {.apikey = apiKey}
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

    'This function accepts the name of a TV series and returns True if any matches are found otherwise False is returned
    Public Function GetTVSeriesName(seriesName As String) As Boolean

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

                If result.TryGetProperty("year", propvalue) Then
                    myyear = Left(propvalue.GetString(), 4)
                End If

                'This code will add any of the properties requested in the result to the tvshows list that is returned
                tvshows.Add(New TVShow With {
                                        .id = result.GetProperty("tvdb_id").GetString(),
                                        .overview = result.GetProperty("overview").GetString(),
                                        .poster_path = result.GetProperty("image_url").GetString(),
                                        .release_date = myyear,
                                        .title = result.GetProperty("name").GetString()
                                })

            Next

        End Using

        If tvshows.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    'This function looks for a specific season and episode number for a TV series and returns True if found otherwise False
    Public Function GetTVEpisode() As Boolean
        client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", My.Settings.JWTToken)

        Dim page As Integer = 0

        Do
            Dim episodeResponse = client.GetAsync($"https://api4.thetvdb.com/v4/series/{mediadict("id")}/episodes/default?page={page}").Result
            Dim episodeJson = episodeResponse.Content.ReadAsStringAsync().Result
            Dim episodeDoc = JsonDocument.Parse(episodeJson)
            Dim dataObj = episodeDoc.RootElement.GetProperty("data")
            Dim episodes = dataObj.GetProperty("episodes")

            If episodes.ValueKind <> JsonValueKind.Array OrElse episodes.GetArrayLength() = 0 Then Exit Do

            For Each ep In episodes.EnumerateArray()
                Dim season = ep.GetProperty("seasonNumber").GetInt32()
                Dim number = ep.GetProperty("number").GetInt32()
                If season = mediadict("searchseason") AndAlso number = mediadict("searchepisode") Then
                    mediadict("episodename") = ep.GetProperty("name").GetString()
                    mediadict("season") = season
                    mediadict("episode") = number
                    Return True
                End If
            Next

            page += 1
        Loop

        Return False

    End Function


    ' Decode a base64url-encoded string
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



End Module
