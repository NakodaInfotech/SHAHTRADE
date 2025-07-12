
Imports BL

Public Class SelectChallan

    Public DT As New DataTable

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SelectChallan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            If ClientName = "GELATO" Then
                DT = OBJCMN.search(" DISTINCT CAST(0 AS BIT) AS CHK,  GDNGELATO.GDN_NO AS CHALLANNO, GDNGELATO.GDN_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GDNGELATO.GDN_TOTALQTY, 0) AS QTY, ISNULL(GDNGELATO.GDN_TOTALAMT, 0) AS GTOTAL", "", " GDNGELATO INNER JOIN LEDGERS ON GDNGELATO.GDN_LEDGERID = LEDGERS.Acc_id", " AND  GDNGELATO.GDN_DONE=0 AND GDNGELATO.GDN_YEARID = " & YearId & " ORDER BY GDNGELATO.GDN_no")
            Else
                DT = OBJCMN.search(" DISTINCT CAST(0 AS BIT) AS CHK,  GDN.GDN_NO AS CHALLANNO, GDN.GDN_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ISNULL(GDN.GDN_TOTALQTY, 0) AS QTY, ISNULL(GDN.GDN_TOTALAMT, 0) AS GTOTAL", "", " GDN INNER JOIN LEDGERS ON GDN.GDN_LEDGERID = LEDGERS.Acc_id", " AND  GDN.GDN_DONE=0 AND GDN.GDN_YEARID = " & YearId & " ORDER BY GDN.GDN_no")
            End If
            gridchallandetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridchallan.FocusedRowHandle = gridchallan.RowCount - 1
                gridchallan.TopRowIndex = gridchallan.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim COUNT As Integer
            For i As Integer = 0 To gridchallan.RowCount - 1
                Dim dtrow As DataRow = gridchallan.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next

            If COUNT > 1 Then
                MsgBox("You Can Select Only One Challan No")
                Exit Sub
            End If

            DT.Columns.Add("CHALLANNO")
           
            For i As Integer = 0 To gridchallan.RowCount - 1
                Dim dtrow As DataRow = gridchallan.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("CHALLANNO"))
                End If
            Next

            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class