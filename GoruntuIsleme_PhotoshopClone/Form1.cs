using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoruntuIsleme_PhotoshopClone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripContainer1_LeftToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void menuDosyaAc_Click(object sender, EventArgs e)
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int sizeY = pictureBox1.Image.Height;
            int katsayi = trackBar1.Value;
            pictureBox1.Height = sizeY * katsayi / 100;

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
