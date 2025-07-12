
Imports System.ComponentModel
Imports BL

Public Class ContractorRateConfig

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDRATEDOUBLECLICK As Boolean
    Dim TEMPLABROW As Integer
    Public FRMSTRING As String = "RATE"
    Dim FRMCAPTION As String

    Private Function ERRORVALID() As Boolean
        Dim bln As Boolean = True
        gridbill.ClearColumnsFilter()

        If CMBCONTRACTOR.Text.Trim.Length = 0 Then
            EP.SetError(CMBCONTRACTOR, "Select Contractor Name")
            bln = False
        End If

        If gridbill.RowCount = 0 Then
            EP.SetError(CMBCONTRACTOR, "Enter Proper Details")
            bln = False
        End If

        Return bln
    End Function

    Sub FILLGRID()

        If GRIDRATEDOUBLECLICK = False Then
            Dim DT As DataTable = gridbilldetails.DataSource
            DT.Rows.Add(CMBITEMNAME.Text.Trim, CMBSIZE.Text.Trim, CMBCOLOR.Text.Trim, Format(Val(TXTPURCHASE.Text.Trim), "0.000"), Format(Val(TXTSALE.Text.Trim), "0.000"))
        ElseIf GRIDRATEDOUBLECLICK = True Then
            Dim DTROW As DataRow = gridbill.GetDataRow(TEMPLABROW)
            DTROW("ITEMNAME") = CMBITEMNAME.Text.Trim
            DTROW("SIZE") = CMBSIZE.Text.Trim
            DTROW("COLOR") = CMBCOLOR.Text.Trim
            DTROW("PURCHASE") = Format(Val(TXTPURCHASE.Text.Trim), "0.000")
            DTROW("SALE") = Format(Val(TXTSALE.Text.Trim), "0.000")
            GRIDRATEDOUBLECLICK = False
        End If

        CMBITEMNAME.Text = ""
        CMBSIZE.Text = ""
        CMBCOLOR.Text = ""
        TXTPURCHASE.Clear()
        TXTSALE.Clear()
        CMBITEMNAME.Focus()

    End Sub

    Private Sub ContractorItemConfig_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub FILLCMB()
        Try
            fillname(CMBCONTRACTOR, False, "")
            FILLITEMNAME(CMBITEMNAME, False)
            fillname(CMBFROMCONTRACTOR, False, "")
            FILLSIZE(CMBSIZE, False)
            FILLCOLOR(CMBCOLOR, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLFROMNAME(ByRef CMBNAME As ComboBox)
        Try
            Dim objclscommon As New ClsCommon
            Dim dt As DataTable
            dt = objclscommon.search(" ACCOUNTSMASTER.ACC_CMPNAME AS NAME ", "", " CONTRACTOR" & FRMSTRING & "CONFIG INNER JOIN ACCOUNTSMASTER ON CONTRACTOR" & FRMSTRING & "CONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID ", " AND CONTRACTOR" & FRMSTRING & "CONFIG.CONFIG_YEARID = " & YearId)
            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "NAME"
                CMBNAME.DataSource = dt
                CMBNAME.DisplayMember = "NAME"
            End If
            CMBNAME.SelectedItem = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub CLEAR()
        Try
            EP.Clear()
            TXTNO.Clear()
            CMBCONTRACTOR.Text = ""
            CMBFROMCONTRACTOR.Text = ""
            CMBITEMNAME.Text = ""
            CMBSIZE.Text = ""
            CMBCOLOR.Text = ""
            TXTPURCHASE.Clear()
            TXTSALE.Clear()
            gridbilldetails.DataSource = Nothing

            GRIDRATEDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ContractorItemConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)


            Dim OBJCMN As New ClsCommon
            Dim dttable As New DataTable

            Me.Text = "Contractor Wise " & FRMCAPTION & " Purchase / Sale Rate"

            FILLCMB()
            CLEAR()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function CHECKGRID() As Boolean
        Try
            Dim BLN As Boolean = True
            gridbill.ClearColumnsFilter()
            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If (GRIDRATEDOUBLECLICK = False And ROW("ITEMNAME") = CMBITEMNAME.Text.Trim And ROW("SIZE") = CMBSIZE.Text.Trim And ROW("COLOR") = CMBCOLOR.Text.Trim) Or (GRIDRATEDOUBLECLICK = True And I <> TEMPLABROW And ROW("ITEMNAME") = CMBITEMNAME.Text.Trim And ROW("SIZE") = CMBSIZE.Text.Trim And ROW("COLOR") = CMBCOLOR.Text.Trim) Then
                    BLN = False
                End If
            Next
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub TXTPURCHASE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSALE.KeyPress, TXTPURCHASE.KeyPress
        numdotkeypress(e, sender, Me)
    End Sub

    Private Sub TXTSALE_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSALE.Validated
        Try
            If CMBITEMNAME.Text.Trim <> "" And CMBSIZE.Text.Trim <> "" And CMBCOLOR.Text.Trim <> "" And (Val(TXTPURCHASE.Text.Trim) > 0 Or Val(TXTSALE.Text.Trim) > 0) Then
                If Not CHECKGRID() Then
                    MsgBox("Rate for This " & FRMCAPTION & " Already Exist in Grid below", MsgBoxStyle.Critical)
                    CMBITEMNAME.Focus()
                    Exit Sub
                End If
                FILLGRID()
            Else
                MsgBox("Enter Proper Details", MsgBoxStyle.Critical)
            End If
            CMBITEMNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDCLEAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDCLEAR.Click
        Try
            CLEAR()
            CMBCONTRACTOR.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDSAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDSAVE.Click
        Try
            If USERADD = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim ALPARAVAL As New ArrayList
            Dim OBJCONFIG As New ClsContractorRateConfig

            ALPARAVAL.Add(Val(TXTNO.Text.Trim))
            ALPARAVAL.Add(CMBCONTRACTOR.Text.Trim)

            Dim ITEMNAME As String = ""
            Dim SIZE As String = ""
            Dim COLOR As String = ""
            Dim PURCHASE As String = ""
            Dim SALE As String = ""

            For I As Integer = 0 To gridbill.RowCount - 1
                Dim ROW As DataRow = gridbill.GetDataRow(I)
                If ITEMNAME = "" Then
                    ITEMNAME = ROW("ITEMNAME")
                    SIZE = ROW("SIZE")
                    COLOR = ROW("COLOR")
                    PURCHASE = Val(ROW("PURCHASE"))
                    SALE = Val(ROW("SALE"))
                Else
                    ITEMNAME = ITEMNAME & "|" & ROW("ITEMNAME")
                    SIZE = SIZE & "|" & ROW("SIZE")
                    COLOR = COLOR & "|" & ROW("COLOR")
                    PURCHASE = PURCHASE & "|" & Val(ROW("PURCHASE"))
                    SALE = SALE & "|" & Val(ROW("SALE"))
                End If
            Next

            ALPARAVAL.Add(ITEMNAME)
            ALPARAVAL.Add(SIZE)
            ALPARAVAL.Add(COLOR)
            ALPARAVAL.Add(PURCHASE)
            ALPARAVAL.Add(SALE)

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJCONFIG.alParaval = ALPARAVAL
            OBJCONFIG.FRMSTRING = FRMSTRING

            Dim INT As Integer = OBJCONFIG.SAVE()
            MsgBox("Details Added")
            CLEAR()
            CMBCONTRACTOR.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCONTRACTOR.Enter
        Try
            If CMBCONTRACTOR.Text.Trim = "" Then fillname(CMBCONTRACTOR, False, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBCONTRACTOR.Validated
        Try
            If CMBCONTRACTOR.Text.Trim <> "" Then

                GRIDRATEDOUBLECLICK = False
                gridbilldetails.DataSource = Nothing
                TXTNO.Clear()
                CMBITEMNAME.Text = ""
                CMBSIZE.Text = ""
                CMBCOLOR.Text = ""
                TXTPURCHASE.Clear()
                TXTSALE.Clear()

                Dim OBJCMN As New ClsCommon
                Dim DT As DataTable = OBJCMN.search(" ITEMMASTER.ITEM_NAME AS ITEMNAME, SIZEMASTER.SIZE_NAME AS SIZE, ISNULL(COLORMASTER.COLOR_NAME,'') AS COLOR, CONTRACTORRATECONFIG_DESC.CONFIG_PURCHASERATE AS PURCHASE, CONTRACTORRATECONFIG_DESC.CONFIG_SALERATE AS SALE, CONTRACTORRATECONFIG.CONFIG_ID AS ID ", "", " CONTRACTORRATECONFIG_DESC INNER JOIN CONTRACTORRATECONFIG ON CONTRACTORRATECONFIG_DESC.CONFIG_ID = CONTRACTORRATECONFIG.CONFIG_ID INNER JOIN ACCOUNTSMASTER ON CONTRACTORRATECONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID INNER JOIN ITEMMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN SIZEMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_SIZEID = SIZEMASTER.SIZE_ID INNER JOIN COLORMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_COLORID = COLORMASTER.COLOR_ID ", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBCONTRACTOR.Text.Trim & "' AND CONTRACTORRATECONFIG.CONFIG_YEARID = " & YearId & " ORDER BY ITEMMASTER.ITEM_NAME, SIZEMASTER.SIZE_NAME, COLORMASTER.COLOR_NAME")
                gridbilldetails.DataSource = DT
                If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item("ID")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDELETE.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If gridbill.RowCount > 0 Then
                Dim TEMPMSG As Integer = MsgBox("Wish To Delete Entry?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbNo Then Exit Sub
                If Val(TXTNO.Text.Trim) > 0 Then

                    Dim OBJCONFIG As New ClsContractorRateConfig
                    Dim ALPARAVAL As New ArrayList
                    ALPARAVAL.Add(Val(TXTNO.Text.Trim))
                    ALPARAVAL.Add(Userid)
                    OBJCONFIG.alParaval = ALPARAVAL

                    Dim INTRES As Integer = OBJCONFIG.DELETE()
                End If
                CLEAR()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbill.DoubleClick
        Try
            If gridbill.GetFocusedRowCellValue("ITEMNAME") <> "" Then
                GRIDRATEDOUBLECLICK = True
                CMBITEMNAME.Text = gridbill.GetFocusedRowCellValue("ITEMNAME")
                CMBSIZE.Text = gridbill.GetFocusedRowCellValue("SIZE")
                CMBCOLOR.Text = gridbill.GetFocusedRowCellValue("COLOR")
                TXTPURCHASE.Text = Val(gridbill.GetFocusedRowCellValue("PURCHASE"))
                TXTSALE.Text = Val(gridbill.GetFocusedRowCellValue("SALE"))
                TEMPLABROW = gridbill.GetFocusedDataSourceRowIndex
                CMBITEMNAME.Focus()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbill_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridbill.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                'dont allow user if any of the grid line is in edit mode.....
                If GRIDRATEDOUBLECLICK = True Then
                    MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                    Exit Sub
                End If
                'end of block

                Dim DT As DataTable = gridbilldetails.DataSource
                DT.Rows.RemoveAt(gridbill.FocusedRowHandle)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCONTRACTOR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBCONTRACTOR.Validating
        Try
            If CMBCONTRACTOR.Text.Trim <> "" Then namevalidate(CMBCONTRACTOR, CMBCODE, e, Me, txtadd, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBITEMNAME_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSIZE.Enter
        Try
            If CMBSIZE.Text.Trim = "" Then FILLSIZE(CMBSIZE, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZE.Validating
        Try
            If CMBSIZE.Text.Trim <> "" Then SIZEVALIDATE(CMBSIZE, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBFROMCONTRACTOR_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBFROMCONTRACTOR.Validating
        Try
            If CMBFROMCONTRACTOR.Text.Trim = "" Then Exit Sub
            If CMBCONTRACTOR.Text.Trim <> "" And CMBFROMCONTRACTOR.Text.Trim <> "" And (CMBCONTRACTOR.Text.Trim <> CMBFROMCONTRACTOR.Text.Trim) Then

                If MsgBox("Wish to Copy Data?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                GRIDRATEDOUBLECLICK = False
                CMBITEMNAME.Text = ""
                TXTPURCHASE.Clear()
                TXTSALE.Clear()
                gridbilldetails.DataSource = Nothing
                TXTNO.Clear()

                Dim OBJCMN As New ClsCommon
                Dim DT, DTR As New DataTable
                DT = OBJCMN.search(" ITEMMASTER.ITEM_NAME AS ITEMNAME, SIZEMASTER.SIZE_NAME AS SIZE, COLORMASTER.COLOR_NAME AS COLOR, CONTRACTORRATECONFIG_DESC.CONFIG_PURCHASERATE AS PURCHASE, CONTRACTORRATECONFIG_DESC.CONFIG_SALERATE AS SALE, CONTRACTORRATECONFIG.CONFIG_ID AS ID ", "", " CONTRACTORRATECONFIG_DESC INNER JOIN CONTRACTORRATECONFIG ON CONTRACTORRATECONFIG_DESC.CONFIG_ID = CONTRACTORRATECONFIG.CONFIG_ID INNER JOIN ACCOUNTSMASTER ON CONTRACTORRATECONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID INNER JOIN ITEMMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN SIZEMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_SIZEID = SIZEMASTER.SIZE_ID INNER JOIN COLORMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_COLORID = COLORMASTER.COLOR_ID ", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBFROMCONTRACTOR.Text.Trim & "' AND CONTRACTORRATECONFIG.CONFIG_YEARID = " & YearId)
                gridbilldetails.DataSource = DT

                'Get ID
                DTR = OBJCMN.search(" CONTRACTORRATECONFIG.CONFIG_ID AS ID ", "", " CONTRACTORRATECONFIG_DESC INNER JOIN CONTRACTORRATECONFIG ON CONTRACTORRATECONFIG_DESC.CONFIG_ID = CONTRACTORRATECONFIG.CONFIG_ID INNER JOIN ACCOUNTSMASTER ON CONTRACTORRATECONFIG.CONFIG_ACCID = ACCOUNTSMASTER.ACC_ID INNER JOIN ITEMMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_ITEMID = ITEMMASTER.ITEM_ID INNER JOIN SIZEMASTER ON CONTRACTORRATECONFIG_DESC.CONFIG_SIZEID = SIZEMASTER.SIZE_ID ", " AND ACCOUNTSMASTER.ACC_CMPNAME = '" & CMBCONTRACTOR.Text.Trim & "' AND CONTRACTORRATECONFIG.CONFIG_YEARID = " & YearId)
                If DTR.Rows.Count > 0 Then TXTNO.Text = DTR.Rows(0).Item("ID")

            Else
                If (CMBCONTRACTOR.Text.Trim = CMBFROMCONTRACTOR.Text.Trim) Then
                    MsgBox("Contractor name cannot be same")
                    CMBFROMCONTRACTOR.SelectedItem = Nothing
                    CMBFROMCONTRACTOR.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDPRINT.Click
        Try
            If Val(TXTNO.Text.Trim) > 0 And CMBCONTRACTOR.Text.Trim <> "" Then
                Dim OBJPRICELIST As New PriceListDesign
                OBJPRICELIST.WHERECLAUSE = "{LEDGERS.ACC_CMPNAME} = '" & CMBCONTRACTOR.Text.Trim & "' AND {CONTRACTORRATECONFIG.CONFIG_YEARID} = " & YearId
                OBJPRICELIST.MdiParent = MDIMain
                OBJPRICELIST.Show()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Enter(sender As Object, e As EventArgs) Handles CMBCOLOR.Enter
        Try
            If CMBCOLOR.Text.Trim = "" Then FILLCOLOR(CMBCOLOR, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBCOLOR_Validating(sender As Object, e As CancelEventArgs) Handles CMBCOLOR.Validating
        Try
            If CMBCOLOR.Text.Trim <> "" Then COLORVALIDATE(CMBCOLOR, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class