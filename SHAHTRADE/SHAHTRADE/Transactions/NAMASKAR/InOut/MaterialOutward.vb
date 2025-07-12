Imports System.ComponentModel
Imports BL

Public Class MaterialOutward
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPOUTWARDNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        TXTOUTWARDNO.Clear()
        OUTWARDDATE.Text = Mydate
        CMBNAME.Text = ""
        TXTREFNO.Clear()

        GRIDOUTWARD.RowCount = 0
        TXTSRNO.Text = 1
        CMBITEMNAME.Text = ""
        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTQTY.Clear()

        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()


        LBLTOTALQTY.Text = 0
        LBLTOTALAMOUNT.Text = 0.0
        LBLTOTALDISCAMT.Text = 0.0
        LBLTOTALOTHERCHGS.Text = 0.0
        LBLTOTALTAXABLEAMT.Text = 0.0
        LBLTOTALCGSTAMT.Text = 0
        LBLTOTALSGSTAMT.Text = 0
        LBLTOTALIGSTAMT.Text = 0
        LBLTOTALGRIDTOTAL.Text = 0.0
        txtremarks.Clear()
        TXTROUNDOFF.Clear()
        TXTGTOTAL.Clear()

        GETMAX_ISSUE_NO()
        EP.Clear()
        GRIDDOUBLECLICK = False

    End Sub

    Sub GETMAX_ISSUE_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(OUTWARD_no),0) + 1 ", " MATERIALOUTWARD ", " and OUTWARD_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTOUTWARDNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
    End Sub

    Private Sub MATERIALOUTWARD_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.F8 Then
                GRIDOUTWARD.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                TXTSRNO.Focus()
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
        FILLITEMNAME(CMBITEMNAME, EDIT)
    End Sub

    Private Sub MaterialOutward_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                Dim OBJISS As New ClsMaterialOutward

                ALPARAVAL.Add(TEMPOUTWARDNO)
                ALPARAVAL.Add(YearId)

                OBJISS.alParaval = ALPARAVAL
                DT = OBJISS.SELECTMATERIALOUTWARD()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTOUTWARDNO.Text = TEMPOUTWARDNO
                        OUTWARDDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        CMBNAME_Validated(sender, e)

                        TXTREFNO.Text = DR("REFNO")
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        TXTROUNDOFF.Text = Val(DR("ROUNDOFF"))
                        TXTGTOTAL.Text = Convert.ToString(DR("GTOTAL"))

                        GRIDOUTWARD.Rows.Add(Val(DR("SRNO")), DR("ITEMNAME").ToString, DR("HSNCODE"), DR("GRIDREMARKS"), Format(Val(DR("QTY")), "0.00"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMOUNT")), "0.00"), Format(Val(DR("DISCPER")), "0.00"), Format(Val(DR("DISCAMT")), "0.00"), Format(Val(DR("OTHERCHARGES")), "0.00"), Format(Val(DR("TAXABLEAMT")), "0.00"), Format(Val(DR("CGSTPER")), "0.00"), Format(Val(DR("CGSTAMT")), "0.00"), Format(Val(DR("SGSTPER")), "0.00"), Format(Val(DR("SGSTAMT")), "0.00"), Format(Val(DR("IGSTPER")), "0.00"), Format(Val(DR("IGSTAMT")), "0.00"), Format(Val(DR("GRIDTOTAL")), "0.00"))
                    Next

                    GRIDOUTWARD.FirstDisplayedScrollingRowIndex = GRIDOUTWARD.RowCount - 1
                End If
                OUTWARDDATE.Focus()
                TOTAL()
                GETSRNO(GRIDOUTWARD)
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
            ALPARAVAL.Add(Format(Convert.ToDateTime(OUTWARDDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALAMOUNT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALDISCAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALOTHERCHGS.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALTAXABLEAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALCGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALSGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALIGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALGRIDTOTAL.Text.Trim))
            ALPARAVAL.Add(Val(TXTROUNDOFF.Text.Trim))
            ALPARAVAL.Add(Val(TXTGTOTAL.Text.Trim))



            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            Dim SRNO As String = ""
            Dim ITEM As String = ""
            Dim HSNCODE As String = ""
            Dim GRIDREMARKS As String = ""
            Dim QTY As String = ""
            Dim RATE As String = ""
            Dim AMOUNT As String = ""
            Dim DISCPER As String = ""
            Dim DISCAMT As String = ""
            Dim OTHERCHARGES As String = ""
            Dim TAXABLEAMT As String = ""
            Dim CGSTPER As String = ""
            Dim CGSTAMT As String = ""
            Dim SGSTPER As String = ""
            Dim SGSTAMT As String = ""
            Dim IGSTPER As String = ""
            Dim IGSTAMT As String = ""
            Dim GRIDTOTAL As String = ""



            For Each ROW As Windows.Forms.DataGridViewRow In GRIDOUTWARD.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        ITEM = ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDREMARKS = ROW.Cells(GDESC.Index).Value.ToString
                        QTY = Val(ROW.Cells(GQTY.Index).Value)
                        RATE = Val(ROW.Cells(GRATE.Index).Value)
                        AMOUNT = Val(ROW.Cells(GAMOUNT.Index).Value)
                        DISCPER = ROW.Cells(GDISCPER.Index).Value.ToString
                        DISCAMT = Val(ROW.Cells(GDISCAMOUNT.Index).Value)
                        OTHERCHARGES = Val(ROW.Cells(GOTHERCHARGES.Index).Value)
                        TAXABLEAMT = Val(ROW.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = ROW.Cells(GCGSTPER.Index).Value.ToString
                        CGSTAMT = Val(ROW.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = ROW.Cells(GSGSTPER.Index).Value.ToString
                        SGSTAMT = Val(ROW.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = ROW.Cells(GIGSTPER.Index).Value.ToString
                        IGSTAMT = Val(ROW.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = Val(ROW.Cells(GGRIDTOTAL.Index).Value)


                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        ITEM = ITEM & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & ROW.Cells(GHSNCODE.Index).Value.ToString
                        GRIDREMARKS = GRIDREMARKS & "|" & ROW.Cells(GDESC.Index).Value.ToString
                        QTY = QTY & "|" & Val(ROW.Cells(GQTY.Index).Value)
                        RATE = RATE & "|" & Val(ROW.Cells(GRATE.Index).Value)
                        AMOUNT = AMOUNT & "|" & Val(ROW.Cells(GAMOUNT.Index).Value)
                        DISCPER = DISCPER & "|" & Val(ROW.Cells(GDISCPER.Index).Value)
                        DISCAMT = DISCAMT & "|" & Val(ROW.Cells(GDISCAMOUNT.Index).Value)
                        OTHERCHARGES = OTHERCHARGES & "|" & Val(ROW.Cells(GOTHERCHARGES.Index).Value)
                        TAXABLEAMT = TAXABLEAMT & "|" & Val(ROW.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = CGSTPER & "|" & Val(ROW.Cells(GCGSTPER.Index).Value)
                        CGSTAMT = CGSTAMT & "|" & Val(ROW.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = SGSTPER & "|" & Val(ROW.Cells(GSGSTPER.Index).Value)
                        SGSTAMT = SGSTAMT & "|" & Val(ROW.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = IGSTPER & "|" & Val(ROW.Cells(GIGSTPER.Index).Value)
                        IGSTAMT = IGSTAMT & "|" & Val(ROW.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = GRIDTOTAL & "|" & Val(ROW.Cells(GGRIDTOTAL.Index).Value)
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(ITEM)
            ALPARAVAL.Add(HSNCODE)
            ALPARAVAL.Add(GRIDREMARKS)
            ALPARAVAL.Add(QTY)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(AMOUNT)
            ALPARAVAL.Add(DISCPER)
            ALPARAVAL.Add(DISCAMT)
            ALPARAVAL.Add(OTHERCHARGES)
            ALPARAVAL.Add(TAXABLEAMT)
            ALPARAVAL.Add(CGSTPER)
            ALPARAVAL.Add(CGSTAMT)
            ALPARAVAL.Add(SGSTPER)
            ALPARAVAL.Add(SGSTAMT)
            ALPARAVAL.Add(IGSTPER)
            ALPARAVAL.Add(IGSTAMT)
            ALPARAVAL.Add(GRIDTOTAL)


            Dim OBJPUR As New ClsMaterialOutward
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.SAVE()
                MsgBox("Details Added !!")
                TEMPOUTWARDNO = DTTABLE.Rows(0).Item(0)
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPOUTWARDNO)
                IntResult = OBJPUR.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            PRINTREPORT(TEMPOUTWARDNO)

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
            If MsgBox("Wish to Print?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJISSOICE As New SaleInvoiceDesign
                OBJISSOICE.MdiParent = MDIMain
                OBJISSOICE.FRMSTRING = "INVOICE"
                OBJISSOICE.WHERECLAUSE = "{MATERIALOUTWARD.OUTWARD_no}=" & Val(INVNO) & " and {MATERIALOUTWARD.OUTWARD_yearid}=" & YearId
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

        If GRIDOUTWARD.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If

        If OUTWARDDATE.Text = "__/__/____" Then
            EP.SetError(OUTWARDDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(OUTWARDDATE.Text) Then
                EP.SetError(OUTWARDDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub TOTAL()
        Try

            LBLTOTALQTY.Text = 0
            LBLTOTALAMOUNT.Text = 0.0
            LBLTOTALDISCAMT.Text = 0.0
            LBLTOTALOTHERCHGS.Text = 0.0
            LBLTOTALTAXABLEAMT.Text = 0.0
            LBLTOTALCGSTAMT.Text = 0.0
            LBLTOTALSGSTAMT.Text = 0.0
            LBLTOTALIGSTAMT.Text = 0.0
            LBLTOTALGRIDTOTAL.Text = 0.0
            TXTROUNDOFF.Clear()
            TXTGTOTAL.Clear()


            Dim dt As New DataTable
            Dim objcmn As New ClsCommon

            For Each row As DataGridViewRow In GRIDOUTWARD.Rows
                If Val(row.Cells(GQTY.Index).Value) > 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0.00")
                If Val(row.Cells(GAMOUNT.Index).Value) > 0 Then LBLTOTALAMOUNT.Text = Format(Val(LBLTOTALAMOUNT.Text) + Val(row.Cells(GAMOUNT.Index).Value), "0.00")
                If Val(row.Cells(GDISCAMOUNT.Index).Value) > 0 Then LBLTOTALDISCAMT.Text = Format(Val(LBLTOTALDISCAMT.Text) + Val(row.Cells(GDISCAMOUNT.Index).Value), "0.00")
                If Val(row.Cells(GOTHERCHARGES.Index).Value) > 0 Then LBLTOTALOTHERCHGS.Text = Format(Val(LBLTOTALOTHERCHGS.Text) + Val(row.Cells(GOTHERCHARGES.Index).Value), "0.00")
                If Val(row.Cells(GTAXABLEAMT.Index).Value) > 0 Then LBLTOTALTAXABLEAMT.Text = Format(Val(LBLTOTALTAXABLEAMT.Text) + Val(row.Cells(GTAXABLEAMT.Index).Value), "0.00")
                If Val(row.Cells(GCGSTAMT.Index).Value) > 0 Then LBLTOTALCGSTAMT.Text = Format(Val(LBLTOTALCGSTAMT.Text) + Val(row.Cells(GCGSTAMT.Index).Value), "0.00")
                If Val(row.Cells(GSGSTAMT.Index).Value) > 0 Then LBLTOTALSGSTAMT.Text = Format(Val(LBLTOTALSGSTAMT.Text) + Val(row.Cells(GSGSTAMT.Index).Value), "0.00")
                If Val(row.Cells(GIGSTAMT.Index).Value) > 0 Then LBLTOTALIGSTAMT.Text = Format(Val(LBLTOTALIGSTAMT.Text) + Val(row.Cells(GIGSTAMT.Index).Value), "0.00")
                If Val(row.Cells(GGRIDTOTAL.Index).Value) > 0 Then LBLTOTALGRIDTOTAL.Text = Format(Val(LBLTOTALGRIDTOTAL.Text) + Val(row.Cells(GGRIDTOTAL.Index).Value), "0.00")

                TXTGTOTAL.Text = Format(Val(LBLTOTALGRIDTOTAL.Text), "0")
                TXTROUNDOFF.Text = Format(Val(TXTGTOTAL.Text) - Val(LBLTOTALGRIDTOTAL.Text), "0.00")
                TXTGTOTAL.Text = Format(Val(TXTGTOTAL.Text), "0.00")

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
            If GRIDOUTWARD.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDOUTWARD.Rows(GRIDOUTWARD.RowCount - 1).Cells(gsrno.Index).Value) + 1
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

        GRIDOUTWARD.Enabled = True

        If GRIDDOUBLECLICK = False Then
            TXTSRNO.Text = GRIDOUTWARD.RowCount + 1
            GRIDOUTWARD.Rows.Add(Val(TXTSRNO.Text.Trim), CMBITEMNAME.Text.Trim, TXTHSNCODE.Text.Trim, TXTGRIDDESC.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0.00"), Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"), Format(Val(TXTDISCPER.Text.Trim), "0.00"), Format(Val(TXTDISCAMT.Text.Trim), "0.00"), Format(Val(TXTOTHERCHARGES.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Format(Val(TXTCGSTPER.Text.Trim), "0.00"), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Format(Val(TXTSGSTPER.Text.Trim), "0.00"), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Format(Val(TXTIGSTPER.Text.Trim), "0.00"), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"))
            GETSRNO(GRIDOUTWARD)

        ElseIf GRIDDOUBLECLICK = True Then
            GRIDOUTWARD.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDOUTWARD.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text
            GRIDOUTWARD.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDOUTWARD.Item(GDESC.Index, TEMPROW).Value = TXTGRIDDESC.Text.Trim
            GRIDOUTWARD.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")
            GRIDOUTWARD.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text), "0.00")
            GRIDOUTWARD.Item(GAMOUNT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text), "0.00")
            GRIDOUTWARD.Item(GDISCPER.Index, TEMPROW).Value = Format(Val(TXTDISCPER.Text), "0.00")
            GRIDOUTWARD.Item(GDISCAMOUNT.Index, TEMPROW).Value = Format(Val(TXTDISCAMT.Text), "0.00")
            GRIDOUTWARD.Item(GOTHERCHARGES.Index, TEMPROW).Value = Format(Val(TXTOTHERCHARGES.Text), "0.00")
            GRIDOUTWARD.Item(GTAXABLEAMT.Index, TEMPROW).Value = Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00")
            GRIDOUTWARD.Item(GCGSTPER.Index, TEMPROW).Value = Val(TXTCGSTPER.Text.Trim)
            GRIDOUTWARD.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDOUTWARD.Item(GSGSTPER.Index, TEMPROW).Value = Val(TXTSGSTPER.Text.Trim)
            GRIDOUTWARD.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDOUTWARD.Item(GIGSTPER.Index, TEMPROW).Value = Val(TXTIGSTPER.Text.Trim)
            GRIDOUTWARD.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDOUTWARD.Item(GGRIDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00")


            GRIDDOUBLECLICK = False
        End If

        TOTAL()
        GRIDOUTWARD.FirstDisplayedScrollingRowIndex = GRIDOUTWARD.RowCount - 1

        CMBITEMNAME.Text = ""
        TXTHSNCODE.Clear()
        TXTGRIDDESC.Clear()
        TXTQTY.Clear()
        TXTRATE.Clear()
        TXTAMOUNT.Clear()
        TXTDISCPER.Clear()
        TXTDISCAMT.Clear()
        TXTOTHERCHARGES.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()

        TXTGRIDTOTAL.Clear()
        TXTSRNO.Text = Val(GRIDOUTWARD.RowCount) + 1

        If ClientName = "NAMASKAR" Then TXTBARCODE.Focus() Else CMBITEMNAME.Focus()

    End Sub

    Private Sub GRIDISSUE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDOUTWARD.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        EDITROW()
    End Sub

    Sub EDITROW()
        Try
            If GRIDOUTWARD.CurrentRow.Index >= 0 And GRIDOUTWARD.Item(gsrno.Index, GRIDOUTWARD.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDOUTWARD.Item(gsrno.Index, GRIDOUTWARD.CurrentRow.Index).Value
                CMBITEMNAME.Text = GRIDOUTWARD.Item(GITEMNAME.Index, GRIDOUTWARD.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDOUTWARD.Item(GHSNCODE.Index, GRIDOUTWARD.CurrentRow.Index).Value.ToString
                TXTGRIDDESC.Text = GRIDOUTWARD.Item(GDESC.Index, GRIDOUTWARD.CurrentRow.Index).Value.ToString
                TXTQTY.Text = Val(GRIDOUTWARD.Item(GQTY.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTRATE.Text = Val(GRIDOUTWARD.Item(GRATE.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDOUTWARD.Item(GAMOUNT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTDISCPER.Text = Val(GRIDOUTWARD.Item(GDISCPER.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTDISCAMT.Text = Val(GRIDOUTWARD.Item(GDISCAMOUNT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTOTHERCHARGES.Text = GRIDOUTWARD.Item(GOTHERCHARGES.Index, GRIDOUTWARD.CurrentRow.Index).Value.ToString

                TXTTAXABLEAMT.Text = Val(GRIDOUTWARD.Item(GTAXABLEAMT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTCGSTPER.Text = Val(GRIDOUTWARD.Item(GCGSTPER.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTCGSTAMT.Text = Val(GRIDOUTWARD.Item(GCGSTAMT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTSGSTPER.Text = Val(GRIDOUTWARD.Item(GSGSTPER.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTSGSTAMT.Text = Val(GRIDOUTWARD.Item(GSGSTAMT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTIGSTPER.Text = Val(GRIDOUTWARD.Item(GIGSTPER.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTIGSTAMT.Text = Val(GRIDOUTWARD.Item(GIGSTAMT.Index, GRIDOUTWARD.CurrentRow.Index).Value)
                TXTGRIDTOTAL.Text = Val(GRIDOUTWARD.Item(GGRIDTOTAL.Index, GRIDOUTWARD.CurrentRow.Index).Value)

                TEMPROW = GRIDOUTWARD.CurrentRow.Index
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDOUTWARD.RowCount = 0
Line1:
            TEMPOUTWARDNO = Val(TXTOUTWARDNO.Text) - 1
            If TEMPOUTWARDNO > 0 Then
                EDIT = True
                MaterialOutward_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDOUTWARD.RowCount = 0 And TEMPOUTWARDNO > 1 Then
                TXTOUTWARDNO.Text = TEMPOUTWARDNO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDOUTWARD.RowCount = 0
LINE1:
            TEMPOUTWARDNO = Val(TXTOUTWARDNO.Text) + 1
            GETMAX_ISSUE_NO()
            Dim MAXNO As Integer = TXTOUTWARDNO.Text.Trim
            CLEAR()
            If Val(TXTOUTWARDNO.Text) - 1 >= TEMPOUTWARDNO Then
                EDIT = True
                MaterialOutward_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDOUTWARD.RowCount = 0 And TEMPOUTWARDNO < MAXNO Then
                TXTOUTWARDNO.Text = TEMPOUTWARDNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            If Val(tstxtbillno.Text.Trim) = 0 Then Exit Sub
            GRIDOUTWARD.RowCount = 0
            TEMPOUTWARDNO = Val(tstxtbillno.Text)
            If TEMPOUTWARDNO > 0 Then
                EDIT = True
                MaterialOutward_Load(sender, e)
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
                Dim objpur As New ClsMaterialOutward
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPOUTWARDNO)
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

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            Dim OBJISS As New MaterialOutwardDetails
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

    Private Sub GRIDISSUE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDOUTWARD.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDOUTWARD.RowCount > 0 Then

                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If

                GRIDOUTWARD.Rows.RemoveAt(GRIDOUTWARD.CurrentRow.Index)
                TOTAL()
                GETSRNO(GRIDOUTWARD)
                TOTAL()

            ElseIf e.KeyCode = Keys.F8 Then
                EDITROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        If ClientName = "HMENTERPRISE" Then numdotkeypress(e, sender, Me) Else numkeypress(e, sender, Me)
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
        CALC()
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim = "" Then txtremarks.Focus()
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPOUTWARDNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OUTWARDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles OUTWARDDATE.GotFocus
        OUTWARDDATE.SelectAll()
    End Sub
    Private Sub OUTWARDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OUTWARDDATE.Validating
        Try
            If OUTWARDDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(OUTWARDDATE.Text, TEMP) Then
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
            TXTCGSTPER.Clear()
            TXTCGSTAMT.Clear()
            TXTSGSTPER.Clear()
            TXTSGSTAMT.Clear()
            TXTIGSTPER.Clear()
            TXTIGSTAMT.Clear()

            If CMBITEMNAME.Text.Trim <> "" And Convert.ToDateTime(OUTWARDDATE.Text).Date >= "01/07/2017" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If CMBNAME.Text.Trim <> "" Then
                        If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                            TXTIGSTPER.Text = 0
                            TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                            TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                        Else
                            TXTCGSTPER.Text = 0
                            TXTSGSTPER.Text = 0
                            TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                        End If
                    End If
                End If
            End If

            TOTAL()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text.Trim <> "" Then
            'GET REGISTER , AGENCT AND TRANS
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid AND LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
            If DT.Rows.Count > 0 Then
                TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
                TXTDISCPER.Text = Val(DT.Rows(0).Item("DISCPER"))
            End If
            GETHSNCODE()
        End If
    End Sub



    Private Sub TXTGRIDTOTAL_Validated(sender As Object, e As EventArgs) Handles TXTGRIDTOTAL.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And Val(TXTGRIDTOTAL.Text.Trim) > 0 Then
                FILLGRID()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTGRIDTOTAL.Text = 0.00
            TXTCGSTAMT.Text = 0.00
            TXTSGSTAMT.Text = 0.00
            TXTIGSTAMT.Text = 0.00
            TXTAMOUNT.Clear()
            TXTDISCAMT.Clear()




            TXTAMOUNT.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTRATE.Text.Trim), "0.00")

            If Val(TXTDISCPER.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 Then TXTDISCAMT.Text = Format((Val(TXTAMOUNT.Text) * Val(TXTDISCPER.Text)) / 100, "0.00")

            TXTTAXABLEAMT.Text = Format(Val(TXTAMOUNT.Text.Trim) - Val(TXTDISCAMT.Text.Trim) + Val(TXTOTHERCHARGES.Text.Trim), "0.00")
            TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTDISCPER_Validated(sender As Object, e As EventArgs) Handles TXTDISCPER.Validated, TXTRATE.Validated, TXTOTHERCHARGES.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTBARCODE_Validated(sender As Object, e As EventArgs) Handles TXTBARCODE.Validated
        Try
            If TXTBARCODE.Text.Trim.Length > 0 Then

                Dim OBJCMN As New ClsCommon
                Dim dt As DataTable = OBJCMN.search(" ITEMMASTER.ITEM_name AS ITEMNAME, HSNMASTER.HSN_CODE AS HSNCODE, ISNULL(ITEM_RATE,0) AS RATE ", "", " ITEMMASTER INNER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID ", " AND ITEM_BARCODE = '" & TXTBARCODE.Text.Trim & "' and ITEM_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    CMBITEMNAME.Text = dt.Rows(0).Item("ITEMNAME")
                    TXTHSNCODE.Text = dt.Rows(0).Item("HSNCODE")
                    TXTRATE.Text = Format(Val(dt.Rows(0).Item("RATE")), "0.00")
                    TXTQTY.Text = 1
                    GETHSNCODE()
                    TXTBARCODE.Clear()
                    TXTQTY.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTQTY_Validated(sender As Object, e As EventArgs) Handles TXTQTY.Validated
        Try
            CALC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

