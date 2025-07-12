Imports BL

Public Class CategoryDetails

    Public frmstring As String      'Used for form Category or GRade

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub CategoryDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Windows.Forms.Keys.E Then       'for Saving
            Call cmdedit_Click(sender, e)
        ElseIf (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf (e.Control = True And e.KeyCode = Windows.Forms.Keys.N) Or (e.KeyCode = Windows.Forms.Keys.A) Then   'for Exit
            showform(False, "", 0)
        End If
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            showform(True, gridgroup.Item(0, gridgroup.CurrentRow.Index).Value.ToString, gridgroup.Item(1, gridgroup.CurrentRow.Index).Value.ToString)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CategoryDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If frmstring = "CATEGORY" Then
                Me.Text = "Category Master"
            ElseIf frmstring = "SUBCATEGORY" Then
                Me.Text = "Sub-Category Master"
            ElseIf frmstring = "MAKE" Then
                Me.Text = "Make Master"
            ElseIf frmstring = "NARRATION" Then
                Me.Text = "Narration Master"
            ElseIf frmstring = "PATTA" Then
                Me.Text = "Patta Master"
            ElseIf frmstring = "SIZE" Then
                Me.Text = "Size Master"
            ElseIf frmstring = "PARTYBANK" Then
                Me.Text = "Bank Name Master"
            ElseIf frmstring = "DISTRICT" Then
                Me.Text = "District Master"
            ElseIf frmstring = "VIA" Then
                Me.Text = "Via Master"
            ElseIf frmstring = "HALLMARK" Then
                Me.Text = "Hallmark Master"
            End If
            fillgrid()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Dim dttable As New DataTable
        Dim objClsCommon As New ClsCommonMaster

        If frmstring = "CATEGORY" Then
            dttable = objClsCommon.search(" CATEGORY_name, CATEGORY_id", "", "CATEGORYmaster", " and CATEGORY_cmpid = " & CmpId & " and CATEGORY_Locationid = " & Locationid & " and CATEGORY_Yearid = " & YearId)
        ElseIf frmstring = "SUBCATEGORY" Then
            dttable = objClsCommon.search(" SUBCATEGORY_name, SUBCATEGORY_id", "", "SUBCATEGORYmaster", " and SUBCATEGORY_cmpid = " & CmpId & " and SUBCATEGORY_Locationid = " & Locationid & " and SUBCATEGORY_Yearid = " & YearId)
        ElseIf frmstring = "MAKE" Then
            dttable = objClsCommon.search(" MAKE_name, MAKE_id", "", "MAKEmaster", " and MAKE_cmpid = " & CmpId & " and MAKE_Locationid = " & Locationid & " and MAKE_Yearid = " & YearId)
        ElseIf frmstring = "NARRATION" Then
            dttable = objClsCommon.search(" NARRATION_name, NARRATION_id", "", "NARRATIONmaster", " and NARRATION_cmpid = " & CmpId & " and NARRATION_Locationid = " & Locationid & " and NARRATION_Yearid = " & YearId)
        ElseIf frmstring = "PATTA" Then
            dttable = objClsCommon.search(" PATTA_name, PATTA_id", "", "PATTAmaster", " and PATTA_cmpid = " & CmpId & " and PATTA_Locationid = " & Locationid & " and PATTA_Yearid = " & YearId)
        ElseIf frmstring = "SIZE" Then
            dttable = objClsCommon.search(" SIZE_name, SIZE_id", "", "SIZEmaster", " and SIZE_cmpid = " & CmpId & " and SIZE_Locationid = " & Locationid & " and SIZE_Yearid = " & YearId)
        ElseIf frmstring = "PARTYBANK" Then
            dttable = objClsCommon.search(" PARTYBANK_name, PARTYBANK_id", "", "PARTYBANKmaster", " and PARTYBANK_cmpid = " & CmpId & " and PARTYBANK_Locationid = " & Locationid & " and PARTYBANK_Yearid = " & YearId)
        ElseIf frmstring = "DISTRICT" Then
            dttable = objClsCommon.search(" DISTRICT_name, DISTRICT_id", "", "DISTRICTmaster", " and DISTRICT_cmpid = " & CmpId & " and DISTRICT_Locationid = " & Locationid & " and DISTRICT_Yearid = " & YearId)
        ElseIf frmstring = "VIA" Then
            dttable = objClsCommon.search(" VIA_name, VIA_id", "", "VIAmaster", " and VIA_cmpid = " & CmpId & " and VIA_Locationid = " & Locationid & " and VIA_Yearid = " & YearId)
        ElseIf frmstring = "HALLMARK" Then
            dttable = objClsCommon.search(" HALLMARK_name, HALLMARK_id", "", "HALLMARKmaster", " and HALLMARK_Yearid = " & YearId)
        End If

        gridgroup.DataSource = dttable
        gridgroup.Columns(0).HeaderText = "Name"

        gridgroup.Columns(0).Width = 250
        gridgroup.Columns(1).Visible = False
        gridgroup.Columns(0).SortMode = Windows.Forms.DataGridViewColumnSortMode.Automatic
        If gridgroup.RowCount > 0 Then gridgroup.FirstDisplayedScrollingRowIndex = gridgroup.RowCount - 1

    End Sub

    Private Sub gridgroup_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridgroup.CellDoubleClick
        Try
            showform(True, gridgroup.Item(0, gridgroup.CurrentRow.Index).Value.ToString, gridgroup.Item(1, gridgroup.CurrentRow.Index).Value.ToString)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub showform(ByVal editval As Boolean, ByVal name As String, ByVal id As Integer)
        Try
            Dim objCategorymaster As New CategoryMaster
            objCategorymaster.edit = editval
            objCategorymaster.MdiParent = MDIMain
            objCategorymaster.frmString = frmstring
            objCategorymaster.TempName = name
            objCategorymaster.TempID = id
            objCategorymaster.Show()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            showform(False, "", 0)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub txtcmp_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcmp.Validated
        Dim rowno, b As Integer

        fillgrid()
        rowno = 0
        For b = 1 To gridgroup.RowCount
            txttempname.Text = gridgroup.Item(0, rowno).Value.ToString()
            txttempname.SelectionStart = 0
            txttempname.SelectionLength = txtcmp.TextLength
            If LCase(txtcmp.Text.Trim) <> LCase(txttempname.SelectedText.Trim) Then
                gridgroup.Rows.RemoveAt(rowno)
            Else
                rowno = rowno + 1
            End If
        Next
    End Sub
End Class