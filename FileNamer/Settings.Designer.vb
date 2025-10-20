<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblOriginalMovie = New System.Windows.Forms.Label()
        Me.lblRenamedMovie = New System.Windows.Forms.Label()
        Me.lblOriginalTV = New System.Windows.Forms.Label()
        Me.lblRenamedTV = New System.Windows.Forms.Label()
        Me.cbWriteResults = New System.Windows.Forms.CheckBox()
        Me.cbSuggestFileName = New System.Windows.Forms.CheckBox()
        Me.cbIndividualFolders = New System.Windows.Forms.CheckBox()
        Me.cbIncludeSubtitleFiles = New System.Windows.Forms.CheckBox()
        Me.cbMakeChanges = New System.Windows.Forms.CheckBox()
        Me.cbRemoveEmptyFolders = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblOriginalMovieFolder = New System.Windows.Forms.Label()
        Me.btnOriginalMovieFolder = New System.Windows.Forms.Button()
        Me.lblRenamedMovieFolder = New System.Windows.Forms.Label()
        Me.btnRenamedMovieFolder = New System.Windows.Forms.Button()
        Me.lblOriginalTVFolder = New System.Windows.Forms.Label()
        Me.btnOriginalTVFolder = New System.Windows.Forms.Button()
        Me.lblRenamedTVFolder = New System.Windows.Forms.Label()
        Me.btnRenamedTVFolder = New System.Windows.Forms.Button()
        Me.lblDefaults = New System.Windows.Forms.Label()
        Me.lblOptions = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.lblDefaultOperation = New System.Windows.Forms.Label()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.cmbDefaultAction = New System.Windows.Forms.ComboBox()
        Me.lblDefaultLibraryTxt = New System.Windows.Forms.Label()
        Me.cmbDefaultMovieLibrary = New System.Windows.Forms.ComboBox()
        Me.cmbDefaultTVLibrary = New System.Windows.Forms.ComboBox()
        Me.lblMovieLibrary = New System.Windows.Forms.Label()
        Me.lblTVLibrary = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblOriginalMovie
        '
        Me.lblOriginalMovie.AutoSize = True
        Me.lblOriginalMovie.Location = New System.Drawing.Point(29, 58)
        Me.lblOriginalMovie.Name = "lblOriginalMovie"
        Me.lblOriginalMovie.Size = New System.Drawing.Size(106, 13)
        Me.lblOriginalMovie.TabIndex = 0
        Me.lblOriginalMovie.Text = "Original Movie Folder"
        '
        'lblRenamedMovie
        '
        Me.lblRenamedMovie.AutoSize = True
        Me.lblRenamedMovie.Location = New System.Drawing.Point(18, 92)
        Me.lblRenamedMovie.Name = "lblRenamedMovie"
        Me.lblRenamedMovie.Size = New System.Drawing.Size(117, 13)
        Me.lblRenamedMovie.TabIndex = 3
        Me.lblRenamedMovie.Text = "Renamed Movie Folder"
        '
        'lblOriginalTV
        '
        Me.lblOriginalTV.AutoSize = True
        Me.lblOriginalTV.Location = New System.Drawing.Point(44, 125)
        Me.lblOriginalTV.Name = "lblOriginalTV"
        Me.lblOriginalTV.Size = New System.Drawing.Size(91, 13)
        Me.lblOriginalTV.TabIndex = 6
        Me.lblOriginalTV.Text = "Original TV Folder"
        '
        'lblRenamedTV
        '
        Me.lblRenamedTV.AutoSize = True
        Me.lblRenamedTV.Location = New System.Drawing.Point(33, 158)
        Me.lblRenamedTV.Name = "lblRenamedTV"
        Me.lblRenamedTV.Size = New System.Drawing.Size(102, 13)
        Me.lblRenamedTV.TabIndex = 9
        Me.lblRenamedTV.Text = "Renamed TV Folder"
        '
        'cbWriteResults
        '
        Me.cbWriteResults.AutoSize = True
        Me.cbWriteResults.Location = New System.Drawing.Point(28, 424)
        Me.cbWriteResults.Name = "cbWriteResults"
        Me.cbWriteResults.Size = New System.Drawing.Size(89, 17)
        Me.cbWriteResults.TabIndex = 12
        Me.cbWriteResults.Text = "Write Results"
        Me.cbWriteResults.UseVisualStyleBackColor = True
        '
        'cbSuggestFileName
        '
        Me.cbSuggestFileName.AutoSize = True
        Me.cbSuggestFileName.Location = New System.Drawing.Point(28, 455)
        Me.cbSuggestFileName.Name = "cbSuggestFileName"
        Me.cbSuggestFileName.Size = New System.Drawing.Size(200, 17)
        Me.cbSuggestFileName.TabIndex = 13
        Me.cbSuggestFileName.Text = "Suggest File Name When Not Found"
        Me.cbSuggestFileName.UseVisualStyleBackColor = True
        '
        'cbIndividualFolders
        '
        Me.cbIndividualFolders.AutoSize = True
        Me.cbIndividualFolders.Location = New System.Drawing.Point(28, 486)
        Me.cbIndividualFolders.Name = "cbIndividualFolders"
        Me.cbIndividualFolders.Size = New System.Drawing.Size(108, 17)
        Me.cbIndividualFolders.TabIndex = 14
        Me.cbIndividualFolders.Text = "Individual Folders"
        Me.cbIndividualFolders.UseVisualStyleBackColor = True
        '
        'cbIncludeSubtitleFiles
        '
        Me.cbIncludeSubtitleFiles.AutoSize = True
        Me.cbIncludeSubtitleFiles.Location = New System.Drawing.Point(28, 552)
        Me.cbIncludeSubtitleFiles.Name = "cbIncludeSubtitleFiles"
        Me.cbIncludeSubtitleFiles.Size = New System.Drawing.Size(123, 17)
        Me.cbIncludeSubtitleFiles.TabIndex = 15
        Me.cbIncludeSubtitleFiles.Text = "Include Subtitle Files"
        Me.cbIncludeSubtitleFiles.UseVisualStyleBackColor = True
        '
        'cbMakeChanges
        '
        Me.cbMakeChanges.AutoSize = True
        Me.cbMakeChanges.Location = New System.Drawing.Point(28, 585)
        Me.cbMakeChanges.Name = "cbMakeChanges"
        Me.cbMakeChanges.Size = New System.Drawing.Size(98, 17)
        Me.cbMakeChanges.TabIndex = 16
        Me.cbMakeChanges.Text = "Make Changes"
        Me.cbMakeChanges.UseVisualStyleBackColor = True
        '
        'cbRemoveEmptyFolders
        '
        Me.cbRemoveEmptyFolders.AutoSize = True
        Me.cbRemoveEmptyFolders.Location = New System.Drawing.Point(28, 519)
        Me.cbRemoveEmptyFolders.Name = "cbRemoveEmptyFolders"
        Me.cbRemoveEmptyFolders.Size = New System.Drawing.Size(243, 17)
        Me.cbRemoveEmptyFolders.TabIndex = 17
        Me.cbRemoveEmptyFolders.Text = "Remove Empty Original Subfolders after Move"
        Me.cbRemoveEmptyFolders.UseVisualStyleBackColor = True
        '
        'lblOriginalMovieFolder
        '
        Me.lblOriginalMovieFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblOriginalMovieFolder.Location = New System.Drawing.Point(143, 54)
        Me.lblOriginalMovieFolder.Name = "lblOriginalMovieFolder"
        Me.lblOriginalMovieFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblOriginalMovieFolder.TabIndex = 25
        Me.lblOriginalMovieFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnOriginalMovieFolder
        '
        Me.btnOriginalMovieFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnOriginalMovieFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOriginalMovieFolder.CausesValidation = False
        Me.btnOriginalMovieFolder.Location = New System.Drawing.Point(420, 50)
        Me.btnOriginalMovieFolder.Name = "btnOriginalMovieFolder"
        Me.btnOriginalMovieFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnOriginalMovieFolder.TabIndex = 24
        Me.btnOriginalMovieFolder.UseVisualStyleBackColor = True
        '
        'lblRenamedMovieFolder
        '
        Me.lblRenamedMovieFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblRenamedMovieFolder.Location = New System.Drawing.Point(143, 88)
        Me.lblRenamedMovieFolder.Name = "lblRenamedMovieFolder"
        Me.lblRenamedMovieFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblRenamedMovieFolder.TabIndex = 27
        Me.lblRenamedMovieFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRenamedMovieFolder
        '
        Me.btnRenamedMovieFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnRenamedMovieFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRenamedMovieFolder.CausesValidation = False
        Me.btnRenamedMovieFolder.Location = New System.Drawing.Point(420, 84)
        Me.btnRenamedMovieFolder.Name = "btnRenamedMovieFolder"
        Me.btnRenamedMovieFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnRenamedMovieFolder.TabIndex = 26
        Me.btnRenamedMovieFolder.UseVisualStyleBackColor = True
        '
        'lblOriginalTVFolder
        '
        Me.lblOriginalTVFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblOriginalTVFolder.Location = New System.Drawing.Point(143, 122)
        Me.lblOriginalTVFolder.Name = "lblOriginalTVFolder"
        Me.lblOriginalTVFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblOriginalTVFolder.TabIndex = 29
        Me.lblOriginalTVFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnOriginalTVFolder
        '
        Me.btnOriginalTVFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnOriginalTVFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOriginalTVFolder.CausesValidation = False
        Me.btnOriginalTVFolder.Location = New System.Drawing.Point(420, 118)
        Me.btnOriginalTVFolder.Name = "btnOriginalTVFolder"
        Me.btnOriginalTVFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnOriginalTVFolder.TabIndex = 28
        Me.btnOriginalTVFolder.UseVisualStyleBackColor = True
        '
        'lblRenamedTVFolder
        '
        Me.lblRenamedTVFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblRenamedTVFolder.Location = New System.Drawing.Point(143, 155)
        Me.lblRenamedTVFolder.Name = "lblRenamedTVFolder"
        Me.lblRenamedTVFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblRenamedTVFolder.TabIndex = 31
        Me.lblRenamedTVFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRenamedTVFolder
        '
        Me.btnRenamedTVFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnRenamedTVFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRenamedTVFolder.CausesValidation = False
        Me.btnRenamedTVFolder.Location = New System.Drawing.Point(420, 151)
        Me.btnRenamedTVFolder.Name = "btnRenamedTVFolder"
        Me.btnRenamedTVFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnRenamedTVFolder.TabIndex = 30
        Me.btnRenamedTVFolder.UseVisualStyleBackColor = True
        '
        'lblDefaults
        '
        Me.lblDefaults.AutoSize = True
        Me.lblDefaults.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaults.Location = New System.Drawing.Point(24, 11)
        Me.lblDefaults.Name = "lblDefaults"
        Me.lblDefaults.Size = New System.Drawing.Size(133, 20)
        Me.lblDefaults.TabIndex = 32
        Me.lblDefaults.Text = "Default Folders"
        '
        'lblOptions
        '
        Me.lblOptions.AutoSize = True
        Me.lblOptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOptions.Location = New System.Drawing.Point(24, 392)
        Me.lblOptions.Name = "lblOptions"
        Me.lblOptions.Size = New System.Drawing.Size(71, 20)
        Me.lblOptions.TabIndex = 33
        Me.lblOptions.Text = "Options"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(389, 609)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(92, 37)
        Me.btnExit.TabIndex = 34
        Me.btnExit.Text = "Save"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lblDefaultOperation
        '
        Me.lblDefaultOperation.AutoSize = True
        Me.lblDefaultOperation.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultOperation.Location = New System.Drawing.Point(24, 302)
        Me.lblDefaultOperation.Name = "lblDefaultOperation"
        Me.lblDefaultOperation.Size = New System.Drawing.Size(124, 20)
        Me.lblDefaultOperation.TabIndex = 35
        Me.lblDefaultOperation.Text = "Default Action"
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Location = New System.Drawing.Point(89, 345)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(37, 13)
        Me.lblAction.TabIndex = 36
        Me.lblAction.Text = "Action"
        '
        'cmbDefaultAction
        '
        Me.cmbDefaultAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultAction.FormattingEnabled = True
        Me.cmbDefaultAction.Items.AddRange(New Object() {"Rename and Move", "Rename and Copy", "Move", "Copy"})
        Me.cmbDefaultAction.Location = New System.Drawing.Point(141, 343)
        Me.cmbDefaultAction.Name = "cmbDefaultAction"
        Me.cmbDefaultAction.Size = New System.Drawing.Size(279, 21)
        Me.cmbDefaultAction.TabIndex = 37
        '
        'lblDefaultLibraryTxt
        '
        Me.lblDefaultLibraryTxt.AutoSize = True
        Me.lblDefaultLibraryTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultLibraryTxt.Location = New System.Drawing.Point(24, 202)
        Me.lblDefaultLibraryTxt.Name = "lblDefaultLibraryTxt"
        Me.lblDefaultLibraryTxt.Size = New System.Drawing.Size(240, 20)
        Me.lblDefaultLibraryTxt.TabIndex = 38
        Me.lblDefaultLibraryTxt.Text = "Default Library For Renames"
        '
        'cmbDefaultMovieLibrary
        '
        Me.cmbDefaultMovieLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultMovieLibrary.FormattingEnabled = True
        Me.cmbDefaultMovieLibrary.Items.AddRange(New Object() {"The Movie Database (TMDB)", "The TV Database (TVDB)"})
        Me.cmbDefaultMovieLibrary.Location = New System.Drawing.Point(141, 238)
        Me.cmbDefaultMovieLibrary.Name = "cmbDefaultMovieLibrary"
        Me.cmbDefaultMovieLibrary.Size = New System.Drawing.Size(279, 21)
        Me.cmbDefaultMovieLibrary.TabIndex = 39
        '
        'cmbDefaultTVLibrary
        '
        Me.cmbDefaultTVLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultTVLibrary.FormattingEnabled = True
        Me.cmbDefaultTVLibrary.Items.AddRange(New Object() {"The Movie Database (TMDB)", "The TV Database (TVDB)"})
        Me.cmbDefaultTVLibrary.Location = New System.Drawing.Point(141, 265)
        Me.cmbDefaultTVLibrary.Name = "cmbDefaultTVLibrary"
        Me.cmbDefaultTVLibrary.Size = New System.Drawing.Size(279, 21)
        Me.cmbDefaultTVLibrary.TabIndex = 40
        '
        'lblMovieLibrary
        '
        Me.lblMovieLibrary.AutoSize = True
        Me.lblMovieLibrary.Location = New System.Drawing.Point(89, 241)
        Me.lblMovieLibrary.Name = "lblMovieLibrary"
        Me.lblMovieLibrary.Size = New System.Drawing.Size(41, 13)
        Me.lblMovieLibrary.TabIndex = 41
        Me.lblMovieLibrary.Text = "Movies"
        '
        'lblTVLibrary
        '
        Me.lblTVLibrary.AutoSize = True
        Me.lblTVLibrary.Location = New System.Drawing.Point(89, 268)
        Me.lblTVLibrary.Name = "lblTVLibrary"
        Me.lblTVLibrary.Size = New System.Drawing.Size(21, 13)
        Me.lblTVLibrary.TabIndex = 42
        Me.lblTVLibrary.Text = "TV"
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 659)
        Me.Controls.Add(Me.lblTVLibrary)
        Me.Controls.Add(Me.lblMovieLibrary)
        Me.Controls.Add(Me.cmbDefaultTVLibrary)
        Me.Controls.Add(Me.cmbDefaultMovieLibrary)
        Me.Controls.Add(Me.lblDefaultLibraryTxt)
        Me.Controls.Add(Me.cmbDefaultAction)
        Me.Controls.Add(Me.lblAction)
        Me.Controls.Add(Me.lblDefaultOperation)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblOptions)
        Me.Controls.Add(Me.lblDefaults)
        Me.Controls.Add(Me.lblRenamedTVFolder)
        Me.Controls.Add(Me.btnRenamedTVFolder)
        Me.Controls.Add(Me.lblOriginalTVFolder)
        Me.Controls.Add(Me.btnOriginalTVFolder)
        Me.Controls.Add(Me.lblRenamedMovieFolder)
        Me.Controls.Add(Me.btnRenamedMovieFolder)
        Me.Controls.Add(Me.lblOriginalMovieFolder)
        Me.Controls.Add(Me.btnOriginalMovieFolder)
        Me.Controls.Add(Me.cbRemoveEmptyFolders)
        Me.Controls.Add(Me.cbMakeChanges)
        Me.Controls.Add(Me.cbIncludeSubtitleFiles)
        Me.Controls.Add(Me.cbIndividualFolders)
        Me.Controls.Add(Me.cbSuggestFileName)
        Me.Controls.Add(Me.cbWriteResults)
        Me.Controls.Add(Me.lblRenamedTV)
        Me.Controls.Add(Me.lblOriginalTV)
        Me.Controls.Add(Me.lblRenamedMovie)
        Me.Controls.Add(Me.lblOriginalMovie)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Settings"
        Me.Text = "Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblOriginalMovie As Label
    Friend WithEvents lblRenamedMovie As Label
    Friend WithEvents lblOriginalTV As Label
    Friend WithEvents lblRenamedTV As Label
    Friend WithEvents cbWriteResults As CheckBox
    Friend WithEvents cbSuggestFileName As CheckBox
    Friend WithEvents cbIndividualFolders As CheckBox
    Friend WithEvents cbIncludeSubtitleFiles As CheckBox
    Friend WithEvents cbMakeChanges As CheckBox
    Friend WithEvents cbRemoveEmptyFolders As CheckBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents lblOriginalMovieFolder As Label
    Friend WithEvents btnOriginalMovieFolder As Button
    Friend WithEvents lblRenamedMovieFolder As Label
    Friend WithEvents btnRenamedMovieFolder As Button
    Friend WithEvents lblOriginalTVFolder As Label
    Friend WithEvents btnOriginalTVFolder As Button
    Friend WithEvents lblRenamedTVFolder As Label
    Friend WithEvents btnRenamedTVFolder As Button
    Friend WithEvents lblDefaults As Label
    Friend WithEvents lblOptions As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents lblDefaultOperation As Label
    Friend WithEvents lblAction As Label
    Friend WithEvents cmbDefaultAction As ComboBox
    Friend WithEvents lblDefaultLibraryTxt As Label
    Friend WithEvents cmbDefaultMovieLibrary As ComboBox
    Friend WithEvents cmbDefaultTVLibrary As ComboBox
    Friend WithEvents lblMovieLibrary As Label
    Friend WithEvents lblTVLibrary As Label
End Class
