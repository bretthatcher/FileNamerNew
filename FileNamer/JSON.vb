Imports System.Net.Http
Imports System.Text.Json

Imports System.Threading.Tasks



Module JSON
    Public Class Movie
        Public Property adult As Boolean
        Public Property backdrop_path As String
        Public Property genre_ids As List(Of Integer)
        Public Property id As Long
        Public Property original_language As String
        Public Property original_title As String
        Public Property overview As String
        Public Property popularity As Double
        Public Property poster_path As String
        Public Property release_date As String
        Public Property title As String
        Public Property video As Boolean
        Public Property vote_average As Double
        Public Property vote_count As Long
    End Class

    Public Class MovieResponse
        Public Property results As List(Of Movie)
    End Class
    Public Sub ParseMovie()
        'Dim url As String = "https://api.themoviedb.org/3/search/movie?api_key=27a8df932b72b4ceb2fed7c4a3cec29d&query=john%20wick&year=2014"
        Dim url As String = "https://api.themoviedb.org/3/search/movie?api_key=27a8df932b72b4ceb2fed7c4a3cec29d&query=john%20wick"
        Task.Run(Function() GetMovieDataAsync(url)).Wait()
        'Dim jsonstring As String = Await GetMovieDataAsync(url)
        'Debug.Print jsontring

        'Dim movie As Movie = JsonSerializer.Deserialize(Of Movie)(response)
        'Debug.Print({movie.Title})

    End Sub

    Async Function GetMovieDataAsync(myurl As String) As Task
        'Dim baseUrl As String = "https://api.themoviedb.org/3/movie/"



        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(myurl)
                response.EnsureSuccessStatusCode()
                Dim jsonString As String = Await response.Content.ReadAsStringAsync()

                'this works if you have the movie class properties exactly aligned with the data you are retrieving
                'Dim myMovieResponse As MovieResponse = JsonSerializer.Deserialize(Of MovieResponse)(jsonString)

                'lets try this method so we only grab what we need from the jsonstring
                Using document As JsonDocument = JsonDocument.Parse(jsonString)
                    ' Navigate to the "users" array
                    Dim results = document.RootElement.GetProperty("results")

                    ' Loop through each user in the array
                    For Each result In results.EnumerateArray()
                        ' Extract only the "id" and "name" properties
                        Dim title As String = result.GetProperty("title").GetString()
                        Dim id As Long = result.GetProperty("id").GetInt32()
                        Dim overview As String = result.GetProperty("overview").GetString()
                        Dim poster_path As String = result.GetProperty("poster_path").GetString()
                        Dim release_date As String = result.GetProperty("release_date").GetString()
                        Debug.Print("Title: " & title)
                        Debug.Print("ID: " & id)
                        Debug.Print("Overview: " & overview)
                        Debug.Print("Poster Path: " & poster_path)
                        Debug.Print("Release Date: " & release_date)
                    Next
                End Using
                Debug.Print(jsonString)


            Catch ex As Exception
                'Console.WriteLine($"Error: {ex.Message}")
            End Try


        End Using



    End Function
End Module
