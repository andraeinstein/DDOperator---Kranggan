﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class strukdepo
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblcetak = New System.Windows.Forms.Label()
        Me.CrystalReport11 = New DDCounter_Operator.CrystalReport1()
        Me.CrystalReport31 = New DDCounter_Operator.CrystalReport3()
        Me.CrystalReport41 = New DDCounter_Operator.CrystalReport4()
        Me.CrystalReport51 = New DDCounter_Operator.CrystalReport5()
        Me.CrystalReport61 = New DDCounter_Operator.CrystalReport6()
        Me.CrystalReport12 = New DDCounter_Operator.CrystalReport1()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.CrystalReport23 = New DDCounter_Operator.CrystalReport2()
        Me.CrystalReport21 = New DDCounter_Operator.CrystalReport2()
        Me.CrystalReport22 = New DDCounter_Operator.CrystalReport2()
        Me.CrystalReport13 = New DDCounter_Operator.CrystalReport1()
        Me.CrystalReport32 = New DDCounter_Operator.CrystalReport3()
        Me.CrystalReport42 = New DDCounter_Operator.CrystalReport4()
        Me.CrystalReport24 = New DDCounter_Operator.CrystalReport2()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(359, 437)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblcetak
        '
        Me.lblcetak.AutoSize = True
        Me.lblcetak.BackColor = System.Drawing.Color.Transparent
        Me.lblcetak.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblcetak.Location = New System.Drawing.Point(191, 448)
        Me.lblcetak.Name = "lblcetak"
        Me.lblcetak.Size = New System.Drawing.Size(128, 13)
        Me.lblcetak.TabIndex = 6
        Me.lblcetak.Text = "Klik OK untuk cetak struk"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = 0
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.DisplayStatusBar = False
        Me.CrystalReportViewer1.DisplayToolbar = False
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ReportSource = Me.CrystalReport24
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(455, 431)
        Me.CrystalReportViewer1.TabIndex = 0
        Me.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'strukdepo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.DDCounter_Operator.My.Resources.Resources.blackwhite
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(453, 470)
        Me.Controls.Add(Me.lblcetak)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "strukdepo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Struk Deposit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CrystalReport21 As DDCounter_Operator.CrystalReport2
    Friend WithEvents lblcetak As System.Windows.Forms.Label
    Friend WithEvents CrystalReport23 As DDCounter_Operator.CrystalReport2
    Friend WithEvents CrystalReport11 As DDCounter_Operator.CrystalReport1
    Friend WithEvents CrystalReport31 As DDCounter_Operator.CrystalReport3
    Friend WithEvents CrystalReport41 As DDCounter_Operator.CrystalReport4
    Friend WithEvents CrystalReport51 As DDCounter_Operator.CrystalReport5
    Friend WithEvents CrystalReport61 As DDCounter_Operator.CrystalReport6
    Friend WithEvents CrystalReport12 As DDCounter_Operator.CrystalReport1
    Friend WithEvents CrystalReport22 As DDCounter_Operator.CrystalReport2
    Friend WithEvents CrystalReport24 As DDCounter_Operator.CrystalReport2
    Friend WithEvents CrystalReport13 As DDCounter_Operator.CrystalReport1
    Friend WithEvents CrystalReport32 As DDCounter_Operator.CrystalReport3
    Friend WithEvents CrystalReport42 As DDCounter_Operator.CrystalReport4
End Class
