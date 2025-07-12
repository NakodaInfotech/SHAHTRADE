Imports db


Public Class ClsYearMaster

    Private objDBOperation As DBOperation
    Public alParaval As New ArrayList
    Dim areaid, cityid, stateid, countryid As Integer
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

    Public Function savearea(ByVal areaname As String, ByVal CMP As Integer, ByVal LOCATION As Integer, ByVal userid As Integer, ByVal YEAR As Integer, Optional ByVal whereclause As String = "", Optional ByVal arearemark As String = "") As Integer

line1:
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable = objclscommon.search("area_id", "", "AreaMaster", whereclause & " AND AREA_NAME = '" & areaname & "'")
        If (dt.Rows.Count) > 0 Then
            areaid = dt.Rows.Item(0).Item("area_id").ToString
        Else

            Dim strCmd As String = "SP_MASTER_AREAMASTER_SAVE"
            Dim alpara As New ArrayList
            With alpara
                .Add(New SqlClient.SqlParameter("@area_name", areaname))
                .Add(New SqlClient.SqlParameter("@area_remark", arearemark))
                .Add(New SqlClient.SqlParameter("@area_cmpid", CMP))
                .Add(New SqlClient.SqlParameter("@area_locationid", LOCATION))
                .Add(New SqlClient.SqlParameter("@area_userid", userid))
                .Add(New SqlClient.SqlParameter("@area_yearid", YEAR))
                .Add(New SqlClient.SqlParameter("@area_transfer", 0))
            End With

            intResult = objDBOperation.executeNonQuery(strCmd, alpara)
            GoTo line1
        End If

    End Function

    Public Function savestreet(ByVal streetname As String, ByVal CMP As Integer, ByVal LOCATION As Integer, ByVal userid As Integer, ByVal YEAR As Integer, Optional ByVal whereclause As String = "", Optional ByVal streetremark As String = "") As Integer

line1:
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable = objclscommon.search("street_id", "", "StreetMaster", whereclause)
        If (dt.Rows.Count) > 0 Then
            areaid = dt.Rows.Item(0).Item("street_id").ToString
        Else

            Dim strCmd As String = "SP_MASTER_STREETMASTER_SAVE"
            Dim alpara As New ArrayList
            With alpara
                .Add(New SqlClient.SqlParameter("@street_name", streetname))
                .Add(New SqlClient.SqlParameter("@street_remark", streetremark))
                .Add(New SqlClient.SqlParameter("@street_cmpid", CMP))
                .Add(New SqlClient.SqlParameter("@street_locationid", LOCATION))
                .Add(New SqlClient.SqlParameter("@street_userid", userid))
                .Add(New SqlClient.SqlParameter("@street_yearid", YEAR))
                .Add(New SqlClient.SqlParameter("@street_transfer", 0))
            End With

            intResult = objDBOperation.executeNonQuery(strCmd, alpara)
            GoTo line1
        End If

    End Function

    Public Function savecity(ByVal cityname As String, ByVal CMP As Integer, ByVal LOCATION As Integer, ByVal userid As Integer, ByVal YEAR As Integer, Optional ByVal whereclause As String = "", Optional ByVal cityremark As String = "") As Integer

line1:
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable = objclscommon.search("city_id", "", "citymaster", whereclause & " AND CITY_NAME = '" & cityname & "'")
        If (dt.Rows.Count) > 0 Then
            cityid = dt.Rows.Item(0).Item("city_id").ToString
        Else

            Dim strCmd As String = "SP_MASTER_CITYMASTER_SAVE"
            Dim alpara As New ArrayList
            With alpara
                .Add(New SqlClient.SqlParameter("@city_name", cityname))
                .Add(New SqlClient.SqlParameter("@city_remark", cityremark))
                .Add(New SqlClient.SqlParameter("@city_CMPID", CMP))
                .Add(New SqlClient.SqlParameter("@city_LOCATIONID", LOCATION))
                .Add(New SqlClient.SqlParameter("@city_userid", userid))
                .Add(New SqlClient.SqlParameter("@city_YEARID", YEAR))
                .Add(New SqlClient.SqlParameter("@city_transfer", 0))
            End With

            intResult = objDBOperation.executeNonQuery(strCmd, alpara)
            GoTo line1
        End If

    End Function

    Public Function savestate(ByVal statename As String, ByVal CMP As Integer, ByVal LOCATION As Integer, ByVal userid As Integer, ByVal YEAR As Integer, Optional ByVal whereclause As String = "", Optional ByVal stateremark As String = "") As Integer

