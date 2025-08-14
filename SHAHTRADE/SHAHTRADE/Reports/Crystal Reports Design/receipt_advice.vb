
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.Form
Imports CrystalDecisions.Shared

Public Class receipt_advice
    Public recno As Integer
    Public recname As String
    Public REGNAME As String
    Dim RPTREC As New recreport
    Dim RPTREC_AASHAAYEN As New RecReport_AASHAAYEN

    Private Sub receipt_advice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control = True And e.KeyCode = Keys.P Then
            CRPO.PrintReport()
        End If
    End Sub

    Private Sub receipt_advice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strsearch As String
        strsearch = ""

        Try


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

            If ClientName = "AASHAAYEN" Then
                crTables = RPTREC_AASHAAYEN.Database.Tables
            Else
                crTables = RPTREC.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next
            '************* END *******************


            strsearch = strsearch & "  {receiptmaster.receipt_no}= " & recno & " and {RECEIPT_REPORT.REGNAME}= '" & REGNAME & "' and {ledgermaster.Acc_cmpname} = '" & recname & "' and {receiptmaster.receipt_cmpid} = " & CmpId & " and {receiptmaster.receipt_LOCATIONid} = " & Locationid & " and {receiptmaster.receipt_YEARid} = " & YearId
            CRPO.SelectionFormula = strsearch

            If ClientName = "AASHAAYEN" Then
                CRPO.ReportSource = RPTREC_AASHAAYEN
            Else
                CRPO.ReportSource = RPTREC
            End If
            CRPO.Zoom(100)
            CRPO.Refresh()

        Catch Exp As LoadSaveReportException
            MsgBox("Incorrect path for loading report.",
                    MsgBoxStyle.Critical, "Load Report Error")

        Catch Exp As Exception
            MsgBox(Exp.Message, MsgBoxStyle.Critical, "General Error")

        End Try
    End Sub

    Sub Transfer()
        Try
            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions



            oDfDopt.DiskFileName = Application.StartupPath & "\RECEIPTREPORT.PDF"

            If ClientName = "AASHAAYEN" Then
                expo = RPTREC_AASHAAYEN.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREC_AASHAAYEN.Export()
            Else
                expo = RPTREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTREC.Export()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub sendmailtool_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sendmailtool.Click
        Dim emailid As String = ""
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Transfer()
        Dim tempattachment As String

        Dim objmail As New SendMail

        tempattachment = "RECEIPTREPORT"
        objmail.subject = "Receipt Voucher"

        Try
            'Dim objmail As New SendMail
            objmail.attachment = tempattachment
            objmail.attachment = Application.StartupPath & "\" & tempattachment & ".PDF"
            If emailid <> "" Then
                objmail.cmbfirstadd.Text = emailid
            End If
            objmail.Show()
            objmail.BringToFront()
        Catch ex As Exception
            Throw ex
        End Try
        Windows.Forms.Cursor.Current = Cursors.Arrow
    End Sub
End Class