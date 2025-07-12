Imports BL
Public Class MaterialOutWardGridDetails
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub CMDADD_Click(sender As Object, e As EventArgs) Handles CMDADD.Click

    End Sub

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            showform(True, GridBill.GetFocusedRowCellValue("ISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Me.Close()
    End Sub
    Sub showform(ByVal EDITVAL As Boolean, ByVal OUTWARDno As Integer)
        Try
            If (EDITVAL = True And USEREDIT = False And USERVIEW = False) Or (EDITVAL = False And USERADD = False) Then
                MsgBox("Insfficient Rights")
            End If
            If (EDITVAL = False) Or (EDITVAL = True And GridBill.RowCount > 0) Then
                Dim OBJMID As New MaterialOutward
                OBJMID.MdiParent = MDIMain
                OBJMID.EDIT = EDITVAL
                OBJMID.TEMPOUTWARDNO = OUTWARDno
                OBJMID.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialOUTWARDGridDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Sale Invoice Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Sale Invoice Details"
            GridBill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Sale Invoice Details", GridBill.VisibleColumns.Count + GridBill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click

    End Sub

    Sub fillgrid()
        Try
            Dim OBJINV As New ClsMaterialOutward
            OBJINV.alParaval.Add(0)
            OBJINV.alParaval.Add(YearId)
            Dim dttable As DataTable = OBJINV.SELECTMATERIALOUTWARD()
            GridBillDetails.DataSource = dttable
            If dttable.Rows.Count > 0 Then
                GridBill.FocusedRowHandle = GridBill.RowCount - 1
                GridBill.TopRowIndex = GridBill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialOUTWARDGridDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles GridBillDetails.DoubleClick
    '    Try
    '        showform(True, GridBill.GetFocusedRowCellValue("ISSUENO"))
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub GridBill_DoubleClick(sender As Object, e As EventArgs) Handles GridBill.DoubleClick
        Try
            showform(True, GridBill.GetFocusedRowCellValue("SRNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class