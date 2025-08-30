Imports System.IO
Imports System.Windows.Forms

Module tools
    Public mediadict As New Dictionary(Of String, String)

    Public Sub PopulateListBoxRecursively(folderPath As String, listBox As ListBox)
        ' Add all files in the current folder
        Dim files As String() = Directory.GetFiles(folderPath)
        For Each file As String In files
            listBox.Items.Add(file)
        Next

        ' Recursively add all subfolders
        Dim folders As String() = Directory.GetDirectories(folderPath)
        For Each folder As String In folders
            'listBox.Items.Add(folder)
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


End Module

