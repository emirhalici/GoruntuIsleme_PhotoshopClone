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

    }
}
