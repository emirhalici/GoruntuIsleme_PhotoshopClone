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

        public void toolStripAktiflestir(bool boo)
        {
            toolStripParlaklik.Enabled = boo;
            toolStripKontrast.Enabled = boo;
            toolStripEsikleme.Enabled = boo;
            toolStripNegatif.Enabled = boo;
            toolStripGri.Enabled = boo;
            toolStripR.Enabled = boo;
            toolStripG.Enabled = boo;
            toolStripB.Enabled = boo;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int sizeY = pictureBox1.Image.Height;
            int katsayi = trackBar1.Value;
            pictureBox1.Height = sizeY * katsayi / 100;
            labelZoom.Text = trackBar1.Value + "%";
        }

        private void trackBarParlaklik_Scroll(object sender, EventArgs e)
        {
            labelParlaklik.Text = trackBarParlaklik.Value.ToString();
        }

        public Bitmap YedekFotograf;

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

        private void btnParlaklikUygula_Click(object sender, EventArgs e)
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

        private void btnParlaklikGeriAl_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = YedekFotograf;
            btnParlaklikGeriAl.Enabled = false;
            btnParlaklikUygula.Enabled = true;
        }

        private void toolStripYeni_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(500, 500);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Gray);
            pictureBox1.Image = bitmap;
            trackBar1.Enabled = true;
            btnParlaklikUygula.Enabled = true;
            toolStripAktiflestir(true);
        }
        private void toolStripAc_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                String ResminYolu = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(ResminYolu);
                trackBar1.Enabled = true;
                btnParlaklikUygula.Enabled = true;
                toolStripAktiflestir(true);
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya acilamadi.");
            }
        }

        private void toolStripKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Jpeg Resmi|*.jpg|Bitmap Resmi|*.bmp|Gif Resmi|*.gif";
                saveFileDialog1.Title = "Resmi Kaydet";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "") //Dosya adı boş değilse kaydedecek.
                {
                    // FileStream nesnesi ile kayıtı gerçekleştirecek.
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

        private void toolStripNegatif_Click(object sender, EventArgs e)
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

        private void toolStripGri_Click(object sender, EventArgs e)
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

        Bitmap OrijinalResim;

        public void RenkAyariYap()
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
    }
}
