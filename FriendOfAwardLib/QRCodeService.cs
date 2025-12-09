using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

public class QRCodeService
{
 

    public byte[] GenerateQRCodeImage(string url)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);

        using Bitmap bitmap = qrCode.GetGraphic(20);
        using MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}
