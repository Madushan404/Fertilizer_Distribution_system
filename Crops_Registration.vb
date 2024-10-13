Public Class Crops_Registration
    Private Sub Crops_Registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
End Class