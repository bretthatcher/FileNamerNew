Imports System.Net
Imports System.IO
Imports System.Windows.Forms

Module tools
    Public Function RemoveIllegalFilenameChars(filename As String) As String
        Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
        For Each c As Char In invalidChars
            filename = filename.Replace(c, "") ' Replace invalid character with an underscore
        Next
        Return filename
    End Function

    Public Function ValidExtension(filepath As String) As Boolean
        Dim validExtensions As String() = {".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv", ".mpg", ".mpeg", ".m4v"}
        Dim fileExtension As String = Path.GetExtension(filepath).ToLower()
        Return validExtensions.Contains(fileExtension)
    End Function

    Public Sub PopulateListBoxRecursively(folderPath As String, listBox As ListBox)
        ' Add all files in the current folder
        Dim files As String() = Directory.GetFiles(folderPath)
        For Each file As String In files
            If ValidExtension(file) Then
                listBox.Items.Add(file)
            End If
        Next

        ' Recursively add all subfolders
        Dim folders As String() = Directory.GetDirectories(folderPath)
        For Each folder As String In folders
            ' Recursive call for the subfolder
            PopulateListBoxRecursively(folder, listBox)
        Next
    End Sub
    Public Function ShowFolderChooser(defaultfolder As String) As String
        ' Create a new instance of FolderBrowserDialog
        Dim folderDialog As New FolderBrowserDialog()

        ' Optional: Set the description shown in the dialog
        folderDialog.Description = "Please select a folder."

        ' Optional: Set the root folder (e.g., Desktop, MyComputer, etc.)
        'folderDialog.RootFolder = Environment.SpecialFolder.Desktop

        ' Optional: Set the initially selected folder
        folderDialog.SelectedPath = defaultfolder

        ' Show the dialog and check if the user clicked OK
        If folderDialog.ShowDialog() = DialogResult.OK Then
            ' Retrieve the selected folder path
            'Dim selectedFolder As String = folderDialog.SelectedPath
            Return folderDialog.SelectedPath
            'MessageBox.Show("You selected: " & selectedFolder)
        End If
    End Function

    Public Function Get_HTTP_Image(picurl As String) As String
        Dim temppic As String = Path.GetTempPath & "temppic.jpg"

        'If File.Exists(temppic) Then File.Delete(temppic)

        Using client As New WebClient()
            client.DownloadFile(picurl, temppic)
        End Using
        Return temppic
    End Function
End Module

