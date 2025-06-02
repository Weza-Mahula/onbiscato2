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
    public partial class userControlMP : UserControl
    {
        public userControlMP()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AjustarColunas()
        {
            string[] colunas = { "id", "cliente", "data_solicitacao", "status" };

            foreach (var dgv in new[] { Datapend, historico })
            {
                dgv.AutoGenerateColumns = false;
                dgv.Columns.Clear();

                dgv.Columns.Add("id", "ID");
                dgv.Columns["id"].DataPropertyName = "id";

                dgv.Columns.Add("cliente", "Cliente");
                dgv.Columns["cliente"].DataPropertyName = "cliente";

                dgv.Columns.Add("data_solicitacao", "Data");
                dgv.Columns["data_solicitacao"].DataPropertyName = "data_solicitacao";

                dgv.Columns.Add("status", "Status");
                dgv.Columns["status"].DataPropertyName = "status";
            }
        }

        private void CarregarSolicitacoes()
        {
            using (var conn = Conexao.Conectar())
            {

                // Solicitações pendentes
                string sqlPend = @"SELECT s.id, c.nome AS cliente, s.data_solicitacao, s.status, s.imagem
                           FROM solicitacoes s
                           JOIN usuario c ON c.id = s.cliente_id
                           WHERE s.prestador_id = @id AND s.status = 'Pendente'";

                using (var cmd = new MySqlCommand(sqlPend, conn))
                {
                    cmd.Parameters.AddWithValue("@id", SessaoUsuario.UsuarioId);
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Datapend.DataSource = dt;
                    }
                }

                // Solicitações do histórico
                string sqlHist = @"SELECT s.id, c.nome AS cliente, s.data_solicitacao, s.status, s.imagem
                           FROM solicitacoes s
                           JOIN usuario c ON c.id = s.cliente_id
                           WHERE s.prestador_id = @id AND s.status IN ('Aceite', 'Negado', 'Finalizado')";

                using (var cmd = new MySqlCommand(sqlHist, conn))
                {
                    cmd.Parameters.AddWithValue("@id", SessaoUsuario.UsuarioId);
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        historico.DataSource = dt;
                    }
                }
            }

            AjustarColunas();
        }

       

        private void AtualizarStatus(string novoStatus)
        {
            if (Datapend.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma solicitação pendente.");
                return;
            }

            int id = Convert.ToInt32(Datapend.SelectedRows[0].Cells["id"].Value);

            using (var conn = Conexao.Conectar())
            {
                string sql = "UPDATE solicitacoes SET status = @status WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", novoStatus);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            CarregarSolicitacoes(); 
        }

        private void ConfigurarColunasDataPend()
        {
            Datapend.Columns.Clear();

            Datapend.Columns.Add("ID", "ID");
            Datapend.Columns["ID"].Visible = false;

            Datapend.Columns.Add("Cliente", "Cliente");

            Datapend.Columns.Add("DataSolicitacao", "Data da Solicitação");

            Datapend.Columns.Add("Status", "Status");

            // Coluna de imagem (oculta)
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Imagem";
            imgCol.HeaderText = "Imagem";
            imgCol.Visible = false;
            Datapend.Columns.Add(imgCol);
        }

        private void userControlMP_Load(object sender, EventArgs e)
        {
            CarregarSolicitacoes();
            ConfigurarColunasDataPend();
        }

        private void btnAceitar_Click(object sender, EventArgs e)
        {
            AtualizarStatus("Aceite");
        }

        private void btnRejeitar_Click(object sender, EventArgs e)
        {
            AtualizarStatus("Negado");
        }

        private void btnVerImagem_Click(object sender, EventArgs e)
        {
            if (Datapend.SelectedRows.Count > 0)
            {
                var imagemBytes = Datapend.SelectedRows[0].Cells["Imagem"].Value as byte[];

                if (imagemBytes != null)
                {
                    FormImagemSoli frm = new FormImagemSoli(imagemBytes);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Esta solicitação não tem imagem.");
                }
            }
        }

    }
}
