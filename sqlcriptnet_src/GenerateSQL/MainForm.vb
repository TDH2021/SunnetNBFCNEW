Public Class MainForm
    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cdConn = New ConnectionData
        With cdConn
            .GetFromRegistry()
            If Not .CheckConnection Then
                Dim oFrm As New SetupDBForm
                If Not oFrm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    End
                End If
            Else
                lblServerConf.Text = " DB: " & .ServerName & " - " & .DatabaseName
                cboTableName.DataSource = GetTableList()
            End If
        End With
    End Sub

    Private Sub FileSetupDatabaseMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileSetupDatabaseMenu.Click
        Dim oFrm As New SetupDBForm
        oFrm.ShowDialog()
        cboTableName.DataSource = GetTableList()
    End Sub

    Private Sub FileExitMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileExitMenu.Click
        Close()
    End Sub

    Private Sub FileNewMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileNewMenu.Click
        rtbContent.Text = ""
    End Sub

    Private Sub FileOpenMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileOpenMenu.Click
        Dim sFile As String
        With OpenFileDialog1
            .Filter = "Text Files|*.txt|SQL Script|*.sql|All Files|*.*"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                sFile = .FileName
                Try
                    rtbContent.LoadFile(sFile, RichTextBoxStreamType.PlainText)
                Catch ex As Exception
                    MsgBox("File Not Found!")
                End Try
            End If
        End With
    End Sub

    Private Sub FileSaveMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileSaveMenu.Click
        Dim sFile As String
        With SaveFileDialog1
            .Filter = "Text Files|*.txt|SQL Script|*.sql|All Files|*.*"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                sFile = .FileName
                Try
                    rtbContent.SaveFile(sFile, RichTextBoxStreamType.PlainText)
                Catch ex As Exception
                    MsgBox("File Not Saved!")
                End Try
            End If
        End With
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If cboTableName.SelectedIndex < 0 Then Exit Sub
        Dim sTable As String = cboTableName.Text
        Dim sItem As String = ""
        If (ListView1.SelectedIndices.Count > 0) Then
            sItem = ListView1.SelectedItems(0).Text
        End If
        Select Case sItem
            Case "Select"
                rtbContent.Text &= GetSelectTag(sTable) & vbCrLf & vbCrLf
            Case "Insert"
                rtbContent.Text &= GetInsertTag(sTable) & vbCrLf & vbCrLf
            Case "Update"
                rtbContent.Text &= GetUpdateTag(sTable) & vbCrLf & vbCrLf
            Case "Delete"
                rtbContent.Text &= GetDeleteTag(sTable) & vbCrLf & vbCrLf
        End Select
    End Sub

    Private Sub EditFontMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditFontMenu.Click
        FontDialog1.Font = rtbContent.Font
        If FontDialog1.ShowDialog() Then
            rtbContent.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub EditCutMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditCutMenu.Click
        If rtbContent.Text = "" Then Exit Sub
        If rtbContent.SelectedText = "" Then
            Clipboard.SetText(rtbContent.Text)
            rtbContent.Text = ""
        Else
            Clipboard.SetText(rtbContent.SelectedText)
            rtbContent.SelectedText = ""
        End If
    End Sub

    Private Sub EditCopyMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditCopyMenu.Click
        If rtbContent.Text = "" Then Exit Sub
        If rtbContent.SelectedText = "" Then
            Clipboard.SetText(rtbContent.Text)
        Else
            Clipboard.SetText(rtbContent.SelectedText)
        End If
    End Sub

    Private Sub EditPasteMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditPasteMenu.Click
        If Clipboard.GetText() = "" Then Exit Sub
        rtbContent.SelectedText = Clipboard.GetText()
    End Sub

    Private Sub tsbNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNew.Click
        FileNewMenu_Click(sender, e)
    End Sub

    Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
        FileOpenMenu_Click(sender, e)
    End Sub

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSave.Click
        FileSaveMenu_Click(sender, e)
    End Sub

    Private Sub tsbCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCut.Click
        EditCutMenu_Click(sender, e)
    End Sub

    Private Sub tsbCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCopy.Click
        EditCopyMenu_Click(sender, e)
    End Sub

    Private Sub tsbPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbPaste.Click
        EditPasteMenu_Click(sender, e)
    End Sub
End Class
