Imports MySql.Data.MySqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class strukvoucher
    Dim objRpt As New CrystalReport3()
    Private Sub strukvoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call atur()
        Dim objConnection2 As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        objConnection2.Close()
        objConnection2.Open()
        If cetaknota = "cetak" Then
            Dim strsql As String = "select t.nota Nota, v.kode_produk 'Kode Produk', o.nama 'Operator', v.denom 'Denom', " _
                               & "v.no_seri 'No. Seri', t.jumlah 'Harga', i.infovoucher 'Informasi' from counter_pulsa.transaksi t,harga.voucher v, harga.informasi i, " _
                               & "harga.operator o where t.`status`=v.idv and v.id_operator=o.id_operator and t.nota=" & cellklik & " order by t.nota desc limit 1"
            Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
            objConnection2.Close()

            Dim ds As New DataSet1()
            cmd.Fill(ds, "DataTable1")

            objRpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Button1.Text = "Print Customer"
            printcustop = "customer"
            Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("txtcopy"), TextObject)
            txtObject.Text = ""
            lblcetak.Text = "Cetak Struk untuk Costumer"
        Else
            Dim strsql As String = "select t.nota Nota, v.kode_produk 'Kode Produk', o.nama 'Operator', v.denom 'Denom', " _
                               & "v.no_seri 'No. Seri', t.jumlah 'Harga', i.infovoucher 'Informasi' from counter_pulsa.transaksi t,harga.voucher v, harga.informasi i, " _
                               & "harga.operator o where t.`status`=v.idv and v.id_operator=o.id_operator order by t.nota desc limit 1"
            Dim cmd As New MySqlDataAdapter(strsql, objConnection2)
            objConnection2.Close()

            Dim ds As New DataSet1()
            cmd.Fill(ds, "DataTable1")

            objRpt.SetDataSource(ds.Tables(0))
            CrystalReportViewer1.ReportSource = objRpt
            CrystalReportViewer1.Refresh()

            Button1.Text = "Print Customer"
            printcustop = "customer"
            Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("txtcopy"), TextObject)
            txtObject.Text = ""
            lblcetak.Text = "Cetak Struk untuk Costumer"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call atur()
        Dim objConnection As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';database='harga'")
        objConnection.Close()
        objConnection.Open()
        objCommand = New MySqlCommand
        objCommand.Connection = objConnection
        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "update struk set ket='print'"
        objCommand.CommandTimeout = 0
        Dim rsl As Integer = objCommand.ExecuteNonQuery()
        If rsl > 0 Then
            If printcustop = "customer" Then
                objRpt.PrintToPrinter(1, False, 0, 0)
                Button1.Text = "Print Operator"
                lblcetak.Text = "Cetak Struk untuk Operator"
                printcustop = "operator"
                Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("txtcopy"), TextObject)
                txtObject.Text = "~ Copy"
                CrystalReportViewer1.ReportSource = objRpt
                CrystalReportViewer1.Refresh()
            Else
                objRpt.PrintToPrinter(1, False, 0, 0)
                Button1.Text = "Print Customer"
                lblcetak.Text = "Cetak Struk untuk Costumer"
                printcustop = "customer"
                Dim txtObject As TextObject = DirectCast(objRpt.ReportDefinition.Sections("Section1").ReportObjects("txtcopy"), TextObject)
                txtObject.Text = ""
                CrystalReportViewer1.Refresh()
                Me.Close()
                objConnection.Close()
                Form1.Timer1.Start()
            End If
        End If
    End Sub
End Class