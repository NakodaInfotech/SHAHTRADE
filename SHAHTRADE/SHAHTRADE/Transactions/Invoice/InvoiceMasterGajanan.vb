Imports BL

Public Class InvoiceMasterGajanan

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK, gridUPLOADDoubleClick, GRIDCHGSDOUBLECLICK As Boolean
    Dim TEMPROW, TEMPUPLOADROW, TEMPCHGSROW As Integer
    Public EDIT As Boolean
    Public TEMPINVNO As Integer
    Public TEMPREGNAME, tempmsg As String
    Dim saleregabbr, salereginitial As String
    Dim saleregid As Integer

    Private Sub cmdEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBREGISTER_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBREGISTER.Enter
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

    Sub GETMAX_INVOICE_NO()
        Dim DTTABLE As DataTable = getmax(" isnull(max(INVOICE_no),0) + 1 ", " INVOICEMASTERGAJ INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID", " AND REGISTERMASTER.REGISTER_NAME = '" & CMBREGISTER.Text.Trim & "' and INVOICE_yearid=" & YearId)
        If DTTABLE.Rows.Count > 0 Then TXTINVOICENO.Text = DTTABLE.Rows(0).Item(0)
    End Sub

    Private Sub CMBREGISTER_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBREGISTER.Validating
        Try
            If CMBREGISTER.Text.Trim.Length > 0 And edit = False Then
                clear()
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
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBPARTYNAME.Enter
        Try
            If CMBPARTYNAME.Text.Trim = "" Then fillname(CMBPARTYNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBPARTYNAME.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectLedger
                OBJLEDGER.STRSEARCH = " and GROUPMASTER.GROUP_SECONDARY = 'Sundry DEBTORS' AND LEDGERS.ACC_TYPE = 'ACCOUNTs'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBCODE.Text = OBJLEDGER.TEMPCODE
                If OBJLEDGER.TEMPNAME <> "" Then CMBPARTYNAME.Text = OBJLEDGER.TEMPNAME
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYNAME_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYNAME.Validated
        If CMBPARTYNAME.Text.Trim <> "" Then
            'GET REGISTER , AGENCT AND TRANS
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(LEDGERS.ACC_GSTIN,'') AS GSTIN ", "", " LEDGERS INNER JOIN GROUPMASTER ON LEDGERS.Acc_cmpid = GROUPMASTER.group_cmpid AND LEDGERS.Acc_locationid = GROUPMASTER.group_locationid AND LEDGERS.Acc_yearid = GROUPMASTER.group_yearid AND LEDGERS.Acc_groupid = GROUPMASTER.group_id LEFT OUTER JOIN STATEMASTER ON LEDGERS.Acc_stateid = STATEMASTER.state_id LEFT OUTER JOIN LEDGERS AS LEDGERS_1 ON LEDGERS.ACC_TRANSID = LEDGERS_1.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_1.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_1.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_1.Acc_yearid LEFT OUTER JOIN LEDGERS AS LEDGERS_2 ON LEDGERS.ACC_AGENTID = LEDGERS_2.Acc_id AND LEDGERS.Acc_cmpid = LEDGERS_2.Acc_cmpid AND LEDGERS.Acc_locationid = LEDGERS_2.Acc_locationid AND LEDGERS.Acc_yearid = LEDGERS_2.Acc_yearid LEFT OUTER JOIN REGISTERMASTER ON LEDGERS.Acc_cmpid = REGISTERMASTER.register_cmpid AND LEDGERS.Acc_locationid = REGISTERMASTER.register_locationid AND LEDGERS.Acc_yearid = REGISTERMASTER.register_yearid AND LEDGERS.ACC_REGISTERID = REGISTERMASTER.register_id", " and LEDGERS.acc_cmpname = '" & CMBPARTYNAME.Text.Trim & "' and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' and LEDGERS.acc_YEARid = " & YearId)
            If DT.Rows.Count > 0 Then
                TXTSTATECODE.Text = DT.Rows(0).Item("STATECODE")
                'TXTGSTIN.Text = DT.Rows(0).Item("GSTIN")
            End If
            GETHSNCODE()
        End If
    End Sub

    Sub GETHSNCODE()
        Try
            TXTSACCODE.Clear()
            TXTCGSTPER.Clear()
            TXTCGSTAMT.Clear()
            TXTSGSTPER.Clear()
            TXTSGSTAMT.Clear()
            TXTIGSTPER.Clear()
            TXTIGSTAMT.Clear()
         

            Dim OBJCMN As New ClsCommon
            Dim DT As New DataTable

            If CMBSACCODE.Text.Trim <> "" And Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                DT = OBJCMN.search("  ISNULL(HSN_CODE, '') AS HSNCODE, ISNULL(HSN_CGST, 0) AS CGSTPER, ISNULL(HSN_SGST, 0) AS SGSTPER, ISNULL(HSN_IGST, 0) AS IGSTPER ", "", " HSNMASTER ", " AND HSNMASTER.HSN_ITEMDESC = '" & CMBSACCODE.Text.Trim & "' AND HSNMASTER.HSN_YEARID='" & YearId & "' ORDER BY HSNMASTER.HSN_ID DESC")
                If DT.Rows.Count > 0 Then
                    TXTSACCODE.Text = DT.Rows(0).Item("HSNCODE")
                    If CMBPARTYNAME.Text.Trim <> "" Then
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


           
            total()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSACCODE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBSACCODE.Enter
        Try
            If CMBSACCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBSACCODE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLHSNITEMDESC(ByRef CMBHSNITEMDESC As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" ISNULL(HSN_ITEMDESC, '') AS HSNITEMDESC ", "", " HSNMASTER ", " AND HSN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "HSNITEMDESC"
                CMBHSNITEMDESC.DataSource = dt
                CMBHSNITEMDESC.DisplayMember = "HSNITEMDESC"
            End If
            CMBHSNITEMDESC.SelectAll()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSACCODE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSACCODE.Validated
        Try
            GETHSNCODE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR()
        EDIT = False
        CMBREGISTER.Enabled = True
        CMBREGISTER.Focus()
    End Sub

    Sub CLEAR()

        INVOICEDATE.Text = Mydate

        TXTSTATECODE.Clear()
        CMBPARTYNAME.Text = ""

        CMBSACCODE.Text = ""
        TXTINVOICENO.Clear()
        TXTSACCODE.Clear()


        TXTREMARKS.Clear()
        TXTINWORDS.Clear()
        txtbillamt.Clear()
        TXTCHARGES.Clear()
        TXTSUBTOTAL.Clear()
        TXTCGSTPER.Clear()
        TXTCGSTAMT.Clear()
        TXTSGSTPER.Clear()
        TXTSGSTAMT.Clear()
        TXTIGSTPER.Clear()
        TXTIGSTAMT.Clear()
        txtroundoff.Clear()
        txtgrandtotal.Clear()
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTCHGSSRNO.Clear()
        TXTNETT.Clear()
        TXTTAXID.Clear()

        TXTDESC.Clear()
        TXTAMT.Clear()

        GRIDINVOICE.RowCount = 0
        GRIDCHGS.RowCount = 0
        TXTSRNO.Text = 1

        GETMAX_INVOICE_NO()
        EP.Clear()

        GRIDDOUBLECLICK = False
        TabControl2.SelectedIndex = 0

    End Sub

    Sub FILLCMB()
        fillregister(CMBREGISTER, " and register_type = 'SALE'")
        fillname(CMBPARTYNAME, EDIT, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS' AND ACC_TYPE = 'ACCOUNTS'")
        fillname(CMBCHARGES, EDIT, "")
        If CMBSACCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBSACCODE)

    End Sub

    Private Sub CMBCHARGES_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Enter
        Try
            If CMBCHARGES.Text.Trim = "" Then fillname(CMBCHARGES, edit, " and (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCHARGES.Validated
        Try
            If CMBCHARGES.Text.Trim <> "" Then
                filltax()

                'GET ADDLESS DONE BY GULKIT
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search("ISNULL(LEDGERS.ACC_ADDLESS,'ADD') AS ADDLESS ", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item("ADDLESS") = "LESS" Then
                        If Val(TXTCHGSPER.Text.Trim) = 0 Then TXTCHGSPER.Text = "-"
                        If Val(TXTCHGSAMT.Text.Trim) = 0 Then TXTCHGSAMT.Text = "-"
                        TXTCHGSPER.Select(TXTCHGSPER.Text.Length, 0)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub filltax()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            TXTTAXID.Text = 0
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable = objclscommon.search(" ISNULL(tax_tax, 0) as TAX, TAX_ID AS TAXID ", "", " TAXMASTER", " AND tax_name = '" & CMBCHARGES.Text & "'  AND tax_cmpid=" & CmpId & " AND tax_LOCATIONID = " & Locationid & " AND tax_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                TXTCHGSPER.Text = dt.Rows(0).Item("TAX")
                TXTTAXID.Text = Val(dt.Rows(0).Item("TAXID"))
                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True
                TXTCHGSPER.ReadOnly = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCHARGES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCHARGES.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" Then namevalidate(CMBCHARGES, CMBCODE, e, Me, TXTTRANSADD, " AND (GROUPMASTER.GROUP_SECONDARY = 'Duties & Taxes' OR GROUPMASTER.GROUP_SECONDARY = 'Indirect Income' or GROUPMASTER.GROUP_SECONDARY = 'Indirect Expenses' OR GROUPMASTER.GROUP_SECONDARY = 'Direct Income' or GROUPMASTER.GROUP_SECONDARY = 'Direct Expenses' )")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSAMT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSAMT.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSAMT, Me)
        Catch ex As Exception
            Throw ex
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

    Sub calchgs()
        Try
            If Val(TXTCHGSPER.Text) <> 0 Then
                'before CALC CHECK HOW TO CALC CHARGES
                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" (CASE WHEN ISNULL(ACC_CALC,'') = '' THEN 'GROSS' ELSE ACC_CALC END) AS CALC", "", "LEDGERS", " AND ACC_CMPNAME = '" & CMBCHARGES.Text.Trim & "' AND ACC_YEARID = " & YearId)
                If DT.Rows(0).Item("CALC") = "GROSS" Then
                    TXTCHGSAMT.Text = Format((Val(txtbillamt.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "NETT" Then
                    'FIRST CALC NETT THEN ADD CHARGES ON THAT NETT TOTAL
                    TXTNETT.Text = Val(txtbillamt.Text.Trim)
                    For Each ROW As DataGridViewRow In GRIDCHGS.Rows
                        If GRIDCHGSDOUBLECLICK = True And ROW.Index >= TEMPCHGSROW Then Exit For
                        TXTNETT.Text = Format(Val(TXTNETT.Text) + Val(ROW.Cells(EAMT.Index).Value), "0.00")
                    Next
                    TXTCHGSAMT.Text = Format((Val(TXTNETT.Text) * Val(TXTCHGSPER.Text)) / 100, "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "QTY" Then
                    TXTCHGSAMT.Text = Format((Val(lbltotalpcs.Text) * Val(TXTCHGSPER.Text)), "0.00")
                ElseIf DT.Rows(0).Item("CALC") = "MTRS" Then
                    TXTCHGSAMT.Text = Format((Val(lbltotalmtrs.Text) * Val(TXTCHGSPER.Text)), "0.00")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub calc()
        Try
            'TXTGRIDTOTAL.Text = 0.0
            'TXTCGSTAMT.Text = 0.0
            'TXTSGSTAMT.Text = 0.0
            'TXTIGSTAMT.Text = 0.0

            'If Val(TXTEACHBOXES.Text.Trim) > 0 And Val(TXTBOXES.Text.Trim) > 0 Then TXTQTY.Text = Val(TXTEACHBOXES.Text.Trim) * Val(TXTBOXES.Text.Trim)

            'TXTAMOUNT.Text = Format(Val(TXTQTY.Text.Trim) * Val(TXTRATE.Text.Trim), "0.000")

            'TXTTAXABLEAMT.Text = Format((Val(TXTAMOUNT.Text.Trim) + Val(TXTOTHERAMT.Text.Trim)), "0.00")

            'TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
            'TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
            'TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")

            'TXTGRIDTOTAL.Text = Format(Val(TXTTAXABLEAMT.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text), "0.00")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub total()

        txtbillamt.Text = "0.0"
        TXTCHARGES.Text = "0.0"
        TXTSUBTOTAL.Text = "0.0"
        TXTCGSTAMT.Text = "0.0"
        TXTSGSTAMT.Text = "0.0"
        TXTIGSTAMT.Text = "0.0"
        txtroundoff.Text = "0.0"
        txtgrandtotal.Text = "0.0"

        For Each row As DataGridViewRow In GRIDINVOICE.Rows

            If Val(row.Cells(GAMT.Index).Value) <> 0 Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(row.Cells(GAMT.Index).Value), "0.00")


      

            'row.Cells(GCGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GCGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            'row.Cells(GSGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GSGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            'row.Cells(GIGSTAMT.Index).Value = Format((Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) * Val(row.Cells(GIGSTPER.Index).EditedFormattedValue) / 100), "0.00")
            'row.Cells(GGRIDTOTAL.Index).Value = Format(Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")


            'If Val(row.Cells(GTAXABLEAMT.Index).Value) <> 0 Then LBLTOTALTAXABLEAMT.Text = Format(Val(LBLTOTALTAXABLEAMT.Text) + Val(row.Cells(GTAXABLEAMT.Index).EditedFormattedValue), "0.00")
            'If Val(row.Cells(GCGSTAMT.Index).Value) <> 0 Then LBLTOTALCGSTAMT.Text = Format(Val(LBLTOTALCGSTAMT.Text) + Val(row.Cells(GCGSTAMT.Index).EditedFormattedValue), "0.00")
            'If Val(row.Cells(GSGSTAMT.Index).Value) <> 0 Then LBLTOTALSGSTAMT.Text = Format(Val(LBLTOTALSGSTAMT.Text) + Val(row.Cells(GSGSTAMT.Index).EditedFormattedValue), "0.00")
            'If Val(row.Cells(GIGSTAMT.Index).Value) <> 0 Then LBLTOTALIGSTAMT.Text = Format(Val(LBLTOTALIGSTAMT.Text) + Val(row.Cells(GIGSTAMT.Index).EditedFormattedValue), "0.00")
            'If Val(row.Cells(GGRIDTOTAL.Index).Value) <> 0 Then TXTTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(row.Cells(GGRIDTOTAL.Index).EditedFormattedValue), "0.00")

            'If Val(row.Cells(GAMT.Index).Value) <> 0 Then txtbillamt.Text = Format(Val(txtbillamt.Text) + Val(row.Cells(GAMT.Index).EditedFormattedValue), "0.00")

        Next

        If GRIDCHGS.RowCount > 0 Then
            For Each row1 As DataGridViewRow In GRIDCHGS.Rows
                TXTCHARGES.Text = Format(Val(TXTCHARGES.Text) + Val(row1.Cells(EAMT.Index).Value), "0.00")
            Next
        End If

        TXTSUBTOTAL.Text = Format(Val(txtbillamt.Text) + Val(TXTCHARGES.Text.Trim), "0.00")

        TXTCGSTAMT.Text = Format(Val(TXTCGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
        TXTSGSTAMT.Text = Format(Val(TXTSGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
        TXTIGSTAMT.Text = Format(Val(TXTIGSTPER.Text) / 100 * Val(TXTSUBTOTAL.Text), "0.00")
        'If cmbaddsub.Text = "Add" Then
        '    TXTSUBTOTAL.Text = Format(Val(TXTTOTAL.Text) + Val(txtotherchg.Text), "0.00")
        'Else
        '    TXTSUBTOTAL.Text = Format((Val(TXTTOTAL.Text)) - Val(txtotherchg.Text), "0.00")
        'End If

        'Dim dt As New DataTable
        'Dim objcmn As New ClsCommon
        'dt = objcmn.search("tax_name, tax_tax as tax", "", " taxmaster", "and TAXMASTER.tax_name = '" & CMBTAX.Text.Trim & "' and tax_yearid = " & YearId)
        'If dt.Rows.Count > 0 Then
        '    TXTTAX.Text = Format((Val(dt.Rows(0).Item(1)) * Val(TXTSUBTOTAL.Text)) / 100, "0.00")
        'End If

        'If CMBEXTRAADDSUB.Text = "Add" Then
        '    TXTGTOTAL.Text = Format(Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) + Val(TXTEXTRACHGS.Text), "0")
        '    txtroundoff.Text = Format(Val(TXTGTOTAL.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) + Val(TXTEXTRACHGS.Text)), "0.00")
        'Else
        '    TXTGTOTAL.Text = Format((Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text)) - Val(TXTEXTRACHGS.Text), "0")
        '    txtroundoff.Text = Format(Val(TXTGTOTAL.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTTAX.Text) - Val(TXTEXTRACHGS.Text)), "0.00")
        'End If


        txtgrandtotal.Text = Format((Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text) + Val(txtroundoff.Text)), "0")

        txtroundoff.Text = Format(Val(txtgrandtotal.Text) - (Val(TXTSUBTOTAL.Text) + Val(TXTCGSTAMT.Text) + Val(TXTSGSTAMT.Text) + Val(TXTIGSTAMT.Text)), "0.00")

        txtgrandtotal.Text = Format(Val(txtgrandtotal.Text), "0.00")
        If Val(txtgrandtotal.Text) > 0 Then TXTINWORDS.Text = CurrencyToWord(txtgrandtotal.Text)

    End Sub


    Private Sub TXTCHGSAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSAMT.Validating
        Try
            If CMBCHARGES.Text.Trim <> "" And Val(TXTCHGSAMT.Text.Trim) <> 0 Then
                Dim dDebit As Decimal
                Dim bValid As Boolean = Decimal.TryParse(TXTCHGSAMT.Text.Trim, dDebit)
                If bValid Then
                    TXTCHGSAMT.Text = Convert.ToDecimal(Val(TXTCHGSAMT.Text))
                    ' everything is good
                    fillchgsgrid()
                    total()
                Else
                    MessageBox.Show("Invalid Number Entered")
                    'e.Cancel = True
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            Else
                If CMBCHARGES.Text.Trim = "" Then
                    MsgBox("Please Fill Charges Name ")
                    CMBCHARGES.Focus()
                    Exit Sub
                ElseIf Val(TXTCHGSPER.Text.Trim) = 0 And Val(TXTCHGSAMT.Text.Trim) = 0 Then
                    MsgBox("Amount can not be zero")
                    TXTCHGSAMT.Clear()
                    TXTCHGSAMT.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillchgsgrid()
        If GRIDCHGSDOUBLECLICK = False Then
            GRIDCHGS.Rows.Add(Val(TXTCHGSSRNO.Text.Trim), CMBCHARGES.Text.Trim, Val(TXTCHGSPER.Text.Trim), Val(TXTCHGSAMT.Text.Trim), Val(TXTTAXID.Text.Trim))
            getsrno(GRIDCHGS)
        ElseIf GRIDCHGSDOUBLECLICK = True Then
            GRIDCHGS.Item(ESRNO.Index, TEMPCHGSROW).Value = Val(TXTCHGSSRNO.Text.Trim)
            GRIDCHGS.Item(ECHARGES.Index, TEMPCHGSROW).Value = CMBCHARGES.Text.Trim
            GRIDCHGS.Item(EPER.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSPER.Text.Trim), "0.00")
            GRIDCHGS.Item(EAMT.Index, TEMPCHGSROW).Value = Format(Val(TXTCHGSAMT.Text.Trim), "0.00")
            GRIDCHGS.Item(ETAXID.Index, TEMPCHGSROW).Value = Format(Val(TXTTAXID.Text.Trim))

            GRIDCHGSDOUBLECLICK = False

        End If
        total()

        GRIDCHGS.FirstDisplayedScrollingRowIndex = GRIDCHGS.RowCount - 1

        TXTCHGSSRNO.Clear()
        CMBCHARGES.Text = ""
        TXTCHGSPER.Clear()
        TXTCHGSAMT.Clear()
        TXTTAXID.Clear()
        If TXTCHGSPER.ReadOnly = True Then TXTCHGSPER.ReadOnly = False

        If GRIDCHGS.RowCount > 0 Then
            TXTCHGSSRNO.Text = Val(GRIDCHGS.Rows(GRIDCHGS.RowCount - 1).Cells(0).Value) + 1
        Else
            TXTCHGSSRNO.Text = 1
        End If
        TXTCHGSSRNO.Focus()
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

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim IntResult As Integer
        Try
            Cursor.Current = Cursors.WaitCursor
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(CMBREGISTER.Text.Trim)
            ALPARAVAL.Add(Format(Convert.ToDateTime(INVOICEDATE.Text).Date, "MM/dd/yyyy"))
            ALPARAVAL.Add(CMBPARTYNAME.Text.Trim)
            ALPARAVAL.Add(TXTSACCODE.Text.Trim)
            ALPARAVAL.Add(Val(txtbillamt.Text.Trim))
            ALPARAVAL.Add(Val(TXTCHARGES.Text.Trim))
            ALPARAVAL.Add(Val(TXTSUBTOTAL.Text.Trim))
            ALPARAVAL.Add(Val(TXTCGSTPER.Text.Trim))
            ALPARAVAL.Add(Val(TXTCGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(TXTSGSTPER.Text.Trim))
            ALPARAVAL.Add(Val(TXTSGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(TXTIGSTPER.Text.Trim))
            ALPARAVAL.Add(Val(TXTIGSTAMT.Text.Trim))
            ALPARAVAL.Add(Val(txtroundoff.Text.Trim))
            ALPARAVAL.Add(Val(txtgrandtotal.Text.Trim))
            ALPARAVAL.Add(TXTINWORDS.Text.Trim)
            ALPARAVAL.Add(TXTREMARKS.Text.Trim)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)


            '' GRID PARAMETERS
            Dim SRNO As String = ""
            Dim DESC As String = ""
            Dim AMT As String = ""
           

            ' Dim GRNNO As String = ""

            For Each ROW As Windows.Forms.DataGridViewRow In GRIDINVOICE.Rows
                If ROW.Cells(0).Value <> Nothing Then
                    If SRNO = "" Then
                        SRNO = ROW.Cells(GSRNO.Index).Value
                        DESC = ROW.Cells(GDESC.Index).Value.ToString
                        AMT = Val(ROW.Cells(GAMT.Index).Value)
                     
                    Else
                        SRNO = SRNO & "|" & ROW.Cells(GSRNO.Index).Value
                        DESC = DESC & "|" & ROW.Cells(GDESC.Index).Value.ToString
                        AMT = AMT & "|" & Val(ROW.Cells(GAMT.Index).Value)
                       
                    End If
                End If
            Next
            ALPARAVAL.Add(SRNO)
            ALPARAVAL.Add(DESC)
            ALPARAVAL.Add(AMT)


            Dim CSRNO As String = ""
            Dim CCHGS As String = ""
            Dim CPER As String = ""
            Dim CAMT As String = ""
            Dim CTAXID As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCHGS.Rows
                If row.Cells(0).Value <> Nothing Then
                    If CSRNO = "" Then
                        CSRNO = row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = row.Cells(ECHARGES.Index).Value.ToString
                        CPER = row.Cells(EPER.Index).Value.ToString
                        CAMT = Val(row.Cells(EAMT.Index).Value)
                        CTAXID = Val(row.Cells(ETAXID.Index).Value)
                    Else
                        CSRNO = CSRNO & "|" & row.Cells(ESRNO.Index).Value.ToString
                        CCHGS = CCHGS & "|" & row.Cells(ECHARGES.Index).Value.ToString
                        CPER = CPER & "|" & row.Cells(EPER.Index).Value.ToString
                        CAMT = CAMT & "|" & Val(row.Cells(EAMT.Index).Value)
                        CTAXID = CTAXID & "|" & Val(row.Cells(ETAXID.Index).Value)

                    End If
                End If
            Next

            ALPARAVAL.Add(CSRNO)
            ALPARAVAL.Add(CCHGS)
            ALPARAVAL.Add(CPER)
            ALPARAVAL.Add(CAMT)
            ALPARAVAL.Add(CTAXID)

           

            Dim OBJPUR As New ClsSaleInvoiceGaj
            OBJPUR.alParaval = ALPARAVAL

            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim DTTABLE As DataTable = OBJPUR.save()
                MsgBox("Details Added !!")
                TEMPINVNO = DTTABLE.Rows(0).Item(0)
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
                ALPARAVAL.Add(TEMPINVNO)
                IntResult = OBJPUR.update()
                MsgBox("Details Updated")
                PRINTREPORT(TEMPINVNO)
                EDIT = False

            End If
            CLEAR()
            CMBPARTYNAME.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBPARTYNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBPARTYNAME, "Please Enter Name")
            bln = False
        End If

        If CMBREGISTER.Text.Trim.Length = 0 Then
            EP.SetError(CMBREGISTER, "Enter Register Name")
            bln = False
        End If

        If GRIDINVOICE.RowCount = 0 Then
            EP.SetError(CMBPARTYNAME, "Please Enter Item Details")
            bln = False
        End If


        If INVOICEDATE.Text = "__/__/____" Then
            EP.SetError(INVOICEDATE, " Please Enter Proper Date")
            bln = False
        Else
            If Not datecheck(INVOICEDATE.Text) Then
                EP.SetError(INVOICEDATE, "Date not in Accounting Year")
                bln = False
            End If
        End If

        If INVOICEDATE.Text <> "__/__/____" Then
            If Convert.ToDateTime(INVOICEDATE.Text).Date >= "01/07/2017" Then
                If TXTSTATECODE.Text.Trim.Length = 0 Then
                    EP.SetError(TXTSTATECODE, "Please enter the state code")
                    bln = False
                End If

                'If TXTGSTIN.Text.Trim.Length = 0 Then
                '    If MsgBox("GSTIN Not Entered, Wish to Proceed?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                '        EP.SetError(TXTSTATECODE, "Enter GSTIN in Party Master")
                '        bln = False
                '    End If
                'End If

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

        For Each row As DataGridViewRow In GRIDINVOICE.Rows
            If row.Cells(GDESC.Index).Value = "" Then
                EP.SetError(TXTDESC, "Please enter Description")
                bln = False
            End If

            If Val(row.Cells(GAMT.Index).Value) = 0 Then
                EP.SetError(TXTAMT, "Amount Cannot Be Zero")
                bln = False
            End If
        Next

       

        Return bln
    End Function


    Private Sub InvoiceMasterGajanan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                Dim OBJINV As New ClsSaleInvoiceGaj

                ALPARAVAL.Add(TEMPINVNO)
                ALPARAVAL.Add(TEMPREGNAME)
                ALPARAVAL.Add(YearId)

                OBJINV.alParaval = ALPARAVAL
                DT = OBJINV.selectNO()

                If DT.Rows.Count > 0 Then
                    For Each DR As DataRow In DT.Rows

                        TXTINVOICENO.Text = TEMPINVNO
                        CMBREGISTER.Text = Convert.ToString(DR("REGISTERNAME"))
                        INVOICEDATE.Text = Format(Convert.ToDateTime(DR("DATE")).Date, "dd/MM/yyyy")
                        CMBPARTYNAME.Text = Convert.ToString(DR("NAME"))
                        TXTSTATECODE.Text = DR("STATECODE")
                        TXTSACCODE.Text = DR("HSNCODE")
                        TXTSUBTOTAL.Text = Val(DR("TOTALAMT"))
                        TXTCHARGES.Text = Val(DR("CHARGES"))
                        TXTSUBTOTAL.Text = Val(DR("SUBTOTAL"))
                        TXTCGSTPER.Text = Val(DR("CGSTPER"))
                        TXTCGSTAMT.Text = Val(DR("CGSTAMT"))
                        TXTSGSTPER.Text = Val(DR("SGSTPER"))
                        TXTSGSTAMT.Text = Val(DR("SGSTAMT"))
                        TXTIGSTPER.Text = Val(DR("IGSTPER"))
                        TXTIGSTAMT.Text = Val(DR("IGSTAMT"))
                        TXTIGSTAMT.Text = Val(DR("ROUNDOFF"))
                        TXTIGSTAMT.Text = Val(DR("GTOTAL"))
                        TXTIGSTAMT.Text = Val(DR("INWORDS"))
                        TXTREMARKS.Text = Convert.ToString(DR("REMARKS"))
                      

                        'TXTAMTREC.Text = DR("AMTREC")
                        'TXTEXTRAAMT.Text = DR("EXTRAAMT")
                        'TXTRETURN.Text = DR("RETURN")
                        'TXTBAL.Text = DR("BALANCE")

                        'If Val(DR("AMTREC")) > 0 Or Val(DR("EXTRAAMT")) > 0 Then
                        '    cmdshowdetails.Visible = True
                        '    PBRECD.Visible = True
                        '    lbllocked.Visible = True
                        '    PBlock.Visible = True
                        'End If

                        'If Val(DR("RETURN")) > 0 Then
                        '    cmdshowdetails.Visible = True
                        '    lbllocked.Visible = True
                        '    PBlock.Visible = True
                        'End If

                        '' grid 
                        GRIDINVOICE.Rows.Add(DR("SRNO").ToString, DR("DESC").ToString, Format(Val(DR("AMT")), "0.00"))
                        TabControl2.SelectedIndex = (0)

                    Next
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

             

                    Dim clscommon As New ClsCommon
                    Dim dtID As DataTable
                    dtID = clscommon.search(" register_abbr, register_initials, register_id ", "", " RegisterMaster ", " and register_name ='" & CMBREGISTER.Text.Trim & "' and register_type = 'SALE' and register_cmpid = " & CmpId & " and register_LOCATIONid = " & Locationid & " and register_YEARid = " & YearId)
                    If dtID.Rows.Count > 0 Then
                        saleregabbr = dtID.Rows(0).Item(0).ToString
                        salereginitial = dtID.Rows(0).Item(1).ToString
                        saleregid = dtID.Rows(0).Item(2)
                    End If

             
                    GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

                    'CHARGES GRID
                    Dim OBJCMN As New ClsCommon
                    Dim dttable As DataTable = OBJCMN.search(" INVOICEMASTERGAJ_CHGS.INVOICE_gridsrno AS GRIDSRNO, LEDGERS.Acc_cmpname AS CHARGES, INVOICEMASTERGAJ_CHGS.INVOICE_PER AS PER, INVOICEMASTERGAJ_CHGS.INVOICE_AMT AS AMT, INVOICEMASTERGAJ_CHGS.INVOICE_TAXID AS TAXID ", "", " INVOICEMASTERGAJ_CHGS INNER JOIN LEDGERS ON INVOICEMASTERGAJ_CHGS.INVOICE_CHARGESID = LEDGERS.Acc_id INNER JOIN REGISTERMASTER ON INVOICEMASTERGAJ_CHGS.INVOICE_REGISTERID = REGISTERMASTER.register_id ", " AND REGISTERMASTER.REGISTER_NAME = '" & TEMPREGNAME & "' AND INVOICEMASTERGAJ_CHGS.INVOICE_NO = " & TEMPINVNO & " AND INVOICEMASTERGAJ_CHGS.INVOICE_YEARID = " & YearId)
                    If dttable.Rows.Count > 0 Then
                        For Each DTR As DataRow In dttable.Rows
                            GRIDCHGS.Rows.Add(DTR("GRIDSRNO"), DTR("CHARGES"), DTR("PER"), DTR("AMT"), DTR("TAXID"))
                        Next
                    End If


                End If
                CMBREGISTER.Enabled = False

                INVOICEDATE.Focus()

                calc()
                total()
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

    Private Sub CMBPARTYNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYNAME.Validating
        Try
            If CMBPARTYNAME.Text.Trim <> "" Then namevalidate(CMBPARTYNAME, CMBCODE, e, Me, txtadd, "and GROUPMASTER.GROUP_SECONDARY = 'SUNDRY DEBTORS'", "SUNDRY DEBTORS", "ACCOUNTS")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTAMT_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTAMT.Validating
        'If ClientName = "SHAHTRADE" And TXTPARTNO.Text.Trim = "" Then
        '    MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        If TXTDESC.Text.Trim <> "" And Val(TXTAMT.Text.Trim) > 0 Then
            fillgrid()

            calc()
            total()
        Else
            MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
        End If
    End Sub

    Sub fillgrid()


        If GRIDDOUBLECLICK = False Then
            GRIDINVOICE.Rows.Add(Val(TXTSRNO.Text.Trim), TXTDESC.Text.Trim, Format(Val(TXTAMT.Text.Trim), "0.00"))
            getsrno(GRIDINVOICE)
        ElseIf GRIDDOUBLECLICK = True Then
            GRIDINVOICE.Item(GSRNO.Index, TEMPROW).Value = Val(TXTSRNO.Text)
            GRIDINVOICE.Item(GDESC.Index, TEMPROW).Value = TXTDESC.Text.Trim
            GRIDINVOICE.Item(GAMT.Index, TEMPROW).Value = Format(Val(TXTAMT.Text), "0.00")
            

            GRIDDOUBLECLICK = False
        End If

        'TXTAMT.ReadOnly = True
        total()
        GRIDINVOICE.FirstDisplayedScrollingRowIndex = GRIDINVOICE.RowCount - 1

        TXTDESC.Clear()
        TXTAMT.Clear()

        TXTSRNO.Text = Val(GRIDINVOICE.RowCount + 1)

        TXTDESC.Focus()

    End Sub

    Private Sub GRIDINVOICE_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDINVOICE.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            editrow()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub editrow()
        Try
            If GRIDINVOICE.CurrentRow.Index >= 0 And GRIDINVOICE.Item(GSRNO.Index, GRIDINVOICE.CurrentRow.Index).Value <> Nothing Then

                GRIDDOUBLECLICK = True
                TXTSRNO.Text = GRIDINVOICE.Item(GSRNO.Index, GRIDINVOICE.CurrentRow.Index).Value
                TXTDESC.Text = GRIDINVOICE.Item(GDESC.Index, GRIDINVOICE.CurrentRow.Index).Value.ToString
                TXTAMT.Text = Val(GRIDINVOICE.Item(GAMT.Index, GRIDINVOICE.CurrentRow.Index).Value)
               
                TEMPROW = GRIDINVOICE.CurrentRow.Index
                TXTDESC.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Toolprevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Toolprevious.Click
        Try
            GRIDINVOICE.RowCount = 0
Line1:
            TEMPINVNO = Val(TXTINVOICENO.Text) - 1
            TEMPREGNAME = CMBREGISTER.Text.Trim
            If TEMPINVNO > 0 Then
                EDIT = True
                InvoiceMasterGajanan_Load(sender, e)
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
                InvoiceMasterGajanan_Load(sender, e)
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



    Private Sub TXTCHGSPER_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTCHGSPER.KeyPress
        Try
            AMOUNTNUMDOTKYEPRESS(e, TXTCHGSPER, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTCHGSPER_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCHGSPER.Validating
        Try
            Dim dDebit As Decimal
            Dim bValid As Boolean = Decimal.TryParse(TXTCHGSPER.Text.Trim, dDebit)
            If bValid Then
                If Val(TXTCHGSPER.Text) = 0 Then TXTCHGSPER.Text = ""
                TXTCHGSPER.Text = Convert.ToDecimal(Val(TXTCHGSPER.Text))
                '' everything is good
                calchgs()
            ElseIf Val(TXTCHGSPER.Text.Trim) = 0 Then
                TXTCHGSAMT.ReadOnly = False
            Else
                MessageBox.Show("Invalid Number Entered")
                'e.Cancel = True
                TXTCHGSPER.Clear()
                TXTCHGSPER.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCHGS_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCHGS.CellDoubleClick
        EDITCHGSROW()

    End Sub

    Private Sub GRIDCHGS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCHGS.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCHGS.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDCHGSDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCHGS.Rows.RemoveAt(GRIDCHGS.CurrentRow.Index)
                total()
                getsrno(GRIDCHGS)
            ElseIf e.KeyCode = Keys.F5 Then
                EDITCHGSROW()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub EDITCHGSROW()
        Try
            TXTCHGSPER.ReadOnly = False
            TXTCHGSAMT.ReadOnly = False
            If GRIDCHGS.CurrentRow.Index >= 0 And GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value <> Nothing Then
                GRIDCHGSDOUBLECLICK = True
                TXTCHGSSRNO.Text = GRIDCHGS.Item(GSRNO.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                CMBCHARGES.Text = GRIDCHGS.Item(ECHARGES.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSPER.Text = GRIDCHGS.Item(EPER.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTCHGSAMT.Text = GRIDCHGS.Item(EAMT.Index, GRIDCHGS.CurrentRow.Index).Value.ToString
                TXTTAXID.Text = GRIDCHGS.Item(ETAXID.Index, GRIDCHGS.CurrentRow.Index).Value.ToString

                If Val(TXTCHGSPER.Text.Trim) > 0 Then TXTCHGSAMT.ReadOnly = True

                TEMPCHGSROW = GRIDCHGS.CurrentRow.Index
                TXTCHGSSRNO.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDINVOICE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDINVOICE.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDINVOICE.RowCount > 0 Then

                'dont allow user if any of the grid line is in edit mode.....
                'cmbMERCHANT.Text.Trim <> Val(txtqty.Text) <> 0 And Val(txtamount.Text.Trim) <> 0 And cmbqtyunit.Text.Trim <> 
                If GRIDDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDINVOICE.Rows.RemoveAt(GRIDINVOICE.CurrentRow.Index)
                total()
                getsrno(GRIDINVOICE)
            ElseIf e.KeyCode = Keys.F5 Then
                editrow()
            ElseIf e.KeyCode = Keys.F12 And GRIDINVOICE.RowCount > 0 Then
                'If GRIDINVOICE.CurrentRow.Cells(GITEMNAME.Index).Value <> "" Then GRIDINVOICE.Rows.Add(CloneWithValues(GRIDINVOICE.CurrentRow))
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        Try
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            Dim objINVDTLS As New InvoiceMasterDetailsGajanan
            objINVDTLS.MdiParent = MDIMain
            objINVDTLS.Show()
            objINVDTLS.BringToFront()
            ' Me.Close()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Try
            If EDIT = True Then PRINTREPORT(TEMPINVNO)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTREPORT(ByVal INVNO As Integer)
        Try
            tempmsg = MsgBox("Wish to Print Invoice?", MsgBoxStyle.YesNo)
            If tempmsg = vbYes Then
                Dim OBJINVOICE As New SaleInvoiceDesign
                OBJINVOICE.MdiParent = MDIMain
                OBJINVOICE.FRMSTRING = "INVOICE"
                OBJINVOICE.WHERECLAUSE = "{INVOICEMASTERGAJ.INVOICE_no}=" & Val(INVNO) & " AND {REGISTERMASTER.REGISTER_NAME} = '" & CMBREGISTER.Text.Trim & "' and {INVOICEMASTERGAJ.INVOICE_yearid}=" & YearId
                OBJINVOICE.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub tooldelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TOOLDELETE.Click
        Call cmddelete_Click(sender, e)
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
                Dim objpur As New ClsSaleInvoiceGaj
                Dim TEMPMSG As Integer = MsgBox("Wish to Delete?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then
                    Dim alparaval As New ArrayList
                    alparaval.Add(TEMPINVNO)
                    alparaval.Add(TEMPREGNAME)
                    alparaval.Add(YearId)

                    objpur.alParaval = alparaval
                    intresult = objpur.delete
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
End Class