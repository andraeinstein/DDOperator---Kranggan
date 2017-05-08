Imports MySql.Data.MySqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmStrukPulsa
    Sub printsemua()
        Call atur()
        Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
        objConnection2.Close()
        objConnection2.Open()
        Dim objRpt = New CrystalReport6()
        Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("Text1"), TextObject)
        txtObject.Text = "Laporan Deposit DD & Ahtefe"
        CrystalReportViewer1.ReportSource = objRpt
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.Zoom(200)

        Dim strsql As String = "select t.nota 'Nota', d.id_member 'ID Member'," _
                               & "t.jumlah Jumlah from transaksi t,transaksi_deposit d " _
                               & "where kode_trx=1 and t.id_trx=d.id_trx and t.tgl_trx>=curdate() and left(d.id_member,1)!='0'"
        Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
        objConnection2.Close()
        Dim ds As New DataSet1()
        cmd.Fill(ds, "DataTable1")
        objRpt.SetDataSource(ds.Tables(0))
        CrystalReportViewer1.ReportSource = objRpt
        objRpt.PrintToPrinter(1, False, 0, 0)
    End Sub
    Sub printdd()
        Call atur()
        Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
        objConnection2.Close()
        objConnection2.Open()
        Dim objRpt = New CrystalReport6()
        Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("Text1"), TextObject)
        txtObject.Text = "Laporan Deposit DDTronik"
        CrystalReportViewer1.ReportSource = objRpt
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.Zoom(200)

        Dim strsql As String = "select t.nota 'Nota', d.id_member 'ID Member'," _
                               & "t.jumlah Jumlah from transaksi t,transaksi_deposit d " _
                               & "where kode_trx=1 and t.id_trx=d.id_trx and d.operator='" & Form1.tsnama.Text & "' and t.tgl_trx>=curdate() and left(d.id_member,1)!='0'"
        Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
        objConnection2.Close()
        Dim ds As New DataSet1()
        cmd.Fill(ds, "DataTable1")
        objRpt.SetDataSource(ds.Tables(0))
        CrystalReportViewer1.ReportSource = objRpt
        objRpt.PrintToPrinter(1, False, 0, 0)
    End Sub
    Sub printahtefe()
        Call atur()
        Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
        objConnection2.Close()
        objConnection2.Open()
        Dim objRpt = New CrystalReport6()
        Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("Text1"), TextObject)
        txtObject.Text = "Laporan Deposit Ahtefe"
        CrystalReportViewer1.ReportSource = objRpt
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.Zoom(200)

        Dim strsql As String = "select t.nota 'Nota', d.id_member 'ID Member'," _
                               & "t.jumlah Jumlah from transaksi t,transaksi_deposit d " _
                               & "where kode_trx=1 and t.id_trx=d.id_trx and d.operator='" & Form1.tsnama.Text & "' and t.tgl_trx>=curdate() and left(d.id_member,1)='0'"
        Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
        objConnection2.Close()
        Dim ds As New DataSet1()
        cmd.Fill(ds, "DataTable1")
        objRpt.SetDataSource(ds.Tables(0))
        CrystalReportViewer1.ReportSource = objRpt
        objRpt.PrintToPrinter(1, False, 0, 0)
    End Sub

    Public Sub frmStrukPulsa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Button1.Focus()
        Call atur()
        Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='counter_pulsa'")
        objConnection2.Close()
        objConnection2.Open()
        If Form1.raddetailpulsa.Checked = True And cetak = "cetakpulsa" Then
            Dim strsql As String = "select t.nota as Nota,p.kode_produk Kode,h.harga Harga, p.no_tujuan 'No. Tujuan', if(t.status=0,'Tunggu',if(t.status=1,'Sukses','Gagal')) as Status " _
                                   & "from counter_pulsa.transaksi t,counter_pulsa.transaksi_pulsa p," _
                                   & "harga.harga h, harga.operator o where kode_trx=2 and t.id_trx=p.id_trx " _
                                   & "and p.kode_produk=h.kode_produk and h.id_operator=o.id_operator and t.status=" & Form1.statuspulsa & " and t.tgl_trx>=curdate()"
            Dim objRpt = New CrystalReport4()
            Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
            objConnection2.Close()
            Dim ds As New DataSet1()
            cmd.Fill(ds, "DataTable1")
            objRpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            objRpt.PrintToPrinter(1, False, 0, 0)
        ElseIf Form1.radpraktispulsa.Checked = True And cetak = "cetakpulsa" Then
            Dim strsql As String = "select sum(h.harga) 'Total Sukses',count(t.kode_trx) 'Jumlah Sukses' " _
                                   & "from counter_pulsa.transaksi t,harga.harga h,counter_pulsa.transaksi_pulsa p " _
                                   & "where kode_trx=2 and t.id_trx=p.id_trx and p.kode_produk=h.kode_produk and t.status=1 and t.tgl_trx>=curdate()"
            Dim objRpt = New CrystalReport5()
            Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
            objConnection2.Close()
            Dim ds As New DataSet1()
            cmd.Fill(ds, "DataTable1")
            objRpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            objRpt.PrintToPrinter(1, False, 0, 0)
        ElseIf Form1.raddetaildeposit.Checked = True And cetak = "cetakdeposit" Then
            If Form1.raddd.Checked = True Then
                Call printdd()
            ElseIf Form1.radah.Checked = True Then
                Call printahtefe()
            Else
                Call printsemua()
            End If
        Else
            Form1.radpraktisdeposit.Checked = True And cetak = "cetakdeposit"
            Dim strsql As String = "select sum(t.jumlah) 'Total Deposit',count(t.kode_trx) 'Jumlah Deposit' from transaksi t,transaksi_deposit d " _
                                   & "where kode_trx=1 and t.id_trx=d.id_trx and t.tgl_trx>=curdate()"
            Dim objRpt = New CrystalReport7()
            Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
            objConnection2.Close()
            Dim ds As New DataSet1()
            cmd.Fill(ds, "DataTable1")
            objRpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            objRpt.PrintToPrinter(1, False, 0, 0)
        End If
        CrystalReportViewer1.Refresh()
        CrystalReportViewer1.Zoom(150)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

End Class