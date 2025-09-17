<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UndoChanges
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
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Original = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Operation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NewFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnUndo = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Original, Me.Operation, Me.NewFile})
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(12, 89)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(980, 417)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Original
        '
        Me.Original.Text = "Original"
        Me.Original.Width = 451
        '
        'Operation
        '
        Me.Operation.Text = "Operation"
        '
        'NewFile
        '
        Me.NewFile.Text = "New"
        Me.NewFile.Width = 463
        '
        'btnUndo
        '
        Me.btnUndo.Location = New System.Drawing.Point(441, 512)
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Size = New System.Drawing.Size(144, 37)
        Me.btnUndo.TabIndex = 1
        Me.btnUndo.Text = "Undo Changes"
        Me.btnUndo.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(848, 543)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(144, 37)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 3
        '
        'UndoChanges
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 592)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnUndo)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "UndoChanges"
        Me.Text = "Undo Changes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents Original As ColumnHeader
    Friend WithEvents Operation As ColumnHeader
    Friend WithEvents NewFile As ColumnHeader
    Friend WithEvents btnUndo As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents Label1 As Label
End Class
