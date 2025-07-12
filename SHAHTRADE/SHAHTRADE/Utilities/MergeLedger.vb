﻿
Imports BL

Public Class MergeLedger

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Me.Close()
    End Sub

    Private Sub cmbname_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbname.Enter
        Try
            If cmbname.Text.Trim = "" Then fillname(cmbname, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbname_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbname.Validating
        Try
            If cmbname.Text.Trim <> "" Then namevalidate(cmbname, CMBACCCODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & cmbname.Text.Trim & "' and ledgers.acc_cmpid = " & CmpId & " and ledgers.acc_LOCATIONid = " & Locationid & " and ledgers.acc_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmergename_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbmergename.Enter, cmbmergename.Enter
        Try
            If cmbmergename.Text.Trim = "" Then fillname(cmbmergename, False, " and ledgers.acc_cmpid = " & CmpId & " and LEDGERS.acc_LOCATIONid = " & Locationid & " and LEDGERS.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbmergename_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmbmergename.Validating
        Try
            If cmbmergename.Text.Trim <> "" Then namevalidate(cmbmergename, CMBMERGECODE, e, Me, txtadd, " and ledgers.acc_cmpname = '" & cmbmergename.Text.Trim & "' and ledgers.acc_cmpid = " & CmpId & " and ledgers.acc_LOCATIONid = " & Locationid & " and ledgers.ACC_YEARID = " & YearId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            EP.Clear()
            If cmbmergename.Text.Trim = cmbname.Text.Trim Then
                EP.SetError(cmbname, "Both the Name cannot be same")
                Exit Sub
            End If

            If cmbname.Text.Trim <> "" And cmbmergename.Text.Trim <> "" Then
                Dim tempmsg As Integer = MsgBox("Merge Ledger " & cmbname.Text.Trim & " with " & cmbmergename.Text.Trim, MsgBoxStyle.YesNo)
                If tempmsg = vbYes Then
                    tempmsg = MsgBox("Are You Sure, All Transactions will be merged in the name of " & cmbmergename.Text.Trim & "?", MsgBoxStyle.YesNo)
                    If tempmsg = vbYes Then

                        Dim OBJMERGER As New ClsMergeLedger
                        Dim ALPARAVAL As New ArrayList
                        ALPARAVAL.Add(cmbname.Text.Trim)
                        ALPARAVAL.Add(cmbmergename.Text.Trim)
                        ALPARAVAL.Add(CmpId)
                        ALPARAVAL.Add(Locationid)
                        ALPARAVAL.Add(YearId)

                        OBJMERGER.alParaval = ALPARAVAL
                        Dim INTRESULT As Integer = OBJMERGER.save()
                        MsgBox("Ledger Merged Successfully")
                        cmbname.Text = ""
                        cmbmergename.Text = ""

                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class