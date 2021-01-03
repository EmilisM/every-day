using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace image_to_ascii_gen
{
    public class Program
    {
        const float redWeight = 0.299f;
        const float greenWeight = 0.587f;
        const float blueWeight = 0.114f;

        static readonly string[] supportedExtensions = { ".jpg", ".jpeg", ".bmp", ".png" };

        static readonly CharacterModel[] characterThresholds = {
            new CharacterModel('@', 0.8m, 1m),
            new CharacterModel('#', 0.6m, 0.8m),
            new CharacterModel('+', 0.4m, 0.6m),
            new CharacterModel('*', 0.2m, 0.4m),
        };

        static readonly CharacterModel[] characterThresholdsInverted = {
            new CharacterModel('*', 0.8m, 1m),
            new CharacterModel('+', 0.6m, 0.8m),
            new CharacterModel('#', 0.4m, 0.6m),
            new CharacterModel('@', 0.2m, 0.4m),
        };

        public static void Main(string[] args)
        {
            CheckCondition(args.Length == 0, "Insufficient params");

            var path = args[0];
            var isInverted = args.Length == 2 && bool.TryParse(args[1], out var result) ? result : false;

            var exists = File.Exists(path);

            CheckCondition(!exists, "File doesn't exist");

            var extension = Path.GetExtension(path);
            var isExtensionSupported = supportedExtensions.Any(e => e == extension);

            CheckCondition(!isExtensionSupported, "File extension is not supported");

            var source = new Bitmap(path);

            var width = source.Width;
            var height = source.Height;

            var grayscaledBitmap = GrayscaleBitmap(source, width, height);
            var values = GetPixelValues(grayscaledBitmap, width, height);

            var directory = Path.GetDirectoryName(path);

            SaveToFile(values, directory, isInverted);
        }

        public static Bitmap GrayscaleBitmap(Bitmap source, int width, int height)
        {
            var grayscale = new Bitmap(width, height);

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

        public static decimal[,] GetPixelValues(Bitmap source, int width, int height)
        {
            var sourceData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            var bytes = new byte[height * sourceData.Stride];

            Marshal.Copy(sourceData.Scan0, bytes, 0, bytes.Length);

            var result = new decimal[height, width];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var offset = y * sourceData.Stride + x * 3;
                    var grayScaleByte = (bytes[offset + 0] + bytes[offset + 1] + bytes[offset + 2]) / 3m;
                    result[y, x] = Math.Round(grayScaleByte / 255m, 2);
                }
            }

            return result;
        }

        public static void SaveToFile(decimal[,] values, string path, bool isInverted)
        {
            using var streamWriter = new StreamWriter($"{path}/result.txt");

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int x = 0; x < values.GetLength(1); x++)
                {
                    var value = values[i, x];
                    streamWriter.Write(GetCharacter(value, isInverted));
                }
                streamWriter.WriteLine();
            }

            streamWriter.Close();
        }

        public static char GetCharacter(decimal grayscaleValue, bool isInverted)
        {
            var thresholdSettings = isInverted ? characterThresholdsInverted : characterThresholds;

            var model = thresholdSettings.FirstOrDefault(ct => grayscaleValue > ct.LowLimit && grayscaleValue <= ct.HighLimit);

            return model != null ? model.Character : ' ';
        }

        public static void CheckCondition(bool condition, string message)
        {
            if (condition)
            {
                Console.WriteLine(message);
                Environment.Exit(0);
            }
        }
    }

    public class CharacterModel
    {
        public char Character { get; set; }
        public decimal LowLimit { get; set; }
        public decimal HighLimit { get; set; }

        public CharacterModel(char character, decimal lowLimit, decimal highLimit)
        {
            Character = character;
            LowLimit = lowLimit;
            HighLimit = highLimit;
        }
    }
}
