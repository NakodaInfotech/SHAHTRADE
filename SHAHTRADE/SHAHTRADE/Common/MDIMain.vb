
Imports BL
Imports System.Windows.Forms

Public Class MDIMain

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub MDIMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CmpName & " (" & AccFrom & " - " & AccTo & ")"
        GETCONN()
        fillYEAR()
        SETENABILITY()

        'GET COMPANY'S DATA FOR VALIDATIONS OF EWB AND GST
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search("ISNULL(CMP_EWBUSER,'') AS EWBUSER, ISNULL(CMP_EWBPASS,'') AS EWBPASS, ISNULL(CMP_GSTIN,'') AS CMPGSTIN, ISNULL(CMP_ZIPCODE,'') AS CMPPINCODE, ISNULL(CITY_NAME,'') AS CITYNAME, CAST(STATE_NAME AS VARCHAR(5)) AS STATENAME, CAST(STATE_REMARK AS VARCHAR(5)) AS STATECODE, ISNULL(NOOFEWAYBILLS,0) AS EWAYCOUNTER", "", " STATEMASTER INNER JOIN CMPMASTER ON STATE_ID = CMP_STATEID LEFT OUTER JOIN CITYMASTER ON CMP_FROMCITYID = CITY_ID LEFT OUTER JOIN EWAYCOUNTER ON CMP_ID = CMPID", " AND CMP_ID = " & CmpId)
        If DT.Rows.Count > 0 Then
            CMPEWBUSER = DT.Rows(0).Item("EWBUSER")
            CMPEWBPASS = DT.Rows(0).Item("EWBPASS")
            CMPGSTIN = DT.Rows(0).Item("CMPGSTIN")
            CMPPINCODE = DT.Rows(0).Item("CMPPINCODE")
            CMPCITYNAME = DT.Rows(0).Item("CITYNAME")
            CMPSTATENAME = DT.Rows(0).Item("STATENAME")
            CMPSTATECODE = DT.Rows(0).Item("STATECODE")
            DT = OBJCMN.search("ISNULL(SUM(NOOFEWAYBILLS),0) AS EWAYCOUNTER", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
            CMPEWAYCOUNTER = Val(DT.Rows(0).Item("EWAYCOUNTER"))
            DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EWAYEXPDATE", "", " EWAYCOUNTER ", " AND CMPID = " & CmpId)
            EWAYEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EWAYEXPDATE")).Date.AddYears(1)

            DT = OBJCMN.search("ISNULL(SUM(NOOFEINVOICE),0) AS EINVOICECOUNTER", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
            CMPEINVOICECOUNTER = Val(DT.Rows(0).Item("EINVOICECOUNTER"))
            DT = OBJCMN.search("ISNULL(MAX(DATE), GETDATE()) AS EINVOICEEXPDATE", "", " EINVOICECOUNTER ", " AND CMPID = " & CmpId)
            EINVOICEEXPDATE = Convert.ToDateTime(DT.Rows(0).Item("EINVOICEEXPDATE")).Date.AddYears(1)
        End If

    End Sub

    Sub SETENABILITY()
        Try

            If UserName = "Admin" Then
                CMP_MASTER.Enabled = True
                YEAR_MASTER.Enabled = True
                ADMIN_MASTER.Enabled = True
                MERGELEDGER.Enabled = True
                YEARTRANSFER_MENU.Enabled = True
                BLOCKUSER_MENU.Enabled = True
            Else
                'ONLY TO CHANGE PASSWORD
                ADMIN_MASTER.Enabled = True
                USERADD.Enabled = False
                USEREDIT.Enabled = True
            End If

            For Each DTROW As DataRow In USERRIGHTS.Rows

                'MASTERS
                If DTROW(0).ToString = "GROUP MASTER" Then
                    If DTROW(1).ToString = True Then
                        GROUP_MASTER.Enabled = True
                        GROUPADD.Enabled = True
                    Else
                        GROUPADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        GROUP_MASTER.Enabled = True
                        GROUPEDIT.Enabled = True
                    Else
                        GROUPEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "ACCOUNTS MASTER" Then
                    If DTROW(1).ToString = True Then
                        ACC_MASTER.Enabled = True
                        TAX_MASTER.Enabled = True
                        ACCADD.Enabled = True
                        TAXADD.Enabled = True
                        NARRATION_MASTER.Enabled = True
                        NARRATIONADD.Enabled = True
                        HSN_MASTER.Enabled = True
                        HSNADD.Enabled = True
                    Else
                        ACCADD.Enabled = False
                        TAXADD.Enabled = False
                        ACCADD.Enabled = False
                        NARRATIONADD.Enabled = False
                        HSNADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ACC_MASTER.Enabled = True
                        TAX_MASTER.Enabled = True
                        ACCEDIT.Enabled = True
                        TAXEDIT.Enabled = True
                        NARRATION_MASTER.Enabled = True
                        NARRATIONEDIT.Enabled = True
                        HSN_MASTER.Enabled = True
                        HSNEDIT.Enabled = True
                    Else
                        ACCEDIT.Enabled = False
                        TAXEDIT.Enabled = False
                        ACCEDIT.Enabled = False
                        NARRATIONEDIT.Enabled = False
                        HSNEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "REGISTER MASTER" Then
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REG_MASTER.Enabled = True
                    End If

                ElseIf DTROW(0).ToString = "LOCATION MASTER" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        LOC_MASTER.Enabled = True
                    End If

                ElseIf DTROW(0).ToString = "UNIT MASTER" Then
                    If DTROW(1).ToString = True Then
                        UNIT_MASTER.Enabled = True
                        UNITADD.Enabled = True
                    Else
                        UNITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        UNIT_MASTER.Enabled = True
                        UNITEDIT.Enabled = True
                    Else
                        UNITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "ITEM MASTER" Then
                    If DTROW(1).ToString = True Then
                        ITEM_MASTER.Enabled = True
                        PATTA_MASTER.Enabled = True
                        SIZE_MASTER.Enabled = True
                        ITEMADD.Enabled = True
                        PATTAADD.Enabled = True
                        SIZEADD.Enabled = True
                        CATEGORY_MASTER.Enabled = True
                        SUBCATEGORY_MASTER.Enabled = True
                        MAKE_MASTER.Enabled = True
                        CATEGORYADD.Enabled = True
                        SUBCATEGORYADD.Enabled = True
                        MAKEADD.Enabled = True
                    Else
                        ITEMADD.Enabled = False
                        PATTAADD.Enabled = False
                        SIZEADD.Enabled = False
                        CATEGORYADD.Enabled = False
                        SUBCATEGORYADD.Enabled = False
                        MAKEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        ITEM_MASTER.Enabled = True
                        PATTA_MASTER.Enabled = True
                        SIZE_MASTER.Enabled = True
                        ITEMEDIT.Enabled = True
                        PATTAEDIT.Enabled = True
                        SIZEEDIT.Enabled = True
                        CATEGORY_MASTER.Enabled = True
                        SUBCATEGORY_MASTER.Enabled = True
                        MAKE_MASTER.Enabled = True
                        CATEGORYEDIT.Enabled = True
                        SUBCATEGORYEDIT.Enabled = True
                        MAKEEDIT.Enabled = True
                    Else
                        ITEMEDIT.Enabled = False
                        PATTAEDIT.Enabled = False
                        SIZEEDIT.Enabled = False
                        CATEGORYEDIT.Enabled = False
                        SUBCATEGORYEDIT.Enabled = False
                        MAKEEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "OPENING" Then
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        OPENINGBILL_MASTER.Enabled = True
                        OPENINGSTOCKVALUE.Enabled = True
                    End If

                    'PURCHASE
                ElseIf DTROW(0).ToString = "PURCHASE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        PURCHASE_MASTER.Enabled = True
                        PURCHASEADD.Enabled = True
                        PURCHASE_TOOL.Enabled = True

                    Else
                        PURCHASEADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PURCHASE_MASTER.Enabled = True
                        PURCHASEEDIT.Enabled = True
                        PURCHASE_TOOL.Enabled = True
                    Else
                        PURCHASEEDIT.Enabled = False
                    End If

                    'SALE
                ElseIf DTROW(0).ToString = "SALE QUOTATION" Then
                    If DTROW(1).ToString = True Then
                        QUOTATION_MASTER.Enabled = True
                        QUOTATIONADD.Enabled = True
                        QUOTATION_TOOL.Enabled = True
                    Else
                        QUOTATIONADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        QUOTATION_MASTER.Enabled = True
                        QUOTATIONEDIT.Enabled = True
                        QUOTATION_TOOL.Enabled = True
                    Else
                        QUOTATIONEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "GDN" Then
                    If DTROW(1).ToString = True Then
                        CHALLAN_MASTER.Enabled = True
                        CHALLANADD.Enabled = True
                        CHALLAN_TOOL.Enabled = True
                    Else
                        CHALLANADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CHALLAN_MASTER.Enabled = True
                        CHALLANEDIT.Enabled = True
                        CHALLAN_TOOL.Enabled = True
                    Else
                        CHALLANEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "SALE INVOICE" Then
                    If DTROW(1).ToString = True Then
                        SALE_MASTER.Enabled = True
                        CASHMEMO_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        CASHMEMO_TOOL.Enabled = True
                        SALEADD.Enabled = True
                        CASHMEMOADD.Enabled = True
                    Else
                        SALEADD.Enabled = False
                        CASHMEMOADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALE_MASTER.Enabled = True
                        CASHMEMO_MASTER.Enabled = True
                        SALE_TOOL.Enabled = True
                        CASHMEMO_TOOL.Enabled = True
                        SALEEDIT.Enabled = True
                        CASHMEMOEDIT.Enabled = True
                    Else
                        SALEEDIT.Enabled = False
                        CASHMEMOEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "LABOUR" Then
                    If DTROW(1).ToString = True Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        LABOURENTRY_MASTER.Enabled = True
                    End If

                    'ACCOUNTS
                ElseIf DTROW(0).ToString = "PAYMENT" Then
                    If DTROW(1).ToString = True Then
                        PAY_MASTER.Enabled = True
                        PAYMENT_TOOL.Enabled = True
                        PAYADD.Enabled = True
                    Else
                        PAYADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        PAY_MASTER.Enabled = True
                        PAYEDIT.Enabled = True
                        PAYMENT_TOOL.Enabled = True
                    Else
                        PAYEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "RECEIPT" Then
                    If DTROW(1).ToString = True Then
                        REC_MASTER.Enabled = True
                        RECEIPT_TOOL.Enabled = True
                        RECADD.Enabled = True
                    Else
                        RECADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        REC_MASTER.Enabled = True
                        RECEIPT_TOOL.Enabled = True
                        RECEDIT.Enabled = True
                    Else
                        RECEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CONTRA VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        CONTRA_MASTER.Enabled = True
                        CONTRAADD.Enabled = True
                    Else
                        CONTRAADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CONTRA_MASTER.Enabled = True
                        CONTRAEDIT.Enabled = True
                    Else
                        CONTRAEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "JOURNAL VOUCHER" Then
                    If DTROW(1).ToString = True Then
                        JV_MASTER.Enabled = True
                        JVADD.Enabled = True
                    Else
                        JVADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        JV_MASTER.Enabled = True
                        JVEDIT.Enabled = True
                    Else
                        JVEDIT.Enabled = False
                    End If


                ElseIf DTROW(0).ToString = "DEBIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        DEBIT_MASTER.Enabled = True
                        DEBITADD.Enabled = True
                    Else
                        DEBITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        DEBIT_MASTER.Enabled = True
                        DEBITEDIT.Enabled = True
                    Else
                        DEBITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "CREDIT NOTE" Then
                    If DTROW(1).ToString = True Then
                        CREDIT_MASTER.Enabled = True
                        CREDITADD.Enabled = True
                    Else
                        CREDITADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        CREDIT_MASTER.Enabled = True
                        CREDITEDIT.Enabled = True
                    Else
                        CREDITEDIT.Enabled = False
                    End If

                ElseIf DTROW(0).ToString = "VOUCHER ENTRY" Then
                    If DTROW(1).ToString = True Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHER_TOOL.Enabled = True
                        VOUCHERADD.Enabled = True
                    Else
                        VOUCHERADD.Enabled = False
                    End If
                    If (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        VOUCHER_MASTER.Enabled = True
                        VOUCHER_TOOL.Enabled = True
                        VOUCHEREDIT.Enabled = True
                    Else
                        VOUCHEREDIT.Enabled = False
                    End If

                    'REPORTS

                ElseIf DTROW(0).ToString = "SALE REPORTS" Then
                    If (DTROW(1) = True) Or (DTROW(2) = True) Or (DTROW(3) = True) Or (DTROW(4) = True) Then
                        SALE_REPORTS.Enabled = True
                    End If

                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillYEAR()
        Try
            Dim objcmn As New ClsCommon
            Dim dt As DataTable
            Dim whereclause As String = ""
            'If UserName <> "Admin" Then
            '    dt = objcmn.search(" distinct user_YEARid", "", "UserMaster", " and User_Name = '" & UserName & "' and user_cmpid = " & CmpId)
            '    For Each DTROW As DataRow In dt.Rows
            '        If whereclause = "" Then
            '            whereclause = " AND YEAR_ID IN (" & DTROW(0)
            '        Else
            '            whereclause = whereclause & "," & DTROW(0)
            '        End If
            '    Next
            '    whereclause = whereclause & ")"
            'End If

            dt = objcmn.search(" distinct user_YEARid", "", "UserMaster", " and User_Name = '" & UserName & "' and user_cmpid = " & CmpId)
            For Each DTROW As DataRow In dt.Rows
                If whereclause = "" Then
                    whereclause = " AND YEAR_ID IN (" & DTROW(0)
                Else
                    whereclause = whereclause & "," & DTROW(0)
                End If
            Next
            whereclause = whereclause & ")"

            dt = objcmn.search("CONVERT(char(11), year_startdate , 6) + '   ---   ' + CONVERT(char(11), year_enddate , 6) ", "", "YearMaster INNER JOIN cmpmaster on cmp_id = year_cmpid", whereclause & " order BY YEAR_ID")
            For Each DTROW As DataRow In dt.Rows
                cmbselectcmp.DropDownItems.Add(DTROW(0))
            Next
            cmbselectcmp.Text = CmpName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCityToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "CITYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewStateToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "STATEMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewCountryToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "COUNTRYMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewAreaToolStripMenuItem.Click
        Try
            Dim objcitymaster As New citymaster
            objcitymaster.MdiParent = Me
            objcitymaster.frmstring = "AREAMASTER"
            objcitymaster.Show()
            objcitymaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try

    End Sub

    Private Sub AddNewAccountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCADD.Click
        Try
            Dim objAccountMaster As New AccountsMaster
            objAccountMaster.MdiParent = Me
            objAccountMaster.frmstring = "ACCOUNTS"
            objAccountMaster.Show()
            objAccountMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAccoutsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ACCEDIT.Click
        Try
            Dim objAccountDetails As New AccountsDetails
            objAccountDetails.MdiParent = Me
            objAccountDetails.frmstring = "ACCOUNTS"
            objAccountDetails.Show()
            objAccountDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingAreaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingAreaToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "AREAMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingCityToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "CITYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingStateToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "STATEMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingCountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditExistingCountryToolStripMenuItem.Click
        Try
            Dim objCityDetails As New CityDetails
            objCityDetails.MdiParent = Me
            objCityDetails.frmstring = "COUNTRYMASTER"
            objCityDetails.Show()
            objCityDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub addUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USERADD.Click
        Try
            Dim objuser As New UserMaster
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub editUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles USEREDIT.Click
        Try
            Dim objuser As New UserDetails
            objuser.MdiParent = Me
            objuser.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub opencmp(ByVal CMP As String)
        Try

            Dim objcmn As New ClsCommon
            Dim DT As DataTable

            DT = objcmn.search("CMPMASTER.CMP_NAME, YEARMASTER.YEAR_DBNAME, CMPMASTER.CMP_ID, YEARMASTER.YEAR_STARTDATE, YEARMASTER.YEAR_ENDDATE, YEARMASTER.YEAR_ID", "", " CMPMASTER INNER JOIN YEARMASTER ON YEARMASTER.YEAR_CMPID = CMPMASTER.CMP_ID", " AND CMPMASTER.CMP_NAME = '" & CMP & "'")
            CmpName = DT.Rows(0).Item(0).ToString
            DBName = DT.Rows(0).Item(1).ToString
            CmpId = DT.Rows(0).Item(2).ToString
            AccFrom = DT.Rows(0).Item(3)
            AccTo = DT.Rows(0).Item(4)
            YearId = DT.Rows(0).Item(5).ToString
            Cmppassword.cmdback.Visible = False
            Cmppassword.lblretype.Visible = False
            Cmppassword.txtretypepassword.Visible = False
            Cmppassword.cmdnext.Text = "&Ok"
            Cmppassword.ShowDialog()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TAXADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXADD.Click
        Try
            Dim OBJTAX As New Taxmaster
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
            OBJTAX.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TAXEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TAXEDIT.Click
        Try
            Dim OBJTAXDETAILS As New TaxDetails
            OBJTAXDETAILS.MdiParent = Me
            OBJTAXDETAILS.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ChangeCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCompany.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Cmpdetails.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Try
            'close all child forms
            Dim frm As Form
            For Each frm In MdiChildren
                frm.Close()
            Next

            Me.Dispose()
            Login.Show()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPEDIT.Click
        Try
            Cmpmaster.edit = True
            Cmpmaster.TEMPCMPNAME = CmpName
            Cmpmaster.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMPADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMPADD.Click
        Try
            Dim obj As New Cmpmaster
            obj.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub BackupCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupCompany.Click
        Try
            backup()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub backup()
        Try
            'TAKE BACKUP
            Dim TEMPMSG As Integer = MsgBox("Create Backup?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then

                'CHECKING FOR BACKUP FOLDER
                If FileIO.FileSystem.DirectoryExists("C:\SHAHTRADEBACKUP") = False Then FileIO.FileSystem.CreateDirectory("C:\SHAHTRADEBACKUP")

                'IF SAME DATE'S BACKUP EXIST THEN DELETE IT THEN RECREATE IT
                If FileIO.FileSystem.FileExists("C:\SHAHTRADEBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak") Then FileIO.FileSystem.DeleteFile("C:\SHAHTRADEBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak")

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.Execute_Any_String(" backup database SHAHTRADE to disk='C:\SHAHTRADEBACKUP\BACKUP " & Now.Day & "-" & Now.Month & "-" & Now.Year & ".bak'", "", "")
                MsgBox("Backup Completed")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MERGELEDGER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MERGELEDGER.Click
        Try
            Dim OBJMERGE As New MergeLedger
            OBJMERGE.MdiParent = Me
            OBJMERGE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BLOCKUSER_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BLOCKUSER_MENU.Click
        Try
            'Dim OBJUSER As New BlockUser
            'OBJUSER.MdiParent = Me
            'OBJUSER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QUOTATIONADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUOTATIONADD.Click
        Try
            Dim OBJQUOTE As New SaleQuotation
            OBJQUOTE.MdiParent = Me
            OBJQUOTE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QUOTATIONEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUOTATIONEDIT.Click
        Try
            Dim OBJQUOTE As New SaleQuotationDetails
            OBJQUOTE.MdiParent = Me
            OBJQUOTE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALEEDIT.Click
        Try
            If ClientName = "NAMASKAR" Or ClientName = "HMENTERPRISE" Then
                Dim OBJINV As New MaterialOutwardDetails
                OBJINV.MdiParent = Me
                OBJINV.Show()
            Else
                Dim OBJINV As New InvoiceDetails
                OBJINV.MdiParent = Me
                OBJINV.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANADD.Click
        Try
            If ClientName = "GELATO" Then
                Dim OBJCHALLAN As New ChallanGelato
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            Else
                Dim OBJCHALLAN As New Challan
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLANEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLANEDIT.Click
        Try
            If ClientName = "GELATO" Then
                Dim OBJCHALLAN As New ChallanGelatoDetails
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            Else
                Dim OBJCHALLAN As New ChallanDetails
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHALLAN_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHALLAN_TOOL.Click
        Try
            If ClientName = "GELATO" Then
                Dim OBJCHALLAN As New ChallanGelato
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            Else
                Dim OBJCHALLAN As New Challan
                OBJCHALLAN.MdiParent = Me
                OBJCHALLAN.Show()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub QUOTATION_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QUOTATION_TOOL.Click
        Try
            Dim OBJQUOTE As New SaleQuotation
            OBJQUOTE.MdiParent = Me
            OBJQUOTE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALE_TOOL.Click
        Try
            If ClientName = "GAJANAN" Then
                Dim OBJINV As New InvoiceMasterGajanan
                OBJINV.MdiParent = Me
                OBJINV.Show()

            ElseIf ClientName = "NAMASKAR" Or ClientName = "HMENTERPRISE" Then
                Dim OBJOUTWARD As New MaterialOutward
                OBJOUTWARD.MdiParent = Me
                OBJOUTWARD.Show()

            Else
                Dim OBJINV As New InvoiceMaster
                OBJINV.MdiParent = Me
                OBJINV.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UNITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNITADD.Click
        Try
            Dim OBJORUNIT As New UnitMaster
            OBJORUNIT.frmString = "UNIT"
            OBJORUNIT.MdiParent = Me
            OBJORUNIT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UNITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UNITEDIT.Click
        Try
            Dim OBJORUNIT As New UnitDetails
            OBJORUNIT.frmstring = "UNIT"
            OBJORUNIT.MdiParent = Me
            OBJORUNIT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ITEMADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ITEMADD.Click
        Try
            Dim OBJITEM As New ItemMaster
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ITEMEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ITEMEDIT.Click
        Try
            Dim OBJITEM As New ItemDetails
            OBJITEM.MdiParent = Me
            OBJITEM.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YEARTRANSFER_MENU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YEARTRANSFER_MENU.Click
        Try
            Dim OBJYEAR As New YearTransfer
            OBJYEAR.MdiParent = Me
            OBJYEAR.FRMSTRING = "YEARTRANSFER"
            OBJYEAR.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CASHMEMOADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHMEMOADD.Click
        Try
            Dim OBJCASH As New CashMemo
            OBJCASH.MdiParent = Me
            OBJCASH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CASHMEMOEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHMEMOEDIT.Click
        Try
            Dim OBJCASH As New CashMemoDetails
            OBJCASH.MdiParent = Me
            OBJCASH.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CASHMEMO_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHMEMO_TOOL.Click
        Try
            If ClientName = "SAKARIA" Or ClientName = "ALENCOT" Then
                Dim OBJCASH As New CashEntry
                OBJCASH.MdiParent = Me
                OBJCASH.Show()
            Else
                Dim OBJCASH As New CashMemo
                OBJCASH.MdiParent = Me
                OBJCASH.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEIPT_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECEIPT_TOOL.Click
        Try
            Dim OBJREC As New Receipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewNarrationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingNarrationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NARRATIONEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmstring = "NARRATION"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewSaleRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewSaleRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewReciptRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewReciptRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "RECEIPT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutstandingFilterToolStripMenuItem.Click
        Try
            Dim OBJOUTSTANDING As New OutstandingFilter
            OBJOUTSTANDING.MdiParent = Me
            OBJOUTSTANDING.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNADD.Click
        Try
            Dim OBJHSN As New HSNMaster
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HSNEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HSNEDIT.Click
        Try
            Dim OBJHSN As New HSNDetails
            OBJHSN.MdiParent = Me
            OBJHSN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASEADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEADD.Click
        Try
            If ClientName = "NAMASKAR" Then
                Dim OBJINWARD As New MaterialInward
                OBJINWARD.MdiParent = Me
                OBJINWARD.Show()
            Else
                Dim OBJPUR As New PurchaseInvoice
                OBJPUR.MdiParent = Me
                OBJPUR.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewPurchaseRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEREGISTERADD.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PURCHASE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURCHASEEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEEDIT.Click
        Try
            If ClientName = "NAMASKAR" Then
                Dim OBJINWARD As New MaterialInwardDetails
                OBJINWARD.MdiParent = Me
                OBJINWARD.Show()
            Else
                Dim OBJPUR As New PurchaseInvoiceDetails
                OBJPUR.MdiParent = Me
                OBJPUR.Show()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGSTOCK_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGSTOCK_MASTER.Click
        Try
            Dim OBJstock As New OpeningStock
            OBJstock.MdiParent = Me
            OBJstock.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTAXREGISTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GSTTAXFILTER_MASTER.Click
        Try
            Dim OBJTAX As New GSTTaxFilter
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYADD.Click
        Try
            Dim OBJPAY As New PaymentMaster
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYEDIT.Click
        Try
            Dim OBJPAY As New PaymentDetails
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECADD.Click
        Try
            Dim OBJREC As New Receipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RECEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RECEDIT.Click
        Try
            Dim OBJREC As New ReceiptDetails
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAADD.Click
        Try
            Dim OBJCON As New ContraEntry
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRAEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTRAEDIT.Click
        Try
            Dim OBJCON As New ContraDetails
            OBJCON.MdiParent = Me
            OBJCON.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JVEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVADD.Click
        Try
            Dim OBJJV As New journal
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingJournalEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JVEDIT.Click
        Try
            Dim OBJJV As New JournalDetails
            OBJJV.MdiParent = Me
            OBJJV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITADD.Click
        Try
            Dim OBJCN As New CREDITNOTE
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CREDITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CREDITEDIT.Click
        Try
            Dim OBJCN As New CreditNoteDetails
            OBJCN.MdiParent = Me
            OBJCN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITADD.Click
        Try
            Dim OBJDN As New DebitNote
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DEBITEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEBITEDIT.Click
        Try
            Dim OBJDN As New DebitNoteDetails
            OBJDN.MdiParent = Me
            OBJDN.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHERADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHERADD.Click
        Try
            Dim OBJEXP As New ExpenseVoucher
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHEREDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHEREDIT.Click
        Try
            Dim OBJEXP As New ExpenseVoucherDetails
            OBJEXP.MdiParent = Me
            OBJEXP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewExpenseRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewExpenseRegisterToolStripMenuItem.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "EXPENSE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PURCHASE_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASE_TOOL.Click
        Try
            If ClientName = "NAMASKAR" Then
                Dim OBJINWARD As New MaterialInward
                OBJINWARD.MdiParent = Me
                OBJINWARD.Show()
            Else
                Dim OBJPUR As New PurchaseInvoice
                OBJPUR.MdiParent = Me
                OBJPUR.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VOUCHER_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VOUCHER_TOOL.Click
        Try
            If ClientName = "ROHIT" Then
                Dim OBJISSUE As New IssueVoucher
                OBJISSUE.MdiParent = Me
                OBJISSUE.Show()
            Else
                Dim OBJEXP As New ExpenseVoucher
                OBJEXP.MdiParent = Me
                OBJEXP.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGBILL_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGBILL_MASTER.Click
        Try
            Dim OBJOP As New OpeningBills
            OBJOP.FRMSTRING = "OPENINGBILLS"
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OPENINGSTOCKVALUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OPENINGSTOCKVALUE.Click
        Try
            Dim OBJOP As New OpeningClosingStock
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MDIMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If ClientName = "SHENSHE" Then
                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False

            ElseIf ClientName = "GAJANAN" Then
                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False
                PURCHASE_TOOL.Visible = False
                PURCHASETOOLSTRIP.Visible = False
                PurchaseToolStripMenuItem.Visible = False
                SaleToolStripMenuItem.Visible = False
                PURCHASEREGISTER.Visible = False

            ElseIf ClientName = "GELATO" Then
                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                PURCHASE_TOOL.Visible = False
                PURCHASETOOLSTRIP.Visible = False
                VOUCHER_TOOL.Visible = False
                VOUCHER_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHENTRY_MASTER.Visible = False
                CASHENTRY_MENUSTRIP.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False
                RECEIPT_TOOL.Visible = False
                RECEIPT_TOOLSTRIP.Visible = False
                PAYMENT_TOOL.Visible = False
                PAYMENT_TOOLSTRIP.Visible = False

                PurchaseToolStripMenuItem.Visible = False
                PURCHASEREGISTER.Visible = False
                AccountsToolStripMenuItem.Visible = False

                QUOTATION_MASTER.Visible = False
                CASHMEMO_MASTER.Visible = False
                SALERETURN_MASTER.Visible = False
                ACCOUNTREPORT_MAIN.Visible = False

            ElseIf ClientName = "SAKARIA" Or ClientName = "ALENCOT" Then
                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                SALE_TOOL.Visible = False
                SALE_TOOLSTRIP.Visible = False
                PURCHASE_TOOL.Visible = False
                PURCHASETOOLSTRIP.Visible = False
                PurchaseToolStripMenuItem.Visible = False
                SaleToolStripMenuItem.Visible = False
                PURCHASEREGISTER.Visible = False
                VOUCHER_TOOL.Visible = False
                VOUCHER_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Text = "Cash Entry"
                CASHENTRY_MASTER.Visible = True
                CASHENTRY_MENUSTRIP.Visible = True

            ElseIf ClientName = "SNSMALAD" Then

                TAX_MASTER.Visible = False
                LOC_MASTER.Visible = False
                REG_MASTER.Visible = False
                OtherMastersToolStripMenuItem.Visible = False


                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                PURCHASE_TOOL.Visible = False
                PURCHASETOOLSTRIP.Visible = False
                SALE_TOOL.Visible = False
                SALE_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False
                VOUCHER_TOOL.Visible = False
                VOUCHER_TOOLSTRIP.Visible = False

                PurchaseToolStripMenuItem.Visible = False
                SaleToolStripMenuItem.Visible = False
                PURCHASEREGISTER.Visible = False

                PATTA_MASTER.Visible = True
                SIZE_MASTER.Visible = True
                CONFIG_MASTER.Visible = True
                LABOURENTRY_MASTER.Visible = True
                CONTRACTORENTRY_MASTER.Visible = True

            ElseIf ClientName = "ROHIT" Then

                TAX_MASTER.Visible = False
                UNIT_MASTER.Visible = False
                REG_MASTER.Visible = False
                REGISTERMASTER_TOOLSTRIP.Visible = False
                OPENINGSTOCK_MASTER.Visible = False

                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                PURCHASE_TOOL.Visible = False
                PURCHASETOOLSTRIP.Visible = False
                PurchaseToolStripMenuItem.Visible = False
                SALE_TOOL.Visible = False
                SALE_TOOLSTRIP.Visible = False
                SaleToolStripMenuItem.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False
                RECEIPT_TOOL.Visible = False
                RECEIPT_TOOLSTRIP.Visible = False
                PAYMENT_TOOL.Visible = False
                PAYMENT_TOOLSTRIP.Visible = False
                AccountsToolStripMenuItem.Visible = False
                REGISTER_MAIN.Visible = False
                ACCOUNTREPORT_MAIN.Visible = False
                REPORTS_MAIN.Visible = False

                ISSUEVOUCHER_MASTER.Visible = True
                HALLMARK_MASTER.Visible = True

            ElseIf ClientName = "NAMASKAR" Or ClientName = "HMENTERPRISE" Then

                TAX_MASTER.Visible = False
                UNIT_MASTER.Visible = False
                REG_MASTER.Visible = False
                REGISTERMASTER_TOOLSTRIP.Visible = False
                OPENINGSTOCK_MASTER.Visible = False

                PURRET_MASTER.Visible = False
                PURRET_TOOLSTRIP.Visible = False

                QUOTATION_MASTER.Visible = False
                CHALLAN_MASTER.Visible = False
                CASHMEMO_MASTER.Visible = False
                SALERETURN_MASTER.Visible = False

                QUOTATION_TOOL.Visible = False
                QUOTATION_TOOLSTRIP.Visible = False
                CHALLAN_TOOL.Visible = False
                CHALLAN_TOOLSTRIP.Visible = False
                CASHMEMO_TOOL.Visible = False
                CASHMEMO_TOOLSTRIP.Visible = False
                RECEIPT_TOOL.Visible = False
                RECEIPT_TOOLSTRIP.Visible = False
                PAYMENT_TOOL.Visible = False
                PAYMENT_TOOLSTRIP.Visible = False
                AccountsToolStripMenuItem.Visible = False
                REGISTER_MAIN.Visible = False
                ACCOUNTREPORT_MAIN.Visible = False
                REPORTS_MAIN.Visible = False
                VOUCHER_TOOL.Visible = False
                VOUCHER_TOOLSTRIP.Visible = False

                If ClientName = "NAMASKAR" Then
                    PURCHASE_TOOL.Text = "Inward"
                    SALE_TOOL.Text = "Outward"
                    SALECONSUMPTION_MASTER.Visible = True
                Else
                    PURCHASE_TOOL.Visible = False
                    PURCHASETOOLSTRIP.Visible = False
                    PurchaseToolStripMenuItem.Visible = False
                End If

            End If

            If ClientName <> "SNSMALAD" Then
                CONTRACTORISS_TOOL.Visible = False
                CONTRACTORISS_TOOLSTRIP.Visible = False
                CONTRACTORREC_TOOL.Visible = False
                CONTRACTORREC_TOOLSTRIP.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PurchaseRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURCHASEREGISTER.Click
        Try
            Dim objpurreg As New PurchaseRegister
            objpurreg.MdiParent = Me
            objpurreg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleRegisterToolStripMenuItem2.Click
        Try
            Dim objsalereg As New SaleRegister
            objsalereg.MdiParent = Me
            objsalereg.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub JournalRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JournalRegisterToolStripMenuItem2.Click
        Try
            Dim OBJJVREG As New JournalRegister
            OBJJVREG.MdiParent = Me
            OBJJVREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContraRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContraRegisterToolStripMenuItem2.Click
        Try
            Dim OBJCONTRAREG As New ContraRegister
            OBJCONTRAREG.MdiParent = Me
            OBJCONTRAREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DebitNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebitNoteRegisterToolStripMenuItem.Click
        Try
            Dim OBJDNREG As New DNRegister
            OBJDNREG.MdiParent = Me
            OBJDNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreditNoteRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditNoteRegisterToolStripMenuItem.Click
        Try
            Dim OBJCNREG As New CNRegister
            OBJCNREG.MdiParent = Me
            OBJCNREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BankBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankBookToolStripMenuItem.Click
        Try
            Dim OBJBANKREG As New BankRegister
            OBJBANKREG.MdiParent = Me
            OBJBANKREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CashBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashBookToolStripMenuItem.Click
        Try
            Dim OBJCASHREG As New cashregister1
            OBJCASHREG.MdiParent = Me
            OBJCASHREG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AdvancesSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvancesSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_adv_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PayableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "PAYOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceivableSettlementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableSettlementToolStripMenuItem.Click
        Try
            Dim OBJADV As New Adv_Receivable_settlement
            OBJADV.flag_Rec_settlement = True
            OBJADV.MdiParent = Me
            OBJADV.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReceivableOutstandingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivableOutstandingToolStripMenuItem.Click
        Try
            Dim OBJOUT As New OutstandingFilter
            OBJOUT.FRMSTRING = "RECOUTSTANDING"
            OBJOUT.MdiParent = Me
            OBJOUT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DayBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayBookToolStripMenuItem.Click
        Try
            Dim OBJDAYBOOK As New DayBook
            OBJDAYBOOK.MdiParent = Me
            OBJDAYBOOK.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerOnScreenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerOnScreenToolStripMenuItem.Click
        Try
            Dim objledger As New LedgerSummary
            objledger.MdiParent = Me
            objledger.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgerBookToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerBookToolStripMenuItem1.Click
        Try
            Dim objledgerbook As New RegisterDetails
            objledgerbook.MdiParent = Me
            objledgerbook.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OutstandingFilterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutstandingFilterToolStripMenuItem.Click
        Try
            Dim OBJOP As New OutstandingFilter
            OBJOP.MdiParent = Me
            OBJOP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LedgersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgersToolStripMenuItem.Click
        Try
            Dim OBJLEDGER As New LedgerFilter
            OBJLEDGER.MdiParent = Me
            OBJLEDGER.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GSTTAXFILTER_MASTER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GSTTAXFILTER_MASTER.Click
        Try
            Dim OBJTAX As New GSTTaxFilter
            OBJTAX.MdiParent = Me
            OBJTAX.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GroupSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupSummaryToolStripMenuItem.Click
        Try
            Dim OBJGROUP As New GroupRegister
            OBJGROUP.MdiParent = Me
            OBJGROUP.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrialBalanceToolStripMenuItem.Click
        Try
            Dim OBJTB As New TB
            OBJTB.MdiParent = Me
            OBJTB.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ProfitLossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfitLossToolStripMenuItem.Click
        Try
            Dim objpl As New PL
            objpl.MdiParent = Me
            objpl.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BalanceSheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalanceSheetToolStripMenuItem.Click
        Try
            Dim OBJBALANCESHEET As New BS
            OBJBALANCESHEET.MdiParent = Me
            OBJBALANCESHEET.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PAYMENT_TOOL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAYMENT_TOOL.Click
        Try
            Dim OBJPAY As New PaymentMaster
            OBJPAY.MdiParent = Me
            OBJPAY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPADD.Click
        Try
            Dim objGroupMaster As New GroupMaster
            objGroupMaster.MdiParent = Me
            objGroupMaster.Show()
            objGroupMaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub EditExistingGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GROUPEDIT.Click
        Try
            Dim objGroupDetails As New GroupDetails
            objGroupDetails.MdiParent = Me
            objGroupDetails.Show()
            objGroupDetails.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "PAYMENT"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CREDITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewRegisterToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewRegisterToolStripMenuItem2.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "DEBITNOTE"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "JOURNAL"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "CONTRA"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub DefaultRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEFAULTREGADD.Click
        Try
            Dim objDEF As New DefaultRegister
            objDEF.MdiParent = Me
            objDEF.Show()
            objDEF.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PURRETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETADD.Click
        Try
            Dim ObjPurchaseReturn As New PurchaseReturn
            ObjPurchaseReturn.MdiParent = Me
            ObjPurchaseReturn.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub PURRETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PURRETEDIT.Click
        Try
            Dim ObjPRDetails As New PurchaseReturnDetails
            ObjPRDetails.MdiParent = Me
            ObjPRDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETADD.Click
        Try
            Dim ObjSaleReturn As New SaleReturn
            ObjSaleReturn.MdiParent = Me
            ObjSaleReturn.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SALERETEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SALERETEDIT.Click
        Try
            Dim ObjSaleReturnDetails As New SaleReturnDetails
            ObjSaleReturnDetails.MdiParent = Me
            ObjSaleReturnDetails.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CASHENTRYADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHENTRYADD.Click
        Try
            Dim ObjCASHENTRY As New CashEntry
            ObjCASHENTRY.MdiParent = Me
            ObjCASHENTRY.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CASHENTRYEDIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CASHENTRYEDIT.Click
        Try
            Dim ObjCASHENTRYDETAILS As New CashEntryDetails
            ObjCASHENTRYDETAILS.MdiParent = Me
            ObjCASHENTRYDETAILS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InterestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestToolStripMenuItem.Click
        Try
            Dim OBJINTCALC As New InterestCalc
            OBJINTCALC.MdiParent = Me
            OBJINTCALC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadLedgersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadLedgersToolStripMenuItem.Click
        Try
            If InputBox("Enter Master Password") <> "Infosys@123" Then Exit Sub

            '************************************ LEDGER UPLOAD ****************************
            'upload the files data
            ''Reading from Excel Woorkbook
            Dim cPart As Microsoft.Office.Interop.Excel.Range
            Dim oExcel As Microsoft.Office.Interop.Excel.Application = CreateObject("Excel.Application")
            Dim oBook As Microsoft.Office.Interop.Excel.Workbook = oExcel.Workbooks.Open("D:\" & InputBox("Enter File Name").ToString.Trim, , False)
            Dim oSheet As New Microsoft.Office.Interop.Excel.Worksheet
            oSheet = oBook.Worksheets("Sheet1")

            'GRID
            Dim ADDITEM As Boolean = True
            Dim TEMPITEMNAME As String = ""

            Dim DTSAVE As New System.Data.DataTable
            DTSAVE.Columns.Add("CODE")
            DTSAVE.Columns.Add("COMPANYNAME")
            DTSAVE.Columns.Add("ADD1")
            DTSAVE.Columns.Add("ADD2")
            DTSAVE.Columns.Add("ADDRESS")
            DTSAVE.Columns.Add("CITYNAME")
            DTSAVE.Columns.Add("PINNO")
            DTSAVE.Columns.Add("STATE")
            DTSAVE.Columns.Add("COUNTRY")
            DTSAVE.Columns.Add("PHONENO")
            DTSAVE.Columns.Add("MOBILENO")
            DTSAVE.Columns.Add("GSTIN")
            DTSAVE.Columns.Add("GROUPNAME")
            DTSAVE.Columns.Add("PANNO")
            DTSAVE.Columns.Add("BROKER")
            DTSAVE.Columns.Add("TRANSPORT")
            DTSAVE.Columns.Add("EMAIL")
            DTSAVE.Columns.Add("CRDAYS")
            DTSAVE.Columns.Add("SALESMAN")
            DTSAVE.Columns.Add("TDSPER")
            DTSAVE.Columns.Add("TDSSECTION")
            DTSAVE.Columns.Add("CMPNONCMP")
            DTSAVE.Columns.Add("DISCOUNT")
            DTSAVE.Columns.Add("CASHDISC")
            DTSAVE.Columns.Add("COMMISSION")
            DTSAVE.Columns.Add("SHIPPINGADD")

            Dim ARR As New ArrayList
            Dim COLIND As Integer = 0
            Dim DTROWSAVE As System.Data.DataRow = DTSAVE.NewRow()

            Dim FROMROWNO As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROWNO As Integer = Val(InputBox("Enter End Row No"))

            For I As Integer = FROMROWNO To TOROWNO

                If IsDBNull(oSheet.Range("A" & I.ToString).Text) = False Then
                    DTROWSAVE("CODE") = oSheet.Range("A" & I.ToString).Text
                Else
                    DTROWSAVE("CODE") = ""
                End If

                If IsDBNull(oSheet.Range("B" & I.ToString).Text) = False Then
                    DTROWSAVE("COMPANYNAME") = oSheet.Range("B" & I.ToString).Text
                Else
                    DTROWSAVE("COMPANYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("C" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD1") = oSheet.Range("C" & I.ToString).Text
                Else
                    DTROWSAVE("ADD1") = ""
                End If

                If IsDBNull(oSheet.Range("D" & I.ToString).Text) = False Then
                    DTROWSAVE("ADD2") = oSheet.Range("D" & I.ToString).Text
                Else
                    DTROWSAVE("ADD2") = ""
                End If

                If IsDBNull(oSheet.Range("E" & I.ToString).Text) = False Then
                    DTROWSAVE("ADDRESS") = oSheet.Range("E" & I.ToString).Text
                Else
                    DTROWSAVE("ADDRESS") = ""
                End If

                If IsDBNull(oSheet.Range("F" & I.ToString).Text) = False Then
                    DTROWSAVE("CITYNAME") = oSheet.Range("F" & I.ToString).Text
                Else
                    DTROWSAVE("CITYNAME") = ""
                End If

                If IsDBNull(oSheet.Range("G" & I.ToString).Text) = False Then
                    DTROWSAVE("PINNO") = oSheet.Range("G" & I.ToString).Text
                Else
                    DTROWSAVE("PINNO") = 0
                End If

                If IsDBNull(oSheet.Range("H" & I.ToString).Text) = False Then
                    DTROWSAVE("STATE") = oSheet.Range("H" & I.ToString).Text
                Else
                    DTROWSAVE("STATE") = ""
                End If

                If IsDBNull(oSheet.Range("I" & I.ToString).Text) = False Then
                    DTROWSAVE("COUNTRY") = oSheet.Range("I" & I.ToString).Text
                Else
                    DTROWSAVE("COUNTRY") = ""
                End If

                If IsDBNull(oSheet.Range("J" & I.ToString).Text) = False Then
                    DTROWSAVE("PHONENO") = oSheet.Range("J" & I.ToString).Text
                Else
                    DTROWSAVE("PHONENO") = ""
                End If

                If IsDBNull(oSheet.Range("K" & I.ToString).Text) = False Then
                    DTROWSAVE("MOBILENO") = oSheet.Range("K" & I.ToString).Text
                Else
                    DTROWSAVE("MOBILENO") = 0
                End If


                If IsDBNull(oSheet.Range("L" & I.ToString).Text) = False Then
                    DTROWSAVE("GSTIN") = oSheet.Range("L" & I.ToString).Text
                Else
                    DTROWSAVE("GSTIN") = ""
                End If

                If IsDBNull(oSheet.Range("M" & I.ToString).Text) = False Then
                    DTROWSAVE("GROUPNAME") = oSheet.Range("M" & I.ToString).Text
                Else
                    DTROWSAVE("GROUPNAME") = ""
                End If

                If IsDBNull(oSheet.Range("N" & I.ToString).Text) = False Then
                    DTROWSAVE("PANNO") = oSheet.Range("N" & I.ToString).Text
                Else
                    DTROWSAVE("PANNO") = ""
                End If

                If IsDBNull(oSheet.Range("O" & I.ToString).Text) = False Then
                    DTROWSAVE("BROKER") = oSheet.Range("O" & I.ToString).Text
                Else
                    DTROWSAVE("BROKER") = ""
                End If

                If IsDBNull(oSheet.Range("P" & I.ToString).Text) = False Then
                    DTROWSAVE("TRANSPORT") = oSheet.Range("P" & I.ToString).Text
                Else
                    DTROWSAVE("TRANSPORT") = ""
                End If

                If IsDBNull(oSheet.Range("Q" & I.ToString).Text) = False Then
                    DTROWSAVE("EMAIL") = oSheet.Range("Q" & I.ToString).Text
                Else
                    DTROWSAVE("EMAIL") = ""
                End If

                If IsDBNull(oSheet.Range("R" & I.ToString).Text) = False Then
                    DTROWSAVE("CRDAYS") = oSheet.Range("R" & I.ToString).Text
                Else
                    DTROWSAVE("CRDAYS") = ""
                End If

                If IsDBNull(oSheet.Range("S" & I.ToString).Text) = False Then
                    DTROWSAVE("SALESMAN") = oSheet.Range("S" & I.ToString).Text
                Else
                    DTROWSAVE("SALESMAN") = ""
                End If

                If IsDBNull(oSheet.Range("T" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSPER") = oSheet.Range("T" & I.ToString).Text
                Else
                    DTROWSAVE("TDSPER") = ""
                End If

                If IsDBNull(oSheet.Range("U" & I.ToString).Text) = False Then
                    DTROWSAVE("TDSSECTION") = oSheet.Range("U" & I.ToString).Text
                Else
                    DTROWSAVE("TDSSECTION") = ""
                End If

                If IsDBNull(oSheet.Range("V" & I.ToString).Text) = False Then
                    DTROWSAVE("CMPNONCMP") = oSheet.Range("V" & I.ToString).Text
                Else
                    DTROWSAVE("CMPNONCMP") = ""
                End If

                If IsDBNull(oSheet.Range("W" & I.ToString).Text) = False Then
                    DTROWSAVE("DISCOUNT") = oSheet.Range("W" & I.ToString).Text
                Else
                    DTROWSAVE("DISCOUNT") = ""
                End If

                If IsDBNull(oSheet.Range("X" & I.ToString).Text) = False Then
                    DTROWSAVE("CASHDISC") = oSheet.Range("X" & I.ToString).Text
                Else
                    DTROWSAVE("CASHDISC") = ""
                End If

                If IsDBNull(oSheet.Range("Y" & I.ToString).Text) = False Then
                    DTROWSAVE("COMMISSION") = oSheet.Range("Y" & I.ToString).Text
                Else
                    DTROWSAVE("COMMISSION") = ""
                End If

                If IsDBNull(oSheet.Range("Z" & I.ToString).Text) = False Then
                    DTROWSAVE("SHIPPINGADD") = oSheet.Range("Z" & I.ToString).Text
                Else
                    DTROWSAVE("SHIPPINGADD") = ""
                End If




                Dim ALPARAVAL As New ArrayList
                Dim OBJCMN As New ClsCommon
                Dim DTTABLE As DataTable = OBJCMN.search("CITY_ID AS CITYID", "", "CITYMASTER ", "AND CITY_NAME = '" & DTROWSAVE("CITYNAME") & "' AND CITY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW CITYNAME
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecity(DTROWSAVE("CITYNAME"), CmpId, Locationid, Userid, YearId, " and city_name = '" & DTROWSAVE("CITYNAME") & "' AND CITY_CMPID = " & CmpId & " AND CITY_LOCATIONID = " & Locationid & " AND CITY_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.search("STATE_ID AS STATEID", "", "STATEMASTER ", "AND STATE_NAME = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW STATE
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savestate(DTROWSAVE("STATE"), CmpId, Locationid, Userid, YearId, " and STATE_name = '" & DTROWSAVE("STATE") & "' AND STATE_YEARID = " & YearId)
                End If


                DTTABLE = OBJCMN.search("COUNTRY_ID AS COUNTRYID", "", "COUNTRYMASTER ", "AND COUNTRY_NAME = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                If DTTABLE.Rows.Count = 0 Then
                    'ADD NEW COUNTRY
                    Dim objyearmaster As New ClsYearMaster
                    objyearmaster.savecountry(DTROWSAVE("COUNTRY"), CmpId, Locationid, Userid, YearId, " and COUNTRY_name = '" & DTROWSAVE("COUNTRY") & "' AND COUNTRY_YEARID = " & YearId)
                End If


                'check whether ITEMNAME is already present or not
                DTTABLE = OBJCMN.search("ACC_CMPNAME AS COMPANYNAME", "", "LEDGERS ", " AND ACC_CMPNAME = '" & DTROWSAVE("COMPANYNAME") & "' AND ACC_YEARID = " & YearId)
                If DTTABLE.Rows.Count > 0 Then GoTo SKIPLINE



                'ADD IN ACCOUNTSMASTER
                ALPARAVAL.Clear()
                Dim OBJSM As New ClsAccountsMaster

                ALPARAVAL.Add(DTROWSAVE("COMPANYNAME"))
                ALPARAVAL.Add("")   'NAME
                ALPARAVAL.Add(DTROWSAVE("GROUPNAME"))
                ALPARAVAL.Add(0)    'OPBAL
                ALPARAVAL.Add("Cr.")
                ALPARAVAL.Add(0)    'INTPER
                ALPARAVAL.Add(DTROWSAVE("ADD1"))
                ALPARAVAL.Add(DTROWSAVE("ADD2"))
                ALPARAVAL.Add("")   'AREA
                ALPARAVAL.Add("")   'STD
                ALPARAVAL.Add(DTROWSAVE("CITYNAME"))
                ALPARAVAL.Add(DTROWSAVE("PINNO"))
                ALPARAVAL.Add(DTROWSAVE("STATE"))
                ALPARAVAL.Add(DTROWSAVE("COUNTRY"))
                ALPARAVAL.Add(Val(DTROWSAVE("CRDAYS")))
                ALPARAVAL.Add(0)    'CRLIMIT
                ALPARAVAL.Add("")   'RESI
                ALPARAVAL.Add("")   'ALT
                ALPARAVAL.Add(DTROWSAVE("PHONENO"))
                ALPARAVAL.Add(DTROWSAVE("MOBILENO"))
                ALPARAVAL.Add("")   'boss mobile
                ALPARAVAL.Add("")   'FAX
                ALPARAVAL.Add("")   'WEBSITE
                ALPARAVAL.Add(DTROWSAVE("EMAIL"))   'EMAIL

                ALPARAVAL.Add("")   'STREET

                ALPARAVAL.Add(0)   'BANK CHK
                ALPARAVAL.Add("")   'PARTY BANK
                ALPARAVAL.Add("")   'ACCNO
                ALPARAVAL.Add("")   'IFSC
                ALPARAVAL.Add("")   'BRANCH

                ALPARAVAL.Add(0)   'BANK CHK1
                ALPARAVAL.Add("")   'PARTY BANK1
                ALPARAVAL.Add("")   'ACCNO1
                ALPARAVAL.Add("")   'IFSC1
                ALPARAVAL.Add("")   'BRANCH1


                ALPARAVAL.Add(DTROWSAVE("TRANSPORT"))   'TRANS
                ALPARAVAL.Add(DTROWSAVE("BROKER"))   'AGENT
                ALPARAVAL.Add(Val(DTROWSAVE("COMMISSION")))    'AGENTCOM
                ALPARAVAL.Add(Val(DTROWSAVE("DISCOUNT")))    'DISC

                ALPARAVAL.Add(DTROWSAVE("PANNO"))   'PAN
                ALPARAVAL.Add("")   'EXISE
                ALPARAVAL.Add("")   'LANDMARK
                ALPARAVAL.Add("")   'ADDLESS
                ALPARAVAL.Add("")   'CST
                ALPARAVAL.Add("")   'TIN
                ALPARAVAL.Add("")   'ST
                ALPARAVAL.Add("")   'DISTRICT
                ALPARAVAL.Add("")   'VIA
                ALPARAVAL.Add("")   'VAT
                ALPARAVAL.Add(DTROWSAVE("GSTIN"))
                ALPARAVAL.Add("")   'REGISTER
                ALPARAVAL.Add(DTROWSAVE("ADDRESS"))
                ALPARAVAL.Add(DTROWSAVE("SHIPPINGADD"))   'SHIPADD
                ALPARAVAL.Add("")   'REMARKS
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(Userid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(0)    'TRANSFER
                ALPARAVAL.Add(DTROWSAVE("CODE"))


                'TDS
                '*******************************
                ALPARAVAL.Add(0)    'ISTDS
                ALPARAVAL.Add("")   'DEDUCTEETYPER
                ALPARAVAL.Add(0)    'ISLOWER

                ALPARAVAL.Add(DTROWSAVE("TDSSECTION"))   'SECTION
                ALPARAVAL.Add("")   'TDSFORM
                ALPARAVAL.Add(Val(0))   'TDSRATE
                ALPARAVAL.Add(Val(DTROWSAVE("TDSPER")))    'TDSPER
                ALPARAVAL.Add(0) 'SURCHARGE
                ALPARAVAL.Add(0) 'LIMIT
                '*******************************

                ALPARAVAL.Add(0)    'TDSAC
                ALPARAVAL.Add(DTROWSAVE("CMPNONCMP"))   'NATUREOFPAY
                ALPARAVAL.Add("ACCOUNTS")   'TYPE

                ALPARAVAL.Add("")   'FORNTYPE

                ALPARAVAL.Add("")   'CNAME
                ALPARAVAL.Add("")   'CDOB
                ALPARAVAL.Add("")   'CDEIGNATION
                ALPARAVAL.Add("")   'CMOB
                ALPARAVAL.Add("")   'CEMAIL


                OBJSM.alParaval = ALPARAVAL
                Dim INTRES As Integer = OBJSM.save()

                DTROWSAVE = DTSAVE.NewRow()

SKIPLINE:
            Next

            oBook.Close()

            Exit Sub

            '************************************ END OF CODE FOR LEDGER UPLOAD ****************************



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PATTAADD_Click(sender As Object, e As EventArgs) Handles PATTAADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "PATTA"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub PATTAEDIT_Click(sender As Object, e As EventArgs) Handles PATTAEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "PATTA"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SIZEADD_Click(sender As Object, e As EventArgs) Handles SIZEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "SIZE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SIZEEDIT_Click(sender As Object, e As EventArgs) Handles SIZEEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "SIZE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub ITEMAVGCONFIG_MASTER_Click(sender As Object, e As EventArgs) Handles ITEMAVGCONFIG_MASTER.Click
        Try
            Dim OBJCONFIG As New ItemAvgConfig
            OBJCONFIG.MdiParent = Me
            OBJCONFIG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LABOURISSADD_Click(sender As Object, e As EventArgs) Handles LABOURISSADD.Click
        Try
            Dim OBJISS As New LabourIssue
            OBJISS.MdiParent = Me
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LABOURISSEDIT_Click(sender As Object, e As EventArgs) Handles LABOURISSEDIT.Click
        Try
            Dim OBJISS As New LabourIssueDetails
            OBJISS.MdiParent = Me
            OBJISS.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LABOURRECADD_Click(sender As Object, e As EventArgs) Handles LABOURRECADD.Click
        Try
            Dim OBJREC As New LabourReceipt
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LABOURRECEDIT_Click(sender As Object, e As EventArgs) Handles LABOURRECEDIT.Click
        Try
            Dim OBJREC As New LabourReceiptDetails
            OBJREC.MdiParent = Me
            OBJREC.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContractorRateConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContractorRateConfigToolStripMenuItem.Click
        Try
            Dim OBJCONFIG As New ContractorRateConfig
            OBJCONFIG.MdiParent = Me
            OBJCONFIG.FRMSTRING = "RATE"
            OBJCONFIG.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        Try
            Dim objregistermaster As New RegisterMaster
            objregistermaster.MdiParent = Me
            objregistermaster.frmstring = "SALE RETURN"
            objregistermaster.Show()
            objregistermaster.BringToFront()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem.Click
        Try
            Dim OBJCONT As New ContractorIssue
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        Try
            Dim OBJCONT As New ContractorIssueDetails
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AddNewEntryToolStripMenuItem1.Click
        Try
            Dim OBJCONT As New ContractorReceipt
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click
        Try
            Dim OBJCONT As New ContractorReceiptDetails
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRACTORISS_TOOL_Click(sender As Object, e As EventArgs) Handles CONTRACTORISS_TOOL.Click
        Try
            Dim OBJCONT As New ContractorIssue
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CONTRACTORREC_TOOL_Click(sender As Object, e As EventArgs) Handles CONTRACTORREC_TOOL.Click
        Try
            Dim OBJCONT As New ContractorReceipt
            OBJCONT.MdiParent = Me
            OBJCONT.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSUEVOUCHERADD_Click(sender As Object, e As EventArgs) Handles ISSUEVOUCHERADD.Click
        Try
            Dim OBJISSUE As New IssueVoucher
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ISSUEVOUCHEREDIT_Click(sender As Object, e As EventArgs) Handles ISSUEVOUCHEREDIT.Click
        Try
            Dim OBJISSUE As New IssueVoucherDetails
            OBJISSUE.MdiParent = Me
            OBJISSUE.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HALLMARKADD_Click(sender As Object, e As EventArgs) Handles HALLMARKADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "HALLMARK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub HALLMARKEDIT_Click(sender As Object, e As EventArgs) Handles HALLMARKEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "HALLMARK"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub UPDATEHUID_MASTER_Click(sender As Object, e As EventArgs) Handles UPDATEHUID_MASTER.Click
        Try
            Dim OBJHUID As New UpdateHUID
            OBJHUID.MdiParent = Me
            OBJHUID.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CATEGORYADD_Click(sender As Object, e As EventArgs) Handles CATEGORYADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "CATEGORY"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CATEGORYEDIT_Click(sender As Object, e As EventArgs) Handles CATEGORYEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "CATEGORY"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SUBCATEGORYADD_Click(sender As Object, e As EventArgs) Handles SUBCATEGORYADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "SUBCATEGORY"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SUBCATEGORYEDIT_Click(sender As Object, e As EventArgs) Handles SUBCATEGORYEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "SUBCATEGORY"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub MAKEADD_Click(sender As Object, e As EventArgs) Handles MAKEADD.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.frmString = "MAKE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub MAKEEDIT_Click(sender As Object, e As EventArgs) Handles MAKEEDIT.Click
        Try
            Dim objCategory As New CategoryDetails
            objCategory.frmString = "MAKE"
            objCategory.MdiParent = Me
            objCategory.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub SALEADD_Click(sender As Object, e As EventArgs) Handles SALEADD.Click
        Try
            If ClientName = "NAMASKAR" Or ClientName = "HMENTERPRISE" Then
                Dim OBJINV As New MaterialOutward
                OBJINV.MdiParent = Me
                OBJINV.Show()
            Else
                Dim OBJINV As New InvoiceMaster
                OBJINV.MdiParent = Me
                OBJINV.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SALECONSUMPTIONADD.Click
        Try
            If ClientName = "NAMASKAR" Then
                Dim OBJINV As New MaterialConsumption
                OBJINV.MdiParent = Me
                OBJINV.Show()
            Else
                Dim OBJINV As New InvoiceMaster
                OBJINV.MdiParent = Me
                OBJINV.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EditExistingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SALECONSUMPTIONEDIT.Click
        Try
            If ClientName = "NAMASKAR" Then
                Dim OBJINV As New MaterialConsumptionDetails
                OBJINV.MdiParent = Me
                OBJINV.Show()
            Else
                Dim OBJINV As New InvoiceMaster
                OBJINV.MdiParent = Me
                OBJINV.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
