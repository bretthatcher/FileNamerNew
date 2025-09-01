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
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lbOriginal = New System.Windows.Forms.ListBox()
        Me.lbNew = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.textOriginalFolder = New System.Windows.Forms.TextBox()
        Me.btnOriginalFolder = New System.Windows.Forms.Button()
        Me.lblOriginal = New System.Windows.Forms.Label()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.btnNewFolder = New System.Windows.Forms.Button()
        Me.textNewFolder = New System.Windows.Forms.TextBox()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.SettingsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1107, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(37, 20)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(93, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
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
        Me.lbOriginal.Size = New System.Drawing.Size(510, 238)
        Me.lbOriginal.TabIndex = 1
        '
        'lbNew
        '
        Me.lbNew.FormattingEnabled = True
        Me.lbNew.Location = New System.Drawing.Point(631, 54)
        Me.lbNew.Name = "lbNew"
        Me.lbNew.Size = New System.Drawing.Size(464, 238)
        Me.lbNew.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(89, 372)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 49)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(271, 372)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(161, 49)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(453, 372)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(161, 49)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(631, 372)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(161, 49)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'textOriginalFolder
        '
        Me.textOriginalFolder.BackColor = System.Drawing.SystemColors.Window
        Me.textOriginalFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.textOriginalFolder.Location = New System.Drawing.Point(200, 28)
        Me.textOriginalFolder.Name = "textOriginalFolder"
        Me.textOriginalFolder.Size = New System.Drawing.Size(301, 20)
        Me.textOriginalFolder.TabIndex = 7
        '
        'btnOriginalFolder
        '
        Me.btnOriginalFolder.Location = New System.Drawing.Point(504, 27)
        Me.btnOriginalFolder.Name = "btnOriginalFolder"
        Me.btnOriginalFolder.Size = New System.Drawing.Size(32, 27)
        Me.btnOriginalFolder.TabIndex = 8
        Me.btnOriginalFolder.Text = "Button5"
        Me.btnOriginalFolder.UseVisualStyleBackColor = True
        '
        'lblOriginal
        '
        Me.lblOriginal.AutoSize = True
        Me.lblOriginal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOriginal.Location = New System.Drawing.Point(12, 33)
        Me.lblOriginal.Name = "lblOriginal"
        Me.lblOriginal.Size = New System.Drawing.Size(70, 20)
        Me.lblOriginal.TabIndex = 9
        Me.lblOriginal.Text = "Original"
        '
        'lblNew
        '
        Me.lblNew.AutoSize = True
        Me.lblNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNew.Location = New System.Drawing.Point(634, 33)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(43, 20)
        Me.lblNew.TabIndex = 10
        Me.lblNew.Text = "New"
        '
        'btnNewFolder
        '
        Me.btnNewFolder.Location = New System.Drawing.Point(1068, 27)
        Me.btnNewFolder.Name = "btnNewFolder"
        Me.btnNewFolder.Size = New System.Drawing.Size(32, 27)
        Me.btnNewFolder.TabIndex = 12
        Me.btnNewFolder.Text = "Button6"
        Me.btnNewFolder.UseVisualStyleBackColor = True
        '
        'textNewFolder
        '
        Me.textNewFolder.BackColor = System.Drawing.SystemColors.Window
        Me.textNewFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.textNewFolder.Location = New System.Drawing.Point(764, 28)
        Me.textNewFolder.Name = "textNewFolder"
        Me.textNewFolder.Size = New System.Drawing.Size(301, 20)
        Me.textNewFolder.TabIndex = 11
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1107, 535)
        Me.Controls.Add(Me.btnNewFolder)
        Me.Controls.Add(Me.textNewFolder)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.lblOriginal)
        Me.Controls.Add(Me.btnOriginalFolder)
        Me.Controls.Add(Me.textOriginalFolder)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
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
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents textOriginalFolder As TextBox
    Friend WithEvents btnOriginalFolder As Button
    Friend WithEvents lblOriginal As Label
    Friend WithEvents lblNew As Label
    Friend WithEvents btnNewFolder As Button
    Friend WithEvents textNewFolder As TextBox
End Class
