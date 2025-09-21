<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.UseDefaultMovieFoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseDefaultTVFoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lbOriginal = New System.Windows.Forms.ListBox()
        Me.lbNew = New System.Windows.Forms.ListBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnOriginalFolder = New System.Windows.Forms.Button()
        Me.lblOriginal = New System.Windows.Forms.Label()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.cbMakeChanges = New System.Windows.Forms.CheckBox()
        Me.cbMovies = New System.Windows.Forms.CheckBox()
        Me.cbTV = New System.Windows.Forms.CheckBox()
        Me.cbSelectAll = New System.Windows.Forms.CheckBox()
        Me.cmbOperation = New System.Windows.Forms.ComboBox()
        Me.btnNewFolder = New System.Windows.Forms.Button()
        Me.lblOriginalFolder = New System.Windows.Forms.Label()
        Me.lblNewFolder = New System.Windows.Forms.Label()
        Me.lblAction = New System.Windows.Forms.Label()
        Me.btnChangeExample = New System.Windows.Forms.Button()
        Me.lblExtras = New System.Windows.Forms.Label()
        Me.lblExample = New System.Windows.Forms.Label()
        Me.lblProcessingText = New System.Windows.Forms.Label()
        Me.lblCurrentItemText = New System.Windows.Forms.Label()
        Me.lblTotalItemsText = New System.Windows.Forms.Label()
        Me.lblProcessingOfText = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.SettingsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1105, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem2.Text = "Undo Changes"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ToolStripSeparator1, Me.UseDefaultMovieFoldersToolStripMenuItem, Me.UseDefaultTVFoldersToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(208, 6)
        '
        'UseDefaultMovieFoldersToolStripMenuItem
        '
        Me.UseDefaultMovieFoldersToolStripMenuItem.Name = "UseDefaultMovieFoldersToolStripMenuItem"
        Me.UseDefaultMovieFoldersToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.UseDefaultMovieFoldersToolStripMenuItem.Text = "Use Default Movie Folders"
        '
        'UseDefaultTVFoldersToolStripMenuItem
        '
        Me.UseDefaultTVFoldersToolStripMenuItem.Name = "UseDefaultTVFoldersToolStripMenuItem"
        Me.UseDefaultTVFoldersToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.UseDefaultTVFoldersToolStripMenuItem.Text = "Use Default TV Folders"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'lbOriginal
        '
        Me.lbOriginal.BackColor = System.Drawing.Color.LightGreen
        Me.lbOriginal.FormattingEnabled = True
        Me.lbOriginal.Location = New System.Drawing.Point(12, 54)
        Me.lbOriginal.Name = "lbOriginal"
        Me.lbOriginal.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbOriginal.Size = New System.Drawing.Size(531, 290)
        Me.lbOriginal.TabIndex = 1
        '
        'lbNew
        '
        Me.lbNew.BackColor = System.Drawing.Color.LightGreen
        Me.lbNew.FormattingEnabled = True
        Me.lbNew.Location = New System.Drawing.Point(615, 54)
        Me.lbNew.Name = "lbNew"
        Me.lbNew.Size = New System.Drawing.Size(480, 290)
        Me.lbNew.TabIndex = 2
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(927, 441)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(168, 49)
        Me.btnProcess.TabIndex = 3
        Me.btnProcess.Text = "Process Files"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnOriginalFolder
        '
        Me.btnOriginalFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnOriginalFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOriginalFolder.CausesValidation = False
        Me.btnOriginalFolder.Location = New System.Drawing.Point(516, 28)
        Me.btnOriginalFolder.Name = "btnOriginalFolder"
        Me.btnOriginalFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnOriginalFolder.TabIndex = 8
        Me.btnOriginalFolder.UseVisualStyleBackColor = True
        '
        'lblOriginal
        '
        Me.lblOriginal.AutoSize = True
        Me.lblOriginal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOriginal.Location = New System.Drawing.Point(9, 33)
        Me.lblOriginal.Name = "lblOriginal"
        Me.lblOriginal.Size = New System.Drawing.Size(70, 20)
        Me.lblOriginal.TabIndex = 9
        Me.lblOriginal.Text = "Original"
        '
        'lblNew
        '
        Me.lblNew.AutoSize = True
        Me.lblNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNew.Location = New System.Drawing.Point(612, 33)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(43, 20)
        Me.lblNew.TabIndex = 10
        Me.lblNew.Text = "New"
        '
        'cbMakeChanges
        '
        Me.cbMakeChanges.AutoSize = True
        Me.cbMakeChanges.Location = New System.Drawing.Point(997, 354)
        Me.cbMakeChanges.Name = "cbMakeChanges"
        Me.cbMakeChanges.Size = New System.Drawing.Size(98, 17)
        Me.cbMakeChanges.TabIndex = 17
        Me.cbMakeChanges.Text = "Make Changes"
        Me.cbMakeChanges.UseVisualStyleBackColor = True
        '
        'cbMovies
        '
        Me.cbMovies.AutoSize = True
        Me.cbMovies.Location = New System.Drawing.Point(549, 218)
        Me.cbMovies.Name = "cbMovies"
        Me.cbMovies.Size = New System.Drawing.Size(60, 17)
        Me.cbMovies.TabIndex = 18
        Me.cbMovies.Text = "Movies"
        Me.cbMovies.UseVisualStyleBackColor = True
        '
        'cbTV
        '
        Me.cbTV.AutoSize = True
        Me.cbTV.Location = New System.Drawing.Point(549, 241)
        Me.cbTV.Name = "cbTV"
        Me.cbTV.Size = New System.Drawing.Size(40, 17)
        Me.cbTV.TabIndex = 19
        Me.cbTV.Text = "TV"
        Me.cbTV.UseVisualStyleBackColor = True
        '
        'cbSelectAll
        '
        Me.cbSelectAll.AutoSize = True
        Me.cbSelectAll.Location = New System.Drawing.Point(453, 354)
        Me.cbSelectAll.Name = "cbSelectAll"
        Me.cbSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.cbSelectAll.TabIndex = 20
        Me.cbSelectAll.Text = "Select All"
        Me.cbSelectAll.UseVisualStyleBackColor = True
        '
        'cmbOperation
        '
        Me.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperation.FormattingEnabled = True
        Me.cmbOperation.Items.AddRange(New Object() {"Rename and Move", "Rename and Copy", "Move", "Copy"})
        Me.cmbOperation.Location = New System.Drawing.Point(977, 414)
        Me.cmbOperation.Name = "cmbOperation"
        Me.cmbOperation.Size = New System.Drawing.Size(118, 21)
        Me.cmbOperation.TabIndex = 21
        '
        'btnNewFolder
        '
        Me.btnNewFolder.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnNewFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnNewFolder.CausesValidation = False
        Me.btnNewFolder.Location = New System.Drawing.Point(1067, 28)
        Me.btnNewFolder.Name = "btnNewFolder"
        Me.btnNewFolder.Size = New System.Drawing.Size(28, 26)
        Me.btnNewFolder.TabIndex = 22
        Me.btnNewFolder.UseVisualStyleBackColor = True
        '
        'lblOriginalFolder
        '
        Me.lblOriginalFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblOriginalFolder.Location = New System.Drawing.Point(239, 32)
        Me.lblOriginalFolder.Name = "lblOriginalFolder"
        Me.lblOriginalFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblOriginalFolder.TabIndex = 23
        Me.lblOriginalFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNewFolder
        '
        Me.lblNewFolder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblNewFolder.Location = New System.Drawing.Point(790, 32)
        Me.lblNewFolder.Name = "lblNewFolder"
        Me.lblNewFolder.Size = New System.Drawing.Size(277, 20)
        Me.lblNewFolder.TabIndex = 24
        Me.lblNewFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAction.Location = New System.Drawing.Point(924, 418)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(50, 16)
        Me.lblAction.TabIndex = 25
        Me.lblAction.Text = "Action"
        '
        'btnChangeExample
        '
        Me.btnChangeExample.BackgroundImage = Global.FileNamer.My.Resources.Resources.folderyellow_92963
        Me.btnChangeExample.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnChangeExample.Location = New System.Drawing.Point(948, 349)
        Me.btnChangeExample.Name = "btnChangeExample"
        Me.btnChangeExample.Size = New System.Drawing.Size(28, 25)
        Me.btnChangeExample.TabIndex = 26
        Me.btnChangeExample.UseVisualStyleBackColor = True
        Me.btnChangeExample.Visible = False
        '
        'lblExtras
        '
        Me.lblExtras.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblExtras.Location = New System.Drawing.Point(684, 352)
        Me.lblExtras.Name = "lblExtras"
        Me.lblExtras.Size = New System.Drawing.Size(265, 19)
        Me.lblExtras.TabIndex = 27
        Me.lblExtras.Visible = False
        '
        'lblExample
        '
        Me.lblExample.AutoSize = True
        Me.lblExample.Location = New System.Drawing.Point(628, 355)
        Me.lblExample.Name = "lblExample"
        Me.lblExample.Size = New System.Drawing.Size(50, 13)
        Me.lblExample.TabIndex = 28
        Me.lblExample.Text = "Example:"
        Me.lblExample.Visible = False
        '
        'lblProcessingText
        '
        Me.lblProcessingText.AutoSize = True
        Me.lblProcessingText.Location = New System.Drawing.Point(750, 459)
        Me.lblProcessingText.Name = "lblProcessingText"
        Me.lblProcessingText.Size = New System.Drawing.Size(81, 13)
        Me.lblProcessingText.TabIndex = 29
        Me.lblProcessingText.Text = "Processing File:"
        Me.lblProcessingText.Visible = False
        '
        'lblCurrentItemText
        '
        Me.lblCurrentItemText.AutoSize = True
        Me.lblCurrentItemText.Location = New System.Drawing.Point(837, 459)
        Me.lblCurrentItemText.Name = "lblCurrentItemText"
        Me.lblCurrentItemText.Size = New System.Drawing.Size(23, 13)
        Me.lblCurrentItemText.TabIndex = 30
        Me.lblCurrentItemText.Text = "first"
        Me.lblCurrentItemText.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblCurrentItemText.Visible = False
        '
        'lblTotalItemsText
        '
        Me.lblTotalItemsText.AutoSize = True
        Me.lblTotalItemsText.Location = New System.Drawing.Point(885, 459)
        Me.lblTotalItemsText.Name = "lblTotalItemsText"
        Me.lblTotalItemsText.Size = New System.Drawing.Size(27, 13)
        Me.lblTotalItemsText.TabIndex = 31
        Me.lblTotalItemsText.Text = "total"
        Me.lblTotalItemsText.Visible = False
        '
        'lblProcessingOfText
        '
        Me.lblProcessingOfText.AutoSize = True
        Me.lblProcessingOfText.Location = New System.Drawing.Point(866, 459)
        Me.lblProcessingOfText.Name = "lblProcessingOfText"
        Me.lblProcessingOfText.Size = New System.Drawing.Size(16, 13)
        Me.lblProcessingOfText.TabIndex = 32
        Me.lblProcessingOfText.Text = "of"
        Me.lblProcessingOfText.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(927, 441)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(168, 49)
        Me.btnCancel.TabIndex = 33
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 502)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblProcessingOfText)
        Me.Controls.Add(Me.lblTotalItemsText)
        Me.Controls.Add(Me.lblCurrentItemText)
        Me.Controls.Add(Me.lblProcessingText)
        Me.Controls.Add(Me.lblExample)
        Me.Controls.Add(Me.lblExtras)
        Me.Controls.Add(Me.btnChangeExample)
        Me.Controls.Add(Me.lblAction)
        Me.Controls.Add(Me.lblNewFolder)
        Me.Controls.Add(Me.lblOriginalFolder)
        Me.Controls.Add(Me.btnNewFolder)
        Me.Controls.Add(Me.cmbOperation)
        Me.Controls.Add(Me.cbSelectAll)
        Me.Controls.Add(Me.cbTV)
        Me.Controls.Add(Me.cbMovies)
        Me.Controls.Add(Me.cbMakeChanges)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.lblOriginal)
        Me.Controls.Add(Me.btnOriginalFolder)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.lbNew)
        Me.Controls.Add(Me.lbOriginal)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "File Namer"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lbOriginal As ListBox
    Friend WithEvents lbNew As ListBox
    Friend WithEvents btnProcess As Button
    Friend WithEvents btnOriginalFolder As Button
    Friend WithEvents lblOriginal As Label
    Friend WithEvents lblNew As Label
    Friend WithEvents cbMakeChanges As CheckBox
    Friend WithEvents cbMovies As CheckBox
    Friend WithEvents cbTV As CheckBox
    Friend WithEvents cbSelectAll As CheckBox
    Friend WithEvents cmbOperation As ComboBox
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents UseDefaultMovieFoldersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UseDefaultTVFoldersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnNewFolder As Button
    Friend WithEvents lblOriginalFolder As Label
    Friend WithEvents lblNewFolder As Label
    Friend WithEvents lblAction As Label
    Friend WithEvents btnChangeExample As Button
    Friend WithEvents lblExtras As Label
    Friend WithEvents lblExample As Label
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents lblProcessingText As Label
    Friend WithEvents lblCurrentItemText As Label
    Friend WithEvents lblTotalItemsText As Label
    Friend WithEvents lblProcessingOfText As Label
    Friend WithEvents btnCancel As Button
End Class
