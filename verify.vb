Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Module verify
    Private ReadOnly host As String = "localhost"
    Private ReadOnly user As String = "root"
    Private ReadOnly password As String = ""
    Private ReadOnly database As String = "fertilizer"
    Public Connect = New MySqlConnection
    Public command = New MySqlCommand
    Public command1 = New MySqlCommand
    Dim str, str1 As String
    Dim grid_reader As IDataReader
    Dim grid As IDataReader


    Public Sub connectdb()

        'database connect


        Connect = New MySqlConnection

        Dim str As String = String.Format("host={0};user={1};password={2};database={3};", host, user, password, database)

        Connect.connectionstring = str

        Try
            If Connect.state = ConnectionState.Closed Then
                Connect.connectionstring = str

                Connect.open()


            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try


    End Sub

    Public Function password_user(ByVal u_name As String, ByVal password As String) As Boolean
        'check user name and password is regidtored 
        ' if registored return value true
        Dim r As Boolean = False

        Dim str = "SELECT COUNT(`UserName`) FROM `agent` WHERE UserName= @uname and Password=@password;"

        Try
            command = New MySqlCommand(str, Connect)
            command.Parameters.AddWithValue("@uname", u_name)
            command.Parameters.AddWithValue("@password", password)
            Dim result As Integer = command.ExecuteScalar()
            If result = 1 Then

                r = True

            End If
            command.Parameters.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return r

    End Function


    Public Function unique_user(ByVal table_name As String, ByVal column_name As String, ByVal textbox_name As String) As Boolean
        'check user alredy registored or not
        ' if user registored return false other vise true
        Dim r As Boolean = False

        Dim str = "SELECT COUNT(" + column_name + ") FROM " + table_name + " WHERE " + column_name + "= @u_name"

        'MsgBox(str)

        Try
            command = New MySqlCommand(str, Connect)

            command.Parameters.AddWithValue("@u_name", textbox_name)

            Dim result As Integer = command.ExecuteScalar()

            If result = 0 Then

                r = True

            End If

            command.Parameters.Clear()

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Return r

    End Function



    Function date_Change(ByVal combo_date As String) As Boolean

        If combo_date = Now() Then
            Return True

        End If
        Return False


    End Function



    Public Function ValidFertilizerName(ByVal name As String) As Boolean
        '  fertilizer name validation.
        Dim pattern As String = "^[A-Za-z0-9\s-]+$"
        Dim regex As New Regex(pattern)
        If regex.IsMatch(name) Then
            Return True

        End If
        Return False

    End Function


    Public Function ValidModelNumber(ByVal modelNumber As String) As Boolean
        '  model number validation.
        Dim pattern As String = "^[A-Za-z0-9\-]+$"
        Dim regex As New Regex(pattern)
        If regex.IsMatch(modelNumber) Then
            Return True
        End If
        Return False
    End Function


    Public Function ValidPrice(ByVal price As String) As Boolean
        '  price validation.
        Dim pattern As String = "^\d+(\.\d{1,2})?$"
        Dim regex As New Regex(pattern)
        If regex.IsMatch(price) Then
            Return True

        End If
        Return False
    End Function





    Function min_length(ByVal str As String, length As Integer) As Boolean
        'check input tex length is correct

        Dim resuts As Boolean = False

        If str.Length < length Then

            resuts = True

        End If

        Return resuts

    End Function



    Function combo_empty(ByVal com As ComboBox) As Boolean
        'check combo box value is empty or not
        Dim results As Boolean = True
        If com.SelectedIndex > -1 Then
            results = False
        End If

        Return results


    End Function



    Function NIC_valide(ByVal nic As String) As Boolean
        Dim valid As Boolean = False
        If nic.Length = 10 Then
            'create pattern 
            Dim pattern As String = "^[0-9]{9}[vV]$"
            Dim regex As New Regex(pattern)
            If regex.IsMatch(nic) Then
                'input data same as pattern
                valid = True
                Return valid
            Else
                Return valid
            End If
        ElseIf nic.Length = 12 And IsNumeric(nic) Then
            valid = True
            Return valid
        Else
            Return valid
        End If


    End Function





    Function checkEmpty(ByVal str As String) As Boolean
        str = str.Trim
        If String.IsNullOrEmpty(str) Then
            Return True

        End If
        Return False
    End Function


    Function farmer_id(ByVal id As Integer, ByVal com As ComboBox, ByVal nic As String) As Boolean
        Try
            If IsNumeric(id) = True And checkEmpty(id) = False Then
                If id.ToString.Substring(0, 2) = 16 And id.ToString.Substring(3, 2) = 21 Then


                    If combo_empty(com) = False Then
                        If id.ToString.Substring(5, 2) = com.SelectedItem() Then


                            If nic.Length = 10 Then
                                'create pattern 
                                Dim pattern As String = "^[0-9]{9}[vV]$"
                                Dim regex As New Regex(pattern)
                                If regex.IsMatch(nic) Then
                                    'input data same as pattern
                                    If id.ToString.Substring(7, 9) = nic.ToString.Substring(0, 9) Then

                                    End If

                                End If


                            ElseIf nic.Length = 12 And IsNumeric(nic) Then
                                If id.ToString.Substring(7, 9) = nic.ToString.Substring(3, 9) Then

                                End If
                            End If


                        End If

                    Else
                        MessageBox.Show("Choose Your VO Domain", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    End If
                End If
            Else
                MessageBox.Show("Eneter farmer ID no", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function ValidEmail(ByVal email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex As New Regex(pattern)

        If regex.IsMatch(email) Then
            Return True
        End If
        Return False



    End Function



    Public Function ValidTP(ByVal phoneNumber As String) As Boolean
        'phone no validation
        Dim pattern As String = "^\d{10}$"
        Dim regex As New Regex(pattern)

        If regex.IsMatch(phoneNumber) Then
            Return True
        End If
        Return False

    End Function


    'Name Validation
    Public Function VALID_Name(ByVal name As String) As Boolean
        Dim pattern As String = "^[a-zA-Z]+(?:[\s.'\-,][a-zA-Z]+)*$"
        Dim regex As New Regex(pattern)

        If regex.IsMatch(name) Then
            Return True
        End If
        Return False
    End Function




    'number validation
    Public Function ValidNu(ByVal numbers As String) As Boolean
        Dim pattern As String = "^[0-9]+$"
        Dim regex As New Regex(pattern)

        If regex.IsMatch(numbers) Then
            Return True
        End If
        Return False
    End Function



    Public Function fertilizer_reg_gride() As DataTable

        Dim f_Reg_table As New DataTable
        str = "select * from fertilizer_registration"
        command = New MySqlCommand(Str, Connect)
        grid_reader = command.ExecuteReader
        f_Reg_table.Load(grid_reader)

        Return f_Reg_table
    End Function

    Public Function sup_reg_gride() As DataTable
        connectdb()
        Dim sup_Reg_table As New DataTable
        str = "select * from `suplier_registration`"
        command.commandText = str
        command.connection = Connect
        grid_reader = command.ExecuteReader
        sup_Reg_table.Load(grid_reader)

        Return sup_Reg_table
    End Function





    Public Function GetCus() As List(Of CusRegister_clss)
        'get registor data to grid view

        Dim Cus_registor As New List(Of CusRegister_clss)
        connectdb()
        Dim t = "select * from customer_register;"
        Dim command As New MySqlCommand(t, Connect)
        Dim result = command.ExecuteReader
        If result.HasRows Then
            While result.Read
                Cus_registor.Add(New CusRegister_clss With {.Nic = result("NIC"), .Full_name = result("Full_Name"), .Address = result("Address"), .Contat = result("Contact_No"), .Contact_P = result("Contact_PNo"), .Gender = result("Gender"), .Email = result("EMail"), .N_farming = result("Nature_Farmer"), .ARO = result("ARO_Domain"), .VO_Domain = result("VO_Domain"), .Acc_Name = result("ACC_Name"), .Acc_No = result("ACC_No"), .Bank_Name = result("Bank_Name"), .Branch_code = result("Branch_Code")})

            End While

        End If
        result.Close()
        Return Cus_registor
    End Function


    'validation for only input numbers and maximum length is 4
    Public Function numlen(ByVal numbers As String) As Boolean
        Dim pattern As String = "^[0-9]{1,4}$"
        Dim regex As New Regex(pattern)

        If regex.IsMatch(numbers) Then
            Return True
        End If
        Return False
    End Function





End Module
