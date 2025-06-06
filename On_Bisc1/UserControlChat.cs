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
    public partial class userControlChat : UserControl
    {
        private int solicitacaoId;
        private int prestadorId;
        private string nomePrestador;
        private Timer timerAtualizacao;

        public userControlChat()
        {
            InitializeComponent();
            this.solicitacaoId = solicitacaoId;
            this.prestadorId = prestadorId;
            this.nomePrestador = nomePrestador;

            if (solicitacaoId <= 0)
            {
                MessageBox.Show("Aperte Ok se não for um Robô!");
                return;
            }

            timerAtualizacao = new Timer();
            timerAtualizacao.Interval = 3000; // 3 segundos
            timerAtualizacao.Tick += TimerAtualizacao_Tick;
            timerAtualizacao.Start();

            CarregarMensagens();
        }

        private void TimerAtualizacao_Tick(object sender, EventArgs e)
        {
            CarregarMensagensNovas();
        }

        private void CarregarMensagens()
        {
            pnlMensagensprestador.Controls.Clear();

            using (var conexao = new MySqlConnection("server=localhost;database=onbiscato;uid=root;pwd=;"))
            {
                conexao.Open();
                string query = @"SELECT remetente_id, mensagem, data_envio 
                                 FROM mensagens 
                                 WHERE solicitacao_id = @solicitacaoId 
                                 ORDER BY data_envio ASC";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@solicitacaoId", solicitacaoId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        int y = 10;
                        while (reader.Read())
                        {
                            int remetente = reader.GetInt32("remetente_id");
                            string texto = reader.GetString("mensagem");
                            DateTime data = reader.GetDateTime("data_envio");

                            Label lbl = new Label();
                            lbl.Text = $"{(remetente == prestadorId ? nomePrestador : "Cliente")}: {texto}\n{data}";
                            lbl.AutoSize = true;
                            lbl.MaximumSize = new Size(400, 0);
                            lbl.BackColor = remetente == prestadorId ? Color.LightBlue : Color.LightGray;
                            lbl.Padding = new Padding(5);
                            lbl.Margin = new Padding(5);
                            lbl.Location = new Point(remetente == prestadorId ? 350 : 10, y);

                            pnlMensagensprestador.Controls.Add(lbl);
                            y += lbl.Height + 10;
                        }
                    }
                }
            }
        }

        private void CarregarMensagensNovas()
        {
            CarregarMensagens(); // Para simplificação, sempre recarrega tudo
        }

        private void btnEnviarprestador_Click(object sender, EventArgs e)
        {
            string mensagem = txtMensagemprestador.Text.Trim();
            if (string.IsNullOrEmpty(mensagem)) return;

            using (var conexao = new MySqlConnection("your_connection_string_here"))
            {
                conexao.Open();
                string query = @"INSERT INTO mensagens (solicitacao_id, remetente_id, mensagem, data_envio)
                                 VALUES (@solicitacaoId, @remetenteId, @mensagem, NOW())";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@solicitacaoId", solicitacaoId);
                    cmd.Parameters.AddWithValue("@remetenteId", prestadorId);
                    cmd.Parameters.AddWithValue("@mensagem", mensagem);
                    cmd.ExecuteNonQuery();
                }
            }

            txtMensagemprestador.Clear();
            CarregarMensagens();
        }

        private void userControlChat_Load(object sender, EventArgs e)
        {
            // opcionalmente carregar aqui também
        }

        private void pnlMensagensprestador_Paint(object sender, PaintEventArgs e) { }
        private void lblSolicitacaoprestador_Click(object sender, EventArgs e) { }
        private void pnlHeaderprestador_Paint(object sender, PaintEventArgs e) { }
        private void txtMensagemprestador_TextChanged(object sender, EventArgs e) { }
    }
}
