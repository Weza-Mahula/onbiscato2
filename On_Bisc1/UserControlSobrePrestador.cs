using MySql.Data.MySqlClient;
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
    public partial class UserControlSobrePrestador : UserControl
    {
        private TextBox txtBiografia;
        private string textoPadrao = "ADICIONE AQUI DETALHES SOBRE O SEU OFÍCIO";

        public UserControlSobrePrestador()
        {
            this.BackColor = Color.FromArgb(224, 224, 244);
            this.Size = new Size(861, 387);
            this.Location = new Point(0, 0); // a localização real será atribuída por userControlPerfil

            txtBiografia = new TextBox();
            txtBiografia.Multiline = true;
            txtBiografia.Font = new Font("Segoe UI", 10);
            txtBiografia.ForeColor = Color.Black;
            txtBiografia.Text = textoPadrao;
            txtBiografia.BorderStyle = BorderStyle.FixedSingle;
            txtBiografia.Size = new Size(800, 320);
            txtBiografia.Location = new Point(30, 30);
            txtBiografia.ReadOnly = true;
            txtBiografia.BackColor = Color.White;

            this.Controls.Add(txtBiografia);
        }

        public void SetBiografia(string texto)
        {
            txtBiografia.Text = string.IsNullOrWhiteSpace(texto) ? textoPadrao : texto;
        }
    }
}




