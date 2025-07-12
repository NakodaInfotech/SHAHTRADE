<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CashEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CashEntry))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LBLSRNO = New System.Windows.Forms.Label()
        Me.ToolPREVIOUS = New System.Windows.Forms.ToolStripButton()
        Me.Toolnext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TXTADD = New System.Windows.Forms.TextBox()
        Me.CMBCODE = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.EP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.LBLTOTALDEBIT = New System.Windows.Forms.Label()
        Me.LBLTOTALCREDIT = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TXTCREDIT = New System.Windows.Forms.TextBox()
        Me.TXTDEBIT = New System.Windows.Forms.TextBox()
        Me.CMBPARTYNAME = New System.Windows.Forms.ComboBox()
        Me.TXTNARRATION = New System.Windows.Forms.TextBox()
        Me.GRIDCASH = New System.Windows.Forms.DataGridView()
        Me.gsrno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GPARTYNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCASHDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GNARRATION = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GDebit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tstxtbillno = New System.Windows.Forms.TextBox()
        Me.TXTSRNO = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.TOOLDELETE = New System.Windows.Forms.ToolStripButton()
        Me.CASHDATE = New System.Windows.Forms.MaskedTextBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.cmdEXIT = New System.Windows.Forms.Button()
        Me.TXTCASHNO = New System.Windows.Forms.TextBox()
        Me.GroupBox5.SuspendLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.GRIDCASH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LBLSRNO
        '
        Me.LBLSRNO.AutoSize = True
        Me.LBLSRNO.BackColor = System.Drawing.Color.Transparent
        Me.LBLSRNO.Location = New System.Drawing.Point(641, 44)
        Me.LBLSRNO.Name = "LBLSRNO"
        Me.LBLSRNO.Size = New System.Drawing.Size(36, 15)
        Me.LBLSRNO.TabIndex = 0
        Me.LBLSRNO.Text = "Sr No"
        '
        'ToolPREVIOUS
        '
        Me.ToolPREVIOUS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolPREVIOUS.Image = CType(resources.GetObject("ToolPREVIOUS.Image"), System.Drawing.Image)
        Me.ToolPREVIOUS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolPREVIOUS.Name = "ToolPREVIOUS"
        Me.ToolPREVIOUS.Size = New System.Drawing.Size(73, 22)
        Me.ToolPREVIOUS.Text = "Previous"
        '
        'Toolnext
        '
        Me.Toolnext.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Toolnext.Image = CType(resources.GetObject("Toolnext.Image"), System.Drawing.Image)
        Me.Toolnext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Toolnext.Name = "Toolnext"
        Me.Toolnext.Size = New System.Drawing.Size(51, 22)
        Me.Toolnext.Text = "Next"
        Me.Toolnext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'TXTADD
        '
        Me.TXTADD.Location = New System.Drawing.Point(336, 28)
        Me.TXTADD.MaxLength = 100
        Me.TXTADD.Name = "TXTADD"
        Me.TXTADD.Size = New System.Drawing.Size(75, 23)
        Me.TXTADD.TabIndex = 14
        Me.TXTADD.Visible = False
        '
        'CMBCODE
        '
        Me.CMBCODE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBCODE.FormattingEnabled = True
        Me.CMBCODE.Location = New System.Drawing.Point(255, 28)
        Me.CMBCODE.MaxLength = 100
        Me.CMBCODE.Name = "CMBCODE"
        Me.CMBCODE.Size = New System.Drawing.Size(75, 23)
        Me.CMBCODE.TabIndex = 13
        Me.CMBCODE.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtremarks)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(25, 415)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(236, 107)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Remarks"
        '
        'txtremarks
        '
        Me.txtremarks.ForeColor = System.Drawing.Color.DimGray
        Me.txtremarks.Location = New System.Drawing.Point(5, 16)
        Me.txtremarks.MaxLength = 200
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(225, 85)
        Me.txtremarks.TabIndex = 0
        Me.txtremarks.TabStop = False
        '
        'cmdclear
        '
        Me.cmdclear.BackColor = System.Drawing.Color.Transparent
        Me.cmdclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdclear.FlatAppearance.BorderSize = 0
        Me.cmdclear.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.ForeColor = System.Drawing.Color.Black
        Me.cmdclear.Location = New System.Drawing.Point(444, 462)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(80, 28)
        Me.cmdclear.TabIndex = 10
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'EP
        '
        Me.EP.BlinkRate = 0
        Me.EP.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.EP.ContainerControl = Me
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALDEBIT)
        Me.BlendPanel1.Controls.Add(Me.LBLTOTALCREDIT)
        Me.BlendPanel1.Controls.Add(Me.Label10)
        Me.BlendPanel1.Controls.Add(Me.TXTCREDIT)
        Me.BlendPanel1.Controls.Add(Me.TXTDEBIT)
        Me.BlendPanel1.Controls.Add(Me.CMBPARTYNAME)
        Me.BlendPanel1.Controls.Add(Me.TXTNARRATION)
        Me.BlendPanel1.Controls.Add(Me.TXTADD)
        Me.BlendPanel1.Controls.Add(Me.CMBCODE)
        Me.BlendPanel1.Controls.Add(Me.GRIDCASH)
        Me.BlendPanel1.Controls.Add(Me.GroupBox5)
        Me.BlendPanel1.Controls.Add(Me.tstxtbillno)
        Me.BlendPanel1.Controls.Add(Me.TXTSRNO)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CASHDATE)
        Me.BlendPanel1.Controls.Add(Me.cmddelete)
        Me.BlendPanel1.Controls.Add(Me.cmdok)
        Me.BlendPanel1.Controls.Add(Me.cmdclear)
        Me.BlendPanel1.Controls.Add(Me.cmdEXIT)
        Me.BlendPanel1.Controls.Add(Me.TXTCASHNO)
        Me.BlendPanel1.Controls.Add(Me.LBLSRNO)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(909, 541)
        Me.BlendPanel1.TabIndex = 1
        '
        'LBLTOTALDEBIT
        '
        Me.LBLTOTALDEBIT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALDEBIT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALDEBIT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALDEBIT.Location = New System.Drawing.Point(534, 401)
        Me.LBLTOTALDEBIT.Name = "LBLTOTALDEBIT"
        Me.LBLTOTALDEBIT.Size = New System.Drawing.Size(80, 14)
        Me.LBLTOTALDEBIT.TabIndex = 688
        Me.LBLTOTALDEBIT.Text = "0"
        Me.LBLTOTALDEBIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBLTOTALCREDIT
        '
        Me.LBLTOTALCREDIT.BackColor = System.Drawing.Color.Transparent
        Me.LBLTOTALCREDIT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTOTALCREDIT.ForeColor = System.Drawing.Color.Black
        Me.LBLTOTALCREDIT.Location = New System.Drawing.Point(614, 401)
        Me.LBLTOTALCREDIT.Name = "LBLTOTALCREDIT"
        Me.LBLTOTALCREDIT.Size = New System.Drawing.Size(80, 14)
        Me.LBLTOTALCREDIT.TabIndex = 687
        Me.LBLTOTALCREDIT.Text = "0"
        Me.LBLTOTALCREDIT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(503, 401)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 14)
        Me.Label10.TabIndex = 684
        Me.Label10.Text = "Total"
        '
        'TXTCREDIT
        '
        Me.TXTCREDIT.Location = New System.Drawing.Point(614, 89)
        Me.TXTCREDIT.MaxLength = 100
        Me.TXTCREDIT.Name = "TXTCREDIT"
        Me.TXTCREDIT.Size = New System.Drawing.Size(80, 23)
        Me.TXTCREDIT.TabIndex = 7
        Me.TXTCREDIT.Text = "0.00"
        Me.TXTCREDIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTDEBIT
        '
        Me.TXTDEBIT.Location = New System.Drawing.Point(534, 89)
        Me.TXTDEBIT.MaxLength = 100
        Me.TXTDEBIT.Name = "TXTDEBIT"
        Me.TXTDEBIT.Size = New System.Drawing.Size(80, 23)
        Me.TXTDEBIT.TabIndex = 6
        Me.TXTDEBIT.Text = "0.00"
        Me.TXTDEBIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CMBPARTYNAME
        '
        Me.CMBPARTYNAME.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CMBPARTYNAME.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CMBPARTYNAME.BackColor = System.Drawing.Color.LemonChiffon
        Me.CMBPARTYNAME.FormattingEnabled = True
        Me.CMBPARTYNAME.Location = New System.Drawing.Point(46, 89)
        Me.CMBPARTYNAME.MaxLength = 100
        Me.CMBPARTYNAME.Name = "CMBPARTYNAME"
        Me.CMBPARTYNAME.Size = New System.Drawing.Size(200, 23)
        Me.CMBPARTYNAME.TabIndex = 3
        '
        'TXTNARRATION
        '
        Me.TXTNARRATION.Location = New System.Drawing.Point(334, 89)
        Me.TXTNARRATION.MaxLength = 200
        Me.TXTNARRATION.Name = "TXTNARRATION"
        Me.TXTNARRATION.Size = New System.Drawing.Size(200, 23)
        Me.TXTNARRATION.TabIndex = 5
        '
        'GRIDCASH
        '
        Me.GRIDCASH.AllowUserToAddRows = False
        Me.GRIDCASH.AllowUserToDeleteRows = False
        Me.GRIDCASH.AllowUserToResizeColumns = False
        Me.GRIDCASH.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(248, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black
        Me.GRIDCASH.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GRIDCASH.BackgroundColor = System.Drawing.Color.White
        Me.GRIDCASH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GRIDCASH.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.GRIDCASH.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.GRIDCASH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GRIDCASH.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.gsrno, Me.GPARTYNAME, Me.GCASHDATE, Me.GNARRATION, Me.GDebit, Me.GCredit})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCASH.DefaultCellStyle = DataGridViewCellStyle11
        Me.GRIDCASH.GridColor = System.Drawing.SystemColors.Control
        Me.GRIDCASH.Location = New System.Drawing.Point(11, 112)
        Me.GRIDCASH.MultiSelect = False
        Me.GRIDCASH.Name = "GRIDCASH"
        Me.GRIDCASH.RowHeadersVisible = False
        Me.GRIDCASH.RowHeadersWidth = 30
        Me.GRIDCASH.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White
        Me.GRIDCASH.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.GRIDCASH.RowTemplate.Height = 20
        Me.GRIDCASH.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GRIDCASH.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GRIDCASH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GRIDCASH.Size = New System.Drawing.Size(720, 286)
        Me.GRIDCASH.TabIndex = 8
        Me.GRIDCASH.TabStop = False
        '
        'gsrno
        '
        Me.gsrno.HeaderText = "Sr."
        Me.gsrno.Name = "gsrno"
        Me.gsrno.ReadOnly = True
        Me.gsrno.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gsrno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.gsrno.Width = 35
        '
        'GPARTYNAME
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GPARTYNAME.DefaultCellStyle = DataGridViewCellStyle9
        Me.GPARTYNAME.HeaderText = "Name"
        Me.GPARTYNAME.Name = "GPARTYNAME"
        Me.GPARTYNAME.ReadOnly = True
        Me.GPARTYNAME.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GPARTYNAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GPARTYNAME.Width = 200
        '
        'GCASHDATE
        '
        Me.GCASHDATE.HeaderText = "Date"
        Me.GCASHDATE.Name = "GCASHDATE"
        Me.GCASHDATE.ReadOnly = True
        Me.GCASHDATE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCASHDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCASHDATE.Width = 88
        '
        'GNARRATION
        '
        Me.GNARRATION.HeaderText = "Narration"
        Me.GNARRATION.Name = "GNARRATION"
        Me.GNARRATION.ReadOnly = True
        Me.GNARRATION.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GNARRATION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GNARRATION.Width = 200
        '
        'GDebit
        '
        Me.GDebit.HeaderText = "Debit"
        Me.GDebit.Name = "GDebit"
        Me.GDebit.ReadOnly = True
        Me.GDebit.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GDebit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GDebit.Width = 80
        '
        'GCredit
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.GCredit.DefaultCellStyle = DataGridViewCellStyle10
        Me.GCredit.HeaderText = "Credit"
        Me.GCredit.Name = "GCredit"
        Me.GCredit.ReadOnly = True
        Me.GCredit.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GCredit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GCredit.Width = 80
        '
        'tstxtbillno
        '
        Me.tstxtbillno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.tstxtbillno.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstxtbillno.Location = New System.Drawing.Point(243, 1)
        Me.tstxtbillno.Name = "tstxtbillno"
        Me.tstxtbillno.Size = New System.Drawing.Size(61, 22)
        Me.tstxtbillno.TabIndex = 15
        Me.tstxtbillno.TabStop = False
        Me.tstxtbillno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TXTSRNO
        '
        Me.TXTSRNO.BackColor = System.Drawing.Color.Linen
        Me.TXTSRNO.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTSRNO.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TXTSRNO.Location = New System.Drawing.Point(11, 89)
        Me.TXTSRNO.Name = "TXTSRNO"
        Me.TXTSRNO.ReadOnly = True
        Me.TXTSRNO.Size = New System.Drawing.Size(35, 23)
        Me.TXTSRNO.TabIndex = 2
        Me.TXTSRNO.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.TOOLDELETE, Me.ToolStripSeparator2, Me.ToolPREVIOUS, Me.Toolnext, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(909, 25)
        Me.ToolStrip1.TabIndex = 16
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
        'TOOLDELETE
        '
        Me.TOOLDELETE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLDELETE.Image = CType(resources.GetObject("TOOLDELETE.Image"), System.Drawing.Image)
        Me.TOOLDELETE.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLDELETE.Name = "TOOLDELETE"
        Me.TOOLDELETE.Size = New System.Drawing.Size(23, 22)
        Me.TOOLDELETE.Text = "&Delete"
        '
        'CASHDATE
        '
        Me.CASHDATE.AsciiOnly = True
        Me.CASHDATE.BackColor = System.Drawing.Color.LemonChiffon
        Me.CASHDATE.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.CASHDATE.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.CASHDATE.Location = New System.Drawing.Point(246, 89)
        Me.CASHDATE.Mask = "00/00/0000"
        Me.CASHDATE.Name = "CASHDATE"
        Me.CASHDATE.Size = New System.Drawing.Size(88, 23)
        Me.CASHDATE.TabIndex = 4
        Me.CASHDATE.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals
        Me.CASHDATE.ValidatingType = GetType(Date)
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.Transparent
        Me.cmddelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmddelete.FlatAppearance.BorderSize = 0
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(530, 462)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 28)
        Me.cmddelete.TabIndex = 11
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.Transparent
        Me.cmdok.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdok.FlatAppearance.BorderSize = 0
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(358, 462)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 28)
        Me.cmdok.TabIndex = 9
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdEXIT
        '
        Me.cmdEXIT.BackColor = System.Drawing.Color.Transparent
        Me.cmdEXIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdEXIT.FlatAppearance.BorderSize = 0
        Me.cmdEXIT.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEXIT.ForeColor = System.Drawing.Color.Black
        Me.cmdEXIT.Location = New System.Drawing.Point(616, 462)
        Me.cmdEXIT.Name = "cmdEXIT"
        Me.cmdEXIT.Size = New System.Drawing.Size(80, 28)
        Me.cmdEXIT.TabIndex = 12
        Me.cmdEXIT.Text = "E&xit"
        Me.cmdEXIT.UseVisualStyleBackColor = False
        '
        'TXTCASHNO
        '
        Me.TXTCASHNO.BackColor = System.Drawing.Color.Linen
        Me.TXTCASHNO.Location = New System.Drawing.Point(680, 40)
        Me.TXTCASHNO.Name = "TXTCASHNO"
        Me.TXTCASHNO.ReadOnly = True
        Me.TXTCASHNO.Size = New System.Drawing.Size(88, 23)
        Me.TXTCASHNO.TabIndex = 1
        Me.TXTCASHNO.TabStop = False
        '
        'CashEntry
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(909, 541)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "CashEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CashEntry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.EP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.GRIDCASH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LBLSRNO As Label
    Friend WithEvents ToolPREVIOUS As ToolStripButton
    Friend WithEvents Toolnext As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TXTADD As TextBox
    Friend WithEvents CMBCODE As ComboBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents txtremarks As TextBox
    Friend WithEvents cmdclear As Button
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents EP As ErrorProvider
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents TXTNARRATION As TextBox
    Friend WithEvents GRIDCASH As DataGridView
    Friend WithEvents tstxtbillno As TextBox
    Friend WithEvents TXTSRNO As TextBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents OpenToolStripButton As ToolStripButton
    Friend WithEvents SaveToolStripButton As ToolStripButton
    Friend WithEvents PrintToolStripButton As ToolStripButton
    Friend WithEvents TOOLDELETE As ToolStripButton
    Friend WithEvents CASHDATE As MaskedTextBox
    Friend WithEvents cmddelete As Button
    Friend WithEvents cmdok As Button
    Friend WithEvents cmdEXIT As Button
    Friend WithEvents TXTCASHNO As TextBox
    Friend WithEvents CMBPARTYNAME As ComboBox
    Friend WithEvents TXTCREDIT As TextBox
    Friend WithEvents TXTDEBIT As TextBox
    Friend WithEvents gsrno As DataGridViewTextBoxColumn
    Friend WithEvents GPARTYNAME As DataGridViewTextBoxColumn
    Friend WithEvents GCASHDATE As DataGridViewTextBoxColumn
    Friend WithEvents GNARRATION As DataGridViewTextBoxColumn
    Friend WithEvents GDebit As DataGridViewTextBoxColumn
    Friend WithEvents GCredit As DataGridViewTextBoxColumn
    Friend WithEvents LBLTOTALDEBIT As Label
    Friend WithEvents LBLTOTALCREDIT As Label
    Friend WithEvents Label10 As Label
End Class
