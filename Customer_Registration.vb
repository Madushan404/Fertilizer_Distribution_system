Imports System.Collections.ObjectModel
Imports MySql.Data.MySqlClient
Imports Mysqlx
Imports Org.BouncyCastle.Asn1.Cms

Public Class Customer_Registration




    Public command As New MySqlCommand


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


        'hide requre lable
        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()
        Lblrequired10.Hide()
        Lblrequired11.Hide()
        Lblrequired12.Hide()
        Lblrequired13.Hide()
        LblRequired14.Hide()
        LblUseralredy.Hide()

    End Sub






    Private Sub BtnAddNew_Click(sender As Object, e As EventArgs) Handles BtnAddNew.Click

        connectdb()
        'data insert part
        'create variable and assing input methord
        Dim NIC, FName, Address, customer_register, NIC_column, Contact, ContactP, Gender, Email, NatureOfFarming, ARO, VODomain, AccName, BankName, BranchCode As String
        Dim AccNo As String

        customer_register = "customer_register"
        NIC_column = "NIC"
        NIC = TxtNic.Text
        FName = TxtFname.Text
        Address = TxtAddress.Text
        Contact = TxtContact.Text
        ContactP = TxtContactP.Text
        Gender = ""
        Email = TxtEmail.Text
        NatureOfFarming = ComboNatureOfFarming.SelectedItem
        ARO = ComboARODomain.SelectedItem
        VODomain = ComboVODomain.SelectedItem
        AccName = TxtAccName.Text
        BankName = ComboBName.SelectedItem
        AccNo = TxtAccNo.Text
        BranchCode = TxtBranchCode.Text

        Dim errors As New ArrayList

        If checkEmpty(TxtNic.Text) Or checkEmpty(TxtFname.Text) Or checkEmpty(TxtAddress.Text) Or checkEmpty(TxtContact.Text) Or checkEmpty(TxtEmail.Text) Or combo_empty(ComboNatureOfFarming) Or combo_empty(ComboARODomain) Or combo_empty(ComboVODomain) Or checkEmpty(TxtAccName.Text) Or combo_empty(ComboBName) Or checkEmpty(TxtAccNo.Text) Or checkEmpty(TxtBranchCode.Text) = True Then
            errors.Add("Fill All Recuried(*) Data")
        Else
            'check user is unique and nic validation
            If unique_user(customer_register, NIC_column, NIC) = True Then
                If NIC_valide(NIC) = False Then
                    errors.Add("Enter correct NIC Format")
                End If
            Else
                errors.Add("User Already Registered")
            End If


            '
            If RdioMale.Checked Then
                Gender = "Male"
                Lblrequired6.Hide()
            ElseIf RdioFemale.Checked Then
                Gender = "Female"
                Lblrequired6.Hide()
            Else
                errors.Add("Select Your Gender.")
                Lblrequired6.Show()
            End If


            If ValidEmail(Email) = False Then
                errors.Add("Email is not valide. Enter Correct Email Address  ")

            End If

            If ValidTP(Contact) = False Then
                errors.Add("Contact No is not valide. Enter Correct Contact No  ")

            End If

            If ValidTP(ContactP) = False Then
                errors.Add("permanet Contact No is not valide. Enter Correct Contact No  ")

            End If

            If VALID_Name(FName) = False Then
                errors.Add("Full name is not valide")
            End If

            If ValidNu(AccNo) = False Then
                errors.Add("Account No is not valide")
            End If


            If ValidNu(BranchCode) = False Then
                errors.Add("Branch Code is not valied")

            End If
        End If

        If errors.Count >= 1 Then

            MessageBox.Show(String.Join(Environment.NewLine, errors.ToArray), "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            ' add  values to database using parameters 
            Dim para As String
            para = "insert into customer_register(NIC,Full_Name,Address,Contact_No,Contact_PNo,Gender,Email,Nature_Farmer,ARO_Domain,VO_Domain,Acc_Name,Acc_No,Bank_Name,Branch_Code) values (@NIC,@FName,@Address,@Contact,@ContactP,@Gender,@Email,@Nature,@ARo,@VO,@Accname,@AccNo,@Bankname,@Branchcode)"
            command = New MySqlCommand(para, Connect)
            command.Parameters.AddWithValue("@NIC", NIC)
            command.Parameters.AddWithValue("@FName", FName)
            'command.Parameters.AddWithValue("@NWInitial", NWinitial)
            command.Parameters.AddWithValue("@Address", Address)
            command.Parameters.AddWithValue("@Contact", Contact)
            command.Parameters.AddWithValue("@ContactP", ContactP)
            command.Parameters.AddWithValue("@Gender", Gender)
            'command.Parameters.AddWithValue("@FID", FId)
            command.Parameters.AddWithValue("@Email", Email)
            command.Parameters.AddWithValue("@Nature", NatureOfFarming)
            command.Parameters.AddWithValue("@ARo", ARO)
            command.Parameters.AddWithValue("@VO", VODomain)
            command.Parameters.AddWithValue("@Accname", AccName)
            command.Parameters.AddWithValue("@AccNo", AccNo)
            command.Parameters.AddWithValue("@Bankname", BankName)
            command.Parameters.AddWithValue("@Branchcode", BranchCode)


            'check data is entered in the table
            Dim check_data_save As Integer = command.ExecuteNonQuery
            If check_data_save = 1 Then
                MessageBox.Show("User Registration successfully.", "Sucsessful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                '  after registor clear enter data
                TxtNic.Clear()
                TxtFname.Clear()
                'TxtNWInitial.Clear()
                TxtAddress.Clear()
                TxtContact.Clear()
                TxtContactP.Clear()
                'TxtFarmerID.Clear()
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



            Else
                MessageBox.Show("Registration Fail", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        End If


    End Sub




    'show and hide recuired labale

    Private Sub TxtNic_Leave(sender As Object, e As EventArgs) Handles TxtNic.Leave
        connectdb()

        If checkEmpty(TxtNic.Text) = True Then
            Lblrequired1.Show()
        Else
            Lblrequired1.Hide()
        End If

        Dim customer_register, NIC_column, NIC As String
        customer_register = "customer_register"
        NIC_column = "NIC"
        NIC = TxtNic.Text
        If unique_user(customer_register, NIC_column, NIC) = False Then
            LblUseralredy.Show()
            BtnAddNew.Hide()
        Else
            LblUseralredy.Hide()
            BtnAddNew.Show()

        End If


    End Sub
    Private Sub TxtNic_Click(sender As Object, e As EventArgs) Handles TxtNic.Click
        Lblrequired1.Hide()
        LblUseralredy.Hide()

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






    Private Sub TxtAccName_Leave(sender As Object, e As EventArgs) Handles TxtAccName.Leave
        If checkEmpty(TxtAccName.Text) = True Then
            Lblrequired10.Show()
        Else
            Lblrequired10.Hide()
        End If
    End Sub

    Private Sub TxtAccName_Click(sender As Object, e As EventArgs) Handles TxtAccName.Click
        Lblrequired10.Hide()
    End Sub





    Private Sub TxtAccNo_Leave(sender As Object, e As EventArgs) Handles TxtAccNo.Leave
        If checkEmpty(TxtAccNo.Text) = True Then
            Lblrequired11.Show()
        Else
            Lblrequired11.Hide()
        End If
    End Sub

    Private Sub TxtAccNo_Click(sender As Object, e As EventArgs) Handles TxtAccNo.Click
        Lblrequired11.Hide()
    End Sub







    Private Sub ComboBName_Leave(sender As Object, e As EventArgs) Handles ComboBName.Leave
        If combo_empty(ComboBName) = True Then
            Lblrequired12.Show()
        Else
            Lblrequired12.Hide()
        End If
    End Sub

    Private Sub ComboBName_Click(sender As Object, e As EventArgs) Handles ComboBName.Click
        Lblrequired12.Hide()
    End Sub






    Private Sub TxtBranchCode_Leave(sender As Object, e As EventArgs) Handles TxtBranchCode.Leave
        If checkEmpty(TxtAccNo.Text) = True Then
            Lblrequired13.Show()
        Else
            Lblrequired13.Hide()
        End If
    End Sub

    Private Sub TxtBranchCode_Click(sender As Object, e As EventArgs) Handles TxtBranchCode.Click
        Lblrequired13.Hide()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Customer_Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
        Lblrequired7.Hide()
        Lblrequired8.Hide()
        Lblrequired9.Hide()
        Lblrequired10.Hide()
        Lblrequired11.Hide()
        Lblrequired12.Hide()
        Lblrequired13.Hide()
        LblRequired14.Hide()
        LblUseralredy.Hide()


    End Sub
    Dim customer_register, NIC_column, NIC As String






End Class