using MySql.Data.MySqlClient;
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
    public partial class userControlPerfil : UserControl
    {
        private UserControlSobrePrestador userControlSobrePrestador;
        private UserControlPublicacoesPrestador userControlPublicacoesPrestador;

        public userControlPerfil()
        {
            InitializeComponent();

            userControlSobrePrestador = new UserControlSobrePrestador();
            userControlPublicacoesPrestador = new UserControlPublicacoesPrestador();

            userControlSobrePrestador.Visible = false;
            userControlPublicacoesPrestador.Visible = true;

            userControlSobrePrestador = new UserControlSobrePrestador();
            userControlSobrePrestador.Size = new Size(861, 387);
            userControlSobrePrestador.Location = new Point(15, 122); // antes era 125
            this.Controls.Add(userControlSobrePrestador);

            this.Controls.Add(userControlSobrePrestador);
            this.Controls.Add(userControlPublicacoesPrestador);
        }
        private void SincronizarFotosPerfil()
        {
            DashboardPrestador mainForm = this.FindForm() as DashboardPrestador;
            if (mainForm != null)
            {
                mainForm.FotoPrincipal.Image = PictureBoxPerfil.Image;
            }
        }
        public PictureBox PerfilImage
        {
            get { return PictureBoxPerfil; }
        }
       


        private void userControlPerfil_Load(object sender, EventArgs e)
        {
            using (var conn = Conexao.Conectar())
            {
                string sql = @"SELECT id, nome, provincia, municipio, bairro, oficio FROM usuario WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", SessaoUsuario.UsuarioId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nomeCompleto = reader["nome"].ToString();
                            string[] partesNome = nomeCompleto.Split(' ');
                            string nomeReduzido = partesNome.Length >= 2
    ? partesNome[0] + " " + partesNome[partesNome.Length - 1]
    : nomeCompleto;

                            LabelNomeCompleto.Text = nomeReduzido;
                            LabelIDPrestador.Text = $"Prestador ID: {reader["id"]} 🟢";

                            string oficio = reader["oficio"]?.ToString();
                            LabelOficio.Text = string.IsNullOrWhiteSpace(oficio) ? "Unknown" : oficio;

                            string provincia = reader["provincia"]?.ToString();
                            string municipio = reader["municipio"]?.ToString();
                            provincia = string.IsNullOrWhiteSpace(provincia) ? "Unknown" : provincia;
                            municipio = string.IsNullOrWhiteSpace(municipio) ? "Unknown" : municipio;
                            LabelProvinciaMuni.Text = $"{provincia} - {municipio}";
                            LabelProvinciaMuni.Font = new Font("Segoe UI", 10F);

                            string bairro = reader["bairro"]?.ToString();
                            LabelBairro.Text = string.IsNullOrWhiteSpace(bairro) ? "Unknown" : bairro;
                            LabelBairro.Font = new Font("Segoe UI", 8F);
                        }
                    }
                }
            }

            Point localizacaoConteudo = new Point(15, 125); // logo após os 88 + 26 de altura dos painéis

            if (userControlSobrePrestador == null)
            {
                userControlSobrePrestador = new UserControlSobrePrestador();
                userControlSobrePrestador.Size = new Size(861, 387);
                userControlSobrePrestador.Location = localizacaoConteudo;
                this.Controls.Add(userControlSobrePrestador);
            }

            if (userControlPublicacoesPrestador == null)
            {
                userControlPublicacoesPrestador = new UserControlPublicacoesPrestador();
                userControlPublicacoesPrestador.Size = new Size(861, 387);
                userControlPublicacoesPrestador.Location = new Point(15, 122);
                this.Controls.Add(userControlPublicacoesPrestador);
            }

            // Mostrar publicações por padrão
            userControlPublicacoesPrestador.Visible = true;
            userControlSobrePrestador.Visible = false;

            btnPublicacoes.BackColor = Color.FromArgb(224, 224, 224);
            btnSobre.BackColor = Color.FromArgb(0, 66, 80);
        }

        private void btnPublicacoes_Click(object sender, EventArgs e)
        {
            userControlPublicacoesPrestador.Visible = true;
            userControlSobrePrestador.Visible = false;
            btnPublicacoes.BackColor = Color.FromArgb(224, 224, 224);
            btnSobre.BackColor = Color.FromArgb(0, 66, 80);
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            userControlPublicacoesPrestador.Visible = false;
            userControlSobrePrestador.Visible = true;

            btnSobre.BackColor = Color.FromArgb(224, 224, 224);
            btnPublicacoes.BackColor = Color.FromArgb(0, 66, 80);
        }
        private void ExibirFotoGrande()
        {
            if (PictureBoxPerfil.Image != null)
            {
                FormFotoGrande form = new FormFotoGrande(PictureBoxPerfil.Image);
                form.ShowDialog();
            }
        }

        public void AtualizarFotoPerfil()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Imagens (*.jpg;*.png)|*.jpg;*.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string caminhoImagem = open.FileName;
                byte[] imagemBytes = File.ReadAllBytes(caminhoImagem);

                try
                {
                    using (var conn = Conexao.Conectar())
                    {
                        string sql = "UPDATE usuario SET foto_perfil = @foto WHERE id = @id";
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@foto", imagemBytes);
                            cmd.Parameters.AddWithValue("@id", SessaoUsuario.UsuarioId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Atualiza as duas PictureBox
                    using (MemoryStream ms = new MemoryStream(imagemBytes))
                    {
                        Image imagem = Image.FromStream(ms);
                        PictureBoxPerfil.Image = imagem;
                        PictureBoxPerfil.SizeMode = PictureBoxSizeMode.Zoom;

                        var form = Application.OpenForms["DashboardPrestador"] as DashboardPrestador;
                        if (form != null)
                        {
                            form.FotoPrincipal.Image = imagem;
                            form.FotoPrincipal.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }

                    MessageBox.Show("Foto de perfil atualizada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar foto: " + ex.Message);
                }
            }
        }


        private void PictureBoxPerfil_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem itemExibir = new ToolStripMenuItem("Exibir Foto");
            ToolStripMenuItem itemAlterar = new ToolStripMenuItem("Alterar Foto");

            itemExibir.Click += (s, args) => ExibirFotoGrande();
            itemAlterar.Click += (s, args) => AtualizarFotoPerfil();

            menu.Items.Add(itemExibir);
            menu.Items.Add(itemAlterar);

            menu.Show(Cursor.Position);
            this.PictureBoxPerfil.Click += new System.EventHandler(this.PictureBoxPerfil_Click);

        }
        public int userId { get; set; }


        private void BotãoEditarPerfil_Click(object sender, EventArgs e)
        {
            editarperfil editarPerfil = new editarperfil();
            editarPerfil.userId = SessaoUsuario.UsuarioId; // Pega o ID da sessão atual

            if (editarPerfil.ShowDialog() == DialogResult.OK)
            {
                LabelProvinciaMuni.Text = editarPerfil.ComboProvi.Text + ", " + editarPerfil.ComboMuni.Text;
                LabelBairro.Text = editarPerfil.TextBairro.Text;

                string[] oficiosSelecionados = editarPerfil.SelecaoOfi.Text.Split(',');
                LabelOficio.Text = oficiosSelecionados.Length > 0 ? oficiosSelecionados[0] : "";

                userControlSobrePrestador.textBoxBio.Text = editarPerfil.NovaBiografia;
            }

        }
     }

}
