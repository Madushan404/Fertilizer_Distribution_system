Imports FontAwesome.Sharp
Public Class Main_Form

    Private CurrentChildForm As Form

    Private Sub OpenChildForm(ChildForm As Form)
        If CurrentChildForm IsNot Nothing Then
            CurrentChildForm.Close()
        End If
        CurrentChildForm = ChildForm
        ChildForm.TopLevel = False
        ChildForm.FormBorderStyle = FormBorderStyle.None
        ChildForm.Dock = DockStyle.Fill

        PanelBackground.Controls.Add(ChildForm)
        PanelBackground.Tag = ChildForm
        ChildForm.BringToFront()
        ChildForm.Show()
        lblHome.Text = ChildForm.Text

    End Sub
    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Add Control Buttons
        MaximizeBox = False
        MinimizeBox = False
        ControlBox = False
        Me.Text = String.Empty
        Me.DoubleBuffered = True

        'Add Hide Menu Button
        HideSubmenu()

        btncus.Hide()
        btncrop.Hide()
        btnland.Hide()
        btnfertilizer.Hide()
        btnsuplier.Hide()


    End Sub
    Private Sub HideSubmenu()
        PanelMenuBttn.Visible = True
    End Sub

    Private Sub ShowSubmenu(Submenu As Panel)
        If Submenu.Visible = False Then
            HideSubmenu()
            Submenu.Visible = True
        Else
            Submenu.Visible = False
        End If

    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim resuit As DialogResult = MessageBox.Show("Do you want to Exit ?", "Conformation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        If resuit = DialogResult.OK Then
            Application.Exit()
        End If




    End Sub

    Private Sub btnMax_Click(sender As Object, e As EventArgs) Handles btnMax.Click
        If WindowState = FormWindowState.Normal Then
            WindowState = FormWindowState.Maximized
        Else

        End If
    End Sub

    Private Sub btnMin_Click(sender As Object, e As EventArgs) Handles btnMin.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PanelBackground_Paint(sender As Object, e As PaintEventArgs) Handles PanelBackground.Paint

    End Sub

    Private Sub btnRepCat_Click(sender As Object, e As EventArgs) Handles btnUpdateDelete.Click

        btncus.Show()
        btncrop.Show()
        btnland.Show()
        btnfertilizer.Show()
        btnsuplier.Show()

    End Sub




    Private Sub btnElectrical_Click(sender As Object, e As EventArgs) Handles btnElectrical.Click

        OpenChildForm(New Customer_Registration)
    End Sub

    Private Sub btnWater_Click(sender As Object, e As EventArgs) Handles btnWater.Click

        OpenChildForm(New Land_Registor)
    End Sub

    Private Sub btnIndoor_Click(sender As Object, e As EventArgs) Handles btnIndoor.Click

        OpenChildForm(New Fertilizer_Registration)
    End Sub

    Private Sub btnOutdoor_Click(sender As Object, e As EventArgs) Handles btnOutdoor.Click

        OpenChildForm(New Fertilizer_supliers)
    End Sub

    Private Sub btnGarden_Click(sender As Object, e As EventArgs) Handles btnGarden.Click

        OpenChildForm(New Crops_Registration)
    End Sub

    Private Sub IconButton6_Click(sender As Object, e As EventArgs) Handles btncus.Click
        OpenChildForm(New Cus_Update_Delete)
    End Sub



    Private Sub btnland_Click(sender As Object, e As EventArgs) Handles btnland.Click
        OpenChildForm(New Land_Update)
    End Sub

    Private Sub btnfertilizer_Click(sender As Object, e As EventArgs) Handles btnfertilizer.Click
        OpenChildForm(New Fertilizer_Update)
    End Sub

    Private Sub btnsuplier_Click(sender As Object, e As EventArgs) Handles btnsuplier.Click
        OpenChildForm(New Fertilizer_Suplier_Update)
    End Sub

    Private Sub btncrop_Click(sender As Object, e As EventArgs) Handles btncrop.Click
        OpenChildForm(New Crops_Update)
    End Sub

    Private Sub btnEmergency_Click(sender As Object, e As EventArgs) Handles btnEmergency.Click
        OpenChildForm(New Stoke)
    End Sub
End Class