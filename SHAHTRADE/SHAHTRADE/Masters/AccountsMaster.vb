
Imports BL
Imports System.Windows.Forms

Public Class AccountsMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public frmstring As String        'Used from Displaying Customer, Vendor, Employee Master
    Public edit As Boolean
    Public tempAccountsName As String
    Public TEMPGROUPNAME As String
    Public tempAccountsCode As String
    Public tempTYPE As String = "ACCOUNTS"
    Public tempISAGENT As String = ""
    Public tempISTRANS As String = ""
    Dim tempAccountId As Integer
    Dim TEMPCONTACTROW As Integer
    Dim gridContactDoubleClick As Boolean

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub AccountsMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub FILLCMPNAME()
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable

            If frmstring = "CUSTOMER" Then

                dt = objclscommon.search("customer_cmpname", "", " CUSTOMERMASTER ", " and CUSTOMER_cmpid = " & CmpId & " and CUSTOMER_locationid = " & Locationid & " and CUSTOMER_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "customer_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "customer_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "VENDOR" Then

                dt = objclscommon.search("VENDOR_cmpname", "", " VENDORMaster ", " and VENDOR_cmpid = " & CmpId & " and VENDOR_locationid = " & Locationid & " and VENDOR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "VENDOR_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "VENDOR_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "ACCOUNTS" Then

                dt = objclscommon.search("acc_cmpname", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "acc_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "acc_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            ElseIf frmstring = "EMPLOYEE" Then

                dt = objclscommon.search("EMP_cmpname", "", " EMPLOYEEMaster ", " and EMP_cmpid = " & CmpId & " and EMP_locationid = " & Locationid & " and EMP_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMP_cmpname"
                    cmbcmpname.DataSource = dt
                    cmbcmpname.DisplayMember = "EMP_cmpname"
                    cmbcmpname.Text = tempAccountsName
                End If

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub FILLCMPCODE()
        Try
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable

            If frmstring = "CUSTOMER" Then

                dt = objclscommon.search("customer_CODE", "", " CUSTOMERMASTER ", " and CUSTOMER_cmpid = " & CmpId & " and CUSTOMER_locationid = " & Locationid & " and CUSTOMER_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "customer_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "customer_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "VENDOR" Then

                dt = objclscommon.search("VENDOR_CODE", "", " VENDORMaster ", " and VENDOR_cmpid = " & CmpId & " and VENDOR_locationid = " & Locationid & " and VENDOR_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "VENDOR_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "VENDOR_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "ACCOUNTS" Then

                dt = objclscommon.search("acc_CODE", "", " ACCOUNTSMaster ", " and acc_cmpid = " & CmpId & " and acc_locationid = " & Locationid & " and acc_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "acc_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "acc_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            ElseIf frmstring = "EMPLOYEE" Then

                dt = objclscommon.search("EMP_CODE", "", " EMPLOYEEMaster ", " and EMP_cmpid = " & CmpId & " and EMP_locationid = " & Locationid & " and EMP_yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "EMP_CODE"
                    CMBCODE.DataSource = dt
                    CMBCODE.DisplayMember = "EMP_CODE"
                    CMBCODE.Text = tempAccountsCode
                End If

            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()

        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable

        dt = objclscommon.search("area_name", "", "AreaMaster", " and area_cmpid =" & CmpId & " and area_Locationid =" & Locationid & " and area_Yearid =" & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "area_name"
            cmbarea.DataSource = dt
            cmbarea.DisplayMember = "area_name"
            cmbarea.Text = ""
        End If

        dt = objclscommon.search("street_name", "", "STREETMaster", " and street_Yearid =" & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "street_name"
            CMBSTREET.DataSource = dt
            CMBSTREET.DisplayMember = "street_name"
            CMBSTREET.Text = ""
        End If

        dt = objclscommon.search("city_name", "", "CityMaster", " and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "City_name"
            cmbcity.DataSource = dt
            cmbcity.DisplayMember = "city_name"
            cmbcity.Text = ""
        End If

        dt = objclscommon.search("country_name", "", "CountryMaster", " and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "country_name"
            cmbcountry.DataSource = dt
            cmbcountry.DisplayMember = "country_name"
            cmbcountry.Text = ""
        End If

        dt = objclscommon.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "Group_name"
            cmbgroup.DataSource = dt
            cmbgroup.DisplayMember = "group_name"
            cmbgroup.Text = ""
        End If

        dt = objclscommon.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_yearid = " & YearId)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "state_name"
            cmbstate.DataSource = dt
            cmbstate.DisplayMember = "state_name"
            cmbstate.Text = ""
        End If
        If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'  AND LEDGERS.ACC_TYPE='TRANSPORT'")

        If CMBDISTRICT.Text = "" Then fillDISTRICT(CMBDISTRICT)
        If CMBVIA.Text = "" Then fillVIA(CMBVIA)

        If CMBPARTYBANK.Text = "" Then fillPARTYBANK(CMBPARTYBANK, edit)
        If CMBPARTYBANK.Text = "" Then fillPARTYBANK(CMBPARTYBANK1, edit)

    End Sub

    Private Sub AccountsMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            FILLCMPNAME()
            FILLCMPCODE()

            cmbgroup.Text = TEMPGROUPNAME
            CMBTYPE.Text = tempTYPE
            CHKTDSAPP.CheckState = CheckState.Checked


            If edit = True Then

                Dim OBJACC As New ClsAccountsMaster
                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(tempAccountsName)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)

                OBJACC.alParaval = ALPARAVAL

                Dim objCommon As New ClsCommonMaster
                Dim dttable As DataTable = OBJACC.GETACCOUNTS


                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If dttable.Rows.Count > 0 Then

                    If Convert.ToBoolean(dttable.Rows(0).Item("LOCKED")) = True Then cmbcmpname.Enabled = False

                    tempAccountId = Val(dttable.Rows(0).Item("ACCID"))
                    cmbcmpname.Text = dttable.Rows(0).Item("CMPNAME").ToString
                    txtname.Text = dttable.Rows(0).Item("NAME").ToString
                    cmbgroup.Text = dttable.Rows(0).Item("GROUPNAME").ToString

                    txtopbal.Text = Val(dttable.Rows(0).Item("OPBAL").ToString)
                    cmbdrcrrs.Text = dttable.Rows(0).Item("DRCR").ToString
                    TXTINTPER.Text = Val(dttable.Rows(0).Item("INTPER"))

                    CMBDEDUCTEETYPE.Text = dttable.Rows(0).Item("DEDUCTEETYPE")

                    txtadd1.Text = dttable.Rows(0).Item("ADD1").ToString
                    txtadd2.Text = dttable.Rows(0).Item("ADD2").ToString
                    cmbarea.Text = dttable.Rows(0).Item("AREA").ToString
                    CMBSTREET.Text = dttable.Rows(0).Item("STREET").ToString
                    cmbcity.Text = dttable.Rows(0).Item("CITY").ToString
                    cmbstate.Text = dttable.Rows(0).Item("STATE").ToString
                    txtzipcode.Text = dttable.Rows(0).Item("ZIPCODE").ToString
                    cmbcountry.Text = dttable.Rows(0).Item("COUNTRY").ToString
                    txtstd.Text = dttable.Rows(0).Item("STD").ToString
                    txtresino.Text = dttable.Rows(0).Item("RESINO").ToString
                    txtaltno.Text = dttable.Rows(0).Item("ALTNO").ToString
                    txttel1.Text = dttable.Rows(0).Item("PHONE").ToString
                    txtmobile.Text = dttable.Rows(0).Item("MOBILE").ToString
                    TXTBOSSMOBILE.Text = dttable.Rows(0).Item("BOSSMOBILENO").ToString
                    txtfax.Text = dttable.Rows(0).Item("FAX").ToString
                    TXTKMS.Text = Val(dttable.Rows(0).Item("KMS"))
                    txtwebsite.Text = dttable.Rows(0).Item("WEBSITE").ToString
                    TXTEMAIL.Text = dttable.Rows(0).Item("EMAIL").ToString
                    CMBTRANS.Text = dttable.Rows(0).Item("TRANS").ToString
                    CMBAGENT.Text = dttable.Rows(0).Item("AGENT").ToString
                    TXTAGENTCOMM.Text = dttable.Rows(0).Item("AGENTCOMM").ToString
                    TXTDISC.Text = dttable.Rows(0).Item("DISC").ToString
                    txtcrdays.Text = Val(dttable.Rows(0).Item("CRDAYS").ToString)
                    txtcrlimit.Text = Val(dttable.Rows(0).Item("CRLIMIT").ToString)
                    txtpanno.Text = dttable.Rows(0).Item("PANNO").ToString
                    txtexciseno.Text = dttable.Rows(0).Item("EXCISENO").ToString
                    TXTLANDMARK.Text = dttable.Rows(0).Item("RANGE").ToString
                    CMBADDLESS.Text = dttable.Rows(0).Item("ADDLESS").ToString
                    txtcstno.Text = dttable.Rows(0).Item("CSTNO").ToString
                    txttinno.Text = dttable.Rows(0).Item("TINNO").ToString
                    txtstno.Text = dttable.Rows(0).Item("STNO").ToString
                    CMBDISTRICT.Text = dttable.Rows(0).Item("DISTRICT").ToString
                    CMBVIA.Text = dttable.Rows(0).Item("VIA").ToString
                    txtvatno.Text = dttable.Rows(0).Item("VATNO").ToString
                    TXTGSTIN.Text = dttable.Rows(0).Item("GSTIN").ToString
                    cmbregister.Text = dttable.Rows(0).Item("REGISTERNAME").ToString
                    txtadd.Text = dttable.Rows(0).Item("ADD").ToString
                    txtshipadd.Text = dttable.Rows(0).Item("SHIPPINGADD").ToString
                    txtremarks.Text = dttable.Rows(0).Item("REMARKS").ToString
                    CMBCODE.Text = dttable.Rows(0).Item("CODE").ToString
                    tempAccountsCode = dttable.Rows(0).Item("CODE").ToString

                    CHKBANK.Checked = Convert.ToBoolean(dttable.Rows(0).Item("ISBANK"))
                    CMBPARTYBANK.Text = dttable.Rows(0).Item("BANK").ToString
                    TXTACCNO.Text = dttable.Rows(0).Item("ACCOUNTNO").ToString
                    TXTIFSC.Text = dttable.Rows(0).Item("IFSC").ToString
                    TXTBRANCH.Text = dttable.Rows(0).Item("BRANCH").ToString


                    CHKBANK1.Checked = Convert.ToBoolean(dttable.Rows(0).Item("ISBANK1"))
                    CMBPARTYBANK1.Text = dttable.Rows(0).Item("BANK1").ToString
                    TXTACCNO1.Text = dttable.Rows(0).Item("ACCOUNTNO1").ToString
                    TXTIFSC1.Text = dttable.Rows(0).Item("IFSC1").ToString
                    TXTBRANCH1.Text = dttable.Rows(0).Item("BRANCH1").ToString


                    CMBTDSFORM.Text = dttable.Rows(0).Item("TDSFORM")
                    If Val(dttable.Rows(0).Item("TDSPER")) > 0 Then
                        CHKTDSAPP.CheckState = CheckState.Checked
                        If dttable.Rows(0).Item("ISLOWER") = True Then
                            CMBISLOWER.Text = "Yes"
                            CMBSECTION.Enabled = True
                            CMBTDSFORM.Enabled = True
                        Else
                            CMBISLOWER.Text = "No"
                        End If
                        CMBSECTION.Text = dttable.Rows(0).Item("SECTION")

                        TXTTDSRATE.Text = Format(Val(dttable.Rows(0).Item("TDSRATE")), "0.00")
                        TXTTDSPER.Text = Format(Val(dttable.Rows(0).Item("TDSPER")), "0.00")
                        If CMBSECTION.Text.Trim = "197" Then
                            TXTTDSRATE.Enabled = True
                        Else
                            TXTTDSRATE.Enabled = False
                        End If

                        If dttable.Rows(0).Item("SURCHARGE") = True Then
                            CMBSURCHARGE.Text = "Yes"
                        Else
                            CMBSURCHARGE.Text = "No"
                        End If
                        TXTLIMIT.Text = dttable.Rows(0).Item("LIMIT")
                    Else
                        CHKTDSAPP.CheckState = CheckState.Unchecked
                    End If

                    CHKTDS.Checked = Convert.ToBoolean(dttable.Rows(0).Item("TDSAC"))
                    CMBNATURE.Text = dttable.Rows(0).Item("NATURE")

                    CMBTYPE.Text = dttable.Rows(0).Item("TYPE")

                End If

                'Dim OBJCOMMON As New ClsCommon
                dttable = objCommon.search(" ISNULL(FORMTYPE.FORM_NAME, '') AS FORMNAME", "", " ACCOUNTSMASTER_FORMTYPE INNER JOIN LEDGERS ON ACCOUNTSMASTER_FORMTYPE.Acc_id = LEDGERS.Acc_id AND ACCOUNTSMASTER_FORMTYPE.Acc_yearid = LEDGERS.Acc_yearid LEFT OUTER JOIN FORMTYPE ON ACCOUNTSMASTER_FORMTYPE.Acc_formid = FORMTYPE.FORM_ID AND ACCOUNTSMASTER_FORMTYPE.Acc_yearid = FORMTYPE.FORM_YEARID", " AND LEDGERS.Acc_cmpname = '" & cmbcmpname.Text & "' AND ACCOUNTSMASTER_FORMTYPE.Acc_yearid = " & YearId)
                If dttable.Rows.Count > 0 Then
                    For Each ROW As DataRow In dttable.Rows
                        For I As Integer = 0 To CHKFORMBOX.Items.Count
                            Dim DTR As DataRowView = CHKFORMBOX.Items(I)
                            If ROW("FORMNAME") = DTR.Item(0) Then
                                CHKFORMBOX.SetItemCheckState(I, CheckState.Checked)
                            End If
                        Next
                    Next
                End If

                'FOR CONTACT
                Dim DT As DataTable = objCommon.search("  ACCOUNTSMASTER_CONTACTDESC.ACC_CONTACT AS NAME, DESIGNATIONMASTER.DESIGNATION_NAME AS DESIGNATION, ACCOUNTSMASTER_CONTACTDESC.ACC_DOB AS DOB, ACCOUNTSMASTER_CONTACTDESC.ACC_MOBILE AS MOBILENO, ACCOUNTSMASTER_CONTACTDESC.ACC_EMAIL AS EMAIL ", "", " DESIGNATIONMASTER INNER JOIN ACCOUNTSMASTER_CONTACTDESC ON DESIGNATIONMASTER.DESIGNATION_ID = ACCOUNTSMASTER_CONTACTDESC.ACC_DESIGNATIONID AND DESIGNATIONMASTER.DESIGNATION_CMPID = ACCOUNTSMASTER_CONTACTDESC.ACC_CMPID AND DESIGNATIONMASTER.DESIGNATION_LOCATIONID = ACCOUNTSMASTER_CONTACTDESC.ACC_LOCATIONID AND DESIGNATIONMASTER.DESIGNATION_YEARID = ACCOUNTSMASTER_CONTACTDESC.ACC_YEARID", " AND ACC_ID = " & tempAccountId & " AND ACC_CMPID = " & CmpId & " AND ACC_LOCATIONID = " & Locationid & " AND ACC_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    For Each DTR As DataRow In DT.Rows
                        If Convert.ToDateTime(DTR("DOB")).Date <> "01/01/1900" Then
                            GRIDCONTACT.Rows.Add(DTR("NAME"), Format(DTR("DOB"), "dd/MM/yyyy"), DTR("DESIGNATION"), DTR("MOBILENO"), DTR("EMAIL"))
                        Else
                            GRIDCONTACT.Rows.Add(DTR("NAME"), "__/__/____", DTR("DESIGNATION"), DTR("MOBILENO"), DTR("EMAIL"))
                        End If
                    Next
                End If


            End If

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub add()
        txtadd.Clear()
        If txtadd1.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd1.Text.Trim & vbNewLine
        If txtadd2.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd2.Text.Trim & vbNewLine

        If cmbarea.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & cmbarea.Text.Trim

        txtadd.Text = txtadd.Text & "" & cmbcity.Text.Trim

        If txtzipcode.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & " - " & txtzipcode.Text.Trim & vbNewLine
        Else
            txtadd.Text = txtadd.Text & vbNewLine
        End If

        If cmbstate.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & "" & cmbstate.Text.Trim
        Else
            txtadd.Text = txtadd.Text & vbNewLine
        End If

        If cmbcountry.Text.Trim <> "" Then
            txtadd.Text = txtadd.Text & " " & cmbcountry.Text.Trim
        End If
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList
            alParaval.Add(UCase(cmbcmpname.Text.Trim))
            alParaval.Add(txtname.Text.Trim)
            alParaval.Add(cmbgroup.Text.Trim)
            alParaval.Add(txtopbal.Text.Trim)
            alParaval.Add(cmbdrcrrs.Text.Trim)

            alParaval.Add(Val(TXTINTPER.Text.Trim))

            alParaval.Add(txtadd1.Text.Trim)
            alParaval.Add(txtadd2.Text.Trim)
            alParaval.Add(cmbarea.Text.Trim)
            alParaval.Add(txtstd.Text.Trim)
            alParaval.Add(cmbcity.Text.Trim)
            alParaval.Add(txtzipcode.Text.Trim)
            alParaval.Add(cmbstate.Text.Trim)
            alParaval.Add(cmbcountry.Text.Trim)
            alParaval.Add(txtcrdays.Text.Trim)
            alParaval.Add(txtcrlimit.Text.Trim)
            alParaval.Add(txtresino.Text.Trim)
            alParaval.Add(txtaltno.Text.Trim)
            alParaval.Add(txttel1.Text.Trim)
            alParaval.Add(txtmobile.Text.Trim)
            alParaval.Add(TXTBOSSMOBILE.Text.Trim)
            alParaval.Add(txtfax.Text.Trim)
            alParaval.Add(Val(TXTKMS.Text.Trim))

            alParaval.Add(txtwebsite.Text.Trim)
            alParaval.Add(TXTEMAIL.Text.Trim)

            alParaval.Add(CMBSTREET.Text.Trim)

            alParaval.Add(CHKBANK.CheckState)
            alParaval.Add(CMBPARTYBANK.Text.Trim)
            alParaval.Add(TXTACCNO.Text.Trim)
            alParaval.Add(TXTIFSC.Text.Trim)
            alParaval.Add(TXTBRANCH.Text.Trim)

            alParaval.Add(CHKBANK1.CheckState)
            alParaval.Add(CMBPARTYBANK1.Text.Trim)
            alParaval.Add(TXTACCNO1.Text.Trim)
            alParaval.Add(TXTIFSC1.Text.Trim)
            alParaval.Add(TXTBRANCH1.Text.Trim)


            alParaval.Add(CMBTRANS.Text.Trim)
            alParaval.Add(CMBAGENT.Text.Trim)
            alParaval.Add(Val(TXTAGENTCOMM.Text.Trim))
            alParaval.Add(Val(TXTDISC.Text.Trim))

            alParaval.Add(txtpanno.Text.Trim)
            alParaval.Add(txtexciseno.Text.Trim)
            alParaval.Add(TXTLANDMARK.Text.Trim)
            alParaval.Add(CMBADDLESS.Text.Trim)
            alParaval.Add(txtcstno.Text.Trim)
            alParaval.Add(txttinno.Text.Trim)
            alParaval.Add(txtstno.Text.Trim)
            alParaval.Add(CMBDISTRICT.Text.Trim)
            alParaval.Add(CMBVIA.Text.Trim)
            alParaval.Add(txtvatno.Text.Trim)
            alParaval.Add(TXTGSTIN.Text.Trim)
            alParaval.Add(cmbregister.Text.Trim)
            alParaval.Add(txtadd.Text.Trim)
            alParaval.Add(txtshipadd.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)
            alParaval.Add(CMBCODE.Text.Trim)

            'TDS
            '*******************************
            alParaval.Add(1)    'CHKTDSAPP
            alParaval.Add(CMBDEDUCTEETYPE.Text.Trim)

            If CMBISLOWER.Text.Trim = "Yes" Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If

            alParaval.Add(CMBSECTION.Text.Trim)
            alParaval.Add(CMBTDSFORM.Text.Trim)
            alParaval.Add(Val(TXTTDSRATE.Text.Trim))
            alParaval.Add(Val(TXTTDSPER.Text.Trim))

            If CMBSURCHARGE.Text.Trim = "Yes" Then
                alParaval.Add(1)
            Else
                alParaval.Add(0)
            End If
            alParaval.Add(Val(TXTLIMIT.Text.Trim))
            '*******************************



            alParaval.Add(CHKTDS.CheckState)
            alParaval.Add(CMBNATURE.Text.Trim)
            alParaval.Add(CMBTYPE.Text.Trim)


            'ADDING FORMTYPE
            Dim FORMTYPE As String = ""
            For Each DTROW As DataRowView In CHKFORMBOX.CheckedItems
                If FORMTYPE = "" Then
                    FORMTYPE = DTROW.Item(0)
                Else
                    FORMTYPE = FORMTYPE & "|" & DTROW.Item(0)
                End If
            Next
            alParaval.Add(FORMTYPE)


            'CONTACT DETAILS
            '********************************
            Dim CNAME As String = ""
            Dim CDOB As String = ""
            Dim DESIGNATION As String = ""
            Dim CMOB As String = ""
            Dim CEMAIL As String = ""

            For Each row As Windows.Forms.DataGridViewRow In GRIDCONTACT.Rows
                If row.Cells(GNAME.Index).Value <> Nothing Then
                    If CNAME = "" Then

                        CNAME = row.Cells(GNAME.Index).Value.ToString
                        If row.Cells(GDOB.Index).Value <> "__/__/____" Then
                            CDOB = Format(Convert.ToDateTime(row.Cells(GDOB.Index).Value).Date, "MM/dd/yyyy")
                        Else
                            CDOB = ""
                        End If
                        DESIGNATION = row.Cells(GDESIGNATION.Index).Value
                        CMOB = row.Cells(GMOBILENO.Index).Value.ToString
                        CEMAIL = row.Cells(GEMAIL.Index).Value

                    Else

                        CNAME = CNAME & "," & row.Cells(GNAME.Index).Value.ToString
                        If row.Cells(GDOB.Index).Value <> "__/__/____" Then
                            CDOB = CDOB & "," & Format(Convert.ToDateTime(row.Cells(GDOB.Index).Value).Date, "MM/dd/yyyy")
                        Else
                            CDOB = CDOB & ","
                        End If
                        DESIGNATION = DESIGNATION & "," & row.Cells(GDESIGNATION.Index).Value
                        CMOB = CMOB & "," & row.Cells(GMOBILENO.Index).Value.ToString
                        CEMAIL = CEMAIL & "," & row.Cells(GEMAIL.Index).Value

                    End If
                End If
            Next

            alParaval.Add(CNAME)
            alParaval.Add(CDOB)
            alParaval.Add(DESIGNATION)
            alParaval.Add(CMOB)
            alParaval.Add(CEMAIL)
            '********************************


            Dim objAccountsMaster As New ClsAccountsMaster
            objAccountsMaster.alParaval = alParaval
            objAccountsMaster.frmstring = frmstring

            If edit = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = objAccountsMaster.save()
                MsgBox("Details Added")
            Else
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(tempAccountId)
                IntResult = objAccountsMaster.update()
                MsgBox("Details Updated")
            End If

            clear()
            cmbcmpname.Focus()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub clear()

        'CONTACT GRID

        TXTCNAME.Clear()
        DTPCDOB.Clear()
        CMBDESIGNATION.Text = ""
        TXTCMOB.Clear()
        TXTCEMAIL.Clear()

        GRIDCONTACT.RowCount = 0

        'TDS
        CHKTDS.CheckState = CheckState.Unchecked
        CMBNATURE.Text = ""

        CHKTDSAPP.CheckState = CheckState.Unchecked
        GROUPTDS.Enabled = False
        CMBDEDUCTEETYPE.Text = ""
        CMBISLOWER.SelectedIndex = 0
        CMBSECTION.SelectedIndex = 0
        CMBTDSFORM.SelectedIndex = 0
        TXTTDSRATE.Clear()
        TXTTDSPER.Clear()
        CMBSURCHARGE.SelectedIndex = 0
        TXTTDSRATE.Enabled = False
        CMBSECTION.Enabled = False
        CMBTDSFORM.Enabled = False

        TXTINTPER.Clear()

        txtadd.Clear()
        txtadd1.Clear()
        txtadd2.Clear()
        txtaltno.Clear()
        cmbcmpname.Text = ""
        txtcrdays.Clear()
        txtcrlimit.Clear()
        txtcstno.Clear()
        CMBADDLESS.Text = ""

        TXTEMAIL.Clear()
        CMBTRANS.Text = ""
        CMBAGENT.Text = ""
        TXTAGENTCOMM.Clear()

        txtexciseno.Clear()
        txtfax.Clear()
        TXTGSTIN.Clear()
        txtmobile.Clear()
        TXTBOSSMOBILE.Clear()
        txtname.Clear()
        txtopbal.Clear()
        txtpanno.Clear()
        TXTLANDMARK.Clear()
        txtremarks.Clear()
        txtresino.Clear()
        txtshipadd.Clear()
        txtstd.Clear()
        txtstno.Clear()
        txttel1.Clear()
        txttinno.Clear()
        txtvatno.Clear()
        txtwebsite.Clear()
        txtzipcode.Clear()
        TXTKMS.Clear()
        cmbarea.Text = ""
        CMBSTREET.Text = ""

        CHKBANK.CheckState = CheckState.Checked
        TXTBANK.Clear()
        CMBPARTYBANK.Text = ""
        TXTACCNO.Clear()
        TXTIFSC.Clear()
        TXTBRANCH.Clear()


        CHKBANK1.CheckState = CheckState.Unchecked
        CMBPARTYBANK1.Text = ""
        TXTACCNO1.Clear()
        TXTIFSC1.Clear()
        TXTBRANCH1.Clear()


        cmbcity.Text = ""
        cmbcountry.Text = ""
        cmbdrcrrs.Text = ""
        cmbgroup.Text = ""
        cmbstate.Text = ""
        CMBCODE.Text = ""
        tempAccountsName = ""
        cmbcmpname.Text = ""
        CMBDISTRICT.Text = ""
        CMBVIA.Text = ""
        edit = False

        'cmbregister.DataSource = Nothing

        If frmstring = "CUSTOMER" Then
            Me.Text = "Customer Master"
            cmbgroup.Text = "Sundry Debtors"
        ElseIf frmstring = "VENDOR" Then
            Me.Text = "Supplier Master"
            cmbgroup.Text = "Sundry Creditors"
        ElseIf frmstring = "ACCOUNTS" Then
            Me.Text = "Accounts Master"
        ElseIf frmstring = "EMPLOYEE" Then
            Me.Text = "Employee Master"
        End If
    End Sub

    Function CHECKDUPLICATE() As Boolean
        Try
            Dim BLN As Boolean = True
            pcase(cmbcmpname)
            CMBCODE.Text = cmbcmpname.Text.Trim
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (edit = False) Or (edit = True And LCase(cmbcmpname.Text) <> LCase(tempAccountsName)) Then
                dt = objclscommon.search("ACC_CMPNAME", "", " LEDGERS", " AND ACC_CMPNAME = '" & cmbcmpname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                If dt.Rows.Count > 0 Then
                    MsgBox("Name Already Exists", MsgBoxStyle.Critical, "EASYBIZ")
                    BLN = False
                End If
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        If cmbcmpname.Text.Trim.Length = 0 Then
            Ep.SetError(cmbcmpname, "Fill Company Name")
            bln = False
        End If

        If Not CHECKDUPLICATE() Then
            Ep.SetError(cmbcmpname, "Name Already Exists")
            bln = False
        End If

        If CMBTYPE.Text.Trim.Length = 0 Then CMBTYPE.Text = "ACCOUNTS"

        If CHKTDS.CheckState = CheckState.Unchecked Then CMBNATURE.Text = ""

        'If CHKTDS.CheckState = CheckState.Checked And CMBNATURE.Text.Trim = "" Then
        '    Ep.SetError(CMBNATURE, "Select Nature Of Payment")
        '    bln = False
        'End If


        If CMBCODE.Text.Trim.Length = 0 Then CMBCODE.Text = cmbcmpname.Text.Trim

        If cmbgroup.Text.Trim.Length = 0 Then
            Ep.SetError(cmbgroup, "Select Group")
            bln = False
        End If
        'If CMBTDSFORM.Text.Trim = "TDS" And Val(TXTTDSPER.Text.Trim) = 0 Then
        '    Ep.SetError(TXTTDSPER, "Enter TDS%")
        '    bln = False
        'End If

        'If CMBTDSFORM.Text.Trim <> "TDS" And Val(TXTTDSPER.Text.Trim) > 0 Then
        '    Ep.SetError(TXTTDSPER, "Remove TDS")
        '    bln = False
        'End If

        If CMBTRANS.Text.Trim = cmbcmpname.Text.Trim Then
            Ep.SetError(CMBTRANS, "Transport Name cannot be same as Company Name")
            bln = False
        End If

        If CMBAGENT.Text.Trim = cmbcmpname.Text.Trim Then
            Ep.SetError(CMBAGENT, "Agent Name cannot be same as Company Name")
            bln = False
        End If





        If txtpanno.Text.Trim <> "" Then

            If CMBDEDUCTEETYPE.Text.Trim = "" Then
                Ep.SetError(txtpanno, "Select Company Type")
                bln = False
            End If

            If txtpanno.Text.Trim.Length <> 10 Then
                Ep.SetError(txtpanno, "Insert Proper PAN No")
                bln = False
            Else
                'validating PAN NO
                For x = 1 To Len(txtpanno.Text.Trim)
                    If x <= 5 Or x = 10 Then
                        If Asc(txtpanno.Text.Substring(x - 1, 1)) < 65 Or Asc(txtpanno.Text.Substring(x - 1, 1)) > 90 Then
                            Ep.SetError(txtpanno, "Insert Proper PAN No")
                            bln = False
                        End If
                    Else
                        If Asc(txtpanno.Text.Substring(x - 1, 1)) < 48 Or Asc(txtpanno.Text.Substring(x - 1, 1)) > 57 Then
                            Ep.SetError(txtpanno, "Insert Proper PAN No")
                            bln = False
                        End If
                    End If
                    'CHECKING 4TH ALPHABET WITH DEDUCTEETYPE
                    'If x = 4 Then
                    '    If CMBDEDUCTEETYPE.Text.Trim = "Individual" Then
                    '        If txtpanno.Text.Substring(x - 1, 1) <> "P" Then
                    '            Ep.SetError(txtpanno, "Insert Proper PAN No")
                    '            bln = False
                    '        End If
                    '    ElseIf CMBDEDUCTEETYPE.Text.Trim = "Firm" Then
                    '        If txtpanno.Text.Substring(x - 1, 1) <> "F" Then
                    '            Ep.SetError(txtpanno, "Insert Proper PAN No")
                    '            bln = False
                    '        End If
                    '    ElseIf CMBDEDUCTEETYPE.Text = "Company" Then
                    '        If txtpanno.Text.Substring(x - 1, 1) <> "C" Then
                    '            Ep.SetError(txtpanno, "Insert Proper PAN No")
                    '            bln = False
                    '        End If
                    '    ElseIf CMBDEDUCTEETYPE.Text = "HUF" Then
                    '        If txtpanno.Text.Substring(x - 1, 1) <> "H" Then
                    '            Ep.SetError(txtpanno, "Insert Proper PAN No")
                    '            bln = False
                    '        End If
                    '    End If
                    'End If
                Next x
            End If
        End If

        If ClientName = "SNSMALAD" And cmbstate.Text.Trim = "" Then cmbstate.Text = "Maharashtra"
        If cmbstate.Text.Trim.Length = 0 Then
            Ep.SetError(cmbstate, "Select State")
            bln = False
        End If


        'CHECKING 1ST TWO ALPHABETS WITH STATE
        If cmbstate.Text.Trim <> "" And TXTGSTIN.Text.Trim <> "" AndAlso TXTGSTIN.Text.Substring(0, 2) <> "88" Then
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" cast(state_remark as varchar(5)) AS STATECODE ", "", " STATEMASTER", " AND state_name = '" & cmbstate.Text.Trim & "'  and STATE_YEARID = " & YearId)
            If DT.Rows(0).Item("STATECODE") <> TXTGSTIN.Text.Substring(0, 2) Then
                Ep.SetError(TXTGSTIN, "State Code does not match with GST No")
                bln = False
            End If
        End If


        If TXTGSTIN.Text.Trim.Length > 0 Then
            'If txtpanno.Text.Trim = "" Then
            '    Ep.SetError(txtpanno, "Enter Pan No")
            '    bln = False
            'End If

            'If txtpanno.Text.Trim.Length <> 10 Then
            '    Ep.SetError(txtpanno, "Insert Proper PAN No")
            '    bln = False
            'End If

            'CHECKING 2ND TO 11TH ALPHABETS WITH PANNO
            'If TXTGSTIN.Text.Trim.Length = 15 Then
            '    If txtpanno.Text.Trim <> TXTGSTIN.Text.Substring(2, 10) Then
            '        Ep.SetError(txtpanno, "Enter Proper PAN Details")
            '        bln = False
            '    End If
            'End If

            If TXTGSTIN.Text.Trim.Length <> 15 Then
                Ep.SetError(TXTGSTIN, "Enter Proper GST No")
                bln = False
            End If
        End If

        If ClientName = "SNSMALAD" And txtmobile.Text.Trim = "" Then
            Ep.SetError(txtmobile, "Enter Mobile No")
            bln = False
        End If

        Return bln
    End Function

    Private Sub cmbgroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgroup.GotFocus
        Try
            If cmbgroup.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("group_name", "", "GroupMaster", " and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "Group_name"
                    cmbgroup.DataSource = dt
                    cmbgroup.DisplayMember = "group_name"
                    cmbgroup.Text = ""
                End If
                cmbgroup.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcity.GotFocus
        Try
            If cmbcity.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("city_name", "", "CityMaster", " and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "City_name"
                    cmbcity.DataSource = dt
                    cmbcity.DisplayMember = "city_name"
                    cmbcity.Text = ""
                End If
                cmbcity.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcity_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcity.Validating
        Try
            If cmbcity.Text.Trim <> "" Then
                pcase(cmbcity)
                Dim objclscommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objclscommon.search("city_name", "", "CityMaster", " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbcity.Text.Trim
                    Dim tempmsg As Integer = MsgBox("City not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbcity.Text = a
                        objyearmaster.savecity(cmbcity.Text.Trim, CmpId, Locationid, Userid, YearId, " and city_name = '" & cmbcity.Text.Trim & "' and city_cmpid = " & CmpId & " and city_Locationid = " & Locationid & " and city_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbcity.DataSource
                        If cmbcity.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbcity.Text)
                                cmbcity.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbstate.GotFocus
        Try
            If cmbstate.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("state_name", "", "StateMaster", " and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "state_name"
                    cmbstate.DataSource = dt
                    cmbstate.DisplayMember = "state_name"
                    cmbstate.Text = ""
                End If
                cmbstate.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbstate.Validating
        Try
            If cmbstate.Text.Trim <> "" Then

                pcase(cmbstate)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("state_name", "", "StateMaster", " and state_name = '" & cmbstate.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbstate.Text.Trim
                    Dim tempmsg As Integer = MsgBox("State not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbstate.Text = a
                        objyearmaster.savestate(cmbstate.Text.Trim, CmpId, Locationid, Userid, YearId, " and state_name = '" & cmbstate.Text.Trim & "' and state_cmpid = " & CmpId & " and state_Locationid = " & Locationid & " and state_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbstate.DataSource
                        If cmbstate.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbstate.Text)
                                cmbstate.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If

            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcountry_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcountry.GotFocus
        Try
            If cmbcountry.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("country_name", "", "CountryMaster", " and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "country_name"
                    cmbcountry.DataSource = dt
                    cmbcountry.DisplayMember = "country_name"
                    cmbcountry.Text = ""
                End If
                cmbcountry.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcountry_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcountry.Validating
        Try
            If cmbcountry.Text.Trim <> "" Then
                pcase(cmbcountry)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("Country_name", "", "CountryMaster", " and Country_name = '" & cmbcountry.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbcountry.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Country not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbcountry.Text = a
                        objyearmaster.savecountry(cmbcountry.Text.Trim, CmpId, Locationid, Userid, YearId, " and Country_name = '" & cmbcountry.Text.Trim & "' and country_cmpid = " & CmpId & " and country_Locationid = " & Locationid & " and country_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbcountry.DataSource
                        If cmbcountry.DataSource <> Nothing Then
                            If dt1.Rows.Count > 0 Then
Line1:
                                dt1.Rows.Add(cmbcountry.Text)
                                cmbcountry.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    'Private Sub txtcmpname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    '    Try

    '        pcase(cmbcmpname)
    '        If (edit = False) Or (edit = True And tempAccountsName <> cmbcmpname.Text.Trim) Then
    '            'for search
    '            Dim objclscommon As New ClsCommonMaster
    '            Dim dt As New DataTable
    '            If frmstring = "CUSTOMER" Then
    '                dt = objclscommon.search("customer_cmpname", "", "CustomerMaster", " and customer_cmpname = '" & cmbcmpname.Text.Trim & "' and customer_cmpid =" & CmpId & " and customer_Locationid =" & Locationid & " and customer_Yearid =" & YearId)
    '            ElseIf frmstring = "VENDOR" Then
    '                dt = objclscommon.search("Vendor_cmpname", "", "VendorMaster", " and Vendor_cmpname = '" & cmbcmpname.Text.Trim & "' and Vendor_cmpid = " & CmpId & " and Vendor_Locationid = " & Locationid & " and Vendor_Yearid = " & YearId)
    '            ElseIf frmstring = "ACCOUNTS" Then
    '                dt = objclscommon.search("acc_cmpname", "", "AccountsMaster", " and acc_cmpname = '" & cmbcmpname.Text.Trim & "' and acc_cmpid = " & CmpId & " and acc_Locationid = " & Locationid & " and acc_Yearid = " & YearId)
    '            ElseIf frmstring = "EMPLOYEE" Then
    '                dt = objclscommon.search("Emp_cmpname", "", "EmployeeMaster", " and Emp_cmpname = '" & cmbcmpname.Text.Trim & "' and Emp_cmpid = " & CmpId & " and Emp_Locationid = " & Locationid & " and Emp_Yearid = " & YearId)
    '            End If
    '            If dt.Rows.Count > 0 Then
    '                MsgBox("Company Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
    '                e.Cancel = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
    '    End Try
    'End Sub

    Private Sub cmbarea_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbarea.GotFocus
        Try
            If cmbarea.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("area_name", "", "AreaMaster", " and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "area_name"
                    cmbarea.DataSource = dt
                    cmbarea.DisplayMember = "area_name"
                    cmbarea.Text = ""
                End If
                cmbarea.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstreet_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSTREET.GotFocus
        Try
            If CMBSTREET.Text.Trim = "" Then
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objclscommon.search("street_name", "", "streetMaster", " and street_Yearid = " & YearId)
                If dt.Rows.Count > 0 Then
                    dt.DefaultView.Sort = "street_name"
                    CMBSTREET.DataSource = dt
                    CMBSTREET.DisplayMember = "street_name"
                    CMBSTREET.Text = ""
                End If
                CMBSTREET.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbarea_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbarea.Validating
        Try
            If cmbarea.Text.Trim <> "" Then
                pcase(cmbarea)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("area_name", "", "areaMaster", " and area_name = '" & cmbarea.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbarea.Text.Trim
                    Dim tempmsg As Integer = MsgBox("area not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        cmbarea.Text = a
                        objyearmaster.savearea(cmbarea.Text.Trim, CmpId, Locationid, Userid, YearId, " and area_name = '" & cmbarea.Text.Trim & "' and area_cmpid = " & CmpId & " and area_Locationid = " & Locationid & " and area_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = cmbarea.DataSource
                        If cmbarea.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(cmbarea.Text)
                                cmbarea.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbstreet_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSTREET.Validating
        Try
            If CMBSTREET.Text.Trim <> "" Then
                pcase(CMBSTREET)
                Dim objClsCommon As New ClsCommonMaster
                Dim objyearmaster As New ClsYearMaster
                Dim dt As DataTable
                dt = objClsCommon.search("street_name", "", "streetMaster", " and street_name = '" & CMBSTREET.Text.Trim & "' and street_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = CMBSTREET.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Street not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'LOCATION MASTER'")
                        If DTROW(0).Item(1) = False Then
                            MsgBox("Insufficient Rights")
                            Exit Sub
                        End If
                        CMBSTREET.Text = a
                        objyearmaster.savestreet(CMBSTREET.Text.Trim, CmpId, Locationid, Userid, YearId, " and street_name = '" & CMBSTREET.Text.Trim & "' and street_Yearid = " & YearId)
                        Dim dt1 As New DataTable
                        dt1 = CMBSTREET.DataSource
                        If CMBSTREET.DataSource <> Nothing Then
line1:
                            If dt1.Rows.Count > 0 Then
                                dt1.Rows.Add(CMBSTREET.Text)
                                CMBSTREET.Text = a
                            End If
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbgroup_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbgroup.Validating
        Try
            If cmbgroup.Text.Trim <> "" Then
                pcase(cmbgroup)
                Dim objClsCommon As New ClsCommonMaster
                Dim dt As DataTable
                dt = objClsCommon.search("group_name", "", "groupMaster", " and group_name = '" & cmbgroup.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                If dt.Rows.Count = 0 Then
                    Dim a As String = cmbgroup.Text.Trim
                    Dim tempmsg As Integer = MsgBox("Group not present, Add New?", MsgBoxStyle.YesNo, "PROCESS")
                    If tempmsg = vbYes Then
                        cmbgroup.Text = a
                        Dim objgroupmaster As New GroupMaster
                        objgroupmaster.txtname.Text = cmbgroup.Text.Trim()
                        objgroupmaster.ShowDialog()
                        dt = objClsCommon.search("group_name", "", "groupMaster", " and group_name = '" & cmbgroup.Text.Trim & "' and group_cmpid = " & CmpId & " and group_Locationid = " & Locationid & " and group_Yearid = " & YearId)
                        If dt.Rows.Count > 0 Then
                            Dim dt1 As DataTable
                            dt1 = cmbgroup.DataSource
                            If cmbgroup.DataSource <> Nothing Then
line1:
                                dt1.Rows.Add(cmbgroup.Text)
                                cmbgroup.Text = a
                            End If
                        End If
                        e.Cancel = True
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo line1
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        pcase(txtname)
    End Sub

    Private Sub txtadd1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtadd1.Validating
        pcase(txtadd1)
    End Sub

    Private Sub txtadd2_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtadd2.Validating
        pcase(txtadd2)
    End Sub

    Private Sub txtzipcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtzipcode.KeyPress, txtcrdays.KeyPress
        numkeypress(e, sender, Me)
    End Sub

    Private Sub txtcrlimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcrlimit.KeyPress, txtopbal.KeyPress, TXTDISC.KeyPress, TXTKMS.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub cmbcmpname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcmpname.Enter
        Try
            If cmbcmpname.Text.Trim = "" Then
                FILLCMPNAME()
                cmbcmpname.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbcmpname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbcmpname.KeyDown
        If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
        If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True
    End Sub

    Private Sub cmbcmpname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbcmpname.Validating
        Try
            If cmbcmpname.Text.Trim <> "" Then
                pcase(cmbcmpname)
                If CMBCODE.Text.Trim = "" Then CMBCODE.Text = cmbcmpname.Text.Trim
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(cmbcmpname.Text) <> LCase(tempAccountsName)) Then
                    dt = objclscommon.search("ACC_CMPNAME", "", " LEDGERS", " AND ACC_CMPNAME = '" & cmbcmpname.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Name Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Function DELETEERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            Dim bln_4_case As Boolean = True
            Select Case tempAccountsName
                Case "Transport Charges"
                    ' Str = "Turquoise"
                    bln_4_case = False
                Case "Other Charges"
                    bln_4_case = False
                Case "PURCHASE"
                    bln_4_case = False
                Case "SALE"
                    bln_4_case = False
                Case "T.D.S."
                    bln_4_case = False
                Case "Cash In Hand"
                    bln_4_case = False
                Case "Petty Cash"
                    bln_4_case = False
                Case "Round Off"
                    bln_4_case = False
                Case "Discount Recd"
                    bln_4_case = False
                Case "Commission Recd"
                    bln_4_case = False
                Case "Discount Given"
                    bln_4_case = False
                Case "Commission A/C"
                    bln_4_case = False
                Case "Profit & Loss A/C"
                    bln_4_case = False
                Case "Opening A/C"
                    bln_4_case = False
            End Select

            If bln_4_case = False Then
                Ep.SetError(cmbcmpname, "Build In Ledger Cannot Delete")
                BLN = False
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If edit = True Then

                Ep.Clear()
                If Not DELETEERRORVALID() Then
                    Exit Sub
                End If

                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                If MsgBox("Wish to Delete Ledger?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                Dim ALPARAVAL As New ArrayList

                ALPARAVAL.Add(tempAccountsName)
                ALPARAVAL.Add(CmpId)
                ALPARAVAL.Add(Locationid)
                ALPARAVAL.Add(YearId)
                ALPARAVAL.Add(Userid)

                Dim OBJACC As New ClsAccountsMaster
                OBJACC.alParaval = ALPARAVAL
                OBJACC.frmstring = frmstring
                Dim DT As DataTable = OBJACC.DELETE
                If DT.Rows.Count > 0 Then
                    MsgBox(DT.Rows(0).Item(0))
                    clear()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCODE.Enter
        Try
            If CMBCODE.Text.Trim = "" Then
                FILLCMPCODE()
                CMBCODE.SelectAll()
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBCODE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCODE.Validating
        Try
            If CMBCODE.Text.Trim <> "" Then
                pcase(CMBCODE)
                Dim objclscommon As New ClsCommonMaster
                Dim dt As DataTable
                If (edit = False) Or (edit = True And LCase(CMBCODE.Text) <> LCase(tempAccountsCode)) Then
                    dt = objclscommon.search("ACC_CODE", "", " LEDGERS", " AND ACC_CODE = '" & CMBCODE.Text.Trim & "' and LEDGERS.acc_cmpid = " & CmpId & " AND LEDGERS.ACC_LOCATIONID = " & Locationid & " AND LEDGERS.ACC_YEARID = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Code Already Exists", MsgBoxStyle.Critical, "PROCESS")
                        e.Cancel = True
                    End If
                End If
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub chkcopy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcopy.CheckedChanged
        Try
            txtadd.Clear()
            If chkcopy.CheckState = CheckState.Checked Then
                If txtadd1.Text.Trim <> "" Then txtadd.Text = txtadd.Text & txtadd1.Text.Trim & "," & vbNewLine
                If CMBSTREET.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBSTREET.Text.Trim & ", "
                If cmbarea.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & cmbarea.Text.Trim & vbNewLine
                If TXTLANDMARK.Text.Trim <> "" Then txtadd.Text = txtadd.Text & "" & TXTLANDMARK.Text.Trim & vbNewLine
                If cmbcity.Text.Trim <> "" Then txtadd.Text = txtadd.Text & cmbcity.Text.Trim
                If txtzipcode.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & " - " & txtzipcode.Text.Trim & "," & vbNewLine
                Else
                    txtadd.Text = txtadd.Text & vbNewLine
                End If
                If CMBVIA.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBVIA.Text.Trim & ", "
                If CMBDISTRICT.Text.Trim <> "" Then txtadd.Text = txtadd.Text & CMBDISTRICT.Text.Trim & vbNewLine

                If cmbstate.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & "" & cmbstate.Text.Trim & ","
                Else
                    txtadd.Text = txtadd.Text & vbNewLine
                End If

                If cmbcountry.Text.Trim <> "" Then
                    txtadd.Text = txtadd.Text & " " & cmbcountry.Text.Trim & "."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKTDSAPP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKTDSAPP.CheckedChanged
        Try
            If cmbgroup.Text.Trim <> "" Then
                Dim objcmn As New ClsCommon
                Dim DT As DataTable = objcmn.search(" GROUP_SECONDARY", "", " GROUPMASTER", " AND GROUP_NAME = '" & cmbgroup.Text.Trim & "' AND GROUP_CMPID = " & CmpId & " AND GROUP_LOCATIONID = " & Locationid & " AND GROUP_YEARID = " & YearId)
                If DT.Rows.Count > 0 Then
                    If DT.Rows(0).Item(0) = "Sundry Creditors" Or DT.Rows(0).Item(0) = "Sundry Debtors" Or DT.Rows(0).Item(0) = "Provisions" Or DT.Rows(0).Item(0) = "Loans" Or DT.Rows(0).Item(0) = "Loans & Advances" Then
                        GROUPTDS.Enabled = CHKTDSAPP.CheckState
                    Else
                        MsgBox("Not Applicable for this Group")
                        CHKTDSAPP.CheckState = CheckState.Unchecked
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBISLOWER_Validating(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBISLOWER.Validating
        Try
            CMBSECTION.Text = ""
            If CMBISLOWER.Text = "Yes" Then
                CMBSECTION.Enabled = True
                CMBSECTION.Focus()
            Else
                CMBSECTION.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSECTION_Validating(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSECTION.Validating
        Try
            TXTTDSRATE.Clear()
            If CMBSECTION.Text = "197" Then
                TXTTDSRATE.Enabled = True
                TXTTDSRATE.Focus()
            Else
                TXTTDSRATE.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTTDSRATE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTDSRATE.KeyPress
        numdotkeypress(e, TXTTDSRATE, Me)
    End Sub

    Private Sub TXTLIMIT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTLIMIT.KeyPress
        numdotkeypress(e, TXTLIMIT, Me)
    End Sub

    Private Sub CMBNATURE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBNATURE.Enter
        Try
            If CMBNATURE.Text.Trim = "" Then FILLNATURE(CMBNATURE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBNATURE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBNATURE.Validating
        Try
            If CMBNATURE.Text.Trim <> "" Then NATUREVALIDATE(CMBNATURE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBTRANS.Enter
        Try
            If CMBTRANS.Text.Trim = "" Then fillname(CMBTRANS, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND  LEDGERS.ACC_TYPE='TRANSPORT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbtrans_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBTRANS.Validating
        Try
            If CMBTRANS.Text.Trim <> "" Then namevalidate(CMBTRANS, cmbhotelcode, e, Me, TXTHOTELADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "TRANSPORT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbagent_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBAGENT.Enter
        Try
            If CMBAGENT.Text.Trim = "" Then fillname(CMBAGENT, edit, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS' AND LEDGERS.ACC_TYPE='AGENT'")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbagent_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBAGENT.Validating
        Try
            If CMBAGENT.Text.Trim <> "" Then namevalidate(CMBAGENT, cmbhotelcode, e, Me, TXTHOTELADD, " AND GROUPMASTER.GROUP_SECONDARY ='SUNDRY CREDITORS'", "SUNDRY CREDITORS", "AGENT")
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub TXTTDSPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTTDSPER.KeyPress
        numdotkeypress(e, TXTTDSPER, Me)
    End Sub

    Private Sub TXTAGENTCOMM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTAGENTCOMM.KeyPress
        numdotkeypress(e, TXTAGENTCOMM, Me)
    End Sub

    Private Sub CMBDISTRICT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDISTRICT.Enter
        Try
            If CMBDISTRICT.Text.Trim = "" Then fillDISTRICT(CMBDISTRICT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDISTRICT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDISTRICT.Validating
        Try
            If CMBDISTRICT.Text.Trim <> "" Then DISTRICTvalidate(CMBDISTRICT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBVIA_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBVIA.Enter
        Try
            If CMBVIA.Text.Trim = "" Then fillVIA(CMBVIA)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBVIA_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBVIA.Validating
        Try
            If CMBVIA.Text.Trim <> "" Then VIAvalidate(CMBVIA, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYBANK.Enter
        Try
            If CMBPARTYBANK.Text.Trim = "" Then fillPARTYBANK(CMBPARTYBANK, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYBANK.Validating
        Try
            If CMBPARTYBANK.Text.Trim <> "" Then PARTYBANKvalidate(CMBPARTYBANK, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPARTYBANK1.Enter
        Try
            If CMBPARTYBANK1.Text.Trim = "" Then fillPARTYBANK(CMBPARTYBANK1, edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPARTYBANK1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPARTYBANK1.Validating
        Try
            If CMBPARTYBANK1.Text.Trim <> "" Then PARTYBANKvalidate(CMBPARTYBANK1, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKBANK1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKBANK1.CheckedChanged
        Try
            If CHKBANK1.CheckState = CheckState.Checked Then
                CHKBANK.CheckState = CheckState.Unchecked
                CMBPARTYBANK.BackColor = Color.White
                CMBPARTYBANK1.BackColor = Color.Linen
                TXTACCNO.BackColor = Color.White
                TXTACCNO1.BackColor = Color.Linen
                TXTIFSC.BackColor = Color.White
                TXTIFSC1.BackColor = Color.Linen
                TXTBRANCH.BackColor = Color.White
                TXTBRANCH1.BackColor = Color.Linen
            Else
                CHKBANK.CheckState = CheckState.Checked
                CMBPARTYBANK1.BackColor = Color.White
                CMBPARTYBANK.BackColor = Color.Linen
                TXTACCNO1.BackColor = Color.White
                TXTACCNO.BackColor = Color.Linen
                TXTIFSC1.BackColor = Color.White
                TXTIFSC.BackColor = Color.Linen
                TXTBRANCH1.BackColor = Color.White
                TXTBRANCH.BackColor = Color.Linen
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CHKBANK_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHKBANK.CheckedChanged
        Try
            If CHKBANK.CheckState = CheckState.Checked Then
                CHKBANK1.CheckState = CheckState.Unchecked
                CMBPARTYBANK1.BackColor = Color.White
                CMBPARTYBANK.BackColor = Color.Linen
                TXTACCNO1.BackColor = Color.White
                TXTACCNO.BackColor = Color.Linen
                TXTIFSC1.BackColor = Color.White
                TXTIFSC.BackColor = Color.Linen
                TXTBRANCH1.BackColor = Color.White
                TXTBRANCH.BackColor = Color.Linen
            Else
                CHKBANK1.CheckState = CheckState.Checked
                CMBPARTYBANK.BackColor = Color.White
                CMBPARTYBANK1.BackColor = Color.Linen
                TXTACCNO.BackColor = Color.White
                TXTACCNO1.BackColor = Color.Linen
                TXTIFSC.BackColor = Color.White
                TXTIFSC1.BackColor = Color.Linen
                TXTBRANCH.BackColor = Color.White
                TXTBRANCH1.BackColor = Color.Linen
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTINTPER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTINTPER.KeyPress
        numdotkeypress(e, TXTINTPER, Me)
    End Sub


    Private Sub TXTCEMAIL_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTCEMAIL.Validating
        Try
            If TXTCNAME.Text.Trim <> "" And TXTCMOB.Text.Trim <> "" Then
                fillCONTACTgrid()
            Else
                MsgBox("Enter Proper Details")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCONTACTgrid()

        If gridContactDoubleClick = False Then
            If DTPCDOB.Text <> "__/__/____" Then
                GRIDCONTACT.Rows.Add(TXTCNAME.Text.Trim, Format(Convert.ToDateTime(DTPCDOB.Text).Date, "dd/MM/yyyy"), CMBDESIGNATION.Text.Trim, TXTCMOB.Text.Trim, TXTCEMAIL.Text.Trim)
            Else
                GRIDCONTACT.Rows.Add(TXTCNAME.Text.Trim, "__/__/____", CMBDESIGNATION.Text.Trim, TXTCMOB.Text.Trim, TXTCEMAIL.Text.Trim)
            End If

            'getsrno(GRIDCONTACT)
        ElseIf gridContactDoubleClick = True Then
            GRIDCONTACT.Item(0, TEMPCONTACTROW).Value = TXTCNAME.Text.Trim
            If DTPCDOB.Text <> "__/__/____" Then
                GRIDCONTACT.Item(1, TEMPCONTACTROW).Value = Format(Convert.ToDateTime(DTPCDOB.Text).Date, "dd/MM/yyyy")
            Else
                GRIDCONTACT.Item(1, TEMPCONTACTROW).Value = "__/__/____"
            End If
            GRIDCONTACT.Item(2, TEMPCONTACTROW).Value = CMBDESIGNATION.Text.Trim
            GRIDCONTACT.Item(3, TEMPCONTACTROW).Value = TXTCMOB.Text.Trim
            GRIDCONTACT.Item(4, TEMPCONTACTROW).Value = TXTCEMAIL.Text.Trim

            gridContactDoubleClick = False
        End If
        GRIDCONTACT.FirstDisplayedScrollingRowIndex = GRIDCONTACT.RowCount - 1

        TXTCNAME.Clear()
        DTPCDOB.Clear()
        CMBDESIGNATION.Text = ""
        TXTCMOB.Clear()
        TXTCEMAIL.Clear()
        TXTCNAME.Focus()

    End Sub

    Private Sub GRIDCONTACT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GRIDCONTACT.KeyDown
        Try
            If e.KeyCode = Keys.Delete And GRIDCONTACT.RowCount > 0 Then
                'dont allow user if any of the grid line is in edit mode.....
                If gridContactDoubleClick = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                GRIDCONTACT.Rows.RemoveAt(GRIDCONTACT.CurrentRow.Index)
                'getsrno(GRIDCONTACT)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GRIDCONTACT_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GRIDCONTACT.CellDoubleClick
        Try
            If e.RowIndex = -1 Then Exit Sub

            If e.RowIndex >= 0 And GRIDCONTACT.Item(GNAME.Index, e.RowIndex).Value <> Nothing Then

                gridContactDoubleClick = True
                TXTCNAME.Text = GRIDCONTACT.Item(GNAME.Index, e.RowIndex).Value
                DTPCDOB.Text = GRIDCONTACT.Item(GDOB.Index, e.RowIndex).Value
                CMBDESIGNATION.Text = GRIDCONTACT.Item(GDESIGNATION.Index, e.RowIndex).Value
                TXTCMOB.Text = GRIDCONTACT.Item(GMOBILENO.Index, e.RowIndex).Value.ToString
                TXTCEMAIL.Text = GRIDCONTACT.Item(GEMAIL.Index, e.RowIndex).Value.ToString
                TEMPCONTACTROW = e.RowIndex
                TXTCNAME.Focus()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBDESIGNATION_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBDESIGNATION.Validating
        Try
            If CMBDESIGNATION.Text.Trim <> "" Then DESIGNATIONVALIDATE(CMBDESIGNATION, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub CMBDESIGNATION_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBDESIGNATION.Enter
        Try
            If CMBDESIGNATION.Text.Trim = "" Then fillDESIGNATION(CMBDESIGNATION)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AccountsMaster_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "ROHIT" Then
                LBLSTNO.Text = "BIS No"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    
End Class