
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class SaleInvoiceDesign

    Public NAME As String = ""
    Public INVNO As Integer = 0
    Public WHERECLAUSE As String
    Public FRMSTRING As String
    Public PERIOD As String
    Public strsearch As String
    Public QUOTENO As Integer
    Public PRINTIGST As Boolean
    Public INVOICETYPE As String = ""

    Public NEWPAGE As Boolean
    Public ADDRESS As Boolean
    Public SHOWPRINTDATE As Boolean
    Public SHOWHEADER As Boolean

    Public DIRECTPRINT As Boolean = False
    Public DIRECTMAIL As Boolean = False
    Public registername As String
    Public PRINTSETTING As Object = Nothing
    Public NOOFCOPIES As Integer = 1


    Dim RPTSALEINVOICE As New SaleInvoiceReport
    Dim RPTSALEINVOICEGAJ As New SaleInvoiceReportGAJ

    Dim RPTINVOICE_HM As New SaleInvoiceReport_HM
    Dim RPTINVOICE As New SaleInvoiceReport_SHENSHE
    Dim RPTISSVOUCHER As New IssueVoucherReport

    Dim RPTCASHMEMO As New CashMemoReport
    Dim RPTQUOTE As New SaleQuotationReport
    Dim RPTQUOTEPDF As New SaleQuotationReportPDF

    Dim RPTCONTISSUE As New ContractorIssReport
    Dim RPTCONTREC As New ContractorRecReport

    Dim RPTCHALLAN_GELATO As New ChallanReport_GELATO
    Dim RPTINVOICE_GELATO As New SaleInvoiceReport_GELATO
    Dim RPTINVOICE_GELATOIGST As New SaleInvoiceReport_GELATOIGST

    Private Sub SaleQuotationDesign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Enter Then
                SendKeys.Send("{tab}")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaleQuotationDesign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If DIRECTPRINT = True Then
                PRINTDIRECTLYTOPRINTER()
                Exit Sub
            End If


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

            If FRMSTRING = "INVOICE" Then
                If ClientName = "SHENSHE" Then
                    crTables = RPTINVOICE.Database.Tables
                ElseIf ClientName = "GAJANAN" Then
                    crTables = RPTSALEINVOICEGAJ.Database.Tables
                ElseIf ClientName = "HMENTERPRISE" Then
                    crTables = RPTINVOICE_HM.Database.Tables
                ElseIf ClientName = "GELATO" Then
                    If PRINTIGST = True Then
                        crTables = RPTINVOICE_GELATOIGST.Database.Tables
                    Else
                        crTables = RPTINVOICE_GELATO.Database.Tables
                    End If
                Else
                    crTables = RPTSALEINVOICE.Database.Tables
                End If

            ElseIf FRMSTRING = "CHALLAN" Then
                crTables = RPTCHALLAN_GELATO.Database.Tables
            ElseIf FRMSTRING = "ISSUEVOUCHER" Then
                crTables = RPTISSVOUCHER.Database.Tables

            ElseIf FRMSTRING = "QUOTE" Then
                crTables = RPTQUOTE.Database.Tables
            ElseIf FRMSTRING = "CASHMEMO" Then
                crTables = RPTCASHMEMO.Database.Tables
            ElseIf FRMSTRING = "CONTRACTORISSUE" Then
                crTables = RPTCONTISSUE.Database.Tables
            ElseIf FRMSTRING = "CONTRACTORRECEIPT" Then
                crTables = RPTCONTREC.Database.Tables
            End If

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next



            CRPO.SelectionFormula = WHERECLAUSE

            If FRMSTRING = "INVOICE" Then
                If ClientName = "SHENSHE" Then
                    CRPO.ReportSource = RPTINVOICE
                ElseIf ClientName = "GAJANAN" Then
                    CRPO.ReportSource = RPTSALEINVOICEGAJ
                ElseIf ClientName = "HMENTERPRISE" Then
                    CRPO.ReportSource = RPTINVOICE_HM
                ElseIf ClientName = "GELATO" Then
                    If PRINTIGST = True Then
                        CRPO.ReportSource = RPTINVOICE_GELATOIGST
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICETYPE & "'"
                    Else
                        CRPO.ReportSource = RPTINVOICE_GELATO
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("INVOICETYPE").Text = "'" & INVOICETYPE & "'"
                    End If
                Else
                    CRPO.ReportSource = RPTSALEINVOICE
                End If
            ElseIf FRMSTRING = "CHALLAN" Then
                CRPO.ReportSource = RPTCHALLAN_GELATO
            ElseIf FRMSTRING = "ISSUEVOUCHER" Then
                CRPO.ReportSource = RPTISSVOUCHER
            ElseIf FRMSTRING = "QUOTE" Then
                CRPO.ReportSource = RPTQUOTE
            ElseIf FRMSTRING = "CASHMEMO" Then
                CRPO.ReportSource = RPTCASHMEMO
            ElseIf FRMSTRING = "CONTRACTORISSUE" Then
                CRPO.ReportSource = RPTCONTISSUE
            ElseIf FRMSTRING = "CONTRACTORRECEIPT" Then
                CRPO.ReportSource = RPTCONTREC
            End If

            CRPO.Zoom(100)
            CRPO.Refresh()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub PRINTDIRECTLYTOPRINTER()
        Try
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldDefinition As ParameterFieldDefinition
            Dim crParameterValues As New ParameterValues
            Dim crParameterDiscreteValue As New ParameterDiscreteValue

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


            Dim OBJ As New Object
            If FRMSTRING = "INVOICE" Then

                strsearch = strsearch & " {INVOICEMASTER.INVOICE_no}= " & INVNO & " AND {REGISTERMASTER.REGISTER_NAME} = '" & registername & "' AND {INVOICEMASTER.INVOICE_cmpid} = " & CmpId & " AND {INVOICEMASTER.INVOICE_locationid} = " & Locationid & " AND {INVOICEMASTER.INVOICE_yearid} = " & YearId
                CRPO.SelectionFormula = strsearch


                If ClientName = "SHENSHE" Then
                    OBJ = New SaleInvoiceReport_SHENSHE
                ElseIf ClientName = "GAJANAN" Then
                    OBJ = New SaleInvoiceReportGAJ
                ElseIf ClientName = "HMENTERPRISE" Then
                    OBJ = New SaleInvoiceReport_HM
                ElseIf ClientName = "GELATO" Then
                    If PRINTIGST = True Then
                        OBJ = New SaleInvoiceReport_GELATOIGST
                    Else
                        OBJ = New SaleInvoiceReport_GELATO
                    End If
                Else
                    OBJ = New SaleInvoiceReport
                End If


            End If

