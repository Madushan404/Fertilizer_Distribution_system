Imports Google.Protobuf.Reflection.FieldOptions.Types
Imports K4os.Compression.LZ4.Streams
Imports System.IO.Pipelines
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Windows.Documents

Public Class Fertilizer_Suplier_Update
    Private Sub Fertilizer_Suplier_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired3.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()
        Lblrequired10.Hide()
        Lblrequired11.Hide()

        GridviweSuplierUpdate.DataSource = sup_reg_gride()

        With GridviweSuplierUpdate

            .Columns(0).HeaderText = "Sup_ID"
            .Columns(1).HeaderText = "Company_name"
            .Columns(2).HeaderText = "Contact_person"
            .Columns(3).HeaderText = "Address"
            .Columns(4).HeaderText = "Contact_No"
            .Columns(5).HeaderText = "Whatsapp_no"
            .Columns(6).HeaderText = "Website"
            .Columns(7).HeaderText = "Email"
            .Columns(8).HeaderText = "Product_offered"
            .Columns(9).HeaderText = "Acc_Name"
            .Columns(10).HeaderText = "Acc_Number"
            .Columns(11).HeaderText = "Bank_Name"
            .Columns(12).HeaderText = "Barnch_code"
        End With




    End Sub

    Private Sub TxtCompanyName_Leave(sender As Object, e As EventArgs) Handles TxtCompanyName.Leave
        If checkEmpty(TxtCompanyName.Text) = True Then
            Lblrequired2.Show()
        Else
            Lblrequired2.Hide()
        End If
    End Sub

    Private Sub TxtCompanyName_Click(sender As Object, e As EventArgs) Handles TxtCompanyName.Click
        Lblrequired2.Hide()
    End Sub

    Private Sub TxtCperson_Leave(sender As Object, e As EventArgs) Handles TxtCperson.Leave
        If checkEmpty(TxtCompanyName.Text) = True Then
            Lblrequired3.Show()
        Else
            Lblrequired3.Hide()
        End If
    End Sub

    Private Sub TxtCperson_Click(sender As Object, e As EventArgs) Handles TxtCperson.Click
        Lblrequired3.Hide()
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

    Private Sub TxtWhatsapp_Leave(sender As Object, e As EventArgs) Handles TxtWhatsapp.Leave
        If checkEmpty(TxtWhatsapp.Text) = True Then
            Lblrequired6.Show()
        Else
            Lblrequired6.Hide()
        End If
    End Sub

    Private Sub TxtWhatsapp_Click(sender As Object, e As EventArgs) Handles TxtWhatsapp.Click
        Lblrequired6.Hide()
    End Sub

    Private Sub TxtEmail_Leave(sender As Object, e As EventArgs) Handles TxtEmail.Leave
        If checkEmpty(TxtEmail.Text) = True Then
            Lblrequired7.Show()
        Else
            Lblrequired7.Hide()
        End If
    End Sub

    Private Sub TxtEmail_Click(sender As Object, e As EventArgs) Handles TxtEmail.Click
        Lblrequired7.Hide()
    End Sub

    Private Sub TxtAccName_Leave(sender As Object, e As EventArgs) Handles TxtAccName.Leave
        If checkEmpty(TxtAccName.Text) = True Then
            Lblrequired9.Show()
        Else
            Lblrequired9.Hide()
        End If
    End Sub

    Private Sub TxtAccName_Click(sender As Object, e As EventArgs) Handles TxtAccName.Click
        Lblrequired9.Hide()
    End Sub

    Private Sub TxtAccNo_Leave(sender As Object, e As EventArgs) Handles TxtAccNo.Leave
        If checkEmpty(TxtAccNo.Text) = True Then
            Lblrequired10.Show()
        Else
            Lblrequired10.Hide()
        End If
    End Sub

    Private Sub TxtAccNo_Click(sender As Object, e As EventArgs) Handles TxtAccNo.Click
        Lblrequired10.Hide()
    End Sub

    Private Sub TxtBranchCode_Leave(sender As Object, e As EventArgs) Handles TxtBranchCode.Leave
        If checkEmpty(TxtBranchCode.Text) = True Then
            Lblrequired11.Show()
        Else
            Lblrequired11.Hide()
        End If
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click


        Dim errors As New ArrayList
        Dim NIC, Gender, msg, response As String

        If checkEmpty(TxtCompanyName.Text) Or checkEmpty(TxtCperson.Text) Or checkEmpty(TxtAddress.Text) Or checkEmpty(TxtContact.Text) Or checkEmpty(TxtEmail.Text) Or checkEmpty(TxtWhatsapp.Text) Or checkEmpty(TxtAccName.Text) Or combo_empty(ComboBName) Or checkEmpty(TxtAccNo.Text) Or checkEmpty(TxtBranchCode.Text) = True Then
            errors.Add("Fill All Recuried(*) Data")
        Else

            'validation part






        End If

        Dim checklist() As String = {"Item 1", "Item 2", "Item 3", "Item 4", "Item 5"}
        ' Create a StringBuilder to store the checklist output
        Dim checklistOutput As New StringBuilder()

        ' Iterate through the checklist and append each item to the StringBuilder
        For Each item As String In checklist
            checklistOutput.AppendLine(item)
        Next

        ' Convert the StringBuilder to a single string
        Dim checklistResult As String = checklistOutput.ToString()

        If errors.Count >= 1 Then
            'if have error in errors array display it
            MessageBox.Show(String.Join(Environment.NewLine, errors.ToArray), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            'update data
            connectdb()
            Dim updates As String = "UPDATE suplier_registration SET Company_Name= @ftype ,Contact_Person= @FName,Address=@mfd,Contact_No=@exd,Whatsapp_No=@bno,Web_site=@txture,Email=@price,Products_offers=@country,Acc_Name=@accname,Acc_No=@accno,Bank_Name=@bankname,Branch_Code=@bcode  WHERE  	Sup_ID =@supid "
            Dim command As New MySqlCommand(updates, Connect)

            command.Parameters.AddWithValue("@ftype", TxtCompanyName.Text)
            command.Parameters.AddWithValue("@FName", TxtCperson.Text)
            command.Parameters.AddWithValue("@supid", TxtSupId.Text)
            command.Parameters.AddWithValue("@bno", TxtWhatsapp.Text)
            command.Parameters.AddWithValue("@mfd", TxtAddress.Text)
            command.Parameters.AddWithValue("@exd", TxtContact.Text)
            command.Parameters.AddWithValue("@price", TxtEmail.Text)

            command.Parameters.AddWithValue("@txture", TxtWeb.Text)
            command.Parameters.AddWithValue("@country", checklistResult)
            command.Parameters.AddWithValue("@accname", TxtAccName.Text)
            command.Parameters.AddWithValue("@accno", TxtAccNo.Text)
            command.Parameters.AddWithValue("@bankname", ComboBName.SelectedItem)
            command.Parameters.AddWithValue("@bcode", TxtBranchCode.Text)








            'conformation massage update data

            msg = "You're Going To Update The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "NIC no is : " & TxtSupId.Text & ControlChars.NewLine & ControlChars.NewLine & "Customer name is : " & TxtCompanyName.Text & ControlChars.NewLine & ControlChars.NewLine & "Address is : " & TxtAddress.Text & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & "This Customer all data Will be Update."


            response = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If response = MsgBoxResult.Yes Then
                command.ExecuteNonQuery()
                Connect.Close()
                MessageBox.Show("Data Update process was successfull.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information)


            End If
        End If



    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        TxtSupId.Clear()
        TxtCompanyName.Clear()

        TxtAddress.Clear()
        TxtContact.Clear()
        TxtWhatsapp.Clear()

        TxtEmail.Clear()
        TxtCperson.Clear()

        'For i As Integer = 0 To CheckedListProducts.Items.Count - 1
        'CheckedListProducts.Items(i).Selected = False
        'Next
        TxtAccName.Clear()
        ComboBName.SelectedIndex = -1
        TxtAccNo.Clear()
        TxtBranchCode.Clear()
        TxtWeb.Clear()
    End Sub

    Private Sub Btndelete_Click(sender As Object, e As EventArgs) Handles Btndelete.Click
        Dim query As String = "DELETE FROM  suplier_registration WHERE Fertilizer_ID= @ID"
        Using cmd As New MySqlCommand(query, Connect)
            cmd.Parameters.AddWithValue("@ID", TxtSupId.Text)

            'confirm Message to delete data
            Dim response, Msg, fid, Fname, ftype As String
            fid = TxtSupId.Text
            ftype = TxtCompanyName.Text
            Fname = TxtCperson.Text

            Msg = "You're Going To Delete The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer Suplier ID is : " & fid & ControlChars.NewLine & ControlChars.NewLine & "Company name is : " & Fname & ControlChars.NewLine & ControlChars.NewLine & "Contact person Type  is : " & ftype & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & " All data related to the Fertilizer Will be Permanently Deleted. This Process Can't Be Undone"


            response = MessageBox.Show(Msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)


            If response = MsgBoxResult.Yes Then
                cmd.ExecuteNonQuery()
                Connect.Close()
                MessageBox.Show("Data Delete process was successfull.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Data delete process NOT successfully.", "Delete Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub GridviweSuplierUpdate_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridviweSuplierUpdate.CellClick

        Dim dr As DataGridViewRow = GridviweSuplierUpdate.SelectedRows(0)

        TxtSupId.Text = dr.Cells(0).Value.ToString()
        TxtCompanyName.Text = dr.Cells(1).Value.ToString()
        TxtCperson.Text = dr.Cells(2).Value.ToString()

        TxtAddress.Text = dr.Cells(3).Value.ToString()
        TxtContact.Text = dr.Cells(4).Value.ToString()
        TxtWhatsapp.Text = dr.Cells(5).Value.ToString()
        TxtEmail.Text = dr.Cells(7).Value.ToString()
        TxtWeb.Text = dr.Cells(6).Value.ToString()
        TxtAccName.Text = dr.Cells(9).Value.ToString()
        ComboBName.Text = dr.Cells(11).Value.ToString()
        TxtAccNo.Text = dr.Cells(10).Value.ToString()
        TxtBranchCode.Text = dr.Cells(12).Value.ToString()

    End Sub
End Class