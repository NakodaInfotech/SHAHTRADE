
Imports System.IO

Module Module1

    '******** COMMON VARIABLES **************
    Public Mydate As Date                                               'Used for SQL Date throughout the Software at the time of login

    'Public Servername As String = "tcp:sql2k801.discountasp.net"         'Used for Servername for reports
    'Public DatabaseName As String = "SQL2008_671357_jjindustries"
    'Public DBUSERNAME As String = "SQL2008_671357_jjindustries_user"          'Used for Servername username for reports
    'Public Dbpassword As String = "infosys123"         ''Usedrr for Servername password for reports
    'Public Dbsecurity As Boolean = False

    '' -------NOTEPAD CODE --------
    'Public oFile As System.IO.File
    'Public oRead As System.IO.StreamReader = File.OpenText("C:\SERVERNAME.txt")
    'Public SERVERNAME As String = oRead.ReadToEnd
    ''' ----------------- NOTEPAD CODE---------

    ''Public Servername As String = "server\SQLEXPRESS"          'Used for Servername for reports'
    'Public DatabaseName As String = "SHAHTRADE"          'Used for Servername for reports'
    'Public DBUSERNAME As String = "sa"
    'Public Dbpassword As String = "Infosys@123"
    'Public Dbsecurity As Boolean = False

    'Public Servername As String = "GULKIT"          'Used for Servername for reports'
    'Public DatabaseName As String = "SHAHTRADE"          'Used for Servername for reports'
    'Public DBUSERNAME As String = ""
    'Public Dbpassword As String = ""
    'Public Dbsecurity As Boolean = True

    Public SERVERNAME As String
    Public DatabaseName As String
    Public DBUSERNAME As String             'Used for Servername username for reports
    Public Dbpassword As String         ''Used for Servername password for reports
    Public Dbsecurity As Boolean

    '******** FORM WISE VARIABLES ************
    '---CMPDETAILS---
    Public CmpName As String            'Used for CmpName throughout the software 
    Public CmpInitials As String        'Used for CmpInitials throughout the software 
    Public DBName As String             'Used for DBName throughout the software 
    Public CmpId As Integer             'Used for CmpId throughout the software
    Public YearId As Integer            'Used for YearId throughout the software
    Public AccFrom, AccTo As DateTime   'Used for A/C year start and end throughout the software
    Public Locationid As Integer        'Used for Locationid while login
    Public strsearch As String        'Used for strsearch 
    Public CMPSTATECODE As String      'Used for COMPANY'S STATE CODE

    '---LOGIN---
    Public Userid As Integer            'Used for Userid while login
    Public UserName As String               'User for UserName while Login
    Public DEPARTMENTNAME As String = ""             'User for DEPARTMENTNAME while Login
    Public GODOWNNAME As String = ""             'User for GODOWNNAME while Login
    Public USERRIGHTS As DataTable          'USED FOR USER RIGHTS THROUGHOUT THE APPLICATION 

    Public ClientName As String = ""
    Public REPORTTYPE As Boolean        'USED TO CHECK IF THE CLIENT WILL USINMG OUR DEFAULT FORMAT OR NOT
    Public MANUALINVOICE As Boolean
    Public SMSPARTY As Boolean
    Public PRINTDIRECT As Boolean
    Public CHQPRINTING As Boolean
    Public SHOWRATES As Boolean
    Public ALLOWEINVOICE As Boolean
    Public ALLOWEWAYBILL As Boolean
    Public PRINTEWAYBILL As Boolean
    Public ALLOWMANUALCNDN As Boolean
    Public CNDNA5 As Boolean

    Public CMPEINVOICECOUNTER As Integer    'Used for COMPANY'S EINVOICE COUNTER
    Public EINVOICEEXPDATE As Date          'Used for COMPANY'S EINVOICE EXPIRY DATE
    Public INVOICESCREENTYPE As String


    'THESE VARIABLES ARE USED FOR EWB AND GST
    Public CMPEWBUSER As String       'Used for COMPANY'S EWBUSER
    Public CMPEWBPASS As String       'Used for COMPANY'S EWBPASS
    Public CMPGSTIN As String       'Used for COMPANY'S GSTIN
    Public CMPPINCODE As String       'Used for COMPANY'S PINCODE
    Public CMPCITYNAME As String       'Used for COMPANY'S CITYNAME
    Public CMPSTATENAME As String       'Used for COMPANY'S STATE NAME
    Public CMPEWAYCOUNTER As Integer    'Used for COMPANY'S EWB COUNTER
    Public EWAYEXPDATE As Date          'Used for COMPANY'S EWB EXPIRY DATE


    Public Sub GETCONN()
        Try
            '-------NOTEPAD CODE --------

            Dim STARTPOS, ENDPOS As Integer
            Dim CONNSTR As String
            Dim oRead As System.IO.StreamReader = File.OpenText("C:\CONNECTIONSTRING.txt")
            CONNSTR = oRead.ReadToEnd

            STARTPOS = CONNSTR.IndexOf("=", 0)
            ENDPOS = CONNSTR.IndexOf(";", STARTPOS)
            SERVERNAME = CONNSTR.Substring(STARTPOS + 1, ENDPOS - STARTPOS - 1).Trim

            STARTPOS = CONNSTR.IndexOf("=", ENDPOS)
            ENDPOS = CONNSTR.IndexOf(";", STARTPOS)
            DatabaseName = CONNSTR.Substring(STARTPOS + 1, ENDPOS - STARTPOS - 1).Trim

            If CONNSTR.IndexOf("User ID", ENDPOS) > 0 Then
                STARTPOS = CONNSTR.IndexOf("=", ENDPOS)
                ENDPOS = CONNSTR.IndexOf(";", STARTPOS)
                DBUSERNAME = CONNSTR.Substring(STARTPOS + 1, ENDPOS - STARTPOS - 1).Trim

                STARTPOS = CONNSTR.IndexOf("=", ENDPOS)
                ENDPOS = CONNSTR.IndexOf(";", STARTPOS)
                Dbpassword = CONNSTR.Substring(STARTPOS + 1, ENDPOS - STARTPOS - 1).Trim

                Dbsecurity = False

            Else
                DBUSERNAME = ""
                Dbpassword = ""
                Dbsecurity = True
            End If

            '----------------- NOTEPAD CODE---------
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'CODE TO PROGRAMMATICALLY CREATE D. S. N.
    'Module CreateDSN

    'Private Declare Function SQLConfigDataSource Lib "ODBCCP32.DLL" (ByVal hwndParent As Integer, ByVal ByValfRequest As Integer, ByVal lpszDriver As String, ByVal lpszAttributes As String) As Integer
    'Private Const vbAPINull As Integer = 0 ' NULL Pointer
    'Private Const ODBC_ADD_DSN As Short = 1 ' Add data source

    'Public Sub CreateUserDSN(ByVal SERVERNAME As String, ByVal DSNNAME As String, ByVal DATABASENAME As String)
    '    Dim intRet As Integer
    '    Dim Driver As String
    '    Dim Attributes As String
    '    Dim sAttributes As New System.Text.StringBuilder

    '    'Set the driver to SQL Server because it is most common.
    '    Driver = "SQL Server"
    '    'Set the attributes delimited by null.
    '    'See driver documentation for a complete
    '    'list of supported attributes.
    '    Attributes = "SERVER=" & SERVERNAME & Chr(0)
    '    Attributes = Attributes & "DESCRIPTION=New DSN" & Chr(0)
    '    Attributes = Attributes & "DSN=" & DSNNAME & Chr(0)
    '    Attributes = Attributes & "DATABASE=" & DATABASENAME & Chr(0)
    '    'To show dialog, use Form1.Hwnd instead of vbAPINull.
    '    intRet = SQLConfigDataSource(vbAPINull, ODBC_ADD_DSN, Driver, Attributes)

    'End Sub


End Module
