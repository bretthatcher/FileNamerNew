

Public Class Duplicate
    Public Class MediaDetails
        Public Property title As String
        Public Property release_date As String
        Public Property overview As String
        Public Property poster_path As String
        Public Property id As Long
    End Class

    Private mediaarray As New List(Of MediaDetails)

    'Private _dupes As List(Of Movie)

    'Public Sub New(dupes As List(Of Movie))
    '    ' This call is required by the designer.
    '   InitializeComponent()
    '  _dupes = dupes
    ' Add any initialization after the InitializeComponent() call.
    'End Sub

    Private Sub Duplicate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If _dupes IsNot Nothing AndAlso _dupes.Count > 0 Then
        ' Populate the ListBox with movie titles
        'For Each dupe In _dupes
        'lbDuplicates.Items.Add(dupe.title & " (" & dupe.release_date & ")")
        'Next
        'End If
        Me.Text = "Choose the correct name for: " & mediadict("searchname").ToString & " (" & mediadict("searchyear").ToString & ")"

        Select Case mediatype
            Case "movie"
                If movies IsNot Nothing AndAlso movies.Count > 0 Then
                    ' Populate the ListBox with movie titles
                    For Each movie In movies
                        If mediadict("searchname").ToString.ToLower() = movie.title.ToLower() And mediadict("searchyear") = movie.release_date Then

                            lbDuplicates.Items.Add(movie.title & " (" & movie.release_date & ") (Exact Match)")

                        Else
                            lbDuplicates.Items.Add(movie.title & " (" & movie.release_date & ")")
                        End If

                        mediaarray.Add(New MediaDetails With {
                            .id = movie.id,
                            .overview = movie.overview,
                            .poster_path = movie.poster_path,
                            .release_date = movie.release_date,
                            .title = movie.title
                        })


                    Next
                End If
            Case "tvshow"
                If tvshows IsNot Nothing AndAlso tvshows.Count > 0 Then
                    For Each tvshow In tvshows
                        If mediadict("searchname").ToString.ToLower() = tvshow.title.ToLower() Then
                            lbDuplicates.Items.Add(tvshow.title & " (" & tvshow.release_date & ") (Exact Match)")
                        Else
                            lbDuplicates.Items.Add(tvshow.title & " (" & tvshow.release_date & ")")
                        End If
                        mediaarray.Add(New MediaDetails With {
                            .id = tvshow.id,
                            .overview = tvshow.overview,
                            .poster_path = tvshow.poster_path,
                            .release_date = tvshow.release_date,
                            .title = tvshow.title
                        })

                    Next
                End If
        End Select

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mediadict("title") = mediaarray(lbDuplicates.SelectedIndex).title
        mediadict("release_date") = mediaarray(lbDuplicates.SelectedIndex).release_date.Substring(0, 4)
        mediadict("id") = mediaarray(lbDuplicates.SelectedIndex).id
        PictureBox1.Image?.Dispose()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lbDuplicates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbDuplicates.SelectedIndexChanged
        'lbDuplicates.SelectedIndex
        lblDescription.Text = mediaarray(lbDuplicates.SelectedIndex).overview
        PictureBox1.Image?.Dispose()
        PictureBox1.Image = Nothing
        If mediaarray(lbDuplicates.SelectedIndex).poster_path <> "" Then
            Dim tempImagePath As String = Get_HTTP_Image("https://image.tmdb.org/t/p/w500" & mediaarray(lbDuplicates.SelectedIndex).poster_path)
            PictureBox1.Image = Image.FromFile(tempImagePath)
        End If

    End Sub
End Class