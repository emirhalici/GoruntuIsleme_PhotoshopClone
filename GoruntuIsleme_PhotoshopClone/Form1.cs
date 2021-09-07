using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GoruntuIsleme_PhotoshopClone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            labelParlaklik.Text = (trackBarParlaklik.Value - 50).ToString();
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
            labelKontrast.Text = (trackBarKontrast.Value - 50).ToString();
        }

        private void btnParlaklikUygula_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
            btnParlaklikGeriAl.Enabled = true;
            btnParlaklikUygula.Enabled = false;
        }

        private void btnParlaklikGeriAl_Click(object sender, EventArgs e)
        {
            YedekFotograf = new Bitmap(pictureBox1.Image);
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

            }
        }
    }
}
