<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UploadExcel
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UploadExcel))
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.UPLOADDATE = New System.Windows.Forms.MaskedTextBox()
        Me.TXTUPLOADNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblgrndate = New System.Windows.Forms.Label()
        Me.DGVSTR = New System.Windows.Forms.DataGridView()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TXTPROJECT = New System.Windows.Forms.TextBox()
        Me.cmdshowdetails = New System.Windows.Forms.Button()
        Me.TXTOTHERCHGSADD = New System.Windows.Forms.TextBox()
        Me.CMBOTHERCHGSCODE = New System.Windows.Forms.ComboBox()
        Me.TXTJOBCARD = New System.Windows.Forms.TextBox()
        Me.CMDEXCELUPLOAD = New System.Windows.Forms.Button()
        Me.LBLDELIVERY = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TXTDRAWINGNO = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.txtadd = New System.Windows.Forms.TextBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CMBNAME = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLEINV = New System.Windows.Forms.ToolStripButton()
        Me.TOOLEWB = New System.Windows.Forms.ToolStripButton()
        Me.tooldelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolprevious = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.toolnext = New System.Windows.Forms.ToolStripButton()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GSRNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GS1NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GITEMNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GTYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GW1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GH1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GW2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GH2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GLENGTH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GGUAGES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GAREA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GC1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GC2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1.SuspendLayout()
        CType(Me.DGVSTR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.UPLOADDATE)
        Me.BlendPanel1.Controls.Add(Me.TXTUPLOADNO)
        Me.BlendPanel1.Controls.Add(Me.Label12)
        Me.BlendPanel1.Controls.Add(Me.lblgrndate)
        Me.BlendPanel1.Controls.Add(Me.DGVSTR)
        Me.BlendPanel1.Controls.Add(Me.txtFilePath)
        Me.BlendPanel1.Controls.Add(Me.Label3)
        Me.BlendPanel1.Controls.Add(Me.TXTPROJECT)
        Me.BlendPanel1.Controls.Add(Me.cmdshowdetails)
        Me.BlendPanel1.Controls.Add(Me.TXTOTHERCHGSADD)
        Me.BlendPanel1.Controls.Add(Me.CMBOTHERCHGSCODE)
        Me.BlendPanel1.Controls.Add(Me.TXTJOBCARD)
        Me.BlendPanel1.Controls.Add(Me.CMDEXCELUPLOAD)
        Me.BlendPanel1.Controls.Add(Me.LBLDELIVERY)
        Me.BlendPanel1.Controls.Add(Me.Label1)
        Me.BlendPanel1.Controls.Add(Me.TXTDRAWINGNO)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.txtadd)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdexit)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.Label6)
        Me.BlendPanel1.Controls.Add(Me.CMBNAME)
        Me.BlendPanel1.Controls.Add(Me.Label2)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 1
        '
        'UPLOADDATE
        '
        Me.UPLOADDATE.AsciiOnly = True
        Me.UPLOADDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.UPLOADDATE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UPLOADDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.UPLOADDATE.Location = New System.Drawing.Point(1046, 97)
        Me.UPLOADDATE.Mask = "00/00/0000"
        Me.UPLOADDATE.Name = "UPLOADDATE"
        Me.UPLOADDATE.Size = New System.Drawing.Size(82, 23)
        Me.UPLOADDATE.TabIndex = 909
        Me.UPLOADDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.UPLOADDATE.ValidatingType = GetType(Date)
        '
        'TXTUPLOADNO
        '
        Me.TXTUPLOADNO.BackColor = System.Drawing.Color.Linen
        Me.TXTUPLOADNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTUPLOADNO.Location = New System.Drawing.Point(1046, 68)
        Me.TXTUPLOADNO.Name = "TXTUPLOADNO"
        Me.TXTUPLOADNO.ReadOnly = True
        Me.TXTUPLOADNO.Size = New System.Drawing.Size(82, 23)
        Me.TXTUPLOADNO.TabIndex = 911
        Me.TXTUPLOADNO.TabStop = False
        Me.TXTUPLOADNO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(1006, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 15)
        Me.Label12.TabIndex = 912
        Me.Label12.Text = "Sr. No"
        '
        'lblgrndate
        '
        Me.lblgrndate.AutoSize = True
        Me.lblgrndate.BackColor = System.Drawing.Color.Transparent
        Me.lblgrndate.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgrndate.ForeColor = System.Drawing.Color.Black
        Me.lblgrndate.Location = New System.Drawing.Point(1012, 101)
        Me.lblgrndate.Name = "lblgrndate"
        Me.lblgrndate.Size = New System.Drawing.Size(32, 15)
        Me.lblgrndate.TabIndex = 910
        Me.lblgrndate.Text = "Date"
        '
        'DGVSTR
        '
        Me.DGVSTR.AllowUserToAddRows = False
        Me.DGVSTR.AllowUserToDeleteRows = False
        Me.DGVSTR.AllowUserToResizeColumns = False
        Me.DGVSTR.AllowUserToResizeRows = False
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White
        Me.DGVSTR.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DGVSTR.BackgroundColor = System.Drawing.Color.White
        Me.DGVSTR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGVSTR.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVSTR.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DGVSTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVSTR.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GSRNO, Me.GS1NO, Me.GITEMNO, Me.GTYPE, Me.GW1, Me.GH1, Me.GW2, Me.GH2, Me.GLENGTH, Me.GQTY, Me.GGUAGES, Me.GAREA, Me.GC1, Me.GC2})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVSTR.DefaultCellStyle = DataGridViewCellStyle15
        Me.DGVSTR.GridColor = System.Drawing.SystemColors.ControlText
        Me.DGVSTR.Location = New System.Drawing.Point(11, 177)
        Me.DGVSTR.Margin = New System.Windows.Forms.Padding(2)
        Me.DGVSTR.MultiSelect = False
        Me.DGVSTR.Name = "DGVSTR"
        Me.DGVSTR.ReadOnly = True
        Me.DGVSTR.RowHeadersVisible = False
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.Black
        Me.DGVSTR.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.DGVSTR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVSTR.Size = New System.Drawing.Size(1161, 202)
        Me.DGVSTR.TabIndex = 908
        Me.DGVSTR.TabStop = False
        '
        'txtFilePath
        '
        Me.txtFilePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFilePath.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(1015, 409)
        Me.txtFilePath.MaxLength = 50
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(30, 23)
        Me.txtFilePath.TabIndex = 19
        Me.txtFilePath.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(52, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 844
        Me.Label3.Text = "Project"
        '
        'TXTPROJECT
        '
        Me.TXTPROJECT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTPROJECT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTPROJECT.Location = New System.Drawing.Point(112, 115)
        Me.TXTPROJECT.MaxLength = 50
        Me.TXTPROJECT.Name = "TXTPROJECT"
        Me.TXTPROJECT.Size = New System.Drawing.Size(243, 23)
        Me.TXTPROJECT.TabIndex = 4
        Me.TXTPROJECT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdshowdetails
        '
        Me.cmdshowdetails.BackColor = System.Drawing.Color.Transparent
        Me.cmdshowdetails.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdshowdetails.FlatAppearance.BorderSize = 0
        Me.cmdshowdetails.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdshowdetails.ForeColor = System.Drawing.Color.Black
        Me.cmdshowdetails.Location = New System.Drawing.Point(603, 454)
        Me.cmdshowdetails.Name = "cmdshowdetails"
        Me.cmdshowdetails.Size = New System.Drawing.Size(100, 28)
        Me.cmdshowdetails.TabIndex = 34
        Me.cmdshowdetails.Text = "S&how Details"
        Me.cmdshowdetails.UseVisualStyleBackColor = False
        Me.cmdshowdetails.Visible = False
        '
        'TXTOTHERCHGSADD
        '
        Me.TXTOTHERCHGSADD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTOTHERCHGSADD.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTOTHERCHGSADD.Location = New System.Drawing.Point(388, 28)
        Me.TXTOTHERCHGSADD.Name = "TXTOTHERCHGSADD"
        Me.TXTOTHERCHGSADD.Size = New System.Drawing.Size(29, 21)
        Me.TXTOTHERCHGSADD.TabIndex = 869
        Me.TXTOTHERCHGSADD.TabStop = False
        Me.TXTOTHERCHGSADD.Visible = False
        '
        'CMBOTHERCHGSCODE
        '
        Me.CMBOTHERCHGSCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBOTHERCHGSCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBOTHERCHGSCODE.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBOTHERCHGSCODE.FormattingEnabled = True
        Me.CMBOTHERCHGSCODE.Items.AddRange(New Object() {""})
        Me.CMBOTHERCHGSCODE.Location = New System.Drawing.Point(350, 27)
        Me.CMBOTHERCHGSCODE.Name = "CMBOTHERCHGSCODE"
        Me.CMBOTHERCHGSCODE.Size = New System.Drawing.Size(32, 22)
        Me.CMBOTHERCHGSCODE.TabIndex = 868
        Me.CMBOTHERCHGSCODE.Visible = False
        '
        'TXTJOBCARD
        '
        Me.TXTJOBCARD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTJOBCARD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTJOBCARD.Location = New System.Drawing.Point(481, 86)
        Me.TXTJOBCARD.MaxLength = 50
        Me.TXTJOBCARD.Name = "TXTJOBCARD"
        Me.TXTJOBCARD.Size = New System.Drawing.Size(190, 23)
        Me.TXTJOBCARD.TabIndex = 10
        '
        'CMDEXCELUPLOAD
        '
        Me.CMDEXCELUPLOAD.BackColor = System.Drawing.Color.Transparent
        Me.CMDEXCELUPLOAD.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDEXCELUPLOAD.FlatAppearance.BorderSize = 0
        Me.CMDEXCELUPLOAD.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDEXCELUPLOAD.ForeColor = System.Drawing.Color.Black
        Me.CMDEXCELUPLOAD.Location = New System.Drawing.Point(497, 454)
        Me.CMDEXCELUPLOAD.Name = "CMDEXCELUPLOAD"
        Me.CMDEXCELUPLOAD.Size = New System.Drawing.Size(100, 28)
        Me.CMDEXCELUPLOAD.TabIndex = 33
        Me.CMDEXCELUPLOAD.TabStop = False
        Me.CMDEXCELUPLOAD.Text = "&Upload"
        Me.CMDEXCELUPLOAD.UseVisualStyleBackColor = False
        '
        'LBLDELIVERY
        '
        Me.LBLDELIVERY.BackColor = System.Drawing.Color.Transparent
        Me.LBLDELIVERY.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLDELIVERY.ForeColor = System.Drawing.Color.Black
        Me.LBLDELIVERY.Location = New System.Drawing.Point(379, 90)
        Me.LBLDELIVERY.Name = "LBLDELIVERY"
        Me.LBLDELIVERY.Size = New System.Drawing.Size(98, 15)
        Me.LBLDELIVERY.TabIndex = 634
        Me.LBLDELIVERY.Text = "Jobcard"
        Me.LBLDELIVERY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(404, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 15)
        Me.Label1.TabIndex = 632
        Me.Label1.Text = "Drawing No"
        '
        'TXTDRAWINGNO
        '
        Me.TXTDRAWINGNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TXTDRAWINGNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTDRAWINGNO.Location = New System.Drawing.Point(481, 115)
        Me.TXTDRAWINGNO.MaxLength = 50
        Me.TXTDRAWINGNO.Name = "TXTDRAWINGNO"
        Me.TXTDRAWINGNO.Size = New System.Drawing.Size(82, 23)
        Me.TXTDRAWINGNO.TabIndex = 3
        '
        'CMBCODE
        '
        Me.CMBCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBCODE.BackColor = System.Drawing.Color.White
        Me.CMBCODE.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(239, 30)
        Me.CMBCODE.MaxDropDownItems = 14
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(34, 23)
        Me.CMBCODE.TabIndex = 2
        Me.CMBCODE.Visible = False
        '
        'txtadd
        '
        Me.txtadd.BackColor = System.Drawing.Color.White
        Me.txtadd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd.ForeColor = System.Drawing.Color.DimGray
        Me.txtadd.Location = New System.Drawing.Point(325, 30)
        Me.txtadd.Multiline = True
        Me.txtadd.Name = "txtadd"
        Me.txtadd.ReadOnly = True
        Me.txtadd.Size = New System.Drawing.Size(10, 19)
        Me.txtadd.TabIndex = 431
        Me.txtadd.Visible = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(603, 490)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 37
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(517, 490)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 36
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(431, 488)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 35
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.Transparent
        Me.cmdexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdexit.FlatAppearance.BorderSize = 0
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(689, 490)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 28)
        Me.cmdexit.TabIndex = 38
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(292, 2)
        Me.tstxtbillno.MaxLength = 50
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(61, 22)
        Me.tstxtbillno.TabIndex = 33
        Me.tstxtbillno.TabStop = False
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(15, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 15)
        Me.Label6.TabIndex = 434
        Me.Label6.Text = "Customer Name"
        '
        'CMBNAME
        '
        Me.CMBNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBNAME.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMBNAME.FormattingEnabled = True
        Me.CMBNAME.Location = New System.Drawing.Point(112, 86)
        Me.CMBNAME.MaxDropDownItems = 14
        Me.CMBNAME.Name = "CMBNAME"
        Me.CMBNAME.Size = New System.Drawing.Size(243, 23)
        Me.CMBNAME.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(7, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 26)
        Me.Label2.TabIndex = 430
        Me.Label2.Text = "Upload Excel"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.TOOLEINV, Me.TOOLEWB, Me.tooldelete, Me.toolStripSeparator, Me.toolprevious, Me.ToolStripSeparator1, Me.toolnext})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 14
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'TOOLEINV
        '
        Me.TOOLEINV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEINV.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLEINV.Image = CType(resources.GetObject("TOOLEINV.Image"), System.Drawing.Image)
        Me.TOOLEINV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEINV.Name = "TOOLEINV"
        Me.TOOLEINV.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEINV.Text = "Generate E-Invoice"
        '
        'TOOLEWB
        '
        Me.TOOLEWB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEWB.Image = Global.SHAHTRADE.My.Resources.Resources.EWAY_BILL_IMAGE
        Me.TOOLEWB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEWB.Name = "TOOLEWB"
        Me.TOOLEWB.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEWB.Text = "Generate EWB"
        '
        'tooldelete
        '
        Me.tooldelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tooldelete.Image = CType(resources.GetObject("tooldelete.Image"), System.Drawing.Image)
        Me.tooldelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tooldelete.Name = "tooldelete"
        Me.tooldelete.Size = New System.Drawing.Size(23, 22)
        Me.tooldelete.Text = "&Delete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'toolprevious
        '
        Me.toolprevious.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolprevious.Image = Global.SHAHTRADE.My.Resources.Resources.PREVIOUS
        Me.toolprevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolprevious.Name = "toolprevious"
        Me.toolprevious.Size = New System.Drawing.Size(73, 22)
        Me.toolprevious.Text = "Previous"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'toolnext
        '
        Me.toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolnext.Image = Global.SHAHTRADE.My.Resources.Resources._NEXT
        Me.toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolnext.Name = "toolnext"
        Me.toolnext.Size = New System.Drawing.Size(51, 22)
        Me.toolnext.Text = "Next"
        Me.toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GSRNO
        '
        Me.GSRNO.HeaderText = "Sr."
        Me.GSRNO.Name = "GSRNO"
        Me.GSRNO.ReadOnly = True
        Me.GSRNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GSRNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GSRNO.Width = 40
        '
        'GS1NO
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GS1NO.DefaultCellStyle = DataGridViewCellStyle11
        Me.GS1NO.HeaderText = "S1 No"
        Me.GS1NO.Name = "GS1NO"
        Me.GS1NO.ReadOnly = True
        Me.GS1NO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GS1NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GITEMNO
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GITEMNO.DefaultCellStyle = DataGridViewCellStyle12
        Me.GITEMNO.HeaderText = "Item No"
        Me.GITEMNO.Name = "GITEMNO"
        Me.GITEMNO.ReadOnly = True
        Me.GITEMNO.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GITEMNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GITEMNO.Width = 80
        '
        'GTYPE
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GTYPE.DefaultCellStyle = DataGridViewCellStyle13
        Me.GTYPE.HeaderText = "Type Of Duct"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.ReadOnly = True
        Me.GTYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GTYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GTYPE.Width = 80
        '
        'GW1
        '
        Me.GW1.HeaderText = "W1"
        Me.GW1.Name = "GW1"
        Me.GW1.ReadOnly = True
        Me.GW1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GW1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GW1.Width = 80
        '
        'GH1
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GH1.DefaultCellStyle = DataGridViewCellStyle14
        Me.GH1.HeaderText = "H1"
        Me.GH1.Name = "GH1"
        Me.GH1.ReadOnly = True
        Me.GH1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GH1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GH1.Width = 80
        '
        'GW2
        '
        Me.GW2.HeaderText = "W2"
        Me.GW2.Name = "GW2"
        Me.GW2.ReadOnly = True
        Me.GW2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GW2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GW2.Width = 80
        '
        'GH2
        '
        Me.GH2.HeaderText = "H2"
        Me.GH2.Name = "GH2"
        Me.GH2.ReadOnly = True
        Me.GH2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GH2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GLENGTH
        '
        Me.GLENGTH.HeaderText = "Length in mm"
        Me.GLENGTH.Name = "GLENGTH"
        Me.GLENGTH.ReadOnly = True
        Me.GLENGTH.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GLENGTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GQTY
        '
        Me.GQTY.HeaderText = "Qty"
        Me.GQTY.Name = "GQTY"
        Me.GQTY.ReadOnly = True
        Me.GQTY.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GQTY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GGUAGES
        '
        Me.GGUAGES.HeaderText = "Guage"
        Me.GGUAGES.Name = "GGUAGES"
        Me.GGUAGES.ReadOnly = True
        Me.GGUAGES.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GGUAGES.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GAREA
        '
        Me.GAREA.HeaderText = "Area in sqmtr"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.ReadOnly = True
        Me.GAREA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GAREA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GC1
        '
        Me.GC1.HeaderText = "Connector C1"
        Me.GC1.Name = "GC1"
        Me.GC1.ReadOnly = True
        Me.GC1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GC1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'GC2
        '
        Me.GC2.HeaderText = "Connector C2"
        Me.GC2.Name = "GC2"
        Me.GC2.ReadOnly = True
        Me.GC2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GC2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'UploadExcel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "UploadExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Upload Excel"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.DGVSTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TXTPROJECT As TextBox
    Friend WithEvents cmdshowdetails As Button
    Friend WithEvents TXTOTHERCHGSADD As TextBox
    Friend WithEvents CMBOTHERCHGSCODE As ComboBox
    Friend WithEvents TXTJOBCARD As TextBox
    Friend WithEvents CMDEXCELUPLOAD As Button
    Friend WithEvents LBLDELIVERY As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TXTDRAWINGNO As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents txtadd As TextBox
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdclear As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdexit As Button
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CMBNAME As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLEINV As ToolStripButton
    Friend WithEvents TOOLEWB As ToolStripButton
    Friend WithEvents tooldelete As ToolStripButton
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents toolprevious As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents toolnext As ToolStripButton
    Friend WithEvents DGVSTR As DataGridView
    Friend WithEvents UPLOADDATE As MaskedTextBox
    Friend WithEvents TXTUPLOADNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents lblgrndate As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents GSRNO As DataGridViewTextBoxColumn
    Friend WithEvents GS1NO As DataGridViewTextBoxColumn
    Friend WithEvents GITEMNO As DataGridViewTextBoxColumn
    Friend WithEvents GTYPE As DataGridViewTextBoxColumn
    Friend WithEvents GW1 As DataGridViewTextBoxColumn
    Friend WithEvents GH1 As DataGridViewTextBoxColumn
    Friend WithEvents GW2 As DataGridViewTextBoxColumn
    Friend WithEvents GH2 As DataGridViewTextBoxColumn
    Friend WithEvents GLENGTH As DataGridViewTextBoxColumn
    Friend WithEvents GQTY As DataGridViewTextBoxColumn
    Friend WithEvents GGUAGES As DataGridViewTextBoxColumn
    Friend WithEvents GAREA As DataGridViewTextBoxColumn
    Friend WithEvents GC1 As DataGridViewTextBoxColumn
    Friend WithEvents GC2 As DataGridViewTextBoxColumn
    Friend WithEvents EP As ErrorProvider
End Class
