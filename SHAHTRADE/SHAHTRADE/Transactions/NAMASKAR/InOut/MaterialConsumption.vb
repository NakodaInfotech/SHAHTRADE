Imports BL
Imports System.ComponentModel
Public Class MaterialConsumption
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPCONSUMPTIONNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        TXTCONSUMENO.Clear()
        CONSUMPTIONDATE.Text = Mydate
        TXTCHALLANNO.Clear()
        CHALLANDATE.Clear()
        CMBNAME.Text = ""
        TXTBARCODE.Clear()
        TXTREFNO.Clear()

        GRIDCONSUMPTION.RowCount = 0
        TXTSRNO.Text = 1
        CMBITEMNAME.Text = ""
        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTQTY.Clear()

        LBLTOTALQTY.Text = 0
        txtremarks.Clear()

        GETMAX_ISSUE_NO()
        EP.Clear()
        GRIDDOUBLECLICK = False

    End Sub

    Sub GETMAX_ISSUE_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(CONSUME_no),0) + 1 ", " MATERIALCONSUMPTION ", " and CONSUME_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCONSUMENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub



    Sub FILLCMB()
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        FILLITEMNAME(CMBITEMNAME, EDIT)
    End Sub

    Private Sub MATERIALCONSUMPTION_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'PURCHASE INVOICE'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor

            FILLCMB()
            CLEAR()

            If EDIT = True Then

                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DT As DataTable
                Dim ALPARAVAL As New ArrayList
                Dim OBJISS As New ClsMaterialConsumption

                ALPARAVAL.Add(TEMPCONSUMPTIONNO)
                ALPARAVAL.Add(YearId)

                OBJISS.alParaval = ALPARAVAL
                DT = OBJISS.SELECTMATERIALCONSUMPTION()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTCONSUMENO.Text = TEMPCONSUMPTIONNO
                        CONSUMPTIONDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        TXTCHALLANNO.Text = DR("CHALLANNO")
                        CHALLANDATE.Text = DR("CHALLANDATE")
                        TXTREFNO.Text = DR("REFNO")
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        GRIDCONSUMPTION.Rows.Add(Val(DR("SRNO")), DR("ITEMNAME").ToString, DR("HSNCODE"), DR("GRIDREMARKS"), Format(Val(DR("QTY")), "0.00"))

                    Next

                    GRIDCONSUMPTION.FirstDisplayedScrollingRowIndex = GRIDCONSUMPTION.RowCount - 1
                End If
                CONSUMPTIONDATE.Focus()
                TOTAL()
                GETSRNO(GRIDCONSUMPTION)
            Else
                EDIT = False
                CLEAR()
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.WaitCursor
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(CONSUMPTIONDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(TXTCHALLANNO.Text.Trim)
            ALPARAVAL.Add(CHALLANDATE.Text.Trim)
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim SRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim HSNCODE As String = ""
            Dim GRIDDESC As String = ""
            Dim QTY As String = ""


            For Each ROW As Windows.Forms.DataGridViewRow In GRIDCONSUMPTION.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        ITEMNAME = ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDDESC = ROW.Cells(GDESC.Index).Value.ToString
                        QTY = Val(ROW.Cells(GQTY.Index).Value)


                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        ITEMNAME = ITEMNAME & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDDESC = GRIDDESC & "|" & ROW.Cells(GDESC.Index).Value.ToString
                        QTY = QTY & "|" & Val(ROW.Cells(GQTY.Index).Value)
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(HSNCODE)
            ALPARAVAL.Add(GRIDDESC)
            ALPARAVAL.Add(QTY)



            Dim OBJPUR As New ClsMaterialConsumption
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.SAVE()
                MsgBox("Details Added !!")
                TEMPCONSUMPTIONNO = DTTABLE.Rows(0).Item(0)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPCONSUMPTIONNO)
                IntResult = OBJPUR.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            PRINTREPORT(TEMPCONSUMPTIONNO)

            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal INVNO As Integer)
        Try
            If MsgBox("Wish to CONSUMPTION Entry?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJISSOICE As New SaleInvoiceDesign
                OBJISSOICE.MdiParent = MDIMain
                OBJISSOICE.FRMSTRING = "MATERIALCONSUMPTION"
                OBJISSOICE.WHERECLAUSE = "{MATERIALCONSUMPTION.CONSUMPTION_no}=" & Val(INVNO) & " and {MATERIALCONSUMPTION.CONSUMPTION_yearid}=" & YearId
                OBJISSOICE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Enter Name")
            bln = False
        End If

        If GRIDCONSUMPTION.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If

        If CONSUMPTIONDATE.Text = "__/__/____" Then
            EP.SetError(CONSUMPTIONDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(CONSUMPTIONDATE.Text) Then
                EP.SetError(CONSUMPTIONDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub TOTAL()
        Try

            LBLTOTALQTY.Text = 0

            Dim dt As New DataTable
            Dim objcmn As New ClsCommon

            For Each row As DataGridViewRow In GRIDCONSUMPTION.Rows
                If Val(row.Cells(GQTY.Index).Value) > 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0.00")
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTSRNO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTSRNO.Enter
        If GRIDDOUBLECLICK = False Then
            If GRIDCONSUMPTION.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDCONSUMPTION.Rows(GRIDCONSUMPTION.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
    End Sub

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLGRID()

        GRIDCONSUMPTION.Enabled = True

        If GRIDDOUBLECLICK = False Then
            TXTSRNO.Text = GRIDCONSUMPTION.RowCount + 1
            GRIDCONSUMPTION.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, TXTHSNCODE.Text.Trim, TXTGRIDDESC.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"))
            GETSRNO(GRIDCONSUMPTION)

        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCONSUMPTION.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDCONSUMPTION.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text
            GRIDCONSUMPTION.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDCONSUMPTION.Item(GDESC.Index, TEMPROW).Value = TXTGRIDDESC.Text.Trim
            GRIDCONSUMPTION.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")

            GRIDDOUBLECLICK = False
        End If

        TOTAL()
        GRIDCONSUMPTION.FirstDisplayedScrollingRowIndex = GRIDCONSUMPTION.RowCount - 1

        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTQTY.Clear()
        TXTSRNO.Text = Val(GRIDCONSUMPTION.RowCount) + 1

        TXTBARCODE.Focus()

    End Sub

    Private Sub GRIDISSUE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCONSUMPTION.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDCONSUMPTION.CurrentRow.Index >= 0 And GRIDCONSUMPTION.Item(gsrno.Index, GRIDCONSUMPTION.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDCONSUMPTION.Item(gsrno.Index, GRIDCONSUMPTION.CurrentRow.Index).Value
                CMBITEMNAME.Text = GRIDCONSUMPTION.Item(GITEMNAME.Index, GRIDCONSUMPTION.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDCONSUMPTION.Item(GHSNCODE.Index, GRIDCONSUMPTION.CurrentRow.Index).Value.ToString
                TXTGRIDDESC.Text = GRIDCONSUMPTION.Item(GDESC.Index, GRIDCONSUMPTION.CurrentRow.Index).Value.ToString
                TXTQTY.Text = Val(GRIDCONSUMPTION.Item(GQTY.Index, GRIDCONSUMPTION.CurrentRow.Index).Value)

                TEMPROW = GRIDCONSUMPTION.CurrentRow.Index
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGROSSWT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTQTY.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 Then
                FILLGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDCONSUMPTION.RowCount = 0
LINE1:
            TEMPCONSUMPTIONNO = Val(TXTCONSUMENO.Text) - 1

            If TEMPCONSUMPTIONNO > 0 Then
                EDIT = True
                MATERIALCONSUMPTION_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

            If GRIDCONSUMPTION.RowCount = 0 And TEMPCONSUMPTIONNO > 1 Then
                TXTCONSUMENO.Text = TEMPCONSUMPTIONNO
                GoTo LINE1
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDCONSUMPTION.RowCount = 0
LINE1:
            TEMPCONSUMPTIONNO = Val(TXTCONSUMENO.Text) + 1
            GETMAX_ISSUE_NO()
            Dim MAXNO As Integer = TXTCONSUMENO.Text.Trim
            CLEAR()
            If Val(TXTCONSUMENO.Text) - 1 >= TEMPCONSUMPTIONNO Then
                EDIT = True
                MATERIALCONSUMPTION_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCONSUMPTION.RowCount = 0 And TEMPCONSUMPTIONNO < MAXNO Then
                TXTCONSUMENO.Text = TEMPCONSUMPTIONNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) = 0 Then Exit Sub
            GRIDCONSUMPTION.RowCount = 0
            TEMPCONSUMPTIONNO = Val(tstxtbillno.Text)
            If TEMPCONSUMPTIONNO > 0 Then
                EDIT = True
                MATERIALCONSUMPTION_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If EDIT = True Then
                Dim intresult As Integer
                Dim objpur As New ClsMaterialConsumption
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPCONSUMPTIONNO)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.DELETE
                    MsgBox("Entry Deleted!!!")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
            CLEAR()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJISS As New MaterialConsumptionDetails
            OBJISS.MdiParent = MDIMain
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub cmbname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTs'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCONSUMPTION.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCONSUMPTION.RowCount > 0 Then

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDCONSUMPTION.Rows.RemoveAt(GRIDCONSUMPTION.CurrentRow.Index)
                TOTAL()
                GETSRNO(GRIDCONSUMPTION)
                TOTAL()

            ElseIf e.KeyCode = Keys.F8 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
        GETHSNCODE()
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPCONSUMPTIONNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONSUMPTIONDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CONSUMPTIONDATE.GotFocus
        CONSUMPTIONDATE.SelectAll()
    End Sub

    Private Sub CONSUMPTIONDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CONSUMPTIONDATE.Validating
        Try
            If CONSUMPTIONDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CONSUMPTIONDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Sub GETHSNCODE()
        Try
            TXTHSNCODE.Clear()
            If CMBITEMNAME.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text.Trim <> "" Then GETHSNCODE()
    End Sub
    Sub getbarcode()
        Try
            If TXTBARCODE.Text.Trim <> "" Then
                Dim objcmn As New ClsCommon
                Dim dt As DataTable = objcmn.search(" ITEMMASTER.ITEM_name AS ITEMNAME, HSNMASTER.HSN_CODE AS HSNCODE ", "", "    ITEMMASTER INNER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_BARCODE = '" & TXTBARCODE.Text.Trim & "' and ITEM_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    CMBITEMNAME.Text = dt.Rows(0).Item("ITEMNAME")
                    TXTHSNCODE.Text = dt.Rows(0).Item("HSNCODE")
                    TXTQTY.Text = 1
                End If
            End If
            TXTBARCODE.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        getbarcode()
    End Sub

    Private Sub MaterialConsumption_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.F8 Then
                GRIDCONSUMPTION.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class