<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IssueVoucherDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IssueVoucherDetails))
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
        Me.GTOTALPCS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALGROSSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALLESSWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALNETTWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTOTALFINEWT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PBPHOTO = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.CHKDONE = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TOOLEXCEL = New System.Windows.Forms.ToolStripButton()
        Me.TOOLREFRESH = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TOOLGRIDDETAILS = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CMDEXIT = New System.Windows.Forms.Button()
        Me.CMDEDIT = New System.Windows.Forms.Button()
        Me.CMDADD = New System.Windows.Forms.Button()
        Me.GINITIALGROSSWT = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.BlendPanel1.TabIndex = 1
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
        Me.gridbill.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GISSUENO, Me.GDATE, Me.GNAME, Me.GHALLMARK, Me.GREQNO, Me.GVOUCHERWT, Me.GRECD, Me.GRECDDATE, Me.GBOXWT, Me.GWTWITHTAG, Me.GINITIALGROSSWT, Me.GTOTALPCS, Me.GTOTALGROSSWT, Me.GTOTALLESSWT, Me.GTOTALNETTWT, Me.GTOTALFINEWT, Me.GREMARKS})
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
        'GTOTALPCS
        '
        Me.GTOTALPCS.Caption = "Total Pcs"
        Me.GTOTALPCS.DisplayFormat.FormatString = "0"
        Me.GTOTALPCS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALPCS.FieldName = "TOTALPCS"
        Me.GTOTALPCS.Name = "GTOTALPCS"
        Me.GTOTALPCS.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALPCS.Visible = True
        Me.GTOTALPCS.VisibleIndex = 11
        '
        'GTOTALGROSSWT
        '
        Me.GTOTALGROSSWT.Caption = "Total Gross Wt"
        Me.GTOTALGROSSWT.DisplayFormat.FormatString = "0.000"
        Me.GTOTALGROSSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALGROSSWT.FieldName = "TOTALGROSSWT"
        Me.GTOTALGROSSWT.Name = "GTOTALGROSSWT"
        Me.GTOTALGROSSWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALGROSSWT.Visible = True
        Me.GTOTALGROSSWT.VisibleIndex = 12
        Me.GTOTALGROSSWT.Width = 100
        '
        'GTOTALLESSWT
        '
        Me.GTOTALLESSWT.Caption = "Total Less Wt"
        Me.GTOTALLESSWT.DisplayFormat.FormatString = "0.000"
        Me.GTOTALLESSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALLESSWT.FieldName = "TOTALLESSWT"
        Me.GTOTALLESSWT.Name = "GTOTALLESSWT"
        Me.GTOTALLESSWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALLESSWT.Visible = True
        Me.GTOTALLESSWT.VisibleIndex = 13
        Me.GTOTALLESSWT.Width = 100
        '
        'GTOTALNETTWT
        '
        Me.GTOTALNETTWT.Caption = "Total Nett Wt"
        Me.GTOTALNETTWT.DisplayFormat.FormatString = "0.000"
        Me.GTOTALNETTWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALNETTWT.FieldName = "TOTALNETTWT"
        Me.GTOTALNETTWT.Name = "GTOTALNETTWT"
        Me.GTOTALNETTWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALNETTWT.Visible = True
        Me.GTOTALNETTWT.VisibleIndex = 14
        Me.GTOTALNETTWT.Width = 100
        '
        'GTOTALFINEWT
        '
        Me.GTOTALFINEWT.Caption = "Total Fine Wt"
        Me.GTOTALFINEWT.DisplayFormat.FormatString = "0.000"
        Me.GTOTALFINEWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GTOTALFINEWT.FieldName = "TOTALFINEWT"
        Me.GTOTALFINEWT.Name = "GTOTALFINEWT"
        Me.GTOTALFINEWT.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)})
        Me.GTOTALFINEWT.Visible = True
        Me.GTOTALFINEWT.VisibleIndex = 15
        Me.GTOTALFINEWT.Width = 100
        '
        'GREMARKS
        '
        Me.GREMARKS.Caption = "Remarks"
        Me.GREMARKS.FieldName = "REMARKS"
        Me.GREMARKS.Name = "GREMARKS"
        Me.GREMARKS.Visible = True
        Me.GREMARKS.VisibleIndex = 16
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TOOLEXCEL, Me.TOOLREFRESH, Me.ToolStripSeparator2, Me.TOOLGRIDDETAILS, Me.ToolStripSeparator1})
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
        'TOOLGRIDDETAILS
        '
        Me.TOOLGRIDDETAILS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TOOLGRIDDETAILS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TOOLGRIDDETAILS.Name = "TOOLGRIDDETAILS"
        Me.TOOLGRIDDETAILS.Size = New System.Drawing.Size(69, 22)
        Me.TOOLGRIDDETAILS.Text = "Grid Details"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'GINITIALGROSSWT
        '
        Me.GINITIALGROSSWT.Caption = "Initial Gross Wt"
        Me.GINITIALGROSSWT.DisplayFormat.FormatString = "0.000"
        Me.GINITIALGROSSWT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GINITIALGROSSWT.FieldName = "INITIALGROSSWT"
        Me.GINITIALGROSSWT.Name = "GINITIALGROSSWT"
        Me.GINITIALGROSSWT.Visible = True
        Me.GINITIALGROSSWT.VisibleIndex = 10
        '
        'IssueVoucherDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1234, 581)
        Me.Controls.Add(Me.BlendPanel1)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "IssueVoucherDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Issue Voucher Details"
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
    Friend WithEvents GTOTALPCS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PBPHOTO As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents CHKDONE As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents TOOLEXCEL As ToolStripButton
    Friend WithEvents TOOLREFRESH As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TOOLGRIDDETAILS As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents CMDEXIT As Button
    Friend WithEvents CMDEDIT As Button
    Friend WithEvents CMDADD As Button
    Friend WithEvents GREQNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GVOUCHERWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GRECDDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GBOXWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GWTWITHTAG As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALGROSSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALLESSWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALNETTWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTOTALFINEWT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GINITIALGROSSWT As DevExpress.XtraGrid.Columns.GridColumn
End Class