line1:
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable = objclscommon.search("state_id", "", "statemaster", whereclause & " AND STATE_NAME = '" & statename & "'")
        If (dt.Rows.Count) > 0 Then
            stateid = dt.Rows.Item(0).Item("state_id").ToString
        Else

            Dim strCmd As String = "SP_MASTER_STATEMASTER_SAVE"
            Dim alpara As New ArrayList
            With alpara
                .Add(New SqlClient.SqlParameter("@state_name", statename))
                .Add(New SqlClient.SqlParameter("@state_remark", stateremark))
                .Add(New SqlClient.SqlParameter("@state_cmpid", CMP))
                .Add(New SqlClient.SqlParameter("@state_locationid", LOCATION))
                .Add(New SqlClient.SqlParameter("@state_userid", userid))
                .Add(New SqlClient.SqlParameter("@state_yearid", YEAR))
                .Add(New SqlClient.SqlParameter("@state_transfer", 0))
            End With

            intResult = objDBOperation.executeNonQuery(strCmd, alpara)
            GoTo line1
        End If
    End Function

    Public Function savecountry(ByVal countryname As String, ByVal CMP As Integer, ByVal LOCATION As Integer, ByVal userid As Integer, ByVal YEAR As Integer, Optional ByVal whereclause As String = "", Optional ByVal countryremark As String = "") As Integer

