Imports System.Data.Odbc ' import namespaces ODBC class
Imports iTextSharp.text ' import namespaces .net pdf library
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.ComponentModel
Imports System.Data.OleDb

Public Class Kasir
    'load
    Private Sub loadData(keyword As String)
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"

        koneksi.Open()
        Dim das As New DataSet
        Dim daa As OleDbDataAdapter
        Dim dasSearch As New DataSet
        Dim daaSearch As OleDbDataAdapter
        Dim dasCart As New DataSet
        Dim daaCart As OleDbDataAdapter
        daaCart = New OleDbDataAdapter("select * from cart", koneksi)
        daa = New OleDbDataAdapter("select * from stok_barang", koneksi)
        daaSearch = New OleDbDataAdapter("select * from stok_barang where nama_barang like '%" & keyword & "%'", koneksi)
        daa.Fill(das, "stok_barang")
        daaSearch.Fill(dasSearch, "stok_barang")

        daaCart.Fill(dasCart, "cart")
        koneksi.Close()
        dataProduk.DataSource = das.Tables("stok_barang")
        dataList.DataSource = dasSearch.Tables("stok_barang")
        dataCart.DataSource = dasCart.Tables("cart")
        With dataCart
            .RowHeadersVisible = False
            .Columns(1).HeaderCell.Value = "Nama Produk"
            .Columns(2).HeaderCell.Value = "Harga"
            .Columns(3).HeaderCell.Value = "Jumlah"
            .Columns(4).HeaderCell.Value = "Total"
            .Columns(0).HeaderCell.Value = "Action"
            .Columns(0).DisplayIndex = 5
            .Columns(1).DisplayIndex = 0
            .Columns(2).DisplayIndex = 1
            .Columns(3).DisplayIndex = 2
            .Columns(4).DisplayIndex = 3
            .Columns(5).DisplayIndex = 4
            .Columns(2).Width = 60
            .Columns(3).Width = 60
            .Columns(4).Width = 80
        End With
    End Sub

    Private Sub Kasir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadData("")
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
    End Sub

    ' Panel Cart Start
    Private Sub Cart_Click(sender As Object, e As EventArgs) Handles Cart.Click
        loadData("")
        dataCart_CurrentCellChanged(dataCart, New EventArgs())
        Add.BackColor = Color.Transparent
        Cart.BackColor = Color.FromArgb(152, 182, 242)
        Belanja.BackColor = Color.Transparent
        Laporan.BackColor = Color.Transparent
        pnlHover.Location = Cart.Location
        pnlCart.Visible = True
        pnlProduk.Visible = False
        pnlBelanja.Visible = False
        pnlLaporan.Visible = False
    End Sub

    Private Sub dataCart_CurrentCellChanged(sender As Object, e As EventArgs) Handles dataCart.CurrentCellChanged
        If dataCart.RowCount > 1 Then
            Dim totalCart As Integer = 0
            For indexCart As Integer = 0 To dataCart.RowCount - 1
                totalCart += dataCart.Rows(indexCart).Cells(4).Value
            Next
            Label6.Text = totalCart.ToString
            bayar.Visible = True
        Else
            Label6.Text = 0
            bayar.Visible = False
        End If
    End Sub

    Private Sub dataCart_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataCart.CellClick
        Try
            Dim waktu As Date
            With dataCart
                If .Columns(e.ColumnIndex).HeaderText = "Action" And .Rows(e.RowIndex).Cells(4).Value <> 0 Then
                    pnlCancelCart.Visible = True
                    txtJumlahCancel.Clear()
                    txtJumlahCancel.Select()
                    grpCancelCart.Text = "Batal Beli " & .Rows(e.RowIndex).Cells(1).Value & ""
                    txtRangeCancel.Text = .Rows(e.RowIndex).Cells(3).Value
                    txtKelipatanCancel.Text = .Rows(e.RowIndex).Cells(2).Value
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dataCart_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dataCart.CellFormatting
        If dataCart.Rows.Count > 1 Then
            If e.ColumnIndex = 5 Then
                Dim d As Date
                If Date.TryParse(e.Value, d) Then
                    e.Value = d.ToString("HH:mm:ss")
                    e.FormattingApplied = True
                End If
            End If
        End If
    End Sub

    Private Sub bayar_Click(sender As Object, e As EventArgs) Handles bayar.Click
        For dgv As Integer = 0 To dataCart.RowCount - 2
            With dataCart
                Preview_Pembelian.DataGridView1.Rows.Add(New String() {.Rows(dgv).Cells(1).Value, .Rows(dgv).Cells(2).Value, .Rows(dgv).Cells(3).Value, .Rows(dgv).Cells(4).Value})
            End With
        Next
        Preview_Pembelian.txtTransaksiID.Text = "B" + Date.Now.ToString("ddMMyyyyhhmmss")
        Preview_Pembelian.txtHarga.Text = Label6.Text
        Preview_Pembelian.txtNominal.Select()
        Preview_Pembelian.ShowDialog()
    End Sub

    Private Sub btnCancelCart_Click(sender As Object, e As EventArgs) Handles btnCancelCart.Click
        Dim split As String() = grpCancelCart.Text.Split(" ")
        Dim index As Integer = dataCart.CurrentCell.RowIndex
        If txtJumlahCancel.Text <> Nothing And txtTotalCancel.Text <> Nothing Then
            Dim hasil = Double.Parse(txtRangeCancel.Text) - Double.Parse(txtJumlahCancel.Text)
            Try
                Call koneksi()
                Dim str As String
                str = "update `stok_barang` set stok = stok + " & Double.Parse(txtJumlahCancel.Text) & " where nama_barang = '" & split(2) & "'"
                cmd = New OleDbCommand(str, conn)
                cmd.ExecuteNonQuery()
                If hasil = 0 Then
                    str = "delete from `cart` where nama_barang = '" & split(2) & "'"
                    cmd = New OleDbCommand(str, conn)
                    cmd.ExecuteNonQuery()
                Else
                    str = "update `cart` set jumlah = jumlah - " & txtJumlahCancel.Text & ", total = total - (" & txtJumlahCancel.Text * txtKelipatanCancel.Text & ") where nama_barang = '" & split(2) & "'"
                    cmd = New OleDbCommand(str, conn)
                    cmd.ExecuteNonQuery()
                End If
                MsgBox("Yes")
                loadData("")
                dataCart_CurrentCellChanged(dataCart, New EventArgs())
                pnlCancelCart.Visible = False
            Catch ex As Exception
                MsgBox("Gagal")
            End Try

            txtJumlahCancel.Clear()
            txtTotalCancel.Clear()
        End If
    End Sub

    Private Sub txtJumlahCancel_TextChanged(sender As Object, e As EventArgs) Handles txtJumlahCancel.TextChanged
        Try
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
        Catch ex As Exception
            MsgBox("Input Salah !", MsgBoxStyle.Critical, "Kesalahan")
            txtJumlahCancel.Clear()
        End Try
    End Sub
    ' Panel Cart END

    ' Panel Produk START
    Private Sub Add_Click_1(sender As Object, e As EventArgs) Handles Add.Click
        loadData("")
        Add.BackColor = Color.FromArgb(152, 182, 242)
        Cart.BackColor = Color.Transparent
        Belanja.BackColor = Color.Transparent
        Laporan.BackColor = Color.Transparent
        pnlHover.Location = Add.Location
        pnlProduk.Visible = True
        pnlCart.Visible = False
        pnlBelanja.Visible = False
        pnlLaporan.Visible = False
        With dataProduk
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "Nama Produk"
            .Columns(1).HeaderCell.Value = "Harga"
            .Columns(2).HeaderCell.Value = "Stok"
        End With
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Call koneksi()
            Dim str As String
            str = "insert into `stok_barang`(nama_barang, harga_barang, stok) values ('" & txtNama.Text & "', " & txtHarga.Text & ", " & txtStok.Text & ")"
            cmd = New OleDbCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Yes")
            txtNama.Clear()
            txtHarga.Clear()
            txtStok.Clear()
            loadData("")
        Catch ex As Exception
            MsgBox("Gagal")
        End Try
    End Sub

    Private Sub dataProduk_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataProduk.CellClick
        Try
            Dim index As Integer
            index = e.RowIndex
            Dim selectedRow As DataGridViewRow
            selectedRow = dataProduk.Rows(index)
            namaSelected.Text = selectedRow.Cells(0).Value.ToString()
            hargaSelected.Text = selectedRow.Cells(1).Value.ToString()
            stokSelected.Text = selectedRow.Cells(2).Value.ToString()
        Catch ex As Exception

        End Try
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
                loadData("")
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
                namaSelected.Clear()
                hargaSelected.Clear()
                stokSelected.Clear()
                loadData("")
            Catch ex As Exception
                MsgBox("Gagal")
            End Try
        End If
    End Sub
    ' Panel Produk END

    ' Panel Belanja START
    Private Sub Belanja_Click_1(sender As Object, e As EventArgs) Handles Belanja.Click
        loadData("")
        pnlHover.Location = Belanja.Location
        pnlCart.Visible = False
        pnlProduk.Visible = False
        pnlBelanja.Visible = True
        pnlLaporan.Visible = False
        Add.BackColor = Color.Transparent
        Cart.BackColor = Color.Transparent
        Belanja.BackColor = Color.FromArgb(152, 182, 242)
        Laporan.BackColor = Color.Transparent
        With dataList
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "Nama Produk"
            .Columns(1).HeaderCell.Value = "Harga"
            .Columns(2).HeaderCell.Value = "Stok"
        End With
    End Sub

    Private Sub dataList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataList.CellClick
        Try
            grpJmlhBeli.Visible = True
            txtJumlahBeli.Clear()
            txtJumlahBeli.Select()
            Dim index As Integer
            index = e.RowIndex
            Dim selectedRow As DataGridViewRow
            selectedRow = dataList.Rows(index)
            grpJmlhBeli.Text = "Beli " + selectedRow.Cells(0).Value.ToString()
            txtKelipatanBeli.Text = selectedRow.Cells(1).Value.ToString()
            txtRangeBeli.Text = selectedRow.Cells(2).Value.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs) Handles txtJumlahBeli.TextChanged
        Try
            If txtJumlahBeli.Text <> Nothing Then
                If Double.Parse(txtJumlahBeli.Text) <= Double.Parse(txtRangeBeli.Text) Then
                    txtTotal.Text = Double.Parse(txtJumlahBeli.Text) * Double.Parse(txtKelipatanBeli.Text)
                Else
                    MsgBox("Tidak boleh lebih dari " + txtRangeBeli.Text)
                    txtJumlahBeli.Clear()
                    txtTotal.Clear()
                End If
            ElseIf txtJumlahBeli.Text = Nothing Then
                txtTotal.Clear()
            End If
        Catch ex As Exception
            MsgBox("Input Salah", MsgBoxStyle.Critical, "Kesalahan")
            txtJumlahBeli.Clear()
        End Try
    End Sub

    Private Sub btnBeli_Click(sender As Object, e As EventArgs) Handles btnBeli.Click
        Try
            Dim split As String() = grpJmlhBeli.Text.Split(" ")
            Dim total As Integer = txtJumlahBeli.Text * txtKelipatanBeli.Text
            Dim check As Boolean = False
            If txtJumlahBeli.Text <> Nothing And txtTotal.Text <> Nothing Then
                Dim hasil = Double.Parse(txtRangeBeli.Text) - Double.Parse(txtJumlahBeli.Text)
                For cok As Integer = 0 To dataCart.Rows.Count - 1
                    If dataCart.Rows(cok).Cells(1).Value = split(1) Then
                        check = True
                    End If
                Next
                Try
                    Call koneksi()
                    Dim str As String
                    str = "update `stok_barang` set stok = " & hasil & " where nama_barang = '" & split(1) & "'"
                    cmd = New OleDbCommand(str, conn)
                    cmd.ExecuteNonQuery()
                    If check = True Then
                        str = "update `cart` set jumlah = jumlah + " & txtJumlahBeli.Text & ", total = total + (" & txtJumlahBeli.Text * txtKelipatanBeli.Text & ") where nama_barang = '" & split(1) & "'"
                        cmd = New OleDbCommand(str, conn)
                        cmd.ExecuteNonQuery()
                        MsgBox("Update Berhasil")
                        loadData("")
                        check = False
                    Else
                        str = "insert into `cart`(nama_barang, harga_barang, jumlah, total) VALUES ('" & split(1) & "', " & txtKelipatanBeli.Text & ", " & txtJumlahBeli.Text & ", (" & txtJumlahBeli.Text * txtKelipatanBeli.Text & "))"
                        cmd = New OleDbCommand(str, conn)
                        cmd.ExecuteNonQuery()
                        MsgBox("Insert Berhasil")
                        loadData("")
                    End If
                Catch ex As Exception
                    MsgBox("Gagal")
                End Try
                txtJumlahBeli.Clear()
                txtTotal.Clear()
                grpJmlhBeli.Visible = False
            End If
        Catch ex As Exception
            MsgBox("Input Salah", MsgBoxStyle.Critical, "Kesalahan")
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        loadData(txtSearch.Text)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        loadData(txtSearch.Text)
    End Sub
    ' Panel Belanja END

    ' Panel Laporan START
    Private Sub Laporan_Click(sender As Object, e As EventArgs) Handles Laporan.Click
        Laporan.BackColor = Color.FromArgb(152, 182, 242)
        Cart.BackColor = Color.Transparent
        Belanja.BackColor = Color.Transparent
        Add.BackColor = Color.Transparent
        pnlHover.Location = Laporan.Location
        pnlProduk.Visible = False
        pnlCart.Visible = False
        pnlLaporan.Visible = True
        pnlBelanja.Visible = False

        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"

        koneksi.Open()
        Dim dToday As New OleDbCommand("SELECT SUM(total) FROM `transaksi` WHERE DAY(waktu) = " & Date.Now.ToString("dd") & ";", koneksi)
        Using readToday As OleDbDataReader = dToday.ExecuteReader()
            While readToday.Read()
                nominalSkrg.Text = readToday.GetValue(0).ToString
            End While
        End Using
        Dim dMonth As New OleDbCommand("SELECT SUM(total) FROM `transaksi` WHERE MONTH(waktu) = " & Date.Now.ToString("MM") & ";", koneksi)
        Using readMonth As OleDbDataReader = dMonth.ExecuteReader()
            While readMonth.Read()
                nominalBln.Text = readMonth.GetValue(0).ToString
            End While
        End Using
        Dim dYear As New OleDbCommand("SELECT SUM(total) FROM `transaksi` WHERE YEAR(waktu) = " & Date.Now.ToString("yyyy") & ";", koneksi)
        Using readYear As OleDbDataReader = dYear.ExecuteReader()
            While readYear.Read()
                nominalThn.Text = readYear.GetValue(0).ToString
            End While
        End Using
        koneksi.Close()
    End Sub

    Private Sub btnLapToday_Click(sender As Object, e As EventArgs) Handles btnLapToday.Click
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"

        koneksi.Open()
        Dim dToday As OleDbDataAdapter
        Dim dsToday As New DataSet
        dToday = New OleDbDataAdapter("SELECT id_transaksi, total, waktu FROM `transaksi` WHERE DAY(waktu) = " & Date.Now.ToString("dd") & ";", koneksi)
        dToday.Fill(dsToday, "transaksi")
        koneksi.Close()
        dataLaporan.DataSource = dsToday.Tables("transaksi")
        With dataLaporan
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "ID Transaksi"
            .Columns(1).HeaderCell.Value = "Total"
            .Columns(2).HeaderCell.Value = "Waktu"
        End With
    End Sub

    Private Sub btnLapMonth_Click(sender As Object, e As EventArgs) Handles btnLapMonth.Click
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"

        koneksi.Open()
        Dim dMonth As OleDbDataAdapter
        Dim dsMonth As New DataSet
        dMonth = New OleDbDataAdapter("SELECT id_transaksi, total, waktu FROM `transaksi` WHERE MONTH(waktu) = " & Date.Now.ToString("MM") & ";", koneksi)
        dMonth.Fill(dsMonth, "transaksi")
        koneksi.Close()
        dataLaporan.DataSource = dsMonth.Tables("transaksi")
        With dataLaporan
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "ID Transaksi"
            .Columns(1).HeaderCell.Value = "Total"
            .Columns(2).HeaderCell.Value = "Waktu"
        End With
    End Sub

    Private Sub btnLapYear_Click(sender As Object, e As EventArgs) Handles btnLapYear.Click
        Dim koneksi As New OleDbConnection
        koneksi.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=POS.mdb"

        koneksi.Open()
        Dim dYear As OleDbDataAdapter
        Dim dsYear As New DataSet
        dYear = New OleDbDataAdapter("SELECT id_transaksi, total, waktu FROM `transaksi` WHERE YEAR(waktu) = " & Date.Now.ToString("yyyy") & ";", koneksi)
        dYear.Fill(dsYear, "transaksi")
        koneksi.Close()
        dataLaporan.DataSource = dsYear.Tables("transaksi")
        With dataLaporan
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "ID Transaksi"
            .Columns(1).HeaderCell.Value = "Total"
            .Columns(2).HeaderCell.Value = "Waktu"
        End With
    End Sub
    ' Panel Laporan END

    Private Sub picClose_Click(sender As Object, e As EventArgs) Handles picClose.Click
        Me.Close()
    End Sub

    Private Sub picMin_Click(sender As Object, e As EventArgs) Handles picMin.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub picMax_Click(sender As Object, e As EventArgs) Handles picMax.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        ElseIf Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        pnlCancelCart.Visible = False
    End Sub
End Class