
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports RestSharp
Imports Newtonsoft.Json
Imports TaxProEInvoice.API
Imports System.IO
Imports System.Net
Imports BL
Imports System.ComponentModel

Public Class InvoiceMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, gridUPLOADDoubleClick As Boolean
    Dim TEMPROW, TEMPUPLOADROW As Integer
    Public EDIT As Boolean
    Public TEMPINVNO As Integer
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
        TXTBALES.Clear()
        TXTDELTHROUGH.Clear()
        TXTDISCPER.Clear()
        CMBTRANSPORT.Text = ""
        CMBAGENT.Text = ""
        VALIDTILL.Text = DateAdd(DateInterval.Month, 1, Mydate)
        INVDATE.Text = Mydate

        GRIDINVOICE.RowCount = 0
        TXTSRNO.Text = 1
        TXTPARTNO.Clear()
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

        TXTVEHICLENO.Clear()
        If CMPCITYNAME <> "" Then CMBFROMCITY.Text = CMPCITYNAME Else CMBFROMCITY.Text = ""
        CMBTOCITY.Text = ""
        CMBPACKING.Text = ""
        txtDeliveryadd.Clear()


        CMDSELECTCHALLAN.Enabled = True

        LBLTOTALQTY.Text = 0
        LBLTOTALAMT.Text = 0.0
        TXTHSNCODE.Clear()
        LBLTOTALCGSTAMT.Text = 0
        LBLTOTALSGSTAMT.Text = 0
        LBLTOTALIGSTAMT.Text = 0
        TXTOTHERAMT.Clear()
        TXTCOURIERCHGS.Clear()
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
        LBLTOTALCOURIERCHGS.Text = 0
        LBLTOTALTAXABLEAMT.Text = 0
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

        txtinwords.Clear()
        TXTCHECKEDBY.Clear()
        TXTPACKEDBY.Clear()

        GETMAX_INVOICE_NO()
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

        TXTIRNNO.Clear()
        TXTACKNO.Clear()
        ACKDATE.Value = Now.Date
        PBQRCODE.Image = Nothing
        CMBDISPATCHFROM.Text = ""
        CHKEXPORTGST.CheckState = CheckState.Unchecked
        CMBMODE.SelectedIndex = 0

    End Sub

    Sub GETMAX_INVOICE_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(INVOICE_no),0) + 1 ", " INVOICEMASTER INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID", " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and INVOICE_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
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
                GRIDINVOICE.Focus()
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
        fillregister(CMBREGISTER, " and register_type = 'SALE'")
        fillname(CMBNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillname(CMBTRANSPORT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'TRANSPORT'")
        fillname(CMBAGENT, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY CREDITORS' AND ACC_TYPE = 'AGENT'")
        filltax(CMBTAX, EDIT)
        FILLITEMNAME(CMBITEMNAME, EDIT)
        fillunit(CMBUNIT)
        fillname(CMBOTHERCHGS, EDIT, "")
        fillname(CMBEXTRACHGS, EDIT, "")
        If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
        If CMBDISPATCHFROM.Text.Trim = "" Then fillname(CMBDISPATCHFROM, EDIT, " AND (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS') AND ACC_TYPE = 'ACCOUNTS'")
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
                Dim OBJINV As New ClsSaleInvoice

                ALPARAVAL.Add(TEMPINVNO)
                ALPARAVAL.Add(TEMPREGNAME)
                ALPARAVAL.Add(YearId)

                OBJINV.alParaval = ALPARAVAL
                DT = OBJINV.selectNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTINVOICENO.Text = TEMPINVNO
                        TXTSTATECODE.Text = DR("STATECODE")
                        TXTGSTIN.Text = DR("GSTIN")
                        CMBREGISTER.Text = Convert.ToString(DR("REGNAME"))
                        INVDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBNAME.Text = Convert.ToString(DR("NAME"))
                        TXTREFNO.Text = Convert.ToString(DR("REFNO"))
                        TXTBALES.Text = Val(DR("BALES"))
                        TXTDELTHROUGH.Text = Convert.ToString(DR("DELIVERYTHROUGH"))
                        CMBTRANSPORT.Text = Convert.ToString(DR("TRANSNAME"))
                        VALIDTILL.Text = DR("VALIDTILL")
                        txtremarks.Text = Convert.ToString(DR("REMARKS"))

                        TXTDESPATCHEDTO.Text = Convert.ToString(DR("DESPATCHEDTO"))
                        CMBPACKING.Text = Convert.ToString(DR("PACKING"))
                        CMBAGENT.Text = Convert.ToString(DR("AGENTNAME"))
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

                        TXTCHECKEDBY.Text = DR("CHECKEDBY")
                        TXTPACKEDBY.Text = DR("PACKEDBY")

                        TXTVEHICLENO.Text = DR("VEHICLENO")
                        CMBFROMCITY.Text = DR("FROMCITY")
                        CMBTOCITY.Text = DR("TOCITY")


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
                        GRIDINVOICE.Rows.Add(DR("SRNO").ToString, DR("PARTNO").ToString, DR("ITEMNAME").ToString, Convert.ToString(DR("HSNCODE")), DR("MAKE").ToString, Format(Val(DR("BOXES")), "0"), Format(Val(DR("EACHBOXES")), "0.00"), Format(Val(DR("QTY")), "0.00"), DR("UNIT"), Format(Val(DR("RATE")), "0.00"), Format(Val(DR("AMT")), "0.00"), Format(Val(DR("OTHERAMT")), "0.00"), Format(Val(DR("COURIERCHGS")), "0.00"), Format(Val(DR("TAXABLEAMT")), "0.00"), Format(Val(DR("CGSTPER")), "0.00"), Format(Val(DR("CGSTAMT")), "0.00"), Format(Val(DR("SGSTPER")), "0.00"), Format(Val(DR("SGSTAMT")), "0.00"), Format(Val(DR("IGSTPER")), "0.00"), Format(Val(DR("IGSTAMT")), "0.00"), Format(Val(DR("GRIDTOTAL")), "0.00"))
                        TabControl1.SelectedIndex = (0)

                        TXTIRNNO.Text = DR("IRNNO")
                        TXTACKNO.Text = DR("ACKNO")
                        ACKDATE.Value = DR("ACKDATE")
                        If IsDBNull(DR("QRCODE")) = False Then
                            PBQRCODE.Image = Image.FromStream(New IO.MemoryStream(DirectCast(DR("QRCODE"), Byte())))
                        Else
                            PBQRCODE.Image = Nothing
                        End If
                        CMBDISPATCHFROM.Text = DR("DISPATCHFROM")
                        CMBMODE.Text = DR("MODE")
                        CHKEXPORTGST.Checked = Convert.ToBoolean(DR("CHKEXPORTGST"))
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
                    Dim DTTABLE As DataTable = OBJCMNN.search("ISNULL(INVOICE_UPSRNO, 0) AS SRNO, ISNULL(INVOICE_UPREMARKS, '') AS REMARKS, ISNULL(INVOICE_UPNAME, '') AS NAME, INVOICE_IMGPATH AS IMGPATH", "", "INVOICEMASTER_UPLOAD", "AND INVOICEMASTER_UPLOAD.INVOICE_NO= " & TEMPINVNO & "  AND INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_UPLOAD.INVOICE_UPSRNO")
                    If DTTABLE.Rows.Count > 0 Then
                        For Each DTR As DataRow In DTTABLE.Rows
                            gridupload.Rows.Add(DTR("SRNO"), DTR("REMARKS"), DTR("NAME"), Image.FromStream(New IO.MemoryStream(DirectCast(DTR("IMGPATH"), Byte()))))
                        Next
                    End If
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1
                End If
                CMBREGISTER.Enabled = False
                CMDSELECTCHALLAN.Enabled = False
                INVDATE.Focus()
                TOTAL()
                getsrno(GRIDINVOICE)

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
            ALPARAVAL.Add(Format(Convert.ToDateTime(INVDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBREGISTER.Text.Trim)
            ALPARAVAL.Add(CMBNAME.Text.Trim)
            ALPARAVAL.Add(TXTREFNO.Text.Trim)
            ALPARAVAL.Add(Val(TXTBALES.Text.Trim))
            ALPARAVAL.Add(TXTDELTHROUGH.Text.Trim)
            ALPARAVAL.Add(CMBTRANSPORT.Text.Trim)
            ALPARAVAL.Add(DELIVERYDATE.Text.Trim)
            ALPARAVAL.Add(VALIDTILL.Text.Trim)

            ALPARAVAL.Add(TXTDESPATCHEDTO.Text.Trim)
            ALPARAVAL.Add(CMBPACKING.Text.Trim)
            ALPARAVAL.Add(CMBAGENT.Text.Trim)

            ALPARAVAL.Add(TXTLRNO.Text.Trim)
            'If LRDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(LRDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(LRDATE.Text.Trim)
            ALPARAVAL.Add(TXTEWAYBILLNO.Text.Trim)
            ALPARAVAL.Add(TXTPONO.Text.Trim)
            'If PODATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(PODATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(PODATE.Text.Trim)
            ALPARAVAL.Add(TXTCHALLANNO.Text.Trim)
            'If CHALLANDATE.Text <> "__/__/____" Then ALPARAVAL.Add(Format(Convert.ToDateTime(CHALLANDATE.Text).Date, "MM/dd/yyyy")) Else ALPARAVAL.Add("")
            ALPARAVAL.Add(CHALLANDATE.Text)
            ALPARAVAL.Add(TXTQUOTENO.Text.Trim)
            ALPARAVAL.Add(txtremarks.Text.Trim)
            ALPARAVAL.Add(Val(LBLTOTALQTY.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALOTHERAMT.Text.Trim))
            ALPARAVAL.Add(Val(LBLTOTALCOURIERCHGS.Text.Trim))
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
            ALPARAVAL.Add(txtinwords.Text.Trim)
            ALPARAVAL.Add(TXTCHECKEDBY.Text.Trim)
            ALPARAVAL.Add(TXTPACKEDBY.Text.Trim)

            ALPARAVAL.Add(TXTVEHICLENO.Text.Trim)
            ALPARAVAL.Add(CMBFROMCITY.Text.Trim)
            ALPARAVAL.Add(CMBTOCITY.Text.Trim)


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
            Dim COURIERCHGS As String = ""
            Dim TAXABLEAMT As String = ""
            Dim CGSTPER As String = ""
            Dim CGSTAMT As String = ""
            Dim SGSTPER As String = ""
            Dim SGSTAMT As String = ""
            Dim IGSTPER As String = ""
            Dim IGSTAMT As String = ""
            Dim GRIDTOTAL As String = ""

            ' Dim GRNNO As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDINVOICE.Rows
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
                        COURIERCHGS = Val(ROW.Cells(GCOURIERCHGS.Index).Value)
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
                        COURIERCHGS = COURIERCHGS & "|" & Val(ROW.Cells(GCOURIERCHGS.Index).Value)
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
            ALPARAVAL.Add(COURIERCHGS)
            ALPARAVAL.Add(TAXABLEAMT)
            ALPARAVAL.Add(CGSTPER)
            ALPARAVAL.Add(CGSTAMT)
            ALPARAVAL.Add(SGSTPER)
            ALPARAVAL.Add(SGSTAMT)
            ALPARAVAL.Add(IGSTPER)
            ALPARAVAL.Add(IGSTAMT)
            ALPARAVAL.Add(GRIDTOTAL)


            ALPARAVAL.Add(TXTIRNNO.Text.Trim)
            ALPARAVAL.Add(TXTACKNO.Text.Trim)
            ALPARAVAL.Add(Format(ACKDATE.Value.Date, "MM/dd/yyyy"))
            If PBQRCODE.Image IsNot Nothing Then
                Dim MS As New IO.MemoryStream
                PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                ALPARAVAL.Add(MS.ToArray)
            Else
                ALPARAVAL.Add(DBNull.Value)
            End If
            ALPARAVAL.Add(CMBDISPATCHFROM.Text.Trim)
            ALPARAVAL.Add(CMBMODE.Text.Trim)
            ALPARAVAL.Add(CHKEXPORTGST.CheckState)

            Dim OBJPUR As New ClsSaleInvoice
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.SAVE()
                MsgBox("Details Added !!")
                TEMPINVNO = DTTABLE.Rows(0).Item(0)
                GENERATEEWB()
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                ALPARAVAL.Add(TEMPINVNO)
                IntResult = OBJPUR.UPDATE()
                MsgBox("Details Updated")
                EDIT = False
            End If
            PRINTREPORT(TEMPINVNO)

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
            Dim OBJORDER As New ClsSaleInvoice

            For Each ROW As Windows.Forms.DataGridViewRow In gridupload.Rows
                Dim MS As New IO.MemoryStream
                Dim ALPARAVAL As New ArrayList
                If ROW.Cells(GUSRNO.Index).Value <> Nothing Then
                    ALPARAVAL.Add(TEMPINVNO)
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

    Sub PRINTREPORT(ByVal INVNO As Integer)
        Try
            If MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo) = vbYes Then
                Dim OBJINVOICE As New SaleInvoiceDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "INVOICE"
                If ClientName = "GELATO" Then
                    If MsgBox("Wish to Print for Customer?", MsgBoxStyle.YesNo) = vbYes Then
                        OBJINVOICE.INVOICETYPE = "Original for Customer"
                    Else
                        OBJINVOICE.INVOICETYPE = "Duplicate for Transport"
                    End If
                    If TXTSTATECODE.Text.Trim <> CMPSTATECODE Then OBJINVOICE.PRINTIGST = True
                End If
                OBJINVOICE.NAME = CMBNAME.Text.Trim
                OBJINVOICE.INVNO = INVNO
                OBJINVOICE.WHERECLAUSE = "{INVOICEMASTER.INVOICE_no}=" & Val(INVNO) & " AND {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "' and {INVOICEMASTER.INVOICE_yearid}=" & YearId
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

        If GRIDINVOICE.RowCount = 0 Then
            EP.SetError(CMBNAME, "Please Enter Item Details")
            bln = False
        End If


        If INVDATE.Text = "__/__/____" Then
            EP.SetError(INVDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(INVDATE.Text) Then
                EP.SetError(INVDATE, "Date not in Accounting Year")
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

        If INVDATE.Text <> "__/__/____" Then
            If Convert.ToDateTime(INVDATE.Text).Date >= "01/07/2017" Then
                If TXTSTATECODE.Text.Trim.Length = 0 Then
                    EP.SetError(TXTSTATECODE, "Please enter the state code")
                    bln = False
                End If

                If TXTGSTIN.Text.Trim.Length = 0 And ClientName <> "SHENSHE" Then
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

        If Convert.ToDateTime(INVDATE.Text).Date >= "01/02/2018" And TXTGTOTAL.Text > 100000 And ClientName <> "SHENSHE" Then
            If TXTEWAYBILLNO.Text.Trim.Length = 0 Then
                If MsgBox("E-Way No. Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    EP.SetError(TXTEWAYBILLNO, " Please Enter E-Way No..... ")
                    bln = False
                End If
            End If
        End If

        Return bln
    End Function

    Sub TOTAL()

        LBLTOTALAMT.Text = "0.0"
        LBLTOTALOTHERAMT.Text = "0.0"
        LBLTOTALCOURIERCHGS.Text = "0.0"
        LBLTOTALTAXABLEAMT.Text = "0.0"
        LBLTOTALCGSTAMT.Text = "0.0"
        LBLTOTALSGSTAMT.Text = "0.0"
        LBLTOTALIGSTAMT.Text = "0.0"

        LBLTOTALQTY.Text = "0.00"
        TXTTOTAL.Text = 0
        TXTTAX.Text = "0.00"
        TXTGTOTAL.Text = 0
        TXTSUBTOTAL.Text = 0

        Dim dt As New DataTable
        Dim objcmn As New ClsCommon

        For Each row As DataGridViewRow In GRIDINVOICE.Rows
            If Val(row.Cells(GQTY.Index).Value) <> 0 Then LBLTOTALQTY.Text = Format(Val(LBLTOTALQTY.Text) + Val(row.Cells(GQTY.Index).Value), "0.00")
            If Val(row.Cells(GAMT.Index).Value) <> 0 Then LBLTOTALAMT.Text = Format(Val(LBLTOTALAMT.Text) + Val(row.Cells(GAMT.Index).Value), "0.00")
            If Val(row.Cells(GOTHERAMT.Index).Value) <> 0 Then LBLTOTALOTHERAMT.Text = Format(Val(LBLTOTALOTHERAMT.Text) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue), "0.00")
            If Val(row.Cells(GCOURIERCHGS.Index).Value) <> 0 Then LBLTOTALCOURIERCHGS.Text = Format(Val(LBLTOTALCOURIERCHGS.Text) + Val(row.Cells(GCOURIERCHGS.Index).EditedFormattedValue), "0.00")
            row.Cells(GTAXABLEAMT.Index).Value = Format(Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue) + Val(row.Cells(GCOURIERCHGS.Index).EditedFormattedValue), "0.00")

            'CHANGE GST% WITH RESPECT TO RATE (TAXABLEAMT / QTY)
            dt = objcmn.search("ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(HSN_RATEGREATERTHAN,0) AS RATE, ISNULL(HSN_CGST1,0) AS CGST1, ISNULL(HSN_SGST1,0) AS SGST1, ISNULL(HSN_IGST1,0) AS IGST1", "", "ITEMMASTER INNER JOIN HSNMASTER ON ITEM_HSNCODEID = HSN_ID", " AND ITEMMASTER.ITEM_NAME = '" & row.Cells(GITEMNAME.Index).Value & "' AND HSN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                'IF WE HAVE NOT MENTIONED RATE SECTION IN HSNCODE THEN APPLY NORMAL GST RATES AND NOT GST1 RATES
                If Val(dt.Rows(0).Item("RATE")) = 0 Then GoTo NORATE
                If Val(dt.Rows(0).Item("RATE")) <= Format((Val(row.Cells(GAMT.Index).EditedFormattedValue) + Val(row.Cells(GOTHERAMT.Index).EditedFormattedValue)) / Val(row.Cells(GQTY.Index).EditedFormattedValue), "0.00") Then
                    If Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GCGSTPER.Index).Value = Val(dt.Rows(0).Item("CGST1"))
                    If Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GSGSTPER.Index).Value = Val(dt.Rows(0).Item("SGST1"))
                    If Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GIGSTPER.Index).Value = Val(dt.Rows(0).Item("IGST1"))
                Else
NORATE:
                    If Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GCGSTPER.Index).Value = Val(dt.Rows(0).Item("CGST"))
                    If Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GSGSTPER.Index).Value = Val(dt.Rows(0).Item("SGST"))
                    If Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) > 0 Then row.Cells(GIGSTPER.Index).Value = Val(dt.Rows(0).Item("IGST"))
                End If
            End If

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
        If Val(TXTGTOTAL.Text) > 0 Then txtinwords.Text = CurrencyToWord(TXTGTOTAL.Text)

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
            If GRIDINVOICE.RowCount > 0 Then
                TXTSRNO.Text = Val(GRIDINVOICE.Rows(GRIDINVOICE.RowCount - 1).Cells(gsrno.Index).Value) + 1
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
            If CMBNAME.Text.Trim <> "" Then namevalidate(CMBNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS", CMBTRANSPORT.Text, CMBAGENT.Text)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()

        GRIDINVOICE.Enabled = True

        If GRIDDOUBLECLICK = False Then
            GRIDINVOICE.Rows.Add(Val(TXTSRNO.Text.Trim), TXTPARTNO.Text.Trim, CMBITEMNAME.Text.Trim, TXTHSNCODE.Text.Trim, TXTMAKE.Text.Trim, Format(Val(TXTBOXES.Text.Trim), "0"), Format(Val(TXTEACHBOXES.Text.Trim), "0.00"), Format(Val(TXTQTY.Text.Trim), "0.00"), CMBUNIT.Text.Trim, Format(Val(TXTRATE.Text.Trim), "0.00"), Format(Val(TXTAMOUNT.Text.Trim), "0.00"), Format(Val(TXTOTHERAMT.Text.Trim), "0.00"), Format(Val(TXTCOURIERCHGS.Text.Trim), "0.00"), Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00"), Val(TXTCGSTPER.Text.Trim), Format(Val(TXTCGSTAMT.Text.Trim), "0.00"), Val(TXTSGSTPER.Text.Trim), Format(Val(TXTSGSTAMT.Text.Trim), "0.00"), Val(TXTIGSTPER.Text.Trim), Format(Val(TXTIGSTAMT.Text.Trim), "0.00"), Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00"))
            getsrno(GRIDINVOICE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINVOICE.Item(gsrno.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDINVOICE.Item(GPARTNO.Index, TEMPROW).Value = TXTPARTNO.Text.Trim
            GRIDINVOICE.Item(GITEMNAME.Index, TEMPROW).Value = CMBITEMNAME.Text
            GRIDINVOICE.Item(GHSNCODE.Index, TEMPROW).Value = TXTHSNCODE.Text.Trim
            GRIDINVOICE.Item(GMAKE.Index, TEMPROW).Value = TXTMAKE.Text.Trim
            GRIDINVOICE.Item(GBOXES.Index, TEMPROW).Value = Format(Val(TXTBOXES.Text), "0.00")
            GRIDINVOICE.Item(GEACHBOXES.Index, TEMPROW).Value = Format(Val(TXTEACHBOXES.Text), "0.00")
            GRIDINVOICE.Item(GQTY.Index, TEMPROW).Value = Format(Val(TXTQTY.Text), "0.00")
            GRIDINVOICE.Item(GUNIT.Index, TEMPROW).Value = CMBUNIT.Text.Trim
            GRIDINVOICE.Item(GRATE.Index, TEMPROW).Value = Format(Val(TXTRATE.Text), "0.00")
            GRIDINVOICE.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMOUNT.Text), "0.00")
            GRIDINVOICE.Item(GOTHERAMT.Index, TEMPROW).Value = Format(Val(TXTOTHERAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GCOURIERCHGS.Index, TEMPROW).Value = Format(Val(TXTCOURIERCHGS.Text.Trim), "0.00")
            GRIDINVOICE.Item(GTAXABLEAMT.Index, TEMPROW).Value = Format(Val(TXTTAXABLEAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GCGSTPER.Index, TEMPROW).Value = Val(TXTCGSTPER.Text.Trim)
            GRIDINVOICE.Item(GCGSTAMT.Index, TEMPROW).Value = Format(Val(TXTCGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GSGSTPER.Index, TEMPROW).Value = Val(TXTSGSTPER.Text.Trim)
            GRIDINVOICE.Item(GSGSTAMT.Index, TEMPROW).Value = Format(Val(TXTSGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GIGSTPER.Index, TEMPROW).Value = Val(TXTIGSTPER.Text.Trim)
            GRIDINVOICE.Item(GIGSTAMT.Index, TEMPROW).Value = Format(Val(TXTIGSTAMT.Text.Trim), "0.00")
            GRIDINVOICE.Item(GGRIDTOTAL.Index, TEMPROW).Value = Format(Val(TXTGRIDTOTAL.Text.Trim), "0.00")

            GRIDDOUBLECLICK = False
        End If
        TXTAMOUNT.ReadOnly = True
        TOTAL()
        GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

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
        TXTCOURIERCHGS.Clear()
        TXTTAXABLEAMT.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        TXTGRIDTOTAL.Clear()
        TXTSRNO.Text = Val(GRIDINVOICE.RowCount)
        If ClientName = "SHAHTRADE" Or ClientName = "GELATO" Then TXTPARTNO.Focus() Else CMBITEMNAME.Focus()

    End Sub

    Private Sub GRIDQUOTE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDINVOICE.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        editrow()
    End Sub

    Sub editrow()
        Try
            If GRIDINVOICE.CurrentRow.Index >= 0 And GRIDINVOICE.Item(gsrno.Index, GRIDINVOICE.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDINVOICE.Item(gsrno.Index, GRIDINVOICE.CurrentRow.Index).Value
                TXTPARTNO.Text = GRIDINVOICE.Item(GPARTNO.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                CMBITEMNAME.Text = GRIDINVOICE.Item(GITEMNAME.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTHSNCODE.Text = GRIDINVOICE.Item(GHSNCODE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTMAKE.Text = GRIDINVOICE.Item(GMAKE.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTBOXES.Text = Val(GRIDINVOICE.Item(GBOXES.Index, GRIDINVOICE.CurrentRow.Index).Value)
                TXTEACHBOXES.Text = Val(GRIDINVOICE.Item(GEACHBOXES.Index, GRIDINVOICE.CurrentRow.Index).Value)

                TXTQTY.Text = Val(GRIDINVOICE.Item(GQTY.Index, GRIDINVOICE.CurrentRow.Index).Value)
                CMBUNIT.Text = GRIDINVOICE.Item(GUNIT.Index, GRIDINVOICE.CurrentRow.Index).Value
                TXTRATE.Text = Val(GRIDINVOICE.Item(GRATE.Index, GRIDINVOICE.CurrentRow.Index).Value)
                TXTAMOUNT.Text = Val(GRIDINVOICE.Item(GAMT.Index, GRIDINVOICE.CurrentRow.Index).Value)
                TXTOTHERAMT.Text = GRIDINVOICE.Item(GOTHERAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCOURIERCHGS.Text = GRIDINVOICE.Item(GCOURIERCHGS.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTTAXABLEAMT.Text = GRIDINVOICE.Item(GTAXABLEAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCGSTPER.Text = GRIDINVOICE.Item(GCGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTCGSTAMT.Text = GRIDINVOICE.Item(GCGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSGSTPER.Text = GRIDINVOICE.Item(GSGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTSGSTAMT.Text = GRIDINVOICE.Item(GSGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTIGSTPER.Text = GRIDINVOICE.Item(GIGSTPER.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTIGSTAMT.Text = GRIDINVOICE.Item(GIGSTAMT.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTGRIDTOTAL.Text = GRIDINVOICE.Item(GGRIDTOTAL.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString

                TEMPROW = GRIDINVOICE.CurrentRow.Index
                TXTPARTNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CALCDISC()
        Try
            If Val(TXTDISCPER.Text.Trim) > 0 And Val(TXTAMOUNT.Text.Trim) > 0 And Val(TXTOTHERAMT.Text.Trim) = 0 And ClientName = "GELATO" Then TXTOTHERAMT.Text = Format(((Val(TXTAMOUNT.Text.Trim) * Val(TXTDISCPER.Text.Trim)) / 100) * -1, "0.00")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        Try
            TXTGRIDTOTAL.Text = 0.0
            TXTCGSTAMT.Text = 0.0
            TXTSGSTAMT.Text = 0.0
            TXTIGSTAMT.Text = 0.0

            If Val(TXTEACHBOXES.Text.Trim) > 0 And Val(TXTBOXES.Text.Trim) > 0 Then TXTQTY.Text = Val(TXTEACHBOXES.Text.Trim) * Val(TXTBOXES.Text.Trim)
            TXTAMOUNT.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000")


            TXTTAXABLEAMT.Text = Format((Val(TXTAMOUNT.Text.Trim) + Val(TXTOTHERAMT.Text.Trim) + Val(TXTCOURIERCHGS.Text.Trim)), "0.00")
            TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTTAXABLEAMT.Text), "0.00")
            TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtqty_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTQTY.Validated, TXTRATE.Validated
        calc()
        CALCDISC()
    End Sub

    Private Sub toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolprevious.Click
        Try
            GRIDINVOICE.RowCount = 0
Line1:
            TEMPINVNO = Val(TXTINVOICENO.Text) - 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            If TEMPINVNO > 0 Then
                EDIT = True
                InvoiceMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDINVOICE.RowCount = 0 And TEMPINVNO > 1 Then
                TXTINVOICENO.Text = TEMPINVNO
                GoTo Line1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub toolnext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolnext.Click
        Try
            GRIDINVOICE.RowCount = 0
LINE1:
            TEMPINVNO = Val(TXTINVOICENO.Text) + 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            GETMAX_INVOICE_NO()
            Dim MAXNO As Integer = TXTINVOICENO.Text.Trim
            CLEAR()
            If Val(TXTINVOICENO.Text) - 1 >= TEMPINVNO Then
                EDIT = True
                InvoiceMaster_Load(sender, e)
            Else
                CLEAR()
                EDIT = False
            End If
            If GRIDINVOICE.RowCount = 0 And TEMPINVNO < MAXNO Then
                TXTINVOICENO.Text = TEMPINVNO
                GoTo LINE1
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub tstxtbillno_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tstxtbillno.Validating
        Try
            GRIDINVOICE.RowCount = 0
            TEMPINVNO = Val(tstxtbillno.Text)
            TEMPREGNAME = CMBREGISTER.Text.Trim
            If TEMPINVNO > 0 Then
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
                Dim objpur As New ClsSaleInvoice
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPINVNO)
                    alparaval.Add(TEMPREGNAME)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.Delete
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
            Dim objpurinv As New InvoiceDetails
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
        TOTAL()
    End Sub

    Private Sub txttax_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTTAX.Validated
        TOTAL()
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

    Private Sub GRIDQUOTE_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDINVOICE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDINVOICE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbitemname.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDINVOICE.Rows.RemoveAt(GRIDINVOICE.CurrentRow.Index)
                TOTAL()
                getsrno(GRIDINVOICE)
                TOTAL()

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

    Private Sub txtqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTQTY.KeyPress, TXTBOXES.KeyPress, TXTBALES.KeyPress, TXTCOURIERCHGS.KeyPress
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

                TEMPUPLOADROW = e.RowIndex
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
        GETHSNCODE()
        calc()
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
            If EDIT = True Then PRINTREPORT(TEMPINVNO)
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub INVDATE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles INVDATE.GotFocus
        INVDATE.SelectAll()
    End Sub

    Private Sub INVDATE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INVDATE.Validating
        Try
            If INVDATE.Text.Trim <> "__/__/____" Then
                'PARSING DATE FORMATS WHETHER THEY ARE PROPER OR NOT
                Dim TEMP As DateTime
                If Not DateTime.TryParse(INVDATE.Text, TEMP) Then
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
            If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If
            Cursor.Current = Cursors.WaitCursor

            Dim OBJSELECTCHALLAN As New SelectChallan
            Dim DTQUOTE As DataTable = OBJSELECTCHALLAN.DT
            OBJSELECTCHALLAN.ShowDialog()

            Dim i As Integer = 0
            If DTQUOTE.Rows.Count > 0 Then
                Dim objcmn As New ClsCommon
                For Each DTROW As DataRow In DTQUOTE.Rows
                    Dim dt As New DataTable
                    If ClientName = "GELATO" Then
                        dt = objcmn.search("   GDNGELATO.GDN_NO AS CHALLANNO, GDNGELATO.GDN_DATE AS CHALLANDATE, ISNULL(GDNGELATO_DESC.GDN_SRNO, 0) AS SRNO, ISNULL(GDNGELATO_DESC.GDN_DESIGNNO, '') AS DESIGNNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME, ISNULL(GDNGELATO.GDN_BOXES, 0) AS BOXES, ISNULL(GDNGELATO_DESC.GDN_QTY, 0) AS QTY, ISNULL(GDNGELATO_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDNGELATO_DESC.GDN_AMOUNT, 0) AS AMT, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GDNGELATO.GDN_REFNO, '') AS REFNO, ISNULL(TRANSLEDGERS.ACC_CMPNAME, '') AS TRANSNAME, ISNULL(AGENTLEDGERS.ACC_CMPNAME, '') AS AGENT, isNull(CAST(STATEMASTER.state_remark AS varchar(15)), '') AS STATECODE, ISNULL(LEDGERS.Acc_GSTIN, '') AS GSTIN, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER, ISNULL(HSNMASTER.HSN_RATEGREATERTHAN,0) AS HSNRATE, ISNULL(HSNMASTER.HSN_CGST1, 0) AS CGSTPER1, ISNULL(HSNMASTER.HSN_SGST1, 0) AS SGSTPER1, ISNULL(HSNMASTER.HSN_IGST1, 0) AS IGSTPER1, ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER", "", " GDNGELATO INNER JOIN GDNGELATO_DESC ON GDNGELATO.GDN_NO = GDNGELATO_DESC.GDN_NO AND GDNGELATO.GDN_YEARID = GDNGELATO_DESC.GDN_YEARID INNER JOIN ITEMMASTER ON GDNGELATO_DESC.GDN_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON GDNGELATO.GDN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS TRANSLEDGERS ON GDNGELATO.GDN_TRANSID = TRANSLEDGERS.Acc_id LEFT OUTER JOIN LEDGERS AS AGENTLEDGERS ON GDNGELATO.GDN_AGENTID = AGENTLEDGERS.Acc_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id ", " AND GDNGELATO.GDN_NO = " & Val(DTROW(0)) & " AND GDNGELATO.GDN_YEARID = " & YearId)
                        If dt.Rows.Count > 0 Then
                            TXTSTATECODE.Text = dt.Rows(0).Item("STATECODE")
                            TXTGSTIN.Text = dt.Rows(0).Item("GSTIN")
                            TXTCHALLANNO.Text = dt.Rows(0).Item("CHALLANNO")
                            CHALLANDATE.Text = dt.Rows(0).Item("CHALLANDATE")
                            CMBNAME.Text = dt.Rows(0).Item("NAME")
                            TXTREFNO.Text = dt.Rows(0).Item("REFNO")
                            TXTBALES.Text = Val(dt.Rows(0).Item("BOXES"))
                            CMBTRANSPORT.Text = dt.Rows(0).Item("TRANSNAME")
                            CMBAGENT.Text = dt.Rows(0).Item("AGENT")

                            For Each DR As DataRow In dt.Rows
                                If Val(TXTDISCPER.Text.Trim) > 0 And Val(DR("RATE")) > 0 And Val(DR("QTY")) > 0 Then TXTOTHERAMT.Text = Format(((Val(DR("AMT")) * Val(TXTDISCPER.Text.Trim)) / 100) * -1, "0.00")

                                If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                    If CHKEXPORTGST.CheckState = CheckState.Checked Then
                                        GRIDINVOICE.Rows.Add(0, DR("DESIGNNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), "", 0, 0, Format(Val(DR("QTY")), "0"), "PCS", Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), Val(TXTOTHERAMT.Text.Trim), 0, Format(Val(DR("AMT")), "0.00"), Val(DR("EXPCGSTPER")), "0.00", Val(DR("EXPSGSTPER")), "0.00", "0.00", "0.00")
                                    Else
                                        GRIDINVOICE.Rows.Add(0, DR("DESIGNNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), "", 0, 0, Format(Val(DR("QTY")), "0"), "PCS", Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), Val(TXTOTHERAMT.Text.Trim), 0, Format(Val(DR("AMT")), "0.00"), Val(DR("CGSTPER")), "0.00", Val(DR("SGSTPER")), "0.00", "0.00", "0.00")
                                    End If
                                Else
                                    If CHKEXPORTGST.CheckState = CheckState.Checked Then
                                        GRIDINVOICE.Rows.Add(0, DR("DESIGNNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), "", 0, 0, Format(Val(DR("QTY")), "0"), "PCS", Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), Val(TXTOTHERAMT.Text.Trim), 0, Format(Val(DR("AMT")), "0.00"), "0.00", "0.00", "0.00", "0.00", DR("EXPIGSTPER"), "0.00")
                                    Else
                                        GRIDINVOICE.Rows.Add(0, DR("DESIGNNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), "", 0, 0, Format(Val(DR("QTY")), "0"), "PCS", Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), Val(TXTOTHERAMT.Text.Trim), 0, Format(Val(DR("AMT")), "0.00"), "0.00", "0.00", "0.00", "0.00", DR("IGSTPER"), "0.00")
                                    End If
                                End If
                            Next

                            TOTAL()
                            getsrno(GRIDINVOICE)
                            CMDSELECTCHALLAN.Enabled = False
                            INVDATE.Focus()
                        End If

                    Else
                        dt = objcmn.search("   GDN.GDN_NO AS CHALLANNO, GDN.GDN_DATE AS CHALLANDATE, ISNULL(GDN_DESC.GDN_SRNO, 0) AS SRNO, ISNULL(GDN_DESC.GDN_PARTNO, '') AS PARTNO, ISNULL(ITEMMASTER.ITEM_name, '') AS ITEMNAME, ISNULL(GDN_DESC.GDN_MAKE, '') AS MAKE, ISNULL(GDN_DESC.GDN_QTY, 0) AS QTY, ISNULL(UNITMASTER.unit_abbr, '') AS UNIT, ISNULL(GDN_DESC.GDN_RATE, 0) AS RATE, ISNULL(GDN_DESC.GDN_AMOUNT, 0) AS AMT, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(GDN.GDN_REFNO, '') AS REFNO, ISNULL(GDN.GDN_DELIVERYTHROUGH, '') AS DELTHROUGH, ISNULL(GDN.GDN_DELIVERYDATE, '') AS DELDATE, ISNULL(GDN.GDN_VALIDTILL, '') AS VALIDTILL, ISNULL(GDN.GDN_DESPATCHEDTO, '') AS PACKING, ISNULL(GDN.GDN_LRNO, '') AS LRNO, ISNULL(GDN.GDN_LRDATE, '') AS LRDATE, ISNULL(GDN.GDN_PONO, '') AS PONO, ISNULL(GDN.GDN_PODATE, '') AS PODATE, ISNULL(GDN.GDN_QUOTENO, '') AS QUOTENO, ISNULL(TAXMASTER.tax_name, '') AS TAXNAME, ISNULL(GDN.GDN_TAXAMT, 0) AS TAXAMT, ISNULL(OTHERCHGS.Acc_cmpname, '') AS OTHERCHGS, ISNULL(GDN.GDN_OTHERCHGS, 0) AS OTHERCHGSAMT, ISNULL(EXTRAOTHERCHGS.Acc_cmpname, '') AS EXTRAOTHERCHGS, ISNULL(GDN.GDN_EXTRACHGS, '') AS EXTRAOTHERCHGSAMT, isNull(CAST(STATEMASTER.state_remark AS varchar(15)), '') AS STATECODE, ISNULL(LEDGERS.Acc_GSTIN, '') AS GSTIN, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER", "", " GDN INNER JOIN GDN_DESC ON GDN.GDN_NO = GDN_DESC.GDN_NO AND GDN.GDN_YEARID = GDN_DESC.GDN_YEARID INNER JOIN UNITMASTER ON UNITMASTER.unit_id = GDN_DESC.GDN_UNITID INNER JOIN ITEMMASTER ON GDN_DESC.GDN_ITEMID = ITEMMASTER.ITEM_id INNER JOIN LEDGERS ON GDN.GDN_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS EXTRAOTHERCHGS ON GDN.GDN_EXTRACHGSID = EXTRAOTHERCHGS.Acc_id LEFT OUTER JOIN TAXMASTER ON GDN.GDN_TAXID = TAXMASTER.tax_id LEFT OUTER JOIN LEDGERS AS OTHERCHGS ON GDN.GDN_OTHERCHGSID = OTHERCHGS.Acc_id ", " AND GDN.GDN_NO = " & Val(DTROW(0)) & " AND GDN.GDN_YEARID = " & YearId)

                        If dt.Rows.Count > 0 Then
                            TXTSTATECODE.Text = dt.Rows(0).Item("STATECODE")
                            TXTGSTIN.Text = dt.Rows(0).Item("GSTIN")
                            TXTCHALLANNO.Text = dt.Rows(0).Item("CHALLANNO")
                            'CHALLANDATE.Text = dt.Rows(0).Item("CHALLANDATE")
                            CHALLANDATE.Text = dt.Rows(0).Item("PODATE")
                            CMBNAME.Text = dt.Rows(0).Item("NAME")
                            TXTREFNO.Text = dt.Rows(0).Item("REFNO")
                            TXTDELTHROUGH.Text = dt.Rows(0).Item("DELTHROUGH")
                            DELIVERYDATE.Text = dt.Rows(0).Item("DELDATE")
                            'TXTDESPATCHEDTO.Text = dt.Rows(0).Item("DESPATCHEDTO")
                            CMBPACKING.Text = dt.Rows(0).Item("PACKING")
                            VALIDTILL.Text = dt.Rows(0).Item("VALIDTILL")
                            TXTLRNO.Text = dt.Rows(0).Item("LRNO")
                            LRDATE.Text = dt.Rows(0).Item("LRDATE")
                            TXTPONO.Text = dt.Rows(0).Item("PONO")
                            PODATE.Text = dt.Rows(0).Item("PODATE")
                            TXTQUOTENO.Text = dt.Rows(0).Item("QUOTENO")

                            CMBOTHERCHGS.Text = dt.Rows(0).Item("OTHERCHGS")

                            If dt.Rows(0).Item("OTHERCHGSAMT") > 0 Then
                                txtotherchg.Text = dt.Rows(0).Item("OTHERCHGSAMT")
                                cmbaddsub.Text = "Add"
                            Else
                                txtotherchg.Text = dt.Rows(0).Item("OTHERCHGSAMT") * (-1)
                                cmbaddsub.Text = "Sub."
                            End If

                            CMBTAX.Text = dt.Rows(0).Item("TAXNAME")
                            TXTTAX.Text = dt.Rows(0).Item("TAXAMT")
                            CMBEXTRACHGS.Text = dt.Rows(0).Item("EXTRAOTHERCHGS")
                            If dt.Rows(0).Item("EXTRAOTHERCHGSAMT") > 0 Then
                                TXTEXTRACHGS.Text = dt.Rows(0).Item("EXTRAOTHERCHGSAMT")
                                CMBEXTRAADDSUB.Text = "Add"
                            Else
                                TXTEXTRACHGS.Text = dt.Rows(0).Item("EXTRAOTHERCHGSAMT") * (-1)
                                CMBEXTRAADDSUB.Text = "Sub."
                            End If


                            ''NEHA
                            ' ''GRID VALUE
                            'For Each DR As DataRow In dt.Rows
                            '    GRIDQUOTE.Rows.Add(0, DR("PARTNO").ToString, DR("ITEMNAME").ToString, DR("MAKE").ToString, Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00").ToString)
                            'Next

                            For Each DR As DataRow In dt.Rows
                                If DR("ITEMNAME").ToString <> "" And Convert.ToDateTime(INVDATE.Text).Date >= "01/07/2017" Then
                                    If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                                        GRIDINVOICE.Rows.Add(0, DR("PARTNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), DR("MAKE").ToString, Format(Val(DR("BOXES")), "0"), Format(Val(DR("EACHBOXES")), "0"), Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), 0, 0, Format(Val(DR("AMT")), "0.00"), Val(DR("CGSTPER")), "0.00", Val(DR("SGSTPER")), "0.00", "0.00", "0.00")
                                    Else
                                        GRIDINVOICE.Rows.Add(0, DR("PARTNO").ToString, DR("ITEMNAME").ToString, DR("HSNCODE"), DR("MAKE").ToString, Format(Val(DR("BOXES")), "0"), Format(Val(DR("EACHBOXES")), "0"), Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00"), 0, 0, Format(Val(DR("AMT")), "0.00"), "0.00", "0.00", "0.00", "0.00", DR("IGSTPER"), "0.00")
                                    End If
                                    'GRIDQUOTE.Rows.Add(0, DR("PARTNO").ToString, DR("ITEMNAME").ToString, "", DR("MAKE").ToString, Format(Val(DR("QTY")), "0"), DR("UNIT"), Format(Val(DR("RATE")), "0.00").ToString, Format(Val(DR("AMT")), "0.00").ToString, "0.00", "0.00", "0.00", "0.00", "0.00", "0.00")
                                End If
                            Next

                            TOTAL()
                            getsrno(GRIDINVOICE)
                            CMDSELECTCHALLAN.Enabled = False
                            INVDATE.Focus()
                        End If
                    End If
                Next
            End If


        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
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

    Private Sub TXTEXTRACHGS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTEXTRACHGS.KeyPress, TXTEACHBOXES.KeyPress, txtotherchg.KeyPress, TXTRATE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub txtotherchg_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtotherchg.Validating, TXTEXTRACHGS.Validating
        TOTAL()
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
            If CMBREGISTER.Text.Trim = "" Then fillregister(CMBREGISTER, " and register_type = 'SALE'")

            Dim clscommon As New ClsCommon
            Dim dt As DataTable
            dt = clscommon.search(" register_name,register_id", "", " RegisterMaster ", " and register_default = 'True' and register_type = 'SALE' AND REGISTER_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                CMBREGISTER.Text = dt.Rows(0).Item(0).ToString
            End If
            GETMAX_INVOICE_NO()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbregister_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBREGISTER.Validating
        Try
            If CMBREGISTER.Text.Trim.Length > 0 And EDIT = False Then
                CLEAR()
                CMBREGISTER.Text = UCase(CMBREGISTER.Text)
                Dim clscommon As New ClsCommon
                Dim dt As DataTable
                dt = clscommon.search(" register_abbr, register_initials, register_id", "", " RegisterMaster", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " AND REGISTER_LOCATIONID = " & Locationid & " AND REGISTER_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    saleregabbr = dt.Rows(0).Item(0).ToString
                    salereginitial = dt.Rows(0).Item(1).ToString
                    saleregid = dt.Rows(0).Item(2)
                    GETMAX_INVOICE_NO()
                    CMBREGISTER.Enabled = False
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
            OBJRECPAY.PURBILLINITIALS = salereginitial & "-" & TEMPINVNO
            OBJRECPAY.SALEBILLINITIALS = salereginitial & "-" & TEMPINVNO
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
            TOTAL()
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

            If CMBITEMNAME.Text.Trim <> "" And Convert.ToDateTime(INVDATE.Text).Date >= "01/07/2017" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("  ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID AND HSNMASTER.HSN_YEARID = ITEMMASTER.item_yearid ", " AND ITEMMASTER.ITEM_NAME= '" & CMBITEMNAME.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTHSNCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If CMBNAME.Text.Trim <> "" Then
                        If TXTSTATECODE.Text.Trim = CMPSTATECODE Then
                            TXTIGSTPER.Text = 0
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then
                                TXTCGSTPER.Text = Val(DT.Rows(0).Item("CGSTPER"))
                                TXTSGSTPER.Text = Val(DT.Rows(0).Item("SGSTPER"))
                            Else
                                TXTCGSTPER.Text = Val(DT.Rows(0).Item("EXPCGSTPER"))
                                TXTSGSTPER.Text = Val(DT.Rows(0).Item("EXPSGSTPER"))
                            End If
                        Else
                            TXTCGSTPER.Text = 0
                            TXTSGSTPER.Text = 0
                            If CHKEXPORTGST.CheckState = CheckState.Unchecked Then
                                TXTIGSTPER.Text = Val(DT.Rows(0).Item("IGSTPER"))
                            Else
                                TXTIGSTPER.Text = Val(DT.Rows(0).Item("EXPIGSTPER"))
                            End If
                        End If
                    End If
                End If
                TOTAL()
            End If

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
                If CMBTOCITY.Text.Trim = "" Then CMBTOCITY.Text = DT.Rows(0).Item("CITYNAME")
            End If
            GETHSNCODE()
            If CMBPACKING.Text.Trim = "" Then CMBPACKING.Text = CMBNAME.Text.Trim
        End If
    End Sub

    Private Sub InvoiceMaster_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ALLOWEINVOICE = False Then TOOLEINV.Visible = False
            If ALLOWEWAYBILL = False Then TOOLEWB.Visible = False

            If ClientName = "SHAHTRADE" Or ClientName = "GELATO" Then

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

                BTNCOURIERCHGS.Left = BTNOTHERAMT.Left + BTNOTHERAMT.Width
                TXTCOURIERCHGS.Left = TXTOTHERAMT.Left + TXTOTHERAMT.Width
                LBLTOTALCOURIERCHGS.Left = TXTCOURIERCHGS.Left


                BTNTAXABLEAMT.Left = BTNCOURIERCHGS.Left + BTNCOURIERCHGS.Width
                TXTTAXABLEAMT.Left = TXTCOURIERCHGS.Left + TXTCOURIERCHGS.Width
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
                GRIDINVOICE.Width = 1930

                CMDSELECTCHALLAN.Visible = True
            End If

            If ClientName = "GELATO" Then
                LBLDELIVERY.Text = "Transport"
                CMBTRANSPORT.Visible = True
                TXTDELTHROUGH.Visible = False
                BTNPARTNO.Text = "Design No"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKEXPORTGST_CheckedChanged(sender As Object, e As EventArgs) Handles CHKEXPORTGST.CheckedChanged
        GETHSNCODE()
    End Sub

    Private Sub TOOLEWB_Click(sender As Object, e As EventArgs) Handles TOOLEWB.Click
        Try
            If EDIT = False Then Exit Sub
            GENERATEEWB()
            PRINTEWB()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GENERATEEWB()
        Try
            If ALLOWEWAYBILL = False Then Exit Sub
            If CMBNAME.Text.Trim = "" Then Exit Sub
            If EDIT = False Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 Then Exit Sub


            If TXTLRNO.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVDATE.Text).Date Then
                    MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Way Bill?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim <> "" Then
                MsgBox("E-Way Bill No Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim TRANSGSTIN As String = ""

            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_DISPATCHFROM, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2 ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")


            'PARTY GST DETAILS 
            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If


            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Or Val(DT.Rows(0).Item("KMS")) = 0 Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                    PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    PARTYADD1 = DT.Rows(0).Item("ADD1")
                    PARTYADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                End If
            End If


            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If CMBTRANSPORT.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & CMBTRANSPORT.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EWAY BILL WILL BE ALLOWED OR NOT, FOR EACH EWAY BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EWB)
            If CMPEWAYCOUNTER = 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EWAYCOUNTER
            Dim USEDEWAYCOUNTER As Integer = 0
            DT = OBJCMN.search("COUNT(COUNTERID) AS EWAYCOUNT", "", "EWAYENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER <= 0 Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EWAYEXPDATE Then
                MsgBox("EWay Bill Package has Expired, Kindly contact Nakoda Infotech on +919987603607", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE 1% THEN INTIMATE
            If CMPEWAYCOUNTER - USEDEWAYCOUNTER < Format((CMPEWAYCOUNTER * 0.01), "0") Then
                MsgBox("Only " & (CMPEWAYCOUNTER - USEDEWAYCOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of EWB Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EWAY BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.03/authenticate?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=05AAACG1539P1ZH&username=05AAACG1539P1ZH&ewbpwd=abc123@@")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/auth?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EWAYENTRY
            'DONT ADD IN EWAYENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVNO.TEXT.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If



            'GENERATING EWAY BILL 
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.03/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=05AAACG1539P1ZH&authtoken=" & TOKEN)
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GENEWAYBILL&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&authtoken=" & TOKEN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"


                Dim j As String = ""

                j = "{"
                j = j & """supplyType"":""O"","
                j = j & """subSupplyType"":""1"","
                j = j & """subSupplyDesc"":"""","
                j = j & """docType"":""INV"","

                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.search("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND INVOICE_YEARID = " & YearId)

                j = j & """docNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """docDate"":""" & INVDATE.Text & """" & ","
                j = j & """fromGstin"":""" & CMPGSTIN & """" & ","
                j = j & """fromTrdName"":""" & CmpName & """" & ","
                j = j & """fromAddr1"":""" & TEMPCMPADD1 & """" & ","
                j = j & """fromAddr2"":""" & TEMPCMPADD2 & """" & ","
                j = j & """fromPlace"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """fromPincode"":""" & CMPPINCODE & """" & ","
                j = j & """actFromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """fromStateCode"":""" & CMPSTATECODE & """" & ","
                j = j & """toGstin"":""" & PARTYGSTIN & """" & ","
                j = j & """toTrdName"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """toAddr1"":""" & PARTYADD1 & """" & ","
                j = j & """toAddr2"":""" & PARTYADD2 & """" & ","
                j = j & """toPlace"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """toPincode"":""" & PARTYPINCODE & """" & ","
                j = j & """actToStateCode"":""" & SHIPTOSTATECODE & """" & ","
                j = j & """toStateCode"":""" & PARTYSTATECODE & """" & ","

                j = j & """transactionType"":""4"","
                j = j & """dispatchFromGSTIN"":""" & CMPGSTIN & """" & ","
                j = j & """dispatchFromTradeName"":""" & CmpName & """" & ","
                j = j & """shipToGSTIN"":""" & SHIPTOGSTIN & """" & ","
                j = j & """shipToTradeName"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """otherValue"":""0"","



                j = j & """totalValue"":""" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & """" & ","
                j = j & """cgstValue"":""" & Val(LBLTOTALCGSTAMT.Text.Trim) & """" & ","
                j = j & """sgstValue"":""" & Val(LBLTOTALSGSTAMT.Text.Trim) & """" & ","
                j = j & """igstValue"":""" & Val(LBLTOTALIGSTAMT.Text.Trim) & """" & ","

                j = j & """cessValue"":""" & "0" & """" & ","
                j = j & """cessNonAdvolValue"":""" & "0" & """" & ","
                j = j & """totInvValue"":""" & Val(TXTGTOTAL.Text.Trim) & """" & ","
                j = j & """transporterId"":""" & TRANSGSTIN & """" & ","
                j = j & """transporterName"":""" & CMBTRANSPORT.Text.Trim & """" & ","


                If TXTVEHICLENO.Text.Trim = "" Then
                    j = j & """transDocNo"":"""","
                    j = j & """transMode"":"""","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":"""","
                    j = j & """vehicleType"":"""","
                Else
                    j = j & """transDocNo"":""" & TXTLRNO.Text.Trim & """" & ","
                    j = j & """transMode"":""" & CMBMODE.SelectedIndex + 1 & """" & ","
                    j = j & """transDistance"":""" & PARTYKMS & """" & ","
                    If LRDATE.Text <> "__/__/____" Then j = j & """transDocDate"":""" & LRDATE.Text & """" & "," Else j = j & """transDocDate"":"""","
                    j = j & """vehicleNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                    j = j & """vehicleType"":""" & "R" & """" & ","
                End If


                j = j & """itemList"":[{"



                'WE NEED TO FETCH SUMMARY OF ITEMS AND HSN TO PASS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ITEM_NAME AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, SUM(INVOICE_QTY) AS QTY, SUM(INVOICE_TAXABLEAMT)  AS TAXABLEAMT FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TEMPINVNO) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " GROUP BY item_name, ISNULL(HSN_CODE,''), ISNULL(HSN_CGST,0), ISNULL(HSN_SGST,0), ISNULL(HSN_IGST,0)  ", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows
                    If CURRROW > 0 Then j = j & ",{"
                    j = j & """productName"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """productDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """hsnCode"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """quantity"":""" & Val(DTROW("QTY")) & """" & ","
                    j = j & """qtyUnit"":""" & "NOS" & """" & ","
                    j = j & """cgstRate"":""" & Val(GRIDINVOICE.Item(GCGSTPER.Index, CURRROW).Value) & """" & ","
                    j = j & """sgstRate"":""" & Val(GRIDINVOICE.Item(GSGSTPER.Index, CURRROW).Value) & """" & ","
                    j = j & """igstRate"":""" & Val(GRIDINVOICE.Item(GIGSTPER.Index, CURRROW).Value) & """" & ","
                    j = j & """cessRate"":""" & "0" & """" & ","
                    'THIS CODE WAS IN V1.02
                    'j = j & """cessAdvol"":""" & "0" & """" & ","
                    j = j & """cessNonAdvol"":""" & "0" & """" & ","
                    j = j & """taxableAmount"":""" & Val(DTROW("TAXABLEAMT")) & """"
                    j = j & " }"
                    CURRROW += 1
                Next

                j = j & " ]}"

                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()


            Catch ex As WebException
                RESPONSE = ex.Response
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()

                Dim ERRORMSG As String = ""

                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("message") + Len("message") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("}", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
                MsgBox("Error While Generating EWB, " & ERRORMSG)



                Exit Sub
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()




            Dim EWBNO As String = ""

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ewayBillNo") + Len("ewayBillNo") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS)
            EWBNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            TXTEWAYBILLNO.Text = EWBNO

            'WE NEED TO UPDATE THIS EWBNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_EWAYBILLNO = '" & TXTEWAYBILLNO.Text.Trim & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TEMPINVNO) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EWAYENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & EWBNO & "','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTEWB()
        Try

            If PRINTEWAYBILL = False Then Exit Sub
            If EDIT = False Then Exit Sub
            If TXTEWAYBILLNO.Text.Trim = "" Then Exit Sub


            If MsgBox("Print EWB?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            Dim TOKENNO As String = ""
            Dim EWBNO As String = ""

            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(TOKENNO, '') AS TOKENNO, ISNULL(EWBNO, '') AS EWBNO ", "", " EWAYENTRY ", " AND EWBNO = '" & TXTEWAYBILLNO.Text.Trim & "' And YearId = " & YearId)
            If DT.Rows.Count = 0 Then Exit Sub
            TOKENNO = DT.Rows(0).Item("TOKENNO")
            EWBNO = DT.Rows(0).Item("EWBNO")

            'Dim URL As New Uri("https://einvapi.charteredinfo.com/v1.03/dec/authenticate?action=ACCESSTOKEN&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&username=" & CMPEWBUSER & "&ewbpwd=" & CMPEWBPASS)
            Dim URL As New Uri("http://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GetEwayBill&aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&authtoken=" & TOKENNO & "&ewbNo=" & EWBNO)


            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)
            REQUEST.Method = "Get"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()
            Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(REQUESTEDTEXT)

            Dim FURL As New Uri("https://einvapi.charteredinfo.com/aspapi/v1.0/printewb?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN)
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/x-www-form-urlencoded"
                REQUEST.ContentLength = buffer.Length

                Dim stream As Stream = REQUEST.GetRequestStream()
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()
                Dim STRREADER As Stream = RESPONSE.GetResponseStream()
                Dim BINREADER As New BinaryReader(STRREADER)
                Dim BFFER As Byte() = BINREADER.ReadBytes(CInt(RESPONSE.ContentLength))
                File.WriteAllBytes(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf", BFFER)
                Process.Start(Application.StartupPath & "\EWB_" & TXTEWAYBILLNO.Text.Trim & ".pdf")

                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT SUCCESS2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            Catch ex As WebException
                RESPONSE = ex.Response
                MsgBox("Error While Printing EWB, Please check the Data Properly")
                'ADD DATA IN EWAYENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED1', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EWAYENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKENNO & "','" & EWBNO & "','PRINT FAILED2', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBFROMCITY.Enter
        Try
            If CMBFROMCITY.Text.Trim = "" Then fillCITY(CMBFROMCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBTOCITY.Enter
        Try
            If CMBTOCITY.Text.Trim = "" Then fillCITY(CMBTOCITY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTRANSCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCITY.Validating
        Try
            If CMBFROMCITY.Text.Trim <> "" Then CITYVALIDATE(CMBFROMCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBTOCITY_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTOCITY.Validating
        Try
            If CMBTOCITY.Text.Trim <> "" Then CITYVALIDATE(CMBTOCITY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Enter
        Try
            If CMBPACKING.Text.Trim = "" Then fillname(CMBPACKING, EDIT, " And (GROUP_SECONDARY = 'SUNDRY DEBTORS' OR GROUP_SECONDARY = 'SUNDRY CREDITORS')   AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPACKING_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPACKING.Validated
        Try
            If CMBPACKING.Text.Trim <> "" Then
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS_1.ACC_CMPNAME,'') AS TRANSNAME, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN, ISNULL(LEDGERS.ACC_DISC,0) AS DISCPER, isnull(LEDGERS.ACC_CRDAYS,0) AS CRDAYS, ISNULL(LEDGERS.ACC_MOBILE,'') AS MOBILENO, ISNULL(LEDGERS.ACC_AGENTCOMM,'') AS AGENTCOMM, ISNULL(CITYMASTER.CITY_NAME,'') AS CITYNAME ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id LEFT OUTER JOIN CITYMASTER ON LEDGERS.ACC_CITYID = CITY_ID ", " and LEDGERS.acc_cmpname = '" & CMBPACKING.Text.Trim & "' and (GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'  OR GROUP_SECONDARY = 'SUNDRY CREDITORS') and LEDGERS.acc_YEARid = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("TRANSNAME") <> "" And EDIT = False Then CMBTRANSPORT.Text = DT.Rows(0).Item("TRANSNAME")
                    If DT.Rows(0).Item("CITYNAME") <> "" And EDIT = False Then CMBTOCITY.Text = DT.Rows(0).Item("CITYNAME")
                End If
            End If
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

    Private Sub TOOLEINV_Click(sender As Object, e As EventArgs) Handles TOOLEINV.Click
        Try
            If EDIT = False Then Exit Sub
            GENERATEINV()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Async Sub GENERATEINV()
        Try
            If ALLOWEINVOICE = False Then Exit Sub
            If CMBNAME.Text.Trim = "" Then Exit Sub

            If Val(LBLTOTALCGSTAMT.Text.Trim) = 0 And Val(LBLTOTALSGSTAMT.Text.Trim) = 0 And Val(LBLTOTALIGSTAMT.Text.Trim) = 0 Then Exit Sub


            If TXTLRNO.Text.Trim <> "" AndAlso LRDATE.Text <> "__/__/____" Then
                If Convert.ToDateTime(LRDATE.Text).Date < Convert.ToDateTime(INVDATE.Text).Date Then
                    MsgBox("LR Date cannot be Before Invoice Date", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If CMBFROMCITY.Text.Trim = "" Then
                MsgBox("Enter From City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If CMBTOCITY.Text.Trim = "" Then
                MsgBox("Enter to City", MsgBoxStyle.Critical)
                Exit Sub
            End If

            If MsgBox("Generate E-Invoice?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            If TXTIRNNO.Text.Trim <> "" Then
                MsgBox("E-Invoice Already Generated", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'BEFORE GENERATING EWAY BILL WE NEED TO VALIDATE WHETHER ALL THE DATA ARE PRESENT OR NOT
            'IF DATA IS NOT PRESENT THEN VALIDATE
            'DATA TO BE CHECKED 
            '   1)CMPEWBUSER | CMPEWBPASS | CMPGSTIN | CMPPINCODE | CMPCITY | CMPSTATE | 
            '   2)PARTYGSTIN | PARTYCITY | PARTYPINCODE | PARTYSTATE | PARTYSTATECODE | PARTYKMS
            '   3)CGST OR SGST OR IGST (ALWAYS USE MTR IN QTYUNIT)
            If CMPEWBUSER = "" Or CMPEWBPASS = "" Or CMPGSTIN = "" Or CMPPINCODE = "" Or CMPCITYNAME = "" Or CMPSTATENAME = "" Then
                MsgBox(" Company Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim TEMPCMPADD1 As String = ""
            Dim TEMPCMPADD2 As String = ""
            Dim TEMPCMPDISPATCHADD1 As String = ""
            Dim PARTYGSTIN As String = ""
            Dim PARTYPINCODE As String = ""
            Dim PARTYSTATECODE As String = ""
            Dim PARTYSTATENAME As String = ""
            Dim SHIPTOGSTIN As String = ""
            Dim SHIPTOSTATECODE As String = ""
            Dim SHIPTOSTATENAME As String = ""
            Dim SHIPTOPINCODE As String = ""
            Dim PARTYKMS As Double = 0
            Dim PARTYADD1 As String = ""
            Dim PARTYADD2 As String = ""
            Dim SHIPTOADD1 As String = ""
            Dim SHIPTOADD2 As String = ""
            Dim TRANSGSTIN As String = ""
            Dim DISPATCHFROM As String = ""
            Dim DISPATCHFROMGSTIN As String = ""
            Dim DISPATCHFROMPINCODE As String = ""
            Dim DISPATCHFROMSTATECODE As String = ""
            Dim DISPATCHFROMSTATENAME As String = ""
            Dim DISPATCHFROMKMS As Double = 0
            Dim DISPATCHFROMADD1 As String = ""
            Dim DISPATCHFROMADD2 As String = ""


            Dim OBJCMN As New ClsCommon
            'CMP ADDRESS DETAILS
            Dim DT As DataTable = OBJCMN.search(" ISNULL(CMP_ADD1, '') AS ADD1, ISNULL(CMP_ADD2,'') AS ADD2, ISNULL(CMP_DISPATCHFROM, '') AS DISPATCHADD ", "", " CMPMASTER ", " AND CMP_ID = " & CmpId)
            TEMPCMPADD1 = DT.Rows(0).Item("ADD1")
            TEMPCMPADD2 = DT.Rows(0).Item("ADD2")
            TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("DISPATCHADD")
            DISPATCHFROM = CmpName
            DISPATCHFROMGSTIN = CMPGSTIN
            DISPATCHFROMPINCODE = CMPPINCODE
            DISPATCHFROMSTATECODE = CMPSTATECODE
            DISPATCHFROMSTATENAME = CMPSTATENAME

            'PARTY GST DETAILS 

            DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBNAME.Text.Trim & "' AND ACC_YEARID = " & YearId)
            If (DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "") And CHKEXPORTGST.Checked = False Then
                MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                Exit Sub
            Else
                PARTYGSTIN = DT.Rows(0).Item("GSTIN")
                SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                PARTYSTATENAME = DT.Rows(0).Item("STATENAME")
                PARTYSTATECODE = DT.Rows(0).Item("STATECODE")
                SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                PARTYPINCODE = DT.Rows(0).Item("PINCODE")
                'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                PARTYADD1 = DT.Rows(0).Item("ADD1")
                PARTYADD2 = DT.Rows(0).Item("ADD2")
            End If

            If CHKEXPORTGST.Checked = True Then
                PARTYGSTIN = "URP"
                SHIPTOGSTIN = "URP"
                PARTYSTATECODE = "96"
                SHIPTOSTATECODE = "96"
                PARTYPINCODE = "999999"
            End If



            'FETCH PINCODE / KMS / ADD1 / ADD2 OF SHIPTO IF IT IS NOT SAME AS CMBNAME
            If CMBPACKING.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBPACKING.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN,  ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_RANGE,'') AS KOTHARIPLACE ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBPACKING.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("PINCODE") = "" Then
                    MsgBox(" Party Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    SHIPTOGSTIN = DT.Rows(0).Item("GSTIN")
                    SHIPTOPINCODE = DT.Rows(0).Item("PINCODE")
                    'PARTYKMS = Val(DT.Rows(0).Item("KMS"))
                    SHIPTOADD1 = DT.Rows(0).Item("ADD1")
                    SHIPTOADD2 = DT.Rows(0).Item("ADD2")
                    SHIPTOSTATENAME = DT.Rows(0).Item("STATENAME")
                    SHIPTOSTATECODE = DT.Rows(0).Item("STATECODE")
                    'KOTHARIPLACE = DT.Rows(0).Item("KOTHARIPLACE")
                End If
            End If


            If CHKEXPORTGST.Checked = True Then
                SHIPTOGSTIN = "URP"
                SHIPTOPINCODE = "999999"
            End If



            'DISPATCHFROM GST DETAILS AND KMS WILL BE FETCHED FROM TXTKMS
            If CMBDISPATCHFROM.Text.Trim <> "" AndAlso CMBNAME.Text.Trim <> CMBDISPATCHFROM.Text.Trim Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN, ISNULL(ACC_ZIPCODE,'') AS PINCODE, ISNULL(STATE_NAME,'') AS STATENAME, ISNULL(CAST(STATE_REMARK AS VARCHAR(20)),'') AS STATECODE, ISNULL(ACC_KMS,0) AS KMS, ISNULL(ACC_ADD1,'') AS ADD1, ISNULL(ACC_ADD2,'') AS ADD2 ", "", " LEDGERS LEFT OUTER JOIN STATEMASTER ON ACC_STATEID = STATE_ID ", " AND ACC_CMPNAME = '" & CMBDISPATCHFROM.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("GSTIN") = "" Or DT.Rows(0).Item("PINCODE") = "" Or DT.Rows(0).Item("STATENAME") = "" Or DT.Rows(0).Item("STATECODE") = "" Then
                    MsgBox(" Dispatch From Details are not filled properly ", MsgBoxStyle.Critical)
                    Exit Sub
                Else
                    DISPATCHFROM = CMBDISPATCHFROM.Text.Trim
                    DISPATCHFROMGSTIN = DT.Rows(0).Item("GSTIN")
                    DISPATCHFROMSTATENAME = DT.Rows(0).Item("STATENAME")
                    DISPATCHFROMSTATECODE = DT.Rows(0).Item("STATECODE")
                    DISPATCHFROMPINCODE = DT.Rows(0).Item("PINCODE")
                    'DISPATCHFROMKMS = Val(TXTKMS.Text.Trim)
                    TEMPCMPDISPATCHADD1 = DT.Rows(0).Item("ADD1")
                    TEMPCMPADD2 = "ADD2" 'DT.Rows(0).Item("ADD2")
                End If
            End If



            'TRANSPORT GSTING IS NOT MANDATORY
            'FOR LOCAL TRANSPORT THERE IS NO GSTIN
            'TRANSPORT GSTIN IF TRANSPORT IS PRESENT
            If CMBTRANSPORT.Text.Trim <> "" Then
                DT = OBJCMN.search(" ISNULL(ACC_GSTIN, '') AS GSTIN ", "", " LEDGERS ", " AND ACC_CMPNAME = '" & CMBTRANSPORT.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then TRANSGSTIN = DT.Rows(0).Item("GSTIN")
                'FOR LOCAL TRANSPORT THERE IS NO GSTIN
                'If TRANSGSTIN = "" Then
                '    MsgBox("Enter Transport GSTIN", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
            End If



            'CHECKING COUNTER AND VALIDATE WHETHER EINVOICE WILL BE ALLOWED OR NOT, FOR EACH EINVOICE BILL WE NEED TO 2 API COUNTS (1 FOR TOKEN AND ANOTHER FOR EINVOICE)
            If CMPEINVOICECOUNTER = 0 Then
                MsgBox("E-Invoice Bill Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'GET USED EINVOICECOUNTER
            Dim USEDEINVOICECOUNTER As Integer = 0
            DT = OBJCMN.search("COUNT(COUNTERID) AS EINVOICECOUNT", "", "EINVOICEENTRY", " AND CMPID =" & CmpId)
            If DT.Rows.Count > 0 Then USEDEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNT"))

            'IF COUNTERS ARE FINISJED
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER <= 0 Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF DATE HAS EXPIRED
            If Now.Date > EINVOICEEXPDATE Then
                MsgBox("E-Invoice Package has Expired, Kindly contact Nakoda Infotech on 02249724411", MsgBoxStyle.Critical)
                Exit Sub
            End If

            'IF BALANCECOUNTERS ARE .10 THEN INTIMATE
            If CMPEINVOICECOUNTER - USEDEINVOICECOUNTER < Format((CMPEINVOICECOUNTER * 0.1), "0") Then
                MsgBox("Only " & (CMPEINVOICECOUNTER - USEDEINVOICECOUNTER) & " API's Left, Kindly contact Nakoda Infotech for Renewal of E-Invoice Package", MsgBoxStyle.Critical)
            End If


            'FOR GENERATING EINVOICE BILL WE NEED TO FIRST GENERATE THE TOKEN
            'THIS IS FOR SANDBOX TEST
            'Dim URL As New Uri("http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&user_name=TaxProEnvPON&eInvPwd=abc34*")
            Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim REQUEST As WebRequest
            Dim RESPONSE As WebResponse
            REQUEST = WebRequest.CreateDefault(URL)

            REQUEST.Method = "GET"
            Try
                RESPONSE = REQUEST.GetResponse()
            Catch ex As WebException
                RESPONSE = ex.Response
            End Try
            Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
            Dim REQUESTEDTEXT As String = READER.ReadToEnd()

            'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
            Dim STARTPOS As Integer = 0
            Dim TEMPSTATUS As String = ""
            Dim TOKEN As String = ""
            Dim ENDPOS As Integer = 0

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
            TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            'ADD DATA IN EINVOICEENTRY
            'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
            'IF STATUS IS FAILED THEN ERROR MESSAGE
            If TEMPSTATUS = "FAILED" Then
                MsgBox("Unable to create E-Invoice", MsgBoxStyle.Critical)
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                Exit Sub
            End If

            Dim j As String = ""
            Dim PRINTINITIALS As String = ""

            'GENERATING EINVOICE
            'FOR SANBOX TEST
            'Dim FURL As New Uri("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=34AACCC1596Q002&AuthToken=" & TOKEN & "&user_name=TaxProEnvPON&QrCodeSize=250")
            Dim FURL As New Uri("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&AuthToken=" & TOKEN & "&user_name=" & CMPEWBUSER & "&QrCodeSize=250")
            REQUEST = WebRequest.CreateDefault(FURL)
            REQUEST.Method = "POST"
            Try
                REQUEST.ContentType = "application/json"



                j = "{"
                j = j & """Version"": ""1.1"","
                j = j & """TranDtls"": {"
                j = j & """TaxSch"":""GST"","
                If CHKEXPORTGST.Checked = True Then j = j & """SupTyp"":""EXPWP""," Else j = j & """SupTyp"":""B2B"","
                j = j & """RegRev"":""N"","
                j = j & """IgstOnIntra"":""N""},"



                'WE NEED TO FETCH INITIALS INSTEAD OF BILLNO
                Dim DTINI As DataTable = OBJCMN.search("INVOICE_PRINTINITIALS AS PRINTINITIALS", "", "INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID", " AND INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND INVOICE_YEARID = " & YearId)
                PRINTINITIALS = DTINI.Rows(0).Item("PRINTINITIALS")

                j = j & """DocDtls"": {"
                j = j & """Typ"":""INV"","
                j = j & """No"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """Dt"":""" & INVDATE.Text & """" & "},"


                'For WORKING ON SANDBOX
                'CMPGSTIN = "34AACCC1596Q002"
                'CMPPINCODE = "605001"
                'CMPSTATECODE = "34"


                j = j & """SellerDtls"": {"
                j = j & """Gstin"":""" & CMPGSTIN & """" & ","
                j = j & """LglNm"":""" & CmpName & """" & ","
                j = j & """TrdNm"":""" & CmpName & """" & ","
                j = j & """Addr1"":""" & TEMPCMPADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMPCITYNAME & """" & "," 'CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & CMPPINCODE & "" & ","
                j = j & """Stcd"":""" & CMPSTATECODE & """" & "},"

                If PARTYADD1 = "" Then PARTYADD1 = PARTYSTATENAME
                If PARTYADD2 = "" Then PARTYADD2 = PARTYSTATENAME

                j = j & """BuyerDtls"": {"
                j = j & """Gstin"":""" & PARTYGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBNAME.Text.Trim & """" & ","
                j = j & """Pos"":""" & PARTYSTATECODE & """" & ","
                j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & PARTYADD2.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & PARTYPINCODE & "" & ","
                j = j & """Stcd"":""" & PARTYSTATECODE & """" & "},"


                j = j & """DispDtls"": {"
                j = j & """Nm"":""" & DISPATCHFROM & """" & ","
                j = j & """Addr1"":""" & TEMPCMPDISPATCHADD1.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Addr2"":""" & TEMPCMPADD2.Trim.Replace(vbCrLf, " ") & """" & ","
                j = j & """Loc"":""" & CMBFROMCITY.Text.Trim & """" & ","
                j = j & """Pin"":" & DISPATCHFROMPINCODE & "" & ","
                j = j & """Stcd"":""" & DISPATCHFROMSTATECODE & """" & "},"

                j = j & """ShipDtls"": {"
                If SHIPTOGSTIN <> "" Then j = j & """Gstin"":""" & SHIPTOGSTIN & """" & ","
                j = j & """LglNm"":""" & CMBPACKING.Text.Trim & """" & ","
                j = j & """TrdNm"":""" & CMBPACKING.Text.Trim & """" & ","
                If SHIPTOADD1 = "" Then j = j & """Addr1"":""" & PARTYADD1.Replace(vbCrLf, " ") & """" & "," Else j = j & """Addr1"":""" & SHIPTOADD1.Replace(vbCrLf, " ") & """" & ","
                If SHIPTOADD2 = "" Then SHIPTOADD2 = " ADDRESS2 "
                j = j & """Addr2"":""" & SHIPTOADD2 & """" & ","
                j = j & """Loc"":""" & CMBTOCITY.Text.Trim & """" & ","
                If SHIPTOPINCODE = "" Then j = j & """Pin"":" & PARTYPINCODE & "" & "," Else j = j & """Pin"":" & SHIPTOPINCODE & "" & ","
                j = j & """Stcd"":""" & SHIPTOSTATECODE & """" & "},"


                j = j & """ItemList"":[{"



                Dim TEMPLINEDISC As Double = 0
                Dim TEMPLINETAXABLEAMT As Double = 0
                Dim TEMPLINECGSTAMT As Double = 0
                Dim TEMPLINESGSTAMT As Double = 0
                Dim TEMPLINEIGSTAMT As Double = 0
                Dim TEMPLINEGRIDTOTALAMT As Double = 0
                Dim TEMPLINECHARGES As Double = 0
                Dim TEMPRATE As Double = 0
                If (Val(LBLTOTALOTHERAMT.Text.Trim) + Val(LBLTOTALCOURIERCHGS.Text.Trim)) < 0 Then
                    TEMPLINEDISC = (Val(LBLTOTALOTHERAMT.Text.Trim) + Val(LBLTOTALCOURIERCHGS.Text.Trim)) / Val(LBLTOTALQTY.Text.Trim)
                Else
                    TEMPRATE = (Val(LBLTOTALOTHERAMT.Text.Trim) + Val(LBLTOTALCOURIERCHGS.Text.Trim)) / Val(LBLTOTALQTY.Text.Trim)
                End If


                Dim TEMPTOTALAMT As Double = 0
                Dim TEMPTOTALDISC As Double = 0
                Dim TEMPTOTALTAXABLEAMT As Double = 0
                Dim TEMPTOTALCGSTAMT As Double = 0
                Dim TEMPTOTALSGSTAMT As Double = 0
                Dim TEMPTOTALIGSTAMT As Double = 0
                Dim TEMPGTOTALAMT As Double = 0


                'WE NEED TO FETCH ALL ITEMS HERE
                'FETCH FROM DESC TABLE 
                DT = OBJCMN.Execute_Any_String(" SELECT ISNULL(INVOICEMASTER_DESC.INVOICE_SRNO,0) AS SRNO, ISNULL(ITEMMASTER.item_name,'') AS ITEMNAME, ISNULL(HSN_CODE,'') AS HSNCODE, ISNULL(HSN_CGST,0) AS CGST, ISNULL(HSN_SGST,0) AS SGST, ISNULL(HSN_IGST,0) AS IGST, ISNULL(INVOICEMASTER_DESC.INVOICE_QTY,0) AS PCS, ISNULL(INVOICEMASTER_DESC.INVOICE_RATE,0) AS RATE, ISNULL(INVOICEMASTER_DESC.INVOICE_AMOUNT,0) AS TOTALAMT, (ISNULL(INVOICEMASTER_DESC.INVOICE_OTHERAMT,0)+ISNULL(INVOICEMASTER_DESC.INVOICE_COURIERCHGS,0)) AS LINEOTHERAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT,0) AS LINETAXABLEAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT,0) AS LINECGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT,0) AS LINESGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTAMT,0) AS LINEIGSTAMT, ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL,0) AS LINEGRIDTOTAL, ISNULL(HSN_TYPE,'Goods') HSNTYPE, ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTPER,0) AS LINECGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTPER,0) AS LINESGSTPER, ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTPER,0) AS LINEIGSTPER FROM INVOICEMASTER INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN ITEMMASTER ON item_id = INVOICE_ITEMID INNER JOIN HSNMASTER ON HSN_ID = INVOICE_HSNCODEID INNER JOIN REGISTERMASTER ON INVOICEMASTER.INVOICE_REGISTERID = REGISTER_ID WHERE INVOICEMASTER.INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and INVOICEMASTER.INVOICE_YEARID = " & YearId & " ORDER BY INVOICEMASTER_DESC.INVOICE_SRNO", "", "")
                Dim CURRROW As Integer = 0
                For Each DTROW As DataRow In DT.Rows

                    'TEMPLINECHARGES = Format(Val(TEMPLINEDISC) * Val(DTROW("PCS")), "0.0000")
                    'TEMPLINETAXABLEAMT = Format(Val(DTROW("TOTALAMT")) + Val(TEMPLINECHARGES), "0.00")
                    'TEMPLINECGSTAMT = Format(Val(DTROW("LINECGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    'TEMPLINESGSTAMT = Format(Val(DTROW("LINESGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    'TEMPLINEIGSTAMT = Format(Val(DTROW("LINEIGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    'TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                    If CURRROW > 0 Then j = j & ", {"
                    j = j & """SlNo"": """ & DTROW("SRNO") & """" & ","
                    j = j & """PrdDesc"":""" & DTROW("ITEMNAME") & """" & ","
                    If DTROW("HSNTYPE") = "Goods" Then j = j & """IsServc"":""" & "N" & """" & "," Else j = j & """IsServc"":""" & "Y" & """" & ","
                    j = j & """HsnCd"":""" & DTROW("HSNCODE") & """" & ","
                    j = j & """Barcde"":""REC9999"","
                    j = j & """Qty"":" & Val(DTROW("PCS")) & "" & ","
                    j = j & """FreeQty"":" & "0" & "" & ","
                    j = j & """Unit"":""" & "PCS" & """" & ","


                    Dim DTHSN As DataTable = OBJCMN.search(" TOP 1 ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_CGST, 0) AS CGSTPER, ISNULL(HSNMASTER.HSN_SGST, 0) AS SGSTPER, ISNULL(HSNMASTER.HSN_IGST, 0) AS IGSTPER,  ISNULL(HSNMASTER.HSN_EXPCGST, 0) AS EXPCGSTPER, ISNULL(HSNMASTER.HSN_EXPSGST, 0) AS EXPSGSTPER, ISNULL(HSNMASTER.HSN_EXPIGST, 0) AS EXPIGSTPER ", "", "HSNMASTER INNER JOIN ITEMMASTER ON HSNMASTER.HSN_ID = ITEMMASTER.ITEM_HSNCODEID ", " AND ITEMMASTER.ITEM_NAME= '" & DTROW("ITEMNAME") & "' AND HSNMASTER.HSN_YEARID=" & YearId)


                    TEMPRATE = Val(DTROW("RATE")) + (Val(DTROW("LINEOTHERAMT")) / Val(DTROW("PCS")))


                    j = j & """UnitPrice"":" & Format(Val(TEMPRATE), "0.00") & "" & ","
                    j = j & """TotAmt"":" & Format(Val(TEMPRATE) * Val(DTROW("PCS")), "0.00") & "" & ","

                    'If Val(DTROW("LINEOTHERAMT")) < 0 Then j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") * -1 & "" & "," Else j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") & "" & ","
                    j = j & """Discount"":" & "0" & "" & ","
                    j = j & """PreTaxVal"":" & "1" & "" & ","
                    TEMPLINETAXABLEAMT = Format(Val(TEMPRATE) * Val(DTROW("PCS")), "0.00")
                    j = j & """AssAmt"":" & Format(Val(TEMPLINETAXABLEAMT), "0.00") & "" & ","

                    If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","

                    If Val(DTROW("LINEIGSTAMT")) > 0 Then
                        j = j & """IgstAmt"":" & Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("IGSTPER")) / 100, "0.00") & "" & ","
                        j = j & """CgstAmt"":" & "0" & "" & ","
                        j = j & """SgstAmt"":" & "0" & "" & ","
                        TEMPLINEIGSTAMT = Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("IGSTPER")) / 100, "0.00")
                        TEMPLINECGSTAMT = 0
                        TEMPLINESGSTAMT = 0
                    Else
                        j = j & """IgstAmt"":" & "0" & "" & ","
                        j = j & """CgstAmt"":" & Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("CGSTPER")) / 100, "0.00") & "" & ","
                        j = j & """SgstAmt"":" & Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("SGSTPER")) / 100, "0.00") & "" & ","
                        TEMPLINEIGSTAMT = 0
                        TEMPLINECGSTAMT = Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("CGSTPER")) / 100, "0.00")
                        TEMPLINESGSTAMT = Format(Val(TEMPLINETAXABLEAMT) * Val(DTHSN.Rows(0).Item("SGSTPER")) / 100, "0.00")
                    End If
                    TEMPLINEGRIDTOTALAMT = Val(TEMPLINETAXABLEAMT) + Val(TEMPLINECGSTAMT) + Val(TEMPLINESGSTAMT) + Val(TEMPLINEIGSTAMT)

                    j = j & """CesRt"":" & "0" & "" & ","
                    j = j & """CesAmt"":" & "0" & "" & ","
                    j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                    j = j & """StateCesRt"":" & "0" & "" & ","
                    j = j & """StateCesAmt"":" & "0" & "" & ","
                    j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                    j = j & """OthChrg"":" & "0" & "" & ","

                    j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                    j = j & """OrdLineRef"":"" "","
                    j = j & """OrgCntry"":""IN"","
                    j = j & """PrdSlNo"":""123"","

                    j = j & """BchDtls"": {"
                    j = j & """Nm"":""123"","
                    j = j & """Expdt"":""" & INVDATE.Text & """" & ","
                    j = j & """wrDt"":""" & INVDATE.Text & """" & "},"

                    j = j & """AttribDtls"": [{"
                    j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                    j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"


                    'If Val(LBLTOTALOTHERAMT.Text.Trim) + Val(LBLTOTALCOURIERCHGS.Text.Trim) < 0 Then

                    '    j = j & """UnitPrice"":" & Val(DTROW("RATE")) & "" & ","
                    '    j = j & """TotAmt"":" & Format(Val(DTROW("TOTALAMT")), "0.00") & "" & ","

                    '    If Val(TEMPLINECHARGES) < 0 Then j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") * -1 & "" & "," Else j = j & """Discount"":" & Format(Val(TEMPLINECHARGES), "0.00") & "" & ","
                    '    j = j & """PreTaxVal"":" & "1" & "" & ","

                    '    j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                    '    If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","

                    '    If Val(LBLTOTALIGSTAMT.Text.Trim) > 0 Then
                    '        j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                    '        j = j & """CgstAmt"":" & "0" & "" & ","
                    '        j = j & """SgstAmt"":" & "0" & "" & ","
                    '    Else
                    '        j = j & """IgstAmt"":" & "0" & "" & ","
                    '        j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                    '        j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                    '    End If

                    '    j = j & """CesRt"":" & "0" & "" & ","
                    '    j = j & """CesAmt"":" & "0" & "" & ","
                    '    j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                    '    j = j & """StateCesRt"":" & "0" & "" & ","
                    '    j = j & """StateCesAmt"":" & "0" & "" & ","
                    '    j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                    '    j = j & """OthChrg"":" & "0" & "" & ","

                    '    j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                    '    j = j & """OrdLineRef"":"" "","
                    '    j = j & """OrgCntry"":""IN"","
                    '    j = j & """PrdSlNo"":""123"","

                    '    j = j & """BchDtls"": {"
                    '    j = j & """Nm"":""123"","
                    '    j = j & """Expdt"":""" & INVDATE.Text & """" & ","
                    '    j = j & """wrDt"":""" & INVDATE.Text & """" & "},"

                    '    j = j & """AttribDtls"": [{"
                    '    j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                    '    j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"

                    'Else


                    '    j = j & """UnitPrice"":" & Format(Val(DTROW("RATE")) + TEMPRATE, "0.00") & "" & ","
                    '    TEMPLINETAXABLEAMT = Format(Val(Val(DTROW("RATE")) + TEMPRATE) * Val(DTROW("PCS")), "0.00")

                    '    If Val(LBLTOTALIGSTAMT.Text.Trim) > 0 Then
                    '        TEMPLINECGSTAMT = 0
                    '        TEMPLINESGSTAMT = 0
                    '        TEMPLINEIGSTAMT = Format(Val(DTHSN.Rows(0).Item("IGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    '    Else
                    '        TEMPLINECGSTAMT = Format(Val(DTHSN.Rows(0).Item("CGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    '        TEMPLINESGSTAMT = Format(Val(DTHSN.Rows(0).Item("SGSTPER")) * Val(TEMPLINETAXABLEAMT) / 100, "0.00")
                    '        TEMPLINEIGSTAMT = 0
                    '    End If
                    '    TEMPLINEGRIDTOTALAMT = Format(Val(TEMPLINETAXABLEAMT + TEMPLINECGSTAMT + TEMPLINESGSTAMT + TEMPLINEIGSTAMT), "0.00")

                    '    j = j & """TotAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                    '    j = j & """Discount"":" & "0" & "" & ","
                    '    j = j & """PreTaxVal"":" & "1" & "" & ","

                    '    j = j & """AssAmt"":" & Val(TEMPLINETAXABLEAMT) & "" & ","
                    '    If CHKEXPORTGST.CheckState = CheckState.Unchecked Then j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("IGSTPER")) & "" & "," Else j = j & """GstRt"":" & Val(DTHSN.Rows(0).Item("EXPIGSTPER")) & "" & ","

                    '    If Val(LBLTOTALIGSTAMT.Text.Trim) > 0 Then
                    '        j = j & """IgstAmt"":" & Val(TEMPLINEIGSTAMT) & "" & ","
                    '        j = j & """CgstAmt"":" & "0" & "" & ","
                    '        j = j & """SgstAmt"":" & "0" & "" & ","
                    '    Else
                    '        j = j & """IgstAmt"":" & "0" & "" & ","
                    '        j = j & """CgstAmt"":" & Val(TEMPLINECGSTAMT) & "" & ","
                    '        j = j & """SgstAmt"":" & Val(TEMPLINESGSTAMT) & "" & ","
                    '    End If

                    '    j = j & """CesRt"":" & "0" & "" & ","
                    '    j = j & """CesAmt"":" & "0" & "" & ","
                    '    j = j & """CesNonAdvlAmt"":" & "0" & "" & ","
                    '    j = j & """StateCesRt"":" & "0" & "" & ","
                    '    j = j & """StateCesAmt"":" & "0" & "" & ","
                    '    j = j & """StateCesNonAdvlAmt"":" & "0" & "" & ","
                    '    j = j & """OthChrg"":" & "0" & "" & ","
                    '    j = j & """TotItemVal"":" & Val(TEMPLINEGRIDTOTALAMT) & "" & ","
                    '    j = j & """OrdLineRef"":"" "","
                    '    j = j & """OrgCntry"":""IN"","
                    '    j = j & """PrdSlNo"":""123"","

                    '    j = j & """BchDtls"": {"
                    '    j = j & """Nm"":""123"","
                    '    j = j & """Expdt"":""" & INVDATE.Text & """" & ","
                    '    j = j & """wrDt"":""" & INVDATE.Text & """" & "},"

                    '    j = j & """AttribDtls"": [{"
                    '    j = j & """Nm"":""" & DTROW("ITEMNAME") & """" & ","
                    '    j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                    '    'j = j & """Val"":""" & Val(TEMPLINEGRIDTOTALAMT) & """" & "}]"
                    'End If



                    j = j & " }"
                    CURRROW += 1


                    'THESE VARIABLES ARE JUST FOR TESTING PURPOSE
                    TEMPTOTALAMT += Val(TEMPLINETAXABLEAMT)
                    TEMPTOTALDISC += Val(TEMPLINECHARGES)
                    TEMPTOTALTAXABLEAMT += Val(TEMPLINETAXABLEAMT)
                    TEMPTOTALCGSTAMT += Val(TEMPLINECGSTAMT)
                    TEMPTOTALSGSTAMT += Val(TEMPLINESGSTAMT)
                    TEMPTOTALIGSTAMT += Val(TEMPLINEIGSTAMT)
                    TEMPGTOTALAMT += Val(TEMPLINEGRIDTOTALAMT)


                Next

                j = j & " ],"



                j = j & """ValDtls"": {"

                j = j & """AssVal"":" & Val(LBLTOTALTAXABLEAMT.Text.Trim) & "" & ","
                j = j & """CgstVal"":" & Val(LBLTOTALCGSTAMT.Text.Trim) & "" & ","
                j = j & """SgstVal"":" & Val(LBLTOTALSGSTAMT.Text.Trim) & "" & ","
                j = j & """IgstVal"":" & Val(LBLTOTALIGSTAMT.Text.Trim) & "" & ","

                j = j & """CesVal"":" & "0" & "" & ","
                j = j & """StCesVal"":" & "0" & "" & ","
                j = j & """Discount"":" & "0" & "" & ","
                j = j & """OthChrg"":" & "0" & "" & ","
                j = j & """RndOffAmt"":" & Val(TXTROUNDOFF.Text.Trim) & "" & ","
                j = j & """TotInvVal"":" & Val(TXTGTOTAL.Text.Trim) & "" & ","
                j = j & """TotInvValFc"":" & "0" & "" & "},"


                j = j & """PayDtls"": {"
                j = j & """Nm"":"" "","
                j = j & """Accdet"":"" "","
                j = j & """Mode"":""Credit"","
                j = j & """Fininsbr"":"" "","
                j = j & """Payterm"":"" "","
                j = j & """Payinstr"":"" "","
                j = j & """Crtrn"":"" "","
                j = j & """Dirdr"":"" "","
                j = j & """Crday"":" & "0" & "" & ","
                j = j & """Paidamt"":" & "0" & "" & ","
                j = j & """Paymtdue"":" & Val(TXTGTOTAL.Text.Trim) & "" & "},"


                j = j & """RefDtls"": {"
                j = j & """InvRm"":""TEST"","
                j = j & """DocPerdDtls"": {"
                j = j & """InvStDt"":""" & INVDATE.Text & """" & ","
                j = j & """InvEndDt"":""" & INVDATE.Text & """" & "},"

                j = j & """PrecDocDtls"": [{"
                j = j & """InvNo"":""" & DTINI.Rows(0).Item("PRINTINITIALS") & """" & ","
                j = j & """InvDt"":""" & INVDATE.Text & """" & ","
                j = j & """OthRefNo"":"" ""}],"

                j = j & """ContrDtls"": [{"
                j = j & """RecAdvRefr"":"" "","
                j = j & """RecAdvDt"":""" & INVDATE.Text & """" & ","
                j = j & """Tendrefr"":"" "","
                j = j & """Contrrefr"":"" "","
                j = j & """Extrefr"":"" "","
                j = j & """Projrefr"":"" "","
                j = j & """Porefr"":"" "","
                j = j & """PoRefDt"":""" & INVDATE.Text & """" & "}]"
                j = j & "},"




                j = j & """AddlDocDtls"": [{"
                j = j & """Url"":""https://einv-apisandbox.nic.in"","
                j = j & """Docs"":""INVOICE"","
                j = j & """Info"":""INVOICE""}],"

                j = j & """TransDocNo"":""" & TXTLRNO.Text.Trim & """" & ","



                j = j & """ExpDtls"": {"
                j = j & """ShipBNo"":"" "","
                j = j & """ShipBDt"":""" & INVDATE.Text & """" & ","
                j = j & """Port"":""INBOM1"","
                j = j & """RefClm"":""N"","
                j = j & """ForCur"":""AED"","
                j = j & """CntCode"":""AE""}"



                'THIS CODE IS WRITTEN COZ WHEN BILLTO AND SHIPTO ARE IN THE SAME PINCODE THEN WE HAVE TO PASS MINIMUM 10 KMS
                'OR ELSE IT WILL GIVE ERROR
                If DISPATCHFROMPINCODE = PARTYPINCODE Then PARTYKMS = 10

                'WE HAVE REMOVED CREATING EWAY DIRECTLY FORM EINVOICE
                'USER HAVE TO MANUALLY CREATE EWAY SEPERATELY
                'If TXTVEHICLENO.Text.Trim <> "" Then
                '    j = j & ","
                '    j = j & """EwbDtls"": {"
                '    j = j & """TransId"":""" & TRANSGSTIN & """" & ","
                '    j = j & """TransName"":""" & cmbtrans.Text.Trim & """" & ","
                '    j = j & """Distance"":""" & PARTYKMS & """" & ","
                '    If LRDATE.Text <> "__/__/____" Then j = j & """TransDocDt"":""" & LRDATE.Text & """" & "," Else j = j & """TransDocDt"":"""","
                '    j = j & """VehNo"":""" & TXTVEHICLENO.Text.Trim & """" & ","
                '    j = j & """VehType"":""" & "R" & """" & ","
                '    j = j & """TransMode"":""1""" & "}"
                'End If

                j = j & "}"


                Dim stream As Stream = REQUEST.GetRequestStream()
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(j)
                stream.Write(buffer, 0, buffer.Length)

                'POST request absenden
                RESPONSE = REQUEST.GetResponse()



            Catch ex As WebException
                'RESPONSE = ex.Response
                'MsgBox("Error While Generating EWB, Please check the Data Properly")
                ''ADD DATA IN EINVOICEENTRY
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                RESPONSE = ex.Response
                READER = New StreamReader(RESPONSE.GetResponseStream())
                REQUESTEDTEXT = READER.ReadToEnd()
                GoTo ERRORMESSAGE
            End Try

            READER = New StreamReader(RESPONSE.GetResponseStream())
            REQUESTEDTEXT = READER.ReadToEnd()


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 3
            TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
            If TEMPSTATUS = "1" Then
                TEMPSTATUS = "SUCCESS"
                MsgBox("E-Invoice Generated Successfully ")

            Else

ERRORMESSAGE:
                TEMPSTATUS = "FAILED"

                Dim ERRORMSG As String = ""
                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ErrorMessage") + Len("ErrorMessage") + 5
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS) - 2
                ERRORMSG = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','FAILED','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

                MsgBox("Error While Generating E-Invoice, " & REQUESTEDTEXT)

                Exit Sub
            End If


            Dim IRNNO As String = ""
            Dim ACKNO As String = ""
            Dim ADATE As String = ""


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackno") + Len("ACKNO") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ACKNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTACKNO.Text = ACKNO


            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("ackdt") + Len("ACKDT") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            ADATE = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            ACKDATE.Value = ADATE

            STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("irn") + Len("IRN") + 5
            ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("\", STARTPOS)
            IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            TXTIRNNO.Text = IRNNO


            'WE NEED TO UPDATE THIS IRNNO IN DATABASE ALSO
            DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_IRNNO = '" & TXTIRNNO.Text.Trim & "', INVOICE_ACKNO = '" & TXTACKNO.Text.Trim & "', INVOICE_ACKDATE = '" & Format(ACKDATE.Value.Date, "MM/dd/yyyy") & "' FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

            'ADD DATA IN EINVOICEENTRY
            DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


            'ADD DATA IN EINVOICEENTRY FOR QRCODE
            If TEMPSTATUS = "SUCCESS" Then

                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", j, RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)

                'GET REGINITIALS AS SAVE WITH IT
                Dim TEMPREG As DataTable = OBJCMN.Execute_Any_String("SELECT REGISTER_INITIALS AS INITIALS FROM REGISTERMASTER WHERE REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND REGISTER_TYPE ='SALE' AND REGISTER_YEARID = " & YearId, "", "")
                bitmap1.Save(Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()

                If PBQRCODE.Image IsNot Nothing Then
                    Dim OBJINVOICE As New ClsSaleInvoice
                    Dim MS As New IO.MemoryStream
                    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                    OBJINVOICE.alParaval.Add(CMBREGISTER.Text.Trim)
                    OBJINVOICE.alParaval.Add(MS.ToArray)
                    OBJINVOICE.alParaval.Add(YearId)
                    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")


                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")

            End If




            'STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("QrCodeImage\", 0) + Len("QrCodeImage\") + 5
            'ENDPOS = REQUESTEDTEXT.ToLower.IndexOf("""", STARTPOS)
            ''Dim QRSTREAM As New MemoryStream
            ''Dim bmp As New Bitmap(QRSTREAM)
            ''bmp.Save(QRSTREAM, Drawing.Imaging.ImageFormat.Bmp)
            ''QRSTREAM.Position = STARTPOS
            ''Dim data As Byte()
            ''QRSTREAM.Read(data, STARTPOS, STARTPOS - ENDPOS)

            'Dim bytes() As Byte
            'Dim ImageInStringFormat As String = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)
            'Dim MS As System.IO.MemoryStream
            'Dim NewImage As Bitmap

            'Dim nbyte() As Byte = System.Text.Encoding.UTF8.GetBytes(ImageInStringFormat)
            'Dim BASE64STRING As String = Convert.ToBase64String(nbyte)

            'bytes = Convert.FromBase64String(BASE64STRING)
            'NewImage = BytesToBitmap(bytes)
            'MS = New System.IO.MemoryStream(bytes)
            'MS.Write(bytes, 0, bytes.Length)
            'NewImage.Save(MS, Drawing.Imaging.ImageFormat.Bmp)    ' = System.Drawing.Image.FromStream(MS, True)
            'NewImage.Save("d:\qrcode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

            'IRNNO = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

            ''ADD data IN EINVOICEENTRY
            'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & IRNNO & "','" & TEMPSTATUS & "', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDUPLOADIRN_Click(sender As Object, e As EventArgs) Handles CMDUPLOADIRN.Click
        If (EDIT = True And USEREDIT = False And USERVIEW = False) Or (EDIT = False And USERADD = False) Then
            MsgBox("Insufficient Rights")
            Exit Sub
        End If

        OpenFileDialog1.Filter = "Pictures (*.png)|*.png"
        OpenFileDialog1.ShowDialog()

        OpenFileDialog1.AddExtension = True
        TXTFILENAME.Text = OpenFileDialog1.SafeFileName
        txtimgpath.Text = OpenFileDialog1.FileName
        TXTNEWIMGPATH.Text = Application.StartupPath & "\UPLOADDOCS\" & TXTINVOICENO.Text.Trim & txtuploadsrno.Text.Trim & TXTFILENAME.Text.Trim
        On Error Resume Next

        If txtimgpath.Text.Trim.Length <> 0 Then
            PBQRCODE.ImageLocation = txtimgpath.Text.Trim
            PBQRCODE.Load(txtimgpath.Text.Trim)
        End If
    End Sub

    Private Sub CMDGETQRCODE_Click(sender As Object, e As EventArgs) Handles CMDGETQRCODE.Click
        Try
            If EDIT = True And TXTIRNNO.Text.Trim <> "" And IsNothing(PBQRCODE.Image) = True Then

                'FIRST GETTOKEN AND THEN GET QRCODE
                Dim OBJCMN As New ClsCommon
                Dim DT As New DataTable

                Dim URL As New Uri("https://einvapi.charteredinfo.com/eivital/dec/v1.04/auth?aspid=1602611918&password=infosys123&Gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&eInvPwd=" & CMPEWBPASS)

                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                Dim REQUEST As WebRequest
                Dim RESPONSE As WebResponse
                REQUEST = WebRequest.CreateDefault(URL)

                REQUEST.Method = "GET"
                Try
                    RESPONSE = REQUEST.GetResponse()
                Catch ex As WebException
                    RESPONSE = ex.Response
                End Try
                Dim READER As StreamReader = New StreamReader(RESPONSE.GetResponseStream())
                Dim REQUESTEDTEXT As String = READER.ReadToEnd()

                'IF STATUS IS NOT 1 THEN TOKEN IS NOT GENERATED
                Dim STARTPOS As Integer = 0
                Dim TEMPSTATUS As String = ""
                Dim TOKEN As String = ""
                Dim ENDPOS As Integer = 0

                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("status") + Len("STATUS") + 2
                TEMPSTATUS = REQUESTEDTEXT.Substring(STARTPOS, 1)
                If TEMPSTATUS = "1" Then TEMPSTATUS = "SUCCESS" Else TEMPSTATUS = "FAILED"




                STARTPOS = REQUESTEDTEXT.ToLower.IndexOf("authtoken") + Len("AUTHTOKEN") + 3
                ENDPOS = REQUESTEDTEXT.ToLower.IndexOf(",", STARTPOS) - 1
                TOKEN = REQUESTEDTEXT.Substring(STARTPOS, ENDPOS - STARTPOS)

                'ADD DATA IN EINVOICEENTRY
                'DONT ADD IN EINVOICEENTRY, DONE BY GULKIT, IF FAILED THEN ADD
                'DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")


                'ONCE WE REC THE TOKEN WE WILL CREATE EWAY BILL
                'IF STATUS IS FAILED THEN ERROR MESSAGE
                If TEMPSTATUS = "FAILED" Then
                    MsgBox("Unable to create Eway Bill", MsgBoxStyle.Critical)
                    DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','','" & TEMPSTATUS & "','" & REQUESTEDTEXT & "', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                    Exit Sub
                End If
                'COMMENT BY WASEEM ON LINE NO 3266,3273,3274,3268,3285

                ''GET SIGNED QRCODE
                Dim req As New RestRequest
                req.AddParameter("application/json", "", RestSharp.ParameterType.RequestBody)
                'Dim client As New RestClient("http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=34AACCC1596Q002&user_name=TaxProEnvPON&AuthToken=" & TOKEN & "&QrCodeSize=250")
                Dim client As New RestClient("https://einvapi.charteredinfo.com/eicore/dec/v1.03/Invoice/irn/" & TXTIRNNO.Text.Trim & "?aspid=1602611918&password=infosys123&gstin=" & CMPGSTIN & "&user_name=" & CMPEWBUSER & "&AuthToken=" & TOKEN & "&QrCodeSize=250")
                ' Dim res As IRestResponse = Await client.ExecuteTaskAsync(req)
                Dim respPl = New RespPl()
                'respPl = JsonConvert.DeserializeObject(Of RespPl)(res.Content)
                Dim respPlGenIRNDec As New RespPlGenIRNDec()
                respPlGenIRNDec = JsonConvert.DeserializeObject(Of RespPlGenIRNDec)(respPl.Data)
                'MsgBox(respPlGenIRNDec.Irn)
                Dim qrImg As Byte() = Convert.FromBase64String(respPlGenIRNDec.QrCodeImage)
                ' Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(Bitmap))
                ' Dim bitmap1 As Bitmap = CType(tc.ConvertFrom(qrImg), Bitmap)



                'bitmap1.Save(Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                'PBQRCODE.ImageLocation = Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                'PBQRCODE.Refresh()


                'GET REGINITIALS AS SAVE WITH IT
                Dim TEMPREG As DataTable = OBJCMN.Execute_Any_String("SELECT REGISTER_INITIALS AS INITIALS FROM REGISTERMASTER WHERE REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' AND REGISTER_TYPE ='SALE' AND REGISTER_YEARID = " & YearId, "", "")
                'bitmap1.Save(Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png")
                PBQRCODE.ImageLocation = Application.StartupPath & "\" & TEMPREG.Rows(0).Item("INITIALS") & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png"
                PBQRCODE.Refresh()



                'If PBQRCODE.Image IsNot Nothing Then
                '    Dim OBJINVOICE As New ClsInvoiceMaster
                '    Dim MS As New IO.MemoryStream
                '    PBQRCODE.Image.Save(MS, Drawing.Imaging.ImageFormat.Png)
                '    OBJINVOICE.alParaval.Add(TXTINVOICENO.Text.Trim)
                '    OBJINVOICE.alParaval.Add(cmbregister.Text.Trim)
                '    OBJINVOICE.alParaval.Add(MS.ToArray)
                '    OBJINVOICE.alParaval.Add(YearId)
                '    Dim INTRES As Integer = OBJINVOICE.SAVEQRCODE()
                'End If

                'DT = OBJCMN.Execute_Any_String("UPDATE INVOICEMASTER SET INVOICE_QRCODE = (SELECT * FROM OPENROWSET(BULK '" & Application.StartupPath & "\" & Val(TXTINVOICENO.Text.Trim) & AccFrom.Year & ".png',SINGLE_BLOB) AS IMG) FROM INVOICEMASTER INNER JOIN REGISTERMASTER ON INVOICE_REGISTERID = REGISTER_ID WHERE INVOICE_NO = " & Val(TXTINVOICENO.Text.Trim) & " AND REGISTER_NAME = '" & cmbregister.Text.Trim & "' AND INVOICE_YEARID = " & YearId, "", "")

                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                DT = OBJCMN.Execute_Any_String("INSERT INTO EINVOICEENTRY VALUES (" & Val(TXTINVOICENO.Text.Trim) & ",'INVOICE','" & TOKEN & "','" & TXTIRNNO.Text.Trim & "','QRCODE SUCCESS1', '', GETDATE(), " & CmpId & "," & Userid & "," & YearId & ")", "", "")
                cmdok_Click(sender, e)

            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub TXTDISCPER_Validated(sender As Object, e As EventArgs) Handles TXTDISCPER.Validated
        Try
            CALCDISC()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class