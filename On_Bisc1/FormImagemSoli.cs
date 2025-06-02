using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace On_Bisc1
{
    public partial class FormImagemSoli : Form
    {
        public FormImagemSoli(byte[] imagem)
        {
            InitializeComponent();
            using (MemoryStream ms = new MemoryStream(imagem))
            {
                pictureBox1.Image = Image.FromStream(ms);
            }
        }

        private void FormImagemSoli_Load(object sender, EventArgs e)
        {

        }
    }

}
