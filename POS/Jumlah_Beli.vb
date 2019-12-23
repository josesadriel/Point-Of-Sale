Imports System.Data.OleDb
Public Class Jumlah_Beli
    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs) Handles txtJumlah.TextChanged
        If txtJumlah.Text <> Nothing Then
            If Double.Parse(txtJumlah.Text) <= Double.Parse(Label2.Text) Then
                txtTotal.Text = Double.Parse(txtJumlah.Text) * Double.Parse(Label6.Text)
            Else
                MsgBox("Tidak boleh lebih dari " + Label2.Text)
                txtJumlah.Clear()
                txtTotal.Clear()
            End If
        ElseIf txtJumlah.Text = Nothing Then
            txtTotal.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim split As String() = GroupBox1.Text.Split(" ")
        If txtJumlah.Text <> Nothing And txtTotal.Text <> Nothing Then
            Kasir.dataCart.Rows.Add(New String() {
                                         split(1),
                                         Label6.Text,
                                         txtJumlah.Text,
                                         txtTotal.Text
                                         })
            Dim hasil = Double.Parse(Label2.Text) - Double.Parse(txtJumlah.Text)
            Try
                Call koneksi()
                Dim str As String
                str = "update `stok_barang` set stok = " & hasil & " where nama_barang = '" & split(1) & "'"
                cmd = New OleDbCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Yes")
            Catch ex As Exception
                MsgBox("Gagal")
            End Try
            txtJumlah.Clear()
            txtTotal.Clear()
            Kasir.Show()
            List_Produk.Close()
            Me.Close()
        End If
    End Sub
End Class