Imports MySql.Data.MySqlClient
Public Class kunciserver

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        SQLQuery("select id,pwd from harga.pengguna where pwd='" & computeHash(txtpass.Text) & "' and id=1")
        If objDataTable.Rows.Count > 0 Then
            Me.Close()
        Else
            MessageBox.Show("Password salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtpass.Text = Nothing
        End If
    End Sub
End Class