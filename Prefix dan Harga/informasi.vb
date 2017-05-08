Imports MySql.Data.MySqlClient
Public Class informasi
    Dim status As String

    Private Sub informasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SQLQuery("select infoclient,infopulsa,infodeposit,infovoucher,status from harga.informasi where id=1")
        txtinfo.Text = objDataTable.Rows(0).Item("infoclient").ToString
        txtinfopulsa.Text = objDataTable.Rows(0).Item("infopulsa").ToString
        txtinfodeposit.Text = objDataTable.Rows(0).Item("infodeposit").ToString
        txtinfovoucher.Text = objDataTable.Rows(0).Item("infovoucher").ToString
        If objDataTable.Rows(0).Item("status").ToString = "non" Then
            radnonaktif.Checked = True
        Else
            radaktif.Checked = True
        End If
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        SQLQuery("update harga.informasi set infoclient='" & txtinfo.Text & "',infopulsa='" & txtinfopulsa.Text & "',infodeposit='" & txtinfodeposit.Text & "',infovoucher='" & txtinfovoucher.Text & "',status='" & status & "'")
        MessageBox.Show("Informasi berhasil di update")
    End Sub

    Private Sub radaktif_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radaktif.CheckedChanged
        status = "aktif"
    End Sub

    Private Sub radnonaktif_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radnonaktif.CheckedChanged
        status = "non"
    End Sub
End Class