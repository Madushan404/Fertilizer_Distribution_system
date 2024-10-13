Imports System.Diagnostics.Contracts
Imports K4os.Compression.LZ4.Streams
Imports MySql.Data.MySqlClient

Public Class Cus_Update_Delete
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged







        'connectdb()

        'Dim search As String = TxtSearch.Text ' input search word

        'Dim query As String = "select * FROM  customer_register WHERE NIC = @ID"
        'Using cmd As New MySqlCommand(query, Connect)
        'cmd.Parameters.AddWithValue("@ID", search)
        ''Connect.Open()
        ' cmd.ExecuteNonQuery()
        'Connect.Close()

        ' End Using
    End Sub




    Dim row_index As String
    Private Sub Cus_DataGridviwe_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Cus_DataGridviwe.CellClick

        Try
            Dim dr As DataGridViewRow = Cus_DataGridviwe.SelectedRows(0)

            TxtNic.Text = dr.Cells(0).Value.ToString()
            TxtFname.Text = dr.Cells(1).Value.ToString()
            TxtAddress.Text = dr.Cells(2).Value.ToString()
            TxtContact.Text = dr.Cells(3).Value.ToString()
            TxtContactP.Text = dr.Cells(4).Value.ToString()
            TxtEmail.Text = dr.Cells(6).Value.ToString()

            Dim gender As String = dr.Cells(5).Value.ToString


            If gender = "Male" Then
                RdioMale.Checked = True
            Else
                RdioFemale.Checked = True

            End If



            ComboNatureOfFarming.Text = dr.Cells(7).Value.ToString()
            ComboARODomain.Text = dr.Cells(8).Value.ToString()
            ComboVODomain.Text = dr.Cells(9).Value.ToString()
            TxtAccName.Text = dr.Cells(10).Value.ToString()
            ComboBName.Text = dr.Cells(12).Value.ToString()
            TxtAccNo.Text = dr.Cells(11).Value.ToString()
            TxtBranchCode.Text = dr.Cells(13).Value.ToString()



        Catch ex As Exception


        End Try



        Dim row As DataGridViewRow
        row = Cus_DataGridviwe.Rows(e.RowIndex)
        row_index = row.Cells(0).Value


    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click


        connectdb()

        Dim selectedID As String = row_index ' Replace with the actual ID you want to delete

        Dim query As String = "DELETE FROM  customer_register WHERE NIC = @ID"
        Using cmd As New MySqlCommand(query, Connect)
            cmd.Parameters.AddWithValue("@ID", selectedID)

            'confirm Message to delete data
            Dim response, Msg, NIC, Fname, Address, Contact As String
            NIC = TxtNic.Text
            Fname = TxtFname.Text
            Address = TxtAddress.Text
            Contact = TxtContact.Text

            Msg = "You're Going To Delete The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "NIC no is : " & NIC & ControlChars.NewLine & ControlChars.NewLine & "Customer name is : " & TxtFname.Text & ControlChars.NewLine & ControlChars.NewLine & "Address is : " & TxtAddress.Text & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & "This Customer all data Will be Permanently Deleted. This Process Can't Be Undone"


            response = MessageBox.Show(Msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)


            If response = MsgBoxResult.Yes Then
                cmd.ExecuteNonQuery()
                Connect.Close()
                MessageBox.Show("Data Delete process was successfull.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data delete process NOT successfully.", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        End Using


        'refresh data gride view
        Dim dt As New DataTable
        dt.Columns.Add("NIC")
        dt.Columns.Add("Full_Name")
        dt.Columns.Add("Address")
        dt.Columns.Add("Contact_No")
        dt.Columns.Add("Contact_PNo")
        dt.Columns.Add("Gender")
        dt.Columns.Add("E-Mail")
        dt.Columns.Add("Nature_Farmer")
        dt.Columns.Add("ARO_Domin")
        dt.Columns.Add("VO_Domain")
        dt.Columns.Add("ACC_Name")
        dt.Columns.Add("ACC_No")
        dt.Columns.Add("Bank_Name")
        dt.Columns.Add("Branch_Code")

        'assing to data relevent column
        Dim Cus_registor As List(Of CusRegister_clss) = GetCus()
        For Each r As CusRegister_clss In Cus_registor
            dt.Rows.Add(r.Nic, r.Full_name, r.Address, r.Contat, r.Contact_P, r.Gender, r.Email, r.N_farming, r.ARO, r.VO_Domain, r.Acc_Name, r.Acc_No, r.Bank_Name, r.Branch_code)
        Next
        Cus_DataGridviwe.DataSource = dt

    End Sub







    Private Sub Cus_Update_Delete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'create gridview column

        Dim dt As New DataTable
        dt.Columns.Add("NIC")
        dt.Columns.Add("Full_Name")
        dt.Columns.Add("Address")
        dt.Columns.Add("Contact_No")
        dt.Columns.Add("Contact_PNo")
        dt.Columns.Add("Gender")
        dt.Columns.Add("E-Mail")
        dt.Columns.Add("Nature_Farmer")
        dt.Columns.Add("ARO_Domin")
        dt.Columns.Add("VO_Domain")
        dt.Columns.Add("ACC_Name")
        dt.Columns.Add("ACC_No")
        dt.Columns.Add("Bank_Name")
        dt.Columns.Add("Branch_Code")




        'assing to data relevent column
        Dim Cus_registor As List(Of CusRegister_clss) = GetCus()
        For Each r As CusRegister_clss In Cus_registor
            dt.Rows.Add(r.Nic, r.Full_name, r.Address, r.Contat, r.Contact_P, r.Gender, r.Email, r.N_farming, r.ARO, r.VO_Domain, r.Acc_Name, r.Acc_No, r.Bank_Name, r.Branch_code)
        Next
        Cus_DataGridviwe.DataSource = dt

        'change grideview column size
        Cus_DataGridviwe.Columns(0).Width = 150
        Cus_DataGridviwe.Columns(1).Width = 300
        Cus_DataGridviwe.Columns(2).Width = 200
        Cus_DataGridviwe.Columns(3).Width = 150
        Cus_DataGridviwe.Columns(4).Width = 150
        Cus_DataGridviwe.Columns(5).Width = 150
        Cus_DataGridviwe.Columns(6).Width = 150
        Cus_DataGridviwe.Columns(8).Width = 150
        Cus_DataGridviwe.Columns(9).Width = 200
        Cus_DataGridviwe.Columns(13).Width = 150


        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()
        Lblrequired101.Hide()
        Lblrequired11.Hide()
        Lblrequired12.Hide()
        Lblrequired13.Hide()
        LblRequired14.Hide()


    End Sub




    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        'reset textbox, radio and combo box
        TxtNic.Clear()
        TxtFname.Clear()

        TxtAddress.Clear()
        TxtContact.Clear()
        TxtContactP.Clear()

        TxtEmail.Clear()
        RdioFemale.Checked = False
        RdioMale.Checked = False
        ComboNatureOfFarming.SelectedIndex = -1
        ComboARODomain.SelectedIndex = -1
        ComboVODomain.SelectedIndex = -1
        TxtAccName.Clear()
        ComboBName.SelectedIndex = -1
        TxtAccNo.Clear()
        TxtBranchCode.Clear()
        TxtSearch.Clear()



        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()
        Lblrequired101.Hide()
        Lblrequired11.Hide()
        Lblrequired12.Hide()
        Lblrequired13.Hide()
        LblRequired14.Hide()
    End Sub



    Private Sub TxtNic_Leave(sender As Object, e As EventArgs) Handles TxtNic.Leave
        If checkEmpty(TxtNic.Text) = True Then
            Lblrequired1.Show()
        Else
            Lblrequired1.Hide()
        End If

    End Sub
    Private Sub TxtNic_Click(sender As Object, e As EventArgs) Handles TxtNic.Click
        Lblrequired1.Hide()
    End Sub




    Private Sub TxtFname_Leave(sender As Object, e As EventArgs) Handles TxtFname.Leave
        If checkEmpty(TxtFname.Text) = True Then
            Lblrequired2.Show()
        Else
            Lblrequired2.Hide()
        End If
    End Sub

    Private Sub TxtFname_Click(sender As Object, e As EventArgs) Handles TxtFname.Click
        Lblrequired2.Hide()
    End Sub





    Private Sub TxtAddress_Leave(sender As Object, e As EventArgs) Handles TxtAddress.Leave
        If checkEmpty(TxtAddress.Text) = True Then
            Lblrequired4.Show()
        Else
            Lblrequired4.Hide()
        End If
    End Sub

    Private Sub TxtAddress_Click(sender As Object, e As EventArgs) Handles TxtAddress.Click
        Lblrequired4.Hide()
    End Sub




    Private Sub TxtContact_Leave(sender As Object, e As EventArgs) Handles TxtContact.Leave
        If checkEmpty(TxtContact.Text) = True Then
            Lblrequired5.Show()
        Else
            Lblrequired5.Hide()
        End If
    End Sub

    Private Sub TxtContact_Click(sender As Object, e As EventArgs) Handles TxtContact.Click
        Lblrequired5.Hide()
    End Sub





    Private Sub TxtEmail_Leave(sender As Object, e As EventArgs) Handles TxtEmail.Leave
        If checkEmpty(TxtEmail.Text) = True Then
            LblRequired14.Show()
        Else
            LblRequired14.Hide()
        End If
    End Sub

    Private Sub TxtEmail_Click(sender As Object, e As EventArgs) Handles TxtEmail.Click
        LblRequired14.Hide()
    End Sub



    Private Sub ComboNatureOfFarming_Leave(sender As Object, e As EventArgs) Handles ComboNatureOfFarming.Leave
        If combo_empty(ComboNatureOfFarming) = True Then
            Lblrequired7.Show()
        Else
            Lblrequired7.Hide()
        End If
    End Sub

    Private Sub ComboNatureOfFarming_Click(sender As Object, e As EventArgs) Handles ComboNatureOfFarming.Click
        Lblrequired7.Hide()
    End Sub






    Private Sub ComboARODomain_Leave(sender As Object, e As EventArgs) Handles ComboARODomain.Leave
        If combo_empty(ComboARODomain) = True Then
            Lblrequired8.Show()
        Else
            Lblrequired8.Hide()
        End If
    End Sub

    Private Sub ComboARODomain_Click(sender As Object, e As EventArgs) Handles ComboARODomain.Click
        Lblrequired8.Hide()
    End Sub




    Private Sub ComboVODomain_Leave(sender As Object, e As EventArgs) Handles ComboVODomain.Leave
        If combo_empty(ComboVODomain) = True Then
            Lblrequired9.Show()
        Else
            Lblrequired9.Hide()
        End If
    End Sub

    Private Sub ComboVODomain_Click(sender As Object, e As EventArgs) Handles ComboVODomain.Click

        Lblrequired9.Hide()
    End Sub






    Private Sub TxtAccName_Leave(sender As Object, e As EventArgs)
        If checkEmpty(TxtAccName.Text) = True Then
            Lblrequired101.Show()
        Else
            Lblrequired101.Hide()
        End If
    End Sub

    Private Sub TxtAccName_Click(sender As Object, e As EventArgs)
        Lblrequired101.Hide()
    End Sub





    Private Sub TxtAccNo_Leave(sender As Object, e As EventArgs)
        If checkEmpty(TxtAccNo.Text) = True Then
            Lblrequired11.Show()
        Else
            Lblrequired11.Hide()
        End If
    End Sub

    Private Sub TxtAccNo_Click(sender As Object, e As EventArgs)
        Lblrequired11.Hide()
    End Sub







    Private Sub ComboBName_Leave(sender As Object, e As EventArgs)
        If combo_empty(ComboBName) = True Then
            Lblrequired12.Show()
        Else
            Lblrequired12.Hide()
        End If
    End Sub

    Private Sub ComboBName_Click(sender As Object, e As EventArgs)
        Lblrequired12.Hide()
    End Sub






    Private Sub TxtBranchCode_Leave(sender As Object, e As EventArgs)
        If checkEmpty(TxtAccNo.Text) = True Then
            Lblrequired13.Show()
        Else
            Lblrequired13.Hide()
        End If
    End Sub

    Private Sub TxtBranchCode_Click(sender As Object, e As EventArgs)
        Lblrequired13.Hide()
    End Sub




    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click


        'validate and chck empty value
        Dim errors As New ArrayList
        Dim NIC, Gender, msg, response As String
        NIC = TxtNic.Text
        Gender = ""
        If checkEmpty(TxtNic.Text) Or checkEmpty(TxtFname.Text) Or checkEmpty(TxtAddress.Text) Or checkEmpty(TxtContact.Text) Or checkEmpty(TxtEmail.Text) Or combo_empty(ComboNatureOfFarming) Or combo_empty(ComboARODomain) Or combo_empty(ComboVODomain) Or checkEmpty(TxtAccName.Text) Or combo_empty(ComboBName) Or checkEmpty(TxtAccNo.Text) Or checkEmpty(TxtBranchCode.Text) = True Then
            errors.Add("Fill All Recuried(*) Data")
        Else
            If NIC_valide(NIC) = False Then
                errors.Add("Enter correct NIC Format")
            End If

            If RdioMale.Checked Then
                Gender = "Male"
            ElseIf RdioFemale.Checked Then
                Gender = "Female"
            Else
                errors.Add("Select Your Gender.")
            End If

            If ValidEmail(TxtEmail.Text) = False Then
                errors.Add("Email is not valide. Enter Correct Email Address  ")

            End If

            If ValidTP(TxtContact.Text) = False Then
                errors.Add("Contact No is not valide. Enter Correct Contact No  ")
            End If

            If ValidTP(TxtContactP.Text) = False Then
                errors.Add("permanet Contact No is not valide. Enter Correct Contact No  ")

            End If

            If VALID_Name(TxtFname.Text) = False Then
                errors.Add("Full name is not valide")
            End If

            If ValidNu(TxtAccNo.Text) = False Then
                errors.Add("Account No is not valide")
            End If


            If ValidNu(TxtBranchCode.Text) = False Then
                errors.Add("Branch Code is not valied")

            End If


        End If

        If errors.Count >= 1 Then
            'if have error in errors array display it
            MessageBox.Show(String.Join(Environment.NewLine, errors.ToArray), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            'update data
            connectdb()
            Dim updates As String = "UPDATE `customer_register` SET `NIC`='" & TxtNic.Text & "',`Full_Name`='" & TxtFname.Text & "',`Address`='" & TxtAddress.Text & "',`Contact_No`='" & TxtContact.Text & "',`Contact_PNo`='" & TxtContactP.Text & "',`Gender`='" & Gender & "',`Email`='" & TxtEmail.Text & "',`Nature_Farmer`='" & ComboNatureOfFarming.SelectedItem & "',`ARO_Domain`='" & ComboARODomain.SelectedItem & "',`VO_Domain`='" & ComboVODomain.SelectedItem & "',`Acc_Name`='" & TxtAccName.Text & "',`Acc_No`='" & TxtAccNo.Text & "',`Bank_Name`='" & ComboBName.SelectedItem & "',`Branch_Code`='" & TxtBranchCode.Text & "' WHERE  `NIC`='" & TxtNic.Text & "'"
            Dim command As New MySqlCommand(updates, Connect)








            'conformation massage update data

            msg = "You're Going To Update The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "NIC no is : " & TxtNic.Text & ControlChars.NewLine & ControlChars.NewLine & "Customer name is : " & TxtFname.Text & ControlChars.NewLine & ControlChars.NewLine & "Address is : " & TxtAddress.Text & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & "This Customer all data Will be Update."


            response = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If response = MsgBoxResult.Yes Then
                command.ExecuteNonQuery()
                Connect.Close()
                MessageBox.Show("Data Update process was successfull.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'refresh data gride view

                Dim dt As New DataTable
                dt.Columns.Add("NIC")
                dt.Columns.Add("Full_Name")
                dt.Columns.Add("Address")
                dt.Columns.Add("Contact_No")
                dt.Columns.Add("Contact_PNo")
                dt.Columns.Add("Gender")
                dt.Columns.Add("E-Mail")
                dt.Columns.Add("Nature_Farmer")
                dt.Columns.Add("ARO_Domin")
                dt.Columns.Add("VO_Domain")
                dt.Columns.Add("ACC_Name")
                dt.Columns.Add("ACC_No")
                dt.Columns.Add("Bank_Name")
                dt.Columns.Add("Branch_Code")

                'assing to data relevent column
                Dim Cus_registor As List(Of CusRegister_clss) = GetCus()
                For Each r As CusRegister_clss In Cus_registor
                    dt.Rows.Add(r.Nic, r.Full_name, r.Address, r.Contat, r.Contact_P, r.Gender, r.Email, r.N_farming, r.ARO, r.VO_Domain, r.Acc_Name, r.Acc_No, r.Bank_Name, r.Branch_code)
                Next
                Cus_DataGridviwe.DataSource = dt



            Else
                MessageBox.Show("Data Update process NOT successfully.", "Update Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click

        TryCast(Cus_DataGridviwe.DataSource, DataTable).DefaultView.RowFilter = String.Format("Full_Name like'%" & TxtSearch.Text & "%'")
        TxtSearch.Text = ""
    End Sub
End Class