SKIPINVOICE:
            crTables = OBJ.Database.Tables

            For Each crTable In crTables
                crtableLogonInfo = crTable.LogOnInfo
                crtableLogonInfo.ConnectionInfo = crConnecttionInfo
                crTable.ApplyLogOnInfo(crtableLogonInfo)
            Next

            OBJ.RecordSelectionFormula = strsearch

            If DIRECTMAIL = False Then
                OBJ.PrintOptions.PrinterName = PRINTSETTING.PrinterSettings.PrinterName
                OBJ.PrintToPrinter(Val(NOOFCOPIES), True, 0, 0)
            Else
                Dim expo As New ExportOptions
                Dim oDfDopt As New DiskFileDestinationOptions
                If FRMSTRING = "INVOICE" Then oDfDopt.DiskFileName = Application.StartupPath & "\INVOICE_" & INVNO & ".pdf"

                'CHECK WHETHER FILE IS PRESENT OR NOT, IF PRESENT THEN DELETE FIRST AND THEN EXPORT
                If File.Exists(oDfDopt.DiskFileName) Then File.Delete(oDfDopt.DiskFileName)

                expo = OBJ.ExportOptions
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                OBJ.Export()
                OBJ.DataDefinition.FormulaFields("SENDMAIL").Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub SendMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMail.Click
        Try
            Dim emailid As String = ""
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            TRANSFER()
            Dim TEMPATTACHMENT As String = ""

            Dim objmail As New SendMail

            If FRMSTRING = "INVOICE" Then
                TEMPATTACHMENT = "Inv-" & Val(INVNO) & "-" & NAME
                objmail.subject = "Invoice"

            ElseIf FRMSTRING = "CHALLAN" Then
                TEMPATTACHMENT = "Challan"
            ElseIf FRMSTRING = "ISSUEVOUCHER" Then
                TEMPATTACHMENT = "IssVoucher"
            ElseIf FRMSTRING = "CASHMEMO" Then
                TEMPATTACHMENT = "Cash Memo"
            Else
                TEMPATTACHMENT = "Sale Quotation"
            End If

            Try
                'Dim objmail As New SendMail
                objmail.attachment = Application.StartupPath & "\" & TEMPATTACHMENT & ".PDF"
                objmail.subject = TEMPATTACHMENT
                If emailid <> "" Then objmail.cmbfirstadd.Text = emailid
                objmail.Show()
                objmail.BringToFront()
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            If ErrHandle(ex.Message.GetHashCode) = False Then Throw ex
        End Try
    End Sub

    Sub TRANSFER()
        Try

            Dim expo As New ExportOptions
            Dim oDfDopt As New DiskFileDestinationOptions

            If FRMSTRING = "INVOICE" Then
                If ClientName = "SHENSHE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\Inv-" & Val(INVNO) & "-" & NAME & ".PDF"
                    expo = RPTINVOICE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE.Export()
                ElseIf ClientName = "GAJANAN" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\Inv-" & Val(INVNO) & "-" & NAME & ".PDF"
                    expo = RPTSALEINVOICEGAJ.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALEINVOICEGAJ.Export()
                ElseIf ClientName = "HMENTERPRISE" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\Inv-" & Val(INVNO) & "-" & NAME & ".PDF"
                    expo = RPTINVOICE_HM.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTINVOICE_HM.Export()
                ElseIf ClientName = "GELATO" Then
                    oDfDopt.DiskFileName = Application.StartupPath & "\Inv-" & Val(INVNO) & "-" & NAME & ".PDF"
                    If PRINTIGST = False Then
                        expo = RPTINVOICE_GELATO.ExportOptions
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_GELATO.Export()
                        RPTINVOICE_GELATO.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                    Else
                        expo = RPTINVOICE_GELATOIGST.ExportOptions
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                        expo.ExportDestinationType = ExportDestinationType.DiskFile
                        expo.ExportFormatType = ExportFormatType.PortableDocFormat
                        expo.DestinationOptions = oDfDopt
                        RPTINVOICE_GELATOIGST.Export()
                        RPTINVOICE_GELATOIGST.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                    End If
                Else
                    RPTSALEINVOICE.DataDefinition.FormulaFields("SENDMAIL").Text = 1
                    oDfDopt.DiskFileName = Application.StartupPath & "\Inv-" & Val(INVNO) & "-" & NAME & ".PDF"
                    expo = RPTSALEINVOICE.ExportOptions
                    expo.ExportDestinationType = ExportDestinationType.DiskFile
                    expo.ExportFormatType = ExportFormatType.PortableDocFormat
                    expo.DestinationOptions = oDfDopt
                    RPTSALEINVOICE.Export()
                    RPTSALEINVOICE.DataDefinition.FormulaFields("SENDMAIL").Text = 0
                End If
            ElseIf FRMSTRING = "CHALLAN" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Challan.PDF"
                expo = RPTCHALLAN_GELATO.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCHALLAN_GELATO.Export()
            ElseIf FRMSTRING = "ISSUEVOUCHER" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\IssVoucher.PDF"
                expo = RPTISSVOUCHER.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTISSVOUCHER.Export()
            ElseIf FRMSTRING = "QUOTE" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Sale Quotation.PDF"
                expo = RPTQUOTEPDF.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTQUOTEPDF.Export()
            ElseIf FRMSTRING = "CASHMEMO" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Cash Memo.PDF"
                expo = RPTQUOTEPDF.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCASHMEMO.Export()
            ElseIf FRMSTRING = "CONTRACTORISSUE" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Cash Memo.PDF"
                expo = RPTCONTISSUE.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCONTISSUE.Export()
            ElseIf FRMSTRING = "CONTRACTORRECEIPT" Then
                oDfDopt.DiskFileName = Application.StartupPath & "\Cash Memo.PDF"
                expo = RPTCONTREC.ExportOptions
                expo.ExportDestinationType = ExportDestinationType.DiskFile
                expo.ExportFormatType = ExportFormatType.PortableDocFormat
                expo.DestinationOptions = oDfDopt
                RPTCONTREC.Export()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class