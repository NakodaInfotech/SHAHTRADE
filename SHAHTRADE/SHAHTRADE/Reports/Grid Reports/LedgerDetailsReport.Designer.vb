<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LedgerDetailsReport
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
        Me.GEMAIL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPANNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GINTPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCRDAYS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTDSPER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSHIPPINGADD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDISC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADD1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTRANSPORT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.GWEBSITE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCMPNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGSTIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDEDUCTEETYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GOPBAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDRCR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAREA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRANGE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTREET = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPINCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCRLIMIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRESINO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPHONE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GALTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GMOBILENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBOSSMOBILE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFAX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GKMS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCSTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GVATNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GSTNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GAGENTCOMM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GADDLESS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.cmdedit = New System.Windows.Forms.Button()
        Me.CMDREFRESH = New System.Windows.Forms.Button()
        Me.CMDPRINT = New System.Windows.Forms.Button()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BlendPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GEMAIL
        '
        Me.GEMAIL.Caption = "Email"
        Me.GEMAIL.FieldName = "EMAIL"
        Me.GEMAIL.Name = "GEMAIL"
        Me.GEMAIL.Visible = True
        Me.GEMAIL.VisibleIndex = 22
        Me.GEMAIL.Width = 150
        '
        'GADD
        '
        Me.GADD.Caption = "Address"
        Me.GADD.FieldName = "ADD"
        Me.GADD.Name = "GADD"
        Me.GADD.Visible = True
        Me.GADD.VisibleIndex = 25
        Me.GADD.Width = 200
        '
        'GTYPE
        '
        Me.GTYPE.Caption = "Type"
        Me.GTYPE.FieldName = "TYPE"
        Me.GTYPE.Name = "GTYPE"
        Me.GTYPE.Visible = True
        Me.GTYPE.VisibleIndex = 3
        Me.GTYPE.Width = 90
        '
        'GAGENT
        '
        Me.GAGENT.Caption = "Agent Name"
        Me.GAGENT.FieldName = "AGENTNAME"
        Me.GAGENT.Name = "GAGENT"
        Me.GAGENT.Visible = True
        Me.GAGENT.VisibleIndex = 13
        Me.GAGENT.Width = 200
        '
        'GPANNO
        '
        Me.GPANNO.Caption = "PAN No"
        Me.GPANNO.FieldName = "PANNO"
        Me.GPANNO.Name = "GPANNO"
        Me.GPANNO.Visible = True
        Me.GPANNO.VisibleIndex = 23
        Me.GPANNO.Width = 100
        '
        'GINTPER
        '
        Me.GINTPER.Caption = "Int %"
        Me.GINTPER.FieldName = "INTPER"
        Me.GINTPER.Name = "GINTPER"
        '
        'GCRDAYS
        '
        Me.GCRDAYS.Caption = "Cr Days"
        Me.GCRDAYS.DisplayFormat.FormatString = "0"
        Me.GCRDAYS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GCRDAYS.FieldName = "CRDAYS"
        Me.GCRDAYS.Name = "GCRDAYS"
        Me.GCRDAYS.Visible = True
        Me.GCRDAYS.VisibleIndex = 16
        '
        'GTDSPER
        '
        Me.GTDSPER.Caption = "TDS %"
        Me.GTDSPER.FieldName = "TDSPER"
        Me.GTDSPER.Name = "GTDSPER"
        '
        'GSHIPPINGADD
        '
        Me.GSHIPPINGADD.Caption = "Shipping Add"
        Me.GSHIPPINGADD.FieldName = "SHIPPINGADD"
        Me.GSHIPPINGADD.Name = "GSHIPPINGADD"
        Me.GSHIPPINGADD.Width = 200
        '
        'GDISC
        '
        Me.GDISC.Caption = "Disc"
        Me.GDISC.FieldName = "DISC"
        Me.GDISC.Name = "GDISC"
        Me.GDISC.Visible = True
        Me.GDISC.VisibleIndex = 27
        '
        'GADD1
        '
        Me.GADD1.Caption = "Add 1"
        Me.GADD1.FieldName = "ADD1"
        Me.GADD1.Name = "GADD1"
        Me.GADD1.Visible = True
        Me.GADD1.VisibleIndex = 4
        '
        'GTRANSPORT
        '
        Me.GTRANSPORT.Caption = "Transport"
        Me.GTRANSPORT.FieldName = "TRANSPORT"
        Me.GTRANSPORT.Name = "GTRANSPORT"
        Me.GTRANSPORT.Visible = True
        Me.GTRANSPORT.VisibleIndex = 15
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdcancel.FlatAppearance.BorderSize = 0
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(734, 541)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 28)
        Me.cmdcancel.TabIndex = 316
        Me.cmdcancel.Text = "&Exit"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'GWEBSITE
        '
        Me.GWEBSITE.Caption = "Website"
        Me.GWEBSITE.FieldName = "WEBSITE"
        Me.GWEBSITE.Name = "GWEBSITE"
        Me.GWEBSITE.Visible = True
        Me.GWEBSITE.VisibleIndex = 20
        Me.GWEBSITE.Width = 150
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(12, 13)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.Size = New System.Drawing.Size(1210, 523)
        Me.gridbilldetails.TabIndex = 494
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCMPNAME, Me.GGSTIN, Me.GDEDUCTEETYPE, Me.GGROUP, Me.GTYPE, Me.GOPBAL, Me.GDRCR, Me.GINTPER, Me.GTDSPER, Me.GADD1, Me.GAREA, Me.GRANGE, Me.GSTREET, Me.GCITY, Me.GPINCODE, Me.GSTATE, Me.GCOUNTRY, Me.GAGENT, Me.GTRANSPORT, Me.GCRDAYS, Me.GCRLIMIT, Me.GRESINO, Me.GPHONE, Me.GALTNO, Me.GMOBILENO, Me.GBOSSMOBILE, Me.GFAX, Me.GADD, Me.GKMS, Me.GWEBSITE, Me.GEMAIL, Me.GPANNO, Me.GCSTNO, Me.GVATNO, Me.GSTNO, Me.GAGENTCOMM, Me.GDISC, Me.GADDLESS, Me.GSHIPPINGADD})
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GCMPNAME
        '
        Me.GCMPNAME.Caption = "Cmp Name"
        Me.GCMPNAME.FieldName = "CMPNAME"
        Me.GCMPNAME.Name = "GCMPNAME"
        Me.GCMPNAME.Visible = True
        Me.GCMPNAME.VisibleIndex = 0
        Me.GCMPNAME.Width = 150
        '
        'GGSTIN
        '
        Me.GGSTIN.Caption = "GSTIN"
        Me.GGSTIN.FieldName = "GSTIN"
        Me.GGSTIN.Name = "GGSTIN"
        Me.GGSTIN.Visible = True
        Me.GGSTIN.VisibleIndex = 1
        '
        'GDEDUCTEETYPE
        '
        Me.GDEDUCTEETYPE.Caption = "DEDUCTEETYPE"
        Me.GDEDUCTEETYPE.FieldName = "DEDUCTEETYPE"
        Me.GDEDUCTEETYPE.Name = "GDEDUCTEETYPE"
        Me.GDEDUCTEETYPE.Width = 150
        '
        'GGROUP
        '
        Me.GGROUP.Caption = "Group Name"
        Me.GGROUP.FieldName = "GROUPNAME"
        Me.GGROUP.Name = "GGROUP"
        Me.GGROUP.Visible = True
        Me.GGROUP.VisibleIndex = 2
        Me.GGROUP.Width = 100
        '
        'GOPBAL
        '
        Me.GOPBAL.Caption = "O/P Bal"
        Me.GOPBAL.FieldName = "OPBAL"
        Me.GOPBAL.Name = "GOPBAL"
        '
        'GDRCR
        '
        Me.GDRCR.Caption = "Dr/Cr"
        Me.GDRCR.FieldName = "DRCR"
        Me.GDRCR.Name = "GDRCR"
        Me.GDRCR.Width = 40
        '
        'GAREA
        '
        Me.GAREA.Caption = "Area"
        Me.GAREA.FieldName = "AREA"
        Me.GAREA.Name = "GAREA"
        Me.GAREA.Visible = True
        Me.GAREA.VisibleIndex = 5
        '
        'GRANGE
        '
        Me.GRANGE.Caption = "Land Mark"
        Me.GRANGE.FieldName = "RANGE"
        Me.GRANGE.Name = "GRANGE"
        Me.GRANGE.Visible = True
        Me.GRANGE.VisibleIndex = 7
        '
        'GSTREET
        '
        Me.GSTREET.Caption = "Street"
        Me.GSTREET.FieldName = "STREET"
        Me.GSTREET.Name = "GSTREET"
        Me.GSTREET.Visible = True
        Me.GSTREET.VisibleIndex = 6
        '
        'GCITY
        '
        Me.GCITY.Caption = "City"
        Me.GCITY.FieldName = "CITY"
        Me.GCITY.Name = "GCITY"
        Me.GCITY.Visible = True
        Me.GCITY.VisibleIndex = 8
        Me.GCITY.Width = 90
        '
        'GPINCODE
        '
        Me.GPINCODE.Caption = "Pin Code"
        Me.GPINCODE.FieldName = "ZIPCODE"
        Me.GPINCODE.Name = "GPINCODE"
        Me.GPINCODE.Visible = True
        Me.GPINCODE.VisibleIndex = 11
        '
        'GSTATE
        '
        Me.GSTATE.Caption = "State"
        Me.GSTATE.FieldName = "STATE"
        Me.GSTATE.Name = "GSTATE"
        Me.GSTATE.Visible = True
        Me.GSTATE.VisibleIndex = 9
        Me.GSTATE.Width = 90
        '
        'GCOUNTRY
        '
        Me.GCOUNTRY.Caption = "Country"
        Me.GCOUNTRY.FieldName = "COUNTRY"
        Me.GCOUNTRY.Name = "GCOUNTRY"
        Me.GCOUNTRY.Visible = True
        Me.GCOUNTRY.VisibleIndex = 10
        '
        'GCRLIMIT
        '
        Me.GCRLIMIT.Caption = "Cr. Limit"
        Me.GCRLIMIT.FieldName = "CRLIMIT"
        Me.GCRLIMIT.Name = "GCRLIMIT"
        Me.GCRLIMIT.Visible = True
        Me.GCRLIMIT.VisibleIndex = 18
        '
        'GRESINO
        '
        Me.GRESINO.Caption = "Office No 1"
        Me.GRESINO.FieldName = "RESINO"
        Me.GRESINO.Name = "GRESINO"
        Me.GRESINO.Visible = True
        Me.GRESINO.VisibleIndex = 12
        Me.GRESINO.Width = 90
        '
        'GPHONE
        '
        Me.GPHONE.Caption = "Office No 2"
        Me.GPHONE.FieldName = "PHONENO"
        Me.GPHONE.Name = "GPHONE"
        Me.GPHONE.Visible = True
        Me.GPHONE.VisibleIndex = 21
        Me.GPHONE.Width = 90
        '
        'GALTNO
        '
        Me.GALTNO.Caption = "Office No 3"
        Me.GALTNO.FieldName = "ALTNO"
        Me.GALTNO.Name = "GALTNO"
        Me.GALTNO.Visible = True
        Me.GALTNO.VisibleIndex = 14
        '
        'GMOBILENO
        '
        Me.GMOBILENO.Caption = "Mobile No"
        Me.GMOBILENO.FieldName = "MOBILE"
        Me.GMOBILENO.Name = "GMOBILENO"
        Me.GMOBILENO.Visible = True
        Me.GMOBILENO.VisibleIndex = 17
        Me.GMOBILENO.Width = 90
        '
        'GBOSSMOBILE
        '
        Me.GBOSSMOBILE.Caption = "Boss Mobile"
        Me.GBOSSMOBILE.FieldName = "BOSSMOBILE"
        Me.GBOSSMOBILE.Name = "GBOSSMOBILE"
        Me.GBOSSMOBILE.Visible = True
        Me.GBOSSMOBILE.VisibleIndex = 24
        '
        'GFAX
        '
        Me.GFAX.Caption = "Fax"
        Me.GFAX.FieldName = "FAX"
        Me.GFAX.Name = "GFAX"
        Me.GFAX.Visible = True
        Me.GFAX.VisibleIndex = 19
        Me.GFAX.Width = 90
        '
        'GKMS
        '
        Me.GKMS.Caption = "Kms"
        Me.GKMS.FieldName = "KMS"
        Me.GKMS.Name = "GKMS"
        Me.GKMS.Visible = True
        Me.GKMS.VisibleIndex = 26
        '
        'GCSTNO
        '
        Me.GCSTNO.Caption = "C.S.T No"
        Me.GCSTNO.FieldName = "CSTNO"
        Me.GCSTNO.Name = "GCSTNO"
        '
        'GVATNO
        '
        Me.GVATNO.Caption = "V.A.T No"
        Me.GVATNO.FieldName = "VATNO"
        Me.GVATNO.Name = "GVATNO"
        '
        'GSTNO
        '
        Me.GSTNO.Caption = "S.T No"
        Me.GSTNO.FieldName = "STNO"
        Me.GSTNO.Name = "GSTNO"
        '
        'GAGENTCOMM
        '
        Me.GAGENTCOMM.Caption = "Comm %"
        Me.GAGENTCOMM.FieldName = "AGENTCOMM"
        Me.GAGENTCOMM.Name = "GAGENTCOMM"
        '
        'GADDLESS
        '
        Me.GADDLESS.Caption = "ADD/LESS"
        Me.GADDLESS.FieldName = "ADDLESS"
        Me.GADDLESS.Name = "GADDLESS"
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.cmdadd)
        Me.BlendPanel1.Controls.Add(Me.cmdedit)
        Me.BlendPanel1.Controls.Add(Me.CMDREFRESH)
        Me.BlendPanel1.Controls.Add(Me.CMDPRINT)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.cmdcancel)
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1547, 655)
        Me.BlendPanel1.TabIndex = 6
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.Color.Transparent
        Me.cmdadd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdadd.FlatAppearance.BorderSize = 0
        Me.cmdadd.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.Color.Black
        Me.cmdadd.Location = New System.Drawing.Point(476, 541)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(80, 28)
        Me.cmdadd.TabIndex = 504
        Me.cmdadd.Text = "&Add New"
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'cmdedit
        '
        Me.cmdedit.BackColor = System.Drawing.Color.Transparent
        Me.cmdedit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmdedit.FlatAppearance.BorderSize = 0
        Me.cmdedit.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.ForeColor = System.Drawing.Color.Black
        Me.cmdedit.Location = New System.Drawing.Point(648, 541)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(80, 28)
        Me.cmdedit.TabIndex = 503
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = False
        '
        'CMDREFRESH
        '
        Me.CMDREFRESH.Location = New System.Drawing.Point(562, 541)
        Me.CMDREFRESH.Name = "CMDREFRESH"
        Me.CMDREFRESH.Size = New System.Drawing.Size(80, 28)
        Me.CMDREFRESH.TabIndex = 502
        Me.CMDREFRESH.Text = "&Refresh"
        Me.CMDREFRESH.UseVisualStyleBackColor = True
        '
        'CMDPRINT
        '
        Me.CMDPRINT.BackColor = System.Drawing.Color.Transparent
        Me.CMDPRINT.BackgroundImage = Global.SHAHTRADE.My.Resources.Resources.Excel_icon
        Me.CMDPRINT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CMDPRINT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CMDPRINT.FlatAppearance.BorderSize = 0
        Me.CMDPRINT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CMDPRINT.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDPRINT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CMDPRINT.Location = New System.Drawing.Point(446, 545)
        Me.CMDPRINT.Name = "CMDPRINT"
        Me.CMDPRINT.Size = New System.Drawing.Size(25, 21)
        Me.CMDPRINT.TabIndex = 501
        Me.CMDPRINT.UseVisualStyleBackColor = False
        '
        'LedgerDetailsReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "LedgerDetailsReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Ledger Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BlendPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GEMAIL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPANNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINTPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCRDAYS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTDSPER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSHIPPINGADD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDISC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADD1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTRANSPORT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmdcancel As Button
    Friend WithEvents GWEBSITE As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCMPNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDEDUCTEETYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GOPBAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDRCR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAREA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPINCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRESINO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GALTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPHONE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GMOBILENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFAX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GKMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Friend WithEvents cmdadd As Button
    Friend WithEvents cmdedit As Button
    Friend WithEvents CMDREFRESH As Button
    Friend WithEvents CMDPRINT As Button
    Friend WithEvents GSTREET As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRANGE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCRLIMIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBOSSMOBILE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCSTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVATNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GSTNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GAGENTCOMM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGSTIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GADDLESS As DevExpress.XtraGrid.Columns.GridColumn
End Class
