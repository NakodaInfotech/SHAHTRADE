
Imports BL

Public Class SelectLabourIssue

    Public LABOURNAME As String = ""
    Public DT As New DataTable

    Sub fillgrid(ByVal WHERE As String)
        Try
            If LABOURNAME <> "" Then WHERE = WHERE & " AND LEDGERS.ACC_CMPNAME = '" & LABOURNAME & "'"
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" CAST(0 AS BIT) AS CHK, LABOURISSUE.ISS_NO AS ISSUENO, LABOURISSUE.ISS_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, ITEMMASTER.ITEM_name AS ITEMNAME, PATTAMASTER.PATTA_name AS PATTA, SIZEMASTER.SIZE_name AS SIZE, LABOURISSUE.ISS_QTY AS ISSQTY, LABOURISSUE.ISS_AVG AS AVG, LABOURISSUE.ISS_TOTALQTY AS TOTALQTY, LABOURISSUE.ISS_RECDQTY AS RECDQTY, LABOURISSUE.ISS_REMARKS AS REMARKS, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ROUND(ISS_TOTALQTY - ISS_RECDQTY,0) AS BALQTY ", "", "LABOURISSUE INNER JOIN LEDGERS ON LABOURISSUE.ISS_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON LABOURISSUE.ISS_ITEMID = ITEMMASTER.ITEM_id INNER JOIN PATTAMASTER ON LABOURISSUE.ISS_PATTAID = PATTAMASTER.PATTA_id INNER JOIN SIZEMASTER ON LABOURISSUE.ISS_SIZEID = SIZEMASTER.SIZE_id  ", WHERE & " AND ISNULL(ISS_CLOSED,0) = 0 AND ROUND(ISS_TOTALQTY - ISS_RECDQTY,0) > 0 AND ISS_YEARID = " & YearId)
            GRIDBILLDETAILS.DataSource = DT
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Dim COUNT As Integer = 0
            For i As Integer = 0 To GRIDBILL.RowCount - 1
                Dim dtrow As DataRow = GRIDBILL.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    COUNT = COUNT + 1
                End If
            Next
            If COUNT > 1 Then
                MsgBox("You Can Select Only One Entry")
                Exit Sub
            End If


            DT.Columns.Add("ISSUENO")
            DT.Columns.Add("DATE")
            DT.Columns.Add("NAME")
            DT.Columns.Add("ITEMNAME")
            DT.Columns.Add("PATTA")
            DT.Columns.Add("SIZE")
            DT.Columns.Add("ISSQTY")
            DT.Columns.Add("AVG")
            DT.Columns.Add("TOTALQTY")
            DT.Columns.Add("RATE")
            DT.Columns.Add("RECDQTY")

            For i As Integer = 0 To GRIDBILL.RowCount - 1
                Dim dtrow As DataRow = GRIDBILL.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("ISSUENO"), dtrow("DATE"), dtrow("NAME"), dtrow("ITEMNAME"), dtrow("PATTA"), dtrow("SIZE"), dtrow("ISSQTY"), dtrow("AVG"), dtrow("TOTALQTY"), dtrow("RATE"), dtrow("RECDQTY"))
                End If
            Next
            Me.Close()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SelectLabourIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectLabourIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fillgrid("")
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

End Class