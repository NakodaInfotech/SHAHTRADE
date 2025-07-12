Imports BL

Public Class MaterialOutwardDetails

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

    Sub showform(ByVal EDITVAL As Boolean, ByVal OUTWARDno As Integer)
        Try
            If (EDITVAL = True And USEREDIT = False And USERVIEW = False) Or (EDITVAL = False And USERADD = False) Then
                MsgBox("Insfficient Rights")
            End If
            If (EDITVAL = False) Or (EDITVAL = True And gridbill.RowCount > 0) Then
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

    Sub fillgrid()
        Try
            If (USEREDIT = False And USERVIEW = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Dim OBJMID As New ClsCommon
            Dim DT As DataTable = OBJMID.search(" MATERIALOUTWARD.OUTWARD_NO AS OUTWARDNO, MATERIALOUTWARD.OUTWARD_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, MATERIALOUTWARD.OUTWARD_REFNO AS REFNO, MATERIALOUTWARD.OUTWARD_REMARKS AS REMARKS, MATERIALOUTWARD.OUTWARD_TOTALQTY AS TOTALQTY, MATERIALOUTWARD.OUTWARD_TOTALAMOUNT AS AMOUNT, MATERIALOUTWARD.OUTWARD_TOTALDISCAMT AS TOTALDISCAMT, MATERIALOUTWARD.OUTWARD_TOTALOTHERCHARGES AS TOTALOTHERCHGS, MATERIALOUTWARD.OUTWARD_TOTALTAXABLEAMT AS TOTAL, MATERIALOUTWARD.OUTWARD_TOTALCGSTAMT AS TOTALCGSTAMT, MATERIALOUTWARD.OUTWARD_TOTALSGSTAMT AS TOTALSGSTAMT, MATERIALOUTWARD.OUTWARD_TOTALIGSTAMT AS TOTALIGSTAMT, MATERIALOUTWARD.OUTWARD_TOTALGRIDTOTAL AS GRIDTOTAL, MATERIALOUTWARD.OUTWARD_GTOTAL AS GTOTAL ", "", " MATERIALOUTWARD INNER JOIN LEDGERS ON MATERIALOUTWARD.OUTWARD_LEDGERID = LEDGERS.Acc_id ", " AND MATERIALOUTWARD.OUTWARD_YEARID  = " & YearId & " ORDER BY MATERIALOUTWARD.OUTWARD_NO ")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = -1
                gridbill.TopRowIndex = -15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TOOLGRIDDETAILS_Click(sender As Object, e As EventArgs) Handles TOOLGRIDDETAILS.Click
        Try
            Dim OBJMIGD As New MaterialOutWardGridDetails
            OBJMIGD.MdiParent = MDIMain
            OBJMIGD.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MaterialOUTWARDDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
            showform(True, gridbill.GetFocusedRowCellValue("OUTWARDNO"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MaterialOutWardDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
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