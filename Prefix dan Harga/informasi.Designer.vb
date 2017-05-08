<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class informasi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtinfo = New System.Windows.Forms.TextBox()
        Me.radaktif = New System.Windows.Forms.RadioButton()
        Me.radnonaktif = New System.Windows.Forms.RadioButton()
        Me.btnsimpan = New System.Windows.Forms.Button()
        Me.txtinfopulsa = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtinfodeposit = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtinfovoucher = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 216)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(174, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Informasi Tampilan Client :"
        '
        'txtinfo
        '
        Me.txtinfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinfo.Location = New System.Drawing.Point(257, 213)
        Me.txtinfo.MaxLength = 80
        Me.txtinfo.Multiline = True
        Me.txtinfo.Name = "txtinfo"
        Me.txtinfo.Size = New System.Drawing.Size(482, 62)
        Me.txtinfo.TabIndex = 0
        '
        'radaktif
        '
        Me.radaktif.AutoSize = True
        Me.radaktif.BackColor = System.Drawing.Color.Transparent
        Me.radaktif.Location = New System.Drawing.Point(257, 290)
        Me.radaktif.Name = "radaktif"
        Me.radaktif.Size = New System.Drawing.Size(138, 17)
        Me.radaktif.TabIndex = 1
        Me.radaktif.TabStop = True
        Me.radaktif.Text = "Aktifkan Informasi Client"
        Me.radaktif.UseVisualStyleBackColor = False
        '
        'radnonaktif
        '
        Me.radnonaktif.AutoSize = True
        Me.radnonaktif.BackColor = System.Drawing.Color.Transparent
        Me.radnonaktif.Location = New System.Drawing.Point(401, 290)
        Me.radnonaktif.Name = "radnonaktif"
        Me.radnonaktif.Size = New System.Drawing.Size(161, 17)
        Me.radnonaktif.TabIndex = 2
        Me.radnonaktif.TabStop = True
        Me.radnonaktif.Text = "Non-Aktifkan Informasi Client"
        Me.radnonaktif.UseVisualStyleBackColor = False
        '
        'btnsimpan
        '
        Me.btnsimpan.Location = New System.Drawing.Point(649, 281)
        Me.btnsimpan.Name = "btnsimpan"
        Me.btnsimpan.Size = New System.Drawing.Size(90, 34)
        Me.btnsimpan.TabIndex = 3
        Me.btnsimpan.Text = "Simpan"
        Me.btnsimpan.UseVisualStyleBackColor = True
        '
        'txtinfopulsa
        '
        Me.txtinfopulsa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinfopulsa.Location = New System.Drawing.Point(256, 9)
        Me.txtinfopulsa.MaxLength = 80
        Me.txtinfopulsa.Multiline = True
        Me.txtinfopulsa.Name = "txtinfopulsa"
        Me.txtinfopulsa.Size = New System.Drawing.Size(482, 62)
        Me.txtinfopulsa.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(216, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Informasi Struk Penjualan Pulsa :"
        '
        'txtinfodeposit
        '
        Me.txtinfodeposit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinfodeposit.Location = New System.Drawing.Point(256, 77)
        Me.txtinfodeposit.MaxLength = 80
        Me.txtinfodeposit.Multiline = True
        Me.txtinfodeposit.Name = "txtinfodeposit"
        Me.txtinfodeposit.Size = New System.Drawing.Size(482, 62)
        Me.txtinfodeposit.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(162, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Informasi Struk Deposit :"
        '
        'txtinfovoucher
        '
        Me.txtinfovoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinfovoucher.Location = New System.Drawing.Point(256, 145)
        Me.txtinfovoucher.MaxLength = 80
        Me.txtinfovoucher.Multiline = True
        Me.txtinfovoucher.Name = "txtinfovoucher"
        Me.txtinfovoucher.Size = New System.Drawing.Size(482, 62)
        Me.txtinfovoucher.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(234, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Informasi Struk Penjualan Voucher :"
        '
        'informasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.DDCounter_Operator.My.Resources.Resources.blackwhite
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(751, 324)
        Me.Controls.Add(Me.txtinfovoucher)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtinfodeposit)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtinfopulsa)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnsimpan)
        Me.Controls.Add(Me.radnonaktif)
        Me.Controls.Add(Me.radaktif)
        Me.Controls.Add(Me.txtinfo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "informasi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Informasi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtinfo As System.Windows.Forms.TextBox
    Friend WithEvents radaktif As System.Windows.Forms.RadioButton
    Friend WithEvents radnonaktif As System.Windows.Forms.RadioButton
    Friend WithEvents btnsimpan As System.Windows.Forms.Button
    Friend WithEvents txtinfopulsa As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtinfodeposit As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtinfovoucher As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
