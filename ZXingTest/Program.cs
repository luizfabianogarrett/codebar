using System;
using System.Drawing;
using System.IO;
using ZXing;

namespace ZXingTest
{
    class Program
    {

        static void Main(string[] args)
        {
            
        }
        static void Main2(string[] args)
        {
            BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.EAN_13,
                
                
            };

            writer.Options.PureBarcode = true;

            var pixelData = writer.Write("7898357417892");

            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    // PNG or JPEG or whatever you want
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    var base64str = Convert.ToBase64String(ms.ToArray());

                    bitmap.Save("c:\\projetos\\test.png", System.Drawing.Imaging.ImageFormat.Png);


                }
            }
        }
        

    }
}
