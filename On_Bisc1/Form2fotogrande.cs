using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace On_Bisc1
{
    public partial class FormFotoGrande : Form
    {
        public FormFotoGrande(Image imagem)
        {
            InitializeComponent();
            pictureBoxGrande.Image = imagem;
            pictureBoxGrande.SizeMode = PictureBoxSizeMode.Zoom;
        }
        public FormFotoGrande()
        {
            InitializeComponent();
        }

        private void pictureBoxGrande_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxGrande_Click(object sender, EventArgs e)
        {

        }
    }
}
