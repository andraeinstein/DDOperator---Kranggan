Imports MySql.Data.MySqlClient
Module Module1
    Dim inifile As IniFile
    Public ip As String
    Public jumlahdata
    Public printcustop
    Public cetak As String
    Public cetaknota As String
    Public cellklik As Integer

    Public Sub atur()
        inifile = New IniFile
        inifile.Load(Application.StartupPath + "/setting.ini")
        ip = inifile.GetKeyValue("setting", "ip")
    End Sub

    Public objConnection2 As New MySqlConnection("Server='192.168.2.50';port=3306;user id='root';password='';database='harga'")
    Public objCommand As MySqlCommand
    Public objDataSet As New DataSet
    Public objDataTable As New DataTable
    Public objDataReader As MySqlDataReader
    Public objDataAdapter As New MySqlDataAdapter
    Public StrSQL As String
    Public Username, Password, mProcess As String
    Public cur As New Form
    Public Msg As Integer
    Public db, user, pwd As String
    Public myConnectionString As String
    Public cek As MySqlDataReader
    Public ds As New DataSet

    Public Function Terbilang(ByVal nilai As Long) As String
        Dim bilangan As String() = {"", "satu", "dua", "tiga", "empat", "lima", _
        "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas"}
        If nilai < 12 Then
            Return " " & bilangan(nilai)
        ElseIf nilai < 20 Then
            Return Terbilang(nilai - 10) & " belas"
        ElseIf nilai < 100 Then
            Return (Terbilang(CInt((nilai \ 10))) & " puluh") + Terbilang(nilai Mod 10)
        ElseIf nilai < 200 Then
            Return " seratus" & Terbilang(nilai - 100)
        ElseIf nilai < 1000 Then
            Return (Terbilang(CInt((nilai \ 100))) & " ratus") + Terbilang(nilai Mod 100)
        ElseIf nilai < 2000 Then
            Return " seribu" & Terbilang(nilai - 1000)
        ElseIf nilai < 1000000 Then
            Return (Terbilang(CInt((nilai \ 1000))) & " ribu") + Terbilang(nilai Mod 1000)
        ElseIf nilai < 1000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000))) & " juta") + Terbilang(nilai Mod 1000000)
        ElseIf nilai < 1000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000))) & " milyar") + Terbilang(nilai Mod 1000000000)
        ElseIf nilai < 1000000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000000))) & " triliun") + Terbilang(nilai Mod 1000000000000)
        Else
            Return ""
        End If
    End Function

    Public Function computeHash(ByVal textToHash As String) As String
        '//dibawah ini adalah sebuah fungsi untuk mengenkripsi text dengan metode MD5
        Dim MD5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim Bytes() As Byte = MD5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(textToHash))
        Dim s As String = Nothing
        For Each by As Byte In Bytes
            s += by.ToString("x2")
        Next
        Return s
    End Function

    Public Sub SQLQuery(ByVal SQCommand As String)
        Call atur()
        Using koneksi As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';")
            Using SQLCMD As New MySqlCommand(SQCommand, koneksi)
                koneksi.Open()
                SQLCMD.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub SQLCari(ByVal SQCommand As String)
        Call atur()

        Using koneksi As New MySqlConnection("Server='" & ip & "';port=3306;user id='root';password='';")
            Using da As New MySqlDataAdapter(SQCommand, koneksi)
                objDataTable = New DataTable
                koneksi.Open()
                da.Fill(objDataTable)
            End Using
        End Using
    End Sub

End Module