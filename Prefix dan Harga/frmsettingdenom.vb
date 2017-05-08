Imports MySql.Data.MySqlClient
Public Class frmsettingdenom
    Sub kon_denom()
        SQLCari("select denom from harga.denom")
        With cmbdenom
            .DataSource = objDataTable
            .DisplayMember = "denom"
            .ValueMember = "denom"
        End With
    End Sub

    Sub kon_operator()
        SQLCari("select nama from harga.operator")
        With cmboperator
            .DataSource = objDataTable
            .DisplayMember = "nama"
            .ValueMember = "nama"
        End With
    End Sub

    Sub datadenom()
        SQLCari("select o.nama Operator,d.denom Denom, d.harga Harga " _
                                   & "from harga.denom_voucher d, harga.operator o where o.id_operator=d.id_operator order by o.nama asc")
        dtgvouc.DataSource = objDataTable
    End Sub

    Private Sub frmsettingdenom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kon_denom()
        Call kon_operator()
        Call datadenom()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SQLCari("select d.* from harga.denom_voucher d, harga.operator o where " _
                                       & "d.denom=" & cmbdenom.Text & " and d.id_operator=o.id_operator " _
                                       & "and o.nama='" & cmboperator.Text & "'")
        If objDataTable.Rows.Count > 0 Then
            SQLQuery("update harga.denom_voucher d inner join harga.operator o on " _
                & "d.id_operator=o.id_operator set d.harga=" & txtharga.Text & " where " _
                & "o.nama='" & cmboperator.Text & "' and d.denom=" & cmbdenom.Text & "")
            MsgBox("Data berhasil diubah")
        Else
            SQLQuery("insert into harga.denom_voucher(denom,harga,id_operator) select " & cmbdenom.Text & "," & txtharga.Text & ",id_operator from harga.operator where nama='" & cmboperator.Text & "'")
            MsgBox("Data berhasil disimpan")
        End If
        cmbdenom.Text = ""
        txtharga.Text = ""
        Call datadenom()
    End Sub

    Private Sub dtgvouc_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgvouc.CellClick
        cmbdenom.Text = dtgvouc.CurrentRow.Cells(1).Value.ToString
        txtharga.Text = dtgvouc.CurrentRow.Cells(2).Value.ToString
        cmboperator.Text = dtgvouc.CurrentRow.Cells(0).Value.ToString
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SQLQuery("delete d from harga.denom_voucher d join harga.operator o on o.id_operator=d.id_operator where d.denom=" & dtgvouc.CurrentRow.Cells(1).Value & " and o.nama='" & dtgvouc.CurrentRow.Cells(0).Value & "'")
        MsgBox("Data berhasil dihapus")
        cmbdenom.Text = ""
        txtharga.Text = ""
        Call datadenom()
    End Sub
End Class