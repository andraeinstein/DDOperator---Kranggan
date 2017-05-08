Imports MySql.Data.MySqlClient
Public Class frmPrefix
    Sub kon_operator()
        SQLCari("select nama from harga.operator")
        If objDataTable.Rows.Count > 0 Then
            With cmboperator
                .DataSource = objDataTable
                .DisplayMember = "nama"
                .ValueMember = "nama"
            End With
        End If
    End Sub
    Private Sub frmPrefix_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kon_operator()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLCari("select prefix from harga.hlrid where prefix='" & txtprefix.Text & "'")
        If objDataTable.Rows.Count > 0 Then
            MessageBox.Show("Kode Prefix sudah ada!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            SQLQuery("insert into harga.hlrid(prefix,id_kartu) select '" & txtprefix.Text & "',k.id_kartu from harga.kartu k,harga.operator o " _
                & "where o.nama='" & cmboperator.Text & "' and k.id_operator=o.id_operator limit 1")
            MsgBox("Data berhasil disimpan")
        End If
    End Sub
End Class