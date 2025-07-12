
Imports BL

Public Class IssueVoucherDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub CMDEXIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub showform(ByVal EDITVAL As Boolean, ByVal INVNO As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJISS As New IssueVoucher
            OBJISS.EDIT = EDITVAL
            OBJISS.MdiParent = MDIMain
            OBJISS.TEMPISSUENO = INVNO
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub IssueVoucherDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.Alt = True And e.KeyCode = Keys.R Then
            Call TOOLREFRESH_Click(sender, e)
        ElseIf e.Alt = True And e.KeyCode = Keys.P Then
            Call TOOLEXCEL_Click(sender, e)
        ElseIf e.KeyCode = Keys.OemQuotes Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub IssueVoucherDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Try
            Dim OBJCMN As New ClsCommon
            Dim dttable As DataTable = OBJCMN.search(" ISSUEVOUCHER.ISS_NO AS ISSUENO, ISSUEVOUCHER.ISS_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(HALLMARKMASTER.HALLMARK_name, '') AS HALLMARK, ISSUEVOUCHER.ISS_REQNO AS REQNO, ISSUEVOUCHER.ISS_VOUCHERWT AS VOUCHERWT, ISSUEVOUCHER.ISS_RECD AS RECD, ISSUEVOUCHER.ISS_RECDATE AS RECDATE, ISSUEVOUCHER.ISS_BOXWT AS BOXWT, ISSUEVOUCHER.ISS_WTWITHTAGS AS WTWITHTAGS, ISNULL(ISSUEVOUCHER.ISS_INITIALGROSSWT,0) AS INITIALGROSSWT, ISSUEVOUCHER.ISS_TOTALPCS AS TOTALPCS, ISSUEVOUCHER.ISS_TOTALGROSSWT AS TOTALGROSSWT, ISSUEVOUCHER.ISS_TOTALLESSWT AS TOTALLESSWT, ISSUEVOUCHER.ISS_TOTALNETTWT AS TOTALNETTWT, ISSUEVOUCHER.ISS_TOTALFINEWT AS TOTALFINEWT, ISSUEVOUCHER.ISS_REMARKS AS REMARKS ", "", " ISSUEVOUCHER INNER JOIN LEDGERS ON ISSUEVOUCHER.ISS_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN HALLMARKMASTER ON ISSUEVOUCHER.ISS_HALLMARKID = HALLMARKMASTER.HALLMARK_id ", " AND ISSUEVOUCHER.ISS_YEARID = " & YearId & " ORDER BY ISSUEVOUCHER.ISS_NO")
            gridbilldetails.DataSource = dttable
            If dttable.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEDIT.Click
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("ISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            If USEREDIT = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(True, gridbill.GetFocusedRowCellValue("ISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJISS As New IssueVoucherGridDetails
            OBJISS.MdiParent = MDIMain
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            showform(False, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLREFRESH.Click
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

    Private Sub TOOLEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Issue Voucher Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim workbook As String = PATH
            If FileIO.FileSystem.FileExists(PATH) = True Then Interaction.GetObject(workbook).close(False)
            GC.Collect()

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Issue Voucher Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Issue Voucher Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class