line1:
        Dim objclscommon As New ClsCommonMaster
        Dim dt As DataTable = objclscommon.search("country_id", "", "countrymaster", whereclause & " AND COUNTRY_NAME = '" & countryname & "'")
        If (dt.Rows.Count) > 0 Then
            countryid = dt.Rows.Item(0).Item("country_id").ToString
        Else

            Dim strCmd As String = "SP_MASTER_COUNTRYMASTER_SAVE"
            Dim alpara As New ArrayList
            With alpara
                .Add(New SqlClient.SqlParameter("@country_name", countryname))
                .Add(New SqlClient.SqlParameter("@country_remark", countryremark))
                .Add(New SqlClient.SqlParameter("@country_cmpid", CMP))
                .Add(New SqlClient.SqlParameter("@country_locationid", LOCATION))
                .Add(New SqlClient.SqlParameter("@country_userid", userid))
                .Add(New SqlClient.SqlParameter("@country_yearid", YEAR))
                .Add(New SqlClient.SqlParameter("@country_transfer", 0))
            End With

            intResult = objDBOperation.executeNonQuery(strCmd, alpara)
            GoTo line1
        End If
    End Function

    Public Function SAVE() As Integer

        Try

            'search whether city is present or not
            If alParaval(8).ToString <> "" Then
                savecity(alParaval(8), alParaval(35), 0, 0, 0)
            End If

            'search whether state is present or not
            If alParaval(10).ToString <> "" Then
                savestate(alParaval(10), 0, 0, alParaval(35), 0)
            End If


            'search whether Country is present or not
            If alParaval(11).ToString <> "" Then
                savecountry(alParaval(11), 0, 0, alParaval(35), 0)
            End If



            'save cmpdetails
            Dim strCommand As String = "SP_MASTER_CMPMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@cmpperson", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@cmppersontype", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@cmpdisplayedname", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@cmpinvinitials", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@cmpinvfooter", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@cmpadd1", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@cmpadd2", alParaval(7)))
                .Add(New SqlClient.SqlParameter("@cmpcityid", cityid))
                .Add(New SqlClient.SqlParameter("@cmpzipcode", alParaval(9)))
                .Add(New SqlClient.SqlParameter("@cmpstateid", stateid))
                .Add(New SqlClient.SqlParameter("@cmpcountryid", countryid))
                .Add(New SqlClient.SqlParameter("@cmptel", alParaval(12)))
                .Add(New SqlClient.SqlParameter("@cmpfax", alParaval(13)))
                .Add(New SqlClient.SqlParameter("@cmpemail", alParaval(14)))
                .Add(New SqlClient.SqlParameter("@cmpwebsite", alParaval(15)))
                .Add(New SqlClient.SqlParameter("@cmpvatno", alParaval(16)))
                .Add(New SqlClient.SqlParameter("@cmpcstno", alParaval(17)))
                .Add(New SqlClient.SqlParameter("@cmpstno", alParaval(18)))
                .Add(New SqlClient.SqlParameter("@cmppanno", alParaval(19)))
                .Add(New SqlClient.SqlParameter("@cmpeccno", alParaval(20)))
                .Add(New SqlClient.SqlParameter("@cmpexciseno", alParaval(21)))
                .Add(New SqlClient.SqlParameter("@cmpplano", alParaval(22)))
                .Add(New SqlClient.SqlParameter("@cmprange", alParaval(23)))
                .Add(New SqlClient.SqlParameter("@cmpdivision", alParaval(24)))
                .Add(New SqlClient.SqlParameter("@cmpexciseoff", alParaval(25)))
                .Add(New SqlClient.SqlParameter("@cmpdivisionoff", alParaval(26)))
                .Add(New SqlClient.SqlParameter("@cmpcommissionerate", alParaval(27)))
                .Add(New SqlClient.SqlParameter("@cmpheadingno", alParaval(28)))
                .Add(New SqlClient.SqlParameter("@cmpGSTIN", alParaval(29)))
                .Add(New SqlClient.SqlParameter("@FROMCITY", alParaval(30)))
                .Add(New SqlClient.SqlParameter("@CMPEWBUSER", alParaval(31)))
                .Add(New SqlClient.SqlParameter("@CMPEWBPASS", alParaval(32)))
                .Add(New SqlClient.SqlParameter("@DISPATCHFROM", alParaval(33)))
                .Add(New SqlClient.SqlParameter("@cmppassword", alParaval(34)))
                .Add(New SqlClient.SqlParameter("@cmpuserid", alParaval(35)))
                .Add(New SqlClient.SqlParameter("@cmptransfer", alParaval(36)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)


            'save yearmaster
            strCommand = "SP_MASTER_YEARMASTER_SAVE"
            alParameter.Clear()
            With alParameter
                .Add(New SqlClient.SqlParameter("@cmpname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@yearstart", alParaval(37)))
                .Add(New SqlClient.SqlParameter("@yearend", alParaval(38)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(35)))
                .Add(New SqlClient.SqlParameter("@locationid", "0"))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(36)))
                .Add(New SqlClient.SqlParameter("@cmpwithotsp", alParaval(40)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)



        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function SAVEYEAR() As Integer

        Try

            'save cmpdetails
            Dim strCommand As String = "SP_MASTER_YEARMASTER_SAVE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@cmpname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@yearstart", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@yearend", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@locationid", "0"))
                .Add(New SqlClient.SqlParameter("@transfer", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@cmpwithotsp", alParaval(5)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
            Return intResult

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function UPDATE() As Integer

        Try

            'search whether city is present or not
            If alParaval(8).ToString <> "" Then
                savecity(alParaval(8), alParaval(35), 0, 0, 0)
            End If

            'search whether state is present or not
            If alParaval(10).ToString <> "" Then
                savestate(alParaval(10), 0, 0, alParaval(35), 0)
            End If


            'search whether Country is present or not
            If alParaval(11).ToString <> "" Then
                savecountry(alParaval(11), 0, 0, alParaval(35), 0)
            End If



            'save cmpdetails
            Dim strCommand As String = "SP_MASTER_CMPMASTER_UPDATE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpname", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@cmpperson", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@cmppersontype", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@cmpdisplayedname", alParaval(3)))
                .Add(New SqlClient.SqlParameter("@cmpinvinitials", alParaval(4)))
                .Add(New SqlClient.SqlParameter("@cmpinvfooter", alParaval(5)))
                .Add(New SqlClient.SqlParameter("@cmpadd1", alParaval(6)))
                .Add(New SqlClient.SqlParameter("@cmpadd2", alParaval(7)))
                .Add(New SqlClient.SqlParameter("@cmpcityid", cityid))
                .Add(New SqlClient.SqlParameter("@cmpzipcode", alParaval(9)))
                .Add(New SqlClient.SqlParameter("@cmpstateid", stateid))
                .Add(New SqlClient.SqlParameter("@cmpcountryid", countryid))
                .Add(New SqlClient.SqlParameter("@cmptel", alParaval(12)))
                .Add(New SqlClient.SqlParameter("@cmpfax", alParaval(13)))
                .Add(New SqlClient.SqlParameter("@cmpemail", alParaval(14)))
                .Add(New SqlClient.SqlParameter("@cmpwebsite", alParaval(15)))
                .Add(New SqlClient.SqlParameter("@cmpvatno", alParaval(16)))
                .Add(New SqlClient.SqlParameter("@cmpcstno", alParaval(17)))
                .Add(New SqlClient.SqlParameter("@cmpstno", alParaval(18)))
                .Add(New SqlClient.SqlParameter("@cmppanno", alParaval(19)))
                .Add(New SqlClient.SqlParameter("@cmpeccno", alParaval(20)))
                .Add(New SqlClient.SqlParameter("@cmpexciseno", alParaval(21)))
                .Add(New SqlClient.SqlParameter("@cmpplano", alParaval(22)))
                .Add(New SqlClient.SqlParameter("@cmprange", alParaval(23)))
                .Add(New SqlClient.SqlParameter("@cmpdivision", alParaval(24)))
                .Add(New SqlClient.SqlParameter("@cmpexciseoff", alParaval(25)))
                .Add(New SqlClient.SqlParameter("@cmpdivisionoff", alParaval(26)))
                .Add(New SqlClient.SqlParameter("@cmpcommissionerate", alParaval(27)))
                .Add(New SqlClient.SqlParameter("@cmpheadingno", alParaval(28)))
                .Add(New SqlClient.SqlParameter("@cmpGSTIN", alParaval(29)))
                .Add(New SqlClient.SqlParameter("@FROMCITY", alParaval(30)))
                .Add(New SqlClient.SqlParameter("@CMPEWBUSER", alParaval(31)))
                .Add(New SqlClient.SqlParameter("@CMPEWBPASS", alParaval(32)))
                .Add(New SqlClient.SqlParameter("@DISPATCHFROM", alParaval(33)))
                .Add(New SqlClient.SqlParameter("@cmppassword", alParaval(34)))
                .Add(New SqlClient.SqlParameter("@TEMPCMPNAME", alParaval(40)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)

        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CMPDELETE() As Integer
        Try
            Dim strCommand As String = "SP_MASTER_CMPMASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(0)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

    Public Function YEARDELETE() As Integer
        Try
            Dim strCommand As String = "SP_MASTER_YEARMASTER_DELETE"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@CMPID", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@YEARID", alParaval(1)))
            End With
            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0
    End Function

#End Region

End Class


