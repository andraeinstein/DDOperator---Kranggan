Imports MySql.Data.MySqlClient
Public Class frmoperator
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLQuery("insert into harga.operator(id_operator,nama) select id_operator+1,'" & txtoperator.Text & "' from harga.operator order by id_operator desc limit 1")
        MsgBox("Data berhasil disimpan")
            txtoperator.Text = ""
        Form1.operatorbaru()
    End Sub

    Private Sub txtdenom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtoperator.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button1_Click(Me, EventArgs.Empty)
        End If
    End Sub
End Class