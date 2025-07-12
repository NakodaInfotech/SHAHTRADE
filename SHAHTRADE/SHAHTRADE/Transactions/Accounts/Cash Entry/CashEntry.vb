Imports System.ComponentModel
Imports BL

Public Class CashEntry
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public edit As Boolean
    Dim TYPE As String  'USED FOR FORMTYPE WHILE RETRIVING DATA FROM GETDATA FUNCTION AND PASSING IN TO SP
    Public FRMSTRING As String  ' USER FOR BOOKTYPE 
    Public TEMPCASHNO As Integer
    Public BILLNO As String
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer

    Sub clear()
        Try
            CMBPARTYNAME.Text = ""
            CASHDATE.Text = Mydate
            TXTNARRATION.Clear()
            TXTCREDIT.Clear()
            TXTDEBIT.Clear()
            txtremarks.Clear()
            GRIDCASH.RowCount = 0
            EP.Clear()
            LBLTOTALDEBIT.Text = 0
            LBLTOTALCREDIT.Text = 0
            TXTSRNO.Text = 1
            GETMAX_CASHNO()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TOTAL()
        Try
            LBLTOTALDEBIT.Text = 0
            LBLTOTALCREDIT.Text = 0
            For Each ROW As DataGridViewRow In GRIDCASH.Rows
                If Val(ROW.Cells(GDebit.Index).Value) > 0 Then LBLTOTALDEBIT.Text = Format(Val(LBLTOTALDEBIT.Text) + Val(ROW.Cells(GDebit.Index).Value), "0.00")
                If Val(ROW.Cells(GCredit.Index).Value) > 0 Then LBLTOTALCREDIT.Text = Format(Val(LBLTOTALCREDIT.Text) + Val(ROW.Cells(GCredit.Index).Value), "0.00")
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try

            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If
            Dim OBJSPL As New ClsCashEntry()

            OBJSPL.ALPARAVAL.Add(Val(LBLTOTALDEBIT.Text.Trim))
            OBJSPL.ALPARAVAL.Add(Val(LBLTOTALCREDIT.Text.Trim))
            OBJSPL.ALPARAVAL.Add(txtremarks.Text.Trim)

            OBJSPL.ALPARAVAL.Add(CmpId)
            OBJSPL.ALPARAVAL.Add(Userid)
            OBJSPL.ALPARAVAL.Add(YearId)

            Dim GRIDSRNO As String = ""
            Dim PARTYNAME As String = ""
            Dim CASHDATE As String = ""
            Dim NARRATION As String = ""
            Dim DEBIT As String = ""
            Dim CREDIT As String = ""



            For Each ROW As DataGridViewRow In GRIDCASH.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = Val(ROW.Cells(gsrno.Index).Value)
                        PARTYNAME = ROW.Cells(GPARTYNAME.Index).Value
                        CASHDATE = Format(Convert.ToDateTime(ROW.Cells(GCASHDATE.Index).Value).Date, "MM/dd/yyyy")
                        NARRATION = ROW.Cells(GNARRATION.Index).Value
                        DEBIT = Val(ROW.Cells(GDebit.Index).Value)
                        CREDIT = Val(ROW.Cells(GCredit.Index).Value)


                    Else
                        GRIDSRNO = GRIDSRNO & "|" & Val(ROW.Cells(gsrno.Index).Value)
                        PARTYNAME = PARTYNAME & "|" & ROW.Cells(GPARTYNAME.Index).Value
                        CASHDATE = CASHDATE & "|" & Format(Convert.ToDateTime(ROW.Cells(GCASHDATE.Index).Value).Date, "MM/dd/yyyy")
                        NARRATION = NARRATION & "|" & ROW.Cells(GNARRATION.Index).Value
                        DEBIT = DEBIT & "|" & Val(ROW.Cells(GDebit.Index).Value)
                        CREDIT = CREDIT & "|" & Val(ROW.Cells(GCredit.Index).Value)

                    End If
                End If
            Next

            OBJSPL.ALPARAVAL.Add(GRIDSRNO)
            OBJSPL.ALPARAVAL.Add(PARTYNAME)
            OBJSPL.ALPARAVAL.Add(CASHDATE)
            OBJSPL.ALPARAVAL.Add(NARRATION)
            OBJSPL.ALPARAVAL.Add(DEBIT)
            OBJSPL.ALPARAVAL.Add(CREDIT)




            If edit = False Then
                Dim DT As DataTable = OBJSPL.SAVE()
                MessageBox.Show("Details Added")
                TXTCASHNO.Text = DT.Rows(0).Item(0)
            Else
                OBJSPL.ALPARAVAL.Add(TEMPCASHNO)
                Dim IntResult As Integer = OBJSPL.UPDATE()
                MessageBox.Show("Details Updated")
            End If

            PRINTREPORT()

            edit = False
            clear()
            CMBPARTYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        'If CMBPARTYNAME.Text.Trim = "" Then
        '    EP.SetError(CMBPARTYNAME, " Please Fill Party Name ")
        '    bln = False
        'End If

        If GRIDCASH.RowCount = 0 Then
            EP.SetError(TXTNARRATION, " Please Enter Data in grid")
            bln = False
        End If

        'If CASHDATE.Text = "__/__/____" Then
        '    EP.SetError(CASHDATE, " Please Enter Proper Date")
        '    bln = False
        'End If

        Return bln
    End Function

    Private Sub CMBPARTYNAME_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYNAME.GotFocus
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, edit, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        If GRIDDOUBLECLICK = False Then
            GRIDCASH.Rows.Add(Val(TXTSRNO.Text.Trim), CMBPARTYNAME.Text.Trim, CASHDATE.Text.Trim, TXTNARRATION.Text.Trim, Val(TXTDEBIT.Text.Trim), Val(TXTCREDIT.Text.Trim))
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCASH.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
            GRIDCASH.Item(GPARTYNAME.Index, TEMPROW).Value = CMBPARTYNAME.Text.Trim
            GRIDCASH.Item(GCASHDATE.Index, TEMPROW).Value = CASHDATE.Text.Trim
            GRIDCASH.Item(GNARRATION.Index, TEMPROW).Value = TXTNARRATION.Text.Trim
            GRIDCASH.Item(GDebit.Index, TEMPROW).Value = Val(TXTDEBIT.Text.Trim)
            GRIDCASH.Item(GCredit.Index, TEMPROW).Value = Val(TXTCREDIT.Text.Trim)
            GRIDDOUBLECLICK = False
        End If

        GRIDCASH.FirstDisplayedScrollingRowIndex = GRIDCASH.RowCount - 1
        TOTAL()

        TXTSRNO.Text = GRIDCASH.RowCount + 1
        CMBPARTYNAME.Text = ""
        CASHDATE.Text = Mydate
        TXTDEBIT.Clear()
        TXTCREDIT.Clear()
        TXTNARRATION.Clear()
        CMBPARTYNAME.Focus()

    End Sub

    Sub GETMAX_CASHNO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(CASH_no),0) + 1 ", "CASHENTRY", " AND CASH_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCASHNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CASHENTRY_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor.Current = Cursors.WaitCursor
            fillcmb()
            clear()

            If edit = True Then

                Dim objclsCASH As New ClsCashEntry()
                Dim dt_po As DataTable = objclsCASH.selectCASH(TEMPCASHNO, CmpId, Locationid, YearId)

                If dt_po.Rows.Count > 0 Then
                    For Each dr As DataRow In dt_po.Rows
                        TXTCASHNO.Text = TEMPCASHNO
                        txtremarks.Text = Convert.ToString(dr("REMARKS"))
                        GRIDCASH.Rows.Add(Val(dr("GRIDSRNO")), dr("PARTYNAME"), Format(Convert.ToDateTime(dr("CASHDATE")).Date, "dd/MM/yyyy"), dr("NARRATION").ToString, Val((dr("DEBIT"))), Val(dr("CREDIT")))
                    Next
                    GRIDCASH.FirstDisplayedScrollingRowIndex = GRIDCASH.RowCount - 1
                    TOTAL()
                Else
                    edit = False
                    clear()
                End If
            End If
            TXTSRNO.Text = Val(GRIDCASH.RowCount) + 1
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, edit, "")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        clear()
        edit = False
        CMBPARTYNAME.Focus()
    End Sub

    Private Sub SamplePricelist_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
                tstxtbillno.SelectAll()
            ElseIf e.KeyCode = Windows.Forms.Keys.F5 Then       'for grid foucs
                GRIDCASH.Focus()
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Dim OBJINVDTLS As New CashEntryDetails
                OBJINVDTLS.MdiParent = MDIMain
                OBJINVDTLS.Show()
            ElseIf e.Alt = True And e.KeyCode = Keys.Left Then
                ToolPREVIOUS_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.Right Then
                Toolnext_Click(sender, e)
            ElseIf e.KeyCode = Keys.P And e.Alt = True Then
                Call PrintToolStripButton_Click(sender, e)
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Sub getsrno(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Try
            Call cmddelete_Click(sender, e)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then
                If MsgBox("Delete Cash Entry ?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alParaval As New ArrayList
                    alParaval.Add(TEMPCASHNO)
                    alParaval.Add(YearId)

                    Dim clspo As New ClsCashEntry()
                    clspo.ALPARAVAL = alParaval
                    Dim IntResult As Integer = clspo.Delete()
                    MsgBox("Cash Entry Deleted")
                    clear()
                    edit = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            If GRIDCASH.CurrentRow.Index >= 0 And GRIDCASH.Item(gsrno.Index, GRIDCASH.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True
                TEMPROW = GRIDCASH.CurrentRow.Index
                TXTSRNO.Text = GRIDCASH.Item(gsrno.Index, GRIDCASH.CurrentRow.Index).Value.ToString
                CMBPARTYNAME.Text = GRIDCASH.Item(GPARTYNAME.Index, GRIDCASH.CurrentRow.Index).Value.ToString
                CASHDATE.Text = GRIDCASH.Item(GCASHDATE.Index, GRIDCASH.CurrentRow.Index).Value
                TXTNARRATION.Text = GRIDCASH.Item(GNARRATION.Index, GRIDCASH.CurrentRow.Index).Value.ToString
                TXTDEBIT.Text = Val(GRIDCASH.Item(GDebit.Index, GRIDCASH.CurrentRow.Index).Value)
                TXTCREDIT.Text = Val(GRIDCASH.Item(GCredit.Index, GRIDCASH.CurrentRow.Index).Value)
                CMBPARTYNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCASH_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCASH.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCASH.RowCount > 0 Then
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                GRIDCASH.Rows.RemoveAt(GRIDCASH.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDCASH)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim objpodtls As New CashEntryDetails
            objpodtls.MdiParent = MDIMain
            objpodtls.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdOK_Click(sender, e)
    End Sub

    Private Sub ToolPREVIOUS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolPREVIOUS.Click
        Try
            GRIDCASH.RowCount = 0
LINE1:
            TEMPCASHNO = Val(TXTCASHNO.Text) - 1
            If TEMPCASHNO > 0 Then
                edit = True
                CASHENTRY_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDCASH.RowCount = 0 And TEMPCASHNO > 1 Then
                TXTCASHNO.Text = TEMPCASHNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub Toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolnext.Click
        Try
            GRIDCASH.RowCount = 0
LINE1:
            TEMPCASHNO = Val(TXTCASHNO.Text) + 1
            GETMAX_CASHNO()
            Dim MAXNO As Integer = TXTCASHNO.Text.Trim
            clear()
            If Val(TXTCASHNO.Text) - 1 >= TEMPCASHNO Then
                edit = True
                CASHENTRY_Load(sender, e)
            Else
                clear()
                edit = False
            End If
            If GRIDCASH.RowCount = 0 And TEMPCASHNO < MAXNO Then
                TXTCASHNO.Text = TEMPCASHNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub PRINTREPORT()
        Try
            'If MsgBox("Wish to Print Price List?", MsgBoxStyle.YesNo) = vbNo Then Exit Sub
            'Dim OBJSMP As New SaleInvoiceDesign
            'OBJSMP.MdiParent = MDIMain
            'OBJSMP.FRMSTRING = "CASHENTRY"
            'OBJSMP.WHERECLAUSE = "{CashEntry.CASH_no}=" & Val(TXTCASHNO.Text.Trim) & " and {CashEntry.CASH_yearid}=" & YearId
            'OBJSMP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If edit = True Then PRINTREPORT()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CASHDATE_Validating(sender As Object, e As CancelEventArgs) Handles CASHDATE.Validating
        Try
            If CASHDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CASHDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validating(sender As Object, e As CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, TXTADD, "", "", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCASH_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GRIDCASH.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNARRATION_Validating(sender As Object, e As CancelEventArgs) Handles TXTNARRATION.Validating
        'Try
        '    If CMBPARTYNAME.Text.Trim <> "" And Val(TXTCREDIT.Text.Trim) > 0 Then
        '        fillgrid()
        '    Else
        '        MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub TXTDEBIT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTDEBIT.KeyPress, TXTCREDIT.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCREDIT_Validating(sender As Object, e As CancelEventArgs) Handles TXTCREDIT.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" And CASHDATE.Text <> "__/__/____" And (Val(TXTDEBIT.Text.Trim) > 0 Or Val(TXTCREDIT.Text.Trim) > 0) Then
                fillgrid()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validated(sender As Object, e As EventArgs) Handles tstxtbillno.Validated
        Try
            If tstxtbillno.Text.Trim.Length = 0 Then Exit Sub
            GRIDCASH.RowCount = 0
            TEMPCASHNO = Val(tstxtbillno.Text)
            If TEMPCASHNO > 0 Then
                edit = True
                CASHENTRY_Load(sender, e)
            Else
                clear()
                edit = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class