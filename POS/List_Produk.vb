Imports System.Data.OleDb
Public Class List_Produk

    Private Sub List_Produk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datagrid("")
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
    End Sub

    Private Sub datagrid(keyword As String)
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"
        koneksi.Open()
        Dim das As New DataSet
        Dim daa As OleDbDataAdapter
        daa = New OleDbDataAdapter("select * from stok_barang where nama_barang like '%" & keyword & "'", koneksi)
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

    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click
        datagrid(TextBox1.Text)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        datagrid(TextBox1.Text)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView1.Rows(index)
        Jumlah_Beli.GroupBox1.Text = "Beli " + selectedRow.Cells(0).Value.ToString()
        Jumlah_Beli.Label2.Text = selectedRow.Cells(2).Value.ToString()
        Jumlah_Beli.Label6.Text = selectedRow.Cells(1).Value.ToString()
        Jumlah_Beli.ShowDialog()
    End Sub

    Private Sub List_Produk_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Kasir.Show()
    End Sub
End Class