
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports DevExpress.XtraGrid
Imports DevExpress.XtraRichEdit.Model
Imports Microsoft.VisualBasic.FileIO

Public Class PalathiAaradhanaGridReport

    Private Sub PalathiAaradhanaGridReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        FILLGRID()
    End Sub

    Sub FILLGRID()
        Try
            Dim url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQ3gIuDxQVwm01C2mPTN1h5n7eAbavb4ZxZSWOkH0tZhy5808aifXQWHqOoQX_bKMWc1ZOg2-V8F4K5/pub?gid=1043649683&single=true&output=csv"

            Dim client As New WebClient()
            Dim csvBytes = client.DownloadData(url)
            Dim csvText = Encoding.UTF8.GetString(csvBytes)


            Dim dt As DataTable = CsvToDataTable(csvText)

            Using con As New SqlConnection("Data Source=LAPTOP;Initial Catalog=SHAHTRADE;User ID=sa;Password=Infosys@123;connection timeout=2000")
                con.Open()

                Using cmd As New SqlCommand("DELETE FROM TEMPPALATI", con)
                    cmd.ExecuteNonQuery()
                End Using

                Using bulk As New SqlBulkCopy(con)
                    bulk.DestinationTableName = "TEMPPALATI"
                    bulk.WriteToServer(dt)
                End Using
            End Using

            'gridbilldetails.DataSource = dt
            'gridbill.PopulateColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CsvToDataTable(csvText As String) As DataTable
        Try
            Dim dt As New DataTable()

            Using reader As New StringReader(csvText)
                Using parser As New TextFieldParser(reader)
                    parser.SetDelimiters(",")
                    parser.HasFieldsEnclosedInQuotes = True

                    Dim firstRow As Boolean = True

                    While Not parser.EndOfData
                        Dim fields = parser.ReadFields()

                        If firstRow Then
                            ' Create columns (UTF-8 + linebreak safe)
                            For Each f In fields
                                dt.Columns.Add(f.Trim())
                            Next
                            firstRow = False
                        Else
                            dt.Rows.Add(fields)
                        End If
                    End While
                End Using
            End Using
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub CMDREFRESH_Click(sender As Object, e As EventArgs) Handles CMDREFRESH.Click
        Try
            FILLGRID
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CMDPRINT_Click(sender As Object, e As EventArgs) Handles CMDPRINT.Click
        Try
            Dim PATH As String = "" = ""
            If FileIO.FileSystem.FileExists(PATH) = True Then FileIO.FileSystem.DeleteFile(PATH)
            PATH = Application.StartupPath & "\Aaradhana Details.XLS"

            Dim opti As New DevExpress.XtraPrinting.XlsExportOptions
            opti.ShowGridLines = True

            Dim PERIOD As String = AccFrom & " - " & AccTo

            opti.SheetName = "Aaradhana Details"
            gridbill.ExportToXls(PATH, opti)
            EXCELCMPHEADER(PATH, "LedAaradhanager Details", gridbill.VisibleColumns.Count + gridbill.GroupCount, "", PERIOD)

        Catch ex As Exception
            MsgBox("Aaradhana Details Excel File is Open, Please Close the File first then try to Export", MsgBoxStyle.Critical)
        End Try
    End Sub
End Class