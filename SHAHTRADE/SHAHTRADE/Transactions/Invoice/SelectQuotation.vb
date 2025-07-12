

Imports System.Windows.Forms
Imports BL

Public Class SelectQuotation
    Dim addcol As Integer = 0
    Public DT As New DataTable
    Dim N As Integer = 0
    Dim tempindex, i As Integer
    Dim col As New DataGridViewCheckBoxColumn  'Dim dt As New DataTable
    Public ENQname As String = ""  'for whereclause in fillgrid
    Public TYPE As String = ""
    Public FRMSTRING As String = ""

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub SelectQuotation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub SelectQuotation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Top = 100
        fillgrid()
    End Sub

    Sub fillgrid(Optional ByVal where As String = "")
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            DT = OBJCMN.search(" CAST (0 AS BIT) AS CHK ,SALEQUOTATION.QUOTE_NO AS QUOTENO, SALEQUOTATION.QUOTE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(SALEQUOTATION.QUOTE_TOTALQTY, 0) AS TOTALQTY, ISNULL(SALEQUOTATION.QUOTE_GTOTAL, 0) AS GTOTAL ", "", "  SALEQUOTATION INNER JOIN LEDGERS ON SALEQUOTATION.QUOTE_LEDGERID = LEDGERS.Acc_id", " AND SALEQUOTATION.QUOTE_DONE = 0 AND SALEQUOTATION.QUOTE_YEARID = " & YearId & " ORDER BY SALEQUOTATION.QUOTE_no")

            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            'Dim COUNT As Integer
            'For i As Integer = 0 To gridbill.RowCount - 1
            '    Dim dtrow As DataRow = gridbill.GetDataRow(i)
            '    If Convert.ToBoolean(dtrow("CHK")) = True Then
            '        COUNT = COUNT + 1
            '    End If
            'Next

            'If COUNT > 1 Then
            '    MsgBox("You Can Select Only One Order")
            '    Exit Sub
            'End If

            DT.Columns.Add("QUOTENO")

            For i As Integer = 0 To gridbill.RowCount - 1
                Dim dtrow As DataRow = gridbill.GetDataRow(i)
                If Convert.ToBoolean(dtrow("CHK")) = True Then
                    DT.Rows.Add(dtrow("QUOTENO"))
                End If
            Next

            Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

End Class