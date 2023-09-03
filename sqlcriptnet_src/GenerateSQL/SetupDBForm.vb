Public Class SetupDBForm

    Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetup.Click
        cdConn = New ConnectionData
        With cdConn
            .ServerName = txtServerName.Text
            .DatabaseName = txtDatabaseName.Text
            .UserID = txtUserID.Text
            .Password = txtPassword.Text
            If Not .CheckConnection() Then
                MsgBox("Connection Failed! Please check the setting.")
            Else
                MainForm.lblServerConf.Text = " DB: " & .ServerName & " - " & .DatabaseName
                Me.DialogResult = Windows.Forms.DialogResult.OK
                .SaveToRegistry()
            End If
        End With
        cdConn = Nothing
    End Sub

    Private Sub SetupDBForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cdConn = New ConnectionData
        With cdConn
            .GetFromRegistry()
            txtServerName.Text = .ServerName
            txtDatabaseName.Text = .DatabaseName
            txtUserID.Text = .UserID
            txtPassword.Text = .Password
        End With
    End Sub
End Class