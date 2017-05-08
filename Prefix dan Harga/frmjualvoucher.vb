Imports MySql.Data.MySqlClient
Public Class frmjualvoucher
    Dim nota As Integer
    Private Sub frmjualvoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SQLCari("select v.idv ID, v.kode_produk as 'Kode Produk', o.nama as 'Operator', v.denom as 'Denom', " _
                                   & "v.no_seri as 'No. Seri', v.tgl_akhir as 'Tgl Akhir', v.status as 'Status', d.harga 'Harga' " _
                                   & "from harga.voucher v,harga.operator o,harga.denom_voucher d " _
                                   & "where v.id_operator=o.id_operator and v.status='Tersedia' and v.denom=d.denom " _
                                   & "and d.id_operator=o.id_operator order by v.no_seri asc")
        dtgvouc.DataSource = objDataTable
    End Sub

    Private Sub btnjual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjual.Click
        SQLCari("select (t.nota)+1 as nota from counter_pulsa.transaksi t where kode_trx=2 or kode_trx=3 order by t.nota desc limit 1")
        If objDataTable.Rows.Count > 0 Then
            nota = objDataReader("nota")
        Else
            nota = 1
        End If

        Dim yesno As Integer = MessageBox.Show("Transaksi Voucher fisik dengan no. seri " & dtgvouc.CurrentRow.Cells(4).Value.ToString & " ?", "Transaksi Voucher Fisik", MessageBoxButtons.YesNo)
        If yesno = DialogResult.Yes Then
            SQLQuery("update harga.voucher set status='Terjual' where idv=" & dtgvouc.CurrentRow.Cells(0).Value & " and status='Tersedia'")
            SQLQuery("insert into counter_pulsa.transaksi(tgl_trx,jumlah,kode_trx,status,ket,nota) values(now()," & dtgvouc.CurrentRow.Cells(7).Value & ",3," & dtgvouc.CurrentRow.Cells(0).Value & ",''," & nota & ")")
            strukvoucher.ShowDialog()
        End If
    End Sub
End Class