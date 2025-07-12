
Imports BL

Public Class LedgerDetailsReport

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub LedgerDetailsReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Or (e.KeyCode = Keys.X And e.Alt = True) Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillgrid(ByVal WHERECLAUSE As String)
        Try
            Dim objclsCMST As New ClsCommonMaster
            Dim dt As DataTable = objclsCMST.search(" ISNULL(LEDGERS.Acc_cmpname,'') AS CMPNAME, ISNULL(LEDGERS.Acc_name,'') AS NAME, ISNULL(GROUPMASTER.group_name,'') AS GROUPNAME, ISNULL(LEDGERS.Acc_opbal,0) AS OPBAL, ISNULL(LEDGERS.Acc_drcr,'') AS DRCR, ISNULL(LEDGERS.ACC_INTPER, 0) AS INTPER, ISNULL(ACCOUNTSMASTER_TDS.ACC_TDSPER, 0) AS TDSPER, ISNULL(LEDGERS.Acc_add1, '') AS add1, ISNULL(LEDGERS.Acc_Streetid, '') AS STREET, ISNULL(AREAMASTER.area_name,'') AS AREA, ISNULL(LEDGERS.Acc_range,'') AS RANGE, ISNULL(CITYMASTER.city_name,'') AS CITY, ISNULL(LEDGERS.Acc_zipcode, 0) AS zipcode,  ISNULL(STATEMASTER.state_name,'') AS STATE, ISNULL(COUNTRYMASTER.country_name,'') AS COUNTRY, ISNULL(LEDGERS.ACC_TYPE,'') AS TYPE, ISNULL(AGENTLEDGERS.ACC_CMPNAME,'') AS AGENTNAME, ISNULL(TRANSLEDGERS.ACC_CMPNAME, '') AS TRANSPORT, ISNULL(LEDGERS.Acc_crdays, 0) AS CRDAYS, ISNULL(LEDGERS.Acc_crlimit, 0) AS CRLIMIT, ISNULL(LEDGERS.Acc_resino,'') AS RESINO, ISNULL(LEDGERS.Acc_phone,'') AS phone, ISNULL(LEDGERS.Acc_altno,'') AS ALTNO, ISNULL(LEDGERS.Acc_mobile,'') AS MOBILE, ISNULL(LEDGERS.ACC_BOSSMOBILE,'') AS BOSSMOBILE, ISNULL(LEDGERS.Acc_fax,'') AS FAX, ISNULL(LEDGERS.Acc_add,'') AS [ADD], ISNULL(LEDGERS.ACC_KMS,0) AS KMS, ISNULL(LEDGERS.Acc_website,'') AS WEBSITE, ISNULL(LEDGERS.Acc_email,'') AS EMAIL, ISNULL(LEDGERS.Acc_panno,'') AS PANNO, ISNULL(LEDGERS.ACC_CSTNO,'') AS CSTNO, ISNULL(LEDGERS.ACC_VATNO,'') AS VATNO, ISNULL(LEDGERS.Acc_stno,'') AS STNO, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(LEDGERS.ACC_DISC, 0) AS DISC, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_ADDLESS,'') AS ADDLESS, ISNULL(CAST(LEDGERS.Acc_shippingadd AS VARCHAR(500)),'') AS SHIPPINGADD ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN PARTYBANKMASTER ON LEDGERS.ACC_BANKID = PARTYBANKMASTER.PARTYBANK_id LEFT OUTER JOIN CITYMASTER AS CITYMASTER ON LEDGERS.Acc_cityid = CITYMASTER.city_id  LEFT OUTER JOIN  AREAMASTER ON LEDGERS.Acc_areaid = AREAMASTER.area_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN COUNTRYMASTER ON LEDGERS.Acc_countryid = COUNTRYMASTER.country_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON LEDGERS.ACC_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN  LEDGERS AS TRANSLEDGERS ON LEDGERS.ACC_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN ACCOUNTSMASTER_TDS ON LEDGERS.Acc_id = ACCOUNTSMASTER_TDS.ACC_ID ", WHERECLAUSE & " AND LEDGERS.ACC_YEARID = " & YearId & " ORDER BY LEDGERS.ACC_CMPNAME")
            gridbilldetails.DataSource = dt
            If dt.Rows.Count > 0 Then gridbill.FocusedRowHandle = gridbill.RowCount - 1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            showform(False, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String)
        Try
            If (editval = True And USEREDIT = False And USERVIEW = False) Or (editval = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If (editval = False) Or (editval = True And gridbill.RowCount > 0) Then
                Dim objaccountsmaster As New AccountsMaster
                objaccountsmaster.MdiParent = MDIMain
                objaccountsmaster.edit = editval
                objaccountsmaster.frmstring = "ACCOUNTS"
                objaccountsmaster.tempAccountsName = name
                objaccountsmaster.Show()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Ledger Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Ledger Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Ledger Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Ledger Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
        'Try
        '    Dim PATH As String = ""
        '    If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
        '    PATH = Application.StartupPath & "\Ledger Details.XLS"

        '    Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
        '    opti.ShowGridLines = True
        '    For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
        '        proc.Kill()
        '    Next

        '    Dim PERIOD As String = ""
        '    PERIOD = AccFrom & " - " & AccTo

        '    opti.SheetName = "Ledger Details"
        '    gridbill.ExportToXls(PATH, opti)
        '    EXCELCMPHEADER(PATH, "Ledger Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub


    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        showform(True, gridbill.GetFocusedRowCellValue("CMPNAME"))
    End Sub

    Private Sub LedgerDetailsReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid("")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("CMPNAME"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class