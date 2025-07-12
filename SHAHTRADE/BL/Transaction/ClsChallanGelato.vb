
Imports DB

Public Class ClsChallanGelato

    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Public Function SAVE() As DataTable
        Dim DTTABLE As DataTable
        Try
            'save  order
            Dim strCommand As String = "SP_TRANS_SALE_GDNGELATO_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSPORT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BOXES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@totalqty", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALAMT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@srno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE30", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE32", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE34", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE36", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE38", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE40", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE42", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE44", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE46", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE48", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE50", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@QTY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMT", alParaval(I)))
                I = I + 1

            End With

            DTTABLE = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return DTTABLE

    End Function

    Public Function UPDATE() As Integer
        Dim intResult As Integer
        Try
            'update order
            Dim strCommand As String = "SP_TRANS_SALE_GDNGELATO_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TRANSPORT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AGENT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REFNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BOXES", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@totalqty", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALAMT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@srno", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@DESIGNNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE30", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE32", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE34", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE36", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE38", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE40", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE42", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE44", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE46", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE48", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@SIZE50", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@QTY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@AMT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@CHALLANNO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTNO() As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_TRANS_SALE_GDNGELATO_SELECT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CHALLANNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))
            End With
            dtTable = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

    Public Function DELETE() As Integer
        Dim intResult As Integer
        Try
            Dim strCommand As String = "SP_TRANS_SALE_GDNGELATO_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CHALLANNO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
