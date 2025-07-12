
Imports BL
Imports DevExpress.XtraEditors.Controls

Public Class UpdateHUID

    Private Sub UpdateHUID_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateHUID_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim WHERECLAUSE As String = ""
            If RBPENDING.Checked = True Then WHERECLAUSE = " AND ISNULL(ISSUEVOUCHER_DESC.ISS_HUID,'') = '' " Else WHERECLAUSE = " AND ISNULL(ISSUEVOUCHER_DESC.ISS_HUID,'') <> ''"
            Dim OBJCMN As New ClsCommonMaster
            Dim DT As DataTable = OBJCMN.search(" ISSUEVOUCHER.ISS_NO AS SRNO, ISSUEVOUCHER.ISS_DATE AS DATE, LEDGERS.Acc_cmpname AS NAME, HALLMARKMASTER.HALLMARK_name AS HALLMARK, ISSUEVOUCHER.ISS_REQNO AS REQNO, ISSUEVOUCHER.ISS_VOUCHERWT AS VOUCHERWT, ISSUEVOUCHER.ISS_RECD AS RECD, ISSUEVOUCHER.ISS_RECDATE AS RECDATE, ISSUEVOUCHER.ISS_BOXWT AS BOXWT, ISSUEVOUCHER.ISS_WTWITHTAGS AS WTWITHTAGS, ISSUEVOUCHER.ISS_TOTALPCS AS TOTALPCS, ISSUEVOUCHER.ISS_TOTALGROSSWT AS TOTALGROSSWT, ISSUEVOUCHER.ISS_TOTALLESSWT AS TOTALLESSWT, ISSUEVOUCHER.ISS_TOTALNETTWT AS TOTALNETTWT, ISSUEVOUCHER.ISS_TOTALFINEWT AS TOTALFINEWT, ISSUEVOUCHER.ISS_REMARKS AS REMARKS, ISSUEVOUCHER_DESC.ISS_SRNO AS GRIDSRNO, ITEMMASTER.ITEM_name AS ITEMNAME, HSNMASTER.HSN_CODE AS HSNCODE, ISSUEVOUCHER_DESC.ISS_GRIDDESC AS GRIDREMARKS, ISSUEVOUCHER_DESC.ISS_PCS AS PCS, ISSUEVOUCHER_DESC.ISS_GROSSWT AS GROSSWT, ISSUEVOUCHER_DESC.ISS_LESSWT AS LESSWT, ISSUEVOUCHER_DESC.ISS_NETTWT AS NETTWT, ISSUEVOUCHER_DESC.ISS_PURITY AS PURITY, ISSUEVOUCHER_DESC.ISS_FINEWT AS FINEWT, ISSUEVOUCHER_DESC.ISS_HUID AS HUID  ", "", " ISSUEVOUCHER INNER JOIN ISSUEVOUCHER_DESC ON ISSUEVOUCHER.ISS_NO = ISSUEVOUCHER_DESC.ISS_NO AND ISSUEVOUCHER.ISS_YEARID = ISSUEVOUCHER_DESC.ISS_YEARID INNER JOIN LEDGERS ON ISSUEVOUCHER.ISS_LEDGERID = LEDGERS.Acc_id INNER JOIN ITEMMASTER ON ISSUEVOUCHER_DESC.ISS_ITEMID = ITEMMASTER.ITEM_id LEFT OUTER JOIN HSNMASTER ON ISSUEVOUCHER_DESC.ISS_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN HALLMARKMASTER ON ISSUEVOUCHER.ISS_HALLMARKID = HALLMARKMASTER.HALLMARK_id ", WHERECLAUSE & " AND ISSUEVOUCHER.ISS_YEARID = " & YearId & " ORDER BY ISSUEVOUCHER.ISS_NO, ISSUEVOUCHER_DESC.ISS_SRNO")
            gridbilldetails.DataSource = DT
            If DT.Rows.Count > 0 Then
                gridbill.FocusedRowHandle = gridbill.RowCount - 1
                gridbill.TopRowIndex = gridbill.RowCount - 15
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREFRESH.Click
        Try
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable
            For I As Integer = 0 To Val(gridbill.RowCount - 1)
                Dim DTROW As DataRow = gridbill.GetDataRow(I)
                If IsDBNull(DTROW("HUID")) = False AndAlso DTROW("HUID") <> "" Then
                    DT = OBJCMN.Execute_Any_String("UPDATE ISSUEVOUCHER_DESC SET ISS_HUID = '" & DTROW("HUID") & "' WHERE ISS_NO = " & Val(DTROW("SRNO")) & " AND ISS_SRNO = " & Val(DTROW("GRIDSRNO")) & " AND ISS_YEARID = " & YearId, "", "")
                End If
            Next
            FILLGRID()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try

            Dim ROW As DataRow = gridbill.GetFocusedDataRow
            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If IsDBNull(ROW("HUID")) = True Then
                MsgBox("No Row To Delete")
                Exit Sub
            End If

            If MsgBox("Delete HUID No?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            DT = OBJCMN.Execute_Any_String("UPDATE ISSUEVOUCHER_DESC SET ISS_HUID = '' WHERE ISS_NO = " & Val(ROW("SRNO")) & " AND ISS_SRNO = " & Val(ROW("GRIDSRNO")) & " AND ISS_YEARID = " & YearId, "", "")
            FILLGRID()
            gridbill.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class