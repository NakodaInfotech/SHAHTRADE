
Imports BL

Public Class ContractorReceiptDetails
    Dim PURCHASEREGID As Integer
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CONTRACTORRECEIPTDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf e.KeyCode = Keys.N And e.Alt = True Then
                showform(False, 0)
            ElseIf e.KeyCode = Keys.O And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRACTORRECEIPTDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'LABOUR'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            FILLGRID(" AND CONTRACTORRECEIPT.CONREC_CMPID= " & CmpId & " AND CONTRACTORRECEIPT.CONREC_yearid = " & YearId & " ORDER BY CONTRACTORRECEIPT.CONREC_NO ")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID(ByVal tepmcondition)
        Try

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" ISNULL(ACCOUNTSMASTER.ACC_CMPNAME, '') AS NAME, ISNULL(CONTRACTORRECEIPT.CONREC_NO, 0) AS RECEIPTNO, CONTRACTORRECEIPT.CONREC_DATE AS DATE, ISNULL(CONTRACTORRECEIPT.CONREC_REFNO, 0) AS REFNO, ISNULL(CONTRACTORRECEIPT.CONREC_TOTALQTY, 0) AS TOTALQTY, ISNULL(CONTRACTORRECEIPT.CONREC_TOTALAMT, 0) AS TOTALAMT ", " ", " CONTRACTORRECEIPT INNER JOIN ACCOUNTSMASTER ON CONTRACTORRECEIPT.CONREC_ACCID = ACCOUNTSMASTER.ACC_ID AND CONTRACTORRECEIPT.CONREC_YEARID = ACCOUNTSMASTER.ACC_YEARID ", tepmcondition)
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal RECEIPTNO As Integer)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim OBJBILL As New ContractorReceipt
                OBJBILL.MdiParent = MDIMain
                OBJBILL.EDIT = editval
                OBJBILL.TEMPNO = RECEIPTNO
                OBJBILL.Show()
                '  Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("RECEIPTNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridCONREC_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("RECEIPTNO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Contractor Receipt Details.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next
            opti.SheetName = "Contractor Receipt Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Contractor Receipt Details", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Dim IntResult As Integer
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If MsgBox("Delete Contractor Receipt?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim alParaval As New ArrayList
                alParaval.Add(gridbill.GetFocusedRowCellValue("RECEIPTNO"))
                alParaval.Add(YearId)
                alParaval.Add(Userid)

                Dim OBJRECEIPT As New ClsContractorReceipt
                OBJRECEIPT.alParaval = alParaval
                IntResult = OBJRECEIPT.DELETE()
                MsgBox("CONREC Deleted")

            End If

            FILLGRID(" AND CONTRACTORRECEIPT.CONREC_CMPID= " & CmpId & " AND CONTRACTORRECEIPT.CONREC_yearid = " & YearId & " ORDER BY CONTRACTORRECEIPT.CONREC_NO ")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class