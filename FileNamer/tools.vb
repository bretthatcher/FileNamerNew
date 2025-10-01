Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
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

    Public Function ValidSubTitleExtension(filepath As String) As Boolean
        Dim validExtensions As String() = {".srt", ".smi", ".ssa", ".ass", ".vtt"}
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
            If My.Settings.IncludeSubtitleFiles = True Then
                If ValidSubTitleExtension(file) Then
                    listBox.Items.Add(file)
                End If
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
        ' Optional: Set the description shown in the dialog
        ' Optional: Set the initially selected folder
        Dim folderDialog As New FolderBrowserDialog With {
            .Description = "Please select a folder.",
            .SelectedPath = defaultfolder
        }
        ' Show the dialog and check if the user clicked OK
        If folderDialog.ShowDialog() = DialogResult.OK Then

            ' Retrieve the selected folder path
            Return folderDialog.SelectedPath

        Else
            Return ""
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

    Sub DeleteEmptyParentFolders(startFolder As String, stopFolder As String)
        Dim currentFolder As String = startFolder

        ' Normalize paths (remove trailing backslashes)
        stopFolder = stopFolder.TrimEnd(Path.DirectorySeparatorChar)

        While Not String.Equals(currentFolder.TrimEnd(Path.DirectorySeparatorChar), stopFolder, StringComparison.OrdinalIgnoreCase)
            If Directory.Exists(currentFolder) Then
                ' Delete if empty
                If Not Directory.EnumerateFileSystemEntries(currentFolder).Any() Then
                    Directory.Delete(currentFolder)
                Else
                    ' Stop if folder is not empty
                    Exit While
                End If
            End If

            ' Move up one folder
            currentFolder = Path.GetDirectoryName(currentFolder)

            ' Safety check
            If String.IsNullOrEmpty(currentFolder) Then Exit While
        End While
    End Sub

End Module

