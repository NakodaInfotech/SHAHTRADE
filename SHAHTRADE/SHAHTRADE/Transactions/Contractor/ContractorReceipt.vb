
Imports BL

Public Class ContractorReceipt

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPNO, TEMPNAME As String

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            EDIT = False
            CMBNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            EP.Clear()

            TXTNO.Clear()
            DTRECEIPT.Value = Mydate
            CMBNAME.Text = ""
            TXTREFNO.Clear()
            TXTREMARKS.Clear()
            TXTINWORDS.Clear()

            GRIDRECEIPT.RowCount = 0
            TXTSRNO.Clear()
            CMBITEMNAME.Text = ""
            CMBSIZE.Text = ""
            CMBCOLOR.Text = ""
            TXTQUANTITY.Clear()
            TXTRATE.Clear()
            'CMBUNIT.Text = "DOZEN"
            TXTAMOUNT.Clear()
            LBQUANTITY.Text = 0
            LBTOTAL.Text = 0

            GETMAX_CONREC_NO()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETMAX_CONREC_NO()
        Try
            Dim DTTABLE As DataTable = getmax(" isnull(max(CONREC_NO),0) + 1 ", " CONTRACTORRECEIPT ", " AND CONREC_YEARID = " & YearId)
            If DTTABLE.Rows.Count > 0 Then
                TXTRECEIPTNO.Text = DTTABLE.Rows(0).Item(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContractorReceipt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDOK_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.Oemcomma Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for Delete
                TXTNO.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{Tab}")
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call Toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "")
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
            If CMBSIZE.Text.Trim = "" Then FILLSIZE(CMBSIZE, EDIT)
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, EDIT)
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContractorReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'LABOUR'")
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


                Dim dt As New DataTable
                Dim ALPARAVAL As New ArrayList
                Dim OBJRECEIPT As New ClsContractorReceipt

                ALPARAVAL.Add(TEMPNO)
                ALPARAVAL.Add(YearId)

                OBJRECEIPT.alParaval = ALPARAVAL
                dt = OBJRECEIPT.SELECTRECEIPT()

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        TXTRECEIPTNO.Text = TEMPNO

                        CMBNAME.Text = Convert.ToString(dr("NAME"))
                        DTRECEIPT.Text = Format(Convert.ToDateTime(dr("DATE")).Date, "dd/MM/yyyy")

                        TXTREFNO.Text = Convert.ToString(dr("REFNO"))
                        TXTREMARKS.Text = Convert.ToString(dr("REMARKS"))
                        TXTINWORDS.Text = Convert.ToString(dr("INWORDS"))

                        'Item Grid
                        GRIDRECEIPT.Rows.Add(dr("GRIDSRNO").ToString, dr("ITEMNAME").ToString, dr("SIZE").ToString, dr("COLOR").ToString, Val(dr("QUANTITY")), dr("UNIT").ToString, Format(Val(dr("RATE")), "0.000"), Format(Val(dr("AMOUNT")), "0.00"))
                        CALC()
                        TOTAL()
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(Format(Convert.ToDateTime(DTRECEIPT.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTREFNO.Text.Trim)

            alParaval.Add(TXTREMARKS.Text.Trim)
            alParaval.Add(TXTINWORDS.Text)

            alParaval.Add(Val(LBQUANTITY.Text.Trim))
            alParaval.Add(Val(LBTOTAL.Text.Trim))

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            'Save GRID
            Dim GRIDSRNO As String = ""
            Dim ITEMNAME As String = ""
            Dim SIZE As String = ""
            Dim COLOR As String = ""
            Dim QUANTITY As String = ""
            Dim UNIT As String = ""
            Dim RATE As String = ""
            Dim AMOUNT As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDRECEIPT.Rows
                If row.Cells(0).Value <> Nothing Then
                    If GRIDSRNO = "" Then
                        GRIDSRNO = row.Cells(GSRNO.Index).Value.ToString
                        ITEMNAME = row.Cells(GITEMNAME.Index).Value.ToString
                        SIZE = row.Cells(GSIZE.Index).Value.ToString
                        COLOR = row.Cells(GCOLOR.Index).Value.ToString
                        QUANTITY = Val(row.Cells(GQUANTITY.Index).Value)
                        UNIT = row.Cells(GUNIT.Index).Value
                        RATE = Format(Val(row.Cells(GRATE.Index).Value), "0.000")

                        If row.Cells(GAMOUNT.Index).Value <> Nothing Then
                            AMOUNT = Format(Val(row.Cells(GAMOUNT.Index).Value), "0.00")
                        Else
                            AMOUNT = "0"
                        End If
                    Else

                        GRIDSRNO = GRIDSRNO & "|" & row.Cells(GSRNO.Index).Value
                        ITEMNAME = ITEMNAME & "|" & row.Cells(GITEMNAME.Index).Value
                        SIZE = SIZE & "|" & row.Cells(GSIZE.Index).Value.ToString
                        COLOR = COLOR & "|" & row.Cells(GCOLOR.Index).Value.ToString
                        QUANTITY = QUANTITY & "|" & Val(row.Cells(GQUANTITY.Index).Value)
                        UNIT = UNIT & "|" & row.Cells(GUNIT.Index).Value
                        RATE = RATE & "|" & Format(Val(row.Cells(GRATE.Index).Value), "0.000")

                        If row.Cells(GAMOUNT.Index).Value <> Nothing Then
                            AMOUNT = AMOUNT & "|" & Format(Val(row.Cells(GAMOUNT.Index).Value), "0.00")
                        Else
                            AMOUNT = AMOUNT & "|" & "0"
                        End If
                    End If
                End If
            Next

            alParaval.Add(GRIDSRNO)
            alParaval.Add(ITEMNAME)
            alParaval.Add(SIZE)
            alParaval.Add(COLOR)
            alParaval.Add(QUANTITY)
            alParaval.Add(UNIT)
            alParaval.Add(RATE)
            alParaval.Add(AMOUNT)

            Dim OBJRECEIPT As New ClsContractorReceipt
            OBJRECEIPT.alParaval = alParaval

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJRECEIPT.save()
                TEMPNO = DTTABLE.Rows(0).Item(0)
                MessageBox.Show("Details Added")

            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                alParaval.Add(TEMPNO)
                IntResult = OBJRECEIPT.Update()
                MessageBox.Show("Details Updated")
                EDIT = False
            End If

            'SHOW NEXT CONREC ON EDIT MODE DONT CLEAR
            PRINTREPORT(TEMPNO)
            CLEAR()
            CMBNAME.Focus()
            'Call toolnext_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTAMOUNT.Clear()
            If Val(TXTRATE.Text) > 0 And Val(TXTQUANTITY.Text) > 0 Then
                TXTAMOUNT.Text = (Val(TXTRATE.Text.Trim) * Val(TXTQUANTITY.Text.Trim))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Please Fill Contractor Name")
                BLN = False
            End If

            If GRIDRECEIPT.RowCount = 0 Then
                EP.SetError(CMBNAME, "Select Item Name")
                BLN = False
            End If


            'If TXTREFNO.Text.Trim.Length = 0 Then
            '    EP.SetError(TXTREFNO, "Please Fill Ref No")
            '    BLN = False
            'End If


            If EDIT = False Then
                If Not REFNOVALIDATE() Then
                    EP.SetError(TXTREFNO, "Challan number already present")
                    BLN = False
                End If
            End If

            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub TOTAL()
        Try
            LBTOTAL.Text = "0"
            LBQUANTITY.Text = "0"

            If GRIDRECEIPT.RowCount > 0 Then
                For Each row As DataGridViewRow In GRIDRECEIPT.Rows
                    row.Cells(GAMOUNT.Index).Value = Format((Val(row.Cells(GQUANTITY.Index).EditedFormattedValue) * row.Cells(GRATE.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GAMOUNT.Index).Value) > 0 Then LBTOTAL.Text = Format(Val(LBTOTAL.Text) + Val(row.Cells(GAMOUNT.Index).EditedFormattedValue), "0.00")
                    If Val(row.Cells(GQUANTITY.Index).Value) > 0 Then LBQUANTITY.Text = Format(Val(LBQUANTITY.Text) + Val(row.Cells(GQUANTITY.Index).EditedFormattedValue), "0")
                Next
            End If

            If Val(LBTOTAL.Text) > 0 Then TXTINWORDS.Text = CurrencyToWord(LBTOTAL.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            Dim IntResult As Integer
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Delete CONREC?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Dim alParaval As New ArrayList
                    alParaval.Add(TXTRECEIPTNO.Text.Trim)
                    alParaval.Add(YearId)
                    alParaval.Add(Userid)

                    Dim OBJRECEIPT As New ClsContractorReceipt
                    OBJRECEIPT.alParaval = alParaval
                    IntResult = OBJRECEIPT.DELETE()
                    MsgBox("CONREC Deleted")
                    CLEAR()
                    EDIT = False
                End If

            Else
                MsgBox("Delete is only in Edit Mode")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolprevious.Click
        Try
            GRIDRECEIPT.RowCount = 0
LINE1:
            TEMPNO = Val(TXTRECEIPTNO.Text) - 1
            If TEMPNO > 0 Then
                EDIT = True
                ContractorReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDRECEIPT.RowCount = 0 And TEMPNO > 1 Then
                TXTRECEIPTNO.Text = TEMPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDRECEIPT.RowCount = 0
LINE1:
            TEMPNO = Val(TXTRECEIPTNO.Text) + 1
            GETMAX_CONREC_NO()
            Dim MAXNO As Integer = TXTRECEIPTNO.Text.Trim
            CLEAR()
            If Val(TXTRECEIPTNO.Text) - 1 >= TEMPNO Then
                EDIT = True
                ContractorReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDRECEIPT.RowCount = 0 And TEMPNO < MAXNO Then
                TXTRECEIPTNO.Text = TEMPNO
                GoTo LINE1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim OBJRECEIPT As New ContractorReceiptDetails
            OBJRECEIPT.MdiParent = MDIMain
            OBJRECEIPT.Show()
            OBJRECEIPT.BringToFront()
            '  Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Try
            CMDOK_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GETSRNO(ByRef grid As System.Windows.Forms.DataGridView)
        Try
            'If edit = False Then
            For Each row As DataGridViewRow In grid.Rows
                row.Cells(0).Value = row.Index + 1
            Next
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSIZE.Enter
        Try
            If CMBSIZE.Text.Trim = "" Then FILLSIZE(CMBSIZE, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZE.Validating
        Try
            If CMBSIZE.Text.Trim <> "" Then SIZEVALIDATE(CMBSIZE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQUANTITY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQUANTITY.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        Try
            numdotkeypress(e, sender, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTNO.Validating
        Try
            If Val(TXTNO.Text.Trim) > 0 Then
                GRIDRECEIPT.RowCount = 0

                TEMPNO = Val(TXTNO.Text)
                If TEMPNO > 0 Then
                    EDIT = True
                    ContractorReceipt_Load(sender, e)
                Else
                    CLEAR()
                    EDIT = False
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Call CMDDELETE_Click(sender, e)
    End Sub

    Sub FILLGRID()
        Try
            If GRIDDOUBLECLICK = False Then
                GRIDRECEIPT.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, CMBSIZE.Text.Trim, CMBCOLOR.Text.Trim, Val(TXTQUANTITY.Text.Trim), CMBUNIT.Text.Trim, Format(Val(TXTRATE.Text.Trim), "0.000"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"))
                GETSRNO(GRIDRECEIPT)

            ElseIf GRIDDOUBLECLICK = True Then
                GRIDRECEIPT.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text.Trim)
                GRIDRECEIPT.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
                GRIDRECEIPT.Item(GSIZE.Index, TEMPROW).Value = CMBSIZE.Text.Trim
                GRIDRECEIPT.Item(GCOLOR.Index, TEMPROW).Value = CMBCOLOR.Text.Trim
                GRIDRECEIPT.Item(GQUANTITY.Index, TEMPROW).Value = Val(TXTQUANTITY.Text.Trim)
                GRIDRECEIPT.Item(GUNIT.Index, TEMPROW).Value = CMBUNIT.Text.Trim
                GRIDRECEIPT.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text.Trim), "0.000")
                GRIDRECEIPT.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text.Trim), "0.00")

                GRIDDOUBLECLICK = False

            End If

            TXTAMOUNT.ReadOnly = True
            TOTAL()
            GRIDRECEIPT.FirstDisplayedScrollingRowIndex = GRIDRECEIPT.RowCount - 1

            CMBITEMNAME.Text = ""
            CMBSIZE.Text = ""
            CMBCOLOR.Text = ""
            TXTQUANTITY.Clear()
            ' CMBUNIT.Text = "DOZEN"
            TXTRATE.Clear()
            TXTAMOUNT.Clear()

            If GRIDRECEIPT.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDRECEIPT.Rows(GRIDRECEIPT.RowCount - 1).Cells(0).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
            CMBITEMNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTRATE.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And CMBSIZE.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And Val(TXTQUANTITY.Text.Trim) > 0 And CMBUNIT.Text.Trim <> "" And Val(TXTRATE.Text.Trim) > 0 Then

                FILLGRID()
                TOTAL()
            Else
                If CMBITEMNAME.Text.Trim = "" Then
                    MsgBox("Please Select Item Name")
                    CMBITEMNAME.Focus()
                    Exit Sub
                End If

                If CMBSIZE.Text.Trim = "" Then
                    MsgBox("Please Select Size")
                    CMBSIZE.Focus()
                    Exit Sub
                End If

                If CMBCOLOR.Text.Trim = "" Then
                    MsgBox("Please Select Color")
                    CMBCOLOR.Focus()
                    Exit Sub
                End If

                If Val(TXTQUANTITY.Text.Trim) = 0 Then
                    MsgBox("Please Fill Quantity ")
                    TXTQUANTITY.Focus()
                    Exit Sub
                End If

                If CMBUNIT.Text.Trim = "" Then
                    MsgBox("Please Select Unit")
                    CMBUNIT.Focus()
                    Exit Sub
                End If

                If Val(TXTRATE.Text.Trim) <= 0 Then
                    MsgBox("Please Fill Rate")
                    TXTRATE.Focus()
                    Exit Sub
                End If

                If Val(TXTRATE.Text) > 0 Then
                    CALC()
                End If
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EDITROW()
        Try
            TEMPROW = GRIDRECEIPT.CurrentRow.Index

            If GRIDRECEIPT.CurrentRow.Index >= 0 And GRIDRECEIPT.Item(GSRNO.Index, GRIDRECEIPT.CurrentRow.Index).Value <> Nothing Then
                GRIDDOUBLECLICK = True

                TXTSRNO.Text = GRIDRECEIPT.Item(GSRNO.Index, TEMPROW).Value.ToString
                CMBITEMNAME.Text = GRIDRECEIPT.Item(GITEMNAME.Index, TEMPROW).Value.ToString
                CMBSIZE.Text = GRIDRECEIPT.Item(GSIZE.Index, TEMPROW).Value.ToString
                CMBCOLOR.Text = GRIDRECEIPT.Item(GCOLOR.Index, TEMPROW).Value.ToString
                TXTQUANTITY.Text = GRIDRECEIPT.Item(GQUANTITY.Index, TEMPROW).Value.ToString
                CMBUNIT.Text = GRIDRECEIPT.Item(GUNIT.Index, TEMPROW).Value.ToString
                TXTRATE.Text = GRIDRECEIPT.Item(GRATE.Index, TEMPROW).Value.ToString
                TXTAMOUNT.Text = GRIDRECEIPT.Item(GAMOUNT.Index, TEMPROW).Value.ToString

                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDPURCHASE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDRECEIPT.CellDoubleClick
        Try
            EDITROW()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTREFNO_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTREFNO.Validating
        Try
            If EDIT = False Then
                If Not REFNOVALIDATE() Then e.Cancel = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function REFNOVALIDATE() As Boolean
        Try
            Dim BLN As Boolean = True
            If TXTREFNO.Text.Trim <> "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable = objclscommon.search(" ContractorReceipt.CONREC_REFNO AS REFNO ", "", " ACCOUNTSMASTER INNER JOIN ContractorReceipt ON ACCOUNTSMASTER.ACC_ID = ContractorReceipt.CONREC_ACCID AND ACCOUNTSMASTER.ACC_YEARID = ContractorReceipt.CONREC_YEARID", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ContractorReceipt.CONREC_REFNO = '" & TXTREFNO.Text.Trim & "'and ACC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Challan number already present", MsgBoxStyle.Critical, "SHENSHE")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTREFNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTREFNO.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    'Private Sub CMBITEMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
    '    Try
    '        FILLRATE()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub CMBSIZE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSIZE.Validated
    '    Try
    '        FILLRATE()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub CMBNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
    '    Try
    '        FILLRATE()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Sub FILLRATE()
    '    Try
    '        If CMBNAME.Text <> "" And CMBITEMNAME.Text <> "" And CMBSIZE.Text <> "" Then
    '            Dim OBJCMN As New ClsCommon
    '            Dim DT As DataTable = OBJCMN.search(" CONTRACTORRECEIPTCONFIG_DESC.CONFIG_PURCHASE AS PURCHASE ", "", " CONTRACTORRECEIPTCONFIG_DESC INNER JOIN CONTRACTORRECEIPTCONFIG ON CONTRACTORRECEIPTCONFIG_DESC.CONFIG_ID = CONTRACTORRECEIPTCONFIG.CONFIG_ID INNER JOIN ACCOUNTSMASTER ON CONTRACTORRECEIPTCONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID INNER JOIN ITEMMASTER ON CONTRACTORRECEIPTCONFIG_DESC.CONFIG_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN SIZEMASTER ON CONTRACTORRECEIPTCONFIG_DESC.CONFIG_SIZEID = SIZEMASTER.SIZE_ID ", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND SIZEMASTER.SIZE_NAME = '" & CMBSIZE.Text.Trim & "' AND CONTRACTORRECEIPTCONFIG.CONFIG_YEARID = " & YearId)
    '            If DT.Rows.Count > 0 Then
    '                If TXTRATE.Text.Trim = "" Then TXTRATE.Text = DT.Rows(0).Item("PURCHASE")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub GRIDPURCHASE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDRECEIPT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDRECEIPT.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block
                GRIDRECEIPT.Rows.RemoveAt(GRIDRECEIPT.CurrentRow.Index)
                GETSRNO(GRIDRECEIPT)
                TOTAL()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBUNIT.Enter
        Try
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBUNIT.Validating
        Try
            If CMBUNIT.Text.Trim <> "" Then unitvalidate(CMBUNIT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal CONTRACTORRECNO As Integer)
        Try
            If MsgBox("Wish to Print Issue Voucher?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJINVOICE As New SaleInvoiceDesign
            OBJINVOICE.MdiParent = MDIMain
            OBJINVOICE.FRMSTRING = "CONTRACTORRECEIPT"
            OBJINVOICE.WHERECLAUSE = "{CONTRACTORRECEIPT.CONREC_NO}=" & Val(CONTRACTORRECNO) & " and {CONTRACTORRECEIPT.CONREC_YEARID}=" & YearId
            OBJINVOICE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_Validated(sender As Object, e As EventArgs) Handles CMBSIZE.Validated, CMBCOLOR.Validated, CMBITEMNAME.Validated
        Try
            FILLRATE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLRATE()
        Try
            If CMBNAME.Text <> "" And CMBITEMNAME.Text <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" CONTRACTORRATECONFIG_DESC.CONFIG_PURCHASERATE AS PURCHASE ", "", " CONTRACTORRATECONFIG_DESC INNER JOIN CONTRACTORRATECONFIG ON CONTRACTORRATECONFIG_DESC.CONFIG_ID = CONTRACTORRATECONFIG.CONFIG_ID INNER JOIN ACCOUNTSMASTER ON CONTRACTORRATECONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID INNER JOIN ITEMMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN SIZEMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_SIZEID = SIZEMASTER.SIZE_ID INNER JOIN COLORMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_COLORID = COLORMASTER.COLOR_ID ", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND SIZEMASTER.SIZE_NAME = '" & CMBSIZE.Text.Trim & "' AND COLORMASTER.COLOR_NAME = '" & CMBCOLOR.Text.Trim & "' AND CONTRACTORRATECONFIG.CONFIG_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then If TXTRATE.Text.Trim = "" Then TXTRATE.Text = DT.Rows(0).Item("PURCHASE")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class