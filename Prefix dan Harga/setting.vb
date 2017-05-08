Imports MySql.Data.MySqlClient
Public Class setting

    Sub kon_operator()
        SQLCari("select nama from harga.operator")
        If objDataTable.Rows.Count > 0 Then
            With ComboBox1
                .DataSource = objDataTable
                .DisplayMember = "nama"
                .ValueMember = "nama"
            End With
        End If
    End Sub

    Sub kon_denom()
        SQLCari("select denom from harga.denom order by denom asc")
        If objDataTable.Rows.Count > 0 Then
            With ComboBox2
                .DataSource = objDataTable
                .DisplayMember = "denom"
                .ValueMember = "denom"
            End With
        End If
    End Sub
    Private Sub setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        kon_operator()
        kon_denom()
        Call atur()
        If Form1.DataGridView2.Rows.Count = 0 Then
        Else
            ComboBox1.SelectedIndex = ComboBox1.FindStringExact(Form1.DataGridView2.CurrentRow.Cells(0).Value)
            ComboBox2.SelectedIndex = ComboBox2.FindStringExact(Form1.DataGridView2.CurrentRow.Cells(1).Value)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLCari("select o.nama,h.denom,h.harga from harga.operator o,harga.harga h where o.id_operator=h.id_operator and o.nama='" & ComboBox1.Text & "' and h.denom='" & ComboBox2.Text & "'")
        If objDataTable.Rows.Count > 0 Then
            SQLQuery("update harga.harga,harga.operator set harga.harga='" & TextBox2.Text & "', harga.kode_produk='" & txtkode.Text & "' where (operator.id_operator=harga.id_operator and operator.nama='" & ComboBox1.Text & "') and harga.denom='" & ComboBox2.Text & "'")
            MsgBox("Data berhasil diubah")
            Form1.total()
        Else
            SQLQuery("insert into harga.harga(id_operator,denom,harga,kode_produk) select id_operator,'" & ComboBox2.Text & "','" & TextBox2.Text & "','" & txtkode.Text & "' from harga.operator where nama='" & ComboBox1.Text & "'")
            MsgBox("Data berhasil disimpan")
        End If
    End Sub

End Class