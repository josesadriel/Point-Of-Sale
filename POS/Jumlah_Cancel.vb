Imports System.Data.OleDb
Public Class Jumlah_Cancel
    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs) Handles txtJumlahCancel.TextChanged
        If txtJumlahCancel.Text <> Nothing Then
            If Double.Parse(txtJumlahCancel.Text) <= Double.Parse(txtRangeCancel.Text) Then
                txtTotalCancel.Text = Double.Parse(txtJumlahCancel.Text) * Double.Parse(txtKelipatanCancel.Text)
            Else
                MsgBox("Tidak boleh lebih dari " + txtRangeCancel.Text)
                txtJumlahCancel.Clear()
                txtTotalCancel.Clear()
            End If
        ElseIf txtJumlahCancel.Text = Nothing Then
            txtTotalCancel.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCancelCart.Click
        Dim split As String() = grpCancelCart.Text.Split(" ")
        Dim index As Integer = Kasir.dataCart.CurrentCell.RowIndex
        If txtJumlahCancel.Text <> Nothing And txtTotalCancel.Text <> Nothing Then
            Dim hasil = Double.Parse(txtRangeCancel.Text) - Double.Parse(txtJumlahCancel.Text)
            Try
                Call koneksi()
                Dim str As String
                str = "update `stok_barang` set stok = stok + " & Double.Parse(txtJumlahCancel.Text) & " where nama_barang = '" & split(2) & "'"
                cmd = New OleDbCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Yes")
                If hasil = 0 Then
                    Kasir.dataCart.Rows.RemoveAt(index)
                Else
                    Kasir.dataCart.Rows(index).Cells(2).Value = hasil
                End If
            Catch ex As Exception
                MsgBox("Gagal")
            End Try

            txtJumlahCancel.Clear()
            txtTotalCancel.Clear()
            Kasir.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Jumlah_Cancel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2

    End Sub

End Class