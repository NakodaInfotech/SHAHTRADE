
Imports BL

Public Class YearTransfer

    Dim INTRES As Integer
    Dim OBJTRF As New ClsYearTransfer
    Public FRMSTRING As String = ""

    Private Sub CMDEXIT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDEXIT.Click
        Me.Close()
    End Sub

    Private Sub CMDOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDOK.Click
        Try
            'transfering data from selected cmp
            If GRIDYEAR.SelectedRows.Count = 0 Then
                MsgBox("Select Year", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim SELECTEDYEARID As Integer = 0
            Dim TEMPMSG As Integer = MsgBox("Transfer Data from Selected Year?", MsgBoxStyle.YesNo)
            If TEMPMSG = vbYes Then
                TEMPMSG = MsgBox("Are you sure, wish to Proceed?", MsgBoxStyle.YesNo)
                If TEMPMSG = vbYes Then

                    SELECTEDYEARID = GRIDYEAR.CurrentRow.Cells(GYEARID.Index).Value

                    If FRMSTRING = "YEARTRANSFER" Then
                       
                        '********* TRANSFERRING DATA ***********
                        TRANSFERUSER(SELECTEDYEARID)

                        TRANSFERPATTA(SELECTEDYEARID)
                        TRANSFERSIZE(SELECTEDYEARID)
                        TRANSFERCOLOR(SELECTEDYEARID)

                        TRANSFERHSN(SELECTEDYEARID)
                        TRANSFERUNIT(SELECTEDYEARID)
                        TRANSFERCATEGORY(SELECTEDYEARID)
                        TRANSFERSUBCATEGORY(SELECTEDYEARID)
                        TRANSFERMAKE(SELECTEDYEARID)
                        TRANSFERITEM(SELECTEDYEARID)

                        TRANSFERGROUP(SELECTEDYEARID)
                        TRANSFERLOCATION(SELECTEDYEARID)
                        TRANSFERAGENT(SELECTEDYEARID)
                        TRANSFERTRANSPORT(SELECTEDYEARID)
                        TRANSFERACCOUNTS(SELECTEDYEARID)

                        TRANSFERRATECONFIG(SELECTEDYEARID)

                        TRANSFERBALANCE(SELECTEDYEARID)


                        'TRANSFER CLOSING STOCK AS OPENINGSTOCK IN NEXT YEAR
                        Dim OBJCMN As New ClsCommon
                        Dim FROMLEDGERID, TOLEDGERID As Integer
                        Dim OPENINGVALUE As Decimal
                        'CHECK WHETHER DATA IS THERE FOR THIS YEAR OR NOT IF PRESENT THEN UPDATE ELSE INSERT
                        Dim DT As DataTable = OBJCMN.search("OPENINGSTOCK", "", "OPENINGCLOSINGSTOCK", " AND YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then DT = OBJCMN.Execute_Any_String("UPDATE OPENINGCLOSINGSTOCK SET OPENINGSTOCK = (SELECT CLOSINGSTOCK FROM OPENINGCLOSINGSTOCK WHERE YEARID = " & SELECTEDYEARID & ") WHERE YEARID = " & YearId, "", "") Else DT = OBJCMN.Execute_Any_String(" INSERT INTO OPENINGCLOSINGSTOCK VALUES ((SELECT ISNULL(CLOSINGSTOCK,0) AS CLOSINGSTOCK FROM OPENINGCLOSINGSTOCK WHERE YEARID = " & SELECTEDYEARID & "),0," & CmpId & "," & YearId & ")", "", "")

                        'ALSO UPDATE IN OPENINGSTOCK ACCOUNSTMASTER
                        'BUT DO ADD ACC POSTING OR THIS
                        DT = OBJCMN.Execute_Any_String("UPDATE ACCOUNTSMASTER SET ACC_DRCR = 'Dr.', ACC_OPBAL = (SELECT CLOSINGSTOCK FROM OPENINGCLOSINGSTOCK WHERE YEARID = " & SELECTEDYEARID & ") WHERE ACC_CMPNAME = 'OPENING STOCK' AND ACC_YEARID = " & YearId, "", "")


                        'POSTNG IN ACCMASTER
                        'OPENING STOCK DEBIT AND OPENING A/C CREDIT
                        DT = OBJCMN.search("ISNULL(ACC_ID,0) AS FROMLEDGERID ", "", "LEDGERS", " AND ACC_CMPNAME = 'Opening A/C' AND ACC_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then FROMLEDGERID = DT.Rows(0).Item(0)



                        DT = OBJCMN.search("ISNULL(ACC_ID,0) AS FROMLEDGERID ", "", "LEDGERS", " AND ACC_CMPNAME = 'Opening Stock' AND ACC_YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then TOLEDGERID = DT.Rows(0).Item(0)


                        DT = OBJCMN.Execute_Any_String(" DELETE FROM ACCMASTER WHERE ACC_TYPE = 'OPENING' AND ACC_FROMID = " & FROMLEDGERID & " AND ACC_TOID = " & TOLEDGERID & " AND ACC_YEARID = " & YearId, "", "")
                        DT = OBJCMN.search("ISNULL(OPENINGSTOCK,0) AS OPENINGSTOCK", "", "OPENINGCLOSINGSTOCK", " AND YEARID = " & YearId)
                        If DT.Rows.Count > 0 Then
                            OPENINGVALUE = DT.Rows(0).Item("OPENINGSTOCK")
                            DT = OBJCMN.Execute_Any_String(" INSERT INTO ACCMASTER VALUES (" & Val(FROMLEDGERID) & "," & Val(OPENINGVALUE) & "," & TOLEDGERID & ",0,'" & Format(AccFrom.Date, "MM/dd/yyyy") & "','OPENING','',''," & CmpId & ",0," & Userid & "," & YearId & ",0,GETDATE())", "", "")
                        End If


                        '******* TRANSFERRING DATA DONE ********

                        MsgBox("Transfer Completed Successfully")
                       
                    ElseIf FRMSTRING = "USERTRANSFER" Then
                        TRANSFERUSER(SELECTEDYEARID)
                        MsgBox("User Transferred Successfully")

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub YearTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If FRMSTRING = "USERTRANSFER" Then
                Me.Text = "Transfer Users"
                LBLUSER.Visible = True
                CMBUSER.Visible = True
                fillUSER(CMBUSER, "")
            End If
            FILLGRID()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub FILLGRID()
        Try
            Dim OBJCMN As New ClsCommon
            Dim DT As DataTable = OBJCMN.search(" CONVERT(char(11), year_startdate , 6) + ' - ' + CONVERT(char(11), year_enddate , 6) AS YEARNAME, YEAR_ID AS YEARID   ", "", " YEARMASTER", " AND YEAR_STARTDATE < '" & Format(AccFrom.Date, "MM/dd/yyyy") & "' AND YEAR_ID <> " & YearId & " AND YEAR_CMPID = " & CmpId & " ORDER BY year_startdate DESC ")
            If DT.Rows.Count > 0 Then
                For Each DTROW As DataRow In DT.Rows
                    GRIDYEAR.Rows.Add(DTROW("YEARNAME"), DTROW("YEARID"))
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERUSER(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(CMBUSER.Text.Trim)
            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERUSER()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERHSN(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERHSN()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERITEM(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERITEM()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERGROUP(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERGROUP()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERLOCATION(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERLOCATION()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERTRANSPORT(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERTRANSPORT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERAGENT(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERAGENT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERACCOUNTS(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERACCOUNTS()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERRATECONFIG(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERRATECONFIG()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERUNIT(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERUNIT()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERCATEGORY(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERCATEGORY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERSUBCATEGORY(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERSUBCATEGORY()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERMAKE(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERMAKE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERPATTA(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERPATTA()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERSIZE(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERSIZE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERCOLOR(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERCOLOR()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub TRANSFERBALANCE(ByVal SELECTEDYEARID As Integer)
        Try
            Dim ALPARAVAL As New ArrayList

            ALPARAVAL.Add(SELECTEDYEARID)
            ALPARAVAL.Add(CmpId)
            ALPARAVAL.Add(Locationid)
            ALPARAVAL.Add(Userid)
            ALPARAVAL.Add(YearId)

            OBJTRF.alParaval = ALPARAVAL
            INTRES = OBJTRF.TRANSFERBALANCE()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class