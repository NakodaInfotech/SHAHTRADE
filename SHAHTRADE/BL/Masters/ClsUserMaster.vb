
Imports DB

Public Class ClsUserMaster

    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim intResult As Integer

#Region "Constructor"
    Public Sub New()
        Try
            objDBOperation = New DBOperation
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Functions"

    Public Function save() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_MASTER_USERMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@Username", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@Password", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@EMAIL", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@TEL", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@SMTP", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@POP", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@SMTPEMAIL", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@SMTPPASS", alParaval(7)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(8)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(9)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(10)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(11)))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(12)))
                .Add(New SqlClient.SqlParameter("@formname", alParaval(13)))
                .Add(New SqlClient.SqlParameter("@add", alParaval(14)))
                .Add(New SqlClient.SqlParameter("@edit", alParaval(15)))
                .Add(New SqlClient.SqlParameter("@view", alParaval(16)))
                .Add(New SqlClient.SqlParameter("@delete", alParaval(17)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function update() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_MASTER_USERMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@Username", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@Password", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@EMAIL", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@TEL", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@SMTP", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@POP", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@SMTPEMAIL", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@SMTPPASS", alParaval(7)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(8)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(9)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(10)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(11)))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(12)))
                .Add(New SqlClient.SqlParameter("@formname", alParaval(13)))
                .Add(New SqlClient.SqlParameter("@add", alParaval(14)))
                .Add(New SqlClient.SqlParameter("@edit", alParaval(15)))
                .Add(New SqlClient.SqlParameter("@view", alParaval(16)))
                .Add(New SqlClient.SqlParameter("@delete", alParaval(17)))
                .Add(New SqlClient.SqlParameter("@UNAME", alParaval(18)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function updateuserdetails() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_MASTER_USERDETAILS_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@Username", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@Password", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@EMAIL", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@TEL", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@SMTP", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@POP", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@SMTPEMAIL", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@SMTPPASS", alParaval(7)))
                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(8)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(9)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(10)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(11)))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(12)))
                .Add(New SqlClient.SqlParameter("@UNAME", alParaval(18)))

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
            strcommand = "SP_MASTER_USERASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@USERNAME", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(1)))
            End With
            dtTable = objDBOperation.execute(strcommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dtTable
    End Function

#End Region

End Class
