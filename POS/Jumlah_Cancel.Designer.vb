<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Jumlah_Cancel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.grpCancelCart = New System.Windows.Forms.GroupBox()
        Me.txtKelipatanCancel = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCancelCart = New System.Windows.Forms.Button()
        Me.txtTotalCancel = New System.Windows.Forms.TextBox()
        Me.txtJumlahCancel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRangeCancel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpCancelCart.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpCancelCart
        '
        Me.grpCancelCart.Controls.Add(Me.txtKelipatanCancel)
        Me.grpCancelCart.Controls.Add(Me.Label5)
        Me.grpCancelCart.Controls.Add(Me.btnCancelCart)
        Me.grpCancelCart.Controls.Add(Me.txtTotalCancel)
        Me.grpCancelCart.Controls.Add(Me.txtJumlahCancel)
        Me.grpCancelCart.Controls.Add(Me.Label4)
        Me.grpCancelCart.Controls.Add(Me.Label3)
        Me.grpCancelCart.Controls.Add(Me.txtRangeCancel)
        Me.grpCancelCart.Controls.Add(Me.Label1)
        Me.grpCancelCart.Location = New System.Drawing.Point(12, 12)
        Me.grpCancelCart.Name = "grpCancelCart"
        Me.grpCancelCart.Size = New System.Drawing.Size(260, 145)
        Me.grpCancelCart.TabIndex = 1
        Me.grpCancelCart.TabStop = False
        Me.grpCancelCart.Text = "GroupBox1"
        '
        'txtKelipatanCancel
        '
        Me.txtKelipatanCancel.AutoSize = True
        Me.txtKelipatanCancel.Location = New System.Drawing.Point(139, 33)
        Me.txtKelipatanCancel.Name = "txtKelipatanCancel"
        Me.txtKelipatanCancel.Size = New System.Drawing.Size(39, 13)
        Me.txtKelipatanCancel.TabIndex = 8
        Me.txtKelipatanCancel.Text = "Label6"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(72, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Kelipatan Rp."
        '
        'btnCancelCart
        '
        Me.btnCancelCart.Location = New System.Drawing.Point(82, 108)
        Me.btnCancelCart.Name = "btnCancelCart"
        Me.btnCancelCart.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelCart.TabIndex = 6
        Me.btnCancelCart.Text = "OK"
        Me.btnCancelCart.UseVisualStyleBackColor = True
        '
        'txtTotalCancel
        '
        Me.txtTotalCancel.Location = New System.Drawing.Point(57, 82)
        Me.txtTotalCancel.Name = "txtTotalCancel"
        Me.txtTotalCancel.ReadOnly = True
        Me.txtTotalCancel.Size = New System.Drawing.Size(100, 20)
        Me.txtTotalCancel.TabIndex = 5
        '
        'txtJumlahCancel
        '
        Me.txtJumlahCancel.Location = New System.Drawing.Point(57, 59)
        Me.txtJumlahCancel.Name = "txtJumlahCancel"
        Me.txtJumlahCancel.Size = New System.Drawing.Size(100, 20)
        Me.txtJumlahCancel.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Total:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Jumlah:"
        '
        'txtRangeCancel
        '
        Me.txtRangeCancel.AutoSize = True
        Me.txtRangeCancel.Location = New System.Drawing.Point(197, 16)
        Me.txtRangeCancel.Name = "txtRangeCancel"
        Me.txtRangeCancel.Size = New System.Drawing.Size(13, 13)
        Me.txtRangeCancel.TabIndex = 1
        Me.txtRangeCancel.Text = "?"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Range Pembatalan: 0 sampai"
        '
        'Jumlah_Cancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 169)
        Me.Controls.Add(Me.grpCancelCart)
        Me.Name = "Jumlah_Cancel"
        Me.Text = "Jumlah_Cancel"
        Me.grpCancelCart.ResumeLayout(False)
        Me.grpCancelCart.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpCancelCart As GroupBox
    Friend WithEvents txtKelipatanCancel As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnCancelCart As Button
    Friend WithEvents txtTotalCancel As TextBox
    Friend WithEvents txtJumlahCancel As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtRangeCancel As Label
    Friend WithEvents Label1 As Label
End Class
