Imports WebCam_Capture
Imports MessagingToolkit.QRCode.Codec

Public Class Scan_Barcode
    WithEvents MyWebcam As WebCamCapture
    Dim Reader As QRCodeDecoder

    Private Sub MyWebcam_ImageCaptured(source As Object, e As WebcamEventArgs) Handles MyWebcam.ImageCaptured
        PictureBox1.Image = e.WebCamImage

    End Sub

    Private Sub StartWebcam()
        Try
            StopWebcam()
            MyWebcam = New WebCamCapture
            MyWebcam.Start(0)
            MyWebcam.Start(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StopWebcam()
        Try
            MyWebcam.Stop()
            MyWebcam.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Scan_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        StartWebcam()
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = Val(Label2.Text) + 1
        If Label2.Text = 4 Then
            Try
                StopWebcam()
                Timer1.Enabled = False
                Reader = New QRCodeDecoder
                MsgBox(Reader.decode(New Data.QRCodeBitmapImage(PictureBox1.Image)))
                Label2.Text = "0"
            Catch exx As Exception
                Label2.Text = "0"
                Timer1.Enabled = True
                StartWebcam()
            End Try
        End If
    End Sub
End Class