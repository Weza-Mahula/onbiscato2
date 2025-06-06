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
        int idPrestador;
        string nomePrestador;
        private int prestadorId;
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

        public DashboardPrestador(int id, string nome)
        {
            InitializeComponent();
            idPrestador = id;
            this.nomePrestador = nome;
            idUsuario = SessaoUsuario.UsuarioId;

            // Inicializações dos UserControls
            userControlHome = new userControlHome();
            userControlMP = new userControlMP();
            userControlServicos = new userControlServicos();
            userControlPerfil = new userControlPerfil();
            userControlChat = new userControlChat(); // ← Aqui estava faltando

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
        userControlPerfil,
        userControlChat
            };

            if (controles != null && controles.Any())
            {
                foreach (var uc in controles)
                {
                    uc.Size = new Size(920, 603);
                    uc.Location = new Point(304, 134);
                    uc.Margin = new Padding(3);
                    uc.Visible = false;
                    this.Controls.Add(uc);
                }
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
        userControlHome,
        userControlMP,
        userControlPerfil,
        userControlChat
    };

            Guna2Button[] todosBotoes = {
        btnHome,
        btnMeusPedidos,
        btnPerfil,
        btnChat
    };

            Color corSelecionado = Color.FromArgb(0, 109, 119);
            Color corNaoSelecionado = Color.FromArgb(0, 66, 80);
            Color textoBranco = Color.White;

            for (int i = 0; i < todosBotoes.Length; i++)
            {
                todosBotoes[i].FillColor = corNaoSelecionado;
                todosBotoes[i].ForeColor = textoBranco;
                todosBotoes[i].BorderColor = corNaoSelecionado;
                todosBotoes[i].HoverState.FillColor = corNaoSelecionado;
                todosBotoes[i].PressedColor = corNaoSelecionado;
                todosBotoes[i].DisabledState.FillColor = corNaoSelecionado;
                todosBotoes[i].CustomBorderColor = corNaoSelecionado;
            }

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

            foreach (var uc in todosUserControls)
                uc.Visible = false;

            controleSelecionado.Location = new Point(304, 134);
            controleSelecionado.Size = new Size(920, 603);
            controleSelecionado.Margin = new Padding(3);
            controleSelecionado.Visible = true;
        }



        private void ReposicionarBotoes()
        {
            int x = 20; // distância da esquerda
            int y = 100; // posição inicial vertical
            int espacamento = 10;

            btnHome.Location = new Point(x, y);
            y += btnHome.Height + espacamento;

            btnMeusPedidos.Location = new Point(x, y);
            y += btnMeusPedidos.Height + espacamento;

            btnPerfil.Location = new Point(x, y);
            y += btnPerfil.Height + espacamento;

            btnTerminarSessao.Location = new Point(x, y);
        }



        private void DashboardPrestador_Load(object sender, EventArgs e)
        {
            ReposicionarBotoes();
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
            
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            AlternarUserControl(userControlPerfil, btnPerfil);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente terminar a sessão?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
        }


        private void userControlServicos1_Load(object sender, EventArgs e)
        {

        }

       public void guna2Button1_Click_2(object sender, EventArgs e)
         {
            AlternarUserControl(userControlChat, btnChat);
            // Substitua pelas variáveis reais do prestador logado:
            int prestadorId = idPrestador;
            // PASSO: Obter o nome do prestador a partir do banco ou sessão
            string nomePrestador = "Nome do Prestador"; // ← Substitui isso com o valor real
            //lblNome.Text = nomePrestador;
        }

    }
}



