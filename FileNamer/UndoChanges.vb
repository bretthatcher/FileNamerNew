Imports System.IO

Public Class UndoChanges
    Private Sub UndoChanges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Please select the files (each row) to undo the change from New back to Original." & vbCrLf _
        & "For files that were moved, the New file will be moved back to the Original file." & vbCrLf _
        & "For files that were copied, the New file will be deleted."

        For Each mychange In filechanges
            Dim item As New ListViewItem(mychange.sourcefile)
            item.SubItems.Add(mychange.fileoperation)
            item.SubItems.Add(mychange.destinationfile)
            ListView1.Items.Add(item)
        Next
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        For Each item As ListViewItem In ListView1.SelectedItems

            Dim origfile As String = item.Text
            Dim operation As String = item.SubItems(1).Text
            Dim destfile As String = item.SubItems(2).Text

            Select Case operation
                Case "move"
                    My.Computer.FileSystem.MoveFile(destfile, origfile, True)
                Case "copy"
                    If File.Exists(destfile) Then
                        File.Delete(destfile)
                    End If
            End Select

        Next

        If ListView1.SelectedItems.Count > 0 Then
            For count As Integer = 0 To ListView1.Items.Count - 1
                If ListView1.Items(count).Selected Then
                    ListView1.Items.RemoveAt(count)
                    If count < filechanges.Count Then
                        filechanges.RemoveAt(count)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub
End Class