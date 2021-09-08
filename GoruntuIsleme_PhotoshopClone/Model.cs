using System.Drawing;
namespace GoruntuIsleme_PhotoshopClone
{
    public static class Model
    {
        public static Bitmap NewBitmap(Bitmap giris)
        {
            return new Bitmap(giris.Width, giris.Height);
        }

        public static Bitmap Parlaklik(Bitmap giris, int fark)
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int R = pixel.R + fark;
                    if (R > 255) R = 255;
                    if (R < 0) R = 0;
                    int G = pixel.G + fark;
                    if (G > 255) G = 255;
                    if (G < 0) G = 0;
                    int B = pixel.B + fark;
                    if (B > 255) B = 255;
                    if (B < 0) B = 0;
                    cikis.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return cikis;
        }

        public static Bitmap Negatif(Bitmap giris)
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int R = 255 - pixel.R;
                    int G = 255 - pixel.G;
                    int B = 255 - pixel.B;
                    if (R > 255) R = 255;
                    if (R < 0) R = 0;
                    if (G > 255) G = 255;
                    if (G < 0) G = 0;
                    if (B > 255) B = 255;
                    if (B < 0) B = 0;
                    cikis.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return cikis;
        }

        public static Bitmap Kontrast(Bitmap giris, int fark)
        {
            Bitmap cikis = NewBitmap(giris);
            double kontrastFaktoru = (259 * (fark + 255)) / (255 * (259 - fark));
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int R = (int)((kontrastFaktoru * (pixel.R - 128)) + 128);
                    int G = (int)((kontrastFaktoru * (pixel.G - 128)) + 128);
                    int B = (int)((kontrastFaktoru * (pixel.B - 128)) + 128);

                    cikis.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return cikis;
        }

        public static Bitmap Gri(Bitmap giris)
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int Gri = (int)(pixel.R * 0.3 + pixel.G * 0.6 + pixel.B * 0.1);
                    cikis.SetPixel(x, y, Color.FromArgb(Gri, Gri, Gri));
                }
            }
            return cikis;
        }

        public static Bitmap RenkAyari(Bitmap giris, int R, int G, int B) // 0-100 arasi
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int r = (int)(R * pixel.R / 100);
                    int g = (int)(G * pixel.G / 100);
                    int b = (int)(B * pixel.B / 100);

                    cikis.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return cikis;
        }

    }
}
