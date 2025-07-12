
Imports BL

Public Class ItemAvgConfig

    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT
    Dim GRIDDOUBLECLICK As Boolean
    Dim TEMPROW As Integer
    Public EDIT As Boolean

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Function errorvalid() As Boolean
        Dim bln As Boolean = True

        If CMBITEMNAME.Text.Trim.Length = 0 Then
            EP.SetError(CMBITEMNAME, "Enter Item Details")
            bln = False
        End If

        If CMBPATTA.Text.Trim.Length = 0 Then
            EP.SetError(CMBPATTA, "Enter Patta")
            bln = False
        End If

        If CMBSIZE.Text.Trim.Length = 0 Then
            EP.SetError(CMBSIZE, "Enter Size")
            bln = False
        End If

        If Val(TXTQTY.Text.Trim.Length) = 0 Then
            EP.SetError(TXTQTY, "Enter Qty")
            bln = False
        End If

        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search(" AVG_NO AS NO ", "", " ITEMAVGCONFIG INNER JOIN ITEMMASTER ON ITEMAVGCONFIG.AVG_ITEMID = ITEMMASTER.item_id INNER JOIN PATTAMASTER ON ITEMAVGCONFIG.AVG_PATTAID = PATTAMASTER.PATTA_id INNER JOIN SIZEMASTER ON ITEMAVGCONFIG.AVG_SIZEID = SIZEMASTER.SIZE_id ", " AND ITEMMASTER.item_name = '" & CMBITEMNAME.Text.Trim & "' AND PATTAMASTER.PATTA_NAME = '" & CMBPATTA.Text.Trim & "' AND SIZEMASTER.SIZE_NAME = '" & CMBSIZE.Text.Trim & "' AND AVG_YEARID = " & YearId)
        If DT.Rows.Count > 0 Then
            If GRIDDOUBLECLICK = False Or (GRIDDOUBLECLICK = True And Val(TXTNO.Text) <> Val(DT.Rows(0).Item("NO"))) Then
                EP.SetError(TXTQTY, "ITEM ALREADY PRESENT .....")
                bln = False
            End If
        End If

        Return bln
    End Function

    Sub EDITROW()
        Try

            If gridbill.GetFocusedRowCellValue("NO") > 0 Then
                GRIDDOUBLECLICK = True
                TXTNO.Text = Val(gridbill.GetFocusedRowCellValue("NO"))
                CMBITEMNAME.Text = gridbill.GetFocusedRowCellValue("ITEMNAME")
                CMBPATTA.Text = gridbill.GetFocusedRowCellValue("PATTA")
                CMBSIZE.Text = gridbill.GetFocusedRowCellValue("SIZE")
                TXTQTY.Text = Val(gridbill.GetFocusedRowCellValue("QTY"))
                TXTSRNO.Focus()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBITEMNAME.Enter
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbMERCHANT_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBITEMNAME.Validating
        Try
            If CMBITEMNAME.Text.Trim <> "" Then ITEMNAMEVALIDATE(CMBITEMNAME, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillcmb()
        Try
            If CMBITEMNAME.Text.Trim = "" Then FILLITEMNAME(CMBITEMNAME, EDIT)
            If CMBPATTA.Text.Trim = "" Then FILLPATTA(CMBPATTA, EDIT)
            If CMBSIZE.Text.Trim = "" Then FILLSIZE(CMBSIZE, EDIT)

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub fillgrid()
        Dim OBJCMN As New ClsCommon
        Dim DT As DataTable = OBJCMN.search(" ISNULL(ITEMAVGCONFIG.AVG_NO, 0) AS NO, ISNULL(ITEMAVGCONFIG.AVG_QTY, 0) AS QTY, ISNULL(PATTAMASTER.PATTA_name, '') AS PATTA, ISNULL(SIZEMASTER.SIZE_name, '') AS SIZE, ISNULL(ITEMMASTER.item_name, '') AS ITEMNAME", "", " ITEMAVGCONFIG INNER JOIN ITEMMASTER ON ITEMAVGCONFIG.AVG_ITEMID = ITEMMASTER.item_id INNER JOIN SIZEMASTER ON ITEMAVGCONFIG.AVG_SIZEID = SIZEMASTER.SIZE_id INNER JOIN PATTAMASTER ON ITEMAVGCONFIG.AVG_PATTAID = PATTAMASTER.PATTA_id ", " AND AVG_YEARID = " & YearId & " order by dbo.ITEMAVGCONFIG.AVG_NO desc ")
        gridbilldetails.DataSource = DT
    End Sub

    Private Sub ITEMAVGCONFIG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If (e.Alt = True And e.KeyCode = Windows.Forms.Keys.X) Or (e.KeyCode = Windows.Forms.Keys.Escape) Then   'for Exit
            Me.Close()
        ElseIf e.KeyCode = Keys.F5 Then
            gridbilldetails.Focus()
        ElseIf e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemPipe Then
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Sub CLEAR()
        Try
            TXTNO.Clear()
            TXTQTY.Clear()
            TXTSRNO.Clear()
            CMBITEMNAME.Text = ""
            CMBSIZE.Text = ""
            CMBPATTA.Text = ""
            GRIDDOUBLECLICK = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PriceList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim DTROW() As DataRow = USERRIGHTS.Select("FormName = 'ITEM MASTER'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            fillcmb()
            CLEAR()

            fillgrid()
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBSIZE_enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBSIZE.Enter
        Try
            If CMBSIZE.Text.Trim = "" Then FILLSIZE(CMBSIZE, EDIT)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub cmbSIZE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBSIZE.Validating
        Try
            If CMBSIZE.Text.Trim <> "" Then SIZEVALIDATE(CMBSIZE, e, Me)
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Private Sub CMBPATTA_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMBPATTA.Enter
        Try
            If CMBPATTA.Text.Trim = "" Then FILLPATTA(CMBPATTA, EDIT)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPATTA_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CMBPATTA.Validating
        Try
            If CMBPATTA.Text.Trim <> "" Then PATTAVALIDATE(CMBPATTA, e, Me)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SAVE()
        Try
            Dim ALPARAVAL As New ArrayList
            Dim OBJSM As New ClsItemAvgConfig


            ALPARAVAL.Add(TXTSRNO.Text.Trim)
            ALPARAVAL.Add(CMBITEMNAME.Text.Trim)
            ALPARAVAL.Add(CMBPATTA.Text.Trim)
            ALPARAVAL.Add(CMBSIZE.Text.Trim)
            ALPARAVAL.Add(Val(TXTQTY.Text.Trim))

            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJSM.alParaval = ALPARAVAL
            If GRIDDOUBLECLICK = False Then
                Dim DT As DataTable = OBJSM.SAVE()
                If DT.Rows.Count > 0 Then TXTNO.Text = DT.Rows(0).Item(0)

            Else
                ALPARAVAL.Add(TXTNO.Text.Trim)
                Dim INTRES As Integer = OBJSM.UPDATE()

            End If
            GRIDDOUBLECLICK = False
            CMBITEMNAME.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gridbilldetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridbilldetails.DoubleClick
        EDITROW()
    End Sub

    Private Sub gridbilldetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gridbilldetails.KeyDown
        Try
            If e.KeyCode = Keys.F5 Then
                EDITROW()
            ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMBPRINT_Click(sender As Object, e As EventArgs) Handles CMBPRINT.Click
        Try

            Dim PATH As String = Application.StartupPath & "\Config Entry.XLS"
            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True
            opti.SheetName = "Config Entry"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "Config Entry", gridbill.VisibleColumns.Count + gridbill.GroupCount)
        Catch ex As Exception
            MsgBox("Config Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TXTRATE_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TXTQTY.Validating
        Try
            EP.Clear()
            If Not errorvalid() Then
                Exit Sub
            End If

            If CMBITEMNAME.Text.Trim <> "" Then
                SAVE()
                CLEAR()
                fillgrid()
            Else
                MsgBox("Enter Proper Details")
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CMDDELETE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If USERDELETE = False Then
                MsgBox("Insufficient Rights")
                Exit Sub
            End If

            If GRIDDOUBLECLICK = True Then
                MessageBox.Show("Row is in Edited Mode, You Cannot Delete This Row")
                Exit Sub
            End If

            Dim TEMPMSG As Integer = MsgBox("Wish To Delete?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbNo Then Exit Sub


            'DELETE FROM TABLE
            Dim OBJSM As New ClsItemAvgConfig
            Dim ALPARAVAL As New ArrayList
            ALPARAVAL.Add(gridbill.GetFocusedRowCellValue("NO"))
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(YearId)

            OBJSM.alParaval = ALPARAVAL
            Dim INTRES As Integer = OBJSM.DELETE()
            CLEAR()
            fillgrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class