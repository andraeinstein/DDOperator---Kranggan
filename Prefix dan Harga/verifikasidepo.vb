Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class verifikasidepo
    Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
    Dim cekpass As String

    Sub inputdepo()
        Call atur()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
        objConnection.Close()
        objConnection.Open()
        objCommand = New MySqlCommand
        objCommand.Connection = objConnection
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "insert into transaksi(tgl_trx,jumlah,kode_trx,ket,nota) values(now(),'" & txtdepo.Text & "',1,''," & lblnota.Text & ")"
        objCommand.CommandTimeout = 0
        Dim rsl As Integer = objCommand.ExecuteNonQuery
        objCommand.CommandText = "insert into transaksi_deposit(id_trx,id_member,nama,suplier) select last_insert_id(),'" & txtid.Text & "','" & txtnama.Text & "',2 from suplier where jenis=0"
        objCommand.CommandTimeout = 0
        Dim rsl2 As Integer = objCommand.ExecuteNonQuery
        objCommand.CommandText = "insert into sms_outbox(tgl_sms,tgl_input,no_hp,nama,pesan,com) select now(),now(),no_hp,nama,concat('INFO.TRANSFER." & txtid.Text & "." & txtdepo.Text & ".',pin),port from suplier where jenis=0"
        objCommand.CommandTimeout = 0
        Dim rsl3 As Integer = objCommand.ExecuteNonQuery
        If rsl > 0 And rsl2 > 0 And rsl3 > 0 Then
            objConnection.Close()
        End If
    End Sub

    Sub cekpassword()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        Dim strsql As String = "select id,kata_awal from passotoritas where kata_awal='" & computeHash(Regex.Replace(txtpass.Text, "[^A-Za-z]", String.Empty)) & "' and id=1"
        objConnection.Close()
        objConnection.Open()
        Dim da As New MySqlDataAdapter(strsql, objConnection)
        Dim objcommand = New MySql.Data.MySqlClient.MySqlCommand(strsql, objConnection)
        cek = objcommand.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            Try
                If Regex.Replace(txtpass.Text, "[^0-9]", String.Empty) = Date.Today.Day Then
                    cekpass = "benar"
                Else
                    cekpass = "salah"
                End If
            Catch ex As Exception
            End Try
        End If
        objConnection.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call atur()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        Call cekpassword()
        If cekpass = "benar" Then
            Try
                objConnection.Close()
                objConnection.Open()
                objCommand = New MySqlCommand
                objCommand.Connection = objConnection
                objCommand.CommandType = CommandType.Text
                objCommand.CommandText = "update akses set ket='benar'"
                objCommand.CommandTimeout = 0
                Dim rsl As Integer = objCommand.ExecuteNonQuery()
                If rsl > 0 Then
                    MessageBox.Show("Deposit transfer disetujui !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    objConnection.Close()
                    Form1.Timer1.Start()
                End If
                Call inputdepo()
                strukdepo.ShowDialog()
                objConnection.Close()
            Catch ex As Exception
            End Try
        Else
            MessageBox.Show("Password salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        objConnection.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call atur()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        objConnection.Close()
        objConnection.Open()
        objCommand = New MySqlCommand
        objCommand.Connection = objConnection
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "update harga.akses set ket='batal'"
        objCommand.CommandTimeout = 0
        Dim rsl As Integer = objCommand.ExecuteNonQuery()
        If rsl > 0 Then
            MessageBox.Show("Pembelian pulsa telah dibatalkan !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            objConnection.Close()
            Form1.Timer1.Start()
        End If
        objConnection.Close()
    End Sub

    Private Sub verifikasidepo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objConnection2.Open()
        StrSQL = "select (t.nota)+1 as nota from transaksi t where kode_trx=1 order by t.nota desc limit 1"
        Dim da As New MySqlDataAdapter(StrSQL, objConnection2)
        Dim objcommand = New MySql.Data.MySqlClient.MySqlCommand(StrSQL, objConnection2)
        objDataReader = objcommand.ExecuteReader
        objDataReader.Read()
        If objDataReader.HasRows Then
            lblnota.Text = objDataReader("nota")
        Else
            lblnota.Text = 1
        End If
        objConnection2.Close()
        txtterbilang.Text = Terbilang(txtdepo.Text) + "rupiah"
        TextBox1.Text = FormatCurrency(txtdepo.Text)
        Me.Activate()
    End Sub
End Class