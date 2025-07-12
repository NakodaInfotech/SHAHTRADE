
Imports System.ComponentModel
Imports BL

Public Class ItemMaster

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Public EDIT As Boolean              'Used for edit
    Public TEMPNAME As String           'Used for edit name
    Public TEMPID As Integer            'Used for edit id

    Private Sub cmdexit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try

            Ep.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList

            alParaval.Add(TXTITEMNAME.Text.Trim)
            alParaval.Add(CMBHSNCODE.Text.Trim)
            alParaval.Add(Val(TXTRATE.Text.Trim))
            alParaval.Add(TXTDESC.Text.Trim)
            alParaval.Add(TXTBARCODE.Text.Trim)
            alParaval.Add(CMBCATEGORY.Text.Trim)
            alParaval.Add(CMBSUBCATEGORY.Text.Trim)
            alParaval.Add(CMBMAKE.Text.Trim)
            alParaval.Add(CMBUNIT.Text.Trim)
            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)

            Dim OBJITEM As New ClsItemMaster
            OBJITEM.alParaval = alParaval
            If EDIT = False Then
                If USERADD = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                IntResult = OBJITEM.SAVE()
                MsgBox("Details Added")

            ElseIf EDIT = True Then
                If USEREDIT = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If
                alParaval.Add(TEMPID)
                IntResult = OBJITEM.UPDATE()
                MsgBox("Details Updated")
                EDIT = False

            End If
            clear()
            TXTITEMNAME.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNCODE_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CMBHSNCODE.KeyDown
        Try
            If e.KeyCode = Keys.Oemcomma Then e.SuppressKeyPress = True
            If e.KeyCode = Keys.OemQuotes Then e.SuppressKeyPress = True

            If e.KeyCode = Keys.F1 Then
                Dim OBJLEDGER As New SelectHSN
                OBJLEDGER.STRSEARCH = " AND HSN_TYPE='GOODS'"
                OBJLEDGER.ShowDialog()
                If OBJLEDGER.TEMPCODE <> "" Then CMBHSNCODE.Text = OBJLEDGER.TEMPCODE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True
        If TXTITEMNAME.Text.Trim.Length = 0 Then
            Ep.SetError(TXTITEMNAME, "Fill Name")
            bln = False
        End If

        If CMBHSNCODE.Text.Trim.Length = 0 And ClientName <> "SNSMALAD" Then
            Ep.SetError(CMBHSNCODE, "Fill HSN Code")
            bln = False
        End If

        If TXTBARCODE.Text.Trim.Length = 0 And ClientName = "NAMASKAR" Then
            If MsgBox("Wish to Generate Auto Barcode?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Ep.SetError(TXTBARCODE, "Enter Product Barcode")
                bln = False
            End If
        End If

        If CMBUNIT.Text.Trim.Length = 0 And ClientName = "NAMASKAR" Then
            Ep.SetError(CMBUNIT, "Fill Unit")
            bln = False
        End If

        Return bln
    End Function

    Sub clear()
        TXTITEMNAME.Clear()
        CMBHSNCODE.Text = ""
        TXTRATE.Clear()
        TXTDESC.Clear()
        TXTBARCODE.Clear()
        TXTBARCODE.ReadOnly = False
        CMBCATEGORY.Text = ""
        CMBSUBCATEGORY.Text = ""
        CMBMAKE.Text = ""
        CMBUNIT.Text = ""
    End Sub

    Private Sub TXTITEMNAME_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTITEMNAME.Validating
        Try

            If (EDIT = False) Or (EDIT = True And LCase(TEMPNAME) <> LCase(TXTITEMNAME.Text.Trim)) Then
                ' search duplication 
                If TXTITEMNAME.Text.Trim <> Nothing Then
                    Dim objclscommon As New ClsCommonMaster
                    Dim dt As DataTable
                    dt = objclscommon.search("Item_name", "", " ITEMMASTER ", " and ITEM_name = '" & TXTITEMNAME.Text.Trim & "' AND ITEM_YEARID=" & YearId)
                    If dt.Rows.Count > 0 Then
                        MsgBox("Item Name Already Exists", MsgBoxStyle.Critical, "")
                        TXTITEMNAME.Focus()
                    End If
                    If ClientName <> "HMENTERPRISE" Then uppercase(TXTITEMNAME)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then               'FOR EXIT
            If errorvalid() = True Then
                Dim tempmsg As Integer = MessageBox.Show("Save Changes?", "", MessageBoxButtons.YesNo)
                If tempmsg = vbYes Then cmdok_Click(sender, e)
            End If
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Oemcomma Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub FILLCMB()
        Try
            If CMBHSNCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBHSNCODE)
            If CMBCATEGORY.Text.Trim = "" Then FILLCATEGORY(CMBCATEGORY, False)
            If CMBSUBCATEGORY.Text.Trim = "" Then FILLSUBCATEGORY(CMBSUBCATEGORY, False)
            If CMBMAKE.Text.Trim = "" Then FILLMAKE(CMBMAKE, False)
            fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLHSNITEMDESC(ByRef CMBHSNCODE As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable

            dt = objclscommon.search(" ISNULL(HSN_CODE, '') AS HSNCODE ", "", " HSNMASTER ", " AND HSN_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "HSNCODE"
                CMBHSNCODE.DataSource = dt
                CMBHSNCODE.DisplayMember = "HSNCODE"
            End If
            CMBHSNCODE.SelectAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ItemMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
        USERADD = DTROW(0).Item(1)
        USEREDIT = DTROW(0).Item(2)
        USERVIEW = DTROW(0).Item(3)
        USERDELETE = DTROW(0).Item(4)

        FILLCMB()
        clear()
        TXTITEMNAME.Text = TEMPNAME

        If EDIT = True Then
            If USEREDIT = False And USERVIEW = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If


            Dim objCommon As New ClsCommonMaster
            Dim dttable As DataTable = objCommon.search("  ITEMMASTER.ITEM_name AS ITEMNAME, ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE,ITEMMASTER.ITEM_DESC AS ITEMDESC, ISNULL(ITEMMASTER.ITEM_RATE,0) AS RATE, ISNULL(ITEM_BARCODE,'') AS BARCODE, ISNULL(CATEGORY_NAME,'') AS CATEGORY, ISNULL(SUBCATEGORY_NAME,'') AS SUBCATEGORY, ISNULL(MAKE_NAME, '') AS MAKE, ISNULL(UNIT_ABBR,'') AS UNIT ", "", " ITEMMASTER LEFT OUTER JOIN HSNMASTER ON ITEMMASTER.ITEM_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN CATEGORYMASTER ON ITEM_CATEGORYID = CATEGORY_ID LEFT OUTER JOIN SUBCATEGORYMASTER ON ITEM_SUBCATEGORYID = SUBCATEGORY_ID LEFT OUTER JOIN MAKEMASTER ON ITEM_MAKEID = MAKE_ID LEFT OUTER JOIN UNITMASTER ON ITEM_UNITID = UNIT_ID", " and ITEM_id = " & TEMPID)
            If dttable.Rows.Count > 0 Then
                TEMPNAME = dttable.Rows(0).Item("ITEMNAME").ToString
                TXTITEMNAME.Text = dttable.Rows(0).Item("ITEMNAME").ToString
                CMBHSNCODE.Text = dttable.Rows(0).Item("HSNCODE")
                TXTRATE.Text = Val(dttable.Rows(0).Item("RATE"))
                TXTDESC.Text = dttable.Rows(0).Item("ITEMDESC").ToString
                TXTBARCODE.Text = dttable.Rows(0).Item("BARCODE").ToString
                If TXTBARCODE.Text.Trim <> "" Then TXTBARCODE.ReadOnly = True
                CMBCATEGORY.Text = dttable.Rows(0).Item("CATEGORY").ToString
                CMBSUBCATEGORY.Text = dttable.Rows(0).Item("SUBCATEGORY").ToString
                CMBMAKE.Text = dttable.Rows(0).Item("MAKE").ToString
                CMBUNIT.Text = dttable.Rows(0).Item("UNIT").ToString
            End If
        End If
    End Sub

    Private Sub cmddelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If EDIT = True Then
                If USERDELETE = False Then
                    MsgBox("Insufficient Rights")
                    Exit Sub
                End If

                Dim tempmsg As Integer = MsgBox("Delete Item Permanently?", MsgBoxStyle.YesNo, "SHAHTRADE")
                If tempmsg = vbYes Then

                    Dim OBJITEM As New ClsItemMaster
                    Dim ALPARAVAL As New ArrayList

                    ALPARAVAL.Add(TXTITEMNAME.Text.Trim)
                    ALPARAVAL.Add(CmpId)
                    ALPARAVAL.Add(Locationid)
                    ALPARAVAL.Add(YearId)

                    OBJITEM.alParaval = ALPARAVAL
                    Dim DT As DataTable = OBJITEM.DELETE()
                    MsgBox(DT.Rows(0).Item(0).ToString)

                    clear()

                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TXTRATE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TXTRATE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub ItemMaster_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ClientName = "SNSMALAD" Then
                CMBHSNCODE.Visible = False
                LBLHSN.Visible = False
                LBLF1.Visible = False
            End If

            If ClientName = "NAMASKAR" Or ClientName = "HMENTERPRISE" Then
                LBLRATE.Text = "MRP"
                TXTRATE.BackColor = Color.LemonChiffon
                GBITEMDETAILS.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNCODE_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBHSNCODE.Enter
        Try
            If CMBHSNCODE.Text.Trim = "" Then FILLHSNITEMDESC(CMBHSNCODE)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBHSNCODE_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBHSNCODE.Validating
        Try
            If CMBHSNCODE.Text.Trim <> "" Then HSNITEMDESCVALIDATE(CMBHSNCODE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBCATEGORY.Enter
        Try
            If CMBCATEGORY.Text.Trim = "" Then FILLCATEGORY(CMBCATEGORY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCATEGORY_Validating(sender As Object, e As CancelEventArgs) Handles CMBCATEGORY.Validating
        Try
            If CMBCATEGORY.Text.Trim <> "" Then CATEGORYVALIDATE(CMBCATEGORY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSUBCATEGORY_Enter(sender As Object, e As EventArgs) Handles CMBSUBCATEGORY.Enter
        Try
            If CMBSUBCATEGORY.Text.Trim = "" Then FILLSUBCATEGORY(CMBSUBCATEGORY, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSUBCATEGORY_Validating(sender As Object, e As CancelEventArgs) Handles CMBSUBCATEGORY.Validating
        Try
            If CMBSUBCATEGORY.Text.Trim <> "" Then SUBCATEGORYVALIDATE(CMBSUBCATEGORY, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMAKE_Enter(sender As Object, e As EventArgs) Handles CMBMAKE.Enter
        Try
            If CMBMAKE.Text.Trim = "" Then FILLMAKE(CMBMAKE, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBMAKE_Validating(sender As Object, e As CancelEventArgs) Handles CMBMAKE.Validating
        Try
            If CMBMAKE.Text.Trim <> "" Then MAKEVALIDATE(CMBMAKE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Enter(sender As Object, e As EventArgs) Handles CMBUNIT.Enter
        Try
            If CMBUNIT.Text.Trim = "" Then fillunit(CMBUNIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBUNIT_Validating(sender As Object, e As CancelEventArgs) Handles CMBUNIT.Validating
        Try
            If CMBUNIT.Text.Trim <> "" Then unitvalidate(CMBUNIT, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class