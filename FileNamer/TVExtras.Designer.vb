<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TVExtras
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
        Me.cmbSeasonText = New System.Windows.Forms.ComboBox()
        Me.cmbSeasonNumber = New System.Windows.Forms.ComboBox()
        Me.cmbSeasonEpisodeSeperator = New System.Windows.Forms.ComboBox()
        Me.cmbEpisodeText = New System.Windows.Forms.ComboBox()
        Me.cmbDescriptionSeperator = New System.Windows.Forms.ComboBox()
        Me.cmbTitleSeperator = New System.Windows.Forms.ComboBox()
        Me.lblTVExtras = New System.Windows.Forms.Label()
        Me.cmbEpisodeNumber = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.cmbDescription = New System.Windows.Forms.ComboBox()
        Me.rbOption1 = New System.Windows.Forms.RadioButton()
        Me.rbOption2 = New System.Windows.Forms.RadioButton()
        Me.rbOption3 = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmbSeasonText
        '
        Me.cmbSeasonText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSeasonText.FormattingEnabled = True
        Me.cmbSeasonText.Items.AddRange(New Object() {"<none>", "S", "s", "Season"})
        Me.cmbSeasonText.Location = New System.Drawing.Point(151, 189)
        Me.cmbSeasonText.Name = "cmbSeasonText"
        Me.cmbSeasonText.Size = New System.Drawing.Size(79, 21)
        Me.cmbSeasonText.TabIndex = 0
        '
        'cmbSeasonNumber
        '
        Me.cmbSeasonNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSeasonNumber.FormattingEnabled = True
        Me.cmbSeasonNumber.Items.AddRange(New Object() {"Leading Zero", "No Leading Zero"})
        Me.cmbSeasonNumber.Location = New System.Drawing.Point(236, 189)
        Me.cmbSeasonNumber.Name = "cmbSeasonNumber"
        Me.cmbSeasonNumber.Size = New System.Drawing.Size(109, 21)
        Me.cmbSeasonNumber.TabIndex = 1
        '
        'cmbSeasonEpisodeSeperator
        '
        Me.cmbSeasonEpisodeSeperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSeasonEpisodeSeperator.FormattingEnabled = True
        Me.cmbSeasonEpisodeSeperator.Items.AddRange(New Object() {"<none>", "X", "x"})
        Me.cmbSeasonEpisodeSeperator.Location = New System.Drawing.Point(351, 189)
        Me.cmbSeasonEpisodeSeperator.Name = "cmbSeasonEpisodeSeperator"
        Me.cmbSeasonEpisodeSeperator.Size = New System.Drawing.Size(79, 21)
        Me.cmbSeasonEpisodeSeperator.TabIndex = 2
        '
        'cmbEpisodeText
        '
        Me.cmbEpisodeText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEpisodeText.FormattingEnabled = True
        Me.cmbEpisodeText.Items.AddRange(New Object() {"<none>", "E", "e", "Episode"})
        Me.cmbEpisodeText.Location = New System.Drawing.Point(436, 189)
        Me.cmbEpisodeText.Name = "cmbEpisodeText"
        Me.cmbEpisodeText.Size = New System.Drawing.Size(79, 21)
        Me.cmbEpisodeText.TabIndex = 3
        '
        'cmbDescriptionSeperator
        '
        Me.cmbDescriptionSeperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDescriptionSeperator.FormattingEnabled = True
        Me.cmbDescriptionSeperator.Items.AddRange(New Object() {"<none>", "<space>", "<dash>", "<space><dash><space>"})
        Me.cmbDescriptionSeperator.Location = New System.Drawing.Point(636, 189)
        Me.cmbDescriptionSeperator.Name = "cmbDescriptionSeperator"
        Me.cmbDescriptionSeperator.Size = New System.Drawing.Size(132, 21)
        Me.cmbDescriptionSeperator.TabIndex = 4
        '
        'cmbTitleSeperator
        '
        Me.cmbTitleSeperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTitleSeperator.FormattingEnabled = True
        Me.cmbTitleSeperator.Items.AddRange(New Object() {"<space>", "<dash>", "<space><dash><space>"})
        Me.cmbTitleSeperator.Location = New System.Drawing.Point(13, 189)
        Me.cmbTitleSeperator.Name = "cmbTitleSeperator"
        Me.cmbTitleSeperator.Size = New System.Drawing.Size(132, 21)
        Me.cmbTitleSeperator.TabIndex = 5
        '
        'lblTVExtras
        '
        Me.lblTVExtras.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVExtras.Location = New System.Drawing.Point(102, 20)
        Me.lblTVExtras.Name = "lblTVExtras"
        Me.lblTVExtras.Size = New System.Drawing.Size(364, 17)
        Me.lblTVExtras.TabIndex = 6
        '
        'cmbEpisodeNumber
        '
        Me.cmbEpisodeNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEpisodeNumber.FormattingEnabled = True
        Me.cmbEpisodeNumber.Items.AddRange(New Object() {"Leading Zero", "No Leading Zero"})
        Me.cmbEpisodeNumber.Location = New System.Drawing.Point(521, 189)
        Me.cmbEpisodeNumber.Name = "cmbEpisodeNumber"
        Me.cmbEpisodeNumber.Size = New System.Drawing.Size(109, 21)
        Me.cmbEpisodeNumber.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 165)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(831, 24)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Title Seperator                      Season Text        Season Number            " &
    " Seperator             Episode Text        Episode Number            Description" &
    " Seperator            Description"
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Location = New System.Drawing.Point(838, 242)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(108, 36)
        Me.btnSaveExit.TabIndex = 12
        Me.btnSaveExit.Text = "Save"
        Me.btnSaveExit.UseVisualStyleBackColor = True
        '
        'cmbDescription
        '
        Me.cmbDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDescription.FormattingEnabled = True
        Me.cmbDescription.Items.AddRange(New Object() {"Show", "Don't Show"})
        Me.cmbDescription.Location = New System.Drawing.Point(774, 189)
        Me.cmbDescription.Name = "cmbDescription"
        Me.cmbDescription.Size = New System.Drawing.Size(70, 21)
        Me.cmbDescription.TabIndex = 13
        '
        'rbOption1
        '
        Me.rbOption1.AutoSize = True
        Me.rbOption1.Location = New System.Drawing.Point(16, 57)
        Me.rbOption1.Name = "rbOption1"
        Me.rbOption1.Size = New System.Drawing.Size(178, 17)
        Me.rbOption1.TabIndex = 14
        Me.rbOption1.TabStop = True
        Me.rbOption1.Text = "TV Show - S01E01 - Description"
        Me.rbOption1.UseVisualStyleBackColor = True
        '
        'rbOption2
        '
        Me.rbOption2.AutoSize = True
        Me.rbOption2.Location = New System.Drawing.Point(16, 95)
        Me.rbOption2.Name = "rbOption2"
        Me.rbOption2.Size = New System.Drawing.Size(169, 17)
        Me.rbOption2.TabIndex = 15
        Me.rbOption2.TabStop = True
        Me.rbOption2.Text = "TV Show - 01x01 - Description"
        Me.rbOption2.UseVisualStyleBackColor = True
        '
        'rbOption3
        '
        Me.rbOption3.AutoSize = True
        Me.rbOption3.Location = New System.Drawing.Point(16, 129)
        Me.rbOption3.Name = "rbOption3"
        Me.rbOption3.Size = New System.Drawing.Size(60, 17)
        Me.rbOption3.TabIndex = 16
        Me.rbOption3.TabStop = True
        Me.rbOption3.Text = "Custom"
        Me.rbOption3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 17)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Example:"
        '
        'TVExtras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 286)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rbOption3)
        Me.Controls.Add(Me.rbOption2)
        Me.Controls.Add(Me.rbOption1)
        Me.Controls.Add(Me.cmbDescription)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbEpisodeNumber)
        Me.Controls.Add(Me.lblTVExtras)
        Me.Controls.Add(Me.cmbTitleSeperator)
        Me.Controls.Add(Me.cmbDescriptionSeperator)
        Me.Controls.Add(Me.cmbEpisodeText)
        Me.Controls.Add(Me.cmbSeasonEpisodeSeperator)
        Me.Controls.Add(Me.cmbSeasonNumber)
        Me.Controls.Add(Me.cmbSeasonText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "TVExtras"
        Me.Text = "TV Extras"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbSeasonText As ComboBox
    Friend WithEvents cmbSeasonNumber As ComboBox
    Friend WithEvents cmbSeasonEpisodeSeperator As ComboBox
    Friend WithEvents cmbEpisodeText As ComboBox
    Friend WithEvents cmbDescriptionSeperator As ComboBox
    Friend WithEvents cmbTitleSeperator As ComboBox
    Friend WithEvents lblTVExtras As Label
    Friend WithEvents cmbEpisodeNumber As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSaveExit As Button
    Friend WithEvents cmbDescription As ComboBox
    Friend WithEvents rbOption1 As RadioButton
    Friend WithEvents rbOption2 As RadioButton
    Friend WithEvents rbOption3 As RadioButton
    Friend WithEvents Label2 As Label
End Class
