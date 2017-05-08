Imports MySql.Data.MySqlClient
Public Class frmdenom
    Private Sub txtkode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtdenom.KeyPress
        'Textbox hanya boleh diisi angka
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
        If e.KeyChar = Convert.ToChar(13) Then
            Button1_Click(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLQuery("insert into harga.denom values(" & txtdenom.Text & ")")
        MsgBox("Data berhasil disimpan")
            txtdenom.Text = ""
        Form1.denombaru()
    End Sub
End Class