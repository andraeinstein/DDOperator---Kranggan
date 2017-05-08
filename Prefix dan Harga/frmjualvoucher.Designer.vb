<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmjualvoucher
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
        Me.dtgvouc = New System.Windows.Forms.DataGridView()
        Me.btnjual = New System.Windows.Forms.Button()
        CType(Me.dtgvouc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgvouc
        '
        Me.dtgvouc.AllowUserToAddRows = False
        Me.dtgvouc.AllowUserToDeleteRows = False
        Me.dtgvouc.AllowUserToResizeColumns = False
        Me.dtgvouc.AllowUserToResizeRows = False
        Me.dtgvouc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtgvouc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dtgvouc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dtgvouc.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.dtgvouc.Location = New System.Drawing.Point(5, 4)
        Me.dtgvouc.MultiSelect = False
        Me.dtgvouc.Name = "dtgvouc"
        Me.dtgvouc.ReadOnly = True
        Me.dtgvouc.RowHeadersVisible = False
        Me.dtgvouc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dtgvouc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgvouc.Size = New System.Drawing.Size(647, 346)
        Me.dtgvouc.StandardTab = True
        Me.dtgvouc.TabIndex = 40
        '
        'btnjual
        '
        Me.btnjual.Location = New System.Drawing.Point(541, 356)
        Me.btnjual.Name = "btnjual"
        Me.btnjual.Size = New System.Drawing.Size(111, 36)
        Me.btnjual.TabIndex = 41
        Me.btnjual.Text = "Proses Jual"
        Me.btnjual.UseVisualStyleBackColor = True
        '
        'frmjualvoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.DDCounter_Operator.My.Resources.Resources.blackwhite
        Me.ClientSize = New System.Drawing.Size(657, 398)
        Me.Controls.Add(Me.btnjual)
        Me.Controls.Add(Me.dtgvouc)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmjualvoucher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Penjualan Voucher Fisik"
        CType(Me.dtgvouc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents dtgvouc As System.Windows.Forms.DataGridView
    Friend WithEvents btnjual As System.Windows.Forms.Button
End Class
