Imports System.Reflection.Emit

Public Class About
    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myFileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
        lblVersionNumber.Text = myFileVersionInfo.FileVersion
    End Sub
End Class