Imports BL
Public Class MaterialConsumptionDetails
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

    Sub showform(ByVal EDITVAL As Boolean, ByVal Consumptionno As Integer)
        Try
            If (EDITVAL = True And USEREDIT = False And USERVIEW = False) Or (EDITVAL = False And USERADD = False) Then
                MsgBox("Insfficient Rights")
            End If
            If (EDITVAL = False) Or (EDITVAL = True And gridbill.RowCount > 0) Then
                Dim OBJMID As New MaterialConsumption
                OBJMID.MdiParent = MDIMain
                OBJMID.EDIT = EDITVAL
                OBJMID.TEMPCONSUMPTIONNO = Consumptionno
                OBJMID.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJMIGD As New MaterialConsumptionGridDetails
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
            Dim DT As DataTable = OBJMID.search("  MATERIALCONSUMPTION.CONSUME_NO AS CONSUMENO, MATERIALCONSUMPTION.CONSUME_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, MATERIALCONSUMPTION.CONSUME_REFNO AS REFNO, 
                         MATERIALCONSUMPTION.CONSUME_REMARKS AS REMARKS, MATERIALCONSUMPTION.CONSUME_TOTALQTY AS TOTALQTY ", "", "   MATERIALCONSUMPTION INNER JOIN
                         LEDGERS ON MATERIALCONSUMPTION.CONSUME_LEDGERID = LEDGERS.Acc_id ", " AND MATERIALCONSUMPTION.CONSUME_YEARID = " & YearId & " ORDER BY MATERIALCONSUMPTION.CONSUME_NO ")
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
            Dim PATH As String = Application.StartupPath & "\Material Consumption.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Material Consumption"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Material Consumption", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Material Consumption Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
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

    Private Sub CMDOK_Click(sender As Object, e As EventArgs) Handles CMDOK.Click
        Try
            showform(True, gridbill.GetFocusedRowCellValue("CONSUMENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialConsumptionDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            showform(True, gridbill.GetFocusedRowCellValue("CONSUMENO"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialConsumptionDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
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