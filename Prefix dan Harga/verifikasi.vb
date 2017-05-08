Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class verifikasi
    Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
    'Dim jumlahdata As Integer
    Dim cekpass As String

    Sub inputtransaksi()
        Dim i As Integer = DataGridView1.CurrentRow.Index
        'objConnection2.Close()
        'objConnection2.Open()
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "insert into transaksi(tgl_trx,jumlah,kode_trx,ket,nota) values(now(),0,2,''," & lblnota.Text & ")"
        objCommand.CommandTimeout = 0
        objCommand.ExecuteNonQuery()
    End Sub

    Sub inputtransaksipulsa()
        Dim i As Integer = DataGridView1.CurrentRow.Index
        objCommand.CommandText = "insert into counter_pulsa.transaksi_pulsa(id_trx,kode_produk,no_tujuan,suplier) " _
            & "select t.id_trx,t.kode_produk,t.no_tujuan,s.id_suplier from harga.tampung t, " _
            & "counter_pulsa.suplier s where s.jenis=1"
        objCommand.CommandTimeout = 0
        objCommand.ExecuteNonQuery()
    End Sub

    Sub inputoutbox()
        Dim i As Integer = DataGridView1.CurrentRow.Index
        objCommand.CommandText = "insert into counter_pulsa.sms_outbox(tgl_sms,tgl_input,no_hp,nama,pesan,com) " _
            & "select now(),now(),s.no_hp,s.nama,concat(t.kode_produk,'.',t.no_tujuan,';',s.pin),s.port " _
            & "from counter_pulsa.suplier s, harga.tampung t where s.jenis=1"
        objCommand.CommandTimeout = 0
        objCommand.ExecuteNonQuery()
    End Sub

    Sub hapustampung()
        Call atur()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        objConnection.Close()
        objConnection.Open()
        objCommand = New MySqlCommand
        objCommand.Connection = objConnection
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "delete from tampung"
        objCommand.CommandTimeout = 0
        objCommand.ExecuteNonQuery()
        objConnection.Close()
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
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Call atur()
        Call cekpassword()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        If cekpass = "benar" Then
            'Try
            objConnection.Close()
            objConnection.Open()
            objCommand = New MySqlCommand
            objCommand.Connection = objConnection
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "update akses set ket='beres'"
            objCommand.CommandTimeout = 0
            Dim rsl As Integer = objCommand.ExecuteNonQuery()
            If rsl > 0 Then
                Dim i As Integer = DataGridView1.CurrentRow.Index
                Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
                objConnection2.Close()
                objConnection2.Open()
                objCommand = New MySqlCommand
                objCommand.Connection = objConnection2
                For a As Integer = 1 To jumlahdata
                    Call inputtransaksi()
                Next
                Call inputtransaksipulsa()
                Call inputoutbox()
                Call hapustampung()
                objConnection.Close()
                objConnection2.Close()
                Form1.Timer1.Start()
            End If
            struk.ShowDialog()
            Me.Close()
            jumlahdata = DataGridView1.RowCount
        Else
            MessageBox.Show("Password salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Catch ex As Exception
            '    MessageBox.Show(e.ToString)
            'End Try
            'Else
            'MessageBox.Show("Password salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If
        End If
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
        Call hapustampung()
    End Sub

    Private Sub verifikasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim total As Integer
        Call atur()
        objConnection2.Close()
        objConnection2.Open()
        StrSQL = "select tampung.id_trx as ID, operator.nama as Operator, harga.denom as Nominal, tampung.no_tujuan as Nomor" _
                                & ", tampung.harga as Harga from tampung, harga, operator where tampung.kode_produk = harga.kode_produk " _
                                & "and harga.id_operator=operator.id_operator"
        Dim cmd As New MySqlCommand(StrSQL, objConnection2)
        Dim da2 As MySqlDataAdapter = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable()
        da2.Fill(dt)
        DataGridView1.DataSource = dt
        objConnection2.Close()
        For Each dgvRow As DataGridViewRow In DataGridView1.Rows
            If Not dgvRow.IsNewRow Then
                total += CInt(dgvRow.Cells(4).Value)
            End If
        Next
        lbltotal.Text = FormatCurrency(total)
        Try
            txtterbilang.Text = Terbilang(lbltotal.Text) + "rupiah"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        jumlahdata = DataGridView1.Rows.Count

        objConnection2.Open()
        StrSQL = "select (t.nota)+1 as nota from counter_pulsa.transaksi t where kode_trx=2 or kode_trx=3 order by t.nota desc limit 1"
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
        Me.Activate()
    End Sub
End Class