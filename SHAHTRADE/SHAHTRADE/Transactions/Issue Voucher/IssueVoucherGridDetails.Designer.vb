<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IssueVoucherGridDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IssueVoucherGridDetails))
        Me.BlendPanel1 = New VbPowerPack.BlendPanel()
        Me.gridbilldetails = New DevExpress.XtraGrid.GridControl()
        Me.gridbill = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GISSUENO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GHALLMARK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREQNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GVOUCHERWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GRECDDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GBOXWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GWTWITHTAG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GITEMNAME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GDESC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGROSSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GLESSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GNETTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GPURITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFINEWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GHUID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PBPHOTO = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADD = New System.Windows.Forms.Button()
        Me.BlendPanel1.SuspendLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BlendPanel1
        '
        Me.BlendPanel1.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(248, Byte), Integer)), System.Drawing.SystemColors.Window)
        Me.BlendPanel1.Controls.Add(Me.gridbilldetails)
        Me.BlendPanel1.Controls.Add(Me.ToolStrip1)
        Me.BlendPanel1.Controls.Add(Me.CMDEXIT)
        Me.BlendPanel1.Controls.Add(Me.CMDEDIT)
        Me.BlendPanel1.Controls.Add(Me.CMDADD)
        Me.BlendPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BlendPanel1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BlendPanel1.Location = New System.Drawing.Point(0, 0)
        Me.BlendPanel1.Name = "BlendPanel1"
        Me.BlendPanel1.Size = New System.Drawing.Size(1234, 581)
        Me.BlendPanel1.TabIndex = 2
        '
        'gridbilldetails
        '
        Me.gridbilldetails.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridbilldetails.Location = New System.Drawing.Point(16, 28)
        Me.gridbilldetails.LookAndFeel.UseDefaultLookAndFeel = False
        Me.gridbilldetails.MainView = Me.gridbill
        Me.gridbilldetails.Name = "gridbilldetails"
        Me.gridbilldetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.PBPHOTO, Me.CHKDONE})
        Me.gridbilldetails.Size = New System.Drawing.Size(1202, 512)
        Me.gridbilldetails.TabIndex = 1
        Me.gridbilldetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridbill})
        '
        'gridbill
        '
        Me.gridbill.Appearance.HeaderPanel.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridbill.Appearance.Row.Font = New System.Drawing.Font("Calibri", 9.75!)
        Me.gridbill.Appearance.Row.Options.UseFont = True
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GISSUENO, Me.GDATE, Me.GNAME, Me.GHALLMARK, Me.GREQNO, Me.GVOUCHERWT, Me.GRECD, Me.GRECDDATE, Me.GBOXWT, Me.GWTWITHTAG, Me.GITEMNAME, Me.GDESC, Me.GPCS, Me.GGROSSWT, Me.GLESSWT, Me.GNETTWT, Me.GPURITY, Me.GFINEWT, Me.GHUID, Me.GREMARKS})
        Me.gridbill.CustomizationFormBounds = New System.Drawing.Rectangle(940, 535, 216, 192)
        Me.gridbill.GridControl = Me.gridbilldetails
        Me.gridbill.Name = "gridbill"
        Me.gridbill.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridbill.OptionsBehavior.AutoExpandAllGroups = True
        Me.gridbill.OptionsBehavior.Editable = False
        Me.gridbill.OptionsCustomization.AllowRowSizing = True
        Me.gridbill.OptionsView.ColumnAutoWidth = False
        Me.gridbill.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gridbill.OptionsView.ShowAutoFilterRow = True
        Me.gridbill.OptionsView.ShowFooter = True
        Me.gridbill.OptionsView.ShowGroupPanel = False
        '
        'GISSUENO
        '
        Me.GISSUENO.Caption = "Sr No"
        Me.GISSUENO.FieldName = "ISSUENO"
        Me.GISSUENO.Name = "GISSUENO"
        Me.GISSUENO.Visible = True
        Me.GISSUENO.VisibleIndex = 0
        Me.GISSUENO.Width = 60
        '
        'GDATE
        '
        Me.GDATE.Caption = "Date"
        Me.GDATE.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GDATE.FieldName = "DATE"
        Me.GDATE.Name = "GDATE"
        Me.GDATE.Visible = True
        Me.GDATE.VisibleIndex = 1
        '
        'GNAME
        '
        Me.GNAME.Caption = "Party Name"
        Me.GNAME.FieldName = "NAME"
        Me.GNAME.Name = "GNAME"
        Me.GNAME.Visible = True
        Me.GNAME.VisibleIndex = 2
        Me.GNAME.Width = 200
        '
        'GHALLMARK
        '
        Me.GHALLMARK.Caption = "Hallmark Name"
        Me.GHALLMARK.FieldName = "HALLMARK"
        Me.GHALLMARK.Name = "GHALLMARK"
        Me.GHALLMARK.Visible = True
        Me.GHALLMARK.VisibleIndex = 3
        Me.GHALLMARK.Width = 200
        '
        'GREQNO
        '
        Me.GREQNO.Caption = "Req No"
        Me.GREQNO.FieldName = "REQNO"
        Me.GREQNO.Name = "GREQNO"
        Me.GREQNO.Visible = True
        Me.GREQNO.VisibleIndex = 4
        Me.GREQNO.Width = 100
        '
        'GVOUCHERWT
        '
        Me.GVOUCHERWT.Caption = "Voucher Wt"
        Me.GVOUCHERWT.DisplayFormat.FormatString = "0.000"
        Me.GVOUCHERWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GVOUCHERWT.FieldName = "VOUCHERWT"
        Me.GVOUCHERWT.Name = "GVOUCHERWT"
        Me.GVOUCHERWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GVOUCHERWT.Visible = True
        Me.GVOUCHERWT.VisibleIndex = 5
        Me.GVOUCHERWT.Width = 85
        '
        'GRECD
        '
        Me.GRECD.Caption = "Recd"
        Me.GRECD.FieldName = "RECD"
        Me.GRECD.Name = "GRECD"
        Me.GRECD.Visible = True
        Me.GRECD.VisibleIndex = 6
        Me.GRECD.Width = 50
        '
        'GRECDDATE
        '
        Me.GRECDDATE.Caption = "Recd Date"
        Me.GRECDDATE.FieldName = "RECDATE"
        Me.GRECDDATE.Name = "GRECDDATE"
        Me.GRECDDATE.Visible = True
        Me.GRECDDATE.VisibleIndex = 7
        '
        'GBOXWT
        '
        Me.GBOXWT.Caption = "Box Wt"
        Me.GBOXWT.DisplayFormat.FormatString = "0.000"
        Me.GBOXWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GBOXWT.FieldName = "BOXWT"
        Me.GBOXWT.Name = "GBOXWT"
        Me.GBOXWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GBOXWT.Visible = True
        Me.GBOXWT.VisibleIndex = 8
        '
        'GWTWITHTAG
        '
        Me.GWTWITHTAG.Caption = "Wt With Tag"
        Me.GWTWITHTAG.DisplayFormat.FormatString = "0.000"
        Me.GWTWITHTAG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GWTWITHTAG.FieldName = "WTWITHTAGS"
        Me.GWTWITHTAG.Name = "GWTWITHTAG"
        Me.GWTWITHTAG.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GWTWITHTAG.Visible = True
        Me.GWTWITHTAG.VisibleIndex = 9
        Me.GWTWITHTAG.Width = 85
        '
        'GITEMNAME
        '
        Me.GITEMNAME.Caption = "Item Name"
        Me.GITEMNAME.FieldName = "ITEMNAME"
        Me.GITEMNAME.Name = "GITEMNAME"
        Me.GITEMNAME.Visible = True
        Me.GITEMNAME.VisibleIndex = 10
        Me.GITEMNAME.Width = 200
        '
        'GDESC
        '
        Me.GDESC.Caption = "Description"
        Me.GDESC.FieldName = "GRIDREMARKS"
        Me.GDESC.Name = "GDESC"
        Me.GDESC.Visible = True
        Me.GDESC.VisibleIndex = 11
        Me.GDESC.Width = 200
        '
        'GPCS
        '
        Me.GPCS.Caption = "Pcs"
        Me.GPCS.DisplayFormat.FormatString = "0"
        Me.GPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPCS.FieldName = "PCS"
        Me.GPCS.Name = "GPCS"
        Me.GPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GPCS.Visible = True
        Me.GPCS.VisibleIndex = 12
        '
        'GGROSSWT
        '
        Me.GGROSSWT.Caption = "Gross Wt"
        Me.GGROSSWT.DisplayFormat.FormatString = "0.000"
        Me.GGROSSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GGROSSWT.FieldName = "GROSSWT"
        Me.GGROSSWT.Name = "GGROSSWT"
        Me.GGROSSWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GGROSSWT.Visible = True
        Me.GGROSSWT.VisibleIndex = 13
        Me.GGROSSWT.Width = 100
        '
        'GLESSWT
        '
        Me.GLESSWT.Caption = "Less Wt"
        Me.GLESSWT.DisplayFormat.FormatString = "0.000"
        Me.GLESSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GLESSWT.FieldName = "LESSWT"
        Me.GLESSWT.Name = "GLESSWT"
        Me.GLESSWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GLESSWT.Visible = True
        Me.GLESSWT.VisibleIndex = 14
        Me.GLESSWT.Width = 100
        '
        'GNETTWT
        '
        Me.GNETTWT.Caption = "Nett Wt"
        Me.GNETTWT.DisplayFormat.FormatString = "0.000"
        Me.GNETTWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GNETTWT.FieldName = "NETTWT"
        Me.GNETTWT.Name = "GNETTWT"
        Me.GNETTWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GNETTWT.Visible = True
        Me.GNETTWT.VisibleIndex = 15
        Me.GNETTWT.Width = 100
        '
        'GPURITY
        '
        Me.GPURITY.Caption = "Purity"
        Me.GPURITY.DisplayFormat.FormatString = "0.00"
        Me.GPURITY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GPURITY.FieldName = "PURITY"
        Me.GPURITY.Name = "GPURITY"
        Me.GPURITY.Visible = True
        Me.GPURITY.VisibleIndex = 16
        '
        'GFINEWT
        '
        Me.GFINEWT.Caption = "Fine Wt"
        Me.GFINEWT.DisplayFormat.FormatString = "0.000"
        Me.GFINEWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GFINEWT.FieldName = "FINEWT"
        Me.GFINEWT.Name = "GFINEWT"
        Me.GFINEWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GFINEWT.Visible = True
        Me.GFINEWT.VisibleIndex = 17
        Me.GFINEWT.Width = 100
        '
        'GHUID
        '
        Me.GHUID.Caption = "HUID"
        Me.GHUID.FieldName = "HUID"
        Me.GHUID.Name = "GHUID"
        Me.GHUID.Visible = True
        Me.GHUID.VisibleIndex = 18
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 19
        Me.GREMARKS.Width = 200
        '
        'PBPHOTO
        '
        Me.PBPHOTO.Name = "PBPHOTO"
        Me.PBPHOTO.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'CHKDONE
        '
        Me.CHKDONE.AutoHeight = False
        Me.CHKDONE.Name = "CHKDONE"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.TOOLREFRESH, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1234, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TOOLEXCEL
        '
        Me.TOOLEXCEL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLEXCEL.Image = Global.SHAHTRADE.My.Resources.Resources.Excel_icon
        Me.TOOLEXCEL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLEXCEL.Name = "TOOLEXCEL"
        Me.TOOLEXCEL.Size = New System.Drawing.Size(23, 22)
        Me.TOOLEXCEL.Text = "Print"
        '
        'TOOLREFRESH
        '
        Me.TOOLREFRESH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TOOLREFRESH.Image = CType(resources.GetObject("TOOLREFRESH.Image"), System.Drawing.Image)
        Me.TOOLREFRESH.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLREFRESH.Name = "TOOLREFRESH"
        Me.TOOLREFRESH.Size = New System.Drawing.Size(23, 22)
        Me.TOOLREFRESH.Text = "ToolStripButton1"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'CMDEXIT
        '
        Me.CMDEXIT.Location = New System.Drawing.Point(662, 546)
        Me.CMDEXIT.Name = "CMDEXIT"
        Me.CMDEXIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEXIT.TabIndex = 4
        Me.CMDEXIT.Text = "E&xit"
        Me.CMDEXIT.UseVisualStyleBackColor = True
        '
        'CMDEDIT
        '
        Me.CMDEDIT.Location = New System.Drawing.Point(577, 546)
        Me.CMDEDIT.Name = "CMDEDIT"
        Me.CMDEDIT.Size = New System.Drawing.Size(80, 28)
        Me.CMDEDIT.TabIndex = 3
        Me.CMDEDIT.Text = "&Edit"
        Me.CMDEDIT.UseVisualStyleBackColor = True
        '
        'CMDADD
        '
        Me.CMDADD.Location = New System.Drawing.Point(492, 546)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(80, 28)
        Me.CMDADD.TabIndex = 2
        Me.CMDADD.Text = "&Add New"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'IssueVoucherGridDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "IssueVoucherGridDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Issue Voucher Grid Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.BlendPanel1.ResumeLayout(False)
        Me.BlendPanel1.PerformLayout()
        CType(Me.gridbilldetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridbill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBPHOTO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CHKDONE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BlendPanel1 As VbPowerPack.BlendPanel
    Private WithEvents gridbilldetails As DevExpress.XtraGrid.GridControl
    Private WithEvents gridbill As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GISSUENO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GHALLMARK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREQNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVOUCHERWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECDDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBOXWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWTWITHTAG As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GITEMNAME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GDESC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGROSSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GLESSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GNETTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GPURITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFINEWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GHUID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PBPHOTO As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDADD As Button
End Class
