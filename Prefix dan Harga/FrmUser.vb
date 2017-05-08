Imports MySql.Data.MySqlClient
Public Class FrmUser
    Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
    Sub loaddata()
        SQLQuery("select id ID,nama Nama,if(shift=1,'Pagi','Sore') Shift from harga.user")
        DataGridView1.DataSource = objDataTable
    End Sub
    Private Sub FrmUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loaddata()
        txtnama.Text = ""
        txtpass.Text = ""
        cmbshift.Text = ""
    End Sub

    Private Sub btnsimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsimpan.Click
        Dim name As String = Form1.tsnama.Text
        Dim shift As Integer
        If cmbshift.Text = "Pagi" Then
            shift = 1
        Else
            shift = 2
        End If
        
        If txtnama.Text = "" Then
            MessageBox.Show("Data belum lengkap!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            If txtnama.Text = name Then
                SQLQuery("update harga.user set pwd='" & computeHash(txtpass.Text) & "',shift=" & shift & " where nama='" & txtnama.Text & "'")
                MsgBox("Data berhasil di update")
                    txtnama.Text = ""
                    txtpass.Text = ""
                    cmbshift.Text = ""
                Call loaddata()
            Else
                MessageBox.Show("Anda tidak berhak mengubah data ini!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        txtnama.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        cmbshift.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
    End Sub

    Private Sub btnhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapus.Click
        If txtnama.Text = "" Then
            MessageBox.Show("Tidak ada data yang akan dihapus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            SQLQuery("delete from harga.user where nama='" & txtnama.Text & "'")
            MsgBox("Data berhasil dihapus")
                txtnama.Text = ""
                txtpass.Text = ""
                cmbshift.Text = ""
            Call loaddata()
        End If
    End Sub

    Private Sub btnbaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbaru.Click
        Dim shift As Integer
        If cmbshift.Text = "Pagi" Then
            shift = 1
        Else
            shift = 2
        End If
        If txtnama.Text = "" Or txtpass.Text = "" Then
            MessageBox.Show("Data belum lengkap!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            SQLQuery("insert into user(nama,shift,pwd) values('" & txtnama.Text & "'," & shift & ",'" & computeHash(txtpass.Text) & "')")
            MsgBox("Data berhasil disimpan")
                txtnama.Text = ""
                txtpass.Text = ""
                cmbshift.Text = ""
            Call loaddata()
        End If
    End Sub
End Class