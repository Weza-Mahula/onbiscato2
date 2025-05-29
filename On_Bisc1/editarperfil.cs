using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace On_Bisc1
{
    public partial class editarperfil : Form
    {
        public int userId { get; set; }

        public editarperfil()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BoxPassAntiga.Text) ||
    !string.IsNullOrWhiteSpace(BoxPassNova.Text) ||
    !string.IsNullOrWhiteSpace(BoxPassNova2.Text))
            {
                if (BoxPassNova.Text != BoxPassNova2.Text)
                {
                    MessageBox.Show("As novas senhas não coincidem!");
                    return;
                }

                if (!SenhaAtualCorreta(BoxPassAntiga.Text))
                {
                    MessageBox.Show("A senha antiga está incorreta!");
                    return;
                }

                AtualizarSenha(BoxPassNova.Text);
            }
            AtualizarDadosNoBanco();
            // Atualiza a propriedade para retornar ao perfil
            NovaBiografia = textsobre.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        private bool SenhaAtualCorreta(string senhaDigitada)
        {
            string hashDigitado = GerarHashSHA256(senhaDigitada);
            string hashAtualBD = "";

            string connStr = "server=localhost;database=onbiscato;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT senha FROM usuario WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            hashAtualBD = reader.GetString("senha");
                    }
                }
            }

            return hashDigitado == hashAtualBD;
        }


        private void AtualizarSenha(string novaSenha)
        {
            string connStr = "server=localhost;database=onbiscato;uid=root;pwd=;";

            string novoHash = GerarHashSHA256(novaSenha);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE usuario SET senha = @senha WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@senha", novoHash);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GerarHashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public void ComboOfi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string oficioSelecionado = ComboOfi.SelectedItem.ToString();

            // Verifica se já existe na caixa
            if (!SelecaoOfi.Text.Contains(oficioSelecionado))
            {
                if (string.IsNullOrWhiteSpace(SelecaoOfi.Text))
                {
                    SelecaoOfi.Text = oficioSelecionado;
                }
                else
                {
                    SelecaoOfi.Text += ", " + oficioSelecionado;
                }
            }
        }
        public string NovaBiografia { get; private set; }
        private void AtualizarDadosNoBanco()
        {
            string connStr = "server=localhost;database=onbiscato;uid=root;pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "UPDATE usuario SET provincia = @provincia, municipio = @municipio, bairro = @bairro, oficio = @oficio, sobre = @sobre WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@provincia", ComboProvi.Text);
                    cmd.Parameters.AddWithValue("@municipio", ComboMuni.Text);
                    cmd.Parameters.AddWithValue("@bairro", TextBairro.Text);
                    cmd.Parameters.AddWithValue("@oficio", SelecaoOfi.Text); // ou string.Join(",", listaOficios)
                    cmd.Parameters.AddWithValue("@sobre", textsobre.Text);
                    cmd.Parameters.AddWithValue("@id", userId); // precisa passar esse ID ao abrir o form

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void editarperfil_Load(object sender, EventArgs e)
        {

        }

        public void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ComboProvi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ComboMuni_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SelecaoOfi_TextChanged(object sender, EventArgs e)
        {

        }

        public void textsobre_TextChanged(object sender, EventArgs e)
        {

        }

        public void BoxPassAntiga_TextChanged(object sender, EventArgs e)
        {

        }

        public void BoxPassNova_TextChanged(object sender, EventArgs e)
        {

        }

        public void BoxPassNova2_TextChanged(object sender, EventArgs e)
        {

        }

        public void TextBairro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
