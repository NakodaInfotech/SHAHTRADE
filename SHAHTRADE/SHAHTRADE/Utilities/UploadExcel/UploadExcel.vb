Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports DevExpress.Utils.CommonDialogs
Imports Microsoft.Office.Interop.Excel
Public Class UploadExcel
    Dim USERADD, USEREDIT, USERVIEW, USERDELETE As Boolean      'USED FOR RIGHT MANAGEMAENT

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If Not ERRORVALID() Then
                Exit Sub
            End If

            Dim IntResult As Integer
            Dim alParaval As New ArrayList
            alParaval.Add(Format(Convert.ToDateTime(UPLOADDATE.Text).Date, "MM/dd/yyyy"))

            alParaval.Add(CMBNAME.Text.Trim)
            alParaval.Add(TXTPROJECT.Text.Trim)
            alParaval.Add(TXTJOBCARD.Text.Trim)
            alParaval.Add(TXTDRAWINGNO.Text.Trim)

            alParaval.Add(CmpId)
            alParaval.Add(Userid)
            alParaval.Add(YearId)
            'Save GRID
            Dim GRIDSRNO As String = ""
            Dim S1NO As String = ""
            Dim ITEMNO As String = ""
            Dim TYPE As String = ""
            Dim W1 As String = ""
            Dim H1 As String = ""
            Dim W2 As String = ""
            Dim H2 As String = ""
            Dim LENGTH As String = ""
            Dim QTY As String = ""
            Dim GUAGE As String = ""
            Dim AREA As String = ""
            Dim C1 As String = ""
            Dim C2 As String = ""


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function ERRORVALID() As Boolean
        Try
            Dim BLN As Boolean = True
            If CMBNAME.Text.Trim.Length = 0 Then
                EP.SetError(CMBNAME, "Please Fill Contractor Name")
                BLN = False
            End If
            Return BLN
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub CMDEXCELUPLOAD_Click(sender As Object, e As EventArgs) Handles CMDEXCELUPLOAD.Click
        Try
            'If EDIT = True Then Exit Sub

            OpenFileDialog1.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx"
            If OpenFileDialog1.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim oExcel As New Excel.Application
            Dim oBook As Excel.Workbook = oExcel.Workbooks.Open(OpenFileDialog1.FileName)
            Dim oSheet As Excel.Worksheet = oBook.Worksheets("STR")

            'Clear grid
            DGVSTR.Rows.Clear()
            Dim srNo As Integer = 1
            'Ask start & end row (same as old code)
            Dim FROMROW As Integer = Val(InputBox("Enter Start Row No"))
            Dim TOROW As Integer = Val(InputBox("Enter End Row No"))

            For i As Integer = FROMROW To TOROW

                'Skip empty Item No
                If oSheet.Range("B" & i).Text.ToString.Trim = "" Then Continue For

                Dim r As Integer = DGVSTR.Rows.Add()
                Try
                    If DGVSTR.Columns.Cast(Of DataGridViewColumn)().Any(Function(c) c.Name = "SRNO") Then
                        DGVSTR.Rows(r).Cells("SRNO").Value = srNo
                    Else
                        DGVSTR.Rows(r).Cells(0).Value = srNo
                    End If
                Catch
                    'ignore if column/index not present
                End Try
                srNo += 1
                DGVSTR.Rows(r).Cells("GS1NO").Value = oSheet.Range("A" & i).Text
                DGVSTR.Rows(r).Cells("GITEMNO").Value = oSheet.Range("B" & i).Text
                DGVSTR.Rows(r).Cells("GTYPE").Value = oSheet.Range("C" & i).Text

                DGVSTR.Rows(r).Cells("GW1").Value = Val(oSheet.Range("D" & i).Text)
                DGVSTR.Rows(r).Cells("GH1").Value = Val(oSheet.Range("E" & i).Text)
                DGVSTR.Rows(r).Cells("GW2").Value = Val(oSheet.Range("F" & i).Text)
                DGVSTR.Rows(r).Cells("GH2").Value = Val(oSheet.Range("G" & i).Text)

                DGVSTR.Rows(r).Cells("GLENGTH").Value = Val(oSheet.Range("H" & i).Text)
                DGVSTR.Rows(r).Cells("GQTY").Value = Val(oSheet.Range("I" & i).Text)

                DGVSTR.Rows(r).Cells("GGUAGES").Value = Val(oSheet.Range("J" & i).Text)
                DGVSTR.Rows(r).Cells("GAREA").Value = Val(oSheet.Range("K" & i).Text)

                DGVSTR.Rows(r).Cells("GC1").Value = oSheet.Range("L" & i).Text
                DGVSTR.Rows(r).Cells("GC2").Value = oSheet.Range("M" & i).Text

            Next

            MessageBox.Show("Excel uploaded successfully")

            'IMPORTANT CLEANUP (VERY IMPORTANT)
            oBook.Close(False)
            oExcel.Quit()

            ReleaseObject(oSheet)
            ReleaseObject(oBook)
            ReleaseObject(oExcel)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub UploadExcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim DTROW() As DataRow
            DTROW = USERRIGHTS.Select("FormName = 'LABOUR'")
            USERADD = DTROW(0).Item(1)
            USEREDIT = DTROW(0).Item(2)
            USERVIEW = DTROW(0).Item(3)
            USERDELETE = DTROW(0).Item(4)

            Cursor.Current = Cursors.WaitCursor
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            If obj IsNot Nothing Then
                Marshal.ReleaseComObject(obj)
            End If
        Catch
        Finally
            obj = Nothing
        End Try
    End Sub
End Class