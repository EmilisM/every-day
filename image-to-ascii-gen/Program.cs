using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace image_to_ascii_gen
{
    public class Program
    {
        const float redWeight = 0.299f;
        const float greenWeight = 0.587f;
        const float blueWeight = 0.114f;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Insufficient params");
                return;
            }

            var path = args[0];
            var exists = File.Exists(path);

            if (!exists || !path.ToLower().Contains(".jpg"))
            {
                Console.WriteLine("File doesn't exist");
                return;
            }

            var imageBitmap = new Bitmap(path);

            var grayscaledBitmap = BitmapToGrayscale(imageBitmap);
            var values = GetPixelGrayscaleValues(grayscaledBitmap);

            var directory = Path.GetDirectoryName(path);

            SaveToFile(values, directory);
        }


        public static Bitmap Downscale(Bitmap source, float targetWidth, float targetHeight)
        {
            var tempBitmap = new Bitmap((int)targetWidth, (int)targetHeight);

            using var graphics = Graphics.FromImage(tempBitmap);

            float scale = Math.Min(targetWidth / source.Width, targetHeight / source.Height);

            var scaleWidth = source.Width * scale;
            var scaleHeight = source.Height * scale;

            graphics.DrawImage(source, (targetWidth - scaleWidth) / 2, (targetHeight - scaleHeight) / 2, scaleWidth, scaleHeight);

            return tempBitmap;
        }

        public static Bitmap BitmapToGrayscale(Bitmap source)
        {
            var width = source.Width;
            var height = source.Height;

            var grayscale = new Bitmap(height, height);

            using var graphics = Graphics.FromImage(grayscale);

            var colorMatrix = new ColorMatrix(
                new float[][]
                {
                    new float[] {redWeight, redWeight, redWeight, 0, 0},
                    new float[] {greenWeight, greenWeight, greenWeight, 0, 0},
                    new float[] {blueWeight, blueWeight, blueWeight, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            using var attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            graphics.DrawImage(source, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);

            return grayscale;
        }

        public static decimal[,] GetPixelGrayscaleValues(Bitmap source)
        {
            var width = source.Width;
            var height = source.Height;

            var sourceData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);

            var bytes = new byte[source.Height * sourceData.Stride];

            Marshal.Copy(sourceData.Scan0, bytes, 0, bytes.Length);

            var result = new decimal[width, height];

            for (var y = 0; y < height; ++y)
            {
                for (var x = 0; x < width; ++x)
                {
                    var offset = y * sourceData.Stride + x * 3;
                    var grayScaleByte = (bytes[offset + 0] + bytes[offset + 1] + bytes[offset + 2]) / 3m;
                    result[x, y] = Math.Round(grayScaleByte / 255m, 2);
                }
            }

            return result;
        }

        public static void SaveToFile(decimal[,] values, string path)
        {
            using var streamWriter = new StreamWriter($"{path}/result.txt");

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int x = 0; x < values.GetLength(1); x++)
                {
                    var value = values[i, x];
                    streamWriter.Write(value < 0.5m ? "@" : " ");
                }
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }
    }
}
