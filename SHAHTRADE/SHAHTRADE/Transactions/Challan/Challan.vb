Imports BL

Public Class Challan

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, gridUPLOADDoubleClick As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPCHALLANNO As Integer
    Public tempmsg As String

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        CMBNAME.Text = ""
        TXTREFNO.Clear()
        TXTDELTHROUGH.Clear()
        VALIDTILL.Text = DateAdd(DateInterval.Month, 1, Mydate)
        CHALLANDATE.Text = Mydate

        GRIDCHALLAN.RowCount = 0
        TXTSRNO.Text = 1
        TXTPARTNO.Clear()
        CMBITEMNAME.Text = ""
        CMBOTHERCHGS.Text = ""
        CMBEXTRACHGS.Text = ""
        TXTMAKE.Clear()
        TXTQTY.Clear()
        CMBUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()
        TXTQUOTENO.Clear()

        TXTDESPATCHEDTO.Clear()
        DELIVERYDATE.Clear()
        TXTLRNO.Clear()
        LRDATE.Clear()
        PODATE.Clear()
        TXTPONO.Clear()

        CMDSELECTQUOTE.Enabled = True

        LBLTOTALQTY.Text = 0
        TXTTOTAL.Text = 0
        CMBTAX.Text = ""
        TXTTAX.Text = 0
        TXTROUNDOFF.Text = 0
        TXTGTOTAL.Text = 0
        TXTSUBTOTAL.Text = 0
        txtremarks.Clear()
        txtotherchg.Text = 0.0
        TXTEXTRACHGS.Text = 0.0

        cmbaddsub.SelectedIndex = 0
        CMBEXTRAADDSUB.SelectedIndex = 0

        GETMAX_CHALLAN_NO()
        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False


        GRIDDOUBLECLICK = False
        gridUPLOADDoubleClick = False

        txtuploadsrno.Text = 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()
        gridupload.RowCount = 0

    End Sub

    Sub GETMAX_CHALLAN_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(GDN_no),0) + 1 ", "  GDN ", " AND GDN_YEARID = " & YearId)
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
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLCMB()
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        filltax(CMBTAX, EDIT)
        FILLITEMNAME(CMBITEMNAME, EDIT)
        fillunit(CMBUNIT)
        fillname(CMBOTHERCHGS, EDIT, "")
        fillname(CMBEXTRACHGS, EDIT, "")
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
                Dim OBJINV As New ClsChallan

                ALPARAVAL.Add(TEMPCHALLANNO)
                ALPARAVAL.Add(YearId)

                OBJINV.alParaval = ALPARAVAL
                DT = OBJINV.selectNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTCHALLANNO.Text = TEMPCHALLANNO
                        CHALLANDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        TXTREFNO.Text = Convert.ToString(DR("REFNO"))
                        TXTDELTHROUGH.Text = Convert.ToString(DR("DELIVERYTHROUGH"))
                        'VALIDTILL.Text = Format(Convert.ToDateTime(DR("VALIDTILL")).Date, "dd/MM/yyyy")
                        VALIDTILL.Text = DR("VALIDTILL")
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        TXTDESPATCHEDTO.Text = Convert.ToString(DR("DESPATCHEDTO"))
                        'If DR("DELIVERYDATE") <> "" Then DELIVERYDATE.Text = Format(Convert.ToDateTime(DR("DELIVERYDATE")).Date, "dd/MM/yyyy")
                        DELIVERYDATE.Text = DR("DELIVERYDATE")
                        TXTLRNO.Text = Convert.ToString(DR("LRNO"))
                        'If DR("LRDATE") <> "" Then LRDATE.Text = Format(Convert.ToDateTime(DR("LRDATE")).Date, "dd/MM/yyyy")
                        LRDATE.Text = DR("LRDATE")
                        TXTPONO.Text = Convert.ToString(DR("PONO"))
                        'If DR("PODATE") <> "" Then PODATE.Text = Format(Convert.ToDateTime(DR("PODATE")).Date, "dd/MM/yyyy")
                        PODATE.Text = DR("PODATE")
                        TXTQUOTENO.Text = Convert.ToString(DR("QUOTENO"))

                        TXTTOTAL.Text = Val(DR("TOTALAMT"))
                        CMBTAX.Text = DR("TAXNAME")
                        TXTTAX.Text = Val(DR("TAXAMT"))
                        TXTROUNDOFF.Text = Val(DR("ROUNDOFF"))

                        TXTGTOTAL.Text = Convert.ToString(DR("GTOTAL"))
                        CMBOTHERCHGS.Text = Convert.ToString(DR("OTHERCHGSNAME"))

                        If DR("OTHERCHGS") > 0 Then
                            txtotherchg.Text = DR("OTHERCHGS")
                            cmbaddsub.Text = "Add"
                        Else
                            txtotherchg.Text = DR("OTHERCHGS") * (-1)
                            cmbaddsub.Text = "Sub."
                        End If

                        CMBTAX.Text = DR("TAXNAME")
                        TXTTAX.Text = DR("TAXAMT")
                        CMBEXTRACHGS.Text = Convert.ToString(DR("EXTRACHGS"))
                        If DR("EXTRACHGSAMT") > 0 Then
                            TXTEXTRACHGS.Text = DR("EXTRACHGSAMT")
                            CMBEXTRAADDSUB.Text = "Add"
                        Else
                            TXTEXTRACHGS.Text = DR("EXTRACHGSAMT") * (-1)
                            CMBEXTRAADDSUB.Text = "Sub."
                        End If


                        If Convert.ToBoolean(DR("DONE")) = True Then
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        '' grid 
                        GRIDCHALLAN.Rows.Add(DR("SRNO").ToString, DR("PARTNO").ToString, DR("ITEMNAME").ToString, DR("MAKE").ToString, Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMT")), "0.00"))
                        TabControl1.SelectedIndex = (0)

                    Next

                    '' GRID UPLOAD
                    Dim OBJCMNN As New ClsCommon
                    Dim DTTABLE As DataTable = OBJCMNN.search("ISNULL(GDN_UPSRNO, 0) AS SRNO, ISNULL(GDN_UPREMARKS, '') AS REMARKS, ISNULL(GDN_UPNAME, '') AS NAME, GDN_IMGPATH AS IMGPATH", "", "GDN_UPLOAD", "AND GDN_UPLOAD.GDN_NO= " & TEMPCHALLANNO & "  AND GDN_YEARID = " & YearId & " ORDER BY GDN_UPLOAD.GDN_UPSRNO")
                    If DTTABLE.Rows.Count > 0 Then
                        For Each DTR As DataRow In DTTABLE.Rows
                            gridupload.Rows.Add(DTR("SRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1
                End If
                CMBNAME.Focus()
                CMDSELECTQUOTE.Enabled = False
                total()
                getsrno(GRIDCHALLAN)
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
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(TXTDELTHROUGH.Text.Trim)
            'If DELIVERYDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(DELIVERYDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            'If VALIDTILL.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(VALIDTILL.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(DELIVERYDATE.Text.Trim)
            ALPARAVAL.Add(VALIDTILL.Text.Trim)

            ALPARAVAL.Add(TXTDESPATCHEDTO.Text.Trim)
            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            'If LRDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(LRDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(LRDATE.Text.Trim)
            ALPARAVAL.Add(TXTPONO.Text.Trim)
            'If PODATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(PODATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(PODATE.Text.Trim)
            ALPARAVAL.Add(TXTQUOTENO.Text.Trim)
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))
            ALPARAVAL.Add(Val(TXTTOTAL.Text.Trim))
            ALPARAVAL.Add(CMBOTHERCHGS.Text.Trim)
            If cmbaddsub.Text.Trim = "Add" Then
                ALPARAVAL.Add(Val(txtotherchg.Text.Trim))
            ElseIf cmbaddsub.Text.Trim = "Sub." Then
                ALPARAVAL.Add(Val((txtotherchg.Text.Trim) * (-1)))
            Else
                ALPARAVAL.Add(0)
            End If
            ALPARAVAL.Add(CMBTAX.Text.Trim)
            ALPARAVAL.Add(Val(TXTTAX.Text.Trim))
            ALPARAVAL.Add(CMBEXTRACHGS.Text.Trim)
            If CMBEXTRAADDSUB.Text.Trim = "Add" Then
                ALPARAVAL.Add(Val(TXTEXTRACHGS.Text.Trim))
            ElseIf CMBEXTRAADDSUB.Text.Trim = "Sub." Then
                ALPARAVAL.Add(Val((TXTEXTRACHGS.Text.Trim) * (-1)))
            Else
                ALPARAVAL.Add(0)
            End If
            ALPARAVAL.Add(Val(TXTROUNDOFF.Text.Trim))
            ALPARAVAL.Add(Val(TXTGTOTAL.Text.Trim))
            If chkdone.CheckState = True Then
                ALPARAVAL.Add(1)
            Else
                ALPARAVAL.Add(0)
            End If

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            '' GRID PARAMETERS
            Dim SRNO As String = ""
            Dim PARTNO As String = ""
            Dim ITEM As String = ""
            Dim MAKE As String = ""
            Dim QTY As String = ""
            Dim UNIT As String = ""
            Dim RATE As String = ""
            Dim AMT As String = ""
            ' Dim GRNNO As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDCHALLAN.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        PARTNO = ROW.Cells(GPARTNO.Index).Value.ToString
                        ITEM = ROW.Cells(GITEMNAME.Index).Value.ToString
                        MAKE = ROW.Cells(GMAKE.Index).Value.ToString
                        QTY = Val(ROW.Cells(GQTY.Index).Value)
                        UNIT = ROW.Cells(GUNIT.Index).Value.ToString
                        RATE = Val(ROW.Cells(GRATE.Index).Value)
                        AMT = Val(ROW.Cells(GAMT.Index).Value)

                    Else
                        SRNO = SRNO & "|" & ROW.Cells(gsrno.Index).Value
                        PARTNO = PARTNO & "|" & ROW.Cells(GPARTNO.Index).Value.ToString
                        ITEM = ITEM & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        MAKE = MAKE & "|" & ROW.Cells(GMAKE.Index).Value.ToString
                        QTY = QTY & "|" & Val(ROW.Cells(GQTY.Index).Value)
                        UNIT = UNIT & "|" & ROW.Cells(GUNIT.Index).Value.ToString
                        RATE = RATE & "|" & Val(ROW.Cells(GRATE.Index).Value)
                        AMT = AMT & "|" & Val(ROW.Cells(GAMT.Index).Value)
                        'GRNNO = GRNNO & "|" & Val(ROW.Cells(GGRNNO.Index).Value)
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(PARTNO)
            ALPARAVAL.Add(ITEM)
            ALPARAVAL.Add(MAKE)
            ALPARAVAL.Add(QTY)
            ALPARAVAL.Add(UNIT)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(AMT)

            Dim OBJPUR As New ClsChallan
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.save()
                MsgBox("Details Added !!")
                TEMPCHALLANNO = DTTABLE.Rows(0).Item(0)

            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPCHALLANNO)
                IntResult = OBJPUR.update()
                MsgBox("Details Updated")
                EDIT = False

            End If
            If gridupload.RowCount > 0 Then SAVEUPLOAD()
            CLEAR()
            CMBNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Sub SAVEUPLOAD()
        Try
            Dim OBJORDER As New ClsChallan

            For Each ROW As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If ROW.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPCHALLANNO)
                    ALPARAVAL.Add(ROW.Cells(GUSRNO.Index).Value)
                    ALPARAVAL.Add(ROW.Cells(GUREMARKS.Index).Value)
                    ALPARAVAL.Add(ROW.Cells(GUNAME.Index).Value)

                    PBSoftCopy.Image = ROW.Cells(GUIMGPATH.Index).Value
                    PBSoftCopy.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    ALPARAVAL.Add(MS.ToArray)
                    ALPARAVAL.Add(YearId)

                    OBJORDER.alParaval = ALPARAVAL
                    Dim INTRESULT As Integer = OBJORDER.SAVEUPLOAD()
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillupload()

        If gridUPLOADDoubleClick = False Then
            gridupload.Rows.Add(Val(txtuploadsrno.Text.Trim), txtuploadremarks.Text.Trim, txtuploadname.Text.Trim, PBSoftCopy.Image)
            getsrno(gridupload)
        ElseIf gridUPLOADDoubleClick = True Then

            gridupload.Item(GUSRNO.Index, TEMPUPLOADROW).Value = Val(txtuploadsrno.Text.Trim)
            gridupload.Item(GUREMARKS.Index, TEMPUPLOADROW).Value = txtuploadremarks.Text.Trim
            gridupload.Item(GUNAME.Index, TEMPUPLOADROW).Value = txtuploadname.Text.Trim
            gridupload.Item(GUIMGPATH.Index, TEMPUPLOADROW).Value = PBSoftCopy.Image

            gridUPLOADDoubleClick = False
        End If
        gridupload.FirstDisplayedScrollingRowIndex = gridupload.RowCount - 1

        txtuploadsrno.Text = gridupload.RowCount + 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()

        txtuploadremarks.Focus()
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
        TXTTOTAL.Text = 0
        'txtotherchgS.Text = 0
        TXTTAX.Text = "0.00"
        TXTGTOTAL.Text = 0
        TXTSUBTOTAL.Text = 0

        For Each row As DataGridViewRow In GRIDCHALLAN.Rows
            If Val(row.Cells(GAMT.Index).Value) <> 0 Then TXTTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(row.Cells(GAMT.Index).Value), "0.000")
            If Val(row.Cells(GQTY.Index).Value) <> 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0.00")
        Next

        If cmbaddsub.Text = "Add" Then
            TXTSUBTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(txtotherchg.Text), "0")
            TXTROUNDOFF.Text = Format(Val(TXTSUBTOTAL.Text) - (Val(TXTTOTAL.Text) + Val(txtotherchg.Text)), "0.00")
        Else
            TXTSUBTOTAL.Text = Format((Val(TXTTOTAL.Text)) - Val(txtotherchg.Text), "0")
            TXTROUNDOFF.Text = Format(Val(TXTSUBTOTAL.Text) - (Val(TXTTOTAL.Text) - Val(txtotherchg.Text)), "0.00")
        End If

        Dim dt As New DataTable
        Dim objcmn As New ClsCommon
        dt = objcmn.search("tax_name, tax_tax as tax", "", " taxmaster", "and TAXMASTER.tax_name = '" & CMBTAX.Text.Trim & "' and tax_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            TXTTAX.Text = Format((Val(dt.Rows(0).Item(1)) * Val(TXTSUBTOTAL.Text)) / 100, "0.00")
        End If

        If CMBEXTRAADDSUB.Text = "Add" Then
            TXTGTOTAL.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) + Val(TXTEXTRACHGS.Text), "0")
            TXTROUNDOFF.Text = Format(Val(TXTGTOTAL.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) + Val(TXTEXTRACHGS.Text)), "0.00")
        Else
            TXTGTOTAL.Text = Format((Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text)) - Val(TXTEXTRACHGS.Text), "0")
            TXTROUNDOFF.Text = Format(Val(TXTGTOTAL.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) - Val(TXTEXTRACHGS.Text)), "0.00")
        End If

        'TXTGTOTAL.Text = Format((Val(TXTTOTAL.Text) + (Val(TXTTAX.Text))), "0")
        'TXTROUNDOFF.Text = Format(Val(TXTGTOTAL.Text) - (Val(TXTTOTAL.Text) + Val(TXTTAX.Text)), "0.00")

        TXTGTOTAL.Text = Format(Val(TXTGTOTAL.Text), "0.00")

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
        If GRIDDOUBLECLICK = False Then
            If GRIDCHALLAN.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDCHALLAN.Rows(GRIDCHALLAN.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
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
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDCHALLAN.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDCHALLAN.Rows.Add(Val(TXTSRNO.Text.Trim), TXTPARTNO.Text.Trim, CMBITEMNAME.Text.Trim, TXTMAKE.Text.Trim, Format(Val(TXTQTY.Text.Trim), "0"), CMBUNIT.Text.Trim, Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"))
            getsrno(GRIDCHALLAN)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDCHALLAN.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDCHALLAN.Item(GPARTNO.Index, TEMPROW).Value = TXTPARTNO.Text.Trim
            GRIDCHALLAN.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text.Trim
            GRIDCHALLAN.Item(GMAKE.Index, TEMPROW).Value = TXTMAKE.Text.Trim
            GRIDCHALLAN.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")
            GRIDCHALLAN.Item(GUNIT.Index, TEMPROW).Value = CMBUNIT.Text.Trim
            GRIDCHALLAN.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text), "0.00")
            GRIDCHALLAN.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text), "0.00")
            GRIDDOUBLECLICK = False
        End If
        TXTAMOUNT.ReadOnly = True
        total()
        GRIDCHALLAN.FirstDisplayedScrollingRowIndex = GRIDCHALLAN.RowCount - 1

        CMBITEMNAME.Text = ""
        TXTPARTNO.Clear()
        TXTMAKE.Clear()
        TXTQTY.Clear()
        CMBUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()
        TXTSRNO.Text = Val(GRIDCHALLAN.RowCount)
        TXTPARTNO.Focus()

    End Sub

    Sub editrow()
        Try
            If GRIDCHALLAN.CurrentRow.Index >= 0 And GRIDCHALLAN.Item(gsrno.Index, GRIDCHALLAN.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDCHALLAN.Item(gsrno.Index, GRIDCHALLAN.CurrentRow.Index).Value
                TXTPARTNO.Text = GRIDCHALLAN.Item(GPARTNO.Index, GRIDCHALLAN.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDCHALLAN.Item(GITEMNAME.Index, GRIDCHALLAN.CurrentRow.Index).Value.ToString
                TXTMAKE.Text = GRIDCHALLAN.Item(GMAKE.Index, GRIDCHALLAN.CurrentRow.Index).Value.ToString
                TXTQTY.Text = Val(GRIDCHALLAN.Item(GQTY.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                CMBUNIT.Text = GRIDCHALLAN.Item(GUNIT.Index, GRIDCHALLAN.CurrentRow.Index).Value
                TXTRATE.Text = Val(GRIDCHALLAN.Item(GRATE.Index, GRIDCHALLAN.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDCHALLAN.Item(GAMT.Index, GRIDCHALLAN.CurrentRow.Index).Value)

                TEMPROW = GRIDCHALLAN.CurrentRow.Index
                TXTPARTNO.Focus()
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
        If TXTPARTNO.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And CMBUNIT.Text.Trim <> "" And Val(TXTRATE.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 Then
            fillgrid()
            total()
        Else
            If TXTPARTNO.Text.Trim = "" Then
                MsgBox("Please Fill Part No ")
                Exit Sub
            End If

            If CMBITEMNAME.Text.Trim = "" Then
                MsgBox("Please Fill Item Name ")
                Exit Sub
            End If

            'If TXTMAKE.Text.Trim = "" Then
            '    MsgBox("Please Fill Make")
            '    Exit Sub
            'End If

            If Val(TXTQTY.Text.Trim) <= 0 Then
                MsgBox("Please Fill Qty ")
                Exit Sub
            End If

            If CMBUNIT.Text.Trim = "" Then
                MsgBox("Please Fill Unit")
                Exit Sub
            End If

            If Val(TXTRATE.Text.Trim) <= 0 Then
                MsgBox("Please Fill Rate ")
                Exit Sub
            End If
        End If
    End Sub

    Sub calc()
        Try
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
                Dim objpur As New ClsChallan
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPCHALLANNO)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.Delete
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
            Dim objpurinv As New ChallanDetails
            objpurinv.MdiParent = MDIMain
            objpurinv.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTAX_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTAX.Enter
        Try
            If CMBTAX.Text.Trim = "" Then filltax(CMBTAX, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTAX_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTAX.Validated
        total()
    End Sub

    Private Sub CMBTAX_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTAX.Validating
        Try
            If CMBTAX.Text.Trim <> "" Then TAXvalidate(CMBTAX, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDQUOTE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHALLAN.KeyDown
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

    Private Sub txtuploadname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
        Try
            If txtuploadremarks.Text.Trim <> "" And txtuploadname.Text.Trim <> "" And PBSoftCopy.ImageLocation <> "" Then
                fillupload()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdupload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdupload.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
        Try
            If gridupload.SelectedRows.Count > 0 Then
                Dim objVIEW As New ViewImage
                objVIEW.pbsoftcopy.Image = PBSoftCopy.Image
                objVIEW.ShowDialog()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDREMOVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSoftCopy.Image = Nothing
            txtimgpath.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtuploadsrno_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If gridUPLOADDoubleClick = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub gridupload_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                gridUPLOADDoubleClick = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                TEMPUPLOADROW = e.RowIndex
                txtuploadremarks.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
        Try
            If e.KeyCode = Keys.Delete And gridupload.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If gridUPLOADDoubleClick = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                gridupload.Rows.RemoveAt(gridupload.CurrentRow.Index)
                getsrno(gridupload)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
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

    Private Sub CMBITEMNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
        If CMBITEMNAME.Text.Trim = "" Then cmbaddsub.Focus()
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If TXTPARTNO.Text.Trim = "" And CMBITEMNAME.Text.Trim = "" Then txtremarks.Focus()
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectAll()
    End Sub

    Private Sub INVDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
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

    Private Sub CHALLANDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
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

    Private Sub LRDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles LRDATE.GotFocus
        LRDATE.SelectAll()
    End Sub

    Private Sub LRDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LRDATE.Validating
        Try
            If LRDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(LRDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSELECTQUOTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTQUOTE.Click
        Try
            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            Dim OBJSELECTQUOTE As New SelectQuotation
            Dim DTQUOTE As DataTable = OBJSELECTQUOTE.DT
            OBJSELECTQUOTE.ShowDialog()

            Dim i As Integer = 0
            If DTQUOTE.Rows.Count > 0 Then
                Dim objcmn As New ClsCommon
                For Each DTROW As DataRow In DTQUOTE.Rows
                    Dim dt As DataTable = objcmn.search(" ISNULL(SALEQUOTATION.QUOTE_NO, 0) AS QUOTENO, ISNULL(SALEQUOTATION_DESC.QUOTE_SRNO, 0) AS SRNO, ISNULL(SALEQUOTATION_DESC.QUOTE_PARTNO, '') AS PARTNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME, ISNULL(SALEQUOTATION_DESC.QUOTE_MAKE, '') AS MAKE, ISNULL(SALEQUOTATION_DESC.QUOTE_QTY, 0) AS QTY, ISNULL(UNIT_ABBR,'') AS UNIT, ISNULL(SALEQUOTATION_DESC.QUOTE_RATE, 0) AS RATE, ISNULL(SALEQUOTATION_DESC.QUOTE_AMOUNT, 0) AS AMT", "", " SALEQUOTATION INNER JOIN  SALEQUOTATION_DESC ON SALEQUOTATION.QUOTE_NO = SALEQUOTATION_DESC.QUOTE_NO AND SALEQUOTATION.QUOTE_YEARID = SALEQUOTATION_DESC.QUOTE_YEARID INNER JOIN ITEMMASTER ON SALEQUOTATION_DESC.QUOTE_ITEMID = ITEMMASTER.ITEM_id INNER JOIN UNITMASTER ON UNIT_ID = QUOTE_UNITID", " AND SALEQUOTATION.QUOTE_NO = " & Val(DTROW(0)) & " AND SALEQUOTATION.QUOTE_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        TXTQUOTENO.Text = dt.Rows(0).Item("QUOTENO")
                        ''GRID VALUE
                        For Each DR As DataRow In dt.Rows
                            GRIDCHALLAN.Rows.Add(0, DR("PARTNO").ToString, DR("ITEMNAME").ToString, DR("MAKE").ToString, Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00").ToString)
                        Next

                        total()
                        getsrno(GRIDCHALLAN)
                        CMDSELECTQUOTE.Enabled = False
                    End If
                Next
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub TXTQTY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress
        numkeypress(e, TXTQTY, Me)
    End Sub

    Private Sub TXTRATE_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub TXTRATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTRATE.Validating
        calc()
    End Sub

    Private Sub TXTQTY_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating
        calc()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        Call cmdok_Click(sender, e)
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tooldelete.Click
        Call cmddelete_Click(sender, e)
    End Sub

    Private Sub TXTTAX_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTTAX.Validated
        total()
    End Sub

    Private Sub CMBOTHERCHGS_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBOTHERCHGS.Enter
        Try
            If CMBOTHERCHGS.Text.Trim = "" Then fillname(CMBOTHERCHGS, EDIT, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBOTHERCHGS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBOTHERCHGS.Validating
        Try
            If CMBOTHERCHGS.Text.Trim <> "" Then namevalidate(CMBOTHERCHGS, CMBOTHERCHGSCODE, e, Me, TXTOTHERCHGSADD, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEXTRACHGS_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBEXTRACHGS.Enter
        Try
            If CMBEXTRACHGS.Text.Trim = "" Then fillname(CMBEXTRACHGS, EDIT, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBEXTRACHGS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBEXTRACHGS.Validating
        Try
            If CMBEXTRACHGS.Text.Trim <> "" Then namevalidate(CMBEXTRACHGS, CMBOTHERCHGSCODE, e, Me, TXTOTHERCHGSADD, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtotherchg_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtotherchg.KeyPress
        numdotkeypress(e, txtotherchg, Me)
    End Sub

    Private Sub txtotherchg_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtotherchg.Validated
        total()
    End Sub

    Private Sub TXTEXTRACHGS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEXTRACHGS.KeyPress
        numdotkeypress(e, TXTEXTRACHGS, Me)
    End Sub

    Private Sub TXTEXTRACHGS_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTEXTRACHGS.Validated
        total()
    End Sub

    Private Sub PODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PODATE.GotFocus
        PODATE.SelectAll()
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPCHALLANNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal CHALLANNO As Integer)
        Try
            If MsgBox("Wish to Print Challan?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJGDN As New SaleInvoiceDesign
                OBJGDN.MdiParent = MDIMain
                OBJGDN.FRMSTRING = "CHALLAN"
                OBJGDN.WHERECLAUSE = "{GDN.GDN_no}=" & Val(CHALLANNO) & " and {GDN.GDN_yearid}=" & YearId
                OBJGDN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PODATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PODATE.Validating
        Try
            If PODATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(PODATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DELIVERYDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DELIVERYDATE.GotFocus
        DELIVERYDATE.SelectAll()
    End Sub

    Private Sub DELIVERYDATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DELIVERYDATE.Validating
        Try
            If DELIVERYDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(DELIVERYDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBUNIT.Enter
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
End Class