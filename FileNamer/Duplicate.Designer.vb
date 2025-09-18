<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Duplicate
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
        Me.lbDuplicates = New System.Windows.Forms.ListBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbDuplicates
        '
        Me.lbDuplicates.FormattingEnabled = True
        Me.lbDuplicates.Location = New System.Drawing.Point(20, 19)
        Me.lbDuplicates.Name = "lbDuplicates"
        Me.lbDuplicates.Size = New System.Drawing.Size(289, 225)
        Me.lbDuplicates.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(672, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(114, 107)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Location = New System.Drawing.Point(663, 202)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(123, 42)
        Me.btnSaveExit.TabIndex = 2
        Me.btnSaveExit.Text = "Save and Exit"
        Me.btnSaveExit.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDescription.Location = New System.Drawing.Point(349, 21)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(284, 106)
        Me.lblDescription.TabIndex = 3
        '
        'Duplicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 265)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lbDuplicates)
        Me.Name = "Duplicate"
        Me.Text = "Choose Media Match"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbDuplicates As ListBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnSaveExit As Button
    Friend WithEvents lblDescription As Label
End Class
