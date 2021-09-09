using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static GoruntuIsleme_PhotoshopClone.Model;

namespace GoruntuIsleme_PhotoshopClone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap OrijinalResim; // rgb renk ayari icin
        Bitmap YedekFotograf; // diger menu ayarlari icin

        public void toolStripAktiflestir(bool boo) // soldaki menuyu aktif-deaktif eder
        {
            toolStripParlaklik.Enabled = boo;
            toolStripKontrast.Enabled = boo;
            toolStripEsikleme.Enabled = boo;
            toolStripNegatif.Enabled = boo;
            toolStripGri.Enabled = boo;
            toolStripR.Enabled = boo;
            toolStripG.Enabled = boo;
            toolStripB.Enabled = boo;
            toolStripBulaniklastirma.Enabled = boo;
            toolStripNetlestirme.Enabled = boo;
            toolStripKenarBulma.Enabled = boo;
            toolStripAlan.Enabled = boo;
            toolStripToplama.Enabled = boo;
            toolStripCikarma.Enabled = boo;
            toolStripOperator.Enabled = boo;
            trackBarR.Enabled = boo;
            trackBarG.Enabled = boo;
            trackBarB.Enabled = boo;
            btnEsikUygula.Enabled = boo;
            btnHistogramGoster.Enabled = boo;
            btnBulaniklastirmaUygula.Enabled = boo;
            btnNetlestirmeUygula.Enabled = boo;
            btnKenarBulmaUygula.Enabled = boo;
            btnNesneyiCikarUygula.Enabled = boo;
            btnAlanUygula.Enabled = boo;
            btnTopCikUygula.Enabled = boo;
            btnMantikUygula.Enabled = boo;
        }

        public void toolStripCheck(bool boo)
        {
            toolStripParlaklik.Checked = boo;
            toolStripKontrast.Checked = boo;
            toolStripEsikleme.Checked = boo;
            toolStripBulaniklastirma.Checked = boo;
            toolStripNetlestirme.Checked = boo;
            toolStripKenarBulma.Checked = boo;
            toolStripAlan.Checked = boo;
            toolStripToplama.Checked = boo;
            toolStripOperator.Checked = boo;
            toolStripCikarma.Checked = boo;
        }


        private void trackBarZoom_Scroll(object sender, EventArgs e) // fotograf goruntuleme boyutunu degistiyor
        {
            int sizeY = pictureBox1.Image.Height;
            int katsayi = trackBarZoom.Value;
            pictureBox1.Height = sizeY * katsayi / 100;
            labelZoom.Text = trackBarZoom.Value + "%";
        }

        private void trackBarParlaklik_Scroll(object sender, EventArgs e)
        {
            labelParlaklik.Text = trackBarParlaklik.Value.ToString();
        }

        private void toolStripParlaklik_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabParlaklikKontrast;
            toolStripCheck(false);
            toolStripParlaklik.Checked = true;
        }

        private void toolStripKontrast_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabParlaklikKontrast;
            toolStripCheck(false);
            toolStripKontrast.Checked = true;
        }

        private void toolStripBulaniklastirma_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabBulaniklastirma;
            toolStripCheck(false);
            toolStripBulaniklastirma.Checked = true;
        }

        private void toolStripEsikleme_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabEsikleme;
            toolStripCheck(false);
            toolStripEsikleme.Checked = true;
        }

        private void trackBarKontrast_Scroll(object sender, EventArgs e)
        {
            labelKontrast.Text = trackBarKontrast.Value.ToString();
        }

        private void btnParlaklikUygula_Click(object sender, EventArgs e) // parlaklik ve kontrast ayari yapiyor
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            Bitmap YeniFotograf = YedekFotograf;
            int parlaklikFark = trackBarParlaklik.Value;
            int kontrastFark = trackBarKontrast.Value;
            if (parlaklikFark != 0)
            {
                YeniFotograf = Model.Parlaklik(YeniFotograf, parlaklikFark);
            }
            if (kontrastFark != 0)
            {
                YeniFotograf = Model.Kontrast(YeniFotograf, kontrastFark);
            }
            pictureBox1.Image = YeniFotograf;
            btnParlaklikGeriAl.Enabled = true;
            btnParlaklikUygula.Enabled = false;
        }

        private void btnParlaklikGeriAl_Click(object sender, EventArgs e) // yapilan parlaklik ve kontrast ayarini geri aliyor
        {
            pictureBox1.Image = YedekFotograf;
            btnParlaklikGeriAl.Enabled = false;
            btnParlaklikUygula.Enabled = true;
        }

        private void toolStripYeni_Click(object sender, EventArgs e) // bos yeni bir resim aciyor
        {
            Bitmap bitmap = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Gray);
            pictureBox1.Image = bitmap;
            trackBarZoom.Enabled = true;
            btnParlaklikUygula.Enabled = true;
            toolStripAktiflestir(true);
        }
        private void toolStripAc_Click(object sender, EventArgs e) // resim dosyasi aciyor
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                String ResminYolu = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(ResminYolu);
                trackBarZoom.Enabled = true;
                btnParlaklikUygula.Enabled = true;
                toolStripAktiflestir(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya acilamadi.");
            }
        }

        private void toolStripKaydet_Click(object sender, EventArgs e) // resmi kaydediyor
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Jpeg Resmi|*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
                saveFileDialog1.Title = "Resmi Kaydet";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    FileStream DosyaAkisi = (FileStream)saveFileDialog1.OpenFile();
                    if (pictureBox1.Image == null)
                    {
                        MessageBox.Show("Lutfen resim kutusunda resim oldugundan emin olun.");
                        return;
                    }

                    try
                    {
                        switch (saveFileDialog1.FilterIndex)
                        {
                            case 1:
                                pictureBox1.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case 2:
                                pictureBox1.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            case 3:
                                pictureBox1.Image.Save(DosyaAkisi, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        DosyaAkisi.Close();
                    }
                    catch (System.NullReferenceException)
                    {
                        MessageBox.Show("Lutfen resim kutusunda resim oldugundan emin olun.");
                        throw;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Resmi kaydederken hata olustu.");
            }
        }

        private void toolStripNegatif_Click(object sender, EventArgs e) // fotografin negatifini aliyor
        {
            if (!toolStripNegatif.Checked)
            {
                pictureBox1.Image = YedekFotograf;
            }
            else
            {
                YedekFotograf = new Bitmap(pictureBox1.Image);
                Bitmap YeniFotograf = Model.Negatif(YedekFotograf);
                pictureBox1.Image = YeniFotograf;
            }
        }

        private void toolStripGri_Click(object sender, EventArgs e) // fotografi griye ceviriyor
        {
            if (!toolStripGri.Checked) // eger deaktive ediliyorsa
            {
                pictureBox1.Image = YedekFotograf;
            }
            else // aktive ediliyorsa
            {
                YedekFotograf = new Bitmap(pictureBox1.Image);
                Bitmap YeniFotograf = Model.Gri(YedekFotograf);
                pictureBox1.Image = YeniFotograf;
            }
        }

        public void RenkAyariYap() // cagirildikca rgb ayarini yapip resmi guncelliyor
        {
            int rOlcek = trackBarR.Value;
            int gOlcek = trackBarG.Value;
            int bOlcek = trackBarB.Value;

            if (OrijinalResim == null) OrijinalResim = new Bitmap(pictureBox1.Image);

            Bitmap YeniResim = Model.RenkAyari(OrijinalResim, rOlcek, gOlcek, bOlcek);
            pictureBox1.Image = YeniResim;
        }

        private void toolStripR_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabRenkler;
            trackBarR.Value = trackBarR.Value == 0 ? 100 : 0;
        }

        private void toolStripG_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabRenkler;
            trackBarG.Value = trackBarG.Value == 0 ? 100 : 0;
        }

        private void toolStripB_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabRenkler;
            trackBarB.Value = trackBarB.Value == 0 ? 100 : 0;
        }

        private void trackBarR_ValueChanged(object sender, EventArgs e)
        {
            labelR.Text = "%" + trackBarR.Value;
            RenkAyariYap();
        }

        private void trackBarG_ValueChanged(object sender, EventArgs e)
        {
            labelG.Text = "%" + trackBarG.Value;
            RenkAyariYap();
        }

        private void trackBarB_ValueChanged(object sender, EventArgs e)
        {
            labelB.Text = "%" + trackBarB.Value;
            RenkAyariYap();
        }

        private void trackBarR_Scroll(object sender, EventArgs e)
        {
            if (trackBarR.Value == 0) toolStripR.Checked = false;
            else toolStripR.Checked = true;
        }

        private void trackBarG_Scroll(object sender, EventArgs e)
        {
            if (trackBarG.Value == 0) toolStripG.Checked = false;
            else toolStripG.Checked = true;
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            if (trackBarB.Value == 0) toolStripB.Checked = false;
            else toolStripB.Checked = true;
        }

        private void radioBtnCiftEsik_CheckedChanged(object sender, EventArgs e)
        {
            trackBarEsik2.Visible = true;
            labelEsik2.Visible = true;
            labelEsik2Text.Visible = true;
            labelEsik1Text.Text = "Alt Eşik";
        }

        private void radioBtnTekEsik_CheckedChanged(object sender, EventArgs e)
        {
            trackBarEsik2.Visible = false;
            labelEsik2.Visible = false;
            labelEsik2Text.Visible = false;
            labelEsik1Text.Text = "Eşik";
        }

        private void trackBarEsik1_Scroll(object sender, EventArgs e)
        {
            labelEsik1.Text = trackBarEsik1.Value.ToString();
        }

        private void trackBarEsik2_Scroll(object sender, EventArgs e)
        {
            labelEsik2.Text = trackBarEsik2.Value.ToString();
        }

        private void btnEsikUygula_Click(object sender, EventArgs e)
        {
            btnEsikUygula.Enabled = false;
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int altDeger = trackBarEsik1.Value;
            int ustDeger = trackBarEsik2.Value;
            Bitmap YeniFotograf;
            if (radioBtnCiftEsik.Checked)
            {
                Console.WriteLine("cift esik, altDeger: " + altDeger + " ustDeger: " + ustDeger);
                YeniFotograf = Esikleme(YedekFotograf, altDeger, ustDeger);
            }
            else
            {
                Console.WriteLine("tek esik, altDeger: " + altDeger);
                YeniFotograf = Esikleme(YedekFotograf, altDeger);
            }
            pictureBox1.Image = YeniFotograf;
            btnEsikGeriAl.Enabled = true;
        }

        private void btnEsikGeriAl_Click(object sender, EventArgs e)
        {
            btnEsikGeriAl.Enabled = false;
            pictureBox1.Image = YedekFotograf;
            btnEsikUygula.Enabled = true;
        }

        private void btnHistogramGoster_Click(object sender, EventArgs e)
        {
            HistogramGosterici gosterici = new HistogramGosterici();
            gosterici.GirisFoto = Histogram(new Bitmap(pictureBox1.Image));
            gosterici.ShowDialog();
            gosterici.Dispose();
        }

        private void trackBarBulaniklastirmaKatsayi_Scroll(object sender, EventArgs e)
        {
            labelBulaniklastirma.Text = ((trackBarBulaniklastirmaKatsayi.Value * 2) + 1).ToString();
        }

        private void btnBulaniklastirmaGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnBulaniklastirmaGeriAl.Enabled = false;
            btnBulaniklastirmaUygula.Enabled = true;
        }

        private void btnBulaniklastirmaUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int Olcek = (trackBarBulaniklastirmaKatsayi.Value * 2) + 1;
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);
            if (radioBtnGauss.Checked) YeniFotograf = Model.BulaniklastirmaGaussian(YedekFotograf, Olcek);
            else if (radioBtnMean.Checked) YeniFotograf = Model.BulaniklastirmaMean(YedekFotograf, Olcek);
            else if (radioBtnMedian.Checked) YeniFotograf = Model.BulaniklastirmaMedian(YedekFotograf, Olcek);
            pictureBox1.Image = YeniFotograf;
            btnBulaniklastirmaGeriAl.Enabled = true;
            btnBulaniklastirmaUygula.Enabled = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabBulaniklastirma;
            toolStripCheck(false);
            toolStripParlaklik.Checked = true;
        }

        private void trackBarNetlestirme_Scroll(object sender, EventArgs e)
        {
            labelNetlestirme.Text = ((trackBarNetlestirme.Value * 2) + 1).ToString();
        }

        private void btnNetlestirmeUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int Olcek = (trackBarNetlestirme.Value * 2) + 1;
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);
            if (radioBtnNetlestirmeKenar.Checked)
            {
                Bitmap BulanikResim = Model.BulaniklastirmaMean(YedekFotograf, Olcek);
                YeniFotograf = Model.KenarGoruntusuKullanarakNetlestirme(YedekFotograf, BulanikResim, Olcek);
            }
            else if (radioBtnNetlestirmeKonvulasyon.Checked)
            {
                YeniFotograf = Model.KonvulasyonIleNetlestirme(YedekFotograf);
            }
            pictureBox1.Image = YeniFotograf;
            btnNetlestirmeGeriAl.Enabled = true;
            btnNetlestirmeUygula.Enabled = false;
        }

        private void btnNetlestirmeGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnNetlestirmeGeriAl.Enabled = false;
            btnNetlestirmeUygula.Enabled = true;
        }

        private void toolStripNetlestirme_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabNetlestirme;
            toolStripCheck(false);
            toolStripNetlestirme.Checked = true;
        }

        private void trackBarKenarBulmaEsik_Scroll(object sender, EventArgs e)
        {
            labelKenarBulmaEsik.Text = trackBarKenarBulmaEsik.Value.ToString();
        }

        private void toolStripKenarBulma_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabKenarBulma;
            toolStripCheck(false);
            toolStripKenarBulma.Checked = true;
        }

        private void btnKenarBulmaUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int esikDegeri = trackBarKenarBulmaEsik.Value;
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);
            if (radioBtnKenarBulmaSobel.Checked)
            {
                YeniFotograf = Model.KenarBulmaSobel(YedekFotograf, esikDegeri);
            }
            else if (radioBtnKenarBulmaCompass.Checked)
            {
                YeniFotograf = Model.KenarBulmaCompass(YedekFotograf, esikDegeri);
            }
            else if (radioBtnKenarBulmaFarkAlarak.Checked)
            {
                int olcek = trackBarKenarBulmaOlcek.Value;
                Bitmap BulanikResim = Model.BulaniklastirmaMean(YedekFotograf, 3);
                YeniFotograf = Model.KenarGoruntusuCikar(YedekFotograf, BulanikResim, olcek);
            }
            pictureBox1.Image = YeniFotograf;
            btnKenarBulmaGeriAl.Enabled = true;
            btnKenarBulmaUygula.Enabled = false;
        }

        private void btnKenarBulmaGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnKenarBulmaGeriAl.Enabled = false;
            btnKenarBulmaUygula.Enabled = true;
        }

        private void radioBtnKenarBulmaFarkAlarak_CheckedChanged(object sender, EventArgs e)
        {
            labelKenarBulmaOlcek.Visible = true;
            labelKenarBulmaOlcekText.Visible = true;
            trackBarKenarBulmaOlcek.Visible = true;
        }

        private void radioBtnKenarBulmaCompass_CheckedChanged(object sender, EventArgs e)
        {
            labelKenarBulmaOlcek.Visible = false;
            labelKenarBulmaOlcekText.Visible = false;
            trackBarKenarBulmaOlcek.Visible = false;
        }

        private void radioBtnKenarBulmaSobel_CheckedChanged(object sender, EventArgs e)
        {
            labelKenarBulmaOlcek.Visible = false;
            labelKenarBulmaOlcekText.Visible = false;
            trackBarKenarBulmaOlcek.Visible = false;
        }

        private void trackBarKenarBulmaOlcek_Scroll(object sender, EventArgs e)
        {
            labelKenarBulmaOlcek.Text = trackBarKenarBulmaOlcek.Value.ToString();
        }

        private void btnResimSec1_Click(object sender, EventArgs e)
        {
            Bitmap resim1 = Model.DosyaAc();
            if (resim1 != null) pictureBoxNesneyiCikar1.Image = resim1;
        }

        private void btnResimSec2_Click(object sender, EventArgs e)
        {
            Bitmap resim2 = Model.DosyaAc();
            if (resim2 != null) pictureBoxNesneyiCikar2.Image = resim2;
        }

        private void btnNesneyiCikarUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);

            Bitmap foto1 = new Bitmap(pictureBoxNesneyiCikar1.Image);
            Bitmap foto2 = new Bitmap(pictureBoxNesneyiCikar2.Image);

            if (foto1 == null || foto2 == null)
            {
                MessageBox.Show("Lütfen resim kutularına resim yükleyiniz.");
                return;
            }

            int olcek = trackBarNesneyiCikarOlcek.Value;
            int esik = trackBarNesneyiCikarEsik.Value;

            Tuple<Bitmap, Bitmap> esitFotolar = ResimBoyutlariniEsitle(foto1, foto2);
            YeniFotograf = ResimdenArkaPlaniCikarOlcekliEsikliRenkli(esitFotolar.Item1, esitFotolar.Item2, olcek, esik);

            pictureBox1.Image = YeniFotograf;
            btnNesneyiCikarGeriAl.Enabled = true;
            btnNesneyiCikarUygula.Enabled = false;
        }

        private void btnNesneyiCikarGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnNesneyiCikarGeriAl.Enabled = false;
            btnNesneyiCikarUygula.Enabled = true;
        }

        private void trackBarNesneyiCikarEsik_Scroll(object sender, EventArgs e)
        {
            labelNesneyiCikarEsik.Text = trackBarNesneyiCikarEsik.Value.ToString();
        }

        private void trackBarNesneyiCikarOlcek_Scroll(object sender, EventArgs e)
        {
            labelNesneyiCikarOlcek.Text = trackBarNesneyiCikarOlcek.Value.ToString();
        }


        private void btnAlanUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);

            int GriEsik = trackBarAlanGriEsik.Value;
            int AlanEsik = trackBarAlanEsik.Value;
            bool negatifAl = checkBoxAlanRenklendirmeNegatif.Checked;
            Bitmap YeniFotograf = new Bitmap(YedekFotograf);
            if (negatifAl) YeniFotograf = Negatif(YeniFotograf);
            YeniFotograf = Gri(YeniFotograf);
            YeniFotograf = Esikleme(YeniFotograf, GriEsik);
            YeniFotograf = BolgeleriAyir(YeniFotograf, AlanEsik);

            pictureBox1.Image = YeniFotograf;

            btnAlanUygula.Enabled = false;
            btnAlanAsindirmaUygula.Enabled = true;
            btnAlanGenisletmeUygula.Enabled = true;
            btnAlanGeriAl.Enabled = true;
        }

        private void btnAlanGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnAlanGeriAl.Enabled = false;
            btnAlanUygula.Enabled = true;
            btnAlanAsindirmaUygula.Enabled = true;
            btnAlanGenisletmeUygula.Enabled = true;
        }

        private void trackBarAlanGriEsik_Scroll(object sender, EventArgs e)
        {
            labelAlanGriEsik.Text = trackBarAlanGriEsik.Value.ToString();
        }

        private void trackBarAlanEsik_Scroll(object sender, EventArgs e)
        {
            labelAlanEsik.Text = trackBarAlanEsik.Value.ToString();
        }

        private void toolStripAlan_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabAlan;
            toolStripCheck(false);
            toolStripAlan.Checked = true;
        }

        private void trackBarTopCik1_Scroll(object sender, EventArgs e)
        {
            labelTopCik1Yuzde.Text = "%" + trackBarTopCik1.Value.ToString();
        }

        private void trackBarTopCik2_Scroll(object sender, EventArgs e)
        {
            labelTopCik2Yuzde.Text = "%" + trackBarTopCik2.Value.ToString();
        }

        private void btnTopCik1Ac_Click(object sender, EventArgs e)
        {
            Bitmap resim1 = Model.DosyaAc();
            if (resim1 != null) pictureBoxTopCik1.Image = resim1;
        }

        private void btnTopCik2Ac_Click(object sender, EventArgs e)
        {
            Bitmap resim2 = Model.DosyaAc();
            if (resim2 != null) pictureBoxTopCik2.Image = resim2;
        }

        private void btnTopCikUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);

            Bitmap foto1 = new Bitmap(pictureBoxTopCik1.Image);
            Bitmap foto2 = new Bitmap(pictureBoxTopCik2.Image);

            if (foto1 == null || foto2 == null)
            {
                MessageBox.Show("Lütfen resim kutularına resim yükleyiniz.");
                return;
            }

            int olcekFoto1 = trackBarTopCik1.Value;
            int olcekFoto2 = trackBarTopCik2.Value;

            Tuple<Bitmap, Bitmap> esitFotolar = ResimBoyutlariniEsitle(foto1, foto2);

            if (radioBtnTopCikSiniraCek.Checked)
            {
                YeniFotograf = ToplamaSiniraCekerek(esitFotolar.Item1, esitFotolar.Item2, olcekFoto2, olcekFoto1);
            }
            else
            {
                YeniFotograf = ToplamaNormalizasyon(esitFotolar.Item1, esitFotolar.Item2, olcekFoto2, olcekFoto1);
            }
            pictureBox1.Image = YeniFotograf;
            btnTopCikGeriAl.Enabled = true;
            btnTopCikUygula.Enabled = false;
        }

        private void btnTopCikGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnTopCikGeriAl.Enabled = false;
            btnTopCikUygula.Enabled = true;
        }

        private void toolStripToplama_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabToplama;
            toolStripCheck(false);
            toolStripToplama.Checked = true;
        }

        private void toolStripOperator_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabOperator;
            toolStripCheck(false);
            toolStripOperator.Checked = true;
        }

        private void toolStripCikarma_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabCikarma;
            toolStripCheck(false);
            toolStripCikarma.Checked = true;
        }

        private void btnMantikUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            Bitmap YeniFotograf = new Bitmap(YedekFotograf.Width, YedekFotograf.Height);
            Bitmap foto1, foto2;
            try
            {
                foto1 = new Bitmap(pictureBoxMantik1.Image);
                foto2 = new Bitmap(pictureBoxMantik2.Image);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen resim kutularına resim yükleyiniz.");
                return;
            }

            bool griFiltre = checkBoxMantikGri.Checked;
            bool olcekKullan = checkBoxMantikOlcekKullan.Checked;

            string mesaj = "";
            if (radioBtnMantikAnd.Checked) mesaj = "AND";
            if (radioBtnMantikNand.Checked) mesaj = "NAND";
            if (radioBtnMantikOr.Checked) mesaj = "OR";
            if (radioBtnMantikXor.Checked) mesaj = "XOR";
            if (radioBtnMantikNor.Checked) mesaj = "NOR";

            Tuple<Bitmap, Bitmap> esitFotolar = ResimBoyutlariniEsitle(foto1, foto2);

            if (olcekKullan)
            {
                int olcek = trackBarMantikOlcek.Value;
                int sabit = trackBarMantikEsik.Value;
                if (griFiltre) YeniFotograf = MantiksalOperatorOlcekliGri(esitFotolar.Item1, mesaj, olcek, sabit);
                else YeniFotograf = MantiksalOperatorOlcekli(esitFotolar.Item1, mesaj, olcek, sabit);
            }
            else
            {
                if (griFiltre) YeniFotograf = MantiksalOperatorGri(esitFotolar.Item1, esitFotolar.Item2, mesaj);
                else YeniFotograf = MantiksalOperator(esitFotolar.Item1, esitFotolar.Item2, mesaj);
            }

            if (YeniFotograf == null)
            {
                MessageBox.Show("Hata olustu.");
                return;
            }

            pictureBox1.Image = YeniFotograf;
            btnMantikUygula.Enabled = false;
            btnMantikGeriAl.Enabled = true;
        }

        private void btnMantikAc1_Click(object sender, EventArgs e)
        {
            Bitmap resim1 = Model.DosyaAc();
            if (resim1 != null) pictureBoxMantik1.Image = resim1;
        }

        private void btnMantikAc2_Click(object sender, EventArgs e)
        {
            Bitmap resim2 = Model.DosyaAc();
            if (resim2 != null) pictureBoxMantik2.Image = resim2;
        }

        private void trackBarMantikOlcek_Scroll(object sender, EventArgs e)
        {
            labelMantikOlcek.Text = trackBarMantikOlcek.Value.ToString();
        }

        private void btnMantikGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnMantikUygula.Enabled = true;
            btnMantikGeriAl.Enabled = false;
        }

        private void trackBarMantikEsik_Scroll(object sender, EventArgs e)
        {
            labelMantikEsik.Text = trackBarMantikEsik.Value.ToString();
        }

        private void btnAlanAsindirmaUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int esik = trackBarAlanAsindirEsik.Value;
            Bitmap SiyahBeyazResim = Gri(YedekFotograf);
            SiyahBeyazResim = EsiklemeSiyahBeyaz(SiyahBeyazResim, esik);
            HistogramGosterici gosterici = new HistogramGosterici();
            gosterici.GirisFoto = SiyahBeyazResim;
            gosterici.ShowDialog();
            gosterici.Dispose();
            Bitmap YeniFotograf = Asindirma(SiyahBeyazResim);
            pictureBox1.Image = YeniFotograf;
            btnAlanAsindirmaUygula.Enabled = false;
            btnAlanGenisletmeUygula.Enabled = false;
            btnAlanUygula.Enabled = false;
            btnAlanGeriAl.Enabled = true;
        }

        private void btnAlanGenisletmeUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            int esik = trackBarAlanAsindirEsik.Value;
            Bitmap SiyahBeyazResim = Gri(YedekFotograf);
            SiyahBeyazResim = EsiklemeSiyahBeyaz(SiyahBeyazResim, esik);
            HistogramGosterici gosterici = new HistogramGosterici();
            gosterici.GirisFoto = SiyahBeyazResim;
            gosterici.ShowDialog();
            gosterici.Dispose();
            Bitmap YeniFotograf = Genisletme(SiyahBeyazResim);
            pictureBox1.Image = YeniFotograf;
            btnAlanAsindirmaUygula.Enabled = false;
            btnAlanGenisletmeUygula.Enabled = false;
            btnAlanUygula.Enabled = false;
            btnAlanGeriAl.Enabled = true;
        }

        private void trackBarAlanAsindirEsik_Scroll(object sender, EventArgs e)
        {
            labelAlanAsindir.Text = trackBarAlanAsindirEsik.Value.ToString();
        }
    }
}
