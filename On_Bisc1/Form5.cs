using Guna.UI2.WinForms;
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
    public partial class DashboardPrestador : Form
    {
        // Instâncias dos UserControls
        userControlHome userControlHome;
        userControlMP userControlMP;
        userControlServicos userControlServicos;
        userControlPerfil userControlPerfil;
        userControlChat userControlChat;
        int idUsuario;
        public PictureBox FotoPrincipal
        {
            get { return pictureBoxFoto; }
        }
        private void AtualizarImagemPerfilNosControles()
        {
            if (userControlPerfil != null)
            {
                userControlPerfil.PerfilImage.Image = pictureBoxFoto.Image;
            }
        }

        public DashboardPrestador()
        {
            InitializeComponent();
            idUsuario = SessaoUsuario.UsuarioId;
            userControlHome = new userControlHome();
            userControlMP = new userControlMP();
            userControlServicos = new userControlServicos();
            userControlPerfil = new userControlPerfil();
            userControlChat = new userControlChat();

            // Adiciona ao formulário
            AdicionarUserControls();

            // Exibe o UserControl inicial
            AlternarUserControl(userControlHome, btnHome);
        }
        private void AdicionarUserControls()
        {
            var controles = new UserControl[]
            {
        userControlHome,
        userControlMP,
        userControlServicos,
        userControlPerfil,
        userControlChat
            };

            foreach (var uc in controles)
            {
                uc.Size = new Size(920, 603);
                uc.Location = new Point(304, 134);
                uc.Margin = new Padding(3, 3, 3, 3);
                uc.Visible = false; // Inicialmente escondido
                this.Controls.Add(uc);
            }
        }
        
        private void ExibirFotoGrande()
        {
            if (pictureBoxFoto.Image != null)
            {
                FormFotoGrande form = new FormFotoGrande(pictureBoxFoto.Image);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhuma imagem para exibir.");
            }
        }
        private void AtualizarFotoPerfil()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione uma nova foto de perfil";
            openFileDialog.Filter = "Imagens (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoImagem = openFileDialog.FileName;

                try
                {
                    byte[] imagemBytes = File.ReadAllBytes(caminhoImagem);

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

                    // Atualizar visualmente a foto
                    using (MemoryStream ms = new MemoryStream(imagemBytes))
                    {
                        Image imagem = Image.FromStream(ms);
                        pictureBoxFoto.Image = imagem;
                        pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;

                        // NOTIFICA A BOX GRANDE
                        userControlPerfil.AtualizarFotoPerfil(); // <-- isso sincroniza
                    }

                    MessageBox.Show("Foto de perfil atualizada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar foto: " + ex.Message);
                }
            }
        }

        private void CarregarFotoPerfil(int idUsuario, PictureBox pictureBoxFoto)
        {
            // Buscar e exibir foto de perfil
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    string sql = "SELECT foto_perfil FROM usuario WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && !reader.IsDBNull(0))
                            {
                                byte[] imagemBytes = (byte[])reader["foto_perfil"];
                                using (var ms = new MemoryStream(imagemBytes))
                                {
                                    pictureBoxFoto.Image = Image.FromStream(ms);
                                    pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;
                                }
                            }
                            else
                            {
                                pictureBoxFoto.Image = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar imagem de perfil: " + ex.Message);
            }
        }
        private void AlternarUserControl(UserControl controleSelecionado, Guna2Button botaoClicado)
        {
            UserControl[] todosUserControls = {
        userControlHome, userControlMP, userControlServicos, userControlPerfil, userControlChat
    };

            Guna2Button[] todosBotoes = {
        btnHome, btnMeusPedidos, btnServicos, btnPerfil, btnChat
    };

            Color corSelecionado = Color.FromArgb(0, 109, 119); // fundo selecionado
            Color corNaoSelecionado = Color.FromArgb(0, 66, 80); // fundo padrão
            Color textoBranco = Color.White;

            for (int i = 0; i < todosBotoes.Length; i++)
            {
                todosBotoes[i].FillColor = corNaoSelecionado;
                todosBotoes[i].ForeColor = textoBranco;

                // Limpa bordas e hover também
                todosBotoes[i].BorderColor = corNaoSelecionado;
                todosBotoes[i].HoverState.FillColor = corNaoSelecionado;
                todosBotoes[i].PressedColor = corNaoSelecionado;
                todosBotoes[i].DisabledState.FillColor = corNaoSelecionado;
                todosBotoes[i].CustomBorderColor = corNaoSelecionado;
            }

            // Ativa o botão clicado
            botaoClicado.CheckedState.FillColor = corSelecionado;
            botaoClicado.CheckedState.ForeColor = textoBranco;
            botaoClicado.HoverState.ForeColor = textoBranco;

            botaoClicado.FillColor = corSelecionado;
            botaoClicado.ForeColor = textoBranco;
            botaoClicado.BorderColor = corSelecionado;
            botaoClicado.HoverState.FillColor = corSelecionado;
            botaoClicado.PressedColor = corSelecionado;
            botaoClicado.DisabledState.FillColor = corSelecionado;
            botaoClicado.CustomBorderColor = corSelecionado;

            // Exibe apenas o UserControl selecionado
            foreach (var uc in todosUserControls)
                uc.Visible = false;

            controleSelecionado.Location = new Point(304, 134);
            controleSelecionado.Size = new Size(920, 603);
            controleSelecionado.Margin = new Padding(3);
            controleSelecionado.Visible = true;
        }





        private void DashboardPrestador_Load(object sender, EventArgs e)
        {

            AlternarUserControl(userControlHome, btnHome);
            CarregarFotoPerfil(SessaoUsuario.UsuarioId, pictureBoxFoto);
            labelNomeP.Text = $"Bem-vindo, {SessaoUsuario.TipoUsuario} {SessaoUsuario.Nome.Split(' ')[0]}";
            labelID.Text = $"{SessaoUsuario.TipoUsuario} ID: {SessaoUsuario.UsuarioId}🟢";
        }

        private void btnTerminarSessao_Click(object sender, EventArgs e)
        {

        }

        private void BemVindo_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            try
            {
                string caminhoImagem = @"C:\Users\Weza\Documents\foto_padrao.jpg";

                if (!File.Exists(caminhoImagem))
                {
                    MessageBox.Show("Imagem padrão não encontrada.");
                    return;
                }

                byte[] imagemBytes = File.ReadAllBytes(caminhoImagem);

                string connStr = "server=localhost;user=root;database=onbiscato";

                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "UPDATE usuario SET foto_perfil = @foto WHERE foto_perfil IS NULL";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@foto", imagemBytes);
                        int afetados = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Imagem padrão aplicada a {afetados} usuários.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            string caminhoImagem = @"C:\Users\Weza\Documents\foto_padrao.jpg";

            if (!File.Exists(caminhoImagem))
            {
                MessageBox.Show("Imagem padrão não encontrada.");
                return;
            }

            byte[] imagemBytes = File.ReadAllBytes(caminhoImagem);

            string connStr = "server=localhost;user=root;database=onbiscato";

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // Supondo que a tabela foto_padrao tenha um campo 'id' autoincrement e 'foto' BLOB
                string sql = "INSERT INTO imagem_padrao (foto) VALUES (@foto)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@foto", imagemBytes);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Imagem padrão inserida na tabela foto_padrao com sucesso!");
                }
            }
        }

        private void pictureBoxFoto_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem itemExibir = new ToolStripMenuItem("Exibir Foto");
            ToolStripMenuItem itemAlterar = new ToolStripMenuItem("Alterar Foto");

            itemExibir.Click += (s, args) => ExibirFotoGrande();
            itemAlterar.Click += (s, args) => AtualizarFotoPerfil();

            menu.Items.Add(itemExibir);
            menu.Items.Add(itemAlterar);

            menu.Show(Cursor.Position);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlHome, btnHome);
        }

        private void btnMeusPedidos_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlMP, btnMeusPedidos);
        }

        private void btnServicos_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlServicos, btnServicos);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlPerfil, btnPerfil);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlChat, btnChat);
        }
    }
}


