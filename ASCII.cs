using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MyMath;
using System.IO;

namespace MyFramework.GUI
{
    public class ASCII
    {
        static Image[]  ascii;
        static int[]    charSetGrayValues;
        static Font     textFont;
        static float    fontResolution;

        const int firstPrintable = 32;
        const int setSize        = 127;

        public static void GenerateASCIICharSet(Font font, bool useFile = true)
        {
            int size = (int)font.Size;

            string fileName = "GUI\\ASCII\\" + font.Name + " " + font.Style.ToString();
            if (useFile && File.Exists(fileName))
            {
                BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open));
                byte[] gValBytes = br.ReadBytes(setSize - firstPrintable);
                charSetGrayValues = new int[setSize - firstPrintable];
                for (int i = 0; i < charSetGrayValues.Length; i++)
                {
                    charSetGrayValues[i] = (int)gValBytes[i];
                }
                return;
            }

            Bitmap[] charSet = new Bitmap[setSize - firstPrintable];
            for (int i = 0; i < charSet.Length; i++)
            {
                charSet[i] = FontToBmp(font, (char) (i + firstPrintable));
                //charSet[i] = FontToBmp(font, (char) (i + firstPrintable), (int)font.Size, font.Height);
            }

            charSetGrayValues = new int[charSet.Length];
            for (int i = 0; i < charSet.Length; i++)
            {
                charSetGrayValues[i] = (byte)MeanGreyVal(charSet[i]);
            }

            charSetGrayValues = Normalize(charSetGrayValues, 32, 255);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            if (useFile && fs.CanWrite)
            {
                BinaryWriter bw = new BinaryWriter(fs);
                byte[] gValBytes = new byte[charSetGrayValues.Length];
                for (int i = 0; i < charSetGrayValues.Length; i++)
                {
                    gValBytes[i] = (byte)charSetGrayValues[i];
                }
                bw.Write(gValBytes);
                bw.Close();
            }
            fs.Close();
        }
        
        static int[] Normalize(int[] x, int min, int max)
        {
            float minVal = x.Min();
            float maxVal = x.Max();
            int[] normalized = new int[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                normalized[i] = (int) ((max - min) / (maxVal - minVal) * (x[i] - minVal)) + min;
            }

            return normalized;
        }

        public static void SetTextFont(Font f, float res = 8)
        {
            fontResolution = res;
            textFont = f;
            ascii = null;
        }

        public static Image charToImage(char c)
        {
            if (ascii == null)
            {
                ascii = new Image[setSize - firstPrintable];
            }
            int i = (int)c - firstPrintable;

            if (ascii[i] == null)
            {
                if (textFont == null)
                {
                    SetTextFont(new Font(new FontFamily("Consolas"), 12), 16);
                }
                ascii[i] = new Image(ImageToASCII(FontToBmp(textFont, c), 1 / fontResolution));
            }

            return ascii[i];
        }
    
        //public static Bitmap FontToBmp(Font f, char c, int width, int height)
        public static Bitmap FontToBmp(Font f, char c)
        {
            //Bitmap bmp = new Bitmap(width, height);
            Bitmap bmp = new Bitmap((int)f.Size, f.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawString(c.ToString(), f, new SolidBrush(Color.Black), 0, 0);

            return bmp;
        }

        public static int MeanGreyVal(Bitmap bmp)
        {
            int mean = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pix = bmp.GetPixel(i, j);
                    mean += (pix.R + pix.G + pix.B) / 3;
                    mean += 255 - pix.A;
                }
            }
            return mean / (bmp.Width * bmp.Height);
        }

        public static int MeanGreyVal(Bitmap bmp, int x, int y, int width, int height)
        {
            int mean = 0;
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    Color pix = bmp.GetPixel(i, j);
                    mean += (pix.R + pix.G + pix.B) / 3;
                    mean += 255 - pix.A;
                }
            }
            return mean / (width * height);
        }

        public static int[,] ApproxMeanGreyVal(Bitmap image, int xRes, int yRes)
        {
            //int hPixls = image.Width  / kernelSize;
            //int vPixls = image.Height / kernelSize;
            int[,] res = new int[xRes, yRes];
            int kernelX = image.Width  / xRes;
            int kernelY = image.Height / yRes;

            for (int i = 0; i < xRes; i++)
            {
                for (int j = 0; j < yRes; j++)
                {
                    int cutoutGreyVal = MeanGreyVal(image, i * kernelX, j * kernelY, kernelX, kernelY);
                    int max = 0;
                    int argmax = 0;
                    for (int k = 0; k < charSetGrayValues.Length; k++)
                    {
                        int curr = 255 - Math.Abs(charSetGrayValues[k] - cutoutGreyVal);
                        if (curr > max)
                        {
                            max = curr;
                            argmax = k;
                        }
                    }
                    res[i, j] = argmax;
                }
            }
            return res;
        }

        public static char[,] ImageToASCII(Bitmap image, int resolutionX, int resolutionY)
        {
            if (charSetGrayValues == null)
            {
                return null;
            }

            int[,] matches = ApproxMeanGreyVal(image, resolutionX, resolutionY);
            char[,] art = new char[matches.GetLength(1), matches.GetLength(0)];
            for (int i = 0; i < matches.GetLength(0); i++)
            {
                for (int j = 0; j < matches.GetLength(1); j++)
                {
                    art[j, i] = (char)(matches[i, j] + firstPrintable);
                }
            }
            return art;
        }

        public static char[,] ImageToASCII(Bitmap image, float scale = 1)
        {
            return ImageToASCII(image, (int)(image.Width * scale), (int)(image.Height * scale));
        }

        public static Image FileToImage(string path, int resolutionX, int resolutionY)
        {
            Bitmap bmp = new Bitmap(path);
            return new Image(ImageToASCII(bmp, resolutionX, resolutionY));
        }

        public static Image FileToImage(string path, float scale = 1)
        {
            Bitmap bmp = new Bitmap(path);
            return new Image(ImageToASCII(bmp, (int)(bmp.Width * scale), (int)(bmp.Height * scale)));
        }
    }
}
