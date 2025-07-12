
Imports BL


Public Class SaleReturn

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, gridUPLOADDoubleClick As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPSALRETNO As Integer
    Public TEMPREGNAME, tempmsg As String
    Dim saleregabbr, salereginitial As String
    Dim saleregid As Integer

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Sub CLEAR()

        tstxtbillno.Clear()
        CMBNAME.Text = ""
        TXTREFNO.Clear()
        TXTDELTHROUGH.Clear()
        VALIDTILL.Text = DateAdd(DateInterval.Month, 1, Mydate)
        SALRETDATE.Text = Mydate
        TXTINVNO.Clear()
        TXTSALRETNO.Clear()
        TXTINVTYPE.Clear()
        GRIDSALRET.RowCount = 0
        TXTSRNO.Text = 1
        TXTPARTNO.Clear()
        TXTINVREGNAME.Clear()
        INVDATE.Value = Mydate
        CMBITEMNAME.Text = ""
        TXTMAKE.Clear()
        TXTQTY.Clear()
        CMBUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()
        TXTQUOTENO.Clear()
        CMBOTHERCHGS.Text = ""
        CMBEXTRACHGS.Text = ""
        TXTDESPATCHEDTO.Clear()
        DELIVERYDATE.Clear()
        TXTLRNO.Clear()
        LRDATE.Clear()
        TXTEWAYBILLNO.Clear()
        PODATE.Clear()
        TXTPONO.Clear()
        TXTCHALLANNO.Clear()
        If ClientName = "SHENSHE" Then CHALLANDATE.Text = Mydate Else CHALLANDATE.Clear()
        CHALLANDATE.Enabled = True

        CMDSELECTCHALLAN.Enabled = True

        LBLTOTALQTY.Text = 0
        LBLTOTALAMT.Text = 0.0
        TXTHSNCODE.Clear()
        LBLTOTALCGSTAMT.Text = 0
        LBLTOTALSGSTAMT.Text = 0
        LBLTOTALIGSTAMT.Text = 0
        TXTOTHERAMT.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTSTATECODE.Clear()
        TXTGSTIN.Clear()
        LBLTOTALOTHERAMT.Text = 0
        LBLTOTALTAXABLEAMT.Text = 0
        TXTSALRETNO.Clear()

        TXTINVTYPE.Clear()
        TXTTOTAL.Text = 0
        CMBTAX.Text = ""
        TXTTAX.Text = 0
        TXTROUNDOFF.Text = 0
        TXTGTOTAL.Text = 0
        TXTSUBTOTAL.Text = 0
        txtotherchg.Text = 0.0
        TXTEXTRACHGS.Text = 0.0
        txtremarks.Clear()
        TXTBAL.Clear()
        cmbaddsub.SelectedIndex = 0
        CMBEXTRAADDSUB.SelectedIndex = 0

        GETMAX_SALRET_NO()
        EP.Clear()
        lbllocked.Visible = False
        PBlock.Visible = False
        PBRECD.Visible = False
        cmdshowdetails.Visible = False

        GRIDDOUBLECLICK = False
        gridUPLOADDoubleClick = False
        TXTEACHBOXES.Clear()
        TXTBOXES.Clear()

        txtuploadsrno.Text = 1
        txtuploadremarks.Clear()
        txtuploadname.Clear()
        PBSoftCopy.Image = Nothing
        txtimgpath.Clear()
        gridupload.RowCount = 0

    End Sub



    Sub GETMAX_SALRET_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(SALRET_no),0) + 1 ", " SALERETURN INNER JOIN REGISTERMASTER ON REGISTER_ID = SALRET_REGISTERID", " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and SALRET_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTSALRETNO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBREGISTER.Enabled = True
        CMBREGISTER.Focus()
    End Sub

    Private Sub InvoiceMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                If errorvalid() = True Then
                    Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                    If tempmsg = vbYes Then cmdok_Click(sender, e)
                End If
                Me.Close()
            ElseIf e.KeyCode = Keys.F8 Then
                GRIDSALRET.Focus()
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
        fillregister(CMBREGISTER, " and register_type = 'SALE RETURN'")
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        filltax(CMBTAX, EDIT)
        FILLITEMNAME(CMBITEMNAME, EDIT)
        fillunit(CMBUNIT)
        fillname(CMBOTHERCHGS, EDIT, "")
        fillname(CMBEXTRACHGS, EDIT, "")
    End Sub

    Private Sub InvoiceMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                Dim OBJINV As New ClsSaleReturn

                ALPARAVAL.Add(TEMPSALRETNO)
                ALPARAVAL.Add(TEMPREGNAME)
                ALPARAVAL.Add(YearId)

                OBJINV.alParaval = ALPARAVAL
                DT = OBJINV.selectNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTSALRETNO.Text = TEMPSALRETNO
                        TXTSTATECODE.Text = DR("STATECODE")
                        TXTGSTIN.Text = DR("GSTIN")
                        CMBREGISTER.Text = Convert.ToString(DR("REGNAME"))
                        SALRETDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        TXTINVNO.Text = DR("INVNO")
                        TXTINVREGNAME.Text = Convert.ToString(DR("INVREGNAME").ToString)
                        INVDATE.Text = Format(Convert.ToDateTime(DR("INVDATE")).Date, "dd/MM/yyyy")
                        TXTINVTYPE.Text = Convert.ToString(DR("INVTYPE").ToString)
                        TXTREFNO.Text = Convert.ToString(DR("REFNO"))
                        TXTDELTHROUGH.Text = Convert.ToString(DR("DELIVERYTHROUGH"))
                        VALIDTILL.Text = DR("VALIDTILL")
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))
                        TXTEWAYBILLNO.Text = DR("EWAYBILLNO")
                        TXTDESPATCHEDTO.Text = Convert.ToString(DR("DESPATCHEDTO"))
                        CMBPACKING.Text = Convert.ToString(DR("PACKING"))
                        DELIVERYDATE.Text = DR("DELIVERYDATE")
                        TXTLRNO.Text = Convert.ToString(DR("LRNO"))
                        LRDATE.Text = DR("LRDATE")
                        TXTEWAYBILLNO.Text = DR("EWAYBILLNO")
                        TXTPONO.Text = Convert.ToString(DR("PONO"))
                        PODATE.Text = DR("PODATE")
                        TXTCHALLANNO.Text = Convert.ToString(DR("CHALLANNO"))
                        CHALLANDATE.Text = DR("CHALLANDATE")
                        TXTQUOTENO.Text = Convert.ToString(DR("QUOTENO"))

                        TXTTOTAL.Text = Val(DR("TOTALAMT"))
                        CMBOTHERCHGS.Text = DR("OTHERCHGSNAME")
                        If DR("OTHERCHGS") > 0 Then
                            txtotherchg.Text = DR("OTHERCHGS")
                            cmbaddsub.Text = "Add"
                        Else
                            txtotherchg.Text = DR("OTHERCHGS") * (-1)
                            cmbaddsub.Text = "Sub."
                        End If

                        CMBTAX.Text = DR("TAXNAME")
                        TXTTAX.Text = Val(DR("TAXAMT"))

                        CMBEXTRACHGS.Text = DR("EXTRACHGSNAME")
                        If DR("EXTRACHGS") > 0 Then
                            TXTEXTRACHGS.Text = DR("EXTRACHGS")
                            CMBEXTRAADDSUB.Text = "Add"
                        Else
                            TXTEXTRACHGS.Text = DR("EXTRACHGS") * (-1)
                            CMBEXTRAADDSUB.Text = "Sub."
                        End If
                        TXTROUNDOFF.Text = Val(DR("ROUNDOFF"))

                        TXTGTOTAL.Text = Convert.ToString(DR("GTOTAL"))


                        TXTAMTREC.Text = DR("AMTREC")
                        TXTEXTRAAMT.Text = DR("EXTRAAMT")
                        TXTRETURN.Text = DR("RETURN")
                        TXTBAL.Text = DR("BALANCE")

                        If Val(DR("AMTREC")) > 0 Or Val(DR("EXTRAAMT")) > 0 Then
                            cmdshowdetails.Visible = True
                            PBRECD.Visible = True
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        If Val(DR("RETURN")) > 0 Then
                            cmdshowdetails.Visible = True
                            lbllocked.Visible = True
                            PBlock.Visible = True
                        End If

                        '' grid 
                        GRIDSALRET.Rows.Add(DR("SRNO").ToString, DR("PARTNO").ToString, DR("ITEMNAME").ToString, Convert.ToString(DR("HSNCODE")), DR("MAKE").ToString, Format(Val(DR("BOXES")), "0"), Format(Val(DR("EACHBOXES")), "0"), Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMT")), "0.00"), Format(Val(DR("OTHERAMT")), "0.00"), Format(Val(DR("TAXABLEAMT")), "0.00"), Format(Val(DR("CGSTPER")), "0.00"), Format(Val(DR("CGSTAMT")), "0.00"), Format(Val(DR("SGSTPER")), "0.00"), Format(Val(DR("SGSTAMT")), "0.00"), Format(Val(DR("IGSTPER")), "0.00"), Format(Val(DR("IGSTAMT")), "0.00"), Format(Val(DR("GRIDTOTAL")), "0.00"))
                        TabControl1.SelectedIndex = (0)

                    Next

                    Dim clscommon As New ClsCommon
                    Dim dtID As DataTable
                    dtID = clscommon.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                    If dtID.Rows.Count > 0 Then
                        saleregabbr = dtID.Rows(0).Item(0).ToString
                        salereginitial = dtID.Rows(0).Item(1).ToString
                        saleregid = dtID.Rows(0).Item(2)
                    End If

                    '' GRID UPLOAD
                    Dim OBJCMNN As New ClsCommon
                    Dim DTTABLE As DataTable = OBJCMNN.search("ISNULL(SALRET_UPSRNO, 0) AS SRNO, ISNULL(SALRET_UPREMARKS, '') AS REMARKS, ISNULL(SALRET_UPNAME, '') AS NAME, SALRET_IMGPATH AS IMGPATH", "", "SALERETURN_UPLOAD", "AND SALERETURN_UPLOAD.SALRET_NO= " & TEMPSALRETNO & "  AND SALRET_YEARID = " & YearId & " ORDER BY SALERETURN_UPLOAD.SALRET_UPSRNO")
                    If DTTABLE.Rows.Count > 0 Then
                        For Each DTR As DataRow In DTTABLE.Rows
                            gridupload.Rows.Add(DTR("SRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    GRIDSALRET.FirstDisplayedScrollingRowIndex = GRIDSALRET.RowCount - 1
                End If
                CMBREGISTER.Enabled = False
                CMDSELECTCHALLAN.Enabled = False
                SALRETDATE.Focus()
                total()
                getsrno(GRIDSALRET)
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
            ALPARAVAL.Add(Format(Convert.ToDateTime(SALRETDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBREGISTER.Text.Trim)
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(TXTINVNO.Text.Trim)
            ALPARAVAL.Add(TXTINVREGNAME.Text.Trim)
            ALPARAVAL.Add(INVDATE.Value.Date)
            ALPARAVAL.Add(TXTINVTYPE.Text.Trim)
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(TXTDELTHROUGH.Text.Trim)
            'If DELIVERYDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(DELIVERYDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            'If VALIDTILL.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(VALIDTILL.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(DELIVERYDATE.Text.Trim)
            ALPARAVAL.Add(VALIDTILL.Text.Trim)


            ALPARAVAL.Add(TXTDESPATCHEDTO.Text.Trim)
            ALPARAVAL.Add(CMBPACKING.Text.Trim)
            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            'If LRDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(LRDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(LRDATE.Text.Trim)
            ALPARAVAL.Add(TXTEWAYBILLNO.Text.Trim)
            ALPARAVAL.Add(TXTPONO.Text.Trim)
            'If PODATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(PODATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(PODATE.Text.Trim)
            ALPARAVAL.Add(TXTCHALLANNO.Text.Trim)
            'If CHALLANDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(CHALLANDATE.Text.Trim)
            ALPARAVAL.Add(TXTQUOTENO.Text.Trim)
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))

            ALPARAVAL.Add(Val(LBLTOTALAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALOTHERAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALTAXABLEAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALCGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALSGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALIGSTAMT.Text.Trim))

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

            ALPARAVAL.Add(Val(TXTAMTREC.Text.Trim))
            ALPARAVAL.Add(Val(TXTEXTRAAMT.Text.Trim))
            ALPARAVAL.Add(Val(TXTRETURN.Text.Trim))
            ALPARAVAL.Add(Val(TXTBAL.Text.Trim))

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)



            '' GRID PARAMETERS
            Dim SRNO As String = ""
            Dim PARTNO As String = ""
            Dim ITEM As String = ""
            Dim HSNCODE As String = ""

            Dim MAKE As String = ""
            Dim BOXES As String = ""
            Dim EACHBOXES As String = ""

            Dim QTY As String = ""
            Dim UNIT As String = ""
            Dim RATE As String = ""
            Dim AMT As String = ""
            Dim OTHERAMT As String = ""
            Dim TAXABLEAMT As String = ""
            Dim CGSTPER As String = ""
            Dim CGSTAMT As String = ""
            Dim SGSTPER As String = ""
            Dim SGSTAMT As String = ""
            Dim IGSTPER As String = ""
            Dim IGSTAMT As String = ""
            Dim GRIDTOTAL As String = ""

            ' Dim GRNNO As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDSALRET.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(gsrno.Index).Value
                        PARTNO = ROW.Cells(GPARTNO.Index).Value.ToString
                        ITEM = ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = ROW.Cells(GHSNCODE.Index).Value.ToString
                        MAKE = ROW.Cells(GMAKE.Index).Value.ToString
                        BOXES = Val(ROW.Cells(GBOXES.Index).Value)
                        EACHBOXES = Val(ROW.Cells(GEACHBOXES.Index).Value)

                        QTY = Val(ROW.Cells(GQTY.Index).Value)

                        UNIT = ROW.Cells(GUNIT.Index).Value.ToString
                        RATE = Val(ROW.Cells(GRATE.Index).Value)
                        AMT = Val(ROW.Cells(GAMT.Index).Value)
                        OTHERAMT = Val(ROW.Cells(GOTHERAMT.Index).Value)
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
                        PARTNO = PARTNO & "|" & ROW.Cells(GPARTNO.Index).Value.ToString
                        ITEM = ITEM & "|" & ROW.Cells(GITEMNAME.Index).Value.ToString
                        HSNCODE = HSNCODE & "|" & ROW.Cells(GHSNCODE.Index).Value.ToString
                        MAKE = MAKE & "|" & ROW.Cells(GMAKE.Index).Value.ToString
                        BOXES = BOXES & "|" & Val(ROW.Cells(GBOXES.Index).Value)
                        EACHBOXES = EACHBOXES & "|" & Val(ROW.Cells(GEACHBOXES.Index).Value)
                        QTY = QTY & "|" & Val(ROW.Cells(GQTY.Index).Value)
                        UNIT = UNIT & "|" & ROW.Cells(GUNIT.Index).Value.ToString
                        RATE = RATE & "|" & Val(ROW.Cells(GRATE.Index).Value)
                        AMT = AMT & "|" & Val(ROW.Cells(GAMT.Index).Value)
                        OTHERAMT = OTHERAMT & "|" & Val(ROW.Cells(GOTHERAMT.Index).Value)
                        TAXABLEAMT = TAXABLEAMT & "|" & Val(ROW.Cells(GTAXABLEAMT.Index).Value)
                        CGSTPER = CGSTPER & "|" & ROW.Cells(GCGSTPER.Index).Value
                        CGSTAMT = CGSTAMT & "|" & Val(ROW.Cells(GCGSTAMT.Index).Value)
                        SGSTPER = SGSTPER & "|" & ROW.Cells(GSGSTPER.Index).Value
                        SGSTAMT = SGSTAMT & "|" & Val(ROW.Cells(GSGSTAMT.Index).Value)
                        IGSTPER = IGSTPER & "|" & ROW.Cells(GIGSTPER.Index).Value
                        IGSTAMT = IGSTAMT & "|" & Val(ROW.Cells(GIGSTAMT.Index).Value)
                        GRIDTOTAL = GRIDTOTAL & "|" & Val(ROW.Cells(GGRIDTOTAL.Index).Value)
                        'GRNNO = GRNNO & "|" & Val(ROW.Cells(GGRNNO.Index).Value)
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(PARTNO)
            ALPARAVAL.Add(ITEM)
            ALPARAVAL.Add(HSNCODE)
            ALPARAVAL.Add(MAKE)
            ALPARAVAL.Add(BOXES)
            ALPARAVAL.Add(EACHBOXES)
            ALPARAVAL.Add(QTY)
            ALPARAVAL.Add(UNIT)
            ALPARAVAL.Add(RATE)
            ALPARAVAL.Add(AMT)
            ALPARAVAL.Add(OTHERAMT)
            ALPARAVAL.Add(TAXABLEAMT)
            ALPARAVAL.Add(CGSTPER)
            ALPARAVAL.Add(CGSTAMT)
            ALPARAVAL.Add(SGSTPER)
            ALPARAVAL.Add(SGSTAMT)
            ALPARAVAL.Add(IGSTPER)
            ALPARAVAL.Add(IGSTAMT)
            ALPARAVAL.Add(GRIDTOTAL)



            Dim OBJPUR As New ClsSaleReturn
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.save()
                MsgBox("Details Added !!")
                TEMPSALRETNO = DTTABLE.Rows(0).Item(0)
                PRINTREPORT(DTTABLE.Rows(0).Item(0))

                'If CHKTDS.CheckState = CheckState.Checked Then
                '    Dim OBJTDS As New DeductTDS
                '    OBJTDS.BILLNO = DTTABLE.Rows(0).Item(0)
                '    OBJTDS.REGISTER = cmbregister.Text.Trim
                '    OBJTDS.ShowDialog()
                'End If
            Else

                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPSALRETNO)
                IntResult = OBJPUR.update()
                MsgBox("Details Updated")
                PRINTREPORT(TEMPSALRETNO)
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
            Dim OBJORDER As New ClsSaleReturn

            For Each ROW As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If ROW.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPSALRETNO)
                    ALPARAVAL.Add(CMBREGISTER.Text.Trim)
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

    Sub PRINTREPORT(ByVal SALRETNO As Integer)
        Try
            tempmsg = MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo)
            If tempmsg = vbYes Then
                Dim OBJINVOICE As New SaleInvoiceDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "SALERETURN INVOICE"
                OBJINVOICE.WHERECLAUSE = "{SALERETURN.SALERETURN_no}=" & Val(SALRETNO) & " AND {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "' and {SALERETURN.SALRET_yearid}=" & YearId
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

        If CMBREGISTER.Text.Trim.Length = 0 Then
            EP.SetError(CMBREGISTER, "Enter Register Name")
            bln = False
        End If

        If GRIDSALRET.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If


        If SALRETDATE.Text = "__/__/____" Then
            EP.SetError(SALRETDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(SALRETDATE.Text) Then
                EP.SetError(SALRETDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        'GET DEFAULT SALEREGISTER IF INVOICENO IS BLANK
        If Convert.ToDateTime(SALRETDATE.Text).Date >= "01/07/2017" And Val(TXTINVNO.Text.Trim) = 0 Then
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" register_name AS REGNAME ", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' AND REGISTER_YEARID = " & YearId)
            If DT.Rows.Count > 0 Then
                TXTINVREGNAME.Text = DT.Rows(0).Item("REGNAME")
            End If
        End If

        'IF INVOICENO IS NOT BLANK THEN CHECK THAT FIGURES CANNOT BE GREATER THEN BALANCEAMT
        If Val(TXTINVNO.Text.Trim) > 0 Then
            Dim BALANCE As Double = 0
            Dim DT As New DataTable
            Dim OBJCMN As New ClsCommon
            If TXTINVTYPE.Text.Trim = "INVOICE" Then
                DT = OBJCMN.search("INVOICE_BALANCE AS INVBAL", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVNO.Text.Trim) & " AND REGISTER_NAME = '" & TXTINVREGNAME.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
            Else
                DT = OBJCMN.search("BILL_BALANCE AS INVBAL", "", "OPENINGBILL INNER JOIN REGISTERMASTER ON BILL_REGISTERID = REGISTER_ID", " AND BILL_NO = " & Val(TXTINVNO.Text.Trim) & " AND REGISTER_NAME = '" & TXTINVREGNAME.Text.Trim & "' AND BILL_YEARID = " & YearId)
            End If
            BALANCE = Val(DT.Rows(0).Item("INVBAL"))
            If EDIT = True Then
                Dim DT1 As DataTable = OBJCMN.search("SALRET_GRANDTOTAL AS RETTOTAL", "", "SALERETURN", " AND SALRET_NO = " & Val(TEMPSALRETNO) & " AND SALRET_YEARID = " & YearId)
                BALANCE += Val(DT1.Rows(0).Item("RETTOTAL"))
            End If
            If Val(TXTSUBTOTAL.Text.Trim) > Val(BALANCE) Then
                EP.SetError(TXTSUBTOTAL, "Amount Greater then Balance Amt, only " & Val(BALANCE) & " can be Used")
                bln = False
            End If

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

        If SALRETDATE.Text <> "__/__/____" Then
            If Convert.ToDateTime(SALRETDATE.Text).Date >= "01/07/2017" Then
                If TXTSTATECODE.Text.Trim.Length = 0 Then
                    EP.SetError(TXTSTATECODE, "Please enter the state code")
                    bln = False
                End If

                If TXTGSTIN.Text.Trim.Length = 0 Then
                    If MsgBox("GSTIN Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        EP.SetError(TXTSTATECODE, "Enter GSTIN in Party Master")
                        bln = False
                    End If
                End If

                If CMPSTATECODE <> TXTSTATECODE.Text.Trim And (Val(LBLTOTALCGSTAMT.Text) > 0 Or Val(LBLTOTALSGSTAMT.Text.Trim) > 0) Then
                    EP.SetError(TXTSTATECODE, "Invaid Entry Done in CGST/SGST")
                    bln = False
                End If

                If CMPSTATECODE = TXTSTATECODE.Text.Trim And Val(LBLTOTALIGSTAMT.Text) > 0 Then
                    EP.SetError(TXTSTATECODE, "Invaid Entry Done in IGST")
                    bln = False
                End If
            End If

        End If

        If Convert.ToDateTime(SALRETDATE.Text).Date >= "01/02/2018" And TXTGTOTAL.Text > 50000 Then
            If TXTEWAYBILLNO.Text.Trim.Length = 0 Then
                If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
                    bln = False
                End If
            End If
        End If



        Return bln
    End Function

    Sub total()

        LBLTOTALAMT.Text = "0.0"
        LBLTOTALOTHERAMT.Text = "0.0"
        LBLTOTALTAXABLEAMT.Text = "0.0"
        LBLTOTALCGSTAMT.Text = "0.0"
        LBLTOTALSGSTAMT.Text = "0.0"
        LBLTOTALIGSTAMT.Text = "0.0"

        LBLTOTALQTY.Text = "0.00"
        TXTTOTAL.Text = 0
        TXTTAX.Text = "0.00"
        TXTGTOTAL.Text = 0
        TXTSUBTOTAL.Text = 0

        For Each row As DataGridViewRow In GRIDSALRET.Rows
            If Val(row.Cells(GQTY.Index).Value) <> 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0.00")
            If Val(row.Cells(GAMT.Index).Value) <> 0 Then LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(row.Cells(GAMT.Index).Value), "0.00")
            If Val(row.Cells(GOTHERAMT.Index).Value) <> 0 Then LBLTOTALOTHERAMT.Text = Format(Val(LBLTOTALOTHERAMT.Text) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
            row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
            row.Cells(GCGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            row.Cells(GSGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            row.Cells(GIGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")


            If Val(row.Cells(GTAXABLEAMT.Index).Value) <> 0 Then LBLTOTALTAXABLEAMT.Text = Format(Val(LBLTOTALTAXABLEAMT.Text) + Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue), "0.00")
            If Val(row.Cells(GCGSTAMT.Index).Value) <> 0 Then LBLTOTALCGSTAMT.Text = Format(Val(LBLTOTALCGSTAMT.Text) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue), "0.00")
            If Val(row.Cells(GSGSTAMT.Index).Value) <> 0 Then LBLTOTALSGSTAMT.Text = Format(Val(LBLTOTALSGSTAMT.Text) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue), "0.00")
            If Val(row.Cells(GIGSTAMT.Index).Value) <> 0 Then LBLTOTALIGSTAMT.Text = Format(Val(LBLTOTALIGSTAMT.Text) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
            If Val(row.Cells(GGRIDTOTAL.Index).Value) <> 0 Then TXTTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(row.Cells(GGRIDTOTAL.Index).EditedFormattedValue), "0.00")

        Next


        If cmbaddsub.Text = "Add" Then
            TXTSUBTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(txtotherchg.Text), "0.00")
        Else
            TXTSUBTOTAL.Text = Format((Val(TXTTOTAL.Text)) - Val(txtotherchg.Text), "0.00")
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

    Private Sub TXTSRNO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTSRNO.Enter
        If GRIDDOUBLECLICK = False Then
            If GRIDSALRET.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDSALRET.Rows(GRIDSALRET.RowCount - 1).Cells(gsrno.Index).Value) + 1
            Else
                TXTSRNO.Text = 1
            End If
        End If
    End Sub

   

    'Private Sub CMBITEMNAME_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
    '    Try
    '        TXTHSNCODE.Clear()
    '        TXTCGSTPER.Clear()
    '        TXTCGSTAMT.Clear()
    '        TXTSGSTPER.Clear()
    '        TXTSGSTAMT.Clear()
    '        TXTIGSTPER.Clear()
    '        TXTIGSTAMT.Clear()
    '        If CMBITEMNAME.Text.Trim <> "" And Convert.ToDateTime(INVDATE.Text).Date >= "01/07/2017" Then
    '            Dim OBJCMN As New ClsCommon
    '            Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
    '            If DT.Rows.Count > 0 Then
    '                TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
    '                If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
    '                    TXTIGSTPER.Text = 0
    '                    TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
    '                    TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
    '                Else
    '                    TXTCGSTPER.Text = 0
    '                    TXTSGSTPER.Text = 0
    '                    TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
    '                End If
    '            End If
    '            calc()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

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

    Sub fillgrid()

        GRIDSALRET.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDSALRET.Rows.Add(Val(TXTSRNO.Text.Trim), TXTPARTNO.Text.Trim, CMBITEMNAME.Text.Trim, TXTHSNCODE.Text.Trim, TXTMAKE.Text.Trim, Format(Val(TXTBOXES.Text.Trim), "0"), Format(Val(TXTEACHBOXES.Text.Trim), "0"), Format(Val(TXTQTY.Text.Trim), "0"), CMBUNIT.Text.Trim, Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"), Format(Val(TXTOTHERAMT.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Val(TXTCGSTPER.Text.Trim), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Val(TXTSGSTPER.Text.Trim), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Val(TXTIGSTPER.Text.Trim), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"))
            getsrno(GRIDSALRET)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDSALRET.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDSALRET.Item(GPARTNO.Index, TEMPROW).Value = TXTPARTNO.Text.Trim
            GRIDSALRET.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text
            GRIDSALRET.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDSALRET.Item(GMAKE.Index, TEMPROW).Value = TXTMAKE.Text.Trim
            GRIDSALRET.Item(GBOXES.Index, TEMPROW).Value = Format(Val(TXTBOXES.Text), "0.00")
            GRIDSALRET.Item(GEACHBOXES.Index, TEMPROW).Value = Format(Val(TXTEACHBOXES.Text), "0.00")
            GRIDSALRET.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")
            GRIDSALRET.Item(GUNIT.Index, TEMPROW).Value = CMBUNIT.Text.Trim
            GRIDSALRET.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text), "0.00")
            GRIDSALRET.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text), "0.00")
            GRIDSALRET.Item(GOTHERAMT.Index, TEMPROW).Value = Format(Val(TXTOTHERAMT.Text.Trim), "0.00")
            GRIDSALRET.Item(GTAXABLEAMT.Index, TEMPROW).Value = Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00")
            GRIDSALRET.Item(GCGSTPER.Index, TEMPROW).Value = Val(TXTCGSTPER.Text.Trim)
            GRIDSALRET.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDSALRET.Item(GSGSTPER.Index, TEMPROW).Value = Val(TXTSGSTPER.Text.Trim)
            GRIDSALRET.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDSALRET.Item(GIGSTPER.Index, TEMPROW).Value = Val(TXTIGSTPER.Text.Trim)
            GRIDSALRET.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDSALRET.Item(GGRIDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If
        TXTAMOUNT.ReadOnly = True
        total()
        GRIDSALRET.FirstDisplayedScrollingRowIndex = GRIDSALRET.RowCount - 1

        CMBITEMNAME.Text = ""
        TXTPARTNO.Clear()
        TXTMAKE.Clear()
        TXTBOXES.Clear()
        TXTEACHBOXES.Clear()

        TXTQTY.Clear()
        'CMBUNIT.Text = ""
        TXTRATE.Clear()
        TXTAMOUNT.Clear()
        TXTOTHERAMT.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTGRIDTOTAL.Clear()
        TXTSRNO.Text = Val(GRIDSALRET.RowCount)
        If ClientName = "SHAHTRADE" Then TXTPARTNO.Focus() Else CMBITEMNAME.Focus()

    End Sub

    Private Sub GRIDQUOTE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDSALRET.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        editrow()
    End Sub

    Sub editrow()
        Try
            If GRIDSALRET.CurrentRow.Index >= 0 And GRIDSALRET.Item(gsrno.Index, GRIDSALRET.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDSALRET.Item(gsrno.Index, GRIDSALRET.CurrentRow.Index).Value
                TXTPARTNO.Text = GRIDSALRET.Item(GPARTNO.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDSALRET.Item(GITEMNAME.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDSALRET.Item(GHSNCODE.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTMAKE.Text = GRIDSALRET.Item(GMAKE.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTBOXES.Text = Val(GRIDSALRET.Item(GBOXES.Index, GRIDSALRET.CurrentRow.Index).Value)
                TXTEACHBOXES.Text = Val(GRIDSALRET.Item(GEACHBOXES.Index, GRIDSALRET.CurrentRow.Index).Value)

                TXTQTY.Text = Val(GRIDSALRET.Item(GQTY.Index, GRIDSALRET.CurrentRow.Index).Value)
                CMBUNIT.Text = GRIDSALRET.Item(GUNIT.Index, GRIDSALRET.CurrentRow.Index).Value
                TXTRATE.Text = Val(GRIDSALRET.Item(GRATE.Index, GRIDSALRET.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDSALRET.Item(GAMT.Index, GRIDSALRET.CurrentRow.Index).Value)
                TXTOTHERAMT.Text = GRIDSALRET.Item(GOTHERAMT.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTTAXABLEAMT.Text = GRIDSALRET.Item(GTAXABLEAMT.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTCGSTPER.Text = GRIDSALRET.Item(GCGSTPER.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTCGSTAMT.Text = GRIDSALRET.Item(GCGSTAMT.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTSGSTPER.Text = GRIDSALRET.Item(GSGSTPER.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTSGSTAMT.Text = GRIDSALRET.Item(GSGSTAMT.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTIGSTPER.Text = GRIDSALRET.Item(GIGSTPER.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTIGSTAMT.Text = GRIDSALRET.Item(GIGSTAMT.Index, GRIDSALRET.CurrentRow.Index).Value.ToString
                TXTGRIDTOTAL.Text = GRIDSALRET.Item(GGRIDTOTAL.Index, GRIDSALRET.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDSALRET.CurrentRow.Index
                TXTPARTNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtamount_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMOUNT.Validating
        'If TXTPARTNO.Text.Trim <> "" And CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And CMBUNIT.Text.Trim <> "" And Val(TXTRATE.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 Then
        '    fillgrid()
        '    total()
        'Else
        '    If TXTPARTNO.Text.Trim = "" Then
        '        MsgBox("Please Fill Part No ")
        '        Exit Sub
        '    End If

        '    If CMBITEMNAME.Text.Trim = "" Then
        '        MsgBox("Please Fill Item Name ")
        '        Exit Sub
        '    End If

        '    'If TXTMAKE.Text.Trim = "" Then
        '    '    MsgBox("Please Fill Make")
        '    '    Exit Sub
        '    'End If

        '    If Val(TXTQTY.Text.Trim) <= 0 Then
        '        MsgBox("Please Fill Qty ")
        '        Exit Sub
        '    End If

        '    If CMBUNIT.Text.Trim = "" Then
        '        MsgBox("Please Fill Unit")
        '        Exit Sub
        '    End If

        '    If Val(TXTRATE.Text.Trim) <= 0 Then
        '        MsgBox("Please Fill Rate ")
        '        Exit Sub
        '    End If
        'End If
    End Sub

    Sub calc()
        Try
            TXTGRIDTOTAL.Text = 0.0
            TXTCGSTAMT.Text = 0.0
            TXTSGSTAMT.Text = 0.0
            TXTIGSTAMT.Text = 0.0

            If Val(TXTEACHBOXES.Text.Trim) > 0 And Val(TXTBOXES.Text.Trim) > 0 Then TXTQTY.Text = Val(TXTEACHBOXES.Text.Trim) * Val(TXTBOXES.Text.Trim)
            TXTAMOUNT.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000")

            TXTTAXABLEAMT.Text = Format((Val(TXTAMOUNT.Text.Trim) + Val(TXTOTHERAMT.Text.Trim)), "0.00")
            TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTQTY.Validated
        calc()
    End Sub

    Private Sub txtrate_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTRATE.Validated
        calc()
    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, TXTRATE, Me)
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDSALRET.RowCount = 0
Line1:
            TEMPSALRETNO = Val(TXTSALRETNO.Text) - 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            If TEMPSALRETNO > 0 Then
                EDIT = True
                InvoiceMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSALRET.RowCount = 0 And TEMPSALRETNO > 1 Then
                TXTSALRETNO.Text = TEMPSALRETNO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDSALRET.RowCount = 0
LINE1:
            TEMPSALRETNO = Val(TXTQUOTENO.Text) + 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            GETMAX_SALRET_NO()
            Dim MAXNO As Integer = TXTSALRETNO.Text.Trim
            CLEAR()
            If Val(TXTQUOTENO.Text) - 1 >= TEMPSALRETNO Then
                EDIT = True
                InvoiceMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDSALRET.RowCount = 0 And TEMPSALRETNO < MAXNO Then
                TXTQUOTENO.Text = TEMPSALRETNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            GRIDSALRET.RowCount = 0
            TEMPSALRETNO = Val(tstxtbillno.Text)
            TEMPREGNAME = CMBREGISTER.Text.Trim
            If TEMPSALRETNO > 0 Then
                EDIT = True
                InvoiceMaster_Load(sender, e)
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
                EP.SetError(lbllocked, "Invoice Locked")
                Exit Sub
            End If

            If EDIT = True Then
                Dim intresult As Integer
                Dim objpur As New ClsSaleReturn
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPSALRETNO)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.Delete
                    MsgBox("SaleReturn Invoice Deleted!!!")
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
            Dim objpurinv As New SaleRegisterDetails
            objpurinv.MdiParent = MDIMain
            objpurinv.Show()
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

    Private Sub cmbtax_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTAX.Validating
        Try
            If CMBTAX.Text.Trim <> "" Then TAXvalidate(CMBTAX, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtax_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTAX.Enter
        Try
            If CMBTAX.Text.Trim = "" Then filltax(CMBTAX, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtax_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTAX.Validated
        total()
    End Sub

    Private Sub txttax_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTTAX.Validated
        total()
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

    Private Sub GRIDQUOTE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDSALRET.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDSALRET.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDSALRET.Rows.RemoveAt(GRIDSALRET.CurrentRow.Index)
                total()
                getsrno(GRIDSALRET)
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

    Sub AMOUNTNUMDOTKYEPRESS(ByVal han As KeyPressEventArgs, ByVal sen As Control, ByVal frm As System.Windows.Forms.Form)
        Try
            Dim mypos As Integer

            If AscW(han.KeyChar) >= 48 And AscW(han.KeyChar) <= 57 Or AscW(han.KeyChar) = 8 Or AscW(han.KeyChar) = 45 Then
                han.KeyChar = han.KeyChar
            ElseIf AscW(han.KeyChar) = 46 Or AscW(han.KeyChar) = 45 Then
                mypos = InStr(1, sen.Text, ".")
                If mypos = 0 Then
                    han.KeyChar = han.KeyChar
                Else
                    han.KeyChar = ""
                End If
            Else
                han.KeyChar = ""
            End If

            If AscW(han.KeyChar) = Keys.Escape Then
                frm.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTOTHERAMT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTOTHERAMT.KeyPress
        AMOUNTNUMDOTKYEPRESS(e, sender, Me)
    End Sub

    Private Sub TXTOTHERAMT_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTOTHERAMT.Validated, TXTBOXES.Validated, TXTEACHBOXES.Validated
        Try
            calc()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress, TXTBOXES.KeyPress, TXTEACHBOXES.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtuploadname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtuploadname.Validating
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

    Private Sub CMDUPLOAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupload.Click
        OpenFileDialog1.Filter = "Pictures (*.bmp;*.jpeg;*.png)|*.bmp;*.jpg;*.png"
        OpenFileDialog1.ShowDialog()
        txtimgpath.Text = OpenFileDialog1.FileName
        On Error Resume Next
        If txtimgpath.Text.Trim.Length <> 0 Then PBSoftCopy.ImageLocation = txtimgpath.Text.Trim
    End Sub

    Private Sub CMDVIEW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDVIEW.Click
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

    Private Sub CMDREMOVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDREMOVE.Click
        Try
            PBSoftCopy.Image = Nothing
            txtimgpath.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTUPLOADSRNO_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtuploadsrno.GotFocus
        If gridUPLOADDoubleClick = False Then
            If gridupload.RowCount > 0 Then
                txtuploadsrno.Text = Val(gridupload.Rows(gridupload.RowCount - 1).Cells(0).Value) + 1
            Else
                txtuploadsrno.Text = 1
            End If
        End If
    End Sub

    Private Sub gridupload_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.RowEnter
        Try
            If e.RowIndex >= 0 Then PBSoftCopy.Image = gridupload.Rows(e.RowIndex).Cells(GUIMGPATH.Index).Value
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridupload_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridupload.KeyDown
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

    Private Sub gridupload_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridupload.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            If e.RowIndex >= 0 And gridupload.Item(GUSRNO.Index, e.RowIndex).Value <> Nothing Then

                gridUPLOADDoubleClick = True
                txtuploadsrno.Text = gridupload.Item(GUSRNO.Index, e.RowIndex).Value
                txtuploadremarks.Text = gridupload.Item(GUREMARKS.Index, e.RowIndex).Value
                txtuploadname.Text = gridupload.Item(GUNAME.Index, e.RowIndex).Value
                PBSoftCopy.Image = gridupload.Item(GUIMGPATH.Index, e.RowIndex).Value

                tempUPLOADrow = e.RowIndex
                txtuploadremarks.Focus()
            End If

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

    Private Sub CMBITEMNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Validated
        'If CMBITEMNAME.Text.Trim = "" Then
        TXTHSNCODE.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        If CMBITEMNAME.Text.Trim <> "" And Convert.ToDateTime(SALRETDATE.Text).Date >= "01/07/2017" Then
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
            If DT.Rows.Count > 0 Then
                TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
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
            calc()
        End If
        'cmbaddsub.Focus()
        'End If
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If TXTPARTNO.Text.Trim = "" And CMBITEMNAME.Text.Trim = "" Then txtremarks.Focus()
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPSALRETNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles SALRETDATE.GotFocus
        SALRETDATE.SelectAll()
    End Sub

    Private Sub INVDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SALRETDATE.Validating
        Try
            If SALRETDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(SALRETDATE.Text, TEMP) Then
                    MsgBox("Enter Proper Date")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PODATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PODATE.GotFocus
        PODATE.SelectAll()
    End Sub

    Private Sub PODATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PODATE.Validating
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

    Private Sub CHALLANDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHALLANDATE.GotFocus
        CHALLANDATE.SelectAll()
    End Sub

    Private Sub CHALLANDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CHALLANDATE.Validating
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

    Private Sub LRDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LRDATE.Validating
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

    Private Sub CMDSELECTQUOTE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSELECTCHALLAN.Click
        Try
            If CMBNAME.Text.Trim = "" Then
                MsgBox("Please Select Party name First", MsgBoxStyle.Critical)
                CMBNAME.Focus()
                Exit Sub
            End If

            Dim DTRET As New DataTable
            Dim OBJSELECTRET As New SelectInvoiceForReturn
            OBJSELECTRET.PARTYNAME = CMBNAME.Text.Trim

            OBJSELECTRET.ShowDialog()
            DTRET = OBJSELECTRET.DT

            If DTRET.Rows.Count > 0 Then

                Dim objclspreq As New ClsCommon()
                'Dim DT As DataTable = objclspreq.search(" DISTINCT  ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()) AS INVDATE, ISNULL(INVOICEMASTER.INVOICE_GDNNO, '') AS GDNNO, ISNULL(INVOICEMASTER.INVOICE_GDNDATE, GETDATE()) AS GDNDATE, ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS, 0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_PER, '') AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(INVOICEMASTER.INVOICE_TOTALPCS, 0) AS TOTALPCS, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS TOTALMTRS ", "", " ITEMMASTER INNER JOIN INVOICEMASTER_DESC ON ITEMMASTER.item_id = INVOICEMASTER_DESC.INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID INNER JOIN QUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = QUALITYMASTER.QUALITY_id INNER JOIN DESIGNMASTER ON INVOICEMASTER_DESC.INVOICE_DESIGNID = DESIGNMASTER.DESIGN_id INNER JOIN COLORMASTER ON INVOICEMASTER_DESC.INVOICE_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id ", "  AND INVOICEMASTER.INVOICE_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND INVOICEMASTER.INVOICE_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " AND INVOICEMASTER.INVOICE_RETURN = " & 0 & " ORDER BY INVNO")
                Dim DT As New DataTable

                If DTRET.Rows(0).Item("INVOICETYPE") = "INVOICE" Then
                    'DT = objclspreq.search(" ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(ISNULL(ITEMMASTER.item_name, '') AS ITEM, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(QUALITYMASTER.QUALITY_name, '') AS QUALITY, ISNULL(DESIGNMASTER.DESIGN_NO, '') AS DESIGN, ISNULL(COLORMASTER.COLOR_name, '') AS COLOR, ISNULL(INVOICEMASTER_DESC.INVOICE_PCS, 0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_MTRS, 0) AS MTRS, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_PER, '') AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(REGISTERMASTER.register_name, '') AS INVREGNAME ", "", " ITEMMASTER INNER JOIN INVOICEMASTER_DESC ON ITEMMASTER.item_id = INVOICEMASTER_DESC.INVOICE_ITEMID INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN COLORMASTER ON INVOICEMASTER_DESC.INVOICE_COLORID = COLORMASTER.COLOR_id LEFT OUTER JOIN DESIGNMASTER ON INVOICEMASTER_DESC.INVOICE_DESIGNID = DESIGNMASTER.DESIGN_id LEFT OUTER JOIN QUALITYMASTER ON INVOICEMASTER_DESC.INVOICE_QUALITYID = QUALITYMASTER.QUALITY_id LEFT OUTER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id ", "  AND INVOICEMASTER.INVOICE_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND INVOICEMASTER.INVOICE_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVNO")
                    DT = objclspreq.search(" ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO, 0) AS SRNO, ISNULL(INVOICEMASTER_DESC.INVOICE_PARTNO, '') AS PARTNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEM, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(INVOICEMASTER_DESC.INVOICE_MAKE, '') AS MAKE, ISNULL(INVOICEMASTER_DESC.INVOICE_BOXES, 0) AS BOXES, ISNULL(INVOICEMASTER_DESC.INVOICE_EACHBOXES, 0) AS EACHBOXES, ISNULL(INVOICEMASTER_DESC.INVOICE_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS PER, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE, 0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT, 0) AS AMT, ISNULL(INVOICEMASTER_DESC.INVOICE_OTHERAMT, 0) AS OTHERAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTPER, 0) AS CGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT, 0) AS CGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTPER, 0) AS SGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT, 0) AS SGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTPER, 0) AS IGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTAMT, 0) AS IGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL, 0) AS GRIDTOTAL, ISNULL(INVOICEMASTER.INVOICE_NO, 0) AS INVNO, ISNULL(INVOICEMASTER.INVOICE_DATE, GETDATE()) AS INVDATE, INVOICEMASTER.INVOICE_REGISTERID AS REGID, REGISTERMASTER.register_name AS INVREGNAME, 'INVOICE' AS INVOICETYPE ", "", " INVOICEMASTER_DESC INNER JOIN ITEMMASTER ON INVOICEMASTER_DESC.INVOICE_ITEMID = ITEMMASTER.ITEM_id INNER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN UNITMASTER ON INVOICEMASTER_DESC.INVOICE_UNITID = UNITMASTER.unit_id INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTERMASTER.register_id ", "  AND INVOICEMASTER.INVOICE_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND INVOICEMASTER.INVOICE_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVNO")

                Else
                    DT = objclspreq.search(" ISNULL(OPENINGBILL.BILL_NO, 0) AS INVNO, '' AS ITEM, '' AS HSNCODE, '' AS QUALITY, '' AS DESIGN, '' AS COLOR, 0 AS PCS, 0 AS MTRS, 0 AS RATE, 'Mtrs' AS PER, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, 0 AS CGSTPER, 0 AS SGSTPER, 0 AS IGSTPER,  ISNULL(REGISTERMASTER.register_name, '') AS INVREGNAME ", "", " OPENINGBILL INNER JOIN LEDGERS ON OPENINGBILL.BILL_LEDGERID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON OPENINGBILL.BILL_REGISTERID = REGISTERMASTER.register_id ", "  AND OPENINGBILL.BILL_NO =" & Val(DTRET.Rows(0).Item("INVNO")) & "  AND OPENINGBILL.BILL_REGISTERID =" & Val(DTRET.Rows(0).Item("REGID")) & "  AND OPENINGBILL.BILL_YEARID = " & YearId & " ORDER BY INVNO")
                End If
                TXTINVNO.Text = DT.Rows(0).Item("INVNO")
                TXTINVREGNAME.Text = DT.Rows(0).Item("INVREGNAME")
                INVDATE.Text = DT.Rows(0).Item("INVDATE")
                TXTINVTYPE.Text = DTRET.Rows(0).Item("INVOICETYPE")

               

                If TXTINVTYPE.Text.Trim = "INVOICE" Then
                    Dim SNO As Integer = 0
                    For Each DTROWPS As DataRow In DT.Rows
                        If EDIT = False Then
                            'GRIDSALRET.Rows.Add(0, "", DTROWPS("ITEM"), DTROWPS("HSNCODE"), DTROWPS("QUALITY"), DTROWPS("DESIGN"), DTROWPS("COLOR"), "0.00", "0.00", Format(Val(DTROWPS("PCS")), "0.00"), "Mtrs", Format(Val(DTROWPS("MTRS")), "0.00"), 0, DTROWPS("PER"), 0, "", "", "SR-" & Val(TXTSALRETNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId, DTROWPS("GDNBARCODE"), 0, 0, 0, DTROWPS("GDNNO"), DTROWPS("SRNO"))
                            'GRIDSALRET.Rows.Add(0, "FRESH", DTROWPS("ITEM"), DTROWPS("HSNCODE"), DTROWPS("QUALITY"), DTROWPS("DESIGN"), DTROWPS("COLOR"), "0.00", "0.00", Format(Val(DTROWPS("PCS")), "0.00"), "Pcs", Format(Val(DTROWPS("MTRS")), "0.00"), Val(DTROWPS("RATE")), DTROWPS("PER"), 0, "", "", "SR-" & Val(TXTSALRETNO.Text.Trim) & "/" & SNO + 1 & "/" & YearId, "", 0, 0, 0, DTROWPS("GDNNO"), "")
                            GRIDSALRET.Rows.Add(0, DTROWPS("PARTNO"), DTROWPS("ITEM"), DTROWPS("HSNCODE"), DTROWPS("MAKE"), DTROWPS("BOXES"), DTROWPS("EACHBOXES"), Format(Val(DTROWPS("QTY")), "0.00"), DTROWPS("PER"), Format(Val(DTROWPS("RATE")), "0.00"), Format(Val(DTROWPS("AMT")), "0.00"), Format(Val(DTROWPS("OTHERAMT")), "0.00"), Format(Val(DTROWPS("TAXABLEAMT")), "0.00"), Format(Val(DTROWPS("CGSTPER")), "0.00"), Format(Val(DTROWPS("CGSTAMT")), "0.00"), Format(Val(DTROWPS("SGSTPER")), "0.00"), Format(Val(DTROWPS("SGSTAMT")), "0.00"), Format(Val(DTROWPS("IGSTPER")), "0.00"), Format(Val(DTROWPS("IGSTAMT")), "0.00"), Format(Val(DTROWPS("GRIDTOTAL")), "0.00"))

                        Else
                            GRIDSALRET.Rows.Add(0, "FRESH", DTROWPS("ITEM"), DTROWPS("HSNCODE"), DTROWPS("QUALITY"), DTROWPS("DESIGN"), DTROWPS("COLOR"), "0.00", "0.00", Format(Val(DTROWPS("PCS")), "0.00"), "Pcs", Format(Val(DTROWPS("MTRS")), "0.00"), Val(DTROWPS("RATE")), DTROWPS("PER"), 0, "", "", "SR-" & Val(TXTSALRETNO.Text.Trim) & "/" & GRIDSALRET.RowCount + 1 & "/" & YearId, DTROWPS("GDNBARCODE"), 0, 0, 0, DTROWPS("GDNNO"), DTROWPS("SRNO"))
                        End If
                        SNO += 1

                        If DTROWPS("ITEM").ToString <> "" And Convert.ToDateTime(SALRETDATE.Text).Date >= "01/07/2017" Then
                            If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                TXTCGSTPER.Text = Val(DTROWPS("CGSTPER"))
                                TXTSGSTPER.Text = Val(DTROWPS("SGSTPER"))
                                TXTIGSTPER.Text = 0
                            Else
                                TXTCGSTPER.Text = 0
                                TXTSGSTPER.Text = 0
                                TXTIGSTPER.Text = Val(DTROWPS("IGSTPER"))
                            End If
                        End If
                    Next
                    getsrno(GRIDSALRET)
                    total()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub TXTCHALLANNO_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHALLANNO.KeyPress
        numkeypress(e, TXTCHALLANNO, Me)
    End Sub

    Private Sub tstxtbillno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstxtbillno.KeyPress
        numkeypress(e, tstxtbillno, Me)
    End Sub

    Private Sub DESPATCHEDDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DELIVERYDATE.GotFocus
        DELIVERYDATE.SelectAll()
    End Sub

    Private Sub DESPATCHEDDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DELIVERYDATE.Validating
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

    Private Sub TXTEXTRACHGS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEXTRACHGS.KeyPress
        numdotkeypress(e, TXTEXTRACHGS, Me)
    End Sub

    Private Sub txtotherchg_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtotherchg.Validating
        total()
    End Sub

    Private Sub TXTEXTRACHGS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTEXTRACHGS.Validating
        total()
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

    Private Sub cmbregister_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBREGISTER.Enter
        Try
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and register_type = 'SALE RETURN'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE RETURN' AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                cmbregister.Text = dt.Rows(0).Item(0).ToString
            End If
            GETMAX_SALRET_NO()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBREGISTER.Validating
        Try
            If cmbregister.Text.Trim.Length > 0 And edit = False Then
                clear()
                cmbregister.Text = UCase(cmbregister.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALE RETURN' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    saleregabbr = dt.Rows(0).Item(0).ToString
                    salereginitial = dt.Rows(0).Item(1).ToString
                    saleregid = dt.Rows(0).Item(2)
                    GETMAX_SALRET_NO()
                    cmbregister.Enabled = False
                Else
                    MsgBox("Register Not Present, Add New from Master Module")
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdshowdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowdetails.Click
        Try
            Dim OBJRECPAY As New ShowRecPay
            OBJRECPAY.MdiParent = MDIMain
            OBJRECPAY.PURBILLINITIALS = salereginitial & "-" & TEMPSALRETNO
            OBJRECPAY.SALEBILLINITIALS = salereginitial & "-" & TEMPSALRETNO
            OBJRECPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTGRIDTOTAL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTGRIDTOTAL.Validating
        If ClientName = "SHAHTRADE" And TXTPARTNO.Text.Trim = "" Then
            MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If CMBITEMNAME.Text.Trim <> "" And Val(TXTQTY.Text.Trim) > 0 And CMBUNIT.Text.Trim <> "" And Val(TXTRATE.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 Then
            fillgrid()
            total()
        Else
            MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        End If
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

            If CMBITEMNAME.Text.Trim <> "" And Convert.ToDateTime(SALRETDATE.Text).Date >= "01/07/2017" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER ", "", " HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
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
                total()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNAME.Validated
        If CMBNAME.Text.Trim <> "" Then
            'GET REGISTER , AGENCT AND TRANS
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid AND LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id", " and LEDGERS.acc_cmpname = '" & CMBNAME.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
            If DT.Rows.Count > 0 Then
                TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
            End If
            GETHSNCODE()
            If ClientName = "SHAHTRADE" Then CMBPACKING.Text = CMBNAME.Text
        End If
    End Sub

    Private Sub InvoiceMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SHAHTRADE" Then

                BTNPARTNO.Visible = True
                TXTPARTNO.Visible = True
                GPARTNO.Visible = True

                BTNDESC.Left = BTNPARTNO.Left + BTNPARTNO.Width
                CMBITEMNAME.Left = TXTPARTNO.Left + TXTPARTNO.Width
                BTNHSNCODE.Left = BTNDESC.Left + BTNDESC.Width
                TXTHSNCODE.Left = CMBITEMNAME.Left + CMBITEMNAME.Width
                BTNMAKE.Visible = True
                TXTMAKE.Visible = True
                GMAKE.Visible = True
                BTNMAKE.Left = BTNHSNCODE.Left + BTNHSNCODE.Width
                TXTMAKE.Left = TXTHSNCODE.Left + TXTHSNCODE.Width

                BTNBOXES.Visible = False
                TXTBOXES.Visible = False
                GBOXES.Visible = False

                BTNEACHBOXES.Visible = False
                TXTEACHBOXES.Visible = False
                GEACHBOXES.Visible = False

                BTNQTY.Left = BTNMAKE.Left + BTNMAKE.Width
                TXTQTY.Left = BTNMAKE.Left + BTNMAKE.Width
                LBLTOTALQTY.Left = TXTQTY.Left

                BTNUNIT.Left = BTNQTY.Left + BTNQTY.Width
                CMBUNIT.Left = TXTQTY.Left + TXTQTY.Width
                BTNRATE.Left = BTNUNIT.Left + BTNUNIT.Width
                TXTRATE.Left = CMBUNIT.Left + CMBUNIT.Width
                BTNAMT.Left = BTNRATE.Left + BTNRATE.Width
                TXTAMOUNT.Left = TXTRATE.Left + TXTRATE.Width
                BTNOTHERAMT.Left = BTNAMT.Left + BTNAMT.Width
                TXTOTHERAMT.Left = TXTAMOUNT.Left + TXTAMOUNT.Width
                LBLTOTALOTHERAMT.Left = TXTOTHERAMT.Left

                BTNTAXABLEAMT.Left = BTNOTHERAMT.Left + BTNOTHERAMT.Width
                TXTTAXABLEAMT.Left = TXTOTHERAMT.Left + TXTOTHERAMT.Width
                LBLTOTALTAXABLEAMT.Left = TXTTAXABLEAMT.Left

                BTNCGSTPER.Left = BTNTAXABLEAMT.Left + BTNTAXABLEAMT.Width
                TXTCGSTPER.Left = TXTTAXABLEAMT.Left + TXTTAXABLEAMT.Width

                BTNCGSTAMT.Left = BTNCGSTPER.Left + BTNCGSTPER.Width
                TXTCGSTAMT.Left = TXTCGSTPER.Left + TXTCGSTPER.Width
                LBLTOTALCGSTAMT.Left = TXTCGSTAMT.Left

                BTNSGSTPER.Left = BTNCGSTAMT.Left + BTNCGSTAMT.Width
                TXTSGSTPER.Left = TXTCGSTAMT.Left + TXTCGSTAMT.Width

                BTNSGSTAMT.Left = BTNSGSTPER.Left + BTNSGSTPER.Width
                TXTSGSTAMT.Left = TXTSGSTPER.Left + TXTSGSTPER.Width
                LBLTOTALSGSTAMT.Left = TXTSGSTAMT.Left

                BTNIGSTPER.Left = BTNSGSTAMT.Left + BTNSGSTAMT.Width
                TXTIGSTPER.Left = TXTSGSTAMT.Left + TXTSGSTAMT.Width
                BTNIGSTAMT.Left = BTNIGSTPER.Left + BTNIGSTPER.Width
                TXTIGSTAMT.Left = TXTIGSTPER.Left + TXTIGSTPER.Width
                LBLTOTALIGSTAMT.Left = TXTIGSTAMT.Left

                BTNGTOTAL.Left = BTNIGSTAMT.Left + BTNIGSTAMT.Width
                TXTGRIDTOTAL.Left = TXTIGSTAMT.Left + TXTIGSTAMT.Width
                GRIDSALRET.Width = 1840

                CMDSELECTCHALLAN.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPACKING.Validating
        Try
            If CMBPACKING.Text.Trim <> "" Then namevalidate(CMBPACKING, CMBCODE, e, Me, txtDeliveryadd, " AND  (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')", "Sundry Creditors", "ACCOUNTS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleReturn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SHAHTRADE" Then

                BTNPARTNO.Visible = True
                TXTPARTNO.Visible = True
                GPARTNO.Visible = True

                BTNDESC.Left = BTNPARTNO.Left + BTNPARTNO.Width
                CMBITEMNAME.Left = TXTPARTNO.Left + TXTPARTNO.Width
                BTNHSNCODE.Left = BTNDESC.Left + BTNDESC.Width
                TXTHSNCODE.Left = CMBITEMNAME.Left + CMBITEMNAME.Width
                BTNMAKE.Visible = True
                TXTMAKE.Visible = True
                GMAKE.Visible = True
                BTNMAKE.Left = BTNHSNCODE.Left + BTNHSNCODE.Width
                TXTMAKE.Left = TXTHSNCODE.Left + TXTHSNCODE.Width

                BTNBOXES.Visible = False
                TXTBOXES.Visible = False
                GBOXES.Visible = False

                BTNEACHBOXES.Visible = False
                TXTEACHBOXES.Visible = False
                GEACHBOXES.Visible = False

                BTNQTY.Left = BTNMAKE.Left + BTNMAKE.Width
                TXTQTY.Left = BTNMAKE.Left + BTNMAKE.Width
                LBLTOTALQTY.Left = TXTQTY.Left

                BTNUNIT.Left = BTNQTY.Left + BTNQTY.Width
                CMBUNIT.Left = TXTQTY.Left + TXTQTY.Width
                BTNRATE.Left = BTNUNIT.Left + BTNUNIT.Width
                TXTRATE.Left = CMBUNIT.Left + CMBUNIT.Width
                BTNAMT.Left = BTNRATE.Left + BTNRATE.Width
                TXTAMOUNT.Left = TXTRATE.Left + TXTRATE.Width
                BTNOTHERAMT.Left = BTNAMT.Left + BTNAMT.Width
                TXTOTHERAMT.Left = TXTAMOUNT.Left + TXTAMOUNT.Width
                LBLTOTALOTHERAMT.Left = TXTOTHERAMT.Left

                BTNTAXABLEAMT.Left = BTNOTHERAMT.Left + BTNOTHERAMT.Width
                TXTTAXABLEAMT.Left = TXTOTHERAMT.Left + TXTOTHERAMT.Width
                LBLTOTALTAXABLEAMT.Left = TXTTAXABLEAMT.Left

                BTNCGSTPER.Left = BTNTAXABLEAMT.Left + BTNTAXABLEAMT.Width
                TXTCGSTPER.Left = TXTTAXABLEAMT.Left + TXTTAXABLEAMT.Width

                BTNCGSTAMT.Left = BTNCGSTPER.Left + BTNCGSTPER.Width
                TXTCGSTAMT.Left = TXTCGSTPER.Left + TXTCGSTPER.Width
                LBLTOTALCGSTAMT.Left = TXTCGSTAMT.Left

                BTNSGSTPER.Left = BTNCGSTAMT.Left + BTNCGSTAMT.Width
                TXTSGSTPER.Left = TXTCGSTAMT.Left + TXTCGSTAMT.Width

                BTNSGSTAMT.Left = BTNSGSTPER.Left + BTNSGSTPER.Width
                TXTSGSTAMT.Left = TXTSGSTPER.Left + TXTSGSTPER.Width
                LBLTOTALSGSTAMT.Left = TXTSGSTAMT.Left

                BTNIGSTPER.Left = BTNSGSTAMT.Left + BTNSGSTAMT.Width
                TXTIGSTPER.Left = TXTSGSTAMT.Left + TXTSGSTAMT.Width
                BTNIGSTAMT.Left = BTNIGSTPER.Left + BTNIGSTPER.Width
                TXTIGSTAMT.Left = TXTIGSTPER.Left + TXTIGSTPER.Width
                LBLTOTALIGSTAMT.Left = TXTIGSTAMT.Left

                BTNGTOTAL.Left = BTNIGSTAMT.Left + BTNIGSTAMT.Width
                TXTGRIDTOTAL.Left = TXTIGSTAMT.Left + TXTIGSTAMT.Width
                GRIDSALRET.Width = 1840

                CMDSELECTCHALLAN.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    

    
   
End Class