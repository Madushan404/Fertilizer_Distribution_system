Imports Google.Protobuf.Reflection.FieldOptions.Types
Imports K4os.Compression.LZ4.Streams
Imports MySql.Data.MySqlClient
Imports System.IO.Pipelines

Public Class Fertilizer_Update
    Private Sub Fertilizer_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired3.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()


        connectdb()
        GridViewFertilizer.DataSource = fertilizer_reg_gride()

        With GridViewFertilizer

            .Columns(0).HeaderText = "Fertilizer_ID"
            .Columns(1).HeaderText = "Fertilizer_Type"
            .Columns(2).HeaderText = "Fertilizer_Name"
            .Columns(3).HeaderText = "MF_Date"
            .Columns(4).HeaderText = "EX_Date"
            .Columns(5).HeaderText = "Batch_No"
            .Columns(6).HeaderText = "Texture"
            .Columns(7).HeaderText = "Unit_Price"
            .Columns(8).HeaderText = "Made_Contry"
        End With

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        TxtBNo.Clear()
        TxtFID.Clear()
        TxtFName.Clear()
        TxtFType.Clear()
        TxtMade.Clear()
        TxtPrice.Clear()
        TxtSearch.Clear()
        ComboTexture.SelectedIndex = -1
        DateManifacture.Value = Now()
        DateExpier.Value = Now()
    End Sub







    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click




        connectdb()
        'data insert part

        Dim fID, ftype, fname, batch_no, Price, Texture, Made As String
        Dim MFD, EXD As DateTime


        fID = TxtFID.Text
        ftype = TxtFType.Text
        fname = TxtFName.Text
        batch_no = TxtBNo.Text
        MFD = DateManifacture.Value
        EXD = DateExpier.Value
        Price = TxtPrice.Text
        Texture = ComboTexture.SelectedItem
        Made = TxtMade.Text



        Dim errors As New ArrayList
        Dim msg, response As String


        If checkEmpty(ftype) Or checkEmpty(fname) Or checkEmpty(batch_no) Or checkEmpty(Price) Or combo_empty(ComboTexture) Or checkEmpty(Made) Or date_Change(MFD) Or date_Change(EXD) = True Then
            errors.Add("Fill All Recuried(*) Data")
        Else
            If ValidPrice(Price) = False Then
                errors.Add("Enter price in correct format ( ex: 250.50)")
            End If


            If DateManifacture.Value > DateExpier.Value Then
                errors.Add("Expier date should be more than Manifacture date")
            End If



            If VALID_Name(Made) = False Then
                errors.Add("Organic country name is not valide")
            End If


            If ValidFertilizerName(fname) = False Then
                errors.Add("Fertilizer name is not valied (You can't use special charactors for naming process)")
            End If

            If ValidFertilizerName(ftype) = False Then

                errors.Add("Fertilizer Type is not  valied (You can't use special charactors for F-type naming process)")
            End If


            If ValidModelNumber(batch_no) = False Then
                errors.Add("Batch Number is not valied (You can use only number and letter)")
            End If

        End If




        If errors.Count >= 1 Then

            MessageBox.Show(String.Join(Environment.NewLine, errors.ToArray), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            'update data
            connectdb()


            Dim updates As String = "UPDATE fertilizer_registration SET Fertilizer_Type= @ftype ,Fertilizer_Name= @FName,MFD_date=@mfd,EXP_date=@exd,Btch_no=@bno,Texture=@txture,Unit_Price=@price,Made_country=@country  WHERE  Fertilizer_ID =@fID "
            Dim command As New MySqlCommand(updates, Connect)


            command.Parameters.AddWithValue("@ftype", ftype)
            command.Parameters.AddWithValue("@FName", fname)
            command.Parameters.AddWithValue("@FID", fID)
            command.Parameters.AddWithValue("@bno", batch_no)
            command.Parameters.AddWithValue("@mfd", MFD)
            command.Parameters.AddWithValue("@exd", EXD)
            command.Parameters.AddWithValue("@price", Price)

            command.Parameters.AddWithValue("@txture", Texture)
            command.Parameters.AddWithValue("@country", Made)




            'conformation massage update data

            msg = "You're Going To Update The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer ID no is : " & TxtFID.Text & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer type is : " & TxtFType.Text & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer name is : " & TxtFName.Text & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & "This Fertilizer all data Will be Update."


            response = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)



            If response = MsgBoxResult.Yes Then
                command.ExecuteNonQuery()
                Connect.Close()
                MessageBox.Show("Data Update process was successfull.", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'refresh data gride view
                connectdb()
                GridViewFertilizer.DataSource = fertilizer_reg_gride()

                With GridViewFertilizer

                    .Columns(0).HeaderText = "Fertilizer_ID"
                    .Columns(1).HeaderText = "Fertilizer_Type"
                    .Columns(2).HeaderText = "Fertilizer_Name"
                    .Columns(3).HeaderText = "MF_Date"
                    .Columns(4).HeaderText = "EX_Date"
                    .Columns(5).HeaderText = "Batch_No"
                    .Columns(6).HeaderText = "Texture"
                    .Columns(7).HeaderText = "Unit_Price"
                    .Columns(8).HeaderText = "Made_Contry"
                End With



                'assing to data relevent column
                ' Dim Cus_registor As List(Of Fartilizer_Reg_Class) = GetCus()
                'For Each r As Fartilizer_Reg_Class In Cus_registor
                'dt.Rows.Add(r., r.Full_name, r.Address, r.Contat, r.Contact_P, r.Gender, r.Email, r.N_farming, r.ARO, r.VO_Domain, r.Acc_Name, r.Acc_No, r.Bank_Name, r.Branch_code)
                'Next
                'GridViewFertilizer.DataSource = dt



            Else
                MessageBox.Show("Data Update process NOT successfully.", "Update Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        End If
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        TryCast(GridViewFertilizer.DataSource, DataTable).DefaultView.RowFilter = String.Format("Fertilizer_Name like'%" & TxtSearch.Text & "%'")
        TxtSearch.Text = ""
    End Sub

    Private Sub GridViewFertilizer_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridViewFertilizer.CellClick

        Dim dr As DataGridViewRow = GridViewFertilizer.SelectedRows(0)

        TxtFID.Text = dr.Cells(0).Value.ToString()
        TxtFType.Text = dr.Cells(1).Value.ToString()
        TxtFName.Text = dr.Cells(2).Value.ToString()
        DateManifacture.Value = dr.Cells(3).Value.ToString()
        DateExpier.Value = dr.Cells(4).Value.ToString()
        TxtBNo.Text = dr.Cells(5).Value.ToString()
        ComboTexture.Text = dr.Cells(6).Value.ToString()
        TxtPrice.Text = dr.Cells(7).Value.ToString()
        TxtMade.Text = dr.Cells(8).Value.ToString()



    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim query As String = "DELETE FROM  fertilizer_registration WHERE Fertilizer_ID= @ID"
        Using cmd As New MySqlCommand(query, Connect)
            cmd.Parameters.AddWithValue("@ID", TxtFID.Text)

            'confirm Message to delete data
            Dim response, Msg, fid, Fname, ftype, batch_no As String
            fid = TxtFID.Text
            ftype = TxtFType.Text
            Fname = TxtFName.Text
            batch_no = TxtBNo.Text
            Msg = "You're Going To Delete The," & ControlChars.NewLine & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer ID is : " & fid & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer name is : " & Fname & ControlChars.NewLine & ControlChars.NewLine & "Fertilizer Type  is : " & ftype & ControlChars.NewLine & ControlChars.NewLine & "Do You Confirm This?" & ControlChars.NewLine & ControlChars.NewLine & " All data related to the Fertilizer Will be Permanently Deleted. This Process Can't Be Undone"


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
        connectdb()
        GridViewFertilizer.DataSource = fertilizer_reg_gride()

        With GridViewFertilizer

            .Columns(0).HeaderText = "Fertilizer_ID"
            .Columns(1).HeaderText = "Fertilizer_Type"
            .Columns(2).HeaderText = "Fertilizer_Name"
            .Columns(3).HeaderText = "MF_Date"
            .Columns(4).HeaderText = "EX_Date"
            .Columns(5).HeaderText = "Batch_No"
            .Columns(6).HeaderText = "Texture"
            .Columns(7).HeaderText = "Unit_Price"
            .Columns(8).HeaderText = "Made_Contry"
        End With
    End Sub
End Class