
Imports MySql.Data.MySqlClient

Public Class Fertilizer_Registration
    Private Sub Fertilizer_Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired3.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()

    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        TxtBNo.Clear()

        TxtFName.Clear()
        TxtFType.Clear()
        TxtMade.Clear()
        TxtPrice.Clear()

        ComboTexture.SelectedIndex = -1
        DateManifacture.Value = Now()
        DateExpier.Value = Now()
    End Sub

    Private Sub BtnAddnew_Click(sender As Object, e As EventArgs) Handles BtnAddnew.Click
        connectdb()
        'data insert part
        'create variable and assing input methord
        Dim ftype, fname, batch_no, Price, Texture, Made, table_name, Column_name As String
        Dim MFD, EXD As DateTime



        table_name = "fertilizer_registration"
        'Column_name = ""
        ftype = TxtFType.Text
        fname = TxtFName.Text
        batch_no = TxtBNo.Text
        MFD = DateManifacture.Value
        EXD = DateExpier.Value
        Price = TxtPrice.Text
        Texture = comboTexture.SelectedItem
        Made = TxtMade.Text


        Dim errors As New ArrayList

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

            ' add  values to database using parameters 
            Dim para As String
            para = "insert into fertilizer_registration (Fertilizer_Type ,Fertilizer_Name,Btch_no,MFD_date,EXP_date,Unit_Price,Texture,Made_country) values (@ftype,@FName,@bno,@mfd,@exd,@price,@txture,@country)"
            command = New MySqlCommand(para, Connect)
            command.Parameters.AddWithValue("@ftype", ftype)
            command.Parameters.AddWithValue("@FName", fname)

            command.Parameters.AddWithValue("@bno", batch_no)
            command.Parameters.AddWithValue("@mfd", MFD)
            command.Parameters.AddWithValue("@exd", EXD)
            command.Parameters.AddWithValue("@price", Price)

            command.Parameters.AddWithValue("@txture", Texture)
            command.Parameters.AddWithValue("@country", Made)


            'check data is entered in the table
            Dim check_data_save As Integer = command.ExecuteNonQuery
            If check_data_save = 1 Then
                MessageBox.Show("Fertilizer Registration successfully.", "Sucsessful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                '  after registor clear enter data
                TxtBNo.Clear()

                TxtFName.Clear()
                TxtFType.Clear()
                TxtMade.Clear()
                TxtPrice.Clear()

                ComboTexture.SelectedIndex = -1
                DateManifacture.Value = Now()
                DateExpier.Value = Now()



            Else
                MessageBox.Show("Registration Fail", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        End If

    End Sub

    Private Sub TxtFType_Leave(sender As Object, e As EventArgs) Handles TxtFType.Leave
        If checkEmpty(TxtFType.Text) = True Then
            Lblrequired2.Show()
        Else
            Lblrequired2.Hide()

        End If
    End Sub

    Private Sub TxtFType_Click(sender As Object, e As EventArgs) Handles TxtFType.Click
        Lblrequired2.Hide()
    End Sub

    Private Sub TxtFName_Leave(sender As Object, e As EventArgs) Handles TxtFName.Leave
        If checkEmpty(TxtFName.Text) = True Then
            Lblrequired4.Show()
        Else
            Lblrequired4.Hide()

        End If
    End Sub

    Private Sub TxtFName_Click(sender As Object, e As EventArgs) Handles TxtFName.Click
        Lblrequired4.Hide()

    End Sub

    Private Sub TxtBNo_Leave(sender As Object, e As EventArgs) Handles TxtBNo.Leave
        If checkEmpty(TxtBNo.Text) = True Then
            Lblrequired6.Show()
        Else
            Lblrequired6.Hide()

        End If
    End Sub

    Private Sub TxtBNo_Click(sender As Object, e As EventArgs) Handles TxtBNo.Click
        Lblrequired6.Hide()
    End Sub

    Private Sub DateManifacture_Leave(sender As Object, e As EventArgs) Handles DateManifacture.Leave
        If date_Change(DateManifacture.Text) = True Then
            Lblrequired3.Show()
        Else
            Lblrequired3.Hide()

        End If
    End Sub

    Private Sub DateManifacture_Click(sender As Object, e As EventArgs) Handles DateManifacture.Click
        Lblrequired3.Hide()
    End Sub

    Private Sub DateExpier_Leave(sender As Object, e As EventArgs) Handles DateExpier.Leave
        If date_Change(DateExpier.Text) = True Then
            Lblrequired5.Show()
        Else
            Lblrequired5.Hide()

        End If
    End Sub

    Private Sub DateExpier_Click(sender As Object, e As EventArgs) Handles DateExpier.Click

        Lblrequired5.Hide()
    End Sub

    Private Sub TxtPrice_Leave(sender As Object, e As EventArgs) Handles TxtPrice.Leave
        If checkEmpty(TxtPrice.Text) = True Then
            Lblrequired8.Show()
        Else
            Lblrequired8.Hide()

        End If
    End Sub

    Private Sub TxtPrice_Click(sender As Object, e As EventArgs) Handles TxtPrice.Click
        Lblrequired8.Hide()
    End Sub

    Private Sub ComboTexture_Leave(sender As Object, e As EventArgs) Handles ComboTexture.Leave
        If combo_empty(ComboTexture) = True Then
            Lblrequired7.Show()
        Else
            Lblrequired7.Hide()

        End If
    End Sub

    Private Sub ComboTexture_Click(sender As Object, e As EventArgs) Handles ComboTexture.Click
        Lblrequired7.Hide()
    End Sub

    Private Sub TxtMade_Leave(sender As Object, e As EventArgs) Handles TxtMade.Leave
        If checkEmpty(TxtMade.Text) = True Then
            Lblrequired9.Show()
        Else
            Lblrequired9.Hide()

        End If
    End Sub

    Private Sub TxtMade_Click(sender As Object, e As EventArgs) Handles TxtMade.Click
        Lblrequired9.Hide()
    End Sub
End Class