
Imports Excel
'Imports DB
'Imports AsianERPBL.ModGeneral
'Imports AsianERPBL.Account
Imports System.Data
Imports Microsoft.Office.Interop


'Imports Microsoft.Office.Interop.Excel


Public Class clsReportDesigner
    'Private objDBOperation As DB.DBOperation

    'Public objUserDetails As ObjUser
    'Private objRepCenter As New clsRepCenter
    Dim dsResult As New DataSet
    Public ALPARAVAL As New ArrayList
    Dim dv As New DataView

    Public Sub New()
        Try
            'objDBOperation = New DB.DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region " INTERNAL MANAGEMENT DECLARATIONS ............. "


#Region "Private Declarations..."
    Private objColumn As New Hashtable
    Private objSheet As Excel.Worksheet
    Private objExcel As Excel.Application
    Private objBook As Excel.Workbook
    'Private objUser As New clsUser
    Private RowIndex As Integer
    Private currentColumn As String
    Private _Report_Title As String
    Private _SaveFilePath As String
    Private _PreviewOption As Integer
#End Region

    Public Sub New(ByVal Report_Title As String, ByVal SaveFilePath As String, ByVal PreivewOption As Integer)
        Dim proc As System.Diagnostics.Process
        Try
            _Report_Title = Report_Title
            _SaveFilePath = SaveFilePath
            _PreviewOption = PreivewOption
            Try
                For Each proc In System.Diagnostics.Process.GetProcessesByName("Excel")
                    If proc.MainWindowTitle = Report_Title Then
                        proc.Kill()
                    End If
                Next
            Catch ex As Exception

            End Try
            ' try{
            '    foreach (Process thisproc in Process.GetProcessesByName(processName)) {
            'if(!thisproc.CloseMainWindow()){
            '//If closing is not successful or no desktop window handle, then force termination.
            'thisproc.Kill();
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetWorkSheet()
        Try
            objExcel = New Excel.Application
            If Not objExcel Is Nothing Then
                objBook = objExcel.Workbooks.Add
                objSheet = DirectCast(objBook.ActiveSheet, Excel.Worksheet)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Quit()
        Try
            objSheet = Nothing
            objBook.Close()
            ReleaseObject(objBook)
            objExcel.Quit()
            ReleaseObject(objExcel)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReleaseObject(ByVal o As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o)
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Private Sub SaveAndClose()
        Try
            objExcel.AlertBeforeOverwriting = False
            objExcel.DisplayAlerts = False
            objSheet.SaveAs(_SaveFilePath)

            If _PreviewOption = 1 Then 'Open In Web Browser                
                objBook.WebPagePreview()
            ElseIf _PreviewOption = 2 Then 'Open In Excel                
                objExcel.Visible = True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Try
                If _PreviewOption <> 2 Then Quit()
            Catch ex As Exception
            End Try
        End Try
    End Sub

    Private Sub SetColumn(ByVal Key As String, ByVal ForColumn As String)
        Try
            objColumn.Add(Key, ForColumn)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ReSetColumn()
        Try
            objColumn.Clear()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private ReadOnly Property Column(ByVal Key As String) As String
        Get
            Try
                Return objColumn.Item(Key).ToString
            Catch ex As Exception
                Throw ex
            End Try
        End Get
    End Property

    Private ReadOnly Property Range(ByVal Key As String) As String
        Get
            Try
                Return objColumn.Item(Key).ToString & RowIndex.ToString
            Catch ex As Exception
                Throw ex
            End Try
        End Get
    End Property

    Private Sub Write(ByVal Text As Object, ByVal Range As String, ByVal Align As Excel.XlHAlign, _
        Optional ByVal ToRange As String = Nothing, Optional ByVal FontBold As Boolean = False, _
        Optional ByVal FontSize As Int16 = 9, Optional ByVal WrapText As Boolean = False, Optional ByVal _
fontItalic As Boolean = False)
        Try
            objSheet.Range(Range).FormulaR1C1 = Text
            'objSheet.Range(Range).BorderAround()
            If Not ToRange Is Nothing Then
                objSheet.Range(Range, ToRange).Merge()
                'objSheet.Range(Range, ToRange).BorderAround()
            End If
            objSheet.Range(Range).HorizontalAlignment = Align
            If FontBold Then objSheet.Range(Range).Font.Bold = True
            If FontSize > 0 Then objSheet.Range(Range).Font.Size = FontSize
            If WrapText Then objSheet.Range(Range).WrapText = True
            If fontItalic Then objSheet.Range(Range).Font.Italic = True


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FORMULA(ByVal Text As Object, ByVal Range As String, ByVal Align As Excel.XlHAlign, _
       Optional ByVal ToRange As String = Nothing, Optional ByVal FontBold As Boolean = False, _
       Optional ByVal FontSize As Int16 = 9, Optional ByVal WrapText As Boolean = False, Optional ByVal _
fontItalic As Boolean = False)
        Try
            objSheet.Range(Range).Formula = Text
            'objSheet.Range(Range).BorderAround()
            If Not ToRange Is Nothing Then
                objSheet.Range(Range, ToRange).Merge()
                'objSheet.Range(Range, ToRange).BorderAround()
            End If
            objSheet.Range(Range).HorizontalAlignment = Align
            If FontBold Then objSheet.Range(Range).Font.Bold = True
            If FontSize > 0 Then objSheet.Range(Range).Font.Size = FontSize
            If WrapText Then objSheet.Range(Range).WrapText = True
            If fontItalic Then objSheet.Range(Range).Font.Italic = True


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LOCKCELLS(ByVal VALUE As Boolean, ByVal Range As String, Optional ByVal ToRange As String = Nothing)
        Try
            If Not ToRange Is Nothing Then
                objSheet.Range(Range, ToRange).Locked = VALUE
            Else
                objSheet.Range(Range).Locked = VALUE
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetBorder(ByVal RowIndex As Integer, Optional ByVal Range As String = Nothing, Optional ByVal ToRange As String = Nothing)

        Dim intI As Integer = 0
        ''RowIndex = 0
        'obj()
        'objSheet.Selec
        'objExcel.Selection("A1:N17").ToString()
        objSheet.Range(Range, ToRange).Select()
        objSheet.Range(Range, ToRange).BorderAround(, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, )
        'For intI = 1 To RowIndex
        '    objSheet.Range(Range, ToRange).Select()
        '    objSheet.Range(Range, ToRange).BorderAround(, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, )
        '    intI += 1
        'Next
    End Sub

    Private Sub SetColumnWidth(ByVal Range As String, ByVal width As Integer)
        'objSheet.Range(Range).BorderAround()
        objSheet.Range(Range).ColumnWidth = width
        '  = objSheet.Range(Range).Select()
        'objSheet.Range(Range).EditionOptions(XlEditionType.xlPublisher, XlEditionOptionsOption.xlAutomaticUpdate, , , XlPictureAppearance.xlScreen, XlPictureAppearance.xlScreen)
    End Sub

    Private Function NextColumn() As String
        Dim nxtColumn As String = String.Empty
        Try
            Dim i As Integer = 0
            Dim length As Integer = currentColumn.Length
            For i = length - 1 To 0 Step -1
                If Not (currentColumn(i).ToString.ToUpper = "Z") Then
                    Dim substr As String = String.Empty
                    If i > 0 Then
                        substr = currentColumn.Substring(0, i)
                    End If
                    nxtColumn = Convert.ToString(Convert.ToChar(Convert.ToInt32(currentColumn(i)) + 1)) & nxtColumn
                    nxtColumn = substr & nxtColumn
                    Exit For
                ElseIf currentColumn(i).ToString.ToUpper = "Z" Then
                    nxtColumn = "A" & nxtColumn
                End If
                If i = 0 Then
                    If Convert.ToString(currentColumn(i)).ToUpper = "Z" Then
                        nxtColumn = "A" & nxtColumn
                    End If
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
        currentColumn = nxtColumn
        Return nxtColumn
    End Function

    Private Sub MeargeCell(ByVal StartCol As String, ByVal StartRow As String, ByVal EndCol As String, ByVal EndRow As String)
        Try

            'objSheet.Range(StartCol & StartRow & ":" & EndCol & EndRow).Merge()
            objSheet.Range(StartCol, EndCol).Merge()

            'With objSheet.Selection                
            '    .WrapText = False
            '    .Orientation = 0
            '    .AddIndent = False
            '    .IndentLevel = 0
            '    .ShrinkToFit = False                
            '    .MergeCells = True
            'End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "CMPHEADER"

    Sub CMPHEADER(ByVal CMPID As Integer, ByVal REPORTTITLE As String)
        Try
            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            'CMPNAME
            Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

            RowIndex = 2
            Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("6"), True, 14)
            SetBorder(RowIndex, Range("1"), Range("6"))

            'ADD1
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("6"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("6"))

            'ADD2
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("6"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("6"))

            RowIndex += 1
            Write(REPORTTITLE, Range("1"), XlHAlign.xlHAlignCenter, Range("6"), True, 12)
            SetBorder(RowIndex, Range("1"), Range("6"))


            'FREEZE TOP 7 ROWS
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("6").ToString & 8).Select()
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("6").ToString & 8).Application.ActiveWindow.FreezePanes = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "GST REPORTS"

    Public Function GSTSUMMARY_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("1"), 25)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable
            Dim DTNP As New System.Data.DataTable
            'CMPNAME
            Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

            RowIndex = 2
            Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("7"), True, 14)
            SetBorder(RowIndex, Range("1"), Range("7"))

            'ADD1
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("7"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("7"))

            'ADD2
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("7"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("7"))

            RowIndex += 1
            Write("GST SUMMARY (" & Format(FROMDATE, "dd/MM/yyyy") & "-" & Format(TODATE, "dd/MM/yyyy") & ")", Range("1"), XlHAlign.xlHAlignCenter, Range("7"), True, 12)
            SetBorder(RowIndex, Range("1"), Range("7"))


            'FREEZE TOP 7 ROWS
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("7").ToString & 8).Select()
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("7").ToString & 8).Application.ActiveWindow.FreezePanes = True


            SetBorder(RowIndex + 1, objColumn.Item("1").ToString & RowIndex + 1, objColumn.Item("7").ToString & RowIndex + 1)

            RowIndex += 2
            Write("", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("TAXABLE AMT", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("CGST", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("SGST", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TAX C+S", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("IGST", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TOTAL", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)

            'FIRST GET OPENING WITH RESPECT TO FROM DATE
            'FOR NOW OPENING WILL BE BLANK
            RowIndex += 1
            Write("OPENING", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)

            Dim WHERECLAUSE As String = ""
            'If REGNAME <> "" Then WHERECLAUSE = " AND REGISTERMASTER.REGISTER_NAME = '" & REGNAME & "'"

            'PURCHASE(REGISTERED)
            RowIndex += 1
            Write("PURCHASE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" ISNULL(SUM(PURCHASEMASTER_DESC.BILL_TAXABLEAMT), 0) AS TAXABLEAMT, ISNULL(SUM(PURCHASEMASTER_DESC.BILL_CGSTAMT), 0) AS CGST, ISNULL(SUM(PURCHASEMASTER_DESC.BILL_SGSTAMT), 0) AS SGST, ISNULL(SUM(PURCHASEMASTER_DESC.BILL_IGSTAMT), 0) AS IGST ", "", " PURCHASEMASTER INNER JOIN REGISTERMASTER ON REGISTERMASTER.register_id = PURCHASEMASTER.BILL_REGISTERID INNER JOIN PURCHASEMASTER_DESC ON PURCHASEMASTER.BILL_NO = PURCHASEMASTER_DESC.BILL_NO AND PURCHASEMASTER.BILL_REGISTERID = PURCHASEMASTER_DESC.BILL_REGISTERID AND PURCHASEMASTER.BILL_YEARID = PURCHASEMASTER_DESC.BILL_YEARID ", WHERECLAUSE & " AND PURCHASEMASTER.BILL_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND PURCHASEMASTER.BILL_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND PURCHASEMASTER.BILL_YEARID = " & YEARID & " AND ISNULL(PURCHASEMASTER.BILL_RCM,0) = 'FALSE' ")
            If DT.Rows.Count > 0 Then
                Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            End If


            'NONPURCHASE (REGISTERED)
            RowIndex += 1
            Write("NONPURCHASE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DTNP = OBJCMN.search("  ISNULL(SUM(NP_TOTALTAXABLEAMT),0) AS TAXABLEAMT, ISNULL(SUM(NP_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(NP_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(NP_TOTALIGSTAMT),0) AS IGST ", "", " NONPURCHASE  INNER JOIN REGISTERMASTER ON REGISTER_ID = NP_REGISTERID", WHERECLAUSE & " AND NP_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND NP_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "'  AND NP_YEARID = " & YEARID & " AND ISNULL(NP_RCM,0) = 'FALSE'")
            If DTNP.Rows.Count > 0 Then
                Write(Val(DTNP.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")) + Val(DTNP.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")) + Val(DTNP.Rows(0).Item("SGST")) + Val(DTNP.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            End If



            ''CREDITNOTE
            'RowIndex += 1
            'Write("CREDIT NOTE", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            'DT = OBJCMN.search(" isnull(SUM(T.TAXABLEAMT ),0)AS TAXABLEAMT, isnull(SUM(T.CGST),0) AS CGST, isnull(SUM(T.SGST),0) AS SGST, isnull(SUM(T.IGST),0) AS IGST ", "", "(SELECT SUM(ISNULL(SALERETURN.SALRET_SUBTOTAL, 0)) AS TAXABLEAMT, SUM(ISNULL(SALERETURN.SALRET_TOTALCGSTAMT, 0)) AS CGST, SUM(ISNULL(SALERETURN.SALRET_TOTALSGSTAMT, 0)) AS SGST, SUM(ISNULL(SALERETURN.SALRET_TOTALIGSTAMT, 0)) AS IGST FROM SALERETURN WHERE SALRET_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND SALRET_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND SALRET_YEARID = " & YEARID & " UNION ALL SELECT ISNULL(SUM(CN_SUBTOTAL),0) AS TAXABLEAMT, ISNULL(SUM(CN_CGSTAMT),0) AS CGST, ISNULL(SUM(CN_SGSTAMT),0) AS SGST, ISNULL(SUM(CN_IGSTAMT),0) AS IGST FROM CREDITNOTEMASTER WHERE CN_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND CN_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND CN_YEARID = " & YEARID & " AND ISNULL(CN_RCM,0) = 'FALSE') AS T", "")
            'If DT.Rows.Count > 0 Then
            '    Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            'End If


            'SALE(REGISTERED)
            RowIndex += 1
            Write("SALE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search("ISNULL(SUM(T.TAXABLEAMT ),0) AS TAXABLEAMT, ISNULL(SUM(T.CGST),0) AS CGST, ISNULL(SUM(T.SGST),0) AS SGST, ISNULL(SUM(T.IGST),0) AS IGST", "", " (SELECT  ISNULL(SUM(INVOICE_TOTALTAXABLEAMT),0) AS TAXABLEAMT, ISNULL(SUM(INVOICE_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(INVOICE_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(INVOICE_TOTALIGSTAMT),0) AS IGST FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = ACC_ID  INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID WHERE INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & WHERECLAUSE & " AND ISNULL(ACC_GSTIN,'') <> '') AS T", "")
            If DT.Rows.Count > 0 Then
                Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            End If



            'SALE
            RowIndex += 1
            Write("SALE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search("isnull(SUM(T.TAXABLEAMT ),0)AS TAXABLEAMT, isnull(SUM(T.CGST),0) AS CGST, isnull(SUM(T.SGST),0) AS SGST, isnull(SUM(T.IGST),0) AS IGST", "", " (SELECT ISNULL(SUM(INVOICE_TOTALTAXABLEAMT),0) AS TAXABLEAMT, ISNULL(SUM(INVOICE_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(INVOICE_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(INVOICE_TOTALIGSTAMT),0) AS IGST FROM INVOICEMASTER INNER JOIN LEDGERS ON INVOICE_LEDGERID = ACC_ID  INNER JOIN REGISTERMASTER ON REGISTER_ID = INVOICE_REGISTERID WHERE INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & WHERECLAUSE & " AND ISNULL(ACC_GSTIN,'') = '' ) AS T", "")
            If DT.Rows.Count > 0 Then
                Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            End If



            ''DEBITNOTE
            'RowIndex += 1
            'Write("DEBIT NOTE", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            'DT = OBJCMN.search(" isnull(SUM(T.TAXABLEAMT ),0)AS TAXABLEAMT, isnull(SUM(T.CGST),0) AS CGST, isnull(SUM(T.SGST),0) AS SGST, isnull(SUM(T.IGST),0) AS IGST ", "", "(SELECT SUM(ISNULL(PURCHASERETURN.PR_SUBTOTAL, 0)) AS TAXABLEAMT, SUM(ISNULL(PURCHASERETURN.PR_TOTALCGSTAMT, 0)) AS CGST, SUM(ISNULL(PURCHASERETURN.PR_TOTALSGSTAMT, 0)) AS SGST, SUM(ISNULL(PURCHASERETURN.PR_TOTALIGSTAMT, 0)) AS IGST FROM PURCHASERETURN WHERE PR_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND PR_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND PR_YEARID = " & YEARID & " UNION ALL SELECT ISNULL(SUM(DN_SUBTOTAL),0) AS TAXABLEAMT, ISNULL(SUM(DN_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(DN_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(DN_TOTALIGSTAMT),0) AS IGST FROM DEBITNOTEMASTER WHERE DN_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND DN_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND DN_YEARID = " & YEARID & ") AS T", "")
            'If DT.Rows.Count > 0 Then
            '    Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
            '    Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            'End If



            'CLOSING
            RowIndex += 1
            Write("CLOSING", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("3").ToString & 9 & ":" & objColumn.Item("3").ToString & 10 & ") - SUM(" & objColumn.Item("3").ToString & 11 & ":" & objColumn.Item("3").ToString & 12 & ")", Range("3"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("4").ToString & 9 & ":" & objColumn.Item("4").ToString & 10 & ") - SUM(" & objColumn.Item("4").ToString & 11 & ":" & objColumn.Item("4").ToString & 12 & ")", Range("4"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("5").ToString & 9 & ":" & objColumn.Item("5").ToString & 10 & ") - SUM(" & objColumn.Item("5").ToString & 11 & ":" & objColumn.Item("5").ToString & 12 & ")", Range("5"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("6").ToString & 9 & ":" & objColumn.Item("6").ToString & 10 & ") - SUM(" & objColumn.Item("6").ToString & 11 & ":" & objColumn.Item("6").ToString & 12 & ")", Range("6"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & 8 & ":" & objColumn.Item("7").ToString & 10 & ") - SUM(" & objColumn.Item("7").ToString & 11 & ":" & objColumn.Item("7").ToString & 12 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 12)
            objSheet.Range(Range("1"), Range("7")).Interior.Color = RGB(255, 165, 0)


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)





            'PURCHASE (URD)
            RowIndex += 3
            Write("PURCHASE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" ISNULL(SUM(BILL_TOTALTAXABLEAMT),0) AS TAXABLEAMT, ISNULL(SUM(BILL_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(BILL_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(BILL_TOTALIGSTAMT),0) AS IGST ", "", " PURCHASEMASTER  INNER JOIN REGISTERMASTER ON REGISTER_ID = BILL_REGISTERID", WHERECLAUSE & " AND BILL_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND BILL_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND BILL_YEARID = " & YEARID & " AND ISNULL(BILL_RCM,0) = 'TRUE' ")
            If DT.Rows.Count > 0 Then
                Write(Val(DT.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DT.Rows(0).Item("CGST")) + Val(DT.Rows(0).Item("SGST")) + Val(DT.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)

            End If


            'NONPURCHASE (URD)
            RowIndex += 1
            Write("NONPURCHASE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DTNP = OBJCMN.search(" ISNULL(SUM(NP_TOTALTAXABLEAMT),0) AS TAXABLEAMT, ISNULL(SUM(NP_TOTALCGSTAMT),0) AS CGST, ISNULL(SUM(NP_TOTALSGSTAMT),0) AS SGST, ISNULL(SUM(NP_TOTALIGSTAMT),0) AS IGST ", "", " NONPURCHASE  INNER JOIN REGISTERMASTER ON REGISTER_ID = NP_REGISTERID", WHERECLAUSE & " AND NP_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND NP_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "'AND NP_YEARID = " & YEARID & " AND ISNULL(NP_RCM,0) = 'TRUE'")
            If DTNP.Rows.Count > 0 Then
                Write(Val(DTNP.Rows(0).Item("TAXABLEAMT")), Range("2"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")), Range("3"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("SGST")), Range("4"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")) + Val(DTNP.Rows(0).Item("SGST")), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("IGST")), Range("6"), XlHAlign.xlHAlignRight, , True, 10)
                Write(Val(DTNP.Rows(0).Item("CGST")) + Val(DTNP.Rows(0).Item("SGST")) + Val(DTNP.Rows(0).Item("IGST")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)

            End If

            'RCM CLOSING
            RowIndex += 1
            Write("RCM CLOSING", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("3").ToString & RowIndex - 2 & ":" & objColumn.Item("3").ToString & RowIndex - 1 & ")", Range("3"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("4").ToString & RowIndex - 2 & ":" & objColumn.Item("4").ToString & RowIndex - 1 & ")", Range("4"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("5").ToString & RowIndex - 2 & ":" & objColumn.Item("5").ToString & RowIndex - 1 & ")", Range("5"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("6").ToString & RowIndex - 2 & ":" & objColumn.Item("6").ToString & RowIndex - 1 & ")", Range("6"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - 2 & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 12)
            objSheet.Range(Range("1"), Range("7")).Interior.Color = RGB(0, 255, 0)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)



            'PAYMENT
            RowIndex += 1
            Write("GST PAYMENT", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DTNP = OBJCMN.search(" ISNULL(SUM(PAYMENT_TOTAL),0) AS PAIDAMOUNT ", "", " PAYMENTMASTER INNER JOIN LEDGERS ON PAYMENT_ledgerid = Acc_id ", " AND LEDGERS.ACC_CMPNAME IN ('CGST', 'SGST','IGST', 'REVERSE CHARGE') AND PAYMENT_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND PAYMENT_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND PAYMENT_YEARID = " & YEARID)
            Write(Val(DTNP.Rows(0).Item("PAIDAMOUNT")), Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            objSheet.Range(Range("1"), Range("7")).Interior.Color = RGB(255, 255, 0)



            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)




            objBook.Application.ActiveWindow.Zoom = 100

            'With objSheet.PageSetup
            '    .Orientation = XlPageOrientation.xlPortrait
            '    .TopMargin = 20
            '    .LeftMargin = 10
            '    .RightMargin = 5
            '    .BottomMargin = 10
            '    .Zoom = False
            'End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTSALEDETAILS_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("3"), 25)
            SetColumnWidth(Range("4"), 15)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable
            Dim DTNP As New System.Data.DataTable
            'CMPNAME
            Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

            RowIndex = 2
            Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("14"), True, 14)
            SetBorder(RowIndex, Range("1"), Range("14"))

            'ADD1
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("14"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("14"))

            'ADD2
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("14"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("14"))

            RowIndex += 1
            Write("GST SALE DETAILS (" & Format(FROMDATE, "dd/MM/yyyy") & "-" & Format(TODATE, "dd/MM/yyyy") & ")", Range("1"), XlHAlign.xlHAlignCenter, Range("14"), True, 12)
            SetBorder(RowIndex, Range("1"), Range("14"))


            'FREEZE TOP 7 ROWS
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("14").ToString & 8).Select()
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("14").ToString & 8).Application.ActiveWindow.FreezePanes = True


            SetBorder(RowIndex + 1, objColumn.Item("1").ToString & RowIndex + 1, objColumn.Item("14").ToString & RowIndex + 1)

            RowIndex += 2
            Write("INV NO", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("INV DT", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("NAME", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("GSTIN", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("STATE", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("CITY", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("QTY", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TAXABLE AMT", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("CGST", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("SGST", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TAX C+S", Range("11"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("IGST", Range("12"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("OTHER CHGS", Range("13"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TOTAL", Range("14"), XlHAlign.xlHAlignCenter, , True, 10)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & RowIndex, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & RowIndex, objColumn.Item("13").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("14").ToString & RowIndex, objColumn.Item("14").ToString & RowIndex)


            'SALE + DEBIT NOTE (REGISTERED)
            RowIndex += 1
            Write("SALE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            ' DT = OBJCMN.search(" INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(INVOICEMASTER.INVOICE_TOTALMTRS, 0) AS QTY, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END) AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGST, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGST, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGST, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'LINE GST') = 'LINE GST' THEN (ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) + ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0)) ELSE ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0) END) AS ROUNDOFF, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID ", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")
            DT = OBJCMN.search("INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.Acc_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(STATEMASTER.state_name, '') AS STATE, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(INVOICEMASTER.INVOICE_TOTALQTY, 0) AS QTY, ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGST, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGST, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGST, ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL", "", "   INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATEMASTER.state_id = LEDGERS.Acc_stateid LEFT OUTER JOIN CITYMASTER ON CITYMASTER.city_id = LEDGERS.Acc_cityid", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")

            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("QTY")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("13"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("14"), XlHAlign.xlHAlignRight, , True, 10)
            Next



            'SALE + DEBIT NOTE (B TO C)
            RowIndex += 2
            Write("SALE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            'DT = OBJCMN.search(" INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(INVOICEMASTER.INVOICE_TOTALQTY, 0) AS QTY, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'LINE GST') = 'LINE GST' THEN ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) ELSE ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0) END) AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGST, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGST, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGST, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'LINE GST') = 'LINE GST' THEN (ISNULL(INVOICEMASTER.INVOICE_CHARGES, 0) + ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0)) ELSE ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0) END) AS ROUNDOFF, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id  LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = ''  ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")

            DT = OBJCMN.search("INVOICEMASTER.INVOICE_NO AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.Acc_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.state_remark, '') AS STATECODE, ISNULL(STATEMASTER.state_name, '') AS STATE, ISNULL(CITYMASTER.city_name, '') AS CITY, ISNULL(INVOICEMASTER.INVOICE_TOTALQTY, 0) AS QTY, ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(INVOICEMASTER.INVOICE_TOTALCGSTAMT, 0) AS CGST, ISNULL(INVOICEMASTER.INVOICE_TOTALSGSTAMT, 0) AS SGST, ISNULL(INVOICEMASTER.INVOICE_TOTALIGSTAMT, 0) AS IGST, ISNULL(INVOICEMASTER.INVOICE_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATEMASTER.state_id = LEDGERS.Acc_stateid LEFT OUTER JOIN CITYMASTER ON CITYMASTER.city_id = LEDGERS.Acc_cityid", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = ''  ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("QTY")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("13"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("14"), XlHAlign.xlHAlignRight, , True, 10)
            Next



            'CLOSING
            RowIndex += 1
            Write("CLOSING", Range("1"), XlHAlign.xlHAlignLeft, Range("4"), True, 10)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & 9 & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("8").ToString & 9 & ":" & objColumn.Item("8").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("9").ToString & 9 & ":" & objColumn.Item("9").ToString & RowIndex - 1 & ")", Range("9"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("10").ToString & 9 & ":" & objColumn.Item("10").ToString & RowIndex - 1 & ")", Range("10"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("11").ToString & 9 & ":" & objColumn.Item("11").ToString & RowIndex - 1 & ")", Range("11"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("12").ToString & 9 & ":" & objColumn.Item("12").ToString & RowIndex - 1 & ")", Range("12"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("13").ToString & 9 & ":" & objColumn.Item("13").ToString & RowIndex - 1 & ")", Range("13"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("14").ToString & 9 & ":" & objColumn.Item("14").ToString & RowIndex - 1 & ")", Range("14"), XlHAlign.xlHAlignRight, , True, 12)


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & RowIndex, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & RowIndex, objColumn.Item("13").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("14").ToString & RowIndex, objColumn.Item("14").ToString & RowIndex)


            SetBorder(RowIndex, objColumn.Item("1").ToString & 8, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 8, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 8, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 8, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 8, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 8, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 8, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 8, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & 8, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & 8, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & 8, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & 8, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & 8, objColumn.Item("13").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("14").ToString & 8, objColumn.Item("14").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTPURCHASEDETAILS_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("3"), 25)
            SetColumnWidth(Range("4"), 15)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable
            Dim DTNP As New System.Data.DataTable
            'CMPNAME
            Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

            RowIndex = 2
            Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("13"), True, 14)
            SetBorder(RowIndex, Range("1"), Range("13"))

            'ADD1
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("13"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("13"))

            'ADD2
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("13"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("13"))

            RowIndex += 1
            Write("GST PURCHASE DETAILS (" & Format(FROMDATE, "dd/MM/yyyy") & "-" & Format(TODATE, "dd/MM/yyyy") & ")", Range("1"), XlHAlign.xlHAlignCenter, Range("13"), True, 12)
            SetBorder(RowIndex, Range("1"), Range("13"))


            'FREEZE TOP 7 ROWS
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("13").ToString & 8).Select()
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("13").ToString & 8).Application.ActiveWindow.FreezePanes = True


            SetBorder(RowIndex + 1, objColumn.Item("1").ToString & RowIndex + 1, objColumn.Item("13").ToString & RowIndex + 1)

            RowIndex += 2
            Write("BILL NO", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("BILL DT", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("NAME", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("GSTIN", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("STATE", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("CITY", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TAXABLE AMT", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("CGST", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("SGST", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TAX C+S", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("IGST", Range("11"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("OTHER CHGS", Range("12"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("TOTAL", Range("13"), XlHAlign.xlHAlignCenter, , True, 10)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & RowIndex, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & RowIndex, objColumn.Item("13").ToString & RowIndex)


            'PURCHASE (REGISTERED)
            RowIndex += 1
            Write("PURCHASE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" PURCHASEMASTER.BILL_PARTYBILLNO AS INVNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(PURCHASEMASTER.BILL_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(PURCHASEMASTER.BILL_TOTALCGSTAMT, 0) AS CGST, ISNULL(PURCHASEMASTER.BILL_TOTALSGSTAMT, 0) AS SGST, ISNULL(PURCHASEMASTER.BILL_TOTALIGSTAMT, 0) AS IGST, ISNULL(PURCHASEMASTER.BILL_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(PURCHASEMASTER.BILL_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID ", " AND BILL_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND BILL_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND BILL_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' ORDER BY PURCHASEMASTER.BILL_PARTYBILLDATE, PURCHASEMASTER.BILL_PARTYBILLNO ")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("13"), XlHAlign.xlHAlignRight, , True, 10)
            Next


            'NONPURCHASE (REGISTERED)
            RowIndex += 2
            Write("NONPURCHASE (REGISTERED)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" NONPURCHASE.NP_REFNO AS INVNO, NONPURCHASE.NP_PARTYBILLDATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(NONPURCHASE.NP_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(NONPURCHASE.NP_TOTALCGSTAMT, 0) AS CGST, ISNULL(NONPURCHASE.NP_TOTALSGSTAMT, 0) AS SGST, ISNULL(NONPURCHASE.NP_TOTALIGSTAMT, 0) AS IGST, ISNULL(NONPURCHASE.NP_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(NONPURCHASE.NP_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID ", " AND NP_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND NP_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND NP_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' ORDER BY NONPURCHASE.NP_PARTYBILLDATE, NONPURCHASE.NP_REFNO ")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("13"), XlHAlign.xlHAlignRight, , True, 10)
            Next



            'PURCHASE (URD)
            RowIndex += 2
            Write("PURCHASE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" PURCHASEMASTER.BILL_PARTYBILLNO AS INVNO, PURCHASEMASTER.BILL_PARTYBILLDATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(PURCHASEMASTER.BILL_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(PURCHASEMASTER.BILL_TOTALCGSTAMT, 0) AS CGST, ISNULL(PURCHASEMASTER.BILL_TOTALSGSTAMT, 0) AS SGST, ISNULL(PURCHASEMASTER.BILL_TOTALIGSTAMT, 0) AS IGST, ISNULL(PURCHASEMASTER.BILL_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(PURCHASEMASTER.BILL_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " PURCHASEMASTER INNER JOIN LEDGERS ON PURCHASEMASTER.BILL_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID ", " AND BILL_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND BILL_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND BILL_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = '' ORDER BY PURCHASEMASTER.BILL_PARTYBILLDATE, PURCHASEMASTER.BILL_PARTYBILLNO ")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("13"), XlHAlign.xlHAlignRight, , True, 10)
            Next


            'NONPURCHASE (URD)
            RowIndex += 2
            Write("NONPURCHASE (URD)", Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
            DT = OBJCMN.search(" NONPURCHASE.NP_REFNO AS INVNO, NONPURCHASE.NP_PARTYBILLDATE AS DATE, ISNULL(LEDGERS.Acc_cmpname, '') AS NAME, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(CITYMASTER.CITY_NAME, '') AS CITY, ISNULL(NONPURCHASE.NP_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, ISNULL(NONPURCHASE.NP_TOTALCGSTAMT, 0) AS CGST, ISNULL(NONPURCHASE.NP_TOTALSGSTAMT, 0) AS SGST, ISNULL(NONPURCHASE.NP_TOTALIGSTAMT, 0) AS IGST, ISNULL(NONPURCHASE.NP_ROUNDOFF, 0) AS ROUNDOFF, ISNULL(NONPURCHASE.NP_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " NONPURCHASE INNER JOIN LEDGERS ON NONPURCHASE.NP_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID LEFT OUTER JOIN CITYMASTER ON CITY_ID = LEDGERS.ACC_CITYID ", " AND NP_PARTYBILLDATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND NP_PARTYBILLDATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND NP_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = '' ORDER BY NONPURCHASE.NP_PARTYBILLDATE, NONPURCHASE.NP_REFNO ")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GSTIN"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("CITY"), Range("6"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGST")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGST")) + Val(DTROW("SGST")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGST")), Range("11"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("ROUNDOFF")), Range("12"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("GRANDTOTAL")), Range("13"), XlHAlign.xlHAlignRight, , True, 10)
            Next



            'CLOSING
            RowIndex += 1
            Write("CLOSING", Range("1"), XlHAlign.xlHAlignLeft, Range("4"), True, 10)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & 9 & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("8").ToString & 9 & ":" & objColumn.Item("8").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("9").ToString & 9 & ":" & objColumn.Item("9").ToString & RowIndex - 1 & ")", Range("9"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("10").ToString & 9 & ":" & objColumn.Item("10").ToString & RowIndex - 1 & ")", Range("10"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("11").ToString & 9 & ":" & objColumn.Item("11").ToString & RowIndex - 1 & ")", Range("11"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("12").ToString & 9 & ":" & objColumn.Item("12").ToString & RowIndex - 1 & ")", Range("12"), XlHAlign.xlHAlignRight, , True, 12)
            FORMULA("=SUM(" & objColumn.Item("13").ToString & 9 & ":" & objColumn.Item("13").ToString & RowIndex - 1 & ")", Range("13"), XlHAlign.xlHAlignRight, , True, 12)


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & RowIndex, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & RowIndex, objColumn.Item("13").ToString & RowIndex)


            SetBorder(RowIndex, objColumn.Item("1").ToString & 8, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 8, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 8, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 8, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 8, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 8, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 8, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 8, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & 8, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & 8, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & 8, objColumn.Item("11").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("12").ToString & 8, objColumn.Item("12").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("13").ToString & 8, objColumn.Item("13").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTB2B_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("1"), 25)
            SetColumnWidth(Range("2"), 17)
            SetColumnWidth(Range("4"), 17)
            SetColumnWidth(Range("5"), 17)
            SetColumnWidth(Range("8"), 17)
            SetColumnWidth(Range("9"), 10)
            SetColumnWidth(Range("10"), 17)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable

            Write("Summary For B2B (4)", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            objSheet.Range(Range("1"), Range("1")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("1")).Interior.Color = RGB(0, 128, 255)

            RowIndex += 1
            Write("No Of Receipients", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("No Of Invoices", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Invoice Value", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Taxable Value", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Cess", Range("11"), XlHAlign.xlHAlignCenter, , True, 10)
            objSheet.Range(Range("1"), Range("11")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("11")).Interior.Color = RGB(0, 128, 255)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)


            RowIndex += 1
            FORMULA("=SUMPRODUCT((" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "<>"""")/COUNTIF(" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "," & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "&""""))", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
            FORMULA("=SUMPRODUCT((" & objColumn.Item("2").ToString & 5 & ":" & objColumn.Item("2").ToString & 40000 & "<>"""")/COUNTIF(" & objColumn.Item("2").ToString & 5 & ":" & objColumn.Item("2").ToString & 40000 & "," & objColumn.Item("2").ToString & 5 & ":" & objColumn.Item("2").ToString & 40000 & "&""""))", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("4").ToString & 5 & ":" & objColumn.Item("4").ToString & 40000 & ")", Range("4"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("10").ToString & 5 & ":" & objColumn.Item("10").ToString & 40000 & ")", Range("10"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("11").ToString & 5 & ":" & objColumn.Item("11").ToString & 40000 & ")", Range("11"), XlHAlign.xlHAlignRight, , True, 10)

            objSheet.Range(objColumn.Item("4").ToString & 3, objColumn.Item("4").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("10").ToString & 3, objColumn.Item("10").ToString & 3).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)



            RowIndex += 1
            Write("GSTIN/UIN Of Receipients", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("Invoice No", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Invoice Date", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Invoice Value", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Place Of Supply", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Reverse Charge", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Invoice Type", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("E-Commerce GSTIN", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Rate", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Taxable Value", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Cess Amount", Range("11"), XlHAlign.xlHAlignCenter, , True, 10)

            objSheet.Range(Range("1"), Range("11")).Interior.Color = RGB(250, 240, 230)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & RowIndex, objColumn.Item("11").ToString & RowIndex)



            'SALE(REGISTERED)
            'OLD CODE WHERE WE WERE NOT FETCHING GRIDTOTAL, MEANS IF THERE ARE MULTIPLE RATES (HSN) IN SINGLE INVOICE THEN THIS CODE WONT SHOW MULTIPLE
            'DT = OBJCMN.search(" INVOICEMASTER.INVOICE_INITIALS AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE, ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, 0 AS GSTPER, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID ", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")
            DT = OBJCMN.search("INVOICEMASTER.INVOICE_INITIALS AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.Acc_GSTIN, '') AS GSTIN, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR(5)), '') AS STATECODE, ISNULL(STATEMASTER.state_name, '') AS STATE, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT, 0)) AS TAXABLEAMT, ISNULL(HSNMASTER.HSN_IGST, 0) AS GSTPER, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL, 0)) AS GRANDTOTAL", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id INNER JOIN INVOICEMASTER_DESC ON INVOICEMASTER.INVOICE_NO = INVOICEMASTER_DESC.INVOICE_NO AND INVOICEMASTER.INVOICE_REGISTERID = INVOICEMASTER_DESC.INVOICE_REGISTERID AND INVOICEMASTER.INVOICE_YEARID = INVOICEMASTER_DESC.INVOICE_YEARID INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID LEFT OUTER JOIN STATEMASTER ON STATEMASTER.state_id = LEDGERS.Acc_stateid ", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') <> '' GROUP BY INVOICEMASTER.INVOICE_INITIALS, INVOICEMASTER.INVOICE_DATE, ISNULL(LEDGERS.Acc_GSTIN, ''), ISNULL(STATEMASTER.state_name, ''), ISNULL(HSNMASTER.HSN_IGST, 0),  INVOICEMASTER.INVOICE_NO, ISNULL(CAST(STATEMASTER.state_remark AS VARCHAR(5)), '') ORDER BY DATE, INVOICEMASTER.INVOICE_NO")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("GSTIN"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("INVNO"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GRANDTOTAL"), Range("4"), XlHAlign.xlHAlignRight, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("5"), XlHAlign.xlHAlignLeft, , False, 10)
                Write("N", Range("6"), XlHAlign.xlHAlignCenter, , False, 10)
                Write("REGULAR", Range("7"), XlHAlign.xlHAlignCenter, , False, 10)
                Write("", Range("8"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("GSTPER")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)

                'GET GSTPER FROM 1ST RECORD OF INVOICEDESC AND FETCH FROM HSNCODE
                'Dim OBJGST As System.Data.DataTable = OBJCMN.search("HSN_IGST AS GSTPER", "", " INVOICEMASTER_DESC INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID ", " AND INVOICE_INITIALS = '" & DTROW("INVNO") & "' AND INVOICE_YEARID = " & YEARID)
                'If OBJGST.Rows.Count > 0 Then Write(Val(OBJGST.Rows(0).Item("GSTPER")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("10"), XlHAlign.xlHAlignRight, , False, 10)
                Write("0", Range("11"), XlHAlign.xlHAlignRight, , False, 10)
            Next

            objSheet.Range(objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("9").ToString & 5, objColumn.Item("9").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("10").ToString & 5, objColumn.Item("10").ToString & RowIndex).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & 5, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 5, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 5, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 5, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & 5, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & 5, objColumn.Item("10").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("11").ToString & 5, objColumn.Item("11").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTB2CL_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("1"), 25)
            SetColumnWidth(Range("3"), 17)
            SetColumnWidth(Range("4"), 17)
            SetColumnWidth(Range("6"), 17)
            SetColumnWidth(Range("8"), 17)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable

            Write("Summary For B2CL (5)", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            objSheet.Range(Range("1"), Range("1")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("1")).Interior.Color = RGB(0, 128, 255)

            RowIndex += 1
            Write("No Of Invoices", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("Total Invoice Value", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Taxable Value", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Cess", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            objSheet.Range(Range("1"), Range("8")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("8")).Interior.Color = RGB(0, 128, 255)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)


            RowIndex += 1
            FORMULA("=SUMPRODUCT((" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "<>"""")/COUNTIF(" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "," & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "&""""))", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
            FORMULA("=SUMPRODUCT(1/COUNTIF(" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "," & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "&"""")," & objColumn.Item("3").ToString & 5 & ":" & objColumn.Item("3").ToString & 40000 & ")", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("6").ToString & 5 & ":" & objColumn.Item("6").ToString & 40000 & ")", Range("6"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & 5 & ":" & objColumn.Item("7").ToString & 40000 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 10)

            objSheet.Range(objColumn.Item("3").ToString & 3, objColumn.Item("3").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("6").ToString & 3, objColumn.Item("6").ToString & 3).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)



            RowIndex += 1
            Write("Invoice No", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Invoice Date", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Invoice Value", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Place Of Supply", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Rate", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Taxable Value", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Cess Amount", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("E-Commerce GSTIN", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)

            objSheet.Range(Range("1"), Range("8")).Interior.Color = RGB(250, 240, 230)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)



            'SALE(URD)
            DT = OBJCMN.search(" INVOICEMASTER.INVOICE_INITIALS AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE,ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, 0 AS GSTPER, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID ", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = '' AND ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) > 250000 ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("INVNO"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("DATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("GRANDTOTAL"), Range("3"), XlHAlign.xlHAlignRight, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("4"), XlHAlign.xlHAlignLeft, , False, 10)

                'GET GSTPER FROM 1ST RECORD OF INVOICEDESC AND FETCH FROM HSNCODE
                Dim OBJGST As System.Data.DataTable = OBJCMN.search("HSN_IGST AS GSTPER", "", " INVOICEMASTER_DESC INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID ", " AND INVOICE_INITIALS = '" & DTROW("INVNO") & "' AND INVOICE_YEARID = " & YEARID)
                If OBJGST.Rows.Count > 0 Then Write(Val(OBJGST.Rows(0).Item("GSTPER")), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                Write("0", Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write("", Range("8"), XlHAlign.xlHAlignRight, , False, 10)
            Next

            objSheet.Range(objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & 5, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 5, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 5, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 5, objColumn.Item("8").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTB2CS_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            SetColumnWidth(Range("1"), 25)
            SetColumnWidth(Range("2"), 17)
            SetColumnWidth(Range("4"), 17)
            SetColumnWidth(Range("6"), 17)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable

            Write("Summary For B2CS (7)", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            objSheet.Range(Range("1"), Range("1")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("1")).Interior.Color = RGB(0, 128, 255)

            RowIndex += 1
            Write("Total Taxable Value", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Cess", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            objSheet.Range(Range("1"), Range("6")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("6")).Interior.Color = RGB(0, 128, 255)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)


            RowIndex += 1
            FORMULA("=SUM(" & objColumn.Item("4").ToString & 5 & ":" & objColumn.Item("4").ToString & 40000 & ")", Range("4"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("5").ToString & 5 & ":" & objColumn.Item("5").ToString & 40000 & ")", Range("5"), XlHAlign.xlHAlignRight, , True, 10)

            objSheet.Range(objColumn.Item("4").ToString & 3, objColumn.Item("4").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("5").ToString & 3, objColumn.Item("5").ToString & 3).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)



            RowIndex += 1
            Write("Type", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Place Of Supply", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Rate", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Taxable Value", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Cess Amount", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("E-Commerce GSTIN", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)

            objSheet.Range(Range("1"), Range("8")).Interior.Color = RGB(250, 240, 230)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)



            'SALE(URD)<250000
            DT = OBJCMN.search(" INVOICEMASTER.INVOICE_INITIALS AS INVNO, INVOICEMASTER.INVOICE_DATE AS DATE, ISNULL(LEDGERS.ACC_GSTIN, '') AS GSTIN, ISNULL(STATEMASTER.STATE_REMARK, '') AS STATECODE, ISNULL(STATEMASTER.STATE_NAME, '') AS STATE,ISNULL(INVOICEMASTER.INVOICE_TOTALTAXABLEAMT, 0) AS TAXABLEAMT, 0 AS GSTPER, ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) AS GRANDTOTAL  ", "", " INVOICEMASTER INNER JOIN LEDGERS ON INVOICEMASTER.INVOICE_LEDGERID = LEDGERS.Acc_id LEFT OUTER JOIN STATEMASTER ON STATE_ID = LEDGERS.ACC_STATEID ", " AND INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICE_YEARID = " & YEARID & " AND ISNULL(ACC_GSTIN,'') = '' AND ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0) <= 250000 ORDER BY INVOICEMASTER.INVOICE_DATE, INVOICEMASTER.INVOICE_NO")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write("OE", Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("STATECODE") & "-" & DTROW("STATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)

                'GET GSTPER FROM 1ST RECORD OF INVOICEDESC AND FETCH FROM HSNCODE
                Dim OBJGST As System.Data.DataTable = OBJCMN.search("HSN_IGST AS GSTPER", "", " INVOICEMASTER_DESC INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID ", " AND INVOICE_INITIALS = '" & DTROW("INVNO") & "' AND INVOICE_YEARID = " & YEARID)
                If OBJGST.Rows.Count > 0 Then Write(Val(OBJGST.Rows(0).Item("GSTPER")), Range("3"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("4"), XlHAlign.xlHAlignRight, , False, 10)
                Write("0", Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                Write("", Range("6"), XlHAlign.xlHAlignRight, , False, 10)
            Next

            objSheet.Range(objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & 5, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 5, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function GSTHSN_EXCEL(ByVal CMPID As Integer, ByVal YEARID As Integer, ByVal FROMDATE As Date, ByVal TODATE As Date, ByVal INVOICESCREENTYPE As String) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 17)
            Next

            SetColumnWidth(Range("1"), 25)
            SetColumnWidth(Range("2"), 25)
            SetColumnWidth(Range("7"), 20)


            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            Dim DT As New System.Data.DataTable

            Write("Summary For HSN (12)", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            objSheet.Range(Range("1"), Range("1")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("1")).Interior.Color = RGB(0, 128, 255)

            RowIndex += 1
            Write("No Of HSN", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("Total Value", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Taxable Value", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Integrated Tax", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Central Tax", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total State/UT Tax", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Cess", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)
            objSheet.Range(Range("1"), Range("10")).Font.Color = RGB(255, 255, 255)
            objSheet.Range(Range("1"), Range("10")).Interior.Color = RGB(0, 128, 255)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)


            RowIndex += 1
            FORMULA("=SUMPRODUCT((" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "<>"""")/COUNTIF(" & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "," & objColumn.Item("1").ToString & 5 & ":" & objColumn.Item("1").ToString & 40000 & "&""""))", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("5").ToString & 5 & ":" & objColumn.Item("5").ToString & 40000 & ")", Range("5"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("6").ToString & 5 & ":" & objColumn.Item("6").ToString & 40000 & ")", Range("6"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("7").ToString & 5 & ":" & objColumn.Item("7").ToString & 40000 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("8").ToString & 5 & ":" & objColumn.Item("8").ToString & 40000 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("9").ToString & 5 & ":" & objColumn.Item("9").ToString & 40000 & ")", Range("9"), XlHAlign.xlHAlignRight, , True, 10)
            FORMULA("=SUM(" & objColumn.Item("10").ToString & 5 & ":" & objColumn.Item("10").ToString & 40000 & ")", Range("10"), XlHAlign.xlHAlignRight, , True, 10)

            objSheet.Range(objColumn.Item("5").ToString & 3, objColumn.Item("5").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("6").ToString & 3, objColumn.Item("6").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("7").ToString & 3, objColumn.Item("7").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("8").ToString & 3, objColumn.Item("8").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("9").ToString & 3, objColumn.Item("9").ToString & 3).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("10").ToString & 3, objColumn.Item("10").ToString & 3).NumberFormat = "0.00"


            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)



            RowIndex += 1
            Write("HSN", Range("1"), XlHAlign.xlHAlignCenter, Range("1"), True, 10)
            Write("Description", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("UQC", Range("3"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Qty", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Total Value", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Taxable Value", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Integrated Tax Amount", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Central Tax Amount", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("State/UT Tax Amount", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Cess Amount", Range("10"), XlHAlign.xlHAlignCenter, , True, 10)

            objSheet.Range(Range("1"), Range("11")).Interior.Color = RGB(250, 240, 230)
            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & RowIndex, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & RowIndex, objColumn.Item("10").ToString & RowIndex)



            'SALE(REGISTERED)
            DT = OBJCMN.search(" ISNULL(HSNMASTER.HSN_CODE, '') AS HSNCODE, ISNULL(HSNMASTER.HSN_ITEMDESC, '') AS HSNDESC, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_QTY, 0)) AS QTY, ROUND(SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_GRIDTOTAL, 0)),0) AS GRANDTOTAL, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT, 0)) AS TAXABLEAMT, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_IGSTAMT, 0)) AS IGSTAMT, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_CGSTAMT, 0)) AS CGSTAMT, SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_SGSTAMT, 0)) AS SGSTAMT  ", "", " INVOICEMASTER_DESC INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID  ", " AND INVOICEMASTER.INVOICE_DATE >= '" & Format(FROMDATE.Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_DATE <= '" & Format(TODATE.Date, "MM/dd/yyyy") & "' AND INVOICEMASTER.INVOICE_YEARID = " & YEARID & " GROUP BY ISNULL(HSNMASTER.HSN_CODE, ''), ISNULL(HSNMASTER.HSN_ITEMDESC, '')")
            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("HSNCODE"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(DTROW("HSNDESC"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                Write("", Range("3"), XlHAlign.xlHAlignLeft, , False, 10)
                Write(Val(DTROW("QTY")), Range("4"), XlHAlign.xlHAlignRight, , False, 10)


                'Dim DTINV As New System.Data.DataTable
                'WE CAN NOT GET GRAND TOTAL IN ABOVE QUERY COZ THIS WIL CALC GRANDTOTAL MULTIPLE TIMES COZ WE HAVE JOIN WITH INVOICEDESC
                'BELOW CODE WASS TAKING TIME 
                'If INVOICESCREENTYPE = "LINE GST" Then
                'Else
                '    DTINV = OBJCMN.search("DISTINCT INVOICEMASTER.INVOICE_NO  ROUND(SUM(ISNULL(INVOICEMASTER.INVOICE_GRANDTOTAL, 0)),0) AS GRANDTOTAL, (CASE WHEN ISNULL(INVOICEMASTER.INVOICE_SCREENTYPE,'LINE GST') = 'LINE GST' THEN SUM(ISNULL(INVOICEMASTER_DESC.INVOICE_TAXABLEAMT, 0)) ELSE SUM(ISNULL(INVOICEMASTER.INVOICE_SUBTOTAL, 0)) END) AS TAXABLEAMT,", "", " INVOICEMASTER_DESC INNER JOIN HSNMASTER ON INVOICEMASTER_DESC.INVOICE_HSNCODEID = HSNMASTER.HSN_ID INNER JOIN INVOICEMASTER ON INVOICEMASTER_DESC.INVOICE_NO = INVOICEMASTER.INVOICE_NO AND INVOICEMASTER_DESC.INVOICE_REGISTERID = INVOICEMASTER.INVOICE_REGISTERID AND INVOICEMASTER_DESC.INVOICE_YEARID = INVOICEMASTER.INVOICE_YEARID ", " AND INVOICEMASTER.INVOICE_YEARID = " & YEARID & " AND HSN_CODE = '" & DTROW("HSNCODE") & "'")
                'End If

                Write(Val(DTROW("GRANDTOTAL")), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("TAXABLEAMT")), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("IGSTAMT")), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("CGSTAMT")), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Val(DTROW("SGSTAMT")), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                Write("0", Range("10"), XlHAlign.xlHAlignRight, , False, 10)
            Next

            objSheet.Range(objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("7").ToString & 5, objColumn.Item("7").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("8").ToString & 5, objColumn.Item("8").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("9").ToString & 5, objColumn.Item("9").ToString & RowIndex).NumberFormat = "0.00"
            objSheet.Range(objColumn.Item("10").ToString & 5, objColumn.Item("10").ToString & RowIndex).NumberFormat = "0.00"



            SetBorder(RowIndex, objColumn.Item("1").ToString & 5, objColumn.Item("1").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("2").ToString & 5, objColumn.Item("2").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("3").ToString & 5, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 5, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 5, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 5, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 5, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 5, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & 5, objColumn.Item("9").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("10").ToString & 5, objColumn.Item("10").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 100

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlLandscape
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

#End Region

#Region "BANKRECO"

    Public Function BANKRECO(ByVal NAME As String, ByVal FROMDATE As Date, ByVal TODATE As Date, ByVal CMPID As Integer, ByVal LOCATIONID As Integer, ByVal YEARID As Integer) As Object
        Try
            Dim OBJRECO As New ClsBankReco
            Dim OBJCMN As New ClsCommon
            Dim ALPARAVAL As New ArrayList
            Dim I As Integer = 0

            ALPARAVAL.Add(NAME)
            ALPARAVAL.Add(FROMDATE)
            ALPARAVAL.Add(TODATE)
            ALPARAVAL.Add(CMPID)
            ALPARAVAL.Add(LOCATIONID)
            ALPARAVAL.Add(YEARID)

            OBJRECO.alParaval = ALPARAVAL
            Dim DT As System.Data.DataTable = OBJRECO.GETDATA()
            Dim DTTOTAL As System.Data.DataTable = OBJRECO.GETTOTAL
            If DT.Rows.Count > 0 Then

                SetWorkSheet()
                For I = 1 To 26
                    SetColumn(I, Chr(64 + I))
                Next


                RowIndex = 1
                For I = 1 To 26
                    SetColumnWidth(Range(I), 14)
                Next


                ' **************************** CMPHEADER *************************
                Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

                RowIndex = 2
                Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 14)
                SetBorder(RowIndex, Range("1"), Range("8"))

                'ADD1
                RowIndex += 1
                Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))

                'ADD2
                RowIndex += 1
                Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))




                'FREEZE TOP 7 ROWS
                objSheet.Range(objColumn.Item("1").ToString & 10, objColumn.Item("8").ToString & 10).Select()
                objSheet.Range(objColumn.Item("1").ToString & 10, objColumn.Item("8").ToString & 10).Application.ActiveWindow.FreezePanes = True

                ' **************************** CMPHEADER *************************




                RowIndex += 2
                Write("Name : " & NAME, Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
                Write("Run Date : " & Now.Date, Range("6"), XlHAlign.xlHAlignLeft, Range("8"), False, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))


                RowIndex += 1
                Write("Bank Reconciliation Statement from " & FROMDATE & " to " & TODATE, Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))


                'HEADERS
                RowIndex += 2
                Write("Booking No", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Date", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Name", Range("3"), XlHAlign.xlHAlignCenter, Range("4"), True, 10)
                Write("Chq No", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Reco Date", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Amount", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Balance", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
                SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)



                RowIndex += 2
                Write("Bank Balance as per Ledger Book", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                Write(Format(Val(DTTOTAL.Rows(0).Item("BOOKBAL")), "0.00") & "  " & DTTOTAL.Rows(0).Item("BOOKDRCR"), Range("8"), XlHAlign.xlHAlignRight, , True, 12)


                'AS PER JEETU
                'GET ALL DEBIT AMOUNT RECOED
                'I = 0
                'RowIndex += 1
                'Write("Chques Deposited and Cleared :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                'RowIndex += 1
                'For Each DTROW As DataRow In DT.Rows
                '    If Val(DTROW("DR")) <> 0 And IsDBNull(DTROW("RECODATE")) = False Then
                '        I += 1
                '        RowIndex += 1
                '        Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                '        Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                '        Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                '        Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                '        Write(DTROW("RECODATE"), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                '        Write(DTROW("DR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                '    End If
                'Next

                'RowIndex += 1
                'Write("Total :", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
                'FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - I & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                'SetBorder(RowIndex, Range("1"), Range("8"))



                'AS PER JEETU
                'GET ALL CREDIT AMOUNT
                'I = 0
                'RowIndex += 2
                'Write("Chques Issused and Presented :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                'RowIndex += 1
                'For Each DTROW As DataRow In DT.Rows
                '    If Val(DTROW("CR")) <> 0 And IsDBNull(DTROW("RECODATE")) = False Then
                '        I += 1
                '        RowIndex += 1
                '        Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                '        Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                '        Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                '        Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                '        Write(DTROW("RECODATE"), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                '        Write(DTROW("CR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                '    End If
                'Next


                'RowIndex += 1
                'Write("Total :", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
                'FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - I & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                'SetBorder(RowIndex, Range("1"), Range("8"))




                'GET ALL DEBIT AMOUNT
                I = 0
                RowIndex += 1
                Write("Chques Deposited but not Cleared :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                RowIndex += 1
                For Each DTROW As DataRow In DT.Rows

                    If Val(DTROW("DR")) <> 0 And IsDBNull(DTROW("RECODATE")) = True Then
                        I += 1
                        RowIndex += 1
                        Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                        Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                        Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                        Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                        Write(DTROW("DR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                    ElseIf Val(DTROW("DR")) <> 0 And IsDBNull(DTROW("RECODATE")) = False Then
                        If Convert.ToDateTime(DTROW("RECODATE")).Date > TODATE.Date Then
                            I += 1
                            RowIndex += 1
                            Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                            Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                            Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                            Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                            Write(DTROW("DR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                        End If
                    End If
                Next


                RowIndex += 1
                Write("Total :", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
                FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - I & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 12)
                SetBorder(RowIndex, Range("1"), Range("8"))



                'GET ALL CREDIT AMOUNT
                I = 0
                RowIndex += 2
                Write("Chques Issused but not Presented :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                RowIndex += 1
                For Each DTROW As DataRow In DT.Rows
                    If Val(DTROW("CR")) <> 0 And IsDBNull(DTROW("RECODATE")) = True Then
                        I += 1
                        RowIndex += 1
                        Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                        Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                        Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                        Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                        Write(DTROW("CR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                    ElseIf Val(DTROW("CR")) <> 0 And IsDBNull(DTROW("RECODATE")) = False Then
                        If Convert.ToDateTime(DTROW("RECODATE")).Date > TODATE.Date Then
                            I += 1
                            RowIndex += 1
                            Write(DTROW("BILLINITIALS"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                            Write(DTROW("BILLDATE"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                            Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                            Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                            Write(DTROW("CR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                        End If
                    End If
                Next


                RowIndex += 1
                Write("Total :", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
                FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - I & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 12)
                SetBorder(RowIndex, Range("1"), Range("8"))


                RowIndex += 2
                Write("Balance as Per Bank Book :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                Write(Format(Val(DTTOTAL.Rows(0).Item("BANKBAL")), "0.00") & "  " & DTTOTAL.Rows(0).Item("BANKDRCR"), Range("8"), XlHAlign.xlHAlignRight, , True, 12)


                SetBorder(RowIndex, objColumn.Item("1").ToString & 9, objColumn.Item("1").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("2").ToString & 9, objColumn.Item("2").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("3").ToString & 9, objColumn.Item("4").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("5").ToString & 9, objColumn.Item("5").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("6").ToString & 9, objColumn.Item("6").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("7").ToString & 9, objColumn.Item("7").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("8").ToString & 9, objColumn.Item("8").ToString & RowIndex)



                objBook.Application.ActiveWindow.Zoom = 95

                With objSheet.PageSetup
                    .Orientation = XlPageOrientation.xlPortrait
                    .TopMargin = 144
                    .LeftMargin = 50.4
                    .RightMargin = 0
                    .BottomMargin = 0
                    .Zoom = False
                    .FitToPagesTall = 1
                    .FitToPagesWide = 1
                End With

                SaveAndClose()

            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Function BANKSTATEMENT(ByVal NAME As String, ByVal FROMDATE As Date, ByVal TODATE As Date, ByVal CMPID As Integer, ByVal LOCATIONID As Integer, ByVal YEARID As Integer) As Object
        Try
            Dim OBJRECO As New ClsBankReco
            Dim OBJCMN As New ClsCommon
            Dim ALPARAVAL As New ArrayList
            Dim I As Integer = 0
            Dim BALANCE As Double = 0

            Dim DT As System.Data.DataTable = OBJCMN.search("DISTINCT RecoDate AS RECODATE, acc_initials AS BILLINITIALS, AGAINST AS NAME, ChqNo AS CHQNO, dr AS DR, cr AS CR", "", " BANKRECO", " AND BANKRECO.NAME = '" & NAME & "' AND ACC_CMPID = " & CMPID & " AND ACC_LOCATIONID = " & LOCATIONID & " AND YEARID = " & YEARID & " AND CAST(RECODATE AS DATE) >= '" & Format(FROMDATE, "MM/dd/yyyy") & "' AND CAST(RECODATE AS DATE) <= '" & Format(TODATE, "MM/dd/yyyy") & "'  ORDER BY RECODATE")


            ALPARAVAL.Add(NAME)
            ALPARAVAL.Add(FROMDATE)
            ALPARAVAL.Add(TODATE)
            ALPARAVAL.Add(CMPID)
            ALPARAVAL.Add(LOCATIONID)
            ALPARAVAL.Add(YEARID)

            OBJRECO.alParaval = ALPARAVAL
            Dim DTTOTAL As System.Data.DataTable = OBJRECO.GETTOTAL
            If DT.Rows.Count > 0 Then

                SetWorkSheet()
                For I = 1 To 26
                    SetColumn(I, Chr(64 + I))
                Next


                RowIndex = 1
                For I = 1 To 26
                    SetColumnWidth(Range(I), 14)
                Next


                ' **************************** CMPHEADER *************************
                Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

                RowIndex = 2
                Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 14)
                SetBorder(RowIndex, Range("1"), Range("8"))

                'ADD1
                RowIndex += 1
                Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))

                'ADD2
                RowIndex += 1
                Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("8"), True, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))




                'FREEZE TOP 7 ROWS
                objSheet.Range(objColumn.Item("1").ToString & 10, objColumn.Item("8").ToString & 10).Select()
                objSheet.Range(objColumn.Item("1").ToString & 10, objColumn.Item("8").ToString & 10).Application.ActiveWindow.FreezePanes = True

                ' **************************** CMPHEADER *************************




                RowIndex += 2
                Write("Name : " & NAME, Range("1"), XlHAlign.xlHAlignLeft, , True, 10)
                Write("Run Date : " & Now.Date, Range("6"), XlHAlign.xlHAlignLeft, Range("8"), False, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))


                RowIndex += 1
                Write("Bank Statement from " & FROMDATE & " to " & TODATE, Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                SetBorder(RowIndex, Range("1"), Range("8"))


                'HEADERS
                RowIndex += 2
                Write("Date", Range("1"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Booking No", Range("2"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Name", Range("3"), XlHAlign.xlHAlignCenter, Range("4"), True, 10)
                Write("Chq No", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Debit", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Credit", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
                Write("Balance", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
                SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)



                RowIndex += 2
                Write("Bank Balance as per Bank Pass Book", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                Write(Format(Val(DTTOTAL.Rows(0).Item("BOOKBAL")), "0.00") & "  " & DTTOTAL.Rows(0).Item("BOOKDRCR"), Range("8"), XlHAlign.xlHAlignRight, , True, 12)
                BALANCE = Val(DTTOTAL.Rows(0).Item("BOOKBAL"))


                'GET ALL DEBIT AMOUNT
                I = 0
                Dim RDATE As Date = Now.Date
                Dim FROW As Boolean = True
                Dim FROMNO As Integer = 0
                RowIndex += 1
                For Each DTROW As DataRow In DT.Rows
                    I += 1
                    RowIndex += 1
                    'GET REOCDATE ONLY ONCE
                    If RDATE <> DTROW("RECODATE") Then
                        If FROW = False Then
                            RowIndex += 1
                            Write(DTROW("RECODATE"), Range("5"), XlHAlign.xlHAlignRight, , True, 10)
                            FORMULA("=SUM(" & objColumn.Item("6").ToString & FROMNO & ":" & objColumn.Item("6").ToString & RowIndex - 1 & ")", Range("6"), XlHAlign.xlHAlignRight, , True, 12)
                            FORMULA("=SUM(" & objColumn.Item("7").ToString & FROMNO & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("7"), XlHAlign.xlHAlignRight, , True, 12)

                            'SET BALANCE
                            BALANCE = (BALANCE + Val(objSheet.Range(Range("7"), Range("7")).Text)) - Val(objSheet.Range(Range("6"), Range("6")).Text)

                            Write(Format(BALANCE, "0.00"), Range("8"), XlHAlign.xlHAlignRight, , True, 12)
                            SetBorder(RowIndex, Range("1"), Range("8"))
                        End If

                        RowIndex += 2
                        Write(DTROW("RECODATE"), Range("1"), XlHAlign.xlHAlignLeft, , False, 10)
                        RDATE = DTROW("RECODATE")
                        'GET TOTAL OF THE PARTICULAR DATE IF NOT FIRST ROW

                        FROMNO = RowIndex
                    End If
                    Write(DTROW("BILLINITIALS"), Range("2"), XlHAlign.xlHAlignLeft, , False, 10)
                    Write(DTROW("NAME"), Range("3"), XlHAlign.xlHAlignLeft, Range("4"), False, 10)
                    Write(DTROW("CHQNO"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                    Write(DTROW("DR"), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                    Write(DTROW("CR"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)
                    FROW = False
                Next


                RowIndex += 1
                Write("Total :", Range("7"), XlHAlign.xlHAlignRight, , True, 10)
                FORMULA("=SUM(" & objColumn.Item("7").ToString & RowIndex - I & ":" & objColumn.Item("7").ToString & RowIndex - 1 & ")", Range("8"), XlHAlign.xlHAlignRight, , True, 12)
                SetBorder(RowIndex, Range("1"), Range("8"))




                RowIndex += 2
                Write("Balance as Per Bank Book :", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
                Write(Format(Val(DTTOTAL.Rows(0).Item("BANKBAL")), "0.00") & "  " & DTTOTAL.Rows(0).Item("BANKDRCR"), Range("8"), XlHAlign.xlHAlignRight, , True, 12)


                SetBorder(RowIndex, objColumn.Item("1").ToString & 9, objColumn.Item("1").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("2").ToString & 9, objColumn.Item("2").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("3").ToString & 9, objColumn.Item("4").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("5").ToString & 9, objColumn.Item("5").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("6").ToString & 9, objColumn.Item("6").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("7").ToString & 9, objColumn.Item("7").ToString & RowIndex)
                SetBorder(RowIndex, objColumn.Item("8").ToString & 9, objColumn.Item("8").ToString & RowIndex)



                objBook.Application.ActiveWindow.Zoom = 95

                With objSheet.PageSetup
                    .Orientation = XlPageOrientation.xlPortrait
                    .TopMargin = 144
                    .LeftMargin = 50.4
                    .RightMargin = 0
                    .BottomMargin = 0
                    .Zoom = False
                    .FitToPagesTall = 1
                    .FitToPagesWide = 1
                End With

                SaveAndClose()

            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

#End Region

#Region "TRIALBALANCE"

    Public Function TRIALBALANCE_EXCEL(ByVal CONDITION As String, ByVal CMPID As Integer, ByVal LOCATIONID As Integer, ByVal YEARID As Integer) As Object
        Try

            SetWorkSheet()
            For I As Integer = 1 To 26
                SetColumn(I, Chr(64 + I))
            Next


            RowIndex = 1
            For i As Integer = 1 To 26
                SetColumnWidth(Range(i), 13)
            Next

            '''''''''''Report Title
            Dim OBJCMN As New ClsCommon
            'CMPNAME
            Dim DTCMP As System.Data.DataTable = OBJCMN.search(" CMP_NAME AS CMPNAME, CMP_ADD1 As ADD1, CMP_ADD2 AS ADD2", "", " CMPMASTER", " AND CMP_ID = " & CMPID)

            RowIndex = 2
            Write(DTCMP.Rows(0).Item("CMPNAME"), Range("1"), XlHAlign.xlHAlignCenter, Range("9"), True, 14)
            SetBorder(RowIndex, Range("1"), Range("9"))

            'ADD1
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD1"), Range("1"), XlHAlign.xlHAlignCenter, Range("9"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("9"))

            'ADD2
            RowIndex += 1
            Write(DTCMP.Rows(0).Item("ADD2"), Range("1"), XlHAlign.xlHAlignCenter, Range("9"), True, 10)
            SetBorder(RowIndex, Range("1"), Range("9"))

            RowIndex += 1
            Write("TRIAL-BALANCE", Range("1"), XlHAlign.xlHAlignCenter, Range("9"), True, 12)
            SetBorder(RowIndex, Range("1"), Range("9"))


            'FREEZE TOP 7 ROWS
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("9").ToString & 8).Select()
            objSheet.Range(objColumn.Item("1").ToString & 8, objColumn.Item("9").ToString & 8).Application.ActiveWindow.FreezePanes = True


            SetBorder(RowIndex + 1, objColumn.Item("1").ToString & RowIndex + 1, objColumn.Item("9").ToString & RowIndex + 1)

            RowIndex += 2
            Write("Name", Range("1"), XlHAlign.xlHAlignLeft, Range("3"), True, 10)
            Write("O/P Dr", Range("4"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("O/P Cr", Range("5"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Debit", Range("6"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Credit", Range("7"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Closing Dr", Range("8"), XlHAlign.xlHAlignCenter, , True, 10)
            Write("Closing Cr", Range("9"), XlHAlign.xlHAlignCenter, , True, 10)

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)


            Dim DT As System.Data.DataTable = OBJCMN.search("*", "", " TEMPTRIALBALANCEPRINT", " ORDER BY ROWNO")

            For Each DTROW As DataRow In DT.Rows
                RowIndex += 1
                Write(DTROW("NAME"), Range("1"), XlHAlign.xlHAlignLeft, Range("3"), False, 10)
                If DTROW("OPENINGDR") > 0 Then
                    Write(Format(Val(DTROW("OPENINGDR")), "0.00"), Range("4"), XlHAlign.xlHAlignRight, , False, 10)
                End If
                If DTROW("OPENINGCR") > 0 Then
                    Write(Format(Val(DTROW("OPENINGCR")), "0.00"), Range("5"), XlHAlign.xlHAlignRight, , False, 10)
                End If
                Write(Format(Val(DTROW("DEBIT")), "0.00"), Range("6"), XlHAlign.xlHAlignRight, , False, 10)
                Write(Format(Val(DTROW("CREDIT")), "0.00"), Range("7"), XlHAlign.xlHAlignRight, , False, 10)

                If DTROW("CLOSINGDR") > 0 Then
                    Write(Format(Val(DTROW("CLOSINGDR")), "0.00"), Range("8"), XlHAlign.xlHAlignRight, , False, 10)
                End If
                If DTROW("CLOSINGCR") > 0 Then
                    Write(Format(Val(DTROW("CLOSINGCR")), "0.00"), Range("9"), XlHAlign.xlHAlignRight, , False, 10)
                End If
                If Left(DTROW("NAME"), 1) = " " And Left(DTROW("NAME"), 6) <> "      " Then
                    objSheet.Range(objColumn.Item("1").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green)
                ElseIf Left(DTROW("NAME"), 1) <> " " Then
                    objSheet.Range(objColumn.Item("1").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon)
                End If

            Next

            SetBorder(RowIndex, objColumn.Item("1").ToString & RowIndex, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & RowIndex, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & RowIndex, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & RowIndex, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & RowIndex, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & RowIndex, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & RowIndex, objColumn.Item("9").ToString & RowIndex)

            SetBorder(RowIndex, objColumn.Item("1").ToString & 8, objColumn.Item("3").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("4").ToString & 8, objColumn.Item("4").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("5").ToString & 8, objColumn.Item("5").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("6").ToString & 8, objColumn.Item("6").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("7").ToString & 8, objColumn.Item("7").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("8").ToString & 8, objColumn.Item("8").ToString & RowIndex)
            SetBorder(RowIndex, objColumn.Item("9").ToString & 8, objColumn.Item("9").ToString & RowIndex)


            objBook.Application.ActiveWindow.Zoom = 95

            With objSheet.PageSetup
                .Orientation = XlPageOrientation.xlPortrait
                .TopMargin = 20
                .LeftMargin = 10
                .RightMargin = 5
                .BottomMargin = 10
                .Zoom = False
                .FitToPagesTall = 1
                .FitToPagesWide = 1
            End With

            SaveAndClose()

        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

#End Region

End Class
