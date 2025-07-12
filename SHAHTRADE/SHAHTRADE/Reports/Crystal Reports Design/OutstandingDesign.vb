Imports BL
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms

Public Class OutstandingDesign


    Dim RPTOUTSTANDINGRECSUMM As New OutstandingReport_Summary_Rec
    Dim RPTOUTSTANDINGRECDTLS As New OutstandingReport_Details_Rec

    'NEWLY ADDED
    Public REPORTNAME As String
    Public DAYS As String
    Public ADDRESS As Integer
    Public NEWPAGE As Boolean
    Public FRMSTRING As String
    Public selfor_ss As String
    Public PERIOD As String
    Public INTEREST As Double
    Public INTDAYS As Integer
    Public SHOWPRINTDATE As Integer

    Private Sub OutstandingDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor.Current = Cursors.WaitCursor

            '**************** SET SERVER ************************
            Dim crtableLogonInfo As New TableLogOnInfo
            Dim crConnecttionInfo As New ConnectionInfo
            Dim crTables As Tables
            Dim crTable As Table


            With crConnecttionInfo
                .ServerName = SERVERNAME
                .DatabaseName = DatabaseName
                .UserID = DBUSERNAME
                .Password = Dbpassword
                .IntegratedSecurity = Dbsecurity
            End With


            If FRMSTRING = "OUTSTANDINGRECSUMM" Then crTables = RPTOUTSTANDINGRECSUMM.Database.Tables
            If FRMSTRING = "OUTSTANDINGRECDTLS" Then crTables = RPTOUTSTANDINGRECDTLS.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

           If FRMSTRING = "OUTSTANDINGRECSUMM" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECSUMM.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECSUMM.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTOUTSTANDINGRECSUMM.Database.Tables

            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then

                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("CALDAYS").Text = "'" & DAYS & "'"
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("ADDRESS").Text = ADDRESS
                RPTOUTSTANDINGRECDTLS.GroupFooterSection1.SectionFormat.EnableNewPageAfter = NEWPAGE
                RPTOUTSTANDINGRECDTLS.DataDefinition.FormulaFields("SHOWPRINTDATE").Text = SHOWPRINTDATE
                crTables = RPTOUTSTANDINGRECDTLS.Database.Tables

            End If

            CRPO.SelectionFormula = selfor_ss

            If FRMSTRING = "OUTSTANDINGRECSUMM" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECSUMM
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                CRPO.ReportSource = RPTOUTSTANDINGRECDTLS
            End If
            '************************ END *******************

            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Transfer()
            Dim objmail As New SendMail
            objmail.attachment = Application.StartupPath & "\Outstanding Report.PDF"
            If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
            objmail.Show()
            objmail.BringToFront()
            Windows.Forms.Cursor.Current = Cursors.Arrow
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions
            oDfDopt.DiskFileName = Application.StartupPath & "\Outstanding Report.pdf"

            If FRMSTRING = "OUTSTANDINGRECSUMM" Then
                expo = RPTOUTSTANDINGRECSUMM.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECSUMM.Export()
            ElseIf FRMSTRING = "OUTSTANDINGRECDTLS" Then
                expo = RPTOUTSTANDINGRECDTLS.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTOUTSTANDINGRECDTLS.Export()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub OutstandingDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class