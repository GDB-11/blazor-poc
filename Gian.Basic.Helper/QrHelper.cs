using SkiaSharp.QrCode;
using SkiaSharp;

namespace Gian.Basic.Helper;

public static class QrHelper
{
    public static MemoryStream CreateQrCode(string content)
    {
        using QRCodeGenerator generator = new();

        QRCodeData qr = generator.CreateQrCode(content, ECCLevel.L);

        SKImageInfo info = new(512, 512);
        using SKSurface surface = SKSurface.Create(info);
        SKCanvas canvas = surface.Canvas;
        canvas.Render(qr, info.Width, info.Height);

        using SKImage image = surface.Snapshot();
        using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
        MemoryStream memoryStream = new();
        data.SaveTo(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
