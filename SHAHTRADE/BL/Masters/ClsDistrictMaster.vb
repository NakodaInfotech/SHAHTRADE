Imports DB

Public Class ClsDistrictMaster

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

#Region "Function"

    Public Function save() As Integer
        Dim intResult As Integer

        Try

            'save Taxmaster
            Dim strCommand As String = "SP_MASTER_DISTRICTMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@district_name", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@district_remark", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@district_cmpid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@district_locationid", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@district_userid", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@district_yearid", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@district_transfer", alParaval(6)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function saveVIA() As Integer
        Dim intResult As Integer

        Try

            'save Taxmaster
            Dim strCommand As String = "SP_MASTER_VIAMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@VIAname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@remarks", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(6)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function


    Public Function Update() As Integer
        Dim intResult As Integer

        Try

            'save Taxmaster
            Dim strCommand As String = "SP_MASTER_DISTRICTMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@VIAname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@remarks", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@VIAid", alParaval(7)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function UpdateVIA() As Integer
        Dim intResult As Integer

        Try

            'save Taxmaster
            Dim strCommand As String = "SP_MASTER_VIAMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@district_name", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@district_remark", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@district_cmpid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@district_locationid", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@district_userid", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@district_yearid", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@district_transfer", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@district_id", alParaval(7)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function DELETE() As DataTable
        Dim dtTable As New DataTable
        Dim strcommand As String = ""
        Try
            strcommand = "SP_MASTER_TYPEMASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@TYPENAME", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@LOCATIONID", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(3)))
            End With
            dtTable = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

#End Region
End Class
