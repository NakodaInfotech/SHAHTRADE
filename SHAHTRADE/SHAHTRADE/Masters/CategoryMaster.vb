Imports BL

Public Class CategoryMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Public frmString As String       'Used for form Category or GRade
    Public TempName As String        'Used for tempname while edit mode
    Public TempID As Integer         'Used for tempname while edit mode
    Public edit As Boolean           'Used for edit

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub txtname_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtname.Validating
        Try
            'for search
            Dim objclscommon As New ClsCommonMaster
            Dim dt As DataTable
            If (edit = False) Or (edit = True And LCase(TempName) <> LCase(txtname.Text.Trim)) Then
                If frmString = "CATEGORY" Then
                    dt = objclscommon.search("category_name", "", "CategoryMaster", " and category_name = '" & txtname.Text.Trim & "' and category_cmpid =" & CmpId & " and category_Locationid =" & Locationid & " and category_Yearid =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Category Name Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "SUBCATEGORY" Then
                    dt = objclscommon.search("SUBcategory_name", "", "SUBCategoryMaster", " and SUBcategory_name = '" & txtname.Text.Trim & "' and SUBcategory_Yearid =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Sub-Category Name Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "MAKE" Then
                    dt = objclscommon.search("MAKE_name", "", "MAKEMaster", " and MAKE_name = '" & txtname.Text.Trim & "' and MAKE_cmpid =" & CmpId & " and MAKE_Locationid =" & Locationid & " and MAKE_Yearid =" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("MAKE Name Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "NARRATION" Then
                    dt = objclscommon.search("NARRATION_name", "", "NARRATIONMaster", " and NARRATION_name = '" & txtname.Text.Trim & "' and NARRATION_cmpid = " & CmpId & " and NARRATION_Locationid = " & Locationid & " and NARRATION_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("NARRATION Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "PARTYBANK" Then
                    dt = objclscommon.search("PARTYBANK_name", "", "PARTYBANKMaster", " and PARTYBANK_name = '" & txtname.Text.Trim & "' and PARTYBANK_cmpid = " & CmpId & " and PARTYBANK_Locationid = " & Locationid & " and PARTYBANK_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("PARTYBANK Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "DISTRICT" Then
                    dt = objclscommon.search("DISTRICT_name", "", "DISTRICTMaster", " and DISTRICT_name = '" & txtname.Text.Trim & "' and DISTRICT_cmpid = " & CmpId & " and DISTRICT_Locationid = " & Locationid & " and DISTRICT_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("DISTRICT Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "VIA" Then
                    dt = objclscommon.search("VIA_name", "", "VIAMaster", " and VIA_name = '" & txtname.Text.Trim & "' and VIA_cmpid = " & CmpId & " and VIA_Locationid = " & Locationid & " and VIA_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("VIA Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "PATTA" Then
                    dt = objclscommon.search("PATTA_name", "", "PATTAMaster", " and PATTA_name = '" & txtname.Text.Trim & "' and PATTA_cmpid = " & CmpId & " and PATTA_Locationid = " & Locationid & " and PATTA_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("PATTA Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "SIZE" Then
                    dt = objclscommon.search("SIZE_name", "", "SIZEMaster", " and SIZE_name = '" & txtname.Text.Trim & "' and SIZE_cmpid = " & CmpId & " and SIZE_Locationid = " & Locationid & " and SIZE_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("SIZE Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                ElseIf frmString = "HALLMARK" Then
                    dt = objclscommon.search("HALLMARK_name", "", "HALLMARKMASTER", " and HALLMARK_name = '" & txtname.Text.Trim & "' And HALLMARK_Yearid = " & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("HALLMARK Name Already Exists", MsgBoxStyle.Critical, "SHAHTRADE")
                        e.Cancel = True
                    End If
                End If
            End If
            pcase(txtname)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(txtname.Text.Trim)
            alParaval.Add(txtremarks.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Locationid)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            alParaval.Add(0)

            If frmString = "CATEGORY" Then
                Dim OBJ As New ClsCategoryMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "SUBCATEGORY" Then
                Dim OBJ As New ClsSubCategoryMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "MAKE" Then
                Dim OBJ As New ClsMakeMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "NARRATION" Then
                Dim OBJ As New ClsNarrationMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "PATTA" Then
                Dim OBJ As New ClsPattaMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.SAVE()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "SIZE" Then
                Dim OBJ As New ClsSizeMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.SAVE()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.UPDATE()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "PARTYBANK" Then
                Dim OBJ As New ClsPARTYBANKMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "DISTRICT" Then
                Dim OBJ As New ClsDistrictMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "VIA" Then
                Dim OBJ As New ClsViaMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            ElseIf frmString = "HALLMARK" Then
                Dim OBJ As New ClsHallmarkMaster
                OBJ.alParaval = alParaval

                If edit = False Then
                    If USERADD = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    IntResult = OBJ.save()
                    MsgBox("Details Added")
                ElseIf edit = True Then
                    If USEREDIT = False Then
                        MsgBox("Insufficient Rights")
                        Exit Sub
                    End If
                    alParaval.Add(TempID)
                    IntResult = OBJ.Update()
                    edit = False
                    MsgBox("Details Updated")
                End If

            End If

            clear()
            txtname.Focus()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If txtname.Text.Trim.Length = 0 Then
            EP.SetError(txtname, "Fill Name")
            bln = False
        End If
        Return bln
    End Function

    Sub clear()
        txtname.Clear()
        txtremarks.Clear()
    End Sub

    Private Sub CategoryMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.S Then       'for Saving
            Call cmdok_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub CategoryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dttable As New DataTable
            Dim objCommon As New ClsCommonMaster

            If frmString = "CATEGORY" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Category Master"
                lblgroup.Text = "Category"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" CATEGORY_name", "", "CATEGORYMaster", " and CATEGORY_id = " & TempID & " and CATEGORY_cmpid = " & CmpId & " and CATEGORY_Locationid = " & Locationid & " and CATEGORY_yearid = " & YearId)

            ElseIf frmString = "SUBCATEGORY" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "SUBCATEGORY Master"
                lblgroup.Text = "SUBCATEGORY"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" SUBCATEGORY_name", "", "SUBCATEGORYMaster", " and SUBCATEGORY_id = " & TempID & " and SUBCATEGORY_cmpid = " & CmpId & " and SUBCATEGORY_Locationid = " & Locationid & " and SUBCATEGORY_yearid = " & YearId)

            ElseIf frmString = "MAKE" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "MAKE Master"
                lblgroup.Text = "MAKE"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" MAKE_name", "", "MAKEMaster", " and MAKE_id = " & TempID & " and MAKE_cmpid = " & CmpId & " and MAKE_Locationid = " & Locationid & " and MAKE_yearid = " & YearId)


            ElseIf frmString = "NARRATION" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Narration Master"
                lblgroup.Text = "Narration"
                lbl.Text = "Enter Narration " & vbNewLine & "(e.g.  Remarks..., etc. )"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" NARRATION_name", "", "NARRATIONMaster", " and NARRATION_id = " & TempID & " and NARRATION_cmpid = " & CmpId & " and NARRATION_Locationid = " & Locationid & " and NARRATION_yearid = " & YearId)


            ElseIf frmString = "PATTA" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Patta Master"
                lblgroup.Text = "Patta"
                lbl.Text = "Enter Patta "
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" PATTA_name", "", "PATTAMaster", " and PATTA_id = " & TempID & " and PATTA_cmpid = " & CmpId & " and PATTA_Locationid = " & Locationid & " and PATTA_yearid = " & YearId)


            ElseIf frmString = "SIZE" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Size Master"
                lblgroup.Text = "Size"
                lbl.Text = "Enter Size"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" SIZE_name", "", "SIZEMaster", " and SIZE_id = " & TempID & " and SIZE_cmpid = " & CmpId & " and SIZE_Locationid = " & Locationid & " and SIZE_yearid = " & YearId)


            ElseIf frmString = "PARTYBANK" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "PartyBank Master"
                lblgroup.Text = "Party Bank"
                lbl.Text = "Enter Party Bank " & vbNewLine & "(e.g.  SBI,Canara..., etc. )"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" PARTYBANK_name", "", "PARTYBANKMaster", " and PARTYBANK_id = " & TempID & " and PARTYBANK_cmpid = " & CmpId & " and PARTYBANK_Locationid = " & Locationid & " and PARTYBANK_yearid = " & YearId)

            ElseIf frmString = "DISTRICT" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "District Master"
                lblgroup.Text = "District"
                lbl.Text = "Enter District " & vbNewLine & "(e.g.  SBI,Canara..., etc. )"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" DISTRICT_name", "", "DISTRICTMaster", " and DISTRICT_id = " & TempID & " and DISTRICT_cmpid = " & CmpId & " and DISTRICT_Locationid = " & Locationid & " and DISTRICT_yearid = " & YearId)

            ElseIf frmString = "VIA" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Via Master"
                lblgroup.Text = "Via"
                lbl.Text = "Enter Via " & vbNewLine & "(e.g.  SBI,Canara..., etc. )"
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" VIA_name", "", "VIAMaster", " and VIA_id = " & TempID & " and VIA_cmpid = " & CmpId & " and VIA_Locationid = " & Locationid & " and VIA_yearid = " & YearId)

            ElseIf frmString = "HALLMARK" Then
                Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ACCOUNTS MASTER'")
                USERADD = DTROW(0).Item(1)
                USEREDIT = DTROW(0).Item(2)
                USERVIEW = DTROW(0).Item(3)
                USERDELETE = DTROW(0).Item(4)

                Me.Text = "Hallmark Master"
                lblgroup.Text = "Hallmark"
                lbl.Text = "Enter Hallmark Name "
                If USEREDIT = False And USERVIEW = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                If edit = True Then dttable = objCommon.search(" HALLMARK_name", "", "HALLMARKMASTER", " and HALLMARK_id = " & TempID & " and HALLMARK_yearid = " & YearId)

            End If

            txtname.Text = TempName

            If dttable.Rows.Count > 0 Then
                txtname.Text = dttable.Rows(0).Item(0).ToString
            End If
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub
End Class