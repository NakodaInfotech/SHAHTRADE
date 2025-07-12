Imports BL

Public Class MaterialInwardDetails

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean

    Private Sub CMDEXIT_Click(sender As Object, e As EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDADD_Click(sender As Object, e As EventArgs) Handles CMDADD.Click
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

    Sub showform(ByVal EDITVAL As Boolean, ByVal Inwardno As Integer)
        Try
            If (EDITVAL = True And USEREDIT = False And USERVIEW = False) Or (EDITVAL = False And USERADD = False) Then
                MsgBox("Insfficient Rights")
            End If
            If (EDITVAL = False) Or (EDITVAL = True And gridbill.RowCount > 0) Then
                Dim OBJMID As New MaterialInward
                OBJMID.MdiParent = MDIMain
                OBJMID.EDIT = EDITVAL
                OBJMID.TEMPINWARDNO = Inwardno
                OBJMID.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJMIGD As New MaterialInwardGridDetails
            OBJMIGD.MdiParent = MDIMain
            OBJMIGD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillgrid()
        Try
            If (USEREDIT = False And USERVIEW = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJMID As New ClsCommon
            Dim DT As DataTable = OBJMID.search(" MATERIALINWARD.INWARD_NO AS ISSUENO, MATERIALINWARD.INWARD_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, MATERIALINWARD.INWARD_CHALLANNO AS CHALLANNO, MATERIALINWARD.INWARD_CHALLANDATE AS CHALLANDATE, MATERIALINWARD.INWARD_REFNO AS REFNO, MATERIALINWARD.INWARD_REMARKS AS REMARKS, MATERIALINWARD.INWARD_TOTALQTY AS TOTALQTY ", "", " MATERIALINWARD INNER JOIN LEDGERS ON MATERIALINWARD.INWARD_LEDGERID = LEDGERS.Acc_id ", "  AND MATERIALINWARD.INWARD_YEARID = " & YearId & " ORDER BY MATERIALINWARD.INWARD_NO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = -1
                gridbill.TopRowIndex = -15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub TOOLEXCEL_Click(sender As Object, e As EventArgs) Handles TOOLEXCEL.Click
        Try
            Dim PATH As String = Application.StartupPath & "\Material Inward.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Material Inward"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Material Inward", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Material Inward Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TOOLREFRESH_Click(sender As Object, e As EventArgs) Handles TOOLREFRESH.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insfficient Rights")
                Exit Sub
            End If
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialInwardDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub gridbill_DoubleClick(sender As Object, e As EventArgs) Handles gridbill.DoubleClick
        Try
            showform(True, gridbill.GetFocusedRowCellValue("ISSUENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialInwardDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
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
End Class