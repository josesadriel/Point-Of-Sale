Imports System.Data.Odbc ' import namespaces ODBC class
Imports System.Data.OleDb
Imports iTextSharp.text ' import namespaces .net pdf library
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.ComponentModel

Public Class Preview_Pembelian
    Private Sub Preview_Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
    End Sub

    Private Sub picClose_Click(sender As Object, e As EventArgs) Handles picClose.Click
        DataGridView1.Rows.Clear()
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtNominal.TextChanged
        If IsNumeric(txtNominal.Text) Then
            If txtNominal.Text <> Nothing Then
                txtKembalian.Text = txtNominal.Text - txtHarga.Text
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsNumeric(txtNominal.Text) Then
            If (txtHarga.Text - txtNominal.Text) <= 0 Then
                Dim beli As String
                For dgrid As Integer = 0 To DataGridView1.RowCount - 2
                    With DataGridView1
                        beli += .Rows(dgrid).Cells(0).Value + "#" + .Rows(dgrid).Cells(1).Value + "#" + .Rows(dgrid).Cells(2).Value + "#" + .Rows(dgrid).Cells(3).Value + "%"
                    End With
                Next
                Try
                    Call koneksi()
                    Dim str As String
                    str = "INSERT INTO `transaksi` (id_transaksi, pembelian, total) VALUES ('" & txtTransaksiID.Text & "', '" & beli & "', " & txtHarga.Text & ");"
                    cmd = New OleDbCommand(str, conn)
                    cmd.ExecuteNonQuery()
                    str = "DELETE * FROM cart;"
                    cmd = New OleDbCommand(str, conn)
                    cmd.ExecuteNonQuery()
                    Kasir.Belanja.PerformClick()
                    Me.Close()
                Catch ex As Exception
                    MsgBox("Gagal")
                End Try

                Dim Paragraph As New Paragraph
                Dim PdfFile As New Document(PageSize.A4, 40, 40, 40, 20)
                PdfFile.AddTitle("Test PDF")

                If Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.Desktop + "\Invoices\") Then
                    System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.Desktop + "\Invoices\")
                End If
                Dim write As PdfWriter = PdfWriter.GetInstance(PdfFile, New FileStream(My.Computer.FileSystem.SpecialDirectories.Desktop + "\Invoices\" + txtTransaksiID.Text + ".pdf", FileMode.Create))
                PdfFile.Open()
                Dim pTitle As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
                Dim pTable As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

                Paragraph = New Paragraph(New Chunk("Test PDF", pTitle))
                Paragraph.Alignment = Element.ALIGN_CENTER
                Paragraph.SpacingAfter = 8.0F

                PdfFile.Add(Paragraph)

                Dim pdfTable As New PdfPTable(DataGridView1.Columns.Count)
                pdfTable.TotalWidth = 500.0F
                pdfTable.LockedWidth = True
                Dim widths(0 To DataGridView1.Columns.Count - 1) As Single
                For i As Integer = 0 To DataGridView1.Columns.Count - 1
                    widths(i) = 1.0F
                Next

                pdfTable.SetWidths(widths)
                pdfTable.HorizontalAlignment = 0
                pdfTable.SpacingBefore = 5.0F

                ' declaration pdf cells
                Dim pdfcell As PdfPCell = New PdfPCell
                ' create pdf header
                For i As Integer = 0 To DataGridView1.Columns.Count - 1
                    pdfcell = New PdfPCell(New Phrase(New Chunk(DataGridView1.Columns(i).HeaderText, pTable)))
                    ' alignment header table
                    pdfcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    pdfcell.BackgroundColor = New iTextSharp.text.BaseColor(240, 240, 240)
                    ' add cells into pdf table
                    pdfTable.AddCell(pdfcell)
                Next

                For i As Integer = 0 To DataGridView1.Rows.Count - 2
                    For j As Integer = 0 To DataGridView1.Columns.Count - 1
                        pdfcell = New PdfPCell(New Phrase(DataGridView1(j, i).Value.ToString(), pTable))
                        pdfTable.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        pdfTable.AddCell(pdfcell)
                    Next
                Next
                ' add pdf table into pdf document
                PdfFile.Add(pdfTable)
                PdfFile.Close() ' close all sessions
                ' show message if hasben exported
                MessageBox.Show("PDF berhasil dibuat !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNominal.Clear()
            End If
        End If
    End Sub
End Class
