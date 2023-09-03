<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Select", 0)
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Insert", 1)
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Update", 2)
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Delete", 3)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.FileNewMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.FileOpenMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.FileSaveMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.FileSetupDatabaseMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.FileExitMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.EditMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.EditCutMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.EditCopyMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.EditPasteMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator
        Me.EditFontMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbNew = New System.Windows.Forms.ToolStripButton
        Me.tsbOpen = New System.Windows.Forms.ToolStripButton
        Me.tsbSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbCut = New System.Windows.Forms.ToolStripButton
        Me.tsbCopy = New System.Windows.Forms.ToolStripButton
        Me.tsbPaste = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.lblServerConf = New System.Windows.Forms.ToolStripLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.imgMainMenu = New System.Windows.Forms.ImageList(Me.components)
        Me.btnMain = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rtbContent = New System.Windows.Forms.RichTextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboTableName = New System.Windows.Forms.ComboBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.EditMenu})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(687, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileMenu
        '
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileNewMenu, Me.FileOpenMenu, Me.FileSaveMenu, Me.ToolStripMenuItem1, Me.FileSetupDatabaseMenu, Me.ToolStripMenuItem3, Me.FileExitMenu})
        Me.FileMenu.Name = "FileMenu"
        Me.FileMenu.Size = New System.Drawing.Size(35, 20)
        Me.FileMenu.Text = "&File"
        '
        'FileNewMenu
        '
        Me.FileNewMenu.Name = "FileNewMenu"
        Me.FileNewMenu.Size = New System.Drawing.Size(162, 22)
        Me.FileNewMenu.Text = "&New"
        '
        'FileOpenMenu
        '
        Me.FileOpenMenu.Name = "FileOpenMenu"
        Me.FileOpenMenu.Size = New System.Drawing.Size(162, 22)
        Me.FileOpenMenu.Text = "&Open"
        '
        'FileSaveMenu
        '
        Me.FileSaveMenu.Name = "FileSaveMenu"
        Me.FileSaveMenu.Size = New System.Drawing.Size(162, 22)
        Me.FileSaveMenu.Text = "&Save"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(159, 6)
        '
        'FileSetupDatabaseMenu
        '
        Me.FileSetupDatabaseMenu.Name = "FileSetupDatabaseMenu"
        Me.FileSetupDatabaseMenu.Size = New System.Drawing.Size(162, 22)
        Me.FileSetupDatabaseMenu.Text = "Setup Database"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(159, 6)
        '
        'FileExitMenu
        '
        Me.FileExitMenu.Name = "FileExitMenu"
        Me.FileExitMenu.Size = New System.Drawing.Size(162, 22)
        Me.FileExitMenu.Text = "E&xit"
        '
        'EditMenu
        '
        Me.EditMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditCutMenu, Me.EditCopyMenu, Me.EditPasteMenu, Me.ToolStripMenuItem6, Me.EditFontMenu})
        Me.EditMenu.Name = "EditMenu"
        Me.EditMenu.Size = New System.Drawing.Size(37, 20)
        Me.EditMenu.Text = "&Edit"
        '
        'EditCutMenu
        '
        Me.EditCutMenu.Name = "EditCutMenu"
        Me.EditCutMenu.Size = New System.Drawing.Size(112, 22)
        Me.EditCutMenu.Text = "&Cut"
        '
        'EditCopyMenu
        '
        Me.EditCopyMenu.Name = "EditCopyMenu"
        Me.EditCopyMenu.Size = New System.Drawing.Size(112, 22)
        Me.EditCopyMenu.Text = "C&opy"
        '
        'EditPasteMenu
        '
        Me.EditPasteMenu.Name = "EditPasteMenu"
        Me.EditPasteMenu.Size = New System.Drawing.Size(112, 22)
        Me.EditPasteMenu.Text = "&Paste"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(109, 6)
        '
        'EditFontMenu
        '
        Me.EditFontMenu.Name = "EditFontMenu"
        Me.EditFontMenu.Size = New System.Drawing.Size(112, 22)
        Me.EditFontMenu.Text = "&Font"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNew, Me.tsbOpen, Me.tsbSave, Me.ToolStripSeparator1, Me.tsbCut, Me.tsbCopy, Me.tsbPaste, Me.ToolStripSeparator2, Me.lblServerConf})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(687, 29)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbNew
        '
        Me.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbNew.Image = Global.GenerateSQL.My.Resources.Resources._New
        Me.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNew.Name = "tsbNew"
        Me.tsbNew.Size = New System.Drawing.Size(23, 26)
        Me.tsbNew.Text = "tsbNew"
        '
        'tsbOpen
        '
        Me.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOpen.Image = Global.GenerateSQL.My.Resources.Resources.Rule1
        Me.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpen.Name = "tsbOpen"
        Me.tsbOpen.Size = New System.Drawing.Size(23, 26)
        Me.tsbOpen.Text = "tsbOpen"
        '
        'tsbSave
        '
        Me.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSave.Image = Global.GenerateSQL.My.Resources.Resources.Save
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(23, 26)
        Me.tsbSave.Text = "tsbSave"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 29)
        '
        'tsbCut
        '
        Me.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbCut.Image = Global.GenerateSQL.My.Resources.Resources.iCut
        Me.tsbCut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCut.Name = "tsbCut"
        Me.tsbCut.Size = New System.Drawing.Size(26, 26)
        Me.tsbCut.Text = "ToolStripButton4"
        '
        'tsbCopy
        '
        Me.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbCopy.Image = Global.GenerateSQL.My.Resources.Resources.iCopy
        Me.tsbCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCopy.Name = "tsbCopy"
        Me.tsbCopy.Size = New System.Drawing.Size(27, 26)
        Me.tsbCopy.Text = "ToolStripButton5"
        '
        'tsbPaste
        '
        Me.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPaste.Image = Global.GenerateSQL.My.Resources.Resources.iPaste
        Me.tsbPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPaste.Name = "tsbPaste"
        Me.tsbPaste.Size = New System.Drawing.Size(27, 26)
        Me.tsbPaste.Text = "ToolStripButton6"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'lblServerConf
        '
        Me.lblServerConf.Name = "lblServerConf"
        Me.lblServerConf.Size = New System.Drawing.Size(106, 26)
        Me.lblServerConf.Text = "DB:Server-Database"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 53)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnMain)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(687, 387)
        Me.SplitContainer1.SplitterDistance = 126
        Me.SplitContainer1.TabIndex = 2
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8})
        Me.ListView1.LabelWrap = False
        Me.ListView1.LargeImageList = Me.imgMainMenu
        Me.ListView1.Location = New System.Drawing.Point(0, 23)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(126, 364)
        Me.ListView1.SmallImageList = Me.imgMainMenu
        Me.ListView1.TabIndex = 1
        Me.ListView1.TileSize = New System.Drawing.Size(180, 34)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Tile
        '
        'imgMainMenu
        '
        Me.imgMainMenu.ImageStream = CType(resources.GetObject("imgMainMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMainMenu.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMainMenu.Images.SetKeyName(0, "Grayed-Table.gif")
        Me.imgMainMenu.Images.SetKeyName(1, "New.gif")
        Me.imgMainMenu.Images.SetKeyName(2, "edit.gif")
        Me.imgMainMenu.Images.SetKeyName(3, "delete1.gif")
        Me.imgMainMenu.Images.SetKeyName(4, "iPaste.gif")
        Me.imgMainMenu.Images.SetKeyName(5, "iCopy.gif")
        '
        'btnMain
        '
        Me.btnMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnMain.Location = New System.Drawing.Point(0, 0)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(126, 23)
        Me.btnMain.TabIndex = 0
        Me.btnMain.Text = "Main Menu"
        Me.btnMain.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rtbContent)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 31)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(557, 334)
        Me.Panel2.TabIndex = 3
        '
        'rtbContent
        '
        Me.rtbContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbContent.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbContent.Location = New System.Drawing.Point(0, 0)
        Me.rtbContent.Name = "rtbContent"
        Me.rtbContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbContent.Size = New System.Drawing.Size(557, 334)
        Me.rtbContent.TabIndex = 1
        Me.rtbContent.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboTableName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(557, 31)
        Me.Panel1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Table Name"
        '
        'cboTableName
        '
        Me.cboTableName.FormattingEnabled = True
        Me.cboTableName.Location = New System.Drawing.Point(78, 4)
        Me.cboTableName.Name = "cboTableName"
        Me.cboTableName.Size = New System.Drawing.Size(189, 21)
        Me.cboTableName.TabIndex = 2
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 365)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(557, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 440)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate SQL.NET"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileNewMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileOpenMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileSaveMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FileExitMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents btnMain As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents imgMainMenu As System.Windows.Forms.ImageList
    Friend WithEvents tsbOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblServerConf As System.Windows.Forms.ToolStripLabel
    Friend WithEvents FileSetupDatabaseMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rtbContent As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTableName As System.Windows.Forms.ComboBox
    Friend WithEvents EditMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditFontMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents tsbSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbCut As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCopy As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPaste As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditCutMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditCopyMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditPasteMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator

End Class
