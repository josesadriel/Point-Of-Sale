Imports System.Data.OleDb
Public Class Add_Product
    Private Sub loadData()
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"
        koneksi.Open()
        Dim das As New DataSet
        Dim daa As OleDbDataAdapter
        daa = New OleDbDataAdapter("select * from stok_barang", koneksi)
        daa.Fill(das, "stok_barang")
        koneksi.Close()
        DataGridView1.DataSource = das.Tables("stok_barang")
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "Nama Produk"
            .Columns(1).HeaderCell.Value = "Harga"
            .Columns(2).HeaderCell.Value = "Stok"
            .Columns(3).HeaderCell.Value = "Terjual"
        End With
    End Sub

    Private Sub Add_Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData()
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Call koneksi()
            Dim str As String
            str = "insert into `stok_barang`(nama_barang, harga_barang, stok) values ('" & txtNama.Text & "', " & txtHarga.Text & ", " & txtStok.Text & ")"
            cmd = New OleDbCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Yes")
            loadData()
        Catch ex As Exception
            MsgBox("Gagal")
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView1.Rows(index)
        namaSelected.Text = selectedRow.Cells(0).Value.ToString()
        hargaSelected.Text = selectedRow.Cells(1).Value.ToString()
        stokSelected.Text = selectedRow.Cells(2).Value.ToString()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If namaSelected.Text <> Nothing Then
            Try
                Call koneksi()
                Dim str As String
                str = "update `stok_barang` set nama_barang = '" & namaSelected.Text & "', harga_barang = " & hargaSelected.Text & ", stok = " & stokSelected.Text & " where nama_barang = '" & namaSelected.Text & "'"
                cmd = New OleDbCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Yes")
                loadData()
            Catch ex As Exception
                MsgBox("Gagal")
            End Try
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If namaSelected.Text <> Nothing Then
            Try
                Call koneksi()
                Dim str As String
                str = "delete from `stok_barang` where nama_barang = '" & namaSelected.Text & "'"
                cmd = New OleDbCommand(str, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Yes")
                loadData()
            Catch ex As Exception
                MsgBox("Gagal")
            End Try
        End If
    End Sub
End Class