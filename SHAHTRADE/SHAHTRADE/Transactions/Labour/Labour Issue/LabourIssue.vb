
Imports BL

Public Class LabourIssue

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean
    Public TEMPISSNO As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        ISSDATE.Text = Mydate

        CMBNAME.Text = ""
        CMBITEMNAME.Text = ""
        CMBPATTA.Text = ""
        CMBSIZE.Text = ""
        TXTISSQTY.Clear()
        TXTAVG.Clear()
        TXTTOTALQTY.Clear()
        txtremarks.Clear()

        GETMAX_ISSUE_NO()
        EP.Clear()
        lbllocked.Visible = False
        LBLCLOSED.Visible = False
        PBlock.Visible = False

    End Sub

    Sub GETMAX_ISSUE_NO()
        Dim DTTABLE As DataTable = getmax(" ISNULL(MAX(ISS_NO),0) + 1 ", "  LABOURISSUE ", " AND ISS_YEARID = " & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTISSNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        CLEAR()
        EDIT = False
        CMBNAME.Focus()
    End Sub

    Private Sub LabourIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
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

    Private Sub LabourIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJLABISS As New ClsLabourIssue

                ALPARAVAL.Add(TEMPISSNO)
                ALPARAVAL.Add(YearId)

                OBJLABISS.alParaval = ALPARAVAL
                DT = OBJLABISS.SELECTLABOURISSUE

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTISSNO.Text = TEMPISSNO
                        ISSDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        CMBITEMNAME.Text = Convert.ToString(DR("ITEMNAME"))
                        CMBPATTA.Text = Convert.ToString(DR("PATTA"))
                        CMBSIZE.Text = Convert.ToString(DR("SIZE"))
                        TXTISSQTY.Text = Val(DR("ISSQTY"))
                        TXTAVG.Text = Val(DR("AVG"))
                        TXTTOTALQTY.Text = Val(DR("TOTALQTY"))
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        If Val(DR("RECDQTY")) > 0 Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Convert.ToBoolean(DR("CLOSED")) = True Then
                            LBLCLOSED.Visible = True
                            PBlock.Visible = True
                        End If
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
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(Format(Convert.ToDateTime(ISSDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(CMBPATTA.Text.Trim)
            ALPARAVAL.Add(CMBSIZE.Text.Trim)

            ALPARAVAL.Add(Val(TXTISSQTY.Text.Trim))
            ALPARAVAL.Add(Val(TXTAVG.Text.Trim))
            ALPARAVAL.Add(Val(TXTTOTALQTY.Text.Trim))

            ALPARAVAL.Add(txtremarks.Text.Trim)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            Dim OBJISS As New ClsLabourIssue
            OBJISS.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJISS.SAVE()
                MsgBox("Details Added !!")

                TXTISSNO.Text = DTTABLE.Rows(0).Item(0)
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPISSNO)
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

        If CMBITEMNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBITEMNAME, "Please Enter Item Name")
            bln = False
        End If

        If CMBPATTA.Text.Trim.Length = 0 Then
            EP.SetError(CMBPATTA, "Please Enter Patta")
            bln = False
        End If

        If CMBSIZE.Text.Trim.Length = 0 Then
            EP.SetError(CMBSIZE, "Please Enter Size")
            bln = False
        End If

        If Val(TXTISSQTY.Text.Trim) = 0 Then
            EP.SetError(TXTISSQTY, "Please Enter Qty")
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
            TXTAVG.Text = 0
            TXTTOTALQTY.Text = 0

            'GET AVG
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("ISNULL(AVG_QTY,0) AS AVGQTY", "", " ITEMAVGCONFIG INNER JOIN ITEMMASTER ON ITEMAVGCONFIG.AVG_ITEMID = ITEMMASTER.ITEM_id INNER JOIN SIZEMASTER ON ITEMAVGCONFIG.AVG_SIZEID = SIZEMASTER.SIZE_id INNER JOIN PATTAMASTER ON ITEMAVGCONFIG.AVG_PATTAID = PATTAMASTER.PATTA_id ", " AND ITEMMASTER.ITEM_NAME = '" & CMBITEMNAME.Text.Trim & "' AND PATTAMASTER.PATTA_NAME = '" & CMBPATTA.Text.Trim & "' AND SIZEMASTER.SIZE_NAME = '" & CMBSIZE.Text.Trim & "' AND AVG_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then TXTAVG.Text = Val(DT.Rows(0).Item("AVGQTY"))
            TXTTOTALQTY.Text = Val(TXTISSQTY.Text.Trim) * Val(TXTAVG.Text.Trim)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTISSQTY.Validated, CMBITEMNAME.Validated, CMBPATTA.Validated, CMBSIZE.Validated
        CALC()
    End Sub

    Private Sub TXTISSQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTISSQTY.KeyPress, tstxtbillno.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            TXTTOTALQTY.Text = 0
Line1:
            TEMPISSNO = Val(TXTISSNO.Text) - 1
            If TEMPISSNO > 0 Then
                EDIT = True
                LabourIssue_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If Val(TXTTOTALQTY.Text.Trim) = 0 And TEMPISSNO > 1 Then
                TXTISSNO.Text = TEMPISSNO
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
            TEMPISSNO = Val(TXTISSNO.Text) + 1
            GETMAX_ISSUE_NO()
            Dim MAXNO As Integer = TXTISSNO.Text.Trim
            CLEAR()
            If Val(TXTISSNO.Text) - 1 >= TEMPISSNO Then
                EDIT = True
                LabourIssue_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If Val(TXTTOTALQTY.Text.Trim) = 0 And TEMPISSNO < MAXNO Then
                TXTISSNO.Text = TEMPISSNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            TXTTOTALQTY.Text = 0
            TEMPISSNO = Val(tstxtbillno.Text)
            If TEMPISSNO > 0 Then
                EDIT = True
                LabourIssue_Load(sender, e)
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
                Dim OBJISS As New ClsLabourIssue
                If MsgBox("Wish to Delete?", MsgBoxStyle.YesNo) = vbYes Then
                    OBJISS.alParaval.Add(TEMPISSNO)
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
            Dim OBJISS As New LabourIssueDetails
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

    Private Sub CMDCLOSED_Click(sender As Object, e As EventArgs) Handles CMDCLOSED.Click
        Try
            If EDIT = False Or LBLCLOSED.Visible = True Then Exit Sub

            If MsgBox("Wish to Close Issue", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.Execute_Any_String("UPDATE LABOURISSUE SET ISS_CLOSED = 'TRUE' WHERE ISS_NO = " & Val(TXTISSNO.Text.Trim) & " AND ISS_YEARID = " & YearId, "", "")
            MsgBox("Entry Locked")
            EDIT = False
            CLEAR()
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

    Private Sub INVDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ISSDATE.Validating
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


End Class