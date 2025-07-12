
Imports BL

Public Class ChallanGelato


    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean
    Public TEMPCHALLANNO As Integer

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        CHALLANDATE.Text = Mydate
        CMBNAME.Text = ""
        CMBTRANSPORT.Text = ""
        CMBAGENT.Text = ""
        TXTREFNO.Clear()
        TXTBOXES.Clear()


        GRIDCHALLAN.RowCount = 0
        TXTSRNO.Text = 1
        TXTDESIGNNO.Clear()
        CMBITEMNAME.Text = ""
        TXT30.Clear()
        TXT32.Clear()
        TXT34.Clear()
        TXT36.Clear()
        TXT38.Clear()
        TXT40.Clear()
        TXT42.Clear()
        TXT44.Clear()
        TXT46.Clear()
        TXT48.Clear()
        TXT50.Clear()
        TXTQTY.Clear()
        TXTRATE.Clear()
        TXTAMOUNT.Clear()


        LBLTOTALQTY.Text = 0
        TXTTOTALAMT.Text = 0

        txtremarks.Clear()
        GETMAX_CHALLAN_NO()
        EP.Clear()

        lbllocked.Visible = False
        PBlock.Visible = False


        GRIDDOUBLECLICK = False

    End Sub

    Sub GETMAX_CHALLAN_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(GDN_no),0) + 1 ", "  GDNGELATO ", " AND GDN_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTCHALLANNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub Challan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.F8 Then
                GRIDCHALLAN.Focus()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{TAB}")
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D1) Then       'for CLEAR
                TabControl1.SelectedIndex = (0)
            ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.D2) Then       'for CLEAR
                TabControl1.SelectedIndex = (1)
            ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then       'for billno foucs
                tstxtbillno.Focus()
            ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
                e.SuppressKeyPress = True
            ElseIf e.KeyCode = Keys.Left And e.Alt = True Then
                Call toolprevious_Click(sender, e)
            ElseIf e.KeyCode = Keys.Right And e.Alt = True Then
                Call toolnext_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Keys.P Then
                Call PrintToolStripButton_Click(sender, e)
            ElseIf e.Alt = True And e.KeyCode = Windows.Forms.Keys.F1 Then
                Call OpenToolStripButton_Click(sender, e)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillname(CMBTRANSPORT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'TRANSPORT'")
        fillname(CMBAGENT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'AGENT'")
        FILLITEMNAME(CMBITEMNAME, EDIT)
    End Sub

    Private Sub Challan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'GDN'")
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
                Dim OBJINV As New ClsChallanGelato

                ALPARAVAL.Add(TEMPCHALLANNO)
                ALPARAVAL.Add(YearId)

                OBJINV.alParaval = ALPARAVAL
                DT = OBJINV.SELECTNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTCHALLANNO.Text = TEMPCHALLANNO
                        CHALLANDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        CMBTRANSPORT.Text = Convert.ToString(DR("TRANSPORT"))
                        CMBAGENT.Text = Convert.ToString(DR("AGENT"))
                        TXTREFNO.Text = Convert.ToString(DR("REFNO"))
                        TXTBOXES.Text = Val(DR("BOXES"))

                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        If Convert.ToBoolean(DR("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        '' grid 
                        GRIDCHALLAN.Rows.Add(Val(DR("SRNO")), DR("DESIGNNO"), DR("ITEMNAME"), Val(DR("SIZE30")), Val(DR("SIZE32")), Val(DR("SIZE34")), Val(DR("SIZE36")), Val(DR("SIZE38")), Val(DR("SIZE40")), Val(DR("SIZE42")), Val(DR("SIZE44")), Val(DR("SIZE46")), Val(DR("SIZE48")), Val(DR("SIZE50")), Format(Val(DR("QTY")), "0"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMT")), "0.00"))

                    Next

                    GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1
                End If
                CMBNAME.Focus()
                total()
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
            ALPARAVAL.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBTRANSPORT.Text.Trim)
            ALPARAVAL.Add(CMBAGENT.Text.Trim)
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(Val(TXTBOXES.Text.Trim))

            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))
            ALPARAVAL.Add(Val(TXTTOTALAMT.Text.Trim))

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            '' GRID PARAMETERS
            Dim SRNO As String = ""
            Dim DESIGNNO As String = ""
            Dim ITEM As String = ""
            Dim SIZE30 As String = ""
            Dim SIZE32 As String = ""
            Dim SIZE34 As String = ""
            Dim SIZE36 As String = ""
            Dim SIZE38 As String = ""
            Dim SIZE40 As String = ""
            Dim SIZE42 As String = ""
            Dim SIZE44 As String = ""
            Dim SIZE46 As String = ""
            Dim SIZE48 As String = ""
            Dim SIZE50 As String = ""
            Dim QTY As String = ""
            Dim RATE As String = ""
            Dim AMT As String = ""
            ' Dim GRNNO As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDCHALLAN.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        DESIGNNO = ROW.Cells(GDESIGNNO.Index).Value.ToString
                        ITEM = ROW.Cells(GITEMNAME.Index).Value.ToString
                        SIZE30 = Val(ROW.Cells(G30.Index).Value)
                        SIZE32 = Val(ROW.Cells(G32.Index).Value)
                        SIZE34 = Val(ROW.Cells(G34.Index).Value)
                        SIZE36 = Val(ROW.Cells(G36.Index).Value)
                        SIZE38 = Val(ROW.Cells(G38.Index).Value)
                        SIZE40 = Val(ROW.Cells(G40.Index).Value)
                        SIZE42 = Val(ROW.Cells(G42.Index).Value)
                        SIZE44 = Val(ROW.Cells(G44.Index).Value)
                        SIZE46 = Val(ROW.Cells(G46.Index).Value)
                        SIZE48 = Val(ROW.Cells(G48.Index).Value)
                        SIZE50 = Val(ROW.Cells(G50.Index).Value)
                        QTY = Val(ROW.Cells(GQTY.Index).Value)
                        RATE = Val(ROW.Cells(GRATE.Index).Value)
                        AMT = Val(ROW.Cells(GAMT.Index).Value)

                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        DESIGNNO = DESIGNNO & "|" & ROW.Cells(GDESIGNNO.Index).Value.ToString
                        ITEM = ITEM & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        SIZE30 = SIZE30 & "|" & Val(ROW.Cells(G30.Index).Value)
                        SIZE32 = SIZE32 & "|" & Val(ROW.Cells(G32.Index).Value)
                        SIZE34 = SIZE34 & "|" & Val(ROW.Cells(G34.Index).Value)
                        SIZE36 = SIZE36 & "|" & Val(ROW.Cells(G36.Index).Value)
                        SIZE38 = SIZE38 & "|" & Val(ROW.Cells(G38.Index).Value)
                        SIZE40 = SIZE40 & "|" & Val(ROW.Cells(G40.Index).Value)
                        SIZE42 = SIZE42 & "|" & Val(ROW.Cells(G42.Index).Value)
                        SIZE44 = SIZE44 & "|" & Val(ROW.Cells(G44.Index).Value)
                        SIZE46 = SIZE46 & "|" & Val(ROW.Cells(G46.Index).Value)
                        SIZE48 = SIZE48 & "|" & Val(ROW.Cells(G48.Index).Value)
                        SIZE50 = SIZE50 & "|" & Val(ROW.Cells(G50.Index).Value)
                        QTY = QTY & "|" & Val(ROW.Cells(GQTY.Index).Value)
                        RATE = RATE & "|" & Val(ROW.Cells(GRATE.Index).Value)
                        AMT = AMT & "|" & Val(ROW.Cells(GAMT.Index).Value)
                        'GRNNO = GRNNO & "|" & Val(ROW.Cells(GGRNNO.Index).Value)
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(DESIGNNO)
            ALPARAVAL.Add(ITEM)
            ALPARAVAL.Add(SIZE30)
            ALPARAVAL.Add(SIZE32)
            ALPARAVAL.Add(SIZE34)
            ALPARAVAL.Add(SIZE36)
            ALPARAVAL.Add(SIZE38)
            ALPARAVAL.Add(SIZE40)
            ALPARAVAL.Add(SIZE42)
            ALPARAVAL.Add(SIZE44)
            ALPARAVAL.Add(SIZE46)
            ALPARAVAL.Add(SIZE48)
            ALPARAVAL.Add(SIZE50)
            ALPARAVAL.Add(QTY)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(AMT)

            Dim OBJPUR As New ClsChallanGelato
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.SAVE()
                MsgBox("Details Added !!")
                TEMPCHALLANNO = DTTABLE.Rows(0).Item(0)

            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPCHALLANNO)
                IntResult = OBJPUR.UPDATE()
                MsgBox("Details Updated")
                EDIT = False

            End If
            PRINTREPORT(TEMPCHALLANNO)
            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub PRINTREPORT(ByVal CHALLANNO As Integer)
        Try
            If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJINVOICE As New SaleInvoiceDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "CHALLAN"
                OBJINVOICE.WHERECLAUSE = "{GDNGELATO.GDN_no}=" & Val(CHALLANNO) & " and {GDNGELATO.GDN_yearid}=" & YearId
                OBJINVOICE.Show()
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

        If GRIDCHALLAN.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If

        If PBlock.Visible = True Then
            EP.SetError(lbllocked, "Challan is Locked")
            bln = False
        End If

        If CHALLANDATE.Text = "__/__/____" Then
            EP.SetError(CHALLANDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(CHALLANDATE.Text) Then
                EP.SetError(CHALLANDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub total()

        LBLTOTALQTY.Text = "0.00"
        TXTTOTALAMT.Text = 0

        For Each row As DataGridViewRow In GRIDCHALLAN.Rows
            If Val(row.Cells(GQTY.Index).Value) <> 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0")
            If Val(row.Cells(GAMT.Index).Value) <> 0 Then TXTTOTALAMT.Text = Format(Val(TXTTOTALAMT.Text) + Val(row.Cells(GAMT.Index).Value), "0.000")
        Next

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

    Private Sub TXTSRNO_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSRNO.Enter
        TXTSRNO.Text = GRIDCHALLAN.RowCount + 1
    End Sub

    Private Sub CMBNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBNAME.KeyDown
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

    Private Sub CMBNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS", CMBTRANSPORT.Text, CMBAGENT.Text)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTRANSPORT.Enter
        Try
            If CMBTRANSPORT.Text.Trim = "" Then fillname(CMBTRANSPORT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBTRANSPORT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND LEDGERS.ACC_TYPE = 'TRANSPORT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBTRANSPORT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSPORT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANSPORT.Validating
        Try
            If CMBTRANSPORT.Text.Trim <> "" Then namevalidate(CMBTRANSPORT, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBAGENT.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry CREDITORS' AND LEDGERS.ACC_TYPE = 'AGENT'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBAGENT.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBAGENT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDCHALLAN.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDCHALLAN.Rows.Add(Val(TXTSRNO.Text.Trim), TXTDESIGNNO.Text.Trim, CMBITEMNAME.Text.Trim, Val(TXT30.Text.Trim), Val(TXT32.Text.Trim), Val(TXT34.Text.Trim), Val(TXT36.Text.Trim), Val(TXT38.Text.Trim), Val(TXT40.Text.Trim), Val(TXT42.Text.Trim), Val(TXT44.Text.Trim), Val(TXT46.Text.Trim), Val(TXT48.Text.Trim), Val(TXT50.Text.Trim), Format(Val(TXTQTY.Text.Trim), "0"), Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"))
            getsrno(GRIDCHALLAN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCHALLAN.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDCHALLAN.Item(GDESIGNNO.Index, TEMPROW).Value = TXTDESIGNNO.Text.Trim
            GRIDCHALLAN.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDCHALLAN.Item(G30.Index, TEMPROW).Value = Val(TXT30.Text.Trim)
            GRIDCHALLAN.Item(G32.Index, TEMPROW).Value = Val(TXT32.Text.Trim)
            GRIDCHALLAN.Item(G34.Index, TEMPROW).Value = Val(TXT34.Text.Trim)
            GRIDCHALLAN.Item(G36.Index, TEMPROW).Value = Val(TXT36.Text.Trim)
            GRIDCHALLAN.Item(G38.Index, TEMPROW).Value = Val(TXT38.Text.Trim)
            GRIDCHALLAN.Item(G40.Index, TEMPROW).Value = Val(TXT40.Text.Trim)
            GRIDCHALLAN.Item(G42.Index, TEMPROW).Value = Val(TXT42.Text.Trim)
            GRIDCHALLAN.Item(G44.Index, TEMPROW).Value = Val(TXT44.Text.Trim)
            GRIDCHALLAN.Item(G46.Index, TEMPROW).Value = Val(TXT46.Text.Trim)
            GRIDCHALLAN.Item(G48.Index, TEMPROW).Value = Val(TXT48.Text.Trim)
            GRIDCHALLAN.Item(G50.Index, TEMPROW).Value = Val(TXT50.Text.Trim)
            GRIDCHALLAN.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")
            GRIDCHALLAN.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text), "0.00")
            GRIDCHALLAN.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text), "0.00")
            GRIDDOUBLECLICK = False
        End If
        TXTAMOUNT.ReadOnly = True
        total()
        GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1

        TXTDESIGNNO.Clear()
        CMBITEMNAME.Text = ""
        TXT30.Clear()
        TXT32.Clear()
        TXT34.Clear()
        TXT36.Clear()
        TXT38.Clear()
        TXT40.Clear()
        TXT42.Clear()
        TXT44.Clear()
        TXT46.Clear()
        TXT48.Clear()
        TXT50.Clear()
        TXTQTY.Clear()
        TXTRATE.Clear()
        TXTAMOUNT.Clear()

        TXTSRNO.Text = Val(GRIDCHALLAN.RowCount) + 1
        TXTDESIGNNO.Focus()

    End Sub

    Sub editrow()
        Try
            If GRIDCHALLAN.CurrentRow.Index >= 0 AndAlso GRIDCHALLAN.Item(gsrno.Index, GRIDCHALLAN.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDCHALLAN.Item(gsrno.Index, GRIDCHALLAN.CurrentRow.Index).Value
                TXTDESIGNNO.Text = GRIDCHALLAN.Item(GDESIGNNO.Index, GRIDCHALLAN.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDCHALLAN.Item(GITEMNAME.Index, GRIDCHALLAN.CurrentRow.Index).Value.ToString
                TXT30.Text = Val(GRIDCHALLAN.Item(G30.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT32.Text = Val(GRIDCHALLAN.Item(G32.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT34.Text = Val(GRIDCHALLAN.Item(G34.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT36.Text = Val(GRIDCHALLAN.Item(G36.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT38.Text = Val(GRIDCHALLAN.Item(G38.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT40.Text = Val(GRIDCHALLAN.Item(G40.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT42.Text = Val(GRIDCHALLAN.Item(G42.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT44.Text = Val(GRIDCHALLAN.Item(G44.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT46.Text = Val(GRIDCHALLAN.Item(G46.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT48.Text = Val(GRIDCHALLAN.Item(G48.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXT50.Text = Val(GRIDCHALLAN.Item(G50.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXTQTY.Text = Val(GRIDCHALLAN.Item(GQTY.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXTRATE.Text = Val(GRIDCHALLAN.Item(GRATE.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDCHALLAN.Item(GAMT.Index, GRIDCHALLAN.CurrentRow.Index).Value)

                TEMPROW = GRIDCHALLAN.CurrentRow.Index
                TXTDESIGNNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDQUOTE_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHALLAN.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        editrow()
    End Sub

    Private Sub TXTAMOUNT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMOUNT.Validating
        If CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And Val(TXTRATE.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 Then
            fillgrid()
            total()
        Else
            MsgBox("Enter Proper Details")
            Exit Sub
        End If
    End Sub

    Sub calc()
        Try
            If Val(TXT30.Text.Trim) > 0 Or Val(TXT32.Text.Trim) > 0 Or Val(TXT34.Text.Trim) > 0 Or Val(TXT36.Text.Trim) > 0 Or Val(TXT38.Text.Trim) > 0 Or Val(TXT40.Text.Trim) > 0 Or Val(TXT42.Text.Trim) > 0 Or Val(TXT44.Text.Trim) > 0 Or Val(TXT46.Text.Trim) > 0 Or Val(TXT48.Text.Trim) > 0 Or Val(TXT50.Text.Trim) > 0 Then
                TXTQTY.Text = Val(TXT30.Text.Trim) + Val(TXT32.Text.Trim) + Val(TXT34.Text.Trim) + Val(TXT36.Text.Trim) + Val(TXT38.Text.Trim) + Val(TXT40.Text.Trim) + Val(TXT42.Text.Trim) + Val(TXT44.Text.Trim) + Val(TXT46.Text.Trim) + Val(TXT48.Text.Trim) + Val(TXT50.Text.Trim)
            End If
            TXTAMOUNT.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDCHALLAN.RowCount = 0
Line1:
            TEMPCHALLANNO = Val(TXTCHALLANNO.Text) - 1
            If TEMPCHALLANNO > 0 Then
                EDIT = True
                Challan_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCHALLAN.RowCount = 0 And TEMPCHALLANNO > 1 Then
                TXTCHALLANNO.Text = TEMPCHALLANNO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDCHALLAN.RowCount = 0
LINE1:
            TEMPCHALLANNO = Val(TXTCHALLANNO.Text) + 1
            GETMAX_CHALLAN_NO()
            Dim MAXNO As Integer = TXTCHALLANNO.Text.Trim
            CLEAR()
            If Val(TXTCHALLANNO.Text) - 1 >= TEMPCHALLANNO Then
                EDIT = True
                Challan_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDCHALLAN.RowCount = 0 And TEMPCHALLANNO < MAXNO Then
                TXTCHALLANNO.Text = TEMPCHALLANNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            GRIDCHALLAN.RowCount = 0
            TEMPCHALLANNO = Val(tstxtbillno.Text)
            If TEMPCHALLANNO > 0 Then
                EDIT = True
                Challan_Load(sender, e)
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

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Challan Locked")
                Exit Sub
            End If

            If EDIT = True Then
                Dim intresult As Integer
                Dim objpur As New ClsChallanGelato
                If MsgBox("Wish to Delete?", MsgBoxStyle.YesNo) = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPCHALLANNO)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.DELETE
                    MsgBox("Challan Deleted!!!")
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
            Dim OBJCHALLAN As New ChallanGelatoDetails
            OBJCHALLAN.MdiParent = MDIMain
            OBJCHALLAN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHALLAN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHALLAN.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHALLAN.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCHALLAN.Rows.RemoveAt(GRIDCHALLAN.CurrentRow.Index)
                total()
                getsrno(GRIDCHALLAN)
                total()

            ElseIf e.KeyCode = Keys.F8 Then
                editrow()
                'ElseIf e.KeyCode = Keys.F12 And gridorders.RowCount > 0 Then
                '    If gridorders.CurrentRow.Cells(GQUALITY.Index).Value <> "" Then gridorders.Rows.Add(CloneWithValues(gridorders.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
        If CMBITEMNAME.Text.Trim = "" Then cmdok.Focus()
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectAll()
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
        Try
            If CHALLANDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(CHALLANDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        CHALLANDATE.SelectAll()
    End Sub

    Private Sub TXTQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress, TXT30.KeyPress, TXT32.KeyPress, TXT34.KeyPress, TXT36.KeyPress, TXT38.KeyPress, TXT40.KeyPress, TXT42.KeyPress, TXT44.KeyPress, TXT46.KeyPress, TXT48.KeyPress, TXT50.KeyPress, TXTBOXES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPCHALLANNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating, TXT30.Validating, TXT32.Validating, TXT34.Validating, TXT36.Validating, TXT38.Validating, TXT40.Validating, TXT42.Validating, TXT44.Validating, TXT46.Validating, TXT48.Validating, TXT50.Validating, TXTQTY.Validating
        calc()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

End Class