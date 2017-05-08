Imports MySql.Data.MySqlClient
Public Class hapus
    Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")

    Sub produk()
        SQLQuery("select operator.nama as Operator, harga.denom as Denom, harga.harga as Harga, harga.kode_produk as Kode from harga.operator o, harga.harga h where o.id_operator=h.id_operator order by o.nama asc")
        DataGridView1.DataSource = objDataTable
    End Sub
    Private Sub hapus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call produk()
        'TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLQuery("delete h from harga.harga h join harga.operator o on o.id_operator=h.id_operator where h.denom=" & DataGridView1.CurrentRow.Cells(1).Value & " and o.nama='" & DataGridView1.CurrentRow.Cells(0).Value.ToString & "'")
        MsgBox("Data berhasil dihapus")
        Call produk()
    End Sub
End Class