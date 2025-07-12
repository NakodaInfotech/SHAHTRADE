
Imports DB

Public Class ClsIssueVoucher

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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUEVOUCHER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HALLMARK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REQNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@VOUCHERWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECD", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BOXWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WTWITHTAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INITIALGROSSWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALGROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALLESSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALNETTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALFINEWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HSNCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LESSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NETTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PURITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FINEWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HUID", alParaval(I)))
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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUEVOUCHER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                Dim I As Integer = 0

                .Add(New SqlClient.SqlParameter("@DATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NAME", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HALLMARK", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@REQNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@VOUCHERWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECD", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@RECDATE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@BOXWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@WTWITHTAGS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@INITIALGROSSWT", alParaval(I)))
                I = I + 1


                .Add(New SqlClient.SqlParameter("@remarks", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALPCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALGROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALLESSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALNETTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@TOTALFINEWT", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@userid", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(I)))
                I = I + 1

                'grid parameters
                .Add(New SqlClient.SqlParameter("@SRNO", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@ITEM", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HSNCODE", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GRIDREMARKS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PCS", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@GROSSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@LESSWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@NETTWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@PURITY", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@FINEWT", alParaval(I)))
                I = I + 1
                .Add(New SqlClient.SqlParameter("@HUID", alParaval(I)))
                I = I + 1

                .Add(New SqlClient.SqlParameter("@ISSUENO", alParaval(I)))
                I = I + 1

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function SELECTISSUE() As DataTable
        Dim dtTable As DataTable
        Try

            Dim strCommand As String = "SP_TRANS_SALE_ISSUEVOUCHER_SELECT"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ISSUENO", alParaval(0)))
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
            Dim strCommand As String = "SP_TRANS_SALE_ISSUEVOUCHER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@ISSUENO", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YearID", alParaval(1)))

            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
