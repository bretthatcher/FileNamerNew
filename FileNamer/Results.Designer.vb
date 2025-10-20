<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Results
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvResults = New System.Windows.Forms.ListView()
        Me.Original = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NewItem = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvResults
        '
        Me.lvResults.BackColor = System.Drawing.Color.LightGreen
        Me.lvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Original, Me.NewItem})
        Me.lvResults.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvResults.FullRowSelect = True
        Me.lvResults.HideSelection = False
        Me.lvResults.Location = New System.Drawing.Point(12, 12)
        Me.lvResults.Name = "lvResults"
        Me.lvResults.Size = New System.Drawing.Size(1069, 496)
        Me.lvResults.TabIndex = 0
        Me.lvResults.UseCompatibleStateImageBehavior = False
        Me.lvResults.View = System.Windows.Forms.View.Details
        '
        'Original
        '
        Me.Original.Text = "Original"
        Me.Original.Width = 533
        '
        'NewItem
        '
        Me.NewItem.Text = "New"
        Me.NewItem.Width = 530
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(1009, 534)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(72, 28)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Results
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1092, 578)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lvResults)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Results"
        Me.Text = "Results"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvResults As ListView
    Friend WithEvents Original As ColumnHeader
    Friend WithEvents NewItem As ColumnHeader
    Friend WithEvents btnExit As Button
End Class
