using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace On_Bisc1
{
    public partial class FormCadastro : Form
    {
        public FormCadastro()
        {
            InitializeComponent();
        }
        private string GerarHashSHA256(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // hexadecimal
                }
                return builder.ToString();
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '*';
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string nome = txtNomeCompleto.Text;
            string email = txtEmail.Text;
            string usuario = txtUsuario.Text;
            string telefone = txtTelefone.Text;
            string senha = txtSenha.Text;
            string confirmarSenha = txtConfirmarSenha.Text;
            string tipo = cmbTipoUsuario.SelectedItem.ToString();

            // Validações básicas
            if (nome == "" || email == "" || usuario == "" || telefone == "" || senha == "" || confirmarSenha == "")
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email inválido.");
                return;
            }

            if (senha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    byte[] fotoPadrao = null;

                    // 1. Buscar imagem padrão da tabela imagem_padrao
                    using (var cmdFoto = new MySqlCommand("SELECT foto FROM imagem_padrao LIMIT 1", conn))
                    using (var reader = cmdFoto.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            fotoPadrao = (byte[])reader["foto"];
                        }
                    }

                    // 2. Inserir o novo usuário com a foto padrão
                    string sql = @"INSERT INTO usuario 
                           (nome, email, nome_usuario, telefone, senha, tipo, foto_perfil) 
                           VALUES 
                           (@nome, @email, @usuario, @telefone, @senha, @tipo, @foto)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        string senhaHash = GerarHashSHA256(senha);

                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@telefone", telefone);
                        cmd.Parameters.AddWithValue("@senha", senhaHash);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@foto", fotoPadrao);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cadastro realizado com sucesso!");
                    }
                }
            }


            catch (MySqlException ex)
            {
                MessageBox.Show("Erro MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro geral: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {

        }
    }
}
