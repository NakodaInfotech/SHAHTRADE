
Imports BL
Imports System.Windows.Forms
Imports System.IO
Imports System.Diagnostics

Public Class OpeningStock

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Dim CLEAR As Boolean = False
    Public EDIT As Boolean
    Public tempMsg As Integer
    Public FRMSTRING As String

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If gridstock.RowCount = 0 Then
            EP.SetError(TXTPARTNO, "Enter Part No.")
            bln = False
        End If

        For Each row As DataGridViewRow In gridstock.Rows
            If Val(row.Cells(GPARTNO.Index).Value) = 0 Then
                EP.SetError(CMBITEMNAME, "Pcs Cannot be 0")
                bln = False
            End If
            If Val(row.Cells(GITEMNAME.Index).Value) = 0 Then
                EP.SetError(CMBITEMNAME, "Mtrs Cannot be 0")
                bln = False
            End If
            If row.Cells(GRATE.Index).Value = "" Then
                EP.SetError(CMBITEMNAME, "Quality cannot be Blank")
                bln = False
            End If
            'If row.Cells(GMERCHANT.Index).Value = "" Then
            '    EP.SetError(cmbtype, "Item Name cannot be Blank")
            '    bln = False
            'End If
            'If row.Cells(Gunit.Index).Value = "" Then
            '    EP.SetError(cmbtype, "Unit cannot be Blank")
            '    bln = False
            'End If
            'If cmbtype.Text = "INHOUSE" Then
            '    If row.Cells(GDESIGN.Index).Value = "" Then
            '        EP.SetError(cmbtype, "Design cannot be Blank")
            '        bln = False
            '    End If
            'ElseIf cmbtype.Text = "JOBBERSTOCK" Then
            '    If row.Cells(gtoname.Index).Value = "" Then
            '        EP.SetError(cmbtype, "Jobber Name cannot be Blank")
            '        bln = False
            '    End If
            'End If
        Next
        Return bln
    End Function

    Sub EDITROW()
        Try
            If gridstock.CurrentRow.Index >= 0 And gridstock.Item(gsrno.Index, gridstock.CurrentRow.Index).Value <> Nothing Then

                If Convert.ToBoolean(gridstock.Rows(gridstock.CurrentRow.Index).Cells(gdone.Index).Value) = True Then 'If row.Cells(16).Value <> "0" Then 
                    MsgBox("Item Locked", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                GRIDDOUBLECLICK = True
                TXTNO.Text = gridstock.Item(GNO.Index, gridstock.CurrentRow.Index).Value.ToString
                txtsrno.Text = gridstock.Item(gsrno.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTPARTNO.Text = gridstock.Item(GPARTNO.Index, gridstock.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = gridstock.Item(GITEMNAME.Index, gridstock.CurrentRow.Index).Value.ToString
                TXTRATE.Text = gridstock.Item(GRATE.Index, gridstock.CurrentRow.Index).Value.ToString
                TEMPROW = gridstock.CurrentRow.Index
                txtsrno.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridSO_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridstock.CellDoubleClick
        EDITROW()
    End Sub

    'Private Sub cmbname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbname.KeyDown
    '    Try
    '        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
    '        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

    '        If e.KeyCode = Keys.F1 Then
    '            Dim OBJLEDGER As New SelectLedger
    '            OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors' and LEDGERS.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.acc_YEARid = " & YearId
    '            OBJLEDGER.ShowDialog()
    '            If OBJLEDGER.TEMPNAME <> "" Then cmbname.Text = OBJLEDGER.TEMPNAME
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
    '    Try
    '        If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS")
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    'Private Sub cmbname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.GotFocus
    '    Try
    '        If cmbname.Text.Trim = "" Then fillname(cmbname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    'Private Sub cmbtoname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbtoname.Validating
    '    Try
    '        If cmbtoname.Text.Trim <> "" Then namevalidate(cmbtoname, CMBCODE, e, Me, txtadd, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'", "Sundry Creditors", "ACCOUNTS")
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    'Private Sub cmbtoname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbtoname.GotFocus
    '    Try
    '        If cmbtoname.Text.Trim = "" Then fillname(cmbtoname, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'Sundry Creditors'")
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub cmbMERCHANT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            'If TXTPARTNO.Text.Trim = "" And CMBITEMNAME.Text.Trim = "" Then txtremarks.Focus()
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        gridstock.Enabled = True

        If GRIDDOUBLECLICK = False Then
            gridstock.Rows.Add(Val(txtsrno.Text.Trim), Val(TXTNO.Text.Trim), TXTPARTNO.Text.Trim, CMBITEMNAME.Text.Trim, Val(TXTRATE.Text.Trim), 0, 0, 0)
            getsrno(gridstock)
            gridstock.FirstDisplayedScrollingRowIndex = gridstock.RowCount - 1
        ElseIf GRIDDOUBLECLICK = True Then
            gridstock.Item(gsrno.Index, TEMPROW).Value = Val(txtsrno.Text.Trim)
            gridstock.Item(GPARTNO.Index, TEMPROW).Value = TXTPARTNO.Text.Trim
            gridstock.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            gridstock.Item(GRATE.Index, TEMPROW).Value = TXTRATE.Text.Trim
            GRIDDOUBLECLICK = False
        End If

        If CLEAR = True Then
            txtsrno.Text = gridstock.RowCount + 1
            CMBITEMNAME.Text = ""
            TXTPARTNO.Clear()
            TXTRATE.Clear()
            TXTNO.Clear()
            'If ClientName = "KDFAB" Or ClientName = "SANGHVI" Then txtMtrs.Focus() Else TXTLOTNO.Focus()
        End If

    End Sub

    Private Sub OpeningStock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.F5 Then
            gridstock.Focus()
        ElseIf e.KeyCode = Keys.F12 And gridstock.RowCount > 0 Then
            If gridstock.SelectedRows.Count = 0 Then Exit Sub
            Dim TEMPROWINDEX As Integer = gridstock.CurrentRow.Index
            TXTPARTNO.Text = gridstock.Item(GPARTNO.Index, TEMPROWINDEX).Value
            CMBITEMNAME.Text = gridstock.Item(GITEMNAME.Index, TEMPROWINDEX).Value
            TXTRATE.Text = gridstock.Item(GRATE.Index, TEMPROWINDEX).Value
           
            txtsrno.Focus()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub OpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'OPENING'")
            'USERADD = DTROW(0).Item(1)
            'USEREDIT = DTROW(0).Item(2)
            'USERVIEW = DTROW(0).Item(3)
            'USERDELETE = DTROW(0).Item(4)

            fillcmb()
            openingdate.Value = AccFrom.Date

            'CMBITEMNAME.Text = "FRESH"
            'If USEREDIT = False And USERVIEW = False Then
            '    'MsgBox("Insufficient Rights")
            '    Exit Sub
            'End If

            Dim OBJCMN As New ClsCommon
            Dim dttable As New DataTable
            'dttable = OBJCMN.search("  STOCKMASTER.SM_TYPE AS TYPE, STOCKMASTER.SM_DATE AS DATE, ISNULL(STOCKMASTER.SM_GRIDSRNO, 0) AS GRIDSRNO, STOCKMASTER.SM_NO AS SMNO, ISNULL(STOCKMASTER.SM_LOTNO, '') AS LOTNO, ISNULL(PIECETYPEMASTER.PIECETYPE_name, '') AS PIECETYPE, ISNULL(ITEMMASTER.item_name, '') AS MERCHANT, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(PROCESSMASTER.PROCESS_NAME, '') AS PROCESS, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME,  ISNULL(TONAME.Acc_cmpname, '') AS TONAME, ISNULL(STOCKMASTER.SM_BILLNO, '') AS BILLNO, ISNULL(GODOWNMASTER.GODOWN_name, '') AS GODOWN, STOCKMASTER.SM_CUT AS CUT, ISNULL(STOCKMASTER.SM_WT, 0) AS WT, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(STOCKMASTER.SM_PCS, 0) AS PCS, ISNULL(STOCKMASTER.SM_MTRS, 0) AS MTRS, ISNULL(STOCKMASTER.SM_RATE, 0) AS RATE, ISNULL(STOCKMASTER.SM_AMOUNT, 0) AS AMOUNT, ISNULL(STOCKMASTER.SM_REMARKS, '') AS REMARKS, ISNULL(STOCKMASTER.SM_BARCODE, '') AS BARCODE, ISNULL(STOCKMASTER.SM_DONE, 0) AS DONE, ISNULL(STOCKMASTER.SM_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER.SM_OUTMTRS, 0) AS OUTMTRS, STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_LOCATIONID AS LOCATIONID, STOCKMASTER.SM_YEARID AS YEARID, ISNULL(SHELFMASTER.SHELF_NAME, '') AS SHELF, ISNULL(RACKMASTER.RACK_NAME, '') AS RACK ", "", " STOCKMASTER LEFT OUTER JOIN SHELFMASTER ON STOCKMASTER.SM_SHELFID = SHELFMASTER.SHELF_ID LEFT OUTER JOIN RACKMASTER ON STOCKMASTER.SM_RACKID = RACKMASTER.RACK_ID LEFT OUTER JOIN LEDGERS AS TONAME ON STOCKMASTER.SM_YEARID = TONAME.Acc_yearid AND STOCKMASTER.SM_LOCATIONID = TONAME.Acc_locationid AND STOCKMASTER.SM_CMPID = TONAME.Acc_cmpid AND STOCKMASTER.SM_LEDGERIDTO = TONAME.Acc_id LEFT OUTER JOIN DESIGNMASTER ON STOCKMASTER.SM_YEARID = DESIGNMASTER.DESIGN_yearid AND STOCKMASTER.SM_LOCATIONID = DESIGNMASTER.DESIGN_locationid AND STOCKMASTER.SM_CMPID = DESIGNMASTER.DESIGN_cmpid AND STOCKMASTER.SM_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN GODOWNMASTER ON STOCKMASTER.SM_GODOWNID = GODOWNMASTER.GODOWN_id AND STOCKMASTER.SM_CMPID = GODOWNMASTER.GODOWN_cmpid AND STOCKMASTER.SM_LOCATIONID = GODOWNMASTER.GODOWN_locationid AND  STOCKMASTER.SM_YEARID = GODOWNMASTER.GODOWN_yearid LEFT OUTER JOIN PIECETYPEMASTER ON STOCKMASTER.SM_PIECETYPEID = PIECETYPEMASTER.PIECETYPE_id AND STOCKMASTER.SM_CMPID = PIECETYPEMASTER.PIECETYPE_cmpid AND STOCKMASTER.SM_LOCATIONID = PIECETYPEMASTER.PIECETYPE_locationid AND STOCKMASTER.SM_YEARID = PIECETYPEMASTER.PIECETYPE_yearid LEFT OUTER JOIN QUALITYMASTER ON STOCKMASTER.SM_QUALITYID = QUALITYMASTER.QUALITY_id AND STOCKMASTER.SM_CMPID = QUALITYMASTER.QUALITY_cmpid AND STOCKMASTER.SM_LOCATIONID = QUALITYMASTER.QUALITY_locationid AND STOCKMASTER.SM_YEARID = QUALITYMASTER.QUALITY_yearid LEFT OUTER JOIN PROCESSMASTER ON STOCKMASTER.SM_YEARID = PROCESSMASTER.PROCESS_YEARID AND STOCKMASTER.SM_LOCATIONID = PROCESSMASTER.PROCESS_LOCATIONID AND STOCKMASTER.SM_CMPID = PROCESSMASTER.PROCESS_CMPID AND STOCKMASTER.SM_PROCESSID = PROCESSMASTER.PROCESS_ID LEFT OUTER JOIN LEDGERS ON STOCKMASTER.SM_LEDGERID = LEDGERS.Acc_id AND STOCKMASTER.SM_CMPID = LEDGERS.Acc_cmpid AND STOCKMASTER.SM_LOCATIONID = LEDGERS.Acc_locationid AND STOCKMASTER.SM_YEARID = LEDGERS.Acc_yearid LEFT OUTER JOIN UNITMASTER ON STOCKMASTER.SM_UNITID = UNITMASTER.unit_id AND STOCKMASTER.SM_CMPID = UNITMASTER.unit_cmpid AND STOCKMASTER.SM_LOCATIONID = UNITMASTER.unit_locationid AND STOCKMASTER.SM_YEARID = UNITMASTER.unit_yearid LEFT OUTER JOIN COLORMASTER ON STOCKMASTER.SM_COLORID = COLORMASTER.COLOR_id AND STOCKMASTER.SM_CMPID = COLORMASTER.COLOR_cmpid AND  STOCKMASTER.SM_LOCATIONID = COLORMASTER.COLOR_locationid AND STOCKMASTER.SM_YEARID = COLORMASTER.COLOR_yearid LEFT OUTER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.item_id AND STOCKMASTER.SM_CMPID = ITEMMASTER.item_cmpid AND STOCKMASTER.SM_LOCATIONID = ITEMMASTER.item_locationid AND STOCKMASTER.SM_YEARID = ITEMMASTER.item_yearid  ", " AND STOCKMASTER.SM_TYPE = '" & FRMSTRING & "' AND STOCKMASTER.SM_CMPID = " & CmpId & " AND STOCKMASTER.SM_LOCATIONID  = " & Locationid & " AND STOCKMASTER.SM_YEARID = " & YearId & " ORDER BY SM_NO")
            dttable = OBJCMN.search("  STOCKMASTER.SM_DATE AS DATE, ISNULL(STOCKMASTER.SM_GRIDSRNO, 0) AS GRIDSRNO, STOCKMASTER.SM_NO AS SMNO, ISNULL(STOCKMASTER.SM_PARTNO, '') AS PARTNO, ISNULL(STOCKMASTER.SM_RATE, 0) AS RATE, ISNULL(STOCKMASTER.SM_DONE, 0) AS DONE, ISNULL(STOCKMASTER.SM_OUTPCS, 0) AS OUTPCS, ISNULL(STOCKMASTER.SM_OUTMTRS, 0) AS OUTMTRS, STOCKMASTER.SM_CMPID AS CMPID, STOCKMASTER.SM_YEARID AS YEARID, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME ", "", " STOCKMASTER INNER JOIN ITEMMASTER ON STOCKMASTER.SM_ITEMID = ITEMMASTER.ITEM_id  ", " AND STOCKMASTER.SM_CMPID = " & CmpId & " AND STOCKMASTER.SM_YEARID = " & YearId & " ORDER BY SM_NO")

            If dttable.Rows.Count > 0 Then
                For Each DR As DataRow In dttable.Rows
                    openingdate.Value = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")

                    gridstock.Rows.Add(DR("GRIDSRNO"), DR("SMNO"), DR("PARTNO"), DR("ITEMNAME"), Format(DR("RATE"), "0.00"), DR("DONE"), Format(DR("OUTPCS"), "0.00"), Format(DR("OUTMTRS"), "0.00"))
                    If Convert.ToBoolean(DR("DONE")) = True Then gridstock.Rows(gridstock.RowCount - 1).DefaultCellStyle.BackColor = Color.Yellow
                Next
                getsrno(gridstock)
                gridstock.FirstDisplayedScrollingRowIndex = gridstock.RowCount - 1
            End If

            If gridstock.RowCount > 0 Then
                txtsrno.Text = Val(gridstock.Rows(gridstock.RowCount - 1).Cells(0).Value) + 1
            Else
                txtsrno.Text = 1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsStockMaster

            ALPARAVAL.Add(openingdate.Value.Date)
           
            ALPARAVAL.Add(txtsrno.Text.Trim)
            ALPARAVAL.Add(TXTPARTNO.Text.Trim)
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(Val(TXTRATE.Text.Trim))
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJSM.alParaval = ALPARAVAL
            If GRIDDOUBLECLICK = False Then
                Dim DT As DataTable = OBJSM.save()
                If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)
            Else
                ALPARAVAL.Add(TXTNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridstock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridstock.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridstock.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                If Val(gridstock.Rows(gridstock.CurrentRow.Index).Cells(goutmtrs.Index).Value) > 0 Then
                    MessageBox.Show("Row Locked, You Cannot Delete This Row")
                    Exit Sub
                End If

                Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'DELETE FROM STOCKMASTER
                Dim OBJSM As New ClsStockMaster
                Dim ALPARAVAL As New ArrayList
                ALPARAVAL.Add(gridstock.Rows(gridstock.CurrentRow.Index).Cells(GNO.Index).Value)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(YearId)

                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.DELETE()

                gridstock.Rows.RemoveAt(gridstock.CurrentRow.Index)
                getsrno(gridstock)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmerchant_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBITEMNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJItem As New SelectItem

                OBJItem.STRSEARCH = " and ITEM_cmpid = " & CmpId & " and ITEM_YEARid = " & YearId
                OBJItem.ShowDialog()
                If OBJItem.TEMPNAME <> "" Then CMBITEMNAME.Text = OBJItem.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREMARKS_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating
        If CMBITEMNAME.Text.Trim <> "" And TXTPARTNO.Text.Trim <> "" And Val(TXTRATE.Text.Trim) > 0 Then
            SAVE()
            CLEAR = True
            fillgrid()
        Else
            MsgBox("Enter Proper Details")
            Exit Sub
        End If
    End Sub
End Class