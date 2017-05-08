Imports MySql.Data.MySqlClient
Public Class SettingPwd

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        SQLCari("select id,pwd from harga.pengguna where pwd='" & computeHash(txtlama.Text) & "' and id=1")
        If objDataTable.Rows.Count > 0 Then
            SQLQuery("update harga.pengguna set pwd='" & computeHash(txtbaru.Text) & "' where id=1")
            MsgBox("Data berhasil diubah")
            Reset()
                Me.Close()
            Form1.total()
        Else
            MessageBox.Show("Password lama salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnsimpanoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpanoto.Click
        SQLCari("select id,kata_awal from harga.passotoritas where kata_awal='" & computeHash(txtpwdlama.Text) & "' and id=1")
        If objDataTable.Rows.Count > 0 Then
            SQLQuery("update harga.passotoritas set kata_awal='" & computeHash(txtpwdbaru.Text) & "' where id=1")
            MsgBox("Data berhasil diubah")
            Reset()
                Me.Close()
            Form1.total()
        Else
            MessageBox.Show("Password lama salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class