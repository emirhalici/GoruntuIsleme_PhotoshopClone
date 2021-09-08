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
            trackBarR.Enabled = boo;
            trackBarG.Enabled = boo;
            trackBarB.Enabled = boo;
            btnEsikUygula.Enabled = boo;
            btnHistogramGoster.Enabled = boo;
            btnBulaniklastirmaGeriAl.Enabled = boo;
            btnBulaniklastirmaUygula.Enabled = boo;
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
        }

        private void toolStripKontrast_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabParlaklikKontrast;
        }

        private void toolStripEsikleme_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabEsikleme;
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
            if (parlaklikFark!=0)
            {
                YeniFotograf = Model.Parlaklik(YeniFotograf, parlaklikFark);
            }
            if (kontrastFark!=0)
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
            } else
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

            if (OrijinalResim==null) OrijinalResim = new Bitmap(pictureBox1.Image);

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
            } else
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
            int Olcek = (trackBarBulaniklastirmaKatsayi.Value * 2)+1;
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
        }
    }
}
