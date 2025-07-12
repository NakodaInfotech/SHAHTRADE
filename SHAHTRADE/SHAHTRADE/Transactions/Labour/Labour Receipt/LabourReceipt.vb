
Imports BL

Public Class LabourReceipt

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPRECNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        RECDATE.Text = Mydate

        CMBNAME.Text = ""
        CMBNAME.Enabled = True
        TXTISSNO.Clear()
        ISSDATE.Clear()
        CMBITEMNAME.Text = ""
        CMBPATTA.Text = ""
        CMBSIZE.Text = ""
        TXTISSQTY.Clear()
        TXTAVG.Clear()
        TXTTOTALQTY.Clear()

        TXTRECDGROSS.Clear()
        TXTRECDDOZEN.Clear()
        TXTRECDPCS.Clear()
        TXTTOTALRECDQTY.Clear()
        TXTDIFF.Clear()
        TXTRATE.Clear()
        TXTAMOUNT.Clear()

        txtremarks.Clear()

        GETMAX_REC_NO()
        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False

    End Sub

    Sub GETMAX_REC_NO()
        Dim DTTABLE As DataTable = getmax(" ISNULL(MAX(REC_NO),0) + 1 ", "  LabourReceipt ", " AND REC_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTRECNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub LabourReceipt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If ERRORVALID() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then CMDSAVE_Click(sender, e)
                End If
                Me.Close()
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
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        fillname(CMBNAME, EDIT, " and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        FILLITEMNAME(CMBITEMNAME, EDIT)
        FILLPATTA(CMBPATTA, EDIT)
        FILLSIZE(CMBSIZE, EDIT)
    End Sub

    Private Sub LabourReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LABOUR'")
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
                Dim OBJLABREC As New ClsLabourReceipt

                ALPARAVAL.Add(TEMPRECNO)
                ALPARAVAL.Add(YearId)

                OBJLABREC.alParaval = ALPARAVAL
                DT = OBJLABREC.SELECTLABOURRECEIPT

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTRECNO.Text = TEMPRECNO
                        RECDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        CMBNAME.Enabled = False
                        TXTISSNO.Text = Val(DR("ISSUENO"))
                        ISSDATE.Text = Format(Convert.ToDateTime(DR("ISSDATE")).Date, "dd/MM/yyyy")
                        CMBITEMNAME.Text = Convert.ToString(DR("ITEMNAME"))
                        CMBPATTA.Text = Convert.ToString(DR("PATTA"))
                        CMBSIZE.Text = Convert.ToString(DR("SIZE"))
                        TXTISSQTY.Text = Val(DR("ISSQTY"))
                        TXTAVG.Text = Val(DR("AVG"))
                        TXTTOTALQTY.Text = Val(DR("TOTALQTY"))

                        TXTRECDGROSS.Text = Val(DR("RECDGROSS"))
                        TXTRECDDOZEN.Text = Val(DR("RECDDOZEN"))
                        TXTRECDPCS.Text = Val(DR("RECDPCS"))
                        TXTTOTALRECDQTY.Text = Val(DR("TOTALRECDQTY"))
                        TXTDIFF.Text = Val(DR("DIFF"))
                        TXTRATE.Text = Val(DR("RATE"))
                        TXTAMOUNT.Text = Val(DR("AMOUNT"))

                        txtremarks.Text = Convert.ToString(DR("REMARKS"))
                    Next

                End If
                CMBNAME.Focus()
                CALC()
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

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(RECDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(Val(TXTISSNO.Text.Trim))
            ALPARAVAL.Add(Format(Convert.ToDateTime(ISSDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(CMBPATTA.Text.Trim)
            ALPARAVAL.Add(CMBSIZE.Text.Trim)

            ALPARAVAL.Add(Val(TXTISSQTY.Text.Trim))
            ALPARAVAL.Add(Val(TXTAVG.Text.Trim))
            ALPARAVAL.Add(Val(TXTTOTALQTY.Text.Trim))

            ALPARAVAL.Add(Val(TXTRECDGROSS.Text.Trim))
            ALPARAVAL.Add(Val(TXTRECDDOZEN.Text.Trim))
            ALPARAVAL.Add(Val(TXTRECDPCS.Text.Trim))
            ALPARAVAL.Add(Val(TXTTOTALRECDQTY.Text.Trim))
            ALPARAVAL.Add(Val(TXTDIFF.Text.Trim))
            ALPARAVAL.Add(Val(TXTRATE.Text.Trim))
            ALPARAVAL.Add(Val(TXTAMOUNT.Text.Trim))

            ALPARAVAL.Add(txtremarks.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            Dim OBJISS As New ClsLabourReceipt
            OBJISS.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJISS.SAVE()
                MsgBox("Details Added !!")

                TXTRECNO.Text = DTTABLE.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPRECNO)
                IntResult = OBJISS.UPDATE()
                MsgBox("Details Updated")
                EDIT = False

            End If

            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True

        If CMBNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBNAME, "Please Enter Name")
            bln = False
        End If


        If Val(TXTISSQTY.Text.Trim) = 0 Then
            EP.SetError(TXTISSQTY, "Please Select Issue Entry")
            bln = False
        End If


        If Val(TXTRATE.Text.Trim) = 0 Then
            EP.SetError(TXTRATE, "Enter Rate")
            bln = False
        End If


        If Val(TXTTOTALRECDQTY.Text.Trim) = 0 Then
            EP.SetError(TXTRATE, "Enter Rate")
            bln = False
        End If

        If Val(TXTDIFF.Text.Trim) < 0 Then
            EP.SetError(TXTDIFF, "Receipt Above Average not Allowed")
            bln = False
        End If


        If RECDATE.Text = "__/__/____" Then
            EP.SetError(RECDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(RECDATE.Text) Then
                EP.SetError(RECDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If


        Return bln
    End Function

    Private Sub cmbname_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBNAME.Enter
        Try
            If CMBNAME.Text.Trim = "" Then fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNAME.Validating
        Try
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS'", "SUNDRY CREDITORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub CALC()
        Try
            TXTTOTALRECDQTY.Text = (Val(TXTRECDGROSS.Text.Trim) * 144) + (Val(TXTRECDDOZEN.Text.Trim) * 12) + Val(TXTRECDPCS.Text.Trim)
            TXTDIFF.Text = Val(TXTTOTALQTY.Text.Trim) - Val(TXTTOTALRECDQTY.Text.Trim)
            TXTAMOUNT.Text = Format((Val(TXTRATE.Text.Trim) / 144) * Val(TXTTOTALRECDQTY.Text.Trim), "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRECDPCS_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTRECDGROSS.Validated, TXTRECDDOZEN.Validated, TXTRECDPCS.Validated, TXTRATE.Validated
        CALC()
    End Sub

    Private Sub TXTISSQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTISSQTY.KeyPress, tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            TXTTOTALQTY.Text = 0
Line1:
            TEMPRECNO = Val(TXTRECNO.Text) - 1
            If TEMPRECNO > 0 Then
                EDIT = True
                LabourReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If Val(TXTTOTALQTY.Text.Trim) = 0 And TEMPRECNO > 1 Then
                TXTRECNO.Text = TEMPRECNO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            TXTTOTALQTY.Text = 0
LINE1:
            TEMPRECNO = Val(TXTRECNO.Text) + 1
            GETMAX_REC_NO()
            Dim MAXNO As Integer = TXTRECNO.Text.Trim
            CLEAR()
            If Val(TXTRECNO.Text) - 1 >= TEMPRECNO Then
                EDIT = True
                LabourReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If Val(TXTTOTALQTY.Text.Trim) = 0 And TEMPRECNO < MAXNO Then
                TXTRECNO.Text = TEMPRECNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            TXTTOTALQTY.Text = 0
            TEMPRECNO = Val(tstxtbillno.Text)
            If TEMPRECNO > 0 Then
                EDIT = True
                LabourReceipt_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If lbllocked.Visible = True Then
                EP.SetError(lbllocked, "Entry Locked")
                Exit Sub
            End If

            If EDIT = True Then
                Dim intresult As Integer
                Dim OBJISS As New ClsLabourReceipt
                If MsgBox("Wish to Delete?", MsgBoxStyle.YesNo) = vbYes Then
                    OBJISS.alParaval.Add(TEMPRECNO)
                    OBJISS.alParaval.Add(YearId)
                    intresult = OBJISS.DELETE
                    MsgBox("Labour Issue Deleted!!!")
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
            Dim OBJISS As New LabourReceiptDetails
            OBJISS.MdiParent = MDIMain
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call CMDSAVE_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub CMDSELECTISSUE_Click(sender As Object, e As EventArgs) Handles CMDSELECTISSUE.Click
        Try

            Dim OBJSELECT As New SelectLabourIssue
            OBJSELECT.LABOURNAME = CMBNAME.Text.Trim
            Dim DT As DataTable = OBJSELECT.DT
            OBJSELECT.ShowDialog()

            Dim objcmn As New ClsCommon
            For Each DR As DataRow In DT.Rows
                TXTISSNO.Text = Val(DR("ISSUENO"))
                ISSDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                CMBNAME.Text = Convert.ToString(DR("NAME"))
                CMBITEMNAME.Text = Convert.ToString(DR("ITEMNAME"))
                CMBPATTA.Text = Convert.ToString(DR("PATTA"))
                CMBSIZE.Text = Convert.ToString(DR("SIZE"))
                TXTISSQTY.Text = Val(DR("ISSQTY"))
                TXTAVG.Text = Val(DR("AVG"))
                TXTTOTALQTY.Text = Val(DR("TOTALQTY"))
                TXTRATE.Text = Val(DR("RATE"))
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
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

    Private Sub INVDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles RECDATE.Validating
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

End Class