Imports DDCounter_Operator.IniFile
Imports MySql.Data.MySqlClient
Public Class SetDb
    Dim inifile As IniFile
    Sub setstatus()
        SQLQuery("update harga.user set status=1 where nama='" & ComboBox1.Text & "'")
    End Sub
    Sub setstatusreverse()
        SQLQuery("update harga.user set status=0 where nama!= '" & ComboBox1.Text & "'")
    End Sub
    Sub loaddata()
        SQLCari("select nama from harga.user")
        With ComboBox1
            .DataSource = objDataTable
            .DisplayMember = "nama"
            .ValueMember = "nama"
        End With
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        inifile = New IniFile
        With inifile
            .AddSection("setting").AddKey("ip").Value = TextBox1.Text
            .Save(Application.StartupPath + "/setting.ini")
        End With
        Try
            SQLCari("select nama,pwd from harga.user where nama='" & ComboBox1.Text & "' and pwd='" & computeHash(txtpass.Text) & "'")
            If objDataTable.Rows.Count > 0 Then
                MsgBox("Koneksi Database Sukses !")
                Form1.ipstatus.Text = "IP Database = " + ip
                Form1.tsnama.Text = ComboBox1.Text
                Form1.Show()
                Call setstatus()
                Call setstatusreverse()
                Me.Hide()
            Else
                MessageBox.Show("Password salah !!", "Set Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Koneksi Database gagal : " & e.ToString)
        End Try
    End Sub

    Private Sub SetDb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        inifile = New IniFile
        inifile.Load(Application.StartupPath + "/setting.ini")
        TextBox1.Text = inifile.GetKeyValue("setting", "ip")
        Call loaddata()
        txtpass.Text = ""
    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Me.Button1_Click(Me, EventArgs.Empty)
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Me.Button1_Click(Me, EventArgs.Empty)
        End If
    End Sub

End Class