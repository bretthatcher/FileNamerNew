Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Results

    Public Property MyList As List(Of MyResults)

    Private Sub Results_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lvResults.View = View.Details
        lvResults.OwnerDraw = True

        If MyList IsNot Nothing Then
            For Each obj As MyResults In MyList
                Dim item As New ListViewItem(obj.Original)
                item.SubItems.Add(obj.NewName.ToString())
                lvResults.Items.Add(item)
            Next
        End If
    End Sub


    Private Sub lvResults_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles lvResults.DrawColumnHeader
        Dim headerFont As New Font("Arial", 12, FontStyle.Bold)
        'Custom background color
        e.Graphics.FillRectangle(Brushes.LightGreen, e.Bounds)
        'Custom Text color
        e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds)
    End Sub

    Private Sub lvResults_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles lvResults.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class

Public Class MyResults
    Public Property Original As String
    Public Property NewName As String
End Class