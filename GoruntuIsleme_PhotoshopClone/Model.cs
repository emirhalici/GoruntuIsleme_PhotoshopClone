using System;
using System.Collections;
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
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
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

        public static Bitmap RenkAyari(Bitmap giris, int rOlcek, int gOlcek, int bOlcek) // 0-100 arasi
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int r = (int)(rOlcek * pixel.R / 100);
                    int g = (int)(gOlcek * pixel.G / 100);
                    int b = (int)(bOlcek * pixel.B / 100);

                    cikis.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return cikis;
        }

        public static Bitmap Esikleme(Bitmap giris, int deger)
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int r = pixel.R >= deger ? pixel.R : 0;
                    int g = pixel.G >= deger ? pixel.G : 0;
                    int b = pixel.B >= deger ? pixel.B : 0;
                    cikis.SetPixel(x, y, Color.FromArgb(r, g, b));
                }

            }
            return cikis;
        }

        public static Bitmap Esikleme(Bitmap giris, int altDeger, int ustDeger)
        {
            Bitmap cikis = NewBitmap(giris);
            //Bitmap gri = Gri(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int r = pixel.R;
                    if (r <= altDeger || r >= ustDeger) r = 0;
                    int g = pixel.G;
                    if (g <= altDeger || g >= ustDeger) g = 0;
                    int b = pixel.G;
                    if (b <= altDeger || b >= ustDeger) b = 0;
                    cikis.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return cikis;
        }

        public static Bitmap Histogram(Bitmap giris)
        {
            ArrayList DiziPiksel = new ArrayList();
            int OrtalamaRenk = 0;
            Color OkunanRenk;
            int R = 0, G = 0, B = 0;
            Bitmap GirisResmi;
            GirisResmi = Gri(giris);
            Bitmap CikisResmi = new Bitmap(GirisResmi.Width, GirisResmi.Height);
            for (int x = 0; x < GirisResmi.Width; x++)
            {
                for (int y = 0; y < GirisResmi.Height; y++)
                {
                    Color pixel = GirisResmi.GetPixel(x, y);
                    OrtalamaRenk = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    DiziPiksel.Add(OrtalamaRenk);
                }
            }
            int[] DiziPikselSayilari = new int[256];
            for (int r = 0; r <= 255; r++)
            {
                int PikselSayisi = 0;
                for (int s = 0; s < DiziPiksel.Count; s++)
                {
                    if (r == Convert.ToInt16(DiziPiksel[s]))
                        PikselSayisi++;
                }
                DiziPikselSayilari[r] = PikselSayisi;
            }
            int RenkMaksPikselSayisi = 0;
            for (int k = 0; k <= 255; k++)
            {
                if (DiziPikselSayilari[k] > RenkMaksPikselSayisi)
                {
                    RenkMaksPikselSayisi = DiziPikselSayilari[k];
                }
            }
            Graphics CizimAlani = Graphics.FromImage(CikisResmi);
            CizimAlani.Clear(Color.Black);
            Pen Kalem1 = new Pen(System.Drawing.Color.Yellow, 1);
            Pen Kalem2 = new Pen(System.Drawing.Color.Red, 1);
            int GrafikYuksekligi = 300;
            double OlcekY = RenkMaksPikselSayisi / GrafikYuksekligi;
            double OlcekX = 1.5;
            int X_kaydirma = 10;
            for (int x = 0; x <= 255; x++)
            {
                if (x % 50 == 0) CizimAlani.DrawLine(Kalem2, (int)(X_kaydirma + x * OlcekX), GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), 0);
                CizimAlani.DrawLine(Kalem1, (int)(X_kaydirma + x * OlcekX), GrafikYuksekligi, (int)(X_kaydirma + x * OlcekX), (GrafikYuksekligi - (int)(DiziPikselSayilari[x] / OlcekY)));
            }
            return CikisResmi;
        }

        public static Bitmap BulaniklastirmaMean(Bitmap GirisResmi, int SablonBoyutu) // 3, 5, 7 
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R;
                            toplamG = toplamG + OkunanRenk.G;
                            toplamB = toplamB + OkunanRenk.B;
                        }
                    }
                    ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                    ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                    ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            return CikisResmi;
        }

        public static Bitmap BulaniklastirmaMedian(Bitmap GirisResmi, int SablonBoyutu) // 3, 5, 7
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int[] R = new int[ElemanSayisi];
            int[] G = new int[ElemanSayisi];
            int[] B = new int[ElemanSayisi];
            int[] Gri = new int[ElemanSayisi];
            int x, y, i, j;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R[k] = OkunanRenk.R;
                            G[k] = OkunanRenk.G;
                            B[k] = OkunanRenk.B;
                            Gri[k] = Convert.ToInt16(R[k] * 0.299 + G[k] * 0.587 + B[k] * 0.114);
                            k++;
                        }
                    }
                    int GeciciSayi = 0;
                    for (i = 0; i < ElemanSayisi; i++)
                    {
                        for (j = i + 1; j < ElemanSayisi; j++)
                        {
                            if (Gri[j] < Gri[i])
                            {
                                GeciciSayi = Gri[i];
                                Gri[i] = Gri[j];
                                Gri[j] = GeciciSayi;
                                GeciciSayi = R[i];
                                R[i] = R[j];
                                R[j] = GeciciSayi;
                                GeciciSayi = G[i];
                                G[i] = G[j];
                                G[j] = GeciciSayi;
                                GeciciSayi = B[i];
                                B[i] = B[j];
                                B[j] = GeciciSayi;
                            }
                        }
                    }
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R[(ElemanSayisi - 1) / 2], G[(ElemanSayisi - 1) /
                   2], B[(ElemanSayisi - 1) / 2]));
                }
            }
            return CikisResmi;
        }


        public static Bitmap BulaniklastirmaGaussian(Bitmap GirisResmi, int SablonBoyutu)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            SablonBoyutu = 5;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            int[] Matris = { 1, 4, 7, 4, 1, 4, 20, 33, 20, 4, 7, 33, 55, 33, 7, 4, 20, 33, 20, 4, 1, 4, 7, 4, 1 };
            int MatrisToplami = 1 + 4 + 7 + 4 + 1 + 4 + 20 + 33 + 20 + 4 + 7 + 33 + 55 + 33 + 7 + 4 + 20 + 33 + 20 + 4 + 1 + 4 + 7 + 4 + 1;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    ortalamaR = toplamR / MatrisToplami;
                    ortalamaG = toplamG / MatrisToplami;
                    ortalamaB = toplamB / MatrisToplami;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            return CikisResmi;
        }

    }
}
