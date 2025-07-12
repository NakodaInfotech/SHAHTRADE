Imports System.Windows.Forms
Imports BL

Public Class PaymentDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim PAYREGID As Integer

    Private Sub PaymentDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.Alt = True And e.KeyCode = Windows.Forms.Keys.A) Then       'for AddNew 
                showform(False, 0)
            ElseIf e.KeyCode = Keys.E And e.Alt = True Then
                CMDOK_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal tepmcondition)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable
            dt = objclsCMST.search(" dbo.PaymentMaster.Payment_no AS [Sr. No], dbo.Ledgers.acc_cmpname AS [Name] ,dbo.PaymentMaster.Payment_date AS [Date], dbo.PaymentMaster.payment_total AS [Total], dbo.PaymentMaster.payment_chqno AS [Chq. No.], dbo.PaymentMaster.payment_registerid AS [Registerid], dbo.PaymentMaster.Payment_remarks AS [Remarks], BANKLEDGERS.Acc_cmpname AS [BankName] ", "", "  PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENTMASTER.payment_cmpid = LEDGERS.Acc_cmpid AND PAYMENTMASTER.payment_LOCATIONid = LEDGERS.Acc_LOCATIONid AND PAYMENTMASTER.payment_YEARid = LEDGERS.Acc_YEARid AND PAYMENTMASTER.payment_ledgerid = LEDGERS.Acc_id INNER JOIN LEDGERS AS BANKLEDGERS ON PAYMENTMASTER.payment_yearid = BANKLEDGERS.Acc_yearid AND PAYMENTMASTER.payment_locationid = BANKLEDGERS.Acc_locationid AND PAYMENTMASTER.payment_cmpid = BANKLEDGERS.Acc_cmpid AND PAYMENTMASTER.payment_accid = BANKLEDGERS.Acc_id", tepmcondition)
            griddetails.DataSource = dt
            If dt.Rows.Count > 0 Then
                gridpayment.FocusedRowHandle = gridpayment.RowCount - 1
                gridpayment.TopRowIndex = gridpayment.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal billno As Integer)
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            If (editval = False) Or (editval = True And gridpayment.RowCount > 0) Then
                Dim objpaymaster As New PaymentMaster
                objpaymaster.MdiParent = MDIMain
                objpaymaster.edit = editval
                objpaymaster.TEMPREGNAME = cmbregister.Text.Trim
                objpaymaster.TEMPPAYMENTNO = billno
                objpaymaster.Show()
                'Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

    Private Sub gridpayment_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridpayment.DoubleClick
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbregister.GotFocus
        Try
            If cmbregister.Text.Trim = "" Then fillregister(cmbregister, " and register_type = 'PAYMENT'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'PAYMENT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbregister.Validating
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If cmbregister.Text.Trim.Length > 0 Then
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_id ", "", " RegisterMaster ", " and register_name ='" & cmbregister.Text.Trim & "' and register_type = 'PAYMENT' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                If dt.Rows.Count > 0 Then
                    PAYREGID = dt.Rows(0).Item(0)
                    'cmbregister.Enabled = False
                    fillgrid(" and dbo.PaymentMaster.payment_cmpid=" & CmpId & " and dbo.PaymentMaster.payment_LOCATIONid=" & Locationid & " and dbo.PaymentMaster.payment_YEARid=" & YearId & " AND PaymentMaster.payment_registerid = " & PAYREGID & " order by dbo.PaymentMaster.payment_no ")
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PAYMENT'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridpayment.GetFocusedRowCellValue("Sr. No"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ExcelExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelExport.Click
        Try
            Dim PATH As String = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Payment Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                proc.Kill()
            Next

            Dim PERIOD As String = ""
            PERIOD = AccFrom & " - " & AccTo

            opti.SheetName = "Payment Details"
            gridpayment.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Payment Details", gridpayment.VisibleColumns.Count + gridpayment.GroupCount, "", PERIOD)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PaymentDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ClientName = "SVS" Then Me.Close()
    End Sub
End Class