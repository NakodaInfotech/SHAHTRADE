
Imports System.ComponentModel
Imports BL

Public Class IssueVoucher

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPISSUENO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        TXTFROM.Clear()
        TXTTO.Clear()
        TXTISSUENO.Clear()
        ISSDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBHALLMARK.Text = ""
        TXTREQNO.Clear()
        TXTVOUCHERWT.Clear()
        CHKRECD.CheckState = CheckState.Unchecked
        RECDATE.Clear()
        TXTBOXWT.Clear()
        TXTWTWITHTAGS.Clear()
        TXTINITIALGROSSWT.Clear()

        GRIDISSUE.RowCount = 0
        TXTSRNO.Text = 1
        CMBITEMNAME.Text = ""
        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTPCS.Text = 1
        TXTGROSSWT.Clear()
        TXTLESSWT.Clear()
        TXTNETTWT.Clear()
        TXTPURITY.Text = 91.6
        TXTFINEWT.Clear()
        TXTHUID.Clear()

        LBLTOTALPCS.Text = 0
        LBLTOTALGROSSWT.Text = 0.0
        LBLTOTALLESSWT.Text = 0.0
        LBLTOTALNETTWT.Text = 0.0
        LBLTOTALFINEWT.Text = 0.0

        txtremarks.Clear()

        GETMAX_ISSUE_NO()
        EP.Clear()
        GRIDDOUBLECLICK = False

    End Sub

    Sub GETMAX_ISSUE_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(ISS_no),0) + 1 ", " ISSUEVOUCHER ", " and ISS_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTISSUENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub

    Private Sub ISSUEVOUCHER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.F8 Then
                GRIDISSUE.Focus()
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

    Sub FILLCMB()
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        FILLHALLMARK(CMBHALLMARK)
        FILLITEMNAME(CMBITEMNAME, EDIT)
    End Sub

    Private Sub ISSUEVOUCHER_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'SALE INVOICE'")
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
                Dim OBJISS As New ClsIssueVoucher

                ALPARAVAL.Add(TEMPISSUENO)
                ALPARAVAL.Add(YearId)

                OBJISS.alParaval = ALPARAVAL
                DT = OBJISS.SELECTISSUE()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTISSUENO.Text = TEMPISSUENO
                        ISSDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        CMBHALLMARK.Text = Convert.ToString(DR("HALLMARK"))
                        TXTREQNO.Text = Convert.ToString(DR("REQNO"))
                        TXTVOUCHERWT.Text = Val(DR("VOUCHERWT"))
                        CHKRECD.Checked = Convert.ToBoolean(DR("RECD"))
                        RECDATE.Text = DR("RECDATE")
                        TXTBOXWT.Text = Val(DR("BOXWT"))
                        TXTWTWITHTAGS.Text = Val(DR("WTWITHTAGS"))
                        TXTINITIALGROSSWT.Text = Val(DR("INITIALGROSSWT"))
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        GRIDISSUE.Rows.Add(DR("SRNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), DR("GRIDREMARKS"), Format(Val(DR("PCS")), "0"), Format(Val(DR("GROSSWT")), "0.000"), Format(Val(DR("LESSWT")), "0.000"), Format(Val(DR("NETTWT")), "0.000"), Format(Val(DR("PURITY")), "0.00"), Format(Val(DR("FINEWT")), "0.000"), DR("HUID"))

                    Next

                    GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1
                End If
                ISSDATE.Focus()
                TOTAL()
                GETSRNO(GRIDISSUE)
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
            ALPARAVAL.Add(Format(Convert.ToDateTime(ISSDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBHALLMARK.Text.Trim)
            ALPARAVAL.Add(TXTREQNO.Text.Trim)
            ALPARAVAL.Add(Val(TXTVOUCHERWT.Text.Trim))
            ALPARAVAL.Add(CHKRECD.Checked)
            ALPARAVAL.Add(RECDATE.Text)
            ALPARAVAL.Add(Val(TXTBOXWT.Text.Trim))
            ALPARAVAL.Add(Val(TXTWTWITHTAGS.Text.Trim))
            ALPARAVAL.Add(Val(TXTINITIALGROSSWT.Text.Trim))

            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALPCS.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALGROSSWT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALLESSWT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALNETTWT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALFINEWT.Text.Trim))

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim SRNO As String = ""
            Dim ITEM As String = ""
            Dim HSNCODE As String = ""
            Dim GRIDREMARKS As String = ""
            Dim PCS As String = ""
            Dim GROSSWT As String = ""
            Dim LESSWT As String = ""
            Dim NETTWT As String = ""
            Dim PURITY As String = ""
            Dim FINEWT As String = ""
            Dim HUID As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDISSUE.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        ITEM = ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDREMARKS = ROW.Cells(GDESC.Index).Value.ToString
                        PCS = Val(ROW.Cells(GPCS.Index).Value)
                        GROSSWT = Val(ROW.Cells(GGROSSWT.Index).Value)
                        LESSWT = Val(ROW.Cells(GLESSWT.Index).Value)
                        NETTWT = Val(ROW.Cells(GNETTWT.Index).Value)
                        PURITY = Val(ROW.Cells(GPURITY.Index).Value)
                        FINEWT = Val(ROW.Cells(GFINEWT.Index).Value)
                        HUID = ROW.Cells(GHUID.Index).Value.ToString

                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        ITEM = ITEM & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDREMARKS = GRIDREMARKS & "|" & ROW.Cells(GDESC.Index).Value.ToString
                        PCS = PCS & "|" & Val(ROW.Cells(GPCS.Index).Value)
                        GROSSWT = GROSSWT & "|" & Val(ROW.Cells(GGROSSWT.Index).Value)
                        LESSWT = LESSWT & "|" & Val(ROW.Cells(GLESSWT.Index).Value)
                        NETTWT = NETTWT & "|" & Val(ROW.Cells(GNETTWT.Index).Value)
                        PURITY = PURITY & "|" & Val(ROW.Cells(GPURITY.Index).Value)
                        FINEWT = FINEWT & "|" & Val(ROW.Cells(GFINEWT.Index).Value)
                        HUID = HUID & "|" & ROW.Cells(GHUID.Index).Value.ToString
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(ITEM)
            ALPARAVAL.Add(HSNCODE)
            ALPARAVAL.Add(GRIDREMARKS)
            ALPARAVAL.Add(PCS)
            ALPARAVAL.Add(GROSSWT)
            ALPARAVAL.Add(LESSWT)
            ALPARAVAL.Add(NETTWT)
            ALPARAVAL.Add(PURITY)
            ALPARAVAL.Add(FINEWT)
            ALPARAVAL.Add(HUID)


            Dim OBJPUR As New ClsIssueVoucher
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.SAVE()
                MsgBox("Details Added !!")
                TEMPISSUENO = DTTABLE.Rows(0).Item(0)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPISSUENO)
                IntResult = OBJPUR.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            PRINTREPORT(TEMPISSUENO)

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
            If MsgBox("Wish to Print Issue voucher?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJISSOICE As New SaleInvoiceDesign
                OBJISSOICE.MdiParent = MDIMain
                OBJISSOICE.FRMSTRING = "ISSUEVOUCHER"
                OBJISSOICE.WHERECLAUSE = "{ISSUEVOUCHER.ISS_no}=" & Val(INVNO) & " and {ISSUEVOUCHER.ISS_yearid}=" & YearId
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

        If CMBHALLMARK.Text.Trim.Length = 0 Then
            EP.SetError(CMBHALLMARK, "Please Enter Hallmark Name")
            bln = False
        End If

        If TXTREQNO.Text.Trim.Length = 0 Then
            EP.SetError(TXTREQNO, "Please Enter Req No")
            bln = False
        End If

        If Val(TXTBOXWT.Text.Trim) = 0 Then
            EP.SetError(TXTBOXWT, "Please Enter Box Wt")
            bln = False
        End If

        If Val(TXTWTWITHTAGS.Text.Trim) = 0 Then
            EP.SetError(TXTWTWITHTAGS, "Please Enter Wt With Tags")
            bln = False
        End If

        If GRIDISSUE.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If


        If ISSDATE.Text = "__/__/____" Then
            EP.SetError(ISSDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(ISSDATE.Text) Then
                EP.SetError(ISSDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub TOTAL()
        Try

            LBLTOTALPCS.Text = 0
            LBLTOTALGROSSWT.Text = 0
            LBLTOTALLESSWT.Text = 0
            LBLTOTALNETTWT.Text = 0
            LBLTOTALFINEWT.Text = 0

            Dim dt As New DataTable
            Dim objcmn As New ClsCommon

            For Each row As DataGridViewRow In GRIDISSUE.Rows
                If Val(row.Cells(GPCS.Index).Value) > 0 Then LBLTOTALPCS.Text = Format(Val(LBLTOTALPCS.Text) + Val(row.Cells(GPCS.Index).Value), "0")
                If Val(row.Cells(GGROSSWT.Index).Value) > 0 Then LBLTOTALGROSSWT.Text = Format(Val(LBLTOTALGROSSWT.Text) + Val(row.Cells(GGROSSWT.Index).Value), "0.000")
                If Val(row.Cells(GLESSWT.Index).Value) > 0 Then LBLTOTALLESSWT.Text = Format(Val(LBLTOTALLESSWT.Text) + Val(row.Cells(GLESSWT.Index).Value), "0.000")
                If Val(row.Cells(GNETTWT.Index).Value) > 0 Then LBLTOTALNETTWT.Text = Format(Val(LBLTOTALNETTWT.Text) + Val(row.Cells(GNETTWT.Index).Value), "0.000")
                If Val(row.Cells(GFINEWT.Index).Value) > 0 Then LBLTOTALFINEWT.Text = Format(Val(LBLTOTALFINEWT.Text) + Val(row.Cells(GFINEWT.Index).Value), "0.000")
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
            If GRIDISSUE.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDISSUE.Rows(GRIDISSUE.RowCount - 1).Cells(gsrno.Index).Value) + 1
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

        GRIDISSUE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            TXTSRNO.Text = GRIDISSUE.RowCount + 1
            GRIDISSUE.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, TXTHSNCODE.Text.Trim, TXTGRIDDESC.Text.Trim, Format(Val(TXTPCS.Text.Trim), "0"), Format(Val(TXTGROSSWT.Text.Trim), "0.000"), Format(Val(TXTLESSWT.Text.Trim), "0.000"), Format(Val(TXTNETTWT.Text.Trim), "0.000"), Format(Val(TXTPURITY.Text.Trim), "0.00"), Format(Val(TXTFINEWT.Text.Trim), "0.000"), TXTHUID.Text.Trim)
            GETSRNO(GRIDISSUE)
            PRINTBARCODE()

        ElseIf GRIDDOUBLECLICK = True Then
            GRIDISSUE.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDISSUE.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text
            GRIDISSUE.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDISSUE.Item(GDESC.Index, TEMPROW).Value = TXTGRIDDESC.Text.Trim
            GRIDISSUE.Item(GPCS.Index, TEMPROW).Value = Format(Val(TXTPCS.Text), "0")
            GRIDISSUE.Item(GGROSSWT.Index, TEMPROW).Value = Format(Val(TXTGROSSWT.Text), "0.000")
            GRIDISSUE.Item(GLESSWT.Index, TEMPROW).Value = Format(Val(TXTLESSWT.Text), "0.000")
            GRIDISSUE.Item(GNETTWT.Index, TEMPROW).Value = Format(Val(TXTNETTWT.Text), "0.000")
            GRIDISSUE.Item(GPURITY.Index, TEMPROW).Value = Format(Val(TXTPURITY.Text), "0.00")
            GRIDISSUE.Item(GFINEWT.Index, TEMPROW).Value = Format(Val(TXTFINEWT.Text), "0.000")
            GRIDISSUE.Item(GHUID.Index, TEMPROW).Value = TXTHUID.Text.Trim

            GRIDDOUBLECLICK = False
        End If

        TOTAL()
        GRIDISSUE.FirstDisplayedScrollingRowIndex = GRIDISSUE.RowCount - 1

        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTPCS.Text = 1
        TXTGROSSWT.Clear()
        TXTLESSWT.Clear()
        TXTNETTWT.Clear()
        TXTPURITY.Text = 91.6
        TXTFINEWT.Clear()
        TXTHUID.Clear()
        TXTSRNO.Text = Val(GRIDISSUE.RowCount) + 1

        TXTGROSSWT.Focus()

    End Sub

    Private Sub GRIDISSUE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDISSUE.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDISSUE.CurrentRow.Index >= 0 And GRIDISSUE.Item(gsrno.Index, GRIDISSUE.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDISSUE.Item(gsrno.Index, GRIDISSUE.CurrentRow.Index).Value
                CMBITEMNAME.Text = GRIDISSUE.Item(GITEMNAME.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDISSUE.Item(GHSNCODE.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTGRIDDESC.Text = GRIDISSUE.Item(GDESC.Index, GRIDISSUE.CurrentRow.Index).Value.ToString
                TXTPCS.Text = Val(GRIDISSUE.Item(GPCS.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTGROSSWT.Text = Val(GRIDISSUE.Item(GGROSSWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTLESSWT.Text = Val(GRIDISSUE.Item(GLESSWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTNETTWT.Text = Val(GRIDISSUE.Item(GNETTWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTPURITY.Text = Val(GRIDISSUE.Item(GPURITY.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTFINEWT.Text = Val(GRIDISSUE.Item(GFINEWT.Index, GRIDISSUE.CurrentRow.Index).Value)
                TXTHUID.Text = GRIDISSUE.Item(GHUID.Index, GRIDISSUE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDISSUE.CurrentRow.Index
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            If Val(TXTGROSSWT.Text.Trim) > 0 Then TXTNETTWT.Text = Format(Val(TXTGROSSWT.Text.Trim) - Val(TXTLESSWT.Text.Trim), "0.000")
            If Val(TXTNETTWT.Text.Trim) > 0 And Val(TXTPURITY.Text.Trim) > 0 Then TXTFINEWT.Text = Format((Val(TXTNETTWT.Text.Trim) * Val(TXTPURITY.Text.Trim)) / 100, "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGROSSWT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTGROSSWT.Validated, TXTLESSWT.Validated, TXTNETTWT.Validated, TXTPURITY.Validated
        CALC()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDISSUE.RowCount = 0
Line1:
            TEMPISSUENO = Val(TXTISSUENO.Text) - 1
            If TEMPISSUENO > 0 Then
                EDIT = True
                ISSUEVOUCHER_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPISSUENO > 1 Then
                TXTISSUENO.Text = TEMPISSUENO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDISSUE.RowCount = 0
LINE1:
            TEMPISSUENO = Val(TXTISSUENO.Text) + 1
            GETMAX_ISSUE_NO()
            Dim MAXNO As Integer = TXTISSUENO.Text.Trim
            CLEAR()
            If Val(TXTISSUENO.Text) - 1 >= TEMPISSUENO Then
                EDIT = True
                ISSUEVOUCHER_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDISSUE.RowCount = 0 And TEMPISSUENO < MAXNO Then
                TXTISSUENO.Text = TEMPISSUENO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) = 0 Then Exit Sub
            GRIDISSUE.RowCount = 0
            TEMPISSUENO = Val(tstxtbillno.Text)
            If TEMPISSUENO > 0 Then
                EDIT = True
                ISSUEVOUCHER_Load(sender, e)
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
                Dim objpur As New ClsIssueVoucher
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPISSUENO)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.DELETE
                    MsgBox("Sale Invoice Deleted!!!")
                    CLEAR()
                    EDIT = False
                End If
            Else
                MsgBox("Delete is only in Edit Mode")
            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJISS As New IssueVoucherDetails
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

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDISSUE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDISSUE.RowCount > 0 Then

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDISSUE.Rows.RemoveAt(GRIDISSUE.CurrentRow.Index)
                TOTAL()
                GETSRNO(GRIDISSUE)
                TOTAL()

            ElseIf e.KeyCode = Keys.F8 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTPCS.KeyPress, TXTGROSSWT.KeyPress, TXTLESSWT.KeyPress, TXTNETTWT.KeyPress, TXTPURITY.KeyPress, TXTFINEWT.KeyPress, TXTVOUCHERWT.KeyPress, TXTBOXWT.KeyPress, TXTWTWITHTAGS.KeyPress, TXTINITIALGROSSWT.KeyPress
        numdotkeypress(e, sender, Me)
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
            If EDIT = True Then
                PRINTREPORT(TEMPISSUENO)
                PRINTBARCODE()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTBARCODE()
        Try

            'FOR PRINTING EACH BARCODE
            If CHKPRINT.CheckState = CheckState.Checked Then
                BARCODEPRINTING(Val(TXTISSUENO.Text.Trim) & "-" & Val(TXTSRNO.Text.Trim), Val(TXTNETTWT.Text.Trim), TXTHUID.Text.Trim)

            Else

                'PRINT BARCODE
                Dim TEMPMSG As Integer = MsgBox("Wish to Print Barcode?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub

                'GET FRESH DATA FROM DATABASE (ONLY GRID)
                'THIS IS DONE COZ FOR MULTIUSER THE NOS WILL BE SAME
                'SO WE WILL ADD BARCODE IN SP AND THEN FETCH THAT DATA HERE AFTER THAT WE WILL PRINT BARCODES
                GRIDISSUE.RowCount = 0
                Dim OBJISSUE As New ClsIssueVoucher
                OBJISSUE.alParaval.Add(TEMPISSUENO)
                OBJISSUE.alParaval.Add(YearId)
                Dim dttable As DataTable = OBJISSUE.SELECTISSUE()
                For Each dr As DataRow In dttable.Rows
                    GRIDISSUE.Rows.Add(Val(dr("SRNO")), dr("ITEMNAME").ToString, dr("HSNCODE"), dr("GRIDREMARKS"), Format(Val(dr("PCS")), "0"), Format(Val(dr("GROSSWT")), "0.000"), Format(Val(dr("LESSWT")), "0.000"), Format(Val(dr("NETTWT")), "0.000"), Format(Val(dr("PURITY")), "0.00"), Format(Val(dr("FINEWT")), "0.000"), dr("HUID"))
                Next

                For Each ROW As DataGridViewRow In GRIDISSUE.Rows

                    'TO PRINT BARCODE FROM SELECTED SRNO
                    If (Val(TXTFROM.Text.Trim) > 0 And Val(TXTTO.Text.Trim) > 0) Then
                        If Val(ROW.Cells(gsrno.Index).Value) < Val(TXTFROM.Text.Trim) Or Val(ROW.Cells(gsrno.Index).Value) > Val(TXTTO.Text.Trim) Then GoTo NEXTLINE
                    End If

                    BARCODEPRINTING(TEMPISSUENO & "-" & Val(ROW.Cells(gsrno.Index).Value), Val(ROW.Cells(GNETTWT.Index).Value), ROW.Cells(GHUID.Index).Value)
NEXTLINE:

                Next

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ISSDATE.GotFocus
        ISSDATE.SelectAll()
    End Sub

    Private Sub ISSDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ISSDATE.Validating
        Try
            If ISSDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(ISSDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles RECDATE.GotFocus
        RECDATE.SelectAll()
    End Sub

    Private Sub RECDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RECDATE.Validating
        Try
            If RECDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(RECDATE.Text, TEMP) Then
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

    Private Sub TXTHUID_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTHUID.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" And Val(TXTPCS.Text.Trim) > 0 And Val(TXTGROSSWT.Text.Trim) > 0 And Val(TXTFINEWT.Text.Trim) > 0 Then
                FILLGRID()
                TOTAL()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub CMBHALLMARK_Enter(sender As Object, e As EventArgs) Handles CMBHALLMARK.Enter
        Try
            If CMBHALLMARK.Text.Trim = "" Then FILLHALLMARK(CMBHALLMARK)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHALLMARK_Validating(sender As Object, e As CancelEventArgs) Handles CMBHALLMARK.Validating
        Try
            If CMBHALLMARK.Text.Trim <> "" Then HALLMARKVALIDATE(CMBHALLMARK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class