Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class Form1
    Public statuspulsa As Integer
    Dim second As Integer = 6
    Dim simpan As String
    Dim cari As String
    'Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
    'Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")

    'disable close button
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200

    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property

    Sub hidup()
        txtkode.Enabled = True
        txtnoser.Enabled = True
        cmboperator.Enabled = True
        cmbdenom.Enabled = True
        cmbstatus.Enabled = True
        DateTimePicker1.Enabled = True
    End Sub
    Sub mati()
        txtkode.Enabled = False
        txtnoser.Enabled = False
        cmboperator.Enabled = False
        cmbdenom.Enabled = False
        cmbstatus.Enabled = False
        DateTimePicker1.Enabled = False
    End Sub
    Sub reset()
        txtkode.Text = Nothing
        txtnoser.Text = Nothing
        cmboperator.Text = Nothing
        cmbdenom.Text = Nothing
        cmbstatus.Text = Nothing
        txtid.Text = Nothing
    End Sub

    Sub prefix()
        SQLCari("select h.prefix as Prefix,o.nama as Operator " _
                               & "from harga.kartu k,harga.hlrid h,harga.operator o where h.id_kartu=k.id_kartu " _
                               & "and k.id_operator=o.id_operator order by Prefix asc")
        dtgprefix.DataSource = objDataTable
    End Sub

    Sub denombaru()
        SQLCari("select denom as Denom,if(right(denom,1)=1,'SMS',if(right(denom,1)=2,'Internet',if(right(denom,1)=3,'Transfer','Regular'))) as Tipe from harga.denom order by denom asc")
        DataGridView1.DataSource = objDataTable
    End Sub

    Sub operatorbaru()
        SQLCari("select nama from harga.operator order by id_operator asc")
        dgoperator.DataSource = objDataTable
    End Sub

    Sub aksesclient()
        SQLCari("select id,pwd from harga.pengguna where id=2")
        If objDataTable.Rows(0).Item("pwd").ToString = "kunci" Then
            toolstripclient.Text = "Akses Client Terkunci"
            toolstripclient.ForeColor = Color.Red
            btnkunciclient.Text = "Aktifkan"
        Else
            toolstripclient.Text = "Akses Client Aktif"
            toolstripclient.ForeColor = Color.Blue
            btnkunciclient.Text = "Kunci"
        End If
    End Sub
    Sub cek()
        'cek keterangan dari table akses
        SQLCari("select ket from harga.akses")
        'jika keterangan = butuh, maka verifikasi pulsa muncul
        If objDataTable.Rows(0).Item("ket").ToString = "butuh" Then
            Timer1.Stop()
            verifikasi.ShowDialog()
            'jika keterangan = depo, maka verifikasi depo muncul
        ElseIf objDataTable.Rows(0).Item("ket").ToString = "depo" Then
            Timer1.Stop()
            SQLCari("select * from harga.tampung2")
            verifikasidepo.txtid.Text = objDataTable.Rows(0).Item("idres").ToString
            verifikasidepo.txtnama.Text = objDataTable.Rows(0).Item("nama").ToString
            verifikasidepo.txtdepo.Text = objDataTable.Rows(0).Item("depo").ToString
            verifikasidepo.ShowDialog()
        Else
        End If
        'form struk muncul
        SQLCari("select ket from harga.struk")
        If objDataTable.Rows(0).Item("ket").ToString = "struk" Then
            Timer1.Stop()
            struk.ShowDialog()
        Else
        End If
    End Sub

    Sub kon_operator()
        SQLCari("select nama from harga.operator")
        With cmboperator
            .DataSource = objDataTable
            .DisplayMember = "nama"
            .ValueMember = "nama"
        End With
    End Sub

    Sub kon_denomvoucher()
        SQLCari("select distinct denom from harga.denom_voucher")
        With cmbdenom
            .DataSource = objDataTable
            .DisplayMember = "denom"
            .ValueMember = "denom"
        End With
    End Sub

    Sub total()
        SQLCari("select operator.nama as Operator, harga.denom as Denom, format(harga.harga,0) as Harga, " _
                               & "harga.kode_produk as Kode from harga.operator, harga.harga " _
                               & "where operator.id_operator=harga.id_operator order by operator.nama asc")
        DataGridView2.DataSource = objDataTable
        DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Sub datavoucher()
        SQLCari("select v.idv ID, v.kode_produk as 'Kode Produk', v.denom as 'Denom', o.nama as 'Operator', " _
                               & "v.no_seri as 'No. Seri', v.tgl_akhir as 'Tgl Akhir', " _
                               & "v.status as 'Status' from harga.voucher v,harga.operator o where v.id_operator=o.id_operator")
        dtgvouc.DataSource = objDataTable
    End Sub

    Sub histdepo()
        Try
            SQLCari("select distinct t.nota Nota,t.tgl_trx 'Tanggal Transaksi',d.id_member 'ID Member'," _
                                   & "d.nama Nama,t.jumlah Jumlah from counter_pulsa.transaksi t,counter_pulsa.transaksi_deposit d " _
                                   & "where kode_trx=1 and t.id_trx=d.id_trx and d.operator='" & tsnama.Text & "' and t.tgl_trx>=curdate() group by t.nota desc")
            dtgdeposit.DataSource = objDataTable
            Dim sum As Nullable(Of Integer) = CInt(objDataTable.Compute("SUM(Jumlah)", objDataTable.DefaultView.RowFilter))
            lbltrxdepo.Text = "Total Deposit = " + FormatCurrency(sum) + ",-"
        Catch ex As Exception
            MessageBox.Show("Data masih kosong : " & ex.Message)
        End Try
    End Sub

    Sub histpulsa()
        Try
            SQLCari("select t.nota Nota, t.tgl_trx 'Tanggal Transaksi', o.nama Operator, " _
                                   & "p.kode_produk Produk, h.harga Harga, p.no_tujuan Tujuan, if(t.status=0,'Tunggu',if(t.status=1,'Sukses','Gagal')) as Status " _
                                   & "from counter_pulsa.transaksi t,counter_pulsa.transaksi_pulsa p," _
                                   & "harga.harga h, harga.operator o where kode_trx=2 and t.id_trx=p.id_trx " _
                                   & "and p.kode_produk=h.kode_produk and h.id_operator=o.id_operator and t.status=" & statuspulsa & " and t.tgl_trx>=curdate()")
            dtgpulsa.DataSource = objDataTable
            Dim sum As Integer = CInt(objDataTable.Compute("SUM(Harga)", objDataTable.DefaultView.RowFilter))
            lbltrxpulsa.Text = "Total Penjualan Pulsa " + FormatCurrency(sum) + ",-"
        Catch ex As Exception
            SQLCari("select t.nota Nota, t.tgl_trx 'Tanggal Transaksi', o.nama Operator, " _
                                   & "p.kode_produk Produk, h.harga Harga, p.no_tujuan Tujuan, if(t.status=0,'Tunggu',if(t.status=1,'Sukses','Gagal')) as Status " _
                                   & "from counter_pulsa.transaksi t,counter_pulsa.transaksi_pulsa p," _
                                   & "harga.harga h, harga.operator o where kode_trx=2 and t.id_trx=p.id_trx " _
                                   & "and p.kode_produk=h.kode_produk and h.id_operator=o.id_operator and t.tgl_trx>=curdate()")
            dtgpulsa.DataSource = objDataTable
            If dtgpulsa.RowCount >= 1 Then
                Dim sum As Integer = CInt(objDataTable.Compute("SUM(Harga)", objDataTable.DefaultView.RowFilter))
                lbltrxpulsa.Text = "Total Penjualan Pulsa " + FormatCurrency(sum) + ",-"
            Else
            End If
        End Try
    End Sub

    Sub histvoucher()
        SQLCari("select t.nota Nota, t.tgl_trx 'Tanggal Transaksi', v.kode_produk 'Kode produk', o.nama 'Operator', v.denom 'Denom', " _
                               & "v.no_seri 'No. Seri', t.jumlah 'Harga' from counter_pulsa.transaksi t,harga.voucher v, " _
                               & "harga.operator o where t.`status`=v.idv and v.id_operator=o.id_operator and t.tgl_trx>=curdate()")
        dtgvoucher.DataSource = objDataTable
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub cmbstatuspulsa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstatuspulsa.SelectedIndexChanged
        Select Case cmbstatuspulsa.Text
            Case "Tunggu"
                statuspulsa = 0
                Call histpulsa()
            Case "Sukses"
                statuspulsa = 1
                Call histpulsa()
            Case "Gagal"
                statuspulsa = 2
                Call histpulsa()
            Case "Semua"
                statuspulsa = 3
                Call histpulsa()
        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Width = 316
        statuspulsa = 3
        total()
        Timer1.Start()
        cmbstatuspulsa.SelectedIndex = 0
        Call atur()
        Call kon_operator()
        Call datavoucher()
        Call histdepo()
        Call histpulsa()
        Call histvoucher()
        Call aksesclient()
        Call prefix()
        Call kon_denomvoucher()
        Call denombaru()
        Call operatorbaru()
        cmbkategori.SelectedIndex = 0
        raddetailpulsa.Checked = True
        raddetaildeposit.Checked = True
        raddd.Checked = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'second = second - 1
        'If second = 0 Then
        '    cek()
        'End If
    End Sub

    Private Sub ToolStripLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        hapus.ShowDialog()
    End Sub

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            hidup()
            txtkode.Focus()
            btntambah.Text = "Simpan"
            btnedit.Text = "Batal"
            simpan = "baru"
            reset()
        Else
            If simpan = "baru" Then
                If txtkode.Text = "" Or txtnoser.Text = "" Or cmboperator.Text = "" Or cmbstatus.Text = "" Then
                    MessageBox.Show("Data belum lengkap !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    SQLQuery("insert into harga.voucher(kode_produk,no_seri,tgl_akhir,status,id_operator,denom) " _
                                            & "select '" & txtkode.Text & "','" & txtnoser.Text & "','" & DateTimePicker1.Value.Date & "','" & cmbstatus.Text & "',id_operator," & cmbdenom.Text & " from harga.operator where nama='" & cmboperator.Text & "'")
                    MsgBox("Data berhasil disimpan")
                    btntambah.Text = "Tambah"
                    btnedit.Text = "Edit"
                    mati()
                    reset()
                    Call datavoucher()
                End If
            Else
            SQLQuery("update harga.voucher v,harga.operator o set v.kode_produk='" & txtkode.Text & "', v.denom=" & cmbdenom.Text & ", v.no_seri='" & txtnoser.Text & "', v.tgl_akhir='" & DateTimePicker1.Value.Date & "', v.status='" & cmbstatus.Text & "',v.id_operator=o.id_operator where o.nama='" & cmboperator.Text & "' and v.idv=" & txtid.Text & "")
            MsgBox("Data berhasil diubah")
                btntambah.Text = "Tambah"
                    btnedit.Text = "Edit"
                    mati()
                    reset()
                    Call datavoucher()
            End If
        End If
    End Sub

    Private Sub btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        If btnedit.Text = "Edit" Then
            If txtkode.Text = "" Or txtnoser.Text = "" Or cmboperator.Text = "" Or cmbstatus.Text = "" Then
                MessageBox.Show("Tidak ada data yang dipilih !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                hidup()
                btntambah.Text = "Simpan"
                btnedit.Text = "Batal"
                simpan = "edit"
            End If
        Else
            btntambah.Text = "Tambah"
            btnedit.Text = "Edit"
            mati()
            reset()
        End If

    End Sub

    Private Sub dtgvouc_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgvouc.CellClick
        txtid.Text = dtgvouc.CurrentRow.Cells(0).Value.ToString
        txtkode.Text = dtgvouc.CurrentRow.Cells(1).Value.ToString
        cmboperator.Text = dtgvouc.CurrentRow.Cells(3).Value.ToString
        txtnoser.Text = dtgvouc.CurrentRow.Cells(4).Value.ToString
        cmbdenom.Text = dtgvouc.CurrentRow.Cells(2).Value.ToString
        DateTimePicker1.Value = dtgvouc.CurrentRow.Cells(5).Value.ToString
        cmbstatus.Text = dtgvouc.CurrentRow.Cells(6).Value.ToString
    End Sub

    Private Sub btnhapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapus.Click
        If txtkode.Text = "" Or txtnoser.Text = "" Or cmboperator.Text = "" Or cmbstatus.Text = "" Then
            MessageBox.Show("Tidak ada data yang dipilih !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            SQLQuery("delete from harga.voucher where idv='" & txtid.Text & "'")
            MsgBox("Data berhasil dihapus")
            reset()
            Call datavoucher()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call atur()
        'RichTextBox1.LoadFile(Application.StartupPath + "/note.ini", RichTextBoxStreamType.PlainText)
        Call kon_operator()
        Call datavoucher()
        Call histdepo()
        Call histpulsa()
        Call histvoucher()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtkode.Text = Nothing Then
            MessageBox.Show("Tidak ada data yang dipilih !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim yesno As Integer = MessageBox.Show("Transaksi Voucher fisik dengan no. seri " & txtnoser.Text & " ?", "Transaksi Voucher Fisik", MessageBoxButtons.YesNo)
            If yesno = DialogResult.Yes Then
                SQLQuery("update harga.voucher set status='Terjual' where idv=" & txtid.Text & " and status='Tersedia'")
                SQLQuery("insert into counter_pulsa.transaksi_voucher(idv,tgl_trx) values(" & txtid.Text & ",now())")
                strukvoucher.ShowDialog()
            End If
        End If
    End Sub

    Private Sub cmbkategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbkategori.SelectedIndexChanged
        Select Case cmbkategori.Text
            Case "ID"
                cari = "idv"
            Case "Kode Produk"
                cari = "kode_produk"
            Case "No. Seri"
                cari = "no_seri"
            Case "Status"
                cari = "status"
        End Select
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        SQLCari("select v.idv ID, v.kode_produk as 'Kode Produk', v.denom as 'Denom', o.nama as 'Operator', " _
                               & "v.no_seri as 'No. Seri', v.tgl_akhir as 'Tgl Akhir', v.status as 'Status' from harga.voucher v,harga.operator o " _
                               & "where v.id_operator=o.id_operator and " & cari & " like '%" & txtcari.Text & "%'")
        dtgvouc.DataSource = objDataTable
    End Sub

    Private Sub txtcari_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcari.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button5_Click(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        total()
    End Sub

    Private Sub btnkunciclient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkunciclient.Click
        If btnkunciclient.Text = "Kunci" Then
            SQLQuery("update harga.pengguna set pwd='kunci' where id=2")
            toolstripclient.Text = "Akses Client Terkunci"
            toolstripclient.ForeColor = Color.Red
            btnkunciclient.Text = "Aktifkan"
            MsgBox("Client terkunci")
        Else
            SQLQuery("update harga.pengguna set pwd='buka' where id=2")
            toolstripclient.Text = "Akses Client Aktif"
            toolstripclient.ForeColor = Color.Blue
            btnkunciclient.Text = "Kunci"
            MsgBox("Client aktif")
        End If
    End Sub

    Private Sub btnkunciserver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkunciserver.Click
        kunciserver.ShowDialog()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprefix.TextChanged
        SQLCari("select h.prefix as Prefix,o.nama as Operator from harga.kartu k,harga.hlrid h,harga.operator o " _
                               & "where h.id_kartu=k.id_kartu and k.id_operator=o.id_operator " _
                               & "and h.prefix like '" & txtprefix.Text & "%' order by Prefix asc")
        dtgprefix.DataSource = objDataTable
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SQLQuery("insert into counter_pulsa.transaksi(tgl_trx,jumlah,kode_trx,ket,nota) values(now(),0,1,'',4)")
        MessageBox.Show("Nota deposit berhasil di reset !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btncetakdeposit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncetakdeposit.Click
        cetak = "cetakdeposit"
        frmStrukPulsa.ShowDialog()
    End Sub

    Private Sub btncetakpulsa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncetakpulsa.Click
        If cmbstatuspulsa.Text = "Semua" Or cmbstatuspulsa.Text = "Tunggu" Then
            MessageBox.Show("Pilih Sukses atau gagal!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            cetak = "cetakpulsa"
            frmStrukPulsa.ShowDialog()
        End If
    End Sub

    Private Sub btncetaknota_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncetaknota.Click
        cetaknota = "cetak"
        struk.ShowDialog()
    End Sub

    Private Sub dtgpulsa_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgpulsa.CellClick
        cellklik = dtgpulsa.CurrentRow.Cells(0).Value.ToString
    End Sub

    Private Sub dtgdeposit_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgdeposit.CellClick
        cellklik = dtgdeposit.CurrentRow.Cells(0).Value.ToString
    End Sub

    Private Sub btncetakdep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncetakdep.Click
        cetaknota = "cetak"
        strukdepo.ShowDialog()
    End Sub

    Private Sub btntambahprefix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambahprefix.Click
        frmPrefix.ShowDialog()
    End Sub

    Private Sub raddetaildeposit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles raddetaildeposit.CheckedChanged
        If Me.raddetaildeposit.Checked = True Then
            raddd.Enabled = True
            radah.Enabled = True
            radsem.Enabled = True
        Else
            raddd.Enabled = False
            radah.Enabled = False
            radsem.Enabled = False
        End If
    End Sub

    Private Sub btnhapusprefix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhapusprefix.Click
        SQLQuery("delete from harga.hlrid where prefix='" & dtgprefix.CurrentRow.Cells(0).Value.ToString & "'")
        dtgprefix.DataSource = objDataTable
        Call prefix()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        frmsettingdenom.ShowDialog()
    End Sub

    Private Sub btnjualvoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnjualvoucher.Click
        frmjualvoucher.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncarivoucher.Click
        SQLCari("select t.nota Nota, t.tgl_trx 'Tanggal Transaksi', v.kode_produk 'Kode produk', " _
                               & "o.nama 'Operator', v.denom 'Denom', v.no_seri 'No. Seri', t.jumlah 'Harga' " _
                               & "from counter_pulsa.transaksi t,harga.voucher v, harga.operator o " _
                               & "where t.`status`=v.idv and v.id_operator=o.id_operator and date(t.tgl_trx) between " _
                               & "'" & dtawal.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "' " _
                               & "and '" & dtakhir.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "'")
        dtgvoucher.DataSource = objDataTable
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox(dtawal.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture))
    End Sub

    Private Sub btncaridepo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncaridepo.Click
        SQLCari("select t.nota Nota,t.tgl_trx 'Tanggal Transaksi',d.id_member 'ID Member'," _
                                   & "d.nama Nama,t.jumlah Jumlah from counter_pulsa.transaksi t,counter_pulsa.transaksi_deposit d " _
                                   & "where kode_trx=1 and t.id_trx=d.id_trx and date(t.tgl_trx) between '" & dtdepoawal.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "' and '" & dtdepoakhir.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "'")
        dtgdeposit.DataSource = objDataTable
    End Sub

    Private Sub btncaripulsa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncaripulsa.Click
        SQLCari("select t.nota Nota, t.tgl_trx 'Tanggal Transaksi', o.nama Operator, " _
                                   & "p.kode_produk Produk, h.harga Harga, p.no_tujuan Tujuan, if(t.status=0,'Tunggu',if(t.status=1,'Sukses','Gagal')) as Status " _
                                   & "from counter_pulsa.transaksi t,counter_pulsa.transaksi_pulsa p," _
                                   & "harga.harga h, harga.operator o where kode_trx=2 and t.id_trx=p.id_trx " _
                                   & "and p.kode_produk=h.kode_produk and h.id_operator=o.id_operator and date(t.tgl_trx) between '" & dtpulsaawal.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "' and '" & dtpulsaakhir.Value.Date.ToString("yyy/MM/dd", CultureInfo.InvariantCulture) & "'")
        dtgpulsa.DataSource = objDataTable
    End Sub

    Private Sub btncetakvoucher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncetakvoucher.Click
        cetaknota = "cetak"
        strukvoucher.ShowDialog()
    End Sub

    Private Sub dtgvoucher_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgvoucher.CellClick
        cellklik = dtgvoucher.CurrentRow.Cells(0).Value.ToString
    End Sub

    Private Sub UbahInformasiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UbahInformasiToolStripMenuItem.Click
        informasi.ShowDialog()
    End Sub

    Private Sub SettingPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingPasswordToolStripMenuItem.Click
        SettingPwd.ShowDialog()
    End Sub

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        With setting
            .ComboBox1.Enabled = True
            .ComboBox2.Enabled = True
            .ShowDialog()
        End With
    End Sub

    Private Sub DataGridView2_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseDoubleClick
        With setting
            .TextBox2.Text = DataGridView2.CurrentRow.Cells(2).Value.ToString.Replace(",", "")
            .txtkode.Text = DataGridView2.CurrentRow.Cells(3).Value.ToString
            .ComboBox1.Enabled = False
            .ComboBox2.Enabled = False
        End With
        setting.ShowDialog()
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim kode As String = DataGridView2.CurrentRow.Cells(3).Value.ToString
        Dim opera As String = DataGridView2.CurrentRow.Cells(0).Value.ToString
        Dim denom As String = DataGridView2.CurrentRow.Cells(1).Value
        SQLQuery("delete h from harga.harga h,harga.operator o where h.kode_produk='" & kode & "' and h.denom=" & denom & " and o.id_operator=h.id_operator and o.nama='" & opera & "'")
        MessageBox.Show("Data Produk berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Button2_Click(sender, e)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        frmdenom.ShowDialog()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim hasil As Integer = MessageBox.Show("Hapus denom " & DataGridView1.CurrentRow.Cells(0).Value & " ?", "Peringatan", MessageBoxButtons.YesNo)
        If hasil = DialogResult.Yes Then
            SQLQuery("delete from harga.denom where denom =" & DataGridView1.CurrentRow.Cells(0).Value & "")
            MessageBox.Show("Data denom berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call denombaru()
        Else
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        frmoperator.ShowDialog()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim hasil As Integer = MessageBox.Show("Hapus operator " & dgoperator.CurrentRow.Cells(0).Value & " ?", "Peringatan", MessageBoxButtons.YesNo)
        If hasil = DialogResult.Yes Then
            SQLQuery("delete from harga.operator where nama ='" & dgoperator.CurrentRow.Cells(0).Value & "'")
            MessageBox.Show("Data operator berhasil di hapus !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call operatorbaru()
        Else
        End If
    End Sub

    Private Sub ToolStripLabel1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel1.Click
        SQLQuery("update harga.user set status=0")
        Application.Exit()
    End Sub

    Private Sub ManajemenOperatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FrmUser.ShowDialog()
    End Sub

    Private Sub ManajemenPenggunaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManajemenPenggunaToolStripMenuItem.Click
        FrmUser.ShowDialog()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Close()
        SetDb.Show()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        SQLQuery("insert into counter_pulsa.transaksi(tgl_trx,jumlah,kode_trx,ket,nota) values(now(),0,2,'',4)")
        MessageBox.Show("Nota pulsa berhasil di reset !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class