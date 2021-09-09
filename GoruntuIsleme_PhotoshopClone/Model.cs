using System;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace GoruntuIsleme_PhotoshopClone
{
    public static class Model
    {
        public static Bitmap NewBitmap(Bitmap giris)
        {
            return new Bitmap(giris.Width, giris.Height);
        }

        public static Bitmap DosyaAc()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                String ResminYolu = openFileDialog1.FileName;
                Bitmap Resim = (Bitmap) Image.FromFile(ResminYolu);
                return Resim;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Dosya acilamadi.");
                return null;
            }
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

        public static Bitmap EsiklemeSiyahBeyaz(Bitmap giris, int deger)
        {
            Bitmap cikis = NewBitmap(giris);
            for (int x = 0; x < cikis.Width; x++)
            {
                for (int y = 0; y < cikis.Height; y++)
                {
                    Color pixel = giris.GetPixel(x, y);
                    int r = pixel.R > deger ? 255 : 0;
                    int g = pixel.G > deger ? 255 : 0;
                    int b = pixel.B > deger ? 255 : 0;
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

        public static Bitmap KenarGoruntusuCikar(Bitmap GirisResmi, Bitmap BulanikResim, double Olcekleme)
        {
            Color OkunanRenk1, OkunanRenk2, DonusenRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int R, G, B;
            //double Olcekleme = 2; //Keskin kenaları daha iyi görmek için değerini artırıyoruz.
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk1 = GirisResmi.GetPixel(x, y);
                    OkunanRenk2 = BulanikResim.GetPixel(x, y);
                    R = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.R - OkunanRenk2.R));
                    G = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.G - OkunanRenk2.G));
                    B = Convert.ToInt16(Olcekleme * Math.Abs(OkunanRenk1.B - OkunanRenk2.B));
                    //===============================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak. (Dikkat: Normalizasyon yapılmamıştır. )
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //================================================================
                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return CikisResmi;
        }
        public static Bitmap KenarGoruntusunuBulanikResimleBirlestir(Bitmap GirisResmi, Bitmap KenarGoruntusu)
        {
            Color OkunanRenk1, OkunanRenk2, DonusenRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int R, G, B;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    OkunanRenk1 = GirisResmi.GetPixel(x, y);
                    OkunanRenk2 = KenarGoruntusu.GetPixel(x, y);
                    R = OkunanRenk1.R + OkunanRenk2.R;
                    G = OkunanRenk1.G + OkunanRenk2.G;
                    B = OkunanRenk1.B + OkunanRenk2.B;
                    //===============================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak. //DİKKAT: Burada sınırı aşan değerler NORMALİZASYON yaparak programlanmalıdır.
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //================================================================
                    DonusenRenk = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, DonusenRenk);
                }
            }
            return CikisResmi;
        }

        public static Bitmap KenarGoruntusuKullanarakNetlestirme(Bitmap GirisResmi, Bitmap BulanikResim, double KenarOlcegi)
        {
            Bitmap KenarGoruntusu = KenarGoruntusuCikar(GirisResmi, BulanikResim, KenarOlcegi);
            Bitmap CikisResmi = KenarGoruntusunuBulanikResimleBirlestir(GirisResmi, KenarGoruntusu);
            return CikisResmi;
        }

        public static Bitmap KonvulasyonIleNetlestirme(Bitmap GirisResmi)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB;
            int R, G, B;
            int[] Matris = { 0, -2, 0, -2, 11, -2, 0, -2, 0 };
            int MatrisToplami = 0 + -2 + 0 + -2 + 11 + -2 + 0 + -2 + 0;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    //Şablon bölgesi (çekirdek matris) içindeki pikselleri tarıyor.
                    int k = 0; //matris içindeki elemanları sırayla okurken kullanılacak.
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
                    R = toplamR / MatrisToplami;
                    G = toplamG / MatrisToplami;
                    B = toplamB / MatrisToplami;
                    //===========================================================
                    //Renkler sınırların dışına çıktıysa, sınır değer alınacak.
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    //===========================================================
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        public static Bitmap KenarBulmaSobel(Bitmap giris, int esikDegeri) 
        {
            Bitmap CikisResmiXY = NewBitmap(giris);
            int SablonBoyutu = 3;
            Color Renk;
            int P1, P2, P3, P4, P5, P6, P7, P8, P9;
            for (int x = (SablonBoyutu - 1) / 2; x < giris.Width - (SablonBoyutu - 1) / 2; x++)
            {
                for (int y = (SablonBoyutu - 1) / 2; y < giris.Height - (SablonBoyutu - 1) / 2; y++)
                {
                    Renk = giris.GetPixel(x - 1, y - 1);
                    P1 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x, y - 1);
                    P2 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x + 1, y - 1);
                    P3 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x - 1, y);
                    P4 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x, y);
                    P5 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x + 1, y);
                    P6 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x - 1, y + 1);
                    P7 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x, y + 1);
                    P8 = (Renk.R + Renk.G + Renk.B) / 3;
                    Renk = giris.GetPixel(x + 1, y + 1);
                    P9 = (Renk.R + Renk.G + Renk.B) / 3;
                    int Gx = Math.Abs(-P1 + P3 - 2 * P4 + 2 * P6 - P7 + P9);
                    int Gy = Math.Abs(P1 + 2 * P2 + P3 - P7 - 2 * P8 - P9);
                    int Gxy = Gx + Gy;

                    if (Gx > 255) Gx = 255;
                    if (Gy > 255) Gy = 255;
                    if (Gxy > 255) Gxy = 255;

                    if (Gx < 0) Gx = 0;
                    if (Gy < 0) Gy = 0;
                    if (Gxy < 0) Gxy = 0;

                    if (Gx < esikDegeri) Gx = 0;
                    if (Gy < esikDegeri) Gy = 0;
                    if (Gxy < esikDegeri) Gxy = 0;
                    CikisResmiXY.SetPixel(x, y, Color.FromArgb(Gxy, Gxy, Gxy));
                }
            }
            return CikisResmiXY;
        }

        public static Bitmap KenarBulmaCompass(Bitmap giris, int esikDegeri)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = giris.Width;
            int ResimYuksekligi = giris.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 3;
            int[,] Matris = {
                { -1, -1, -1, 1, -2, 1, 1, 1, 1 },
                { -1, -1, 1, -1, -2, 1, 1, 1, 1 },
                {-1, 1, 1, -1, -2, 1, -1, 1, 1 },
                { 1, 1, 1, -1, -2, 1, -1, -1, 1 },
                { 1, 1, 1, 1, -2, 1, -1, -1,-1 },
                { 1, 1, 1, 1, -2, -1, 1, -1, -1 },
                { 1, 1, -1, 1, -2, -1, 1, 1, -1 },
                { 1, -1, -1, 1, -2,-1, 1, 1, 1 }
            }; // Compass Matrisi
            int[] gToplam = { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (int y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    int[] gDegerleri = { 0, 0, 0, 0, 0, 0, 0, 0 };
                    for (int t = 0; t < 8; t++)
                    {
                        int k = 0;
                        int G = 0;
                        for (int i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                        {
                            for (int j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                            {
                                Color c = giris.GetPixel(x + i, y + j);
                                int grayscale = Convert.ToInt16(c.R * 0.3 + c.G * 0.6 + c.B * 0.1);
                                G += grayscale * Matris[t, k];
                                k++;
                            }
                        }
                        gDegerleri[t] = G;
                    }
                    int g = gDegerleri.Max();
                    if (g > 255) g = 255;
                    if (g < esikDegeri) g = 0;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            return CikisResmi;
        }

        public static Tuple<Bitmap, Bitmap> ResimBoyutlariniEsitle(Bitmap AnaResim, Bitmap DigerResim) // DigerResim AnaResim'den buyuk olmali.
        {
            int h1 = AnaResim.Height; int h2 = DigerResim.Height;
            int w1 = AnaResim.Width; int w2 = DigerResim.Width;

            int x = w1 > w2 ? w2 : w1;
            int y = h1 > h2 ? h2 : h1;
            Bitmap CikisResmi = new Bitmap(x, y);
            Bitmap CikisResmi2 = new Bitmap(x, y);
            for (int h = 0; h < y; h++)
            {
                for (int w = 0; w < x; w++)
                {
                    try
                    {
                        CikisResmi.SetPixel(w, h, DigerResim.GetPixel(w, h));
                        CikisResmi2.SetPixel(w, h, AnaResim.GetPixel(w, h));
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Hata olustu.");
                        Tuple<Bitmap, Bitmap> retu = Tuple.Create(CikisResmi, CikisResmi2);
                        return retu;
                    }

                }
            }
            Tuple<Bitmap, Bitmap> tuple = Tuple.Create(CikisResmi, CikisResmi2);
            return tuple;
        }

        public static Bitmap ResimdenArkaPlaniCikarOlcekliEsikli(Bitmap AnaResim, Bitmap Arkaplan, int Olcek, int Esikleme) // goruntuler esit olmali veya AnaResim daha kucuk olmali
        {
            int ResimGenisligi = AnaResim.Width;
            int ResimYuksekligi = AnaResim.Height;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            for (int x = 0; x < AnaResim.Width; x++)
            {
                for (int y = 0; y < AnaResim.Height; y++)
                {
                    Color Pixel1 = AnaResim.GetPixel(x, y);
                    Color Pixel2 = Arkaplan.GetPixel(x, y);
                    int R = Math.Abs(Pixel1.R - Pixel2.R);
                    if (R < Esikleme) R = 0;
                    R = Olcek * R;
                    if (R > 255) R = 255;
                    Color FinalPixel = Color.FromArgb(R, R, R);
                    CikisResmi.SetPixel(x, y, FinalPixel);
                }
            }
            return CikisResmi;
        }

        public static Bitmap ResimdenArkaPlaniCikarOlcekliEsikliRenkli(Bitmap AnaResim, Bitmap Arkaplan, int Olcek, int Esikleme) // goruntuler esit olmali veya AnaResim daha kucuk olmali
        {
            int ResimGenisligi = AnaResim.Width;
            int ResimYuksekligi = AnaResim.Height;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            for (int x = 0; x < AnaResim.Width; x++)
            {
                for (int y = 0; y < AnaResim.Height; y++)
                {
                    Color Pixel1 = AnaResim.GetPixel(x, y);
                    Color Pixel2 = Arkaplan.GetPixel(x, y);
                    int R = Math.Abs(Pixel1.R - Pixel2.R);
                    if (R < Esikleme) R = 0;
                    R = Olcek * R;
                    if (R > 255) R = 255;
                    int G = Math.Abs(Pixel1.G - Pixel2.G);
                    if (G < Esikleme) G = 0;
                    G = Olcek * G;
                    if (G > 255) G = 255;
                    int B = Math.Abs(Pixel1.B - Pixel2.B);
                    if (B < Esikleme) B = 0;
                    B = Olcek * B;
                    if (B > 255) B = 255;
                    Color FinalPixel = Color.FromArgb(R, G, B);
                    CikisResmi.SetPixel(x, y, FinalPixel);
                }
            }
            return CikisResmi;
        }

        public static Bitmap BolgeleriAyir(Bitmap GirisResmi, int Esikleme)
        {
            Bitmap CikisResmi;
            int KomsularinEnKucukEtiketDegeri = 0;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int PikselSayisi = ResimGenisligi * ResimYuksekligi;
            int x, y, i, j, EtiketNo = 0;
            int[,] EtiketNumarasi = new int[ResimGenisligi, ResimYuksekligi];
            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    EtiketNumarasi[x, y] = 0;
                }
            }
            int IlkDeger = 0, SonDeger = 0;
            bool DegisimVar = false; 
            do
            {
                DegisimVar = false;
                for (y = 1; y < ResimYuksekligi - 1; y++) 
                {
                    for (x = 1; x < ResimGenisligi - 1; x++)
                    {
                        if (GirisResmi.GetPixel(x, y).R > Esikleme)
                        {
                            IlkDeger = EtiketNumarasi[x, y];
                            KomsularinEnKucukEtiketDegeri = 0;
                            for (j = -1; j <= 1; j++) 
                            {
                                for (i = -1; i <= 1; i++)
                                {
                                    if (EtiketNumarasi[x + i, y + j] != 0 &&
                                   KomsularinEnKucukEtiketDegeri == 0)
                                    {
                                        KomsularinEnKucukEtiketDegeri = EtiketNumarasi[x + i, y + j];
                                    }
                                    else if (EtiketNumarasi[x + i, y + j] <
                                    KomsularinEnKucukEtiketDegeri && EtiketNumarasi[x + i, y + j] != 0 &&
                                    KomsularinEnKucukEtiketDegeri != 0) 
                                    {
                                        KomsularinEnKucukEtiketDegeri = EtiketNumarasi[x + i, y + j];
                                    }
                                }
                            }
                            if (KomsularinEnKucukEtiketDegeri != 0) 
                            {
                                EtiketNumarasi[x, y] = KomsularinEnKucukEtiketDegeri;
                            }
                            else if (KomsularinEnKucukEtiketDegeri == 0) 
                            {
                                EtiketNo = EtiketNo + 1;
                                EtiketNumarasi[x, y] = EtiketNo;
                            }
                            SonDeger = EtiketNumarasi[x, y]; 
                            if (IlkDeger != SonDeger)
                                DegisimVar = true;
                        }
                    }
                }
            } while (DegisimVar == true); 
            int[] DiziEtiket = new int[PikselSayisi];
            i = 0;
            for (x = 1; x < ResimGenisligi - 1; x++)
            {
                for (y = 1; y < ResimYuksekligi - 1; y++)
                {
                    i++;
                    DiziEtiket[i] = EtiketNumarasi[x, y];
                }
            }
            Array.Sort(DiziEtiket);
            int[] TekrarsizEtiketNumaralari = DiziEtiket.Distinct().ToArray();
            int[] RenkDizisi = new int[TekrarsizEtiketNumaralari.Length];

            for (j = 0; j < TekrarsizEtiketNumaralari.Length; j++)
            {
                RenkDizisi[j] = TekrarsizEtiketNumaralari[j];
            }

            int RenkSayisi = RenkDizisi.Length;
            Color[] Renkler = new Color[RenkSayisi];
            Random Rastgele = new Random();
            int Kirmizi, Yesil, Mavi;
            for (int r = 0; r < RenkSayisi; r++)
            {
                Kirmizi = Rastgele.Next(5, 25) * 10; 
                Yesil = Rastgele.Next(5, 25) * 10;
                Mavi = Rastgele.Next(5, 25) * 10;
                Renkler[r] = Color.FromArgb(Kirmizi, Yesil, Mavi);
            }
            for (x = 1; x < ResimGenisligi - 1; x++) 
            {
                for (y = 1; y < ResimYuksekligi - 1; y++)
                {
                    int RenkSiraNo = Array.IndexOf(RenkDizisi, EtiketNumarasi[x, y]); 
                    if (GirisResmi.GetPixel(x, y).R < Esikleme) 
                    {
                        CikisResmi.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        CikisResmi.SetPixel(x, y, Renkler[RenkSiraNo]);
                    }
                }
            }
            return CikisResmi;
        }

        public static Bitmap Asindirma(Bitmap GirisResmi) // TODO
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Graphics.FromImage(CikisResmi).Clear(Color.Black);
            int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) 
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    bool RenkSiyah = false;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            if (OkunanRenk.R < 128) //Siyah ise
                                RenkSiyah = true;
                        }
                    }
                    if (RenkSiyah == true) //Komşularda siyah varsa
                    {
                        Color KendiRengi = GirisResmi.GetPixel(x, y);
                        if (KendiRengi.R > 128) //kendi rengin beyaz ise onu da siyah yap.
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255)); 
                    }
                    else //komşularda siyah yok ise rengi kirmizi
                        CikisResmi.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                }
            }
            return CikisResmi;
        }

        public static Bitmap Genisletme(Bitmap GirisResmi)
        {
            Color OkunanRenk;
            Bitmap CikisResmi;
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Graphics.FromImage(CikisResmi).Clear(Color.Black);
            int x, y, i, j;
            int SablonBoyutu = 3;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++) //Resmi taramaya şablonun yarısı kadar dış kenarlardan içeride başlayacak ve bitirecek.
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    bool RenkSiyah = false;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            if (OkunanRenk.R < 128) //Siyah ise
                                RenkSiyah = true;
                        }
                    }
                    if (RenkSiyah == true) //Komşularda siyah varsa
                    {
                        Color KendiRengi = GirisResmi.GetPixel(x, y);
                        if (KendiRengi.R > 128) //kendi rengin beyaz ise onu da siyah yap.
                            CikisResmi.SetPixel(x, y, Color.FromArgb(255, 0, 0)); //siyah yerine kırmızı kullandık.Genişleyen bölgeyi görmek için
                    }
                    else //komşularda siyah yok ise kendi rengi yine aynı beyaz kalmalı.
                        CikisResmi.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                }
            }
            return CikisResmi;
        }

        public static Bitmap ToplamaSiniraCekerek(Bitmap giris1, Bitmap giris2, int olcek1, int olcek2)
        {
            Color OkunanRenk1, OkunanRenk2;
            Bitmap CikisResmi;
            CikisResmi = new Bitmap(giris1.Width, giris1.Height);
            int R, G, B;
            for (int x = 0; x < giris1.Width; x++)
            {
                for (int y = 0; y < giris1.Height; y++)
                {
                    OkunanRenk1 = giris1.GetPixel(x, y);
                    OkunanRenk2 = giris2.GetPixel(x, y);
                    R = (OkunanRenk1.R * olcek1 / 100) + (OkunanRenk2.R * olcek2 / 100);
                    G = (OkunanRenk1.G * olcek1 / 100) + (OkunanRenk2.G * olcek2 / 100);
                    B = (OkunanRenk1.B * olcek1 / 100) + (OkunanRenk2.B * olcek2 / 100);
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        public static Bitmap ToplamaNormalizasyon(Bitmap giris1, Bitmap giris2, int olcek1, int olcek2)
        {
            Color OkunanRenk1, OkunanRenk2;
            Bitmap CikisResmi;
            CikisResmi = new Bitmap(giris1.Width, giris1.Height);
            int R, G, B;
            int EnYuksekDeger = 0, EnDusukDeger = 0;
            for (int x = 0; x < giris1.Width; x++)
            {
                for (int y = 0; y < giris1.Height; y++)
                {
                    OkunanRenk1 = giris1.GetPixel(x, y);
                    OkunanRenk2 = giris2.GetPixel(x, y);
                    R = (OkunanRenk1.R * olcek1 / 100) + (OkunanRenk2.R * olcek2 / 100);
                    G = (OkunanRenk1.G * olcek1 / 100) + (OkunanRenk2.G * olcek2 / 100);
                    B = (OkunanRenk1.B * olcek1 / 100) + (OkunanRenk2.B * olcek2 / 100);
                    int Gri = (R + G + B) / 3;
                    if (Gri > EnYuksekDeger) EnYuksekDeger = Gri;
                    if (Gri < EnDusukDeger) EnDusukDeger = Gri;
                }
            }

            int AltSinir = 0; if (EnDusukDeger > AltSinir) AltSinir = EnDusukDeger;
            int UstSinir = 255; if (EnYuksekDeger < UstSinir) UstSinir = EnYuksekDeger;

            for(int x = 0; x < giris1.Width; x++)
            {
                for (int y = 0; y < giris1.Height; y++)
                {
                    Color Renk1 = giris1.GetPixel(x, y);
                    Color Renk2 = giris2.GetPixel(x, y);
                    R = (Renk1.R * olcek1 / 100) + (Renk2.R * olcek2 / 100);
                    G = (Renk1.G * olcek1 / 100) + (Renk2.G * olcek2 / 100);
                    B = (Renk1.B * olcek1 / 100) + (Renk2.B * olcek2 / 100);
                    int NormalDegerR = (((UstSinir - AltSinir) * (R - EnDusukDeger)) / (EnYuksekDeger - EnDusukDeger)) + AltSinir;
                    int NormalDegerG = (((UstSinir - AltSinir) * (G - EnDusukDeger)) / (EnYuksekDeger - EnDusukDeger)) + AltSinir;
                    int NormalDegerB = (((UstSinir - AltSinir) * (B - EnDusukDeger)) / (EnYuksekDeger - EnDusukDeger)) + AltSinir;
                    if (NormalDegerR > 255) NormalDegerR = 255;
                    if (NormalDegerG > 255) NormalDegerG = 255;
                    if (NormalDegerB > 255) NormalDegerB = 255;
                    if (NormalDegerR < 0) NormalDegerR = 0;
                    if (NormalDegerG < 0) NormalDegerG = 0;
                    if (NormalDegerB < 0) NormalDegerB = 0;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(NormalDegerR, NormalDegerG, NormalDegerB));
                }
            }
            return CikisResmi;
        }

        public static Bitmap MantiksalOperatorGri(Bitmap giris1, Bitmap giris2, String mantiksalOperator)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = giris1.Width;
            int ResimYuksekligi = giris2.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1, Renk2;
            int x, y;
            for (x = 0; x < ResimGenisligi; x++)
 {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = giris1.GetPixel(x, y);
                    Renk2 = giris2.GetPixel(x, y);
                    string binarySayi1 = Convert.ToString(Renk1.R, 2).PadLeft(8, '0');
                    string binarySayi2 = Convert.ToString(Renk2.R, 2).PadLeft(8, '0');
                    string Bit1 = null, Bit2 = null, StringIkiliSayi = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1 = binarySayi1.Substring(i, 1);
                        Bit2 = binarySayi2.Substring(i, 1);
                        switch (mantiksalOperator)
                        {
                            case "AND":
                                if (Bit1 == "1" && Bit2 == "1") StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            case "NAND":
                                if (Bit1 == "1" && Bit2 == "1") StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "OR":
                                if (Bit1 == "1" || Bit2 == "1") StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            case "NOR":
                                if (Bit1 == "1" || Bit2 == "1") StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "XOR":
                                if (Bit1 == Bit2) StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "XNOR":
                                if (Bit1 == Bit2) StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            default:
                                return null;
                        }
                    }
                    int R = Convert.ToInt32(StringIkiliSayi, 2);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, R, R));
                }
            }
            return CikisResmi;
        }

        public static Bitmap MantiksalOperator(Bitmap giris1, Bitmap giris2, string mantiksalOperator)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = giris1.Width;
            int ResimYuksekligi = giris2.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1, Renk2;
            int x, y;
            int R = 0, G = 0, B = 0;
            for (x = 0; x < ResimGenisligi; x++) 
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = giris1.GetPixel(x, y);
                    Renk2 = giris2.GetPixel(x, y);
                    string binarySayi1R = Convert.ToString(Renk1.R, 2).PadLeft(8, '0');
                    string binarySayi2R = Convert.ToString(Renk2.R, 2).PadLeft(8, '0');
                    string binarySayi1G = Convert.ToString(Renk1.G, 2).PadLeft(8, '0');
                    string binarySayi2G = Convert.ToString(Renk2.G, 2).PadLeft(8, '0');
                    string binarySayi1B = Convert.ToString(Renk1.B, 2).PadLeft(8, '0');
                    string binarySayi2B = Convert.ToString(Renk2.B, 2).PadLeft(8, '0');
                    string Bit1R = null, Bit1G = null, Bit1B = null, Bit2R = null, Bit2G = null, Bit2B = null;
                    string StringIkiliSayiR = null, StringIkiliSayiG = null, StringIkiliSayiB = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1R = binarySayi1R.Substring(i, 1);
                        Bit2R = binarySayi2R.Substring(i, 1);
                        Bit1G = binarySayi1G.Substring(i, 1);
                        Bit2G = binarySayi2G.Substring(i, 1);
                        Bit1B = binarySayi1B.Substring(i, 1);
                        Bit2B = binarySayi2B.Substring(i, 1);
                        switch (mantiksalOperator)
                        {
                            case "AND":
                                if (Bit1R == "1" && Bit2R == "1") StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == "1" && Bit2G == "1") StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == "1" && Bit2B == "1") StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            case "NAND":
                                if (Bit1R == "1" && Bit2R == "1") StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == "1" && Bit2G == "1") StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == "1" && Bit2B == "1") StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "OR":
                                if (Bit1R == "1" || Bit2R == "1") StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == "1" || Bit2G == "1") StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == "1" || Bit2B == "1") StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            case "NOR":
                                if (Bit1R == "1" || Bit2R == "1") StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == "1" || Bit2G == "1") StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == "1" || Bit2B == "1") StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "XOR":
                                if (Bit1R == Bit2R) StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == Bit2G) StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == Bit2B) StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "XNOR":
                                if (Bit1R == Bit2R) StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == Bit2G) StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == Bit2B) StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            default:
                                return null;
                        }
                    }
                    R = Convert.ToInt32(StringIkiliSayiR, 2);
                    G = Convert.ToInt32(StringIkiliSayiG, 2);
                    B = Convert.ToInt32(StringIkiliSayiB, 2);
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        public static Bitmap MantiksalOperatorOlcekli(Bitmap giris1, string mantiksalOperator, int olcek, int sabit)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = giris1.Width;
            int ResimYuksekligi = giris1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1;
            int x, y;
            int R = 0, G = 0, B = 0;
            string binarySayi2 = Convert.ToString(sabit, 2).PadLeft(8, '0');
            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = giris1.GetPixel(x, y);
                    string binarySayi1R = Convert.ToString(Renk1.R, 2).PadLeft(8, '0');
                    string binarySayi1G = Convert.ToString(Renk1.G, 2).PadLeft(8, '0');
                    string binarySayi1B = Convert.ToString(Renk1.B, 2).PadLeft(8, '0');
                    string Bit1R = null, Bit1G = null, Bit1B = null;
                    string StringIkiliSayiR = null, StringIkiliSayiG = null, StringIkiliSayiB = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1R = binarySayi1R.Substring(i, 1);
                        Bit1G = binarySayi1G.Substring(i, 1);
                        Bit1B = binarySayi1B.Substring(i, 1);
                        string Bit2 = binarySayi2.Substring(i, 1);
                        switch (mantiksalOperator)
                        {
                            case "AND":
                                if (Bit1R == "1" && Bit2 == "1") StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == "1" && Bit2 == "1") StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == "1" && Bit2 == "1") StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            case "NAND":
                                if (Bit1R == "1" && Bit2 == "1") StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == "1" && Bit2 == "1") StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == "1" && Bit2 == "1") StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "OR":
                                if (Bit1R == "1" || Bit2 == "1") StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == "1" || Bit2 == "1") StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == "1" || Bit2 == "1") StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            case "NOR":
                                if (Bit1R == "1" || Bit2 == "1") StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == "1" || Bit2 == "1") StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == "1" || Bit2 == "1") StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "XOR":
                                if (Bit1R == Bit2) StringIkiliSayiR += "0"; // R
                                else StringIkiliSayiR += "1";
                                if (Bit1G == Bit2) StringIkiliSayiG += "0"; // G
                                else StringIkiliSayiG += "1";
                                if (Bit1B == Bit2) StringIkiliSayiB += "0"; // B
                                else StringIkiliSayiB += "1";
                                break;
                            case "XNOR":
                                if (Bit1R == Bit2) StringIkiliSayiR += "1"; // R
                                else StringIkiliSayiR += "0";
                                if (Bit1G == Bit2) StringIkiliSayiG += "1"; // G
                                else StringIkiliSayiG += "0";
                                if (Bit1B == Bit2) StringIkiliSayiB += "1"; // B
                                else StringIkiliSayiB += "0";
                                break;
                            default:
                                return null;
                        }
                    }
                    R = Convert.ToInt32(StringIkiliSayiR, 2);
                    R *= olcek;
                    if (R > 255) R = 255;
                    G = Convert.ToInt32(StringIkiliSayiG, 2);
                    G *= olcek;
                    if (G > 255) G = 255;
                    B = Convert.ToInt32(StringIkiliSayiB, 2);
                    B *= olcek;
                    if (B > 255) B = 255;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }
            return CikisResmi;
        }

        public static Bitmap MantiksalOperatorOlcekliGri(Bitmap giris1, string mantiksalOperator, int olcek, int sabit)
        {
            Bitmap CikisResmi;
            int ResimGenisligi = giris1.Width;
            int ResimYuksekligi = giris1.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Color Renk1;
            int x, y;
            string binarySayi2 = Convert.ToString(sabit, 2).PadLeft(8, '0');
            for (x = 0; x < ResimGenisligi; x++)
            {
                for (y = 0; y < ResimYuksekligi; y++)
                {
                    Renk1 = giris1.GetPixel(x, y);
                    string binarySayi1 = Convert.ToString(Renk1.R, 2).PadLeft(8, '0');
                    string Bit1 = null, Bit2 = null, StringIkiliSayi = null;
                    for (int i = 0; i < 8; i++)
                    {
                        Bit1 = binarySayi1.Substring(i, 1);
                        Bit2 = binarySayi2.Substring(i, 1);
                        switch (mantiksalOperator)
                        {
                            case "AND":
                                if (Bit1 == "1" && Bit2 == "1") StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            case "NAND":
                                if (Bit1 == "1" && Bit2 == "1") StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "OR":
                                if (Bit1 == "1" || Bit2 == "1") StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            case "NOR":
                                if (Bit1 == "1" || Bit2 == "1") StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "XOR":
                                if (Bit1 == Bit2) StringIkiliSayi += "0";
                                else StringIkiliSayi += "1";
                                break;
                            case "XNOR":
                                if (Bit1 == Bit2) StringIkiliSayi += "1";
                                else StringIkiliSayi += "0";
                                break;
                            default:
                                return null;
                        }
                    }
                    int R = Convert.ToInt32(StringIkiliSayi, 2);
                    R *= olcek;
                    if (R > 255) R = 255;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(R, R, R));
                }
            }
            return CikisResmi;
        }
    }
}
