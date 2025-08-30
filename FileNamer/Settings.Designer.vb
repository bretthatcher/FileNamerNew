<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
        Me.lblOriginalMovieFolder = New System.Windows.Forms.Label()
        Me.txtOriginalMovieFolder = New System.Windows.Forms.TextBox()
        Me.btnOriginalMovieFolder = New System.Windows.Forms.Button()
        Me.btnRenamedMovieFolder = New System.Windows.Forms.Button()
        Me.txtRenamedMovieFolder = New System.Windows.Forms.TextBox()
        Me.lblRenamedMovieFolder = New System.Windows.Forms.Label()
        Me.btnOriginalTVFolder = New System.Windows.Forms.Button()
        Me.txtOriginalTVFolder = New System.Windows.Forms.TextBox()
        Me.lblOriginalTVFolder = New System.Windows.Forms.Label()
        Me.btnRenamedTVFolder = New System.Windows.Forms.Button()
        Me.txtRenamedTVFolder = New System.Windows.Forms.TextBox()
        Me.lblRenamedTVFolder = New System.Windows.Forms.Label()
        Me.cbWriteResults = New System.Windows.Forms.CheckBox()
        Me.cbSuggestFileName = New System.Windows.Forms.CheckBox()
        Me.cbIndividualFolders = New System.Windows.Forms.CheckBox()
        Me.cbIncludeSubtitleFiles = New System.Windows.Forms.CheckBox()
        Me.cbMakeChanges = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lblOriginalMovieFolder
        '
        Me.lblOriginalMovieFolder.AutoSize = True
        Me.lblOriginalMovieFolder.Location = New System.Drawing.Point(18, 22)
        Me.lblOriginalMovieFolder.Name = "lblOriginalMovieFolder"
        Me.lblOriginalMovieFolder.Size = New System.Drawing.Size(106, 13)
        Me.lblOriginalMovieFolder.TabIndex = 0
        Me.lblOriginalMovieFolder.Text = "Original Movie Folder"
        '
        'txtOriginalMovieFolder
        '
        Me.txtOriginalMovieFolder.Location = New System.Drawing.Point(131, 17)
        Me.txtOriginalMovieFolder.Name = "txtOriginalMovieFolder"
        Me.txtOriginalMovieFolder.ReadOnly = True
        Me.txtOriginalMovieFolder.Size = New System.Drawing.Size(283, 20)
        Me.txtOriginalMovieFolder.TabIndex = 1
        '
        'btnOriginalMovieFolder
        '
        Me.btnOriginalMovieFolder.Location = New System.Drawing.Point(411, 17)
        Me.btnOriginalMovieFolder.Name = "btnOriginalMovieFolder"
        Me.btnOriginalMovieFolder.Size = New System.Drawing.Size(23, 21)
        Me.btnOriginalMovieFolder.TabIndex = 2
        Me.btnOriginalMovieFolder.Text = "Button1"
        Me.btnOriginalMovieFolder.UseVisualStyleBackColor = True
        '
        'btnRenamedMovieFolder
        '
        Me.btnRenamedMovieFolder.Location = New System.Drawing.Point(412, 44)
        Me.btnRenamedMovieFolder.Name = "btnRenamedMovieFolder"
        Me.btnRenamedMovieFolder.Size = New System.Drawing.Size(23, 21)
        Me.btnRenamedMovieFolder.TabIndex = 5
        Me.btnRenamedMovieFolder.Text = "Button2"
        Me.btnRenamedMovieFolder.UseVisualStyleBackColor = True
        '
        'txtRenamedMovieFolder
        '
        Me.txtRenamedMovieFolder.Enabled = False
        Me.txtRenamedMovieFolder.Location = New System.Drawing.Point(132, 44)
        Me.txtRenamedMovieFolder.Name = "txtRenamedMovieFolder"
        Me.txtRenamedMovieFolder.Size = New System.Drawing.Size(283, 20)
        Me.txtRenamedMovieFolder.TabIndex = 4
        '
        'lblRenamedMovieFolder
        '
        Me.lblRenamedMovieFolder.AutoSize = True
        Me.lblRenamedMovieFolder.Location = New System.Drawing.Point(18, 49)
        Me.lblRenamedMovieFolder.Name = "lblRenamedMovieFolder"
        Me.lblRenamedMovieFolder.Size = New System.Drawing.Size(117, 13)
        Me.lblRenamedMovieFolder.TabIndex = 3
        Me.lblRenamedMovieFolder.Text = "Renamed Movie Folder"
        '
        'btnOriginalTVFolder
        '
        Me.btnOriginalTVFolder.Location = New System.Drawing.Point(412, 71)
        Me.btnOriginalTVFolder.Name = "btnOriginalTVFolder"
        Me.btnOriginalTVFolder.Size = New System.Drawing.Size(23, 21)
        Me.btnOriginalTVFolder.TabIndex = 8
        Me.btnOriginalTVFolder.Text = "Button3"
        Me.btnOriginalTVFolder.UseVisualStyleBackColor = True
        '
        'txtOriginalTVFolder
        '
        Me.txtOriginalTVFolder.Enabled = False
        Me.txtOriginalTVFolder.Location = New System.Drawing.Point(132, 71)
        Me.txtOriginalTVFolder.Name = "txtOriginalTVFolder"
        Me.txtOriginalTVFolder.Size = New System.Drawing.Size(283, 20)
        Me.txtOriginalTVFolder.TabIndex = 7
        '
        'lblOriginalTVFolder
        '
        Me.lblOriginalTVFolder.AutoSize = True
        Me.lblOriginalTVFolder.Location = New System.Drawing.Point(18, 76)
        Me.lblOriginalTVFolder.Name = "lblOriginalTVFolder"
        Me.lblOriginalTVFolder.Size = New System.Drawing.Size(91, 13)
        Me.lblOriginalTVFolder.TabIndex = 6
        Me.lblOriginalTVFolder.Text = "Original TV Folder"
        '
        'btnRenamedTVFolder
        '
        Me.btnRenamedTVFolder.Location = New System.Drawing.Point(412, 98)
        Me.btnRenamedTVFolder.Name = "btnRenamedTVFolder"
        Me.btnRenamedTVFolder.Size = New System.Drawing.Size(23, 21)
        Me.btnRenamedTVFolder.TabIndex = 11
        Me.btnRenamedTVFolder.Text = "Button4"
        Me.btnRenamedTVFolder.UseVisualStyleBackColor = True
        '
        'txtRenamedTVFolder
        '
        Me.txtRenamedTVFolder.Enabled = False
        Me.txtRenamedTVFolder.Location = New System.Drawing.Point(132, 98)
        Me.txtRenamedTVFolder.Name = "txtRenamedTVFolder"
        Me.txtRenamedTVFolder.Size = New System.Drawing.Size(283, 20)
        Me.txtRenamedTVFolder.TabIndex = 10
        '
        'lblRenamedTVFolder
        '
        Me.lblRenamedTVFolder.AutoSize = True
        Me.lblRenamedTVFolder.Location = New System.Drawing.Point(18, 103)
        Me.lblRenamedTVFolder.Name = "lblRenamedTVFolder"
        Me.lblRenamedTVFolder.Size = New System.Drawing.Size(102, 13)
        Me.lblRenamedTVFolder.TabIndex = 9
        Me.lblRenamedTVFolder.Text = "Renamed TV Folder"
        '
        'cbWriteResults
        '
        Me.cbWriteResults.AutoSize = True
        Me.cbWriteResults.Location = New System.Drawing.Point(21, 169)
        Me.cbWriteResults.Name = "cbWriteResults"
        Me.cbWriteResults.Size = New System.Drawing.Size(89, 17)
        Me.cbWriteResults.TabIndex = 12
        Me.cbWriteResults.Text = "Write Results"
        Me.cbWriteResults.UseVisualStyleBackColor = True
        '
        'cbSuggestFileName
        '
        Me.cbSuggestFileName.AutoSize = True
        Me.cbSuggestFileName.Location = New System.Drawing.Point(21, 200)
        Me.cbSuggestFileName.Name = "cbSuggestFileName"
        Me.cbSuggestFileName.Size = New System.Drawing.Size(200, 17)
        Me.cbSuggestFileName.TabIndex = 13
        Me.cbSuggestFileName.Text = "Suggest File Name When Not Found"
        Me.cbSuggestFileName.UseVisualStyleBackColor = True
        '
        'cbIndividualFolders
        '
        Me.cbIndividualFolders.AutoSize = True
        Me.cbIndividualFolders.Location = New System.Drawing.Point(21, 231)
        Me.cbIndividualFolders.Name = "cbIndividualFolders"
        Me.cbIndividualFolders.Size = New System.Drawing.Size(108, 17)
        Me.cbIndividualFolders.TabIndex = 14
        Me.cbIndividualFolders.Text = "Individual Folders"
        Me.cbIndividualFolders.UseVisualStyleBackColor = True
        '
        'cbIncludeSubtitleFiles
        '
        Me.cbIncludeSubtitleFiles.AutoSize = True
        Me.cbIncludeSubtitleFiles.Location = New System.Drawing.Point(21, 262)
        Me.cbIncludeSubtitleFiles.Name = "cbIncludeSubtitleFiles"
        Me.cbIncludeSubtitleFiles.Size = New System.Drawing.Size(127, 17)
        Me.cbIncludeSubtitleFiles.TabIndex = 15
        Me.cbIncludeSubtitleFiles.Text = "Include SubTitle Files"
        Me.cbIncludeSubtitleFiles.UseVisualStyleBackColor = True
        '
        'cbMakeChanges
        '
        Me.cbMakeChanges.AutoSize = True
        Me.cbMakeChanges.Location = New System.Drawing.Point(21, 293)
        Me.cbMakeChanges.Name = "cbMakeChanges"
        Me.cbMakeChanges.Size = New System.Drawing.Size(98, 17)
        Me.cbMakeChanges.TabIndex = 16
        Me.cbMakeChanges.Text = "Make Changes"
        Me.cbMakeChanges.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.cbMakeChanges)
        Me.Controls.Add(Me.cbIncludeSubtitleFiles)
        Me.Controls.Add(Me.cbIndividualFolders)
        Me.Controls.Add(Me.cbSuggestFileName)
        Me.Controls.Add(Me.cbWriteResults)
        Me.Controls.Add(Me.btnRenamedTVFolder)
        Me.Controls.Add(Me.txtRenamedTVFolder)
        Me.Controls.Add(Me.lblRenamedTVFolder)
        Me.Controls.Add(Me.btnOriginalTVFolder)
        Me.Controls.Add(Me.txtOriginalTVFolder)
        Me.Controls.Add(Me.lblOriginalTVFolder)
        Me.Controls.Add(Me.btnRenamedMovieFolder)
        Me.Controls.Add(Me.txtRenamedMovieFolder)
        Me.Controls.Add(Me.lblRenamedMovieFolder)
        Me.Controls.Add(Me.btnOriginalMovieFolder)
        Me.Controls.Add(Me.txtOriginalMovieFolder)
        Me.Controls.Add(Me.lblOriginalMovieFolder)
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblOriginalMovieFolder As Label
    Friend WithEvents txtOriginalMovieFolder As TextBox
    Friend WithEvents btnOriginalMovieFolder As Button
    Friend WithEvents btnRenamedMovieFolder As Button
    Friend WithEvents txtRenamedMovieFolder As TextBox
    Friend WithEvents lblRenamedMovieFolder As Label
    Friend WithEvents btnOriginalTVFolder As Button
    Friend WithEvents txtOriginalTVFolder As TextBox
    Friend WithEvents lblOriginalTVFolder As Label
    Friend WithEvents btnRenamedTVFolder As Button
    Friend WithEvents txtRenamedTVFolder As TextBox
    Friend WithEvents lblRenamedTVFolder As Label
    Friend WithEvents cbWriteResults As CheckBox
    Friend WithEvents cbSuggestFileName As CheckBox
    Friend WithEvents cbIndividualFolders As CheckBox
    Friend WithEvents cbIncludeSubtitleFiles As CheckBox
    Friend WithEvents cbMakeChanges As CheckBox
End Class
