
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient
Public Class Login
    Private Sub Guna2ToggleSwitch1_CheckedChanged(sender As Object, e As EventArgs) Handles ShowPassword.CheckedChanged
        If ShowPassword.Checked = True Then
            TxtPassword.PasswordChar = ""
        Else
            TxtPassword.PasswordChar = "*"
        End If
    End Sub



    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        connectdb()

        Dim user, password As String
        user = TxtUser.Text
        password = TxtPassword.Text

        If password_user(user, password) = True Then

            Main_Form.Show()
            Me.Hide()


        Else
            MsgBox("Enter currect username and password", MsgBoxStyle.Critical, "Error ")

        End If




    End Sub



    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Dim resuit As DialogResult = MessageBox.Show("Do you want to Exit ?", "Conformation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If resuit = DialogResult.OK Then
            Me.Close()
        End If

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class