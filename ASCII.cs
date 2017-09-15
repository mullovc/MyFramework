using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyFramework.GUI
{
    public class ASCII
    {
        static Image[]  ascii;
        static Bitmap[] charSet;
        static int[]    charSetGrayValues;
        static Font     font;
        static Font     textFont;

        const int firstPrintable = 32;
        const int setSize        = 127;

        public static void GenerateASCIICharSet(Font f)
        {
            font = f;

            int size = (int)font.Size;

            charSet = new Bitmap[setSize - firstPrintable];
            for (int i = 0; i < charSet.Length; i++)
            {
                charSet[i] = FontToBmp(font, (char) (i + firstPrintable));
                //charSet[i] = FontToBmp(font, (char) (i + firstPrintable), (int)font.Size, font.Height);
            }

            charSetGrayValues = new int[charSet.Length];
            for (int i = 0; i < charSet.Length; i++)
            {
                charSetGrayValues[i] = MeanGreyVal(charSet[i]);
            }
        }

        public static void SetTextFont(Font f)
        {
            textFont = f;
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
                ascii[i] = new Image(ImageToASCII(FontToBmp(textFont, c)));
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

        public static int[,] ApproxMeanGreyValWithCharSet(Bitmap image)
        {
            //assumes identical dimensions of all candidates
            int candWidth  = charSet[0].Width;
            int candHeight = charSet[0].Height;
            int hPixls = image.Width  / candWidth;
            int vPixls = image.Height / candHeight;
            int[,] res = new int[hPixls, vPixls];

            for (int i = 0; i < hPixls; i++)
            {
                for (int j = 0; j < vPixls; j++)
                {
                    int cutoutGreyVal = MeanGreyVal(image, i * candWidth, j * candHeight, candWidth, candHeight);
                    int max = 0;
                    int argmax = 0;
                    for (int k = 0; k < charSet.Length; k++)
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

        public static char[,] ImageToASCII(Bitmap image)
        {
            if (charSetGrayValues == null)
            {
                return null;
            }

            int[,] matches = ApproxMeanGreyValWithCharSet(image);
            char[,] art = new char[matches.GetLength(1), matches.GetLength(0)];
            for (int i = 0; i < matches.GetLength(0); i++)
            {
                for (int j = 0; j < matches.GetLength(1); j++)
                {
                    art[j, i] = (char)(matches[i, j] + 32);
                }
            }
            return art;
        }
    }
}
