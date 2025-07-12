Imports DB
Imports System.Data

Public Class ClsProgressBar

    Private objDBOperation As DBOperation
    Dim intResult As Integer
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

#Region "Functions"

    Public Function ExecSP() As DataTable
        Dim dt As DataTable

        Try

            Dim strCommand As String = "SP_GETMAXNO"
            Dim alParameter As New ArrayList
            With alParameter
                .Add(New SqlClient.SqlParameter("@fld1", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@TBname", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@whereclause", alParaval(2)))
            End With
            dt = objDBOperation.execute(strCommand, alParameter).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function CREATESTATE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATESTATE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#Region "GENERAL"

    Public Function CREATEDEPARTMENT() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEDEPARTMENT"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEUSER() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEUSER"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEPIECETYPE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEPIECETYPE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEPROCESS() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEPROCESS"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEUNIT() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEUNIT"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEPARAMETER() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEPARAMETER"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEGODOWN() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEGODOWN"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region

    'Public Function UseDatabase() As Integer
    '    Dim dtTable As New DataTable
    '    Dim intResult As Integer

    '    Try

    '        Dim strCommand As String = "USE [Master]"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return intResult
    'End Function

    'Public Function CreateTables(ByVal dbname As String) As Integer

    '    Try

    '        Dim dtTable As New DataTable
    '        Dim intResult As Integer


    '        '*********** FOR GRN **************
    '        'create GRN
    '        Dim strCommand As String = " use [" & dbname & "]  CREATE TABLE GRN (grn_no int PRIMARY KEY, grn_ledgerid int NULL,	grn_date datetime NULL, grn_pono varchar(50) NULL, grn_podate datetime NULL, grn_challanno varchar(50) NULL, grn_challandt datetime NULL, grn_invoiceno varchar(50) NULL, grn_invoicedt datetime NULL, grn_refno varchar(50) NULL, grn_remarks text NULL, grn_terms text NULL, grn_transledgerid int NULL, grn_lrno varchar(50) NULL, grn_lrdate datetime NULL, grn_transrefno varchar(50) NULL, grn_transremarks text NULL, grn_cmpid int NULL, grn_locationid int NULL, grn_userid int NULL, grn_yearid int NULL, grn_transfer bit NULL, grn_created datetime NULL)"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create GRN_GRIDSET
    '        strCommand = " use [" & dbname & "]  CREATE TABLE GRN_GRIDSET (sr_no int NULL, gridsrno int NULL, setid int NULL, itemid int NULL, gradeid int NULL, cmpid int NULL, locationid int NULL, userid int NULL, yearid int NULL, transfer bit NULL, created datetime NULL)"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create GRN_ITEMDESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE GRN_ITEMDESC (grn_no int NULL, grn_srno int NULL, grn_type varchar(10) NULL, grn_gradeid int NULL, grn_itemid int NULL, grn_setid int NULL, grn_heatno varchar(50) NULL, grn_remarks text NULL, grn_make varchar(50) NULL, grn_sizeid int NULL, grn_length float NULL, grn_unitid int NULL, grn_qty float NULL, grn_qtyunitid int NULL, grn_pcs float NULL, grn_rate float NULL, grn_amt float NULL, grn_cmpid int NULL, grn_locationid int NULL, grn_userid int NULL, grn_yearid int NULL, grn_transfer bit NULL, grn_testing bit NULL, grn_passfail smallint NULL, grn_pogridsrno int NULL)"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create GRN_UPLOADDESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE GRN_UPLOADDESC (grn_no int NULL, grn_srno int NULL, grn_imgpath text NULL, grn_remarks text NULL, grn_name varchar(200) NULL, grn_cmpid int NULL, grn_locationid int NULL, grn_userid int NULL, grn_yearid int NULL, grn_transfer bit NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************


    '        '*********** FOR GRNTEST **************
    '        'create GRNTEST
    '        strCommand = " use [" & dbname & "]  CREATE TABLE GRNTEST (grntest_no int PRIMARY KEY, grntest_date datetime NULL, grntest_labref varchar(50) NULL, grntest_labdate datetime NULL, grntest_grade1id int NULL, Grntest_vendorid int NULL, grntest_itemid int NULL, grntest_challanno varchar(50) NULL, 	grntest_invno varchar(50) NULL, 	grntest_pono int NULL, 	grntest_podate datetime NULL, 	grntest_grnno int NULL, 	grntest_remarks text NULL, 	grntest_cmpid int NULL, grntest_locationid int NULL, grntest_userid int NULL, grntest_yearid int NULL, grntest_transfer bit NULL, grntest_created datetime NULL)"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create GRNTEST_DESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE GRNTEST_DESC (grntest_no int NULL, grntest_testingid int NULL, grntest_parameterid int NULL, grntest_start varchar(50)  NULL, grntest_end varchar(50)  NULL, grntest_unitid int NULL, grntest_vendorreport varchar(50)  NULL, grntest_userreport varchar(50)  NULL, grntest_witnessreport varchar(50)  NULL, grntest_gridsrno float NULL, grntest_status varchar(20)  NULL, grntest_sizeid int NULL, grntest_length float NULL, 	grntest_sizeunitid int NULL, grntest_heatno varchar(20)  NULL,  grntest_gradeid int NULL, grntest_suppno varchar(50)  NULL, grntest_labno varchar(50)  NULL, grntest_witnessno varchar(50)  NULL, grntest_cmpid int NULL, grntest_locationid int NULL, grntest_userid int NULL, grntest_yearid int NULL, grntest_transfer bit NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create TEMP_GRNTEST_DESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE TEMP_GRNTEST_DESC (grntest_no int NULL, grntest_testingid int NULL, grntest_parameterid int NULL, grntest_start varchar(50)  NULL, grntest_end varchar(50)  NULL, grntest_unitid int NULL, grntest_vendorreport varchar(50)  NULL, grntest_userreport varchar(50)  NULL, grntest_gridsrno float NULL, grntest_status varchar(20)  NULL, grntest_sizeid int NULL, grntest_length float NULL, grntest_sizeunitid int NULL, grntest_heatno varchar(20)  NULL, grntest_gradeid int NULL, grntest_cmpid int NULL, grntest_locationid int NULL, grntest_userid int NULL, grntest_yearid int NULL, grntest_transfer bit NULL) "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************


    '        '*********** FOR PURCHASRORDER **************
    '        'create PURCHASEORDER
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEORDER (po_no int PRIMARY KEY, po_date datetime NULL, po_ledgerid int NULL, po_quot_no int NULL, po_quot_date datetime NULL, po_quot_ourref varchar(50)  NULL, po_quot_partyref varchar(50)  NULL, po_totalqty float NULL, po_totalamt float NULL, po_remarks text  NULL, po_terms text  NULL, po_cmpid int NULL, po_locationid int NULL, po_userid int NULL, po_yearid int NULL, po_transfer bit NULL, po_created datetime NULL, po_done bit NULL, po_quot_partyrefdate datetime NULL, po_amendno varchar(20)  NULL, po_amd_done bit NULL)"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEORDER_DESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEORDER_DESC (po_no int NULL, po_gridsrno int NULL, po_type varchar(50)  NULL, po_gradeid int NULL, po_itemid int NULL, po_setid int NULL, po_gridremarks text  NULL, po_make varchar(50)  NULL, po_sizeid int NULL, po_length float NULL, po_unitid int NULL, po_qty float NULL, po_qtyunitid int NULL, Po_pcs float NULL, po_rate float NULL, po_amt float NULL, po_preqno int NULL, po_prgridsrno int NULL, po_cmpid int NULL, po_locationid int NULL, po_userid int NULL, po_yearid int NULL, po_transfer bit NULL, po_recdqty float NULL)  "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEORDER_GRIDSET
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEORDER_GRIDSET (sr_no int NULL, gridsrno int NULL, setid int NULL, itemid int NULL, gradeid int NULL, cmpid int NULL, locationid int NULL, userid int NULL, yearid int NULL, transfer bit NULL, created datetime NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************


    '        '*********** FOR PURCHASEQUOTATION **************
    '        'create PURCHASEQUOTATION
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEQUOTATION (quotation_no int PRIMARY KEY, quotation_ledgerid int NULL, quotation_date datetime NULL, quotation_ourref varchar(100)  NULL, quotation_refno varchar(100)  NULL, quotation_imgpath text  NULL, quotation_remarks text  NULL, quotation_cmpid int NULL, quotation_locationid int NULL, quotation_userid int NULL, quotation_yearid int NULL, quotation_transfer bit NULL, quotation_created datetime NULL, quotation_done bit NULL, quotation_partyrefdate datetime NULL) "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEQUOTATION_DESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEQUOTATION_DESC (quotation_no int NULL, quotation_srno int NULL, quotation_type varchar(10)  NULL, quotation_gradeid int NULL, quotation_itemid int NULL, quotation_setid int NULL, quotation_remarks text  NULL, quotation_make varchar(50)  NULL, quotation_sizeid int NULL, quotation_lenth float NULL, quotation_unitid int NULL, quotation_qty float NULL, quotation_qtyunitid int NULL, quotation_pcs float NULL, quotation_rate float NULL, quotation_amt float NULL, quotation_preqno int NULL, quotation_prgridsrno int NULL, quotation_cmpid int NULL, quotation_locationid int NULL,  quotation_userid int NULL, quotation_yearid int NULL, quotation_transfer bit NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEQUOTATION_GRIDSET   
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEQUOTATION_GRIDSET (sr_no int NULL, gridsrno int NULL, setid int NULL, itemid int NULL, gradeid int NULL, cmpid int NULL, locationid int NULL, userid int NULL, yearid int NULL, transfer bit NULL, created datetime NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************


    '        '*********** FOR PURCHASEREQUEST **************
    '        'create PURCHASEREQUEST   
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEREQUEST (PREQ_SRNO int PRIMARY KEY, PREQ_DATE datetime NULL, PREQ_REQUESTBY varchar(150)  NULL, PREQ_REF varchar(50)  NULL, PREQ_TOTALQTY float NULL, PREQ_REMARKS varchar(1000)  NULL, PREQ_CMPID int NULL, PREQ_LOCATIONID int NULL, PREQ_USERID int NULL, PREQ_YEARID int NULL, PREQ_TRANSFER bit NULL, PREQ_CREATED datetime NULL ) "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEREQUEST_DESC
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEREQUEST_DESC (preqdesc_srno int NULL, preqdesc_gridsrno int NULL, preqdesc_type varchar(50)  NULL, preqdesc_gradeid int NULL, preqdesc_itemid int NULL, preqdesc_setid int NULL, preqdesc_gridremarks varchar(500)  NULL, preqdesc_gridmake varchar(300)  NULL, preqdesc_sizeid int NULL, preqdesc_length float NULL, preqdesc_unitid int NULL, preqdesc_qty float NULL, preqdesc_qtyunitid int NULL, preqdesc_pcs float NULL, preqdesc_cmpid int NULL, preqdesc_locationid int NULL, preqdesc_userid int NULL, preqdesc_yearid int NULL, preqdesc_transfer bit NULL, preqdesc_done bit NULL) "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create PURCHASEREQUEST_GRIDSET
    '        strCommand = " use [" & dbname & "]  CREATE TABLE PURCHASEREQUEST_GRIDSET (sr_no int NULL, gridsrno int NULL, setid int NULL, itemid int NULL, gradeid int NULL, cmpid int NULL, locationid int NULL, userid int NULL, 	yearid int NULL, transfer bit NULL, created datetime NULL) "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************


    '        '*********** FOR TEMP TABLES **************
    '        'create PURCHASEREQUEST_GRIDSET
    '        strCommand = " use [" & dbname & "]  CREATE TABLE TEMPTABLE_GRIDSET (Id int NULL, tablename varchar(100)  NULL, sr_no int NULL, gridsrno int NULL, setid int NULL, itemid int NULL, gradeid int NULL, cmpid int NULL, locationid int NULL, userid int NULL, yearid int NULL, transfer bit NULL, created datetime NULL )"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************
    '        Return intResult

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function CreateViews(ByVal dbname As String) As Integer

    '    Try
    '        Dim dtTable As New DataTable
    '        Dim intResult As Integer

    '        '*********** FOR APPROVED STOCK **************
    '        'create Approved_Item
    '        Dim strCommand As String = " CREATE VIEW Approved_Item AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, ItemMaster.item_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.],  UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN.grn_no FROM MaterialTypeMaster RIGHT OUTER JOIN ItemMaster ON MaterialTypeMaster.material_cmpid = ItemMaster.item_cmpid AND MaterialTypeMaster.material_id = ItemMaster.item_materialtypeid RIGHT OUTER JOIN SizeMaster RIGHT OUTER JOIN dbo.GRN INNER JOIN dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id ON SizeMaster.size_id = dbo.GRN_ITEMDESC.grn_sizeid AND SizeMaster.size_cmpid = dbo.GRN_ITEMDESC.grn_cmpid ON ItemMaster.item_id = dbo.GRN_ITEMDESC.grn_itemid AND ItemMaster.item_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 1) AND (dbo.GRN_ITEMDESC.grn_setid = 0) GROUP BY GradeMaster.grade_name, ItemMaster.item_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create Approved_SET
    '        strCommand = " CREATE VIEW Approved_Set AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, SetMaster.set_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.], UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN.grn_no FROM SizeMaster RIGHT OUTER JOIN dbo.GRN INNER JOIN Dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id LEFT OUTER JOIN MaterialTypeMaster RIGHT OUTER JOIN SetMaster ON MaterialTypeMaster.material_cmpid = SetMaster.set_cmpid AND MaterialTypeMaster.material_id = SetMaster.set_materialid ON dbo.GRN_ITEMDESC.grn_setid = SetMaster.set_id AND dbo.GRN_ITEMDESC.grn_cmpid = SetMaster.set_cmpid ON SizeMaster.size_id = dbo.GRN_ITEMDESC.grn_sizeid AND SizeMaster.size_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 1) AND (dbo.GRN_ITEMDESC.grn_itemid = 0) GROUP BY GradeMaster.grade_name, SetMaster.set_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create Approved_STOCKS
    '        strCommand = " CREATE VIEW Approved_Stocks AS SELECT grn_no AS [GRN No.], [Material Type], Grade, [Item / Set Name], [Heat No], Size, Length, Unit, [Total Qty.], [Qty Unit], grn_cmpid, cmp_displayedname, cmp_add1, cmp_add2 FROM dbo.Approved_Item UNION ALL SELECT grn_no AS [GRN No.], [Material Type] AS Expr1, Grade AS Expr2, [Item / Set Name] AS Expr3, [Heat No] AS Expr4, Size AS Expr5, Length AS Expr6, Unit AS Expr7, [Total Qty.] AS Expr8, [Qty Unit] AS Expr9, grn_cmpid AS Expr10, cmp_displayedname AS Expr11, cmp_add1 AS Expr12, cmp_add2 AS Expr13 FROM dbo.Approved_Set "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR ON APPROVAL STOCK **************
    '        'create ON_APPROVAL_ITEM
    '        strCommand = " CREATE VIEW On_Approval_Item AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, ItemMaster.item_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.], UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN.grn_no FROM MaterialTypeMaster RIGHT OUTER JOIN ItemMaster ON MaterialTypeMaster.material_cmpid = ItemMaster.item_cmpid AND MaterialTypeMaster.material_id = ItemMaster.item_materialtypeid RIGHT OUTER JOIN SizeMaster RIGHT OUTER JOIN dbo.GRN INNER JOIN dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id ON SizeMaster.size_id = dbo.GRN_ITEMDESC.grn_sizeid AND SizeMaster.size_cmpid = dbo.GRN_ITEMDESC.grn_cmpid ON ItemMaster.item_id = dbo.GRN_ITEMDESC.grn_itemid AND ItemMaster.item_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 0) AND (dbo.GRN_ITEMDESC.grn_setid = 0) AND (MaterialTypeMaster.material_name = 'Finished Goods' Or MaterialTypeMaster.material_name = 'Semi Finished Goods' OR MaterialTypeMaster.material_name = 'Raw Material') GROUP BY GradeMaster.grade_name, ItemMaster.item_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create ON_APPROVAL_SET
    '        strCommand = " CREATE VIEW On_Approval_Set AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, SetMaster.set_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.], UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_no FROM SizeMaster RIGHT OUTER JOIN dbo.GRN INNER JOIN dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id LEFT OUTER JOIN MaterialTypeMaster RIGHT OUTER JOIN SetMaster ON MaterialTypeMaster.material_cmpid = SetMaster.set_cmpid AND MaterialTypeMaster.material_id = SetMaster.set_materialid ON dbo.GRN_ITEMDESC.grn_setid = SetMaster.set_id AND dbo.GRN_ITEMDESC.grn_cmpid = SetMaster.set_cmpid ON SizeMaster.size_id = dbo.GRN_ITEMDESC.grn_sizeid AND SizeMaster.size_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 0) AND (dbo.GRN_ITEMDESC.grn_itemid = 0) GROUP BY GradeMaster.grade_name, SetMaster.set_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN_ITEMDESC.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create ON_APPROVAL_STOCK
    '        strCommand = " CREATE VIEW On_Approval_Stock AS SELECT grn_no AS [GRN No.], [Material Type], Grade, [Item / Set Name], [Heat No], Size, Length, Unit, [Total Qty.], [Qty Unit], grn_cmpid, cmp_displayedname, cmp_add1, cmp_add2 FROM dbo.On_Approval_Item UNION ALL SELECT grn_no AS [GRN No.], [Material Type] AS Expr1, Grade AS Expr2, [Item / Set Name] AS Expr3, [Heat No] AS Expr4, Size AS Expr5, Length AS Expr6, Unit AS Expr7, [Total Qty.] AS Expr8, [Qty Unit] AS Expr9, grn_cmpid AS Expr10, cmp_displayedname AS Expr11, cmp_add1 AS Expr12, cmp_add2 AS Expr13 FROM dbo.On_Approval_Set "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR REJECTED STOCK **************
    '        'create REJECTED_ITEM
    '        strCommand = " CREATE VIEW Rejected_Item AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, ItemMaster.item_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.], UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN.grn_no FROM MaterialTypeMaster RIGHT OUTER JOIN ItemMaster ON MaterialTypeMaster.material_cmpid = ItemMaster.item_cmpid AND MaterialTypeMaster.material_id = ItemMaster.item_materialtypeid RIGHT OUTER JOIN SizeMaster RIGHT OUTER JOIN dbo.GRN INNER JOIN dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id ON SizeMaster.size_id = dbo.GRN_ITEMDESC.grn_sizeid AND SizeMaster.size_cmpid = dbo.GRN_ITEMDESC.grn_cmpid ON ItemMaster.item_id = dbo.GRN_ITEMDESC.grn_itemid AND ItemMaster.item_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 2) AND (dbo.GRN_ITEMDESC.grn_setid = 0) GROUP BY GradeMaster.grade_name, ItemMaster.item_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create REJECTED_SET
    '        strCommand = " CREATE VIEW Rejected_Set AS SELECT MaterialTypeMaster.material_name AS [Material Type], GradeMaster.grade_name AS Grade, SetMaster.set_name AS [Item / Set Name], dbo.GRN_ITEMDESC.grn_heatno AS [Heat No], SizeMaster.size_alias AS Size, dbo.GRN_ITEMDESC.grn_length AS Length, UnitMaster.unit_abbr AS Unit, SUM(dbo.GRN_ITEMDESC.grn_qty) AS [Total Qty.], UnitMaster_1.unit_abbr AS [Qty Unit], dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN.grn_no FROM MaterialTypeMaster RIGHT OUTER JOIN SetMaster ON MaterialTypeMaster.material_cmpid = SetMaster.set_cmpid AND MaterialTypeMaster.material_id = SetMaster.set_materialid RIGHT OUTER JOIN dbo.GRN INNER JOIN dbo.GRN_ITEMDESC ON dbo.GRN.grn_no = dbo.GRN_ITEMDESC.grn_no LEFT OUTER JOIN CmpMaster ON dbo.GRN.grn_cmpid = CmpMaster.cmp_id ON SetMaster.set_id = dbo.GRN_ITEMDESC.grn_setid AND SetMaster.set_cmpid = dbo.GRN_ITEMDESC.grn_cmpid LEFT OUTER JOIN SizeMaster ON dbo.GRN_ITEMDESC.grn_sizeid = SizeMaster.size_id AND dbo.GRN_ITEMDESC.grn_cmpid = SizeMaster.size_cmpid LEFT OUTER JOIN GradeMaster ON dbo.GRN_ITEMDESC.grn_gradeid = GradeMaster.grade_id AND dbo.GRN_ITEMDESC.grn_cmpid = GradeMaster.grade_cmpid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster.unit_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_qtyunitid = UnitMaster_1.unit_id LEFT OUTER JOIN VendorMaster ON dbo.GRN.grn_locationid = VendorMaster.Vendor_locationid AND dbo.GRN.grn_cmpid = VendorMaster.Vendor_cmpid And dbo.GRN.grn_ledgerid = VendorMaster.Vendor_id WHERE (dbo.GRN_ITEMDESC.grn_passfail = 2) AND (dbo.GRN_ITEMDESC.grn_itemid = 0) GROUP BY GradeMaster.grade_name, SetMaster.set_name, SizeMaster.size_alias, UnitMaster.unit_abbr, dbo.GRN_ITEMDESC.grn_length, dbo.GRN_ITEMDESC.grn_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, UnitMaster_1.unit_abbr, MaterialTypeMaster.material_name, dbo.GRN.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create REJECTED_STOCK
    '        strCommand = " CREATE VIEW Rejected_Stocks AS SELECT grn_no AS [GRN No.], [Material Type], Grade, [Item / Set Name], [Heat No], Size, Length, Unit, [Total Qty.], [Qty Unit], grn_cmpid, cmp_displayedname, cmp_add1, cmp_add2 FROM dbo.Rejected_Item UNION ALL SELECT grn_no AS [GRN No.], [Material Type] AS Expr1, Grade AS Expr2, [Item / Set Name] AS Expr3, [Heat No] AS Expr4, Size AS Expr5, Length AS Expr6, Unit AS Expr7, [Total Qty.] AS Expr8, [Qty Unit] AS Expr9, grn_cmpid AS Expr10, cmp_displayedname AS Expr11, cmp_add1 AS Expr12, cmp_add2 AS Expr13 FROM dbo.Rejected_Set "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR GRN DETAILS **************
    '        'create V_GRN_DTLS
    '        strCommand = " CREATE VIEW [dbo].[v_GRN_dtls] AS SELECT dbo.GRN.grn_no, VendorMaster_1.Vendor_cmpname, dbo.GRN.grn_date, dbo.GRN.grn_pono, dbo.GRN.grn_podate, dbo.GRN.grn_challanno, dbo.GRN.grn_invoiceno, dbo.GRN.grn_refno, dbo.GRN.grn_remarks, dbo.GRN.grn_terms, VendorMaster_1.Vendor_cmpname AS Expr1, dbo.GRN.grn_lrno, dbo.GRN.grn_lrdate, dbo.GRN.grn_transrefno, dbo.GRN.grn_transremarks, dbo.GRN_ITEMDESC.grn_srno, dbo.GRN_ITEMDESC.grn_type, GradeMaster.grade_name, COALESCE (ItemMaster.item_name, SetMaster.set_name) AS [Item Set Name], dbo.GRN_ITEMDESC.grn_remarks AS grn_remark, dbo.GRN_ITEMDESC.grn_make, SizeMaster.size_alias, dbo.GRN_ITEMDESC.grn_length, UnitMaster_1.unit_abbr, dbo.GRN_ITEMDESC.grn_qty, UnitMaster_1.unit_abbr AS UnitQty, dbo.GRN_ITEMDESC.grn_pcs, dbo.GRN_ITEMDESC.grn_rate, dbo.GRN_ITEMDESC.grn_amt, dbo.GRN_ITEMDESC.grn_testing, dbo.GRN_UPLOADDESC.grn_srno AS Expr3, dbo.GRN_UPLOADDESC.grn_imgpath, dbo.GRN_UPLOADDESC.grn_remarks AS Expr4, dbo.GRN_UPLOADDESC.grn_name, dbo.GRN.grn_challandt, dbo.GRN.grn_invoicedt, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, dbo.GRN_ITEMDESC.grn_heatno, CmpMaster.cmp_invinitials FROM GradeMaster RIGHT OUTER JOIN UnitMaster AS UnitMaster_1 RIGHT OUTER JOIN dbo.GRN_ITEMDESC ON UnitMaster_1.unit_cmpid = dbo.GRN_ITEMDESC.grn_cmpid AND UnitMaster_1.unit_id = dbo.GRN_ITEMDESC.grn_qtyunitid LEFT OUTER JOIN UnitMaster ON dbo.GRN_ITEMDESC.grn_cmpid = UnitMaster_1.unit_cmpid AND dbo.GRN_ITEMDESC.grn_unitid = UnitMaster_1.unit_id LEFT OUTER JOIN SizeMaster ON dbo.GRN_ITEMDESC.grn_cmpid = SizeMaster.size_cmpid AND dbo.GRN_ITEMDESC.grn_sizeid = SizeMaster.size_id LEFT OUTER JOIN SetMaster ON dbo.GRN_ITEMDESC.grn_cmpid = SetMaster.set_cmpid AND dbo.GRN_ITEMDESC.grn_setid = SetMaster.set_id LEFT OUTER JOIN ItemMaster ON dbo.GRN_ITEMDESC.grn_cmpid = ItemMaster.item_cmpid AND dbo.GRN_ITEMDESC.grn_itemid = ItemMaster.item_id ON GradeMaster.grade_cmpid = dbo.GRN_ITEMDESC.grn_cmpid AND GradeMaster.grade_id = dbo.GRN_ITEMDESC.grn_gradeid RIGHT OUTER JOIN VendorMaster AS VendorMaster_1 RIGHT OUTER JOIN CmpMaster INNER JOIN dbo.GRN ON CmpMaster.cmp_id = dbo.GRN.grn_cmpid ON VendorMaster_1.Vendor_cmpid = dbo.GRN.grn_cmpid AND VendorMaster_1.Vendor_id = dbo.GRN.grn_ledgerid ON dbo.GRN_ITEMDESC.grn_no = dbo.GRN.grn_no LEFT OUTER JOIN dbo.GRN_UPLOADDESC ON dbo.GRN.grn_no = dbo.GRN_UPLOADDESC.grn_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR PO DETAILS **************
    '        'create V_PO_DTLS
    '        strCommand = " CREATE VIEW v_POrder_dtls_report AS SELECT dbo.PURCHASEORDER.po_no, dbo.PURCHASEORDER.po_date, VendorMaster.Vendor_cmpname, dbo.PURCHASEORDER.po_quot_no, dbo.PURCHASEORDER.po_quot_date, dbo.PURCHASEORDER.po_quot_ourref, dbo.PURCHASEORDER.po_quot_partyref, dbo.PURCHASEORDER.po_totalqty, dbo.PURCHASEORDER.po_totalamt, dbo.PURCHASEORDER.po_remarks, dbo.PURCHASEORDER.po_terms, dbo.PURCHASEORDER.po_done, dbo.PURCHASEORDER_DESC.po_gridsrno, dbo.PURCHASEORDER_DESC.po_type, GradeMaster.grade_name, ItemMaster.item_name, SetMaster.set_name, dbo.PURCHASEORDER_DESC.po_gridremarks, dbo.PURCHASEORDER_DESC.po_make, SizeMaster.size_alias, dbo.PURCHASEORDER_DESC.po_length, UnitMaster.unit_abbr, dbo.PURCHASEORDER_DESC.po_qty, dbo.PURCHASEORDER_DESC.po_rate, dbo.PURCHASEORDER_DESC.po_amt, dbo.PURCHASEORDER.po_cmpid, dbo.PURCHASEORDER_DESC.po_preqno, CmpMaster.cmp_name, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, UnitMaster_1.unit_abbr AS Expr1, dbo.PURCHASEORDER_DESC.po_pcs, CmpMaster.cmp_invinitials FROM dbo.PURCHASEORDER INNER JOIN dbo.PURCHASEORDER_DESC ON dbo.PURCHASEORDER.po_no = dbo.PURCHASEORDER_DESC.po_no AND dbo.PURCHASEORDER.po_cmpid = dbo.PURCHASEORDER_DESC.po_cmpid LEFT OUTER JOIN UnitMaster ON UnitMaster.unit_id = dbo.PURCHASEORDER_DESC.po_unitid LEFT OUTER JOIN SetMaster ON SetMaster.set_id = dbo.PURCHASEORDER_DESC.po_setid LEFT OUTER JOIN GradeMaster ON GradeMaster.grade_id = dbo.PURCHASEORDER_DESC.po_gradeid LEFT OUTER JOIN SizeMaster ON SizeMaster.size_id = dbo.PURCHASEORDER_DESC.po_sizeid LEFT OUTER JOIN ItemMaster ON ItemMaster.item_id = dbo.PURCHASEORDER_DESC.po_itemid LEFT OUTER JOIN VendorMaster ON VendorMaster.Vendor_id = dbo.PURCHASEORDER.po_ledgerid INNER JOIN CmpMaster ON CmpMaster.cmp_id = dbo.PURCHASEORDER.po_cmpid LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.PURCHASEORDER_DESC.po_qtyunitid = UnitMaster_1.unit_id AND dbo.PURCHASEORDER_DESC.po_cmpid = UnitMaster_1.unit_cmpid "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR PQUO DETAILS **************
    '        'create V_PQUO_DTLS
    '        strCommand = " CREATE VIEW v_PQuot_dtls_report AS SELECT  TOP (100) PERCENT dbo.PURCHASEQUOTATION.quotation_no, VendorMaster.Vendor_cmpname, dbo.PURCHASEQUOTATION.quotation_date, dbo.PURCHASEQUOTATION.quotation_ourref, dbo.PURCHASEQUOTATION.quotation_refno, dbo.PURCHASEQUOTATION.quotation_imgpath, dbo.PURCHASEQUOTATION.quotation_remarks AS quot_remark, dbo.PURCHASEQUOTATION_DESC.quotation_srno, dbo.PURCHASEQUOTATION_DESC.quotation_type, GradeMaster.grade_name, COALESCE (ItemMaster.item_name, SetMaster.set_name) AS [Item Name], dbo.PURCHASEQUOTATION_DESC.quotation_remarks, dbo.PURCHASEQUOTATION_DESC.quotation_make, SizeMaster.size_alias, dbo.PURCHASEQUOTATION_DESC.quotation_lenth, UnitMaster_1.unit_abbr, dbo.PURCHASEQUOTATION_DESC.quotation_qty, UnitMaster.unit_abbr AS Expr1, dbo.PURCHASEQUOTATION_DESC.quotation_pcs, dbo.PURCHASEQUOTATION_DESC.quotation_rate, dbo.PURCHASEQUOTATION_DESC.quotation_amt, dbo.PURCHASEQUOTATION_DESC.quotation_preqno, dbo.PURCHASEQUOTATION_DESC.quotation_prgridsrno, dbo.PURCHASEQUOTATION.quotation_done, dbo.PURCHASEQUOTATION.quotation_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, CmpMaster.cmp_invinitials FROM dbo.PURCHASEQUOTATION_DESC INNER JOIN dbo.PURCHASEQUOTATION ON dbo.PURCHASEQUOTATION_DESC.quotation_no = dbo.PURCHASEQUOTATION.quotation_no LEFT OUTER JOIN UnitMaster ON dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = UnitMaster.unit_cmpid AND dbo.PURCHASEQUOTATION_DESC.quotation_qtyunitid = UnitMaster.unit_id LEFT OUTER JOIN CmpMaster ON dbo.PURCHASEQUOTATION.quotation_cmpid = CmpMaster.cmp_id LEFT OUTER JOIN VendorMaster ON dbo.PURCHASEQUOTATION.quotation_cmpid = VendorMaster.Vendor_cmpid AND dbo.PURCHASEQUOTATION.quotation_ledgerid = VendorMaster.Vendor_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = UnitMaster_1.unit_cmpid AND dbo.PURCHASEQUOTATION_DESC.quotation_unitid = UnitMaster_1.unit_id LEFT OUTER JOIN SizeMaster ON dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = SizeMaster.size_cmpid AND dbo.PURCHASEQUOTATION_DESC.quotation_sizeid = SizeMaster.size_id LEFT OUTER JOIN SetMaster ON dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = SetMaster.set_cmpid AND dbo.PURCHASEQUOTATION_DESC.quotation_setid = SetMaster.set_id LEFT OUTER JOIN ItemMaster ON dbo.PURCHASEQUOTATION_DESC.quotation_itemid = ItemMaster.item_id AND dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = ItemMaster.item_cmpid LEFT OUTER JOIN GradeMaster ON dbo.PURCHASEQUOTATION_DESC.quotation_gradeid = GradeMaster.grade_id AND dbo.PURCHASEQUOTATION_DESC.quotation_cmpid = GradeMaster.grade_cmpid ORDER BY dbo.PURCHASEQUOTATION.quotation_no "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR PREQUEST DETAILS **************
    '        'create V_PQUO_DTLS
    '        strCommand = " CREATE VIEW v_PRequisition_report AS SELECT dbo.PURCHASEREQUEST.PREQ_SRNO, dbo.PURCHASEREQUEST.PREQ_DATE, dbo.PURCHASEREQUEST.PREQ_REQUESTBY, dbo.PURCHASEREQUEST.PREQ_REF, dbo.PURCHASEREQUEST.PREQ_TOTALQTY, dbo.PURCHASEREQUEST_DESC.preqdesc_gridsrno, dbo.PURCHASEREQUEST_DESC.preqdesc_type, GradeMaster.grade_name, ItemMaster.item_name, SetMaster.set_name, SizeMaster.size_alias, dbo.PURCHASEREQUEST_DESC.preqdesc_length, UnitMaster_1.unit_abbr, dbo.PURCHASEREQUEST_DESC.preqdesc_qty, dbo.PURCHASEREQUEST_DESC.preqdesc_gridremarks, dbo.PURCHASEREQUEST.PREQ_REMARKS, dbo.PURCHASEREQUEST_DESC.preqdesc_gridmake, dbo.PURCHASEREQUEST_DESC.preqdesc_done, dbo.PURCHASEREQUEST_DESC.preqdesc_cmpid, CmpMaster.cmp_displayedname, CmpMaster.cmp_add1, CmpMaster.cmp_add2, UnitMaster.unit_abbr AS UnitQty, dbo.PURCHASEREQUEST_DESC.preqdesc_pcs AS Pcs, CmpMaster.cmp_invinitials FROM dbo.PURCHASEREQUEST INNER JOIN dbo.PURCHASEREQUEST_DESC ON dbo.PURCHASEREQUEST.PREQ_SRNO = dbo.PURCHASEREQUEST_DESC.preqdesc_srno AND dbo.PURCHASEREQUEST.PREQ_CMPID = dbo.PURCHASEREQUEST_DESC.preqdesc_cmpid LEFT OUTER JOIN UnitMaster ON dbo.PURCHASEREQUEST_DESC.preqdesc_qtyunitid = UnitMaster.unit_id AND dbo.PURCHASEREQUEST_DESC.preqdesc_cmpid = UnitMaster.unit_cmpid LEFT OUTER JOIN CmpMaster ON dbo.PURCHASEREQUEST.PREQ_CMPID = CmpMaster.cmp_id LEFT OUTER JOIN UnitMaster AS UnitMaster_1 ON dbo.PURCHASEREQUEST_DESC.preqdesc_unitid = UnitMaster_1.unit_id LEFT OUTER JOIN SetMaster ON SetMaster.set_id = dbo.PURCHASEREQUEST_DESC.preqdesc_setid LEFT OUTER JOIN GradeMaster ON GradeMaster.grade_id = dbo.PURCHASEREQUEST_DESC.preqdesc_gradeid LEFT OUTER JOIN SizeMaster ON SizeMaster.size_id = dbo.PURCHASEREQUEST_DESC.preqdesc_sizeid LEFT OUTER JOIN ItemMaster ON ItemMaster.item_id = dbo.PURCHASEREQUEST_DESC.preqdesc_itemid"
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"

    '        Return intResult

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function CreateSP(ByVal dbname As String) As Integer

    '    Try
    '        Dim dtTable As New DataTable
    '        Dim intResult As Integer


    '        '*********** FOR GRIDSET **************
    '        'create SP_DELETETEMP_GRIDSET_DELETE
    '        Dim strCommand As String = " CREATE PROCEDURE SP_DELETETEMP_GRIDSET_DELETE (@temptableid INTEGER	,@Cmpid INTEGER ) AS BEGIN DELETE FROM temptable_gridset WHERE ID=@temptableid AND cmpid=@Cmpid	END "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create SP_FILL_TEMP_GRIDSET_EDIT_MODE
    '        strCommand = " CREATE procedure SP_FILL_TEMP_GRIDSET_EDIT_MODE (@TABLENAME varchar(500),@fld2 varchar(5000)) AS DECLARE @temptableno int SELECT @temptableno = isnull(MAX(ID), 0) + 1 FROM TEMPTABLE_GRIDSET select @temptableno begin EXEC('insert into TEMPTABLE_GRIDSET  select '+ @temptableno +','+ @fld2 ) end "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create SP_FILL_TEMP_GRIDSET_FOR_PQ
    '        strCommand = " CREATE procedure SP_FILL_TEMP_GRIDSET_FOR_PQ (@TABLENAME varchar(500),@fld2 varchar(5000)) AS begin EXEC('insert into TEMPTABLE_GRIDSET select '+ @fld2 )end "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create SP_GRIDSET_SAVE_TEMPTABLE
    '        strCommand = " CREATE PROCEDURE SP_GRIDSET_SAVE_TEMPTABLE (@temptableid varchar(100), @tablename varchar(100), @formsrno varchar(4000), @gridsrno varchar(4000), @setid varchar(4000), @ITEMNAME varchar(4000), @GRADENAME varchar(4000), @cmpid int, @locationid int, @userid int, @yearid int, @transfer bit, @itemid varchar(4000)) AS declare @gridsrno_del varchar(4000),@spgridsrno_del int,@strgridsrno_del varchar(4000) set @gridsrno_del=@gridsrno declare @formno_del varchar(4000),@spformno_del int,@strformno_del varchar(4000) set @formno_del=@formsrno declare @temptableno int declare @spgridsrno int, @strgridsrno varchar(8000) declare @spformno int ,@strformno varchar(3000) declare @spitemid int ,@stritemid varchar(3000) declare @spgrade int, @strgrade varchar(8000),@grade_id int  declare @spsetid int , @strsetid varchar(8000) declare @spitem_set_name int, @stritem_set_name varchar(8000),@item_id int,@set_id int if @temptableid <> '' begin set @temptableno=CAST(@temptableid AS INT) End Else begin SELECT @temptableno = isnull(MAX(ID), 0) + 1 FROM temptable_gridset End select @temptableno begin while  @gridsrno_del <> '' begin SET @spgridsrno_del = CHARINDEX(',', @gridsrno_del) IF @spgridsrno_del>0  BEGIN SET @strgridsrno_del = CAST(LEFT(@gridsrno_del, @spgridsrno_del-1) AS VARCHAR) SET @gridsrno_del = RIGHT(@gridsrno_del, LEN(@gridsrno_del)-@spgridsrno_del) End Else BEGIN SET @strgridsrno_del = CAST(@gridsrno_del AS VARCHAR) SET @gridsrno_del = '' End SET @spformno_del = CHARINDEX(',', @formno_del) IF @spformno_del>0 BEGIN SET @strformno_del = CAST(LEFT(@formno_del, @spformno_del-1) AS VARCHAR) SET @formno_del = RIGHT(@formno_del, LEN(@formno_del)-@spformno_del) End Else BEGIN SET @strformno_del = CAST(@formno_del AS VARCHAR) SET @formno_del = '' End if @strgridsrno_del <>'' and @temptableno <>'' begin delete from temptable_gridset where gridsrno=cast(@strgridsrno_del as int)  and id=@temptableno and cmpid=@cmpid End End End begin while @GRADENAME <> '' 	begin SET @spgrade = CHARINDEX(',', @GRADENAME) IF @spgrade>0 BEGIN SET @strgrade = CAST(LEFT(@GRADENAME, @spgrade-1) AS VARCHAR) SET @GRADENAME = RIGHT(@GRADENAME, LEN(@GRADENAME)-@spgrade) End Else BEGIN SET @strgrade = CAST(@GRADENAME AS VARCHAR) SET @GRADENAME = '' End SET @spitem_set_name = CHARINDEX(',', @ITEMNAME) IF @spitem_set_name>0 BEGIN SET @stritem_set_name = CAST(LEFT(@ITEMNAME, @spitem_set_name-1) AS VARCHAR(150)) SET @ITEMNAME = RIGHT(@ITEMNAME, LEN(@ITEMNAME)-@spitem_set_name) End Else BEGIN SET @stritem_set_name = CAST(@ITEMNAME AS VARCHAR(150)) SET @ITEMNAME = '' End SET @spsetid = CHARINDEX(',', @setid) IF @spsetid>0 BEGIN SET @strsetid = CAST(LEFT(@setid, @spsetid-1) AS VARCHAR) SET @setid = RIGHT(@setid, LEN(@setid)-@spsetid) End Else BEGIN SET @strsetid = CAST(@setid AS VARCHAR) SET @setid = '' End SET @spgridsrno = CHARINDEX(',', @gridsrno) IF @spgridsrno>0 BEGIN SET @strgridsrno = CAST(LEFT(@gridsrno, @spgridsrno-1) AS VARCHAR) SET @gridsrno = RIGHT(@gridsrno, LEN(@gridsrno)-@spgridsrno) End Else BEGIN SET @strgridsrno = CAST(@gridsrno AS VARCHAR) SET @gridsrno = '' End SET @spformno = CHARINDEX(',', @formsrno) IF @spformno>0 BEGIN SET @strformno = CAST(LEFT(@formsrno, @spformno-1) AS VARCHAR) SET @formsrno = RIGHT(@formsrno, LEN(@formsrno)-@spformno) End Else BEGIN SET @strformno = CAST(@formsrno AS VARCHAR) SET @formsrno = '' End SET @spitemid = CHARINDEX(',', @itemid) IF @spitemid>0 BEGIN SET @stritemid = CAST(LEFT(@itemid, @spitemid-1) AS VARCHAR) SET @itemid = RIGHT(@itemid, LEN(@itemid)-@spitemid) End ElsE BEGIN SET @stritemid = CAST(@itemid AS VARCHAR) SET @itemid = '' End select @grade_id = isnull(grade_id,0) from grademaster where grade_name = @strgrade and grade_cmpid = @cmpid INSERT INTO  temptable_gridset values (@temptableno,@tablename,	CAST(@strformno AS INT), CAST(@strgridsrno AS INT), CAST(@strsetid AS INT), CAST(@stritemid AS int), CAST(@grade_id AS INT), @cmpid , @locationid , @userid , @yearid  , @transfer, getdate()) set @grade_id = 0 End end "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"   


    '        '*********** FOR GETMAXNO **************
    '        'create SP_GETMAXNO
    '        strCommand = " CREATE procedure SP_GETMAXNO (@fld1 varchar(200), @TBname varchar(150), @whereclause varchar(2000) ) AS declare @where varchar(2000) set @where= ' WHERE 1=1 ' EXEC(' SELECT ' + @fld1 + ' FROM ' + @TBNAME + @where + ' ' + @whereclause) return  "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************" 


    '        '*********** FOR GRN **************
    '        'create SP_GRN_SAVE
    '        strCommand = " CREATE PROCEDURE SP_GRN_SAVE (@VENDOR VARCHAR(100), @DATE  DATETIME, @PONO VARCHAR(100), @PODATE  DATETIME, @challanno varchar(100), @invoiceno varchar(100), @refno varchar(100), @remarks text, @terms text, @transvendor VARCHAR(100), @lrno VARCHAR(100), @lrdate datetime, @transrefno VARCHAR(100), @transremarks text, @cmpid int, @locationid int, @userid int, @yearid int, @transfer bit, @gridsrno varchar(4000), @type varchar(4000), @grade varchar(4000), @item_or_setname varchar(4000), @heatno varchar(4000), @gridremarks varchar(4000), @gridmake varchar(4000), @sizename varchar(4000), @length varchar(4000), @unit varchar(4000), @qty varchar(4000), @qtyunit varchar(4000), @pcs varchar(4000), @rate varchar(4000), @amount varchar(4000), @pogridsrno varchar(4000), @griduploadsrno varchar(4000), @uploadremarks varchar(4000), @name varchar(4000), @imgpath varchar(8000), @challan_date datetime, @invoice_date datetime, @griddone varchar(4000), @temptableid int) AS declare @spgridsrno int, @strgridsrno varchar(8000) declare @sptype int, @strtype varchar(8000) declare @spgrade int, @strgrade varchar(8000),@grade_id int declare @spitem_set_name int, @stritem_set_name varchar(8000),@item_id int,@set_id int declare @spheatno int, @strheatno varchar(8000) declare @spgridremarks int, @strgridremarks varchar(8000) declare @spgridmake int, @strgridmake varchar(8000) declare @spsizename int, @strsizename varchar(8000),@size_id int declare @splength int, @strlength varchar(8000) declare @spunit int, @strunit varchar(8000),@unit_id int declare @spqty int, @strqty varchar(8000) declare @spqtyunit int, @strqtyunit varchar(8000),@qtyunit_id int declare @sppcs int, @strpcs varchar(8000) declare @sprate int, @strrate varchar(8000) declare @spamt int, @stramt varchar(8000) declare @sppogridsrno int, @strpogridsrno varchar(8000) declare @spgriduploadsrno int, @strgriduploadsrno varchar(8000) declare @spuploadremarks int, @struploadremarks varchar(8000) declare @spname int, @strname varchar(8000) declare @spimgpath int, @strimgpath varchar(8000) declare @spgriddone int, @strgriddone varchar(8000) declare @ledgerid int select @ledgerid = vendor_id from vendormaster where vendormaster.vendor_cmpname = @vendor and vendormaster.vendor_cmpid = @cmpid declare @transledgerid int select @transledgerid = vendor_id from vendormaster where vendormaster.vendor_cmpname = @transvendor and vendormaster.vendor_cmpid = @cmpid declare @GRN_NO bigint  SELECT @GRN_NO = isnull(MAX(grn_no), 0) + 1 FROM GRN where grn_cmpid =@cmpid declare @no int SELECT @no = isnull(MAX(grn_no), 0) + 1 FROM GRN where grn_cmpid =@cmpid select @no insert into GRN ( grn_no, grn_LEDGERID, grn_date, grn_pono, grn_podate, grn_challanno, grn_challandt, grn_invoiceno, grn_invoicedt, grn_refno, grn_remarks, grn_terms, grn_transledgerid, grn_lrno, grn_lrdate, grn_transrefno, grn_transremarks, grn_cmpid, grn_locationid, grn_userid, grn_yearid, grn_transfer, grn_created ) values ( @grn_NO, @ledgerid, @date, @pono, @podate, @challanno, @challan_date, @invoiceno, @invoice_date, @refno, @remarks,	@terms,	@transledgerid, @lrno,	@lrdate, @transrefno, @transremarks, @cmpid, @locationid, @userid, @yearid, @transfer, getdate()) begin UPDATE PURCHASEORDER SET PO_AMD_DONE = 1 WHERE po_no=@pono End begin while @gridsrno	<> '' begin SET @spgridsrno = CHARINDEX(',', @gridsrno) IF @spgridsrno>0  BEGIN SET @strgridsrno = CAST(LEFT(@gridsrno, @spgridsrno-1) AS VARCHAR) SET @gridsrno = RIGHT(@gridsrno, LEN(@gridsrno)-@spgridsrno) End Else BEGIN SET @strgridsrno = CAST(@gridsrno AS VARCHAR) SET @gridsrno = '' End SET @sptype = CHARINDEX(',', @type) IF @sptype>0 BEGIN SET @strtype = CAST(LEFT(@type, @sptype-1) AS VARCHAR) SET @type = RIGHT(@type, LEN(@type)-@sptype) End Else BEGIN SET @strtype = CAST(@type AS VARCHAR) SET @type = '' End SET @spgrade = CHARINDEX(',', @grade) IF @spgrade>0 BEGIN SET @strgrade = CAST(LEFT(@grade, @spgrade-1) AS VARCHAR) SET @grade = RIGHT(@grade, LEN(@grade)-@spgrade) End Else BEGIN SET @strgrade = CAST(@grade AS VARCHAR) SET @grade = '' End SET @spitem_set_name = CHARINDEX(',', @item_or_setname) IF @spitem_set_name>0 BEGIN SET @stritem_set_name = CAST(LEFT(@item_or_setname, @spitem_set_name-1) AS VARCHAR(150)) SET @item_or_setname = RIGHT(@item_or_setname, LEN(@item_or_setname)-@spitem_set_name) End Else BEGIN SET @stritem_set_name = CAST(@item_or_setname AS VARCHAR(150)) SET @item_or_setname = '' End SET @spheatno = CHARINDEX(',', @heatno) IF @spheatno>0 BEGIN SET @strheatno = CAST(LEFT(@heatno, @spheatno-1) AS varchar) SET @heatno = RIGHT(@heatno, LEN(@heatno)-@spheatno) End Else BEGIN SET @strheatno = CAST(@heatno AS varchar) SET @heatno = '' End SET @spgridremarks = CHARINDEX(',', @gridremarks) IF @spgridremarks>0 BEGIN SET @strgridremarks = CAST(LEFT(@gridremarks, @spgridremarks-1) AS varchar(150)) SET @gridremarks = RIGHT(@gridremarks, LEN(@gridremarks)-@spgridremarks) End Else BEGIN SET @strgridremarks = CAST(@gridremarks AS varchar(150)) SET @gridremarks = '' End SET @spgridmake = CHARINDEX(',', @gridmake) IF @spgridmake>0 BEGIN SET @strgridmake = CAST(LEFT(@gridmake, @spgridmake-1) AS varchar) SET @gridmake = RIGHT(@gridmake, LEN(@gridmake)-@spgridmake) End Else BEGIN SET @strgridmake = CAST(@gridmake AS varchar) SET @gridmake = '' End SET @spsizename = CHARINDEX(',', @sizename) IF @spsizename>0 BEGIN SET @strsizename = CAST(LEFT(@sizename, @spsizename-1) AS VARCHAR) SET @sizename = RIGHT(@sizename, LEN(@sizename)-@spsizename) End Else BEGIN SET @strsizename = CAST(@sizename AS VARCHAR) SET @sizename = '' End SET @splength = CHARINDEX(',', @length) IF @splength>0  BEGIN SET @strlength = CAST(LEFT(@length, @splength-1) AS FLOAT) SET @length = RIGHT(@length, LEN(@length)-@splength) End Else BEGIN SET @strlength = CAST(@length AS float) SET @length = '' End SET @spunit = CHARINDEX(',', @unit) IF @spunit>0 BEGIN SET @strunit = CAST(LEFT(@unit, @spunit-1) AS VARCHAR) SET @unit = RIGHT(@unit, LEN(@unit)-@spunit) End Else BEGIN SET @strunit = CAST(@unit AS VARCHAR)  SET @unit = '' End SET @spqty = CHARINDEX(',', @qty) IF @spqty>0 BEGIN SET @strqty = CAST(LEFT(@qty, @spqty-1) AS FLOAT) SET @qty = RIGHT(@qty, LEN(@qty)-@spqty) End Else BEGIN SET @strqty = CAST(@qty AS FLOAT) SET @qty = '' End SET @spqtyunit = CHARINDEX(',', @qtyunit) IF @spqtyunit>0 BEGIN SET @strqtyunit = CAST(LEFT(@qtyunit, @spqtyunit-1) AS VARCHAR) SET @qtyunit = RIGHT(@qtyunit, LEN(@qtyunit)-@spqtyunit) End Else BEGIN SET @strqtyunit = CAST(@qtyunit AS VARCHAR) SET @qtyunit = '' End SET @sppcs = CHARINDEX(',', @pcs) IF @sppcs>0 BEGIN SET @strpcs = CAST(LEFT(@pcs, @sppcs-1) AS FLOAT) SET @pcs = RIGHT(@pcs, LEN(@pcs)-@sppcs) End Else BEGIN SET @strpcs = CAST(@pcs AS FLOAT) SET @pcs = '' End SET @sprate = CHARINDEX(',', @rate) IF @sprate>0 BEGIN SET @strrate = CAST(LEFT(@rate, @sprate-1) AS FLOAT) SET @rate = RIGHT(@rate, LEN(@rate)-@sprate) End Else BEGIN SET @strrate = CAST(@rate AS FLOAT) SET @rate = '' End SET @spamt = CHARINDEX(',', @amount) IF @spamt>0 BEGIN SET @stramt = CAST(LEFT(@amount, @spamt-1) AS FLOAT) SET @amount = RIGHT(@amount, LEN(@amount)-@spamt) End Else BEGIN SET @stramt = CAST(@amount AS FLOAT) SET @amount = '' End SET @sppogridsrno = CHARINDEX(',', @pogridsrno) IF @sppogridsrno>0 BEGIN SET @strpogridsrno = CAST(LEFT(@pogridsrno, @sppogridsrno-1) AS FLOAT) SET @pogridsrno = RIGHT(@pogridsrno, LEN(@pogridsrno)-@sppogridsrno) End Else BEGIN SET @strpogridsrno = CAST(@pogridsrno AS integer) SET @pogridsrno = '' End begin update PurchaseOrder_desc set po_recdqty = po_recdqty + CAST(@strqty AS float) where po_no = @Pono and po_gridsrno = CAST(@strpogridsrno AS integer) End SET @spgriduploadsrno = CHARINDEX(',', @griduploadsrno) IF @spgriduploadsrno>0 BEGIN SET @strgriduploadsrno = CAST(LEFT(@griduploadsrno, @spgriduploadsrno-1) AS VARCHAR) SET @griduploadsrno = RIGHT(@griduploadsrno, LEN(@griduploadsrno)-@spgriduploadsrno) End Else BEGIN SET @strgriduploadsrno = CAST(@griduploadsrno AS VARCHAR) SET @griduploadsrno = '' End SET @spUploadremarks = CHARINDEX(',', @Uploadremarks) IF @spUploadremarks>0 BEGIN SET @strUploadremarks = CAST(LEFT(@Uploadremarks, @spUploadremarks-1) AS varchar) SET @Uploadremarks = RIGHT(@Uploadremarks, LEN(@Uploadremarks)-@spUploadremarks) End Else BEGIN SET @strUploadremarks = CAST(@Uploadremarks AS varchar) SET @Uploadremarks = '' End SET @spname = CHARINDEX(',', @name) IF @spname>0 BEGIN SET @strname = CAST(LEFT(@name, @spname-1) AS varchar) SET @name = RIGHT(@name, LEN(@name)-@spname) End Else BEGIN SET @strname = CAST(@name AS varchar) SET @name = '' End SET @spImgpath = CHARINDEX(',', @Imgpath) IF @spImgpath>0 BEGIN SET @strImgpath = CAST(LEFT(@Imgpath, @spImgpath-1) AS text) SET @Imgpath = RIGHT(@Imgpath, LEN(@Imgpath)-@spImgpath) End Else BEGIN SET @strImgpath = CAST(@Imgpath AS text) SET @Imgpath = '' End SET @spgriddone = CHARINDEX(',', @griddone) IF @spgriddone>0 BEGIN SET @strgriddone = CAST(LEFT(@griddone, @spgriddone-1) AS bit) SET @griddone = RIGHT(@griddone, LEN(@griddone)-@spgriddone) End Else BEGIN SET @strgriddone = CAST(@griddone AS bit) SET @griddone = '' End IF @strtype='Item' begin select @item_id = isnull(item_id,0) from ItemMaster where item_name = @stritem_set_name and item_cmpid = @cmpid End ELSE begin select @set_id = isnull(set_id,0) from SetMaster where set_name = @stritem_set_name and set_cmpid = @cmpid	End select @grade_id = isnull(grade_id,0) from grademaster where grade_name = @strgrade and grade_cmpid = @cmpid select @size_id = ISNULL(size_id,0) from sizemaster where size_alias = @strsizename and size_cmpid = @cmpid select @unit_id = isnull(unit_id,0) from unitmaster where unit_abbr = @strunit and unit_cmpid = @cmpid select @qtyunit_id = isnull(unit_id,0) from unitmaster where unit_abbr = @strqtyunit and unit_cmpid = @cmpid IF @strtype='Item'	begin set @set_id =0 End IF @strtype='Set' begin set @item_id =0 End IF @strtype='Set' begin set @grade_id =0 End INSERT INTO  grn_ITEMDESC values ( @grn_NO, CAST(@strgridsrno AS INT), CAST(@strtype AS varchar), CAST(@grade_id AS INT), CAST(@item_id AS int), CAST(@set_id AS INT), CAST(@strheatno AS varchar), CAST(@strgridremarks AS varchar),	CAST(@strgridmake AS varchar),	CAST(@size_id AS INT),	CAST(@strlength AS float),	CAST(@unit_id AS INT), CAST(@strqty AS float), CAST(@qtyunit_id AS INT), CAST(@strpcs AS float), CAST(@strrate AS float), CAST(@stramt as float), @cmpid , @locationid , @userid , @yearid  , @transfer , CAST(@strgriddone AS bit),0, CAST(@strpogridsrno AS integer) ) if @strgriduploadsrno <> '' INSERT INTO  grn_UPLOADDESC values ( @grn_NO, CAST(@strgriduploadsrno AS INT), CAST(@struploadremarks AS varchar), CAST(@strname AS varchar), CAST(@strimgpath AS text), @cmpid , @locationid , @userid , @yearid  , @transfer ) insert into GRN_GRIDSET select @grn_NO,gridsrno,setid,itemid,gradeid,@cmpid,@locationid,@userid,@yearid,@transfer,getdate() from temptable_gridset where ID=@temptableid and gridsrno=@strgridsrno  set @grade_id = 0 set @item_id = 0 set @size_id = 0 set @set_id = 0 set @unit_id = 0 set @qtyunit_id = 0 End End "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)

    '        'create SP_GRN_UPDATE
    '        strCommand = " create PROCEDURE SP_GRN_UPDATE (@VENDOR VARCHAR(100), @DATE  DATETIME,	@PONO VARCHAR(100), @PODATE  DATETIME, @challanno varchar(100), @invoiceno varchar(100), @refno varchar(100), @remarks text, @terms text, @transvendor VARCHAR(100), @lrno VARCHAR(100), @lrdate datetime, @transrefno VARCHAR(100), @transremarks text, @cmpid int, @locationid int, @userid int, @yearid int, @transfer bit, @gridsrno varchar(4000), @type varchar(4000), @grade varchar(4000), @item_or_setname varchar(4000), @heatno varchar(4000), @gridremarks varchar(4000), @gridmake varchar(4000), @sizename varchar(4000),	@length varchar(4000), @unit varchar(4000), @qty varchar(4000), @qtyunit varchar(4000), @pcs varchar(4000), @rate varchar(4000), @amount varchar(4000), @pogridsrno varchar(4000), @griduploadsrno varchar(4000), @uploadremarks varchar(4000), @name varchar(4000), @imgpath varchar(8000), @challan_date datetime, @invoice_date datetime, @griddone varchar(4000), @temptableid INT, @GRNno int ) AS declare @spgridsrno int, @strgridsrno varchar(8000) declare @sptype int, @strtype varchar(8000) declare @spgrade int, @strgrade varchar(8000),@grade_id int declare @spitem_set_name int, @stritem_set_name varchar(8000),@item_id int,@set_id int declare @spheatno int, @strheatno varchar(8000) declare @spgridremarks int, @strgridremarks varchar(8000) declare @spgridmake int, @strgridmake varchar(8000) declare @spqtyunit int, @strqtyunit varchar(8000),@qtyunit_id int declare @sppcs int, @strpcs varchar(8000) declare @spsizename int, @strsizename varchar(8000),@size_id int declare @splength int, @strlength varchar(8000) declare @spunit int, @strunit varchar(8000),@unit_id int declare @spqty int, @strqty varchar(8000) declare @sprate int, @strrate varchar(8000) declare @spamt int, @stramt varchar(8000) declare @sppogridsrno int, @strpogridsrno varchar(8000) declare @spgriduploadsrno int, @strgriduploadsrno varchar(8000) declare @spuploadremarks int, @struploadremarks varchar(8000) declare @spname int, @strname varchar(8000) declare @spimgpath int, @strimgpath varchar(8000) declare @spgriddone int, @strgriddone varchar(8000) declare @ledgerid int select @ledgerid = vendor_id from vendormaster where vendormaster.vendor_cmpname = @vendor and vendormaster.vendor_cmpid = @cmpid declare @transledgerid int select @transledgerid = vendor_id from vendormaster where vendormaster.vendor_cmpname = @transvendor and vendormaster.vendor_cmpid = @cmpid BEGIN update GRN set grn_ledgerid=@ledgerid, grn_date=@date, grn_pono=@pono, grn_podate = @podate, grn_challanno = @challanno, grn_challandt=@challan_date, grn_invoiceno = @invoiceno, grn_invoicedt=@invoice_date, grn_refno = @refno, grn_remarks=@remarks, grn_terms=@terms, grn_transledgerid=@transledgerid, grn_lrno=@lrno, grn_lrdate=@lrdate, grn_transrefno=@transrefno, grn_transremarks=@transremarks where grn_no=@GRNno End begin declare @srno int declare @grnqty int declare CUR_MAIN cursor for select grn_pogridsrno,  grn_qty from grn_itemDESC where grn_no = @GRNno open CUR_MAIN fetch next from CUR_MAIN into @srno,  @grnqty while @@Fetch_STATUS = 0 begin update purchaseorder_desc set purchaseorder_desc.po_recdqty =  purchaseorder_desc.po_recdqty - @grnqty where purchaseorder_desc.po_gridsrno = @srno and purchaseorder_desc.po_no = @pono fetch next from CUR_MAIN into @srno,  @grnqty  End close CUR_MAIN deallocate CUR_MAIN End BEGIN DELETE FROM GRN_ITEMDESC WHERE grn_no=@GRNno DELETE FROM GRN_UPLOADDESC WHERE grn_no=@GRNno End begin delete from GRN_GRIDSET where sr_no=@GRNno End begin while @gridsrno	<> '' begin	SET @spgridsrno = CHARINDEX(',', @gridsrno) IF @spgridsrno>0 BEGIN SET @strgridsrno = CAST(LEFT(@gridsrno, @spgridsrno-1) AS VARCHAR) SET @gridsrno = RIGHT(@gridsrno, LEN(@gridsrno)-@spgridsrno) END ELSE BEGIN SET @strgridsrno = CAST(@gridsrno AS VARCHAR) SET @gridsrno = '' END SET @sptype = CHARINDEX(',', @type) IF @sptype>0 BEGIN SET @strtype = CAST(LEFT(@type, @sptype-1) AS VARCHAR) SET @type = RIGHT(@type, LEN(@type)-@sptype) END ELSE BEGIN SET @strtype = CAST(@type AS VARCHAR) SET @type = '' END SET @spgrade = CHARINDEX(',', @grade) IF @spgrade>0 BEGIN SET @strgrade = CAST(LEFT(@grade, @spgrade-1) AS VARCHAR) SET @grade = RIGHT(@grade, LEN(@grade)-@spgrade) END ELSE BEGIN SET @strgrade = CAST(@grade AS VARCHAR) SET @grade = '' END 	SET @spitem_set_name = CHARINDEX(',', @item_or_setname) IF @spitem_set_name>0 BEGIN SET @stritem_set_name = CAST(LEFT(@item_or_setname, @spitem_set_name-1) AS VARCHAR(150)) SET @item_or_setname = RIGHT(@item_or_setname, LEN(@item_or_setname)-@spitem_set_name) END ELSE BEGIN SET @stritem_set_name = CAST(@item_or_setname AS VARCHAR(150)) SET @item_or_setname = '' END SET @spheatno = CHARINDEX(',', @heatno) IF @spheatno>0 BEGIN SET @strheatno = CAST(LEFT(@heatno, @spheatno-1) AS varchar) SET @heatno = RIGHT(@heatno, LEN(@heatno)-@spheatno) END ELSE BEGIN SET @strheatno = CAST(@heatno AS varchar) SET @heatno = '' END SET @spgridremarks = CHARINDEX(',', @gridremarks) IF @spgridremarks>0 BEGIN SET @strgridremarks = CAST(LEFT(@gridremarks, @spgridremarks-1) AS varchar(150)) SET @gridremarks = RIGHT(@gridremarks, LEN(@gridremarks)-@spgridremarks) END ELSE BEGIN SET @strgridremarks = CAST(@gridremarks AS varchar(150)) SET @gridremarks = '' END SET @spgridmake = CHARINDEX(',', @gridmake) IF @spgridmake>0 BEGIN SET @strgridmake = CAST(LEFT(@gridmake, @spgridmake-1) AS varchar) SET @gridmake = RIGHT(@gridmake, LEN(@gridmake)-@spgridmake) END ELSE BEGIN SET @strgridmake = CAST(@gridmake AS varchar) SET @gridmake = '' END SET @spsizename = CHARINDEX(',', @sizename) IF @spsizename>0 BEGIN SET @strsizename = CAST(LEFT(@sizename, @spsizename-1) AS VARCHAR) SET @sizename = RIGHT(@sizename, LEN(@sizename)-@spsizename) END ELSE BEGIN SET @strsizename = CAST(@sizename AS VARCHAR) SET @sizename = '' END SET @splength = CHARINDEX(',', @length) IF @splength>0 BEGIN SET @strlength = CAST(LEFT(@length, @splength-1) AS FLOAT) SET @length = RIGHT(@length, LEN(@length)-@splength) END ELSE BEGIN SET @strlength = CAST(@length AS float) SET @length = '' END SET @spunit = CHARINDEX(',', @unit) IF @spunit>0 BEGIN SET @strunit = CAST(LEFT(@unit, @spunit-1) AS VARCHAR) SET @unit = RIGHT(@unit, LEN(@unit)-@spunit) END ELSE BEGIN SET @strunit = CAST(@unit AS VARCHAR) SET @unit = '' END 	SET @spqty = CHARINDEX(',', @qty) IF @spqty>0 BEGIN SET @strqty = CAST(LEFT(@qty, @spqty-1) AS FLOAT) SET @qty = RIGHT(@qty, LEN(@qty)-@spqty) END ELSE BEGIN SET @strqty = CAST(@qty AS FLOAT) SET @qty = '' END SET @spqtyunit = CHARINDEX(',', @qtyunit) IF @spqtyunit>0 BEGIN SET @strqtyunit = CAST(LEFT(@qtyunit, @spqtyunit-1) AS VARCHAR) SET @qtyunit = RIGHT(@qtyunit, LEN(@qtyunit)-@spqtyunit) END ELSE BEGIN SET @strqtyunit = CAST(@qtyunit AS VARCHAR) SET @qtyunit = '' END SET @sppcs = CHARINDEX(',', @pcs) IF @sppcs>0 BEGIN SET @strpcs = CAST(LEFT(@pcs, @sppcs-1) AS FLOAT) SET @pcs = RIGHT(@pcs, LEN(@pcs)-@sppcs) END ELSE BEGIN SET @strpcs = CAST(@pcs AS FLOAT) SET @pcs = '' END SET @sprate = CHARINDEX(',', @rate) IF @sprate>0 BEGIN SET @strrate = CAST(LEFT(@rate, @sprate-1) AS FLOAT) SET @rate = RIGHT(@rate, LEN(@rate)-@sprate) END ELSE BEGIN SET @strrate = CAST(@rate AS FLOAT) SET @rate = '' END SET @spamt = CHARINDEX(',', @amount) IF @spamt>0 BEGIN SET @stramt = CAST(LEFT(@amount, @spamt-1) AS FLOAT) SET @amount = RIGHT(@amount, LEN(@amount)-@spamt) END ELSE BEGIN  SET @stramt = CAST(@amount AS FLOAT)  SET @amount = '' END 	SET @sppogridsrno = CHARINDEX(',', @pogridsrno) IF @sppogridsrno>0 BEGIN SET @strpogridsrno = CAST(LEFT(@pogridsrno, @sppogridsrno-1) AS FLOAT) SET @pogridsrno = RIGHT(@pogridsrno, LEN(@pogridsrno)-@sppogridsrno) END ELSE BEGIN SET @strpogridsrno = CAST(@pogridsrno AS integer) SET @pogridsrno = '' END begin update PurchaseOrder_desc set po_recdqty = po_recdqty + CAST(@strqty AS float) where po_no = @Pono and po_gridsrno = CAST(@strpogridsrno AS integer)  end SET @spgriduploadsrno = CHARINDEX(',', @griduploadsrno) IF @spgriduploadsrno>0 BEGIN SET @strgriduploadsrno = CAST(LEFT(@griduploadsrno, @spgriduploadsrno-1) AS VARCHAR) SET @griduploadsrno = RIGHT(@griduploadsrno, LEN(@griduploadsrno)-@spgriduploadsrno) End Else BEGIN SET @strgriduploadsrno = CAST(@griduploadsrno AS VARCHAR) SET @griduploadsrno = '' End SET @spUploadremarks = CHARINDEX(',', @Uploadremarks) IF @spUploadremarks>0 BEGIN SET @strUploadremarks = CAST(LEFT(@Uploadremarks, @spUploadremarks-1) AS varchar) SET @Uploadremarks = RIGHT(@Uploadremarks, LEN(@Uploadremarks)-@spUploadremarks) End Else BEGIN SET @strUploadremarks = CAST(@Uploadremarks AS varchar)  SET @Uploadremarks = '' End SET @spname = CHARINDEX(',', @name) IF @spname>0 BEGIN SET @strname = CAST(LEFT(@name, @spname-1) AS varchar) SET @name = RIGHT(@name, LEN(@name)-@spname) End Else BEGIN SET @strname = CAST(@name AS varchar) SET @name = '' End SET @spImgpath = CHARINDEX(',', @Imgpath) IF @spImgpath>0 BEGIN SET @strImgpath = CAST(LEFT(@Imgpath, @spImgpath-1) AS text) SET @Imgpath = RIGHT(@Imgpath, LEN(@Imgpath)-@spImgpath) End Else BEGIN SET @strImgpath = CAST(@Imgpath AS text) SET @Imgpath = '' End SET @spgriddone = CHARINDEX(',', @griddone) IF @spgriddone>0 BEGIN SET @strgriddone = CAST(LEFT(@griddone, @spgriddone-1) AS bit) SET @griddone = RIGHT(@griddone, LEN(@griddone)-@spgriddone) End Else BEGIN SET @strgriddone = CAST(@griddone AS bit)  SET @griddone = '' End IF @strtype='Item' begin select @item_id = isnull(item_id,0) from ItemMaster where item_name = @stritem_set_name and item_cmpid = @cmpid End ELSE begin select @set_id = isnull(set_id,0) from SetMaster where set_name = @stritem_set_name and set_cmpid = @cmpid	End select @grade_id = isnull(grade_id,0) from grademaster where grade_name = @strgrade and grade_cmpid = @cmpid select @size_id = ISNULL(size_id,0) from sizemaster where size_alias = @strsizename and size_cmpid = @cmpid select @unit_id = isnull(unit_id,0) from unitmaster where unit_abbr = @strunit and unit_cmpid = @cmpid select @qtyunit_id = isnull(unit_id,0) from unitmaster where unit_abbr = @strqtyunit and unit_cmpid = @cmpid IF @strtype='Item'	begin set @set_id =0 End IF @strtype='Set' begin set	@item_id =0 End IF @strtype='Set' begin set	@grade_id =0 End INSERT INTO  grn_ITEMDESC values (@GRNno, CAST(@strgridsrno AS INT), CAST(@strtype AS varchar), CAST(@grade_id AS INT), CAST(@item_id AS int), CAST(@set_id AS INT), CAST(@strheatno AS varchar),	CAST(@strgridremarks AS varchar), CAST(@strgridmake AS varchar), CAST(@size_id AS INT),	CAST(@strlength AS float),	CAST(@unit_id AS INT), CAST(@strqty AS float), CAST(@qtyunit_id AS INT), CAST(@strpcs AS float), CAST(@strrate AS float), CAST(@stramt as float),	@cmpid , @locationid , @userid ,  @yearid  , @transfer , CAST(@strgriddone AS bit), 0, CAST(@strpogridsrno AS integer) )   if @strgriduploadsrno <> '' INSERT INTO  grn_UPLOADDESC values ( @GRNno, CAST(@strgriduploadsrno AS INT), CAST(@struploadremarks AS varchar), CAST(@strname AS varchar), CAST(@strimgpath AS text), @cmpid , @locationid , @userid ,  @yearid  , @transfer )  insert into GRN_GRIDSET  select @GRNno,gridsrno,setid,itemid,gradeid,@cmpid,@locationid,@userid,@yearid,@transfer,getdate() from temptable_gridset where ID=@temptableid and gridsrno=@strgridsrno set @grade_id = 0 set @item_id = 0 set @size_id = 0 set @set_id = 0 set @unit_id = 0 set @qtyunit_id = 0 End end "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"


    '        '*********** FOR GRN TEST **************
    '        'create SP_GRNTEST_SAVE
    '        strCommand = " create PROCEDURE SP_GRNTEST_SAVE (@DATE  DATETIME,@otherref VARCHAR(100), @labDATE  DATETIME,@grade1 varchar(100), @name VARCHAR(100),	@Itemname varchar(100),@challanno varchar(100), @invno varchar(100), @pono varchar(100), @podate datetime,	@grnno int,	@remarks text, @cmpid int, @locationid int, @userid int, @yearid int, @transfer bit, @gradeid varchar(4000), @status varchar(4000), @sizeid varchar(4000), @unitid varchar(4000), @length varchar(4000), @heatno varchar(4000), @suppno varchar(4000), @labno varchar(4000), @witnessno varchar(4000), @testname varchar(4000), @parameter varchar(4000), @startrange varchar(4000), @endrange varchar(4000), @unit varchar(4000), @vendorreport varchar(4000), @userreport varchar(4000), @witnessreport varchar(4000), @gridsrno varchar(4000) ) AS declare @sptestname int, @strtestname varchar(8000), @Testingid int declare @spparameter int, @strparameter varchar(8000), @Parameterid int declare @spstartrange int, @strstartrange varchar(8000) declare @spendrange int, @strendrange varchar(8000) declare @spunit int, @strunit varchar(8000),@Unit_id1 int declare @spvendorreport int, @strvendorreport varchar(8000) declare @spuserreport int, @struserreport varchar(8000) declare @spwitnessreport int, @strwitnessreport varchar(8000) declare @spstatus int,@strstatus varchar(5000) declare @spsizeid int ,@strsizeid varchar(4000),@tempint INT declare @splength int,@strlength varchar(4000) declare @spgridsrno int,@strgridsrno varchar(4000) declare @spsizeunitid int,@strsizeunitid varchar(4000) declare @spheatno int,@strheatno varchar(4000) declare @spsuppno int,@strsuppno varchar(4000) declare @splabno int,@strlabno varchar(4000) declare @spwitnessno int,@strwitnessno varchar(4000) declare @spgradeid int,@strgradeid varchar(4000) declare @heatnoid int declare @GRNTEST_NO bigint SELECT @GRNTEST_NO = isnull(MAX(grntest_no), 0) + 1 FROM GRNTEST where grntest_cmpid =@cmpid declare @ledgerid int, @grade1id int, @itemid int, @setid int,   @unitfrmid int select @ledgerid = vendor_id from vendormaster where vendormaster.vendor_cmpname = @name and vendormaster.vendor_cmpid = @cmpid select @itemid = item_id from itemmaster where itemmaster.item_name = @itemname and itemmaster.item_cmpid = @cmpid select @grade1id = grade_id from grademaster where grademaster.grade_name = @grade1 and grademaster.grade_cmpid = @cmpid select @heatnoid = parameter_id from parametermaster where parametermaster.parameter_name = 'HEAT NO' and parametermaster.parameter_cmpid = @cmpid insert into GRNTEST (grntest_no, grntest_date, grntest_labref, grntest_labdate, grntest_grade1id,grntest_vendorid,grntest_itemid,grntest_challanno, grntest_invno,grntest_pono,grntest_podate,grntest_grnno,grntest_remarks,grntest_cmpid, grntest_locationid, grntest_userid, grntest_yearid, grntest_transfer, grntest_created )values ( @GRNTEST_NO, @date, @otherref, @labdate, @grade1id, @ledgerid, @itemid, @challanno, @invno, @pono, @podate, @grnno, @remarks, @cmpid, @locationid, @userid, @yearid, @transfer, getdate()) begin while @testname	<> '' begin	SET @sptestname = CHARINDEX(',', @testname) IF @sptestname>0 BEGIN SET @strtestname = CAST(LEFT(@testname, @sptestname-1) AS VARCHAR(100)) SET @testname = RIGHT(@testname, LEN(@testname)-@sptestname) END ELSE BEGIN SET @strtestname = CAST(@testname AS VARCHAR(100)) SET @testname = '' END SET @spParameter = CHARINDEX(',', @Parameter) IF @spParameter>0 BEGIN SET @strParameter = CAST(LEFT(@Parameter, @spParameter-1) AS VARCHAR) SET @Parameter = RIGHT(@Parameter, LEN(@Parameter)-@spParameter) END ELSE BEGIN SET @strParameter = CAST(@Parameter AS VARCHAR) SET @Parameter = '' END SET @spStartRange = CHARINDEX(',', @StartRange) IF @spStartRange>0 BEGIN SET @strStartRange = CAST(LEFT(@StartRange, @spStartRange-1) AS VARCHAR) SET @StartRange = RIGHT(@StartRange, LEN(@StartRange)-@spStartRange) END ELSE BEGIN SET @strStartRange = CAST(@StartRange AS VARCHAR) SET @StartRange = '' END SET @spEndRange = CHARINDEX(',', @EndRange) IF @spEndRange>0 BEGIN SET @strEndRange = CAST(LEFT(@EndRange, @spEndRange-1) AS VARCHAR) SET @EndRange = RIGHT(@EndRange, LEN(@EndRange)-@spEndRange) END ELSE BEGIN SET @strEndRange = CAST(@EndRange AS VARCHAR) SET @EndRange = '' END SET @spunit = CHARINDEX(',', @unit) IF @spunit>0 BEGIN SET @strunit = CAST(LEFT(@unit, @spunit-1) AS varchar) SET @unit = RIGHT(@unit, LEN(@unit)-@spunit) END ELSE BEGIN SET @strunit = CAST(@unit AS varchar) SET @unit = '' END SET @spVendorreport = CHARINDEX(',', @Vendorreport) IF @spVendorreport>0 BEGIN SET @strVendorreport = CAST(LEFT(@Vendorreport, @spVendorreport-1) AS VARCHAR(150)) SET @Vendorreport = RIGHT(@Vendorreport, LEN(@Vendorreport)-@spVendorreport) END ELSE BEGIN SET @strVendorreport = CAST(@Vendorreport AS VARCHAR(150)) SET @Vendorreport = '' END SET @spUserreport = CHARINDEX(',', @Userreport) IF @spUserreport>0 BEGIN SET @strUserreport = CAST(LEFT(@Userreport, @spUserreport-1) AS VARCHAR(150)) SET @Userreport = RIGHT(@Userreport, LEN(@Userreport)-@spUserreport) END ELSE BEGIN SET @strUserreport = CAST(@Userreport AS VARCHAR(150)) SET @Userreport = '' END SET @spwitnessreport = CHARINDEX(',', @witnessreport) IF @spwitnessreport>0 BEGIN SET @strwitnessreport = CAST(LEFT(@witnessreport, @spwitnessreport-1) AS VARCHAR(150)) SET @witnessreport = RIGHT(@witnessreport, LEN(@witnessreport)-@spwitnessreport) END ELSE BEGIN SET @strwitnessreport = CAST(@witnessreport AS VARCHAR(150)) SET @witnessreport = '' END SET @spgridsrno = CHARINDEX(',', @gridsrno ) IF @spgridsrno>0 BEGIN SET @strgridsrno= CAST(LEFT(@gridsrno, @spgridsrno-1) AS float) SET @gridsrno = RIGHT(@gridsrno, LEN(@gridsrno)-@spgridsrno) END  ELSE BEGIN  SET @strgridsrno= CAST(@gridsrno AS float) SET @gridsrno= ''  END SET @spsizeid = CHARINDEX(',', @sizeid) IF @spsizeid>0 BEGIN SET @strsizeid = CAST(LEFT(@sizeid, @spsizeid-1) AS int) SET @sizeid = RIGHT(@sizeid, LEN(@sizeid)-@spsizeid) END ELSE BEGIN SET @strsizeid = CAST(@sizeid AS int) SET @sizeid = '' END SET @spstatus = CHARINDEX(',', @status) IF @spstatus>0 BEGIN SET @strstatus = CAST(LEFT(@status, @spstatus-1) AS VARCHAR(100)) SET @status = RIGHT(@status, LEN(@status)-@spstatus) END ELSE BEGIN SET @strstatus = CAST(@status AS VARCHAR(100)) SET @status = '' END begin update GRN_ITEMDESC set grn_testing = 1 where grn_no = @grnno and grn_srno = @strgridsrno end if @strstatus = 'Approved' begin update GRN_ITEMDESC set grn_passfail = 1 where grn_no = @grnno and grn_srno = @strgridsrno end if @strstatus = 'Under Testing'  begin update GRN_ITEMDESC set grn_passfail = 0 where grn_no = @grnno and grn_srno = @strgridsrno end ELSE IF @strSTATUS = 'Rejected' begin update GRN_ITEMDESC set grn_passfail = 2 where grn_no = @grnno and grn_srno = @strgridsrno end SET @splength = CHARINDEX(',', @length)  IF @splength>0  BEGIN  SET @strlength = CAST(LEFT(@length, @splength-1) AS float) SET @length = RIGHT(@length, LEN(@length)-@splength) END ELSE BEGIN SET @strlength = CAST(@length AS float) SET @length = '' END	SET @spsizeunitid = CHARINDEX(',', @unitid) IF @spsizeunitid>0 BEGIN SET @strsizeunitid = CAST(LEFT(@unitid, @spsizeunitid-1) AS int) SET @unitid = RIGHT(@unitid, LEN(@unitid)-@spsizeunitid) END ELSE BEGIN SET @strsizeunitid = CAST(@unitid AS int) SET @unitid = '' END SET @spheatno = CHARINDEX(',', @heatno) IF @spheatno>0 BEGIN SET @strheatno = CAST(LEFT(@heatno, @spheatno-1) AS VARCHAR) SET @heatno = RIGHT(@heatno, LEN(@heatno)-@spheatno) END ELSE BEGIN SET @strheatno = CAST(@heatno AS VARCHAR) SET @heatno = '' END SET @spgradeid = CHARINDEX(',', @gradeid) IF @spgradeid>0 BEGIN SET @strgradeid = CAST(LEFT(@gradeid, @spgradeid-1) AS int) SET @gradeid = RIGHT(@gradeid, LEN(@gradeid)-@spgradeid) END ELSE BEGIN SET @strgradeid = CAST(@gradeid AS int) SET @gradeid = '' END	SET @spsuppno = CHARINDEX(',', @suppno) IF @spsuppno>0 BEGIN SET @strsuppno = CAST(LEFT(@suppno, @spsuppno-1) AS VARCHAR) SET @suppno = RIGHT(@suppno, LEN(@suppno)-@spsuppno) END ELSE BEGIN SET @strsuppno = CAST(@suppno AS VARCHAR) SET @suppno = '' END SET @splabno = CHARINDEX(',', @labno) IF @splabno>0 BEGIN SET @strlabno = CAST(LEFT(@labno, @splabno-1) AS VARCHAR) SET @labno = RIGHT(@labno, LEN(@labno)-@splabno) END ELSE BEGIN SET @strlabno = CAST(@labno AS VARCHAR) SET @labno = '' END SET @spwitnessno = CHARINDEX(',', @witnessno) IF @spwitnessno>0 BEGIN SET @strwitnessno = CAST(LEFT(@witnessno, @spwitnessno-1) AS VARCHAR) SET @witnessno = RIGHT(@witnessno, LEN(@witnessno)-@spwitnessno) END ELSE BEGIN SET @strwitnessno = CAST(@witnessno AS VARCHAR) SET @witnessno = '' END select @Testingid = isnull(Testing_id,0) from TestingMaster where Testing_name = @strtestname and  Testing_cmpid = @cmpid select @parameterid = isnull(Parameter_id,0) from ParameterMaster where Parameter_name = @strParameter and Parameter_cmpid = @cmpid select @Unit_id1 = isnull(Unit_id,0) from UnitMaster where Unit_abbr = @strunit and unit_cmpid = @cmpid INSERT INTO  GRNTEST_DESC values (@GRNTEST_NO, @Testingid, @Parameterid, CAST(@strstartrange AS varchar), CAST(@strendrange as varchar), @Unit_id1, CAST(@strvendorreport as varchar),	CAST(@struserreport as varchar), CAST(@strwitnessreport as varchar), CAST(@strgridsrno as float), CAST(@strstatus as varchar), CAST(@strsizeid AS int) , CAST(@strlength AS FLOAT), CAST(@strsizeunitid AS int) , cast(@strheatno as varchar), CAST(@strgradeid as int), cast(@strsuppno as varchar), cast(@strlabno as varchar), cast(@strwitnessno as varchar), @cmpid , @locationid , @userid ,  @yearid  , @transfer  ) declare @tempuserreport varchar(100) if CAST(@strwitnessreport as varchar) <> ''  begin set @tempuserreport = CAST(@strwitnessreport as varchar) end if CAST(@struserreport as varchar) <> '' and CAST(@strwitnessreport as varchar) = ''  begin set @tempuserreport = CAST(@struserreport as varchar)  end if CAST(@strvendorreport as varchar) <> '' and CAST(@struserreport as varchar) = '' and CAST(@strwitnessreport as varchar) = ''  begin set @tempuserreport = CAST(@strvendorreport as varchar) end INSERT INTO  TEMP_GRNTEST_DESC values  ( @GRNTEST_NO, @Testingid, @Parameterid, CAST(@strstartrange AS varchar), CAST(@strendrange as varchar), CAST(@Unit_id1 AS VARCHAR), CAST(@strvendorreport as varchar), @tempuserreport, CAST(@strgridsrno as varchar),	CAST(@strstatus as varchar), CAST(@strsizeid AS int) , CAST(@strlength AS FLOAT), CAST(@strsizeunitid AS int) , cast(@strheatno as varchar), CAST(@strgradeid AS int) , @cmpid , @locationid , @userid ,  @yearid  , @transfer  ) (select @tempint =count(*) from temp_grntest_desc where temp_grntest_desc.grntest_parameterid=@heatnoid and temp_grntest_desc.grntest_sizeid=@strsizeid and temp_grntest_desc.grntest_gridsrno=@strgridsrno) IF @tempint<=0 BEGIN	EXEC SP_TEMPTABLE_GRNTEST_SAVE @GRNTEST_NO, @Testingid, @Parameterid, @strstartrange , @strendrange , @Unit_id1 , @strvendorreport , @struserreport , @strgridsrno , @strstatus , @strsizeid , @strlength , @strsizeunitid , @strheatno , @strgradeid , @cmpid,  @locationid, @userid, @yearid, @transfer END set @testingid = 0 set @parameterid = 0 set @unit_id1 = 0 end end "
    '        dtTable = objDBOperation.execute(strCommand).Tables(0)
    '        '*****************************************************"

    '        Return intResult

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

#Region "Accounts"

    Public Function CREATEGROUP() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEGROUP"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATELEDGER() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATELEDGER"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEREGISTER() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEREGISTER"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))


            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATETAX() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATETAX"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region

#Region "Item"

    Public Function CREATEGRADE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEGRADE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEMATERIALTYPE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEMATERIALTYPE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEITEM() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEITEM"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATESIZE() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATESIZE"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATESET() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATESET"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

    Public Function CREATEWEIGHT() As Integer

        Try

            'save group
            Dim strCommand As String = "SP_PROGRESSBAR_CREATEWEIGHT"
            Dim alParameter As New ArrayList
            With alParameter

                .Add(New SqlClient.SqlParameter("@cmpid", alParaval(0)))
                .Add(New SqlClient.SqlParameter("@locationid", alParaval(1)))
                .Add(New SqlClient.SqlParameter("@userid", alParaval(2)))
                .Add(New SqlClient.SqlParameter("@yearid", alParaval(3)))

            End With

            intResult = objDBOperation.executeNonQuery(strCommand, alParameter)
        Catch ex As Exception
            Throw ex
        End Try
        Return 0

    End Function

#End Region

#End Region

End Class
