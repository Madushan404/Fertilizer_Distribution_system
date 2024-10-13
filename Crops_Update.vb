Public Class Crops_Update
    Private Sub Crops_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lblrequired1.Hide()
        Lblrequired2.Hide()
        Lblrequired4.Hide()
        Lblrequired3.Hide()
        Lblrequired5.Hide()
        Lblrequired6.Hide()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        TxtCropId.Clear()
        TxtCropName.Clear()
        TxtCropType.Clear()
        TxtFID.Clear()
        TxtFQuentity.Clear()
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles BtnUpdate.Click

    End Sub
End Class