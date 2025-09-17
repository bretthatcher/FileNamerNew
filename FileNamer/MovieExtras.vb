Public Class MovieExtras
    Private Sub MovieExtras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add some sample options
        ListBox1.Items.AddRange(New String() {"Video Resolution", "Video Codec", "Audio Channels"})
        If My.Settings.MovieExtras IsNot Nothing Then
            For Each item As String In My.Settings.MovieExtras
                ListBox2.Items.Add(item)
            Next
        Else
            My.Settings.MovieExtras = New System.Collections.Specialized.StringCollection()
        End If
        ' Enable drag-and-drop
        ListBox1.AllowDrop = False   ' Source only
        ListBox2.AllowDrop = True    ' Target & reorder

        ' Show empty result
        UpdateResultLabel()
    End Sub
    ' ---------------------------
    ' BUTTONS
    ' ---------------------------

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        AddItemToListBox2(ListBox1.SelectedItem)
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If ListBox2.SelectedItem IsNot Nothing Then
            ListBox2.Items.Remove(ListBox2.SelectedItem)
            UpdateResultLabel()
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        If ListBox2.SelectedIndex > 0 Then
            Dim idx = ListBox2.SelectedIndex
            Dim item = ListBox2.SelectedItem
            ListBox2.Items.RemoveAt(idx)
            ListBox2.Items.Insert(idx - 1, item)
            ListBox2.SelectedIndex = idx - 1
            UpdateResultLabel()
        End If
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        If ListBox2.SelectedIndex >= 0 AndAlso ListBox2.SelectedIndex < ListBox2.Items.Count - 1 Then
            Dim idx = ListBox2.SelectedIndex
            Dim item = ListBox2.SelectedItem
            ListBox2.Items.RemoveAt(idx)
            ListBox2.Items.Insert(idx + 1, item)
            ListBox2.SelectedIndex = idx + 1
            UpdateResultLabel()
        End If
    End Sub

    ' ---------------------------
    ' DRAG-AND-DROP
    ' ---------------------------

    ' Drag from ListBox1 → ListBox2 (add)
    Private Sub ListBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDown
        If ListBox1.SelectedItem IsNot Nothing Then
            ListBox1.DoDragDrop(ListBox1.SelectedItem, DragDropEffects.Copy)
        End If

        ' Double-click handling
        If e.Button = MouseButtons.Left AndAlso e.Clicks = 2 Then
            Dim index As Integer = ListBox1.IndexFromPoint(e.Location)
            If index >= 0 Then AddItemToListBox2(ListBox1.Items(index))
        End If
    End Sub

    ' Drag within ListBox2 → reorder
    Private Sub ListBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDown
        If ListBox2.SelectedItem IsNot Nothing Then
            ListBox2.DoDragDrop(ListBox2.SelectedItem, DragDropEffects.Move)
        End If

        ' Double-click to remove
        If e.Button = MouseButtons.Left AndAlso e.Clicks = 2 Then
            Dim index As Integer = ListBox2.IndexFromPoint(e.Location)
            If index >= 0 Then
                ListBox2.Items.RemoveAt(index)
                UpdateResultLabel()
            End If
        End If
    End Sub

    Private Sub ListBox2_DragOver(sender As Object, e As DragEventArgs) Handles ListBox2.DragOver
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub ListBox2_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox2.DragDrop
        Dim point As Point = ListBox2.PointToClient(New Point(e.X, e.Y))
        Dim index As Integer = ListBox2.IndexFromPoint(point)
        If index < 0 Then index = ListBox2.Items.Count

        Dim data As String = TryCast(e.Data.GetData(GetType(String)), String)
        If data Is Nothing Then Exit Sub

        If e.Effect = DragDropEffects.Copy Then
            AddItemToListBox2(data, index)
        ElseIf e.Effect = DragDropEffects.Move Then
            Dim oldIndex As Integer = ListBox2.Items.IndexOf(data)
            If oldIndex <> index Then
                ListBox2.Items.RemoveAt(oldIndex)
                If oldIndex < index Then index -= 1
                ListBox2.Items.Insert(index, data)
                ListBox2.SelectedIndex = index
                UpdateResultLabel()
            End If
        End If
    End Sub

    ' ---------------------------
    ' DELETE KEY
    ' ---------------------------

    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        If e.KeyCode = Keys.Delete AndAlso ListBox2.SelectedItem IsNot Nothing Then
            ListBox2.Items.Remove(ListBox2.SelectedItem)
            UpdateResultLabel()
        End If
    End Sub

    ' ---------------------------
    ' HELPER METHODS
    ' ---------------------------

    Private Sub AddItemToListBox2(item As Object, Optional insertIndex As Integer = -1)
        If item Is Nothing Then Exit Sub
        Dim value As String = item.ToString()

        ' Prevent duplicates
        If ListBox2.Items.Contains(value) Then Exit Sub

        If insertIndex < 0 OrElse insertIndex > ListBox2.Items.Count Then
            ListBox2.Items.Add(value)
            ListBox2.SelectedIndex = ListBox2.Items.Count - 1
        Else
            ListBox2.Items.Insert(insertIndex, value)
            ListBox2.SelectedIndex = insertIndex
        End If

        UpdateResultLabel()
    End Sub

    Private Sub UpdateResultLabel()
        lblResult.Text = "Movie (2025)"
        For count As Integer = 0 To ListBox2.Items.Count - 1
            Select Case ListBox2.Items(count).ToString
                Case "Video Resolution"
                    lblResult.Text = lblResult.Text & " 1080p"
                Case "Video Codec"
                    lblResult.Text = lblResult.Text & " HVEC"
                Case "Audio Channels"
                    lblResult.Text = lblResult.Text & " 6CH"
            End Select
        Next
        'lblResult.Text = "Your selection: " & String.Join(" ", ListBox2.Items.Cast(Of String)())
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.MovieExtras.Clear()

        For count As Integer = 0 To ListBox2.Items.Count - 1
            My.Settings.MovieExtras.Add(ListBox2.Items(count).ToString)
        Next
        Me.Close()

    End Sub
End Class