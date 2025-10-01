Imports System.IO

Public Class UndoChanges
    Private Sub UndoChanges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Please select the files (each row) to undo the change from New back to Original." & vbCrLf _
        & "For files that were moved, the New file will be moved back to the Original file." & vbCrLf _
        & "For files that were copied, the New file will copied back to the Original File if it does not exist and the New file will be deleted."

        For Each mychange In filechanges
            Dim item As New ListViewItem(mychange.sourcefile)
            item.SubItems.Add(mychange.fileoperation)
            item.SubItems.Add(mychange.destinationfile)
            ListView1.Items.Add(item)
        Next
        'This will select the first item in the list when the form loads
        'If ListView1.Items.Count > 0 Then
        'ListView1.Items(0).Selected = True
        'End If

    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click

        If ListView1.SelectedItems.Count > 0 Then
            For Each item As ListViewItem In ListView1.SelectedItems

                Dim origfile As String = item.Text
                Dim operation As String = item.SubItems(1).Text
                Dim destfile As String = item.SubItems(2).Text

                Select Case operation
                    Case "move"
                        If File.Exists(destfile) Then
                            My.Computer.FileSystem.MoveFile(destfile, origfile, FileIO.UIOption.AllDialogs)
                        End If

                    Case "copy"
                        If File.Exists(destfile) Then
                            If File.Exists(origfile) Then
                                File.Delete(destfile)
                            Else
                                My.Computer.FileSystem.CopyFile(destfile, origfile, FileIO.UIOption.AllDialogs)
                            End If
                        End If
                End Select

            Next

            If ListView1.SelectedIndices.Count > 0 Then
                For index As Integer = ListView1.SelectedIndices.Count - 1 To 0 Step -1
                    ListView1.Items.RemoveAt(ListView1.SelectedIndices(index))
                    filechanges.RemoveAt(index)
                Next
            End If
        Else
            MsgBox("Please select files to undo changes.")
        End If
        If ListView1.Items.Count = 0 Then
            btnUndo.Enabled = False
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub
End Class