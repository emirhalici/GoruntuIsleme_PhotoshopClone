using System;
using System.Collections;
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
    public partial class HistogramGosterici : Form
    {
        public HistogramGosterici()
        {
            InitializeComponent();
        }

        public Bitmap GirisFoto { get; set; }

        private void HistogramGosterici_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = GirisFoto;
        }
    }
}
