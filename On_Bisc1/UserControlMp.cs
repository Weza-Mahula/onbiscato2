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
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex].Name == "status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Aceite")
                    e.CellStyle.ForeColor = Color.Green;
                else if (status == "Negado")
                    e.CellStyle.ForeColor = Color.Red;
                else if (status == "Pendente")
                    e.CellStyle.ForeColor = Color.Goldenrod;
            }
        }

        private void AjustarColunas()
        {
            foreach (var dgv in new[] { DataPend, historico })
            {
                dgv.AutoGenerateColumns = false;
                dgv.Columns.Clear();

                dgv.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "id",
                    HeaderText = "ID",
                    DataPropertyName = "id",
                    Visible = false
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "cliente",
                    HeaderText = "Cliente",
                    DataPropertyName = "cliente",
                    Width = 200
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "data_solicitacao",
                    HeaderText = "Data",
                    DataPropertyName = "data_solicitacao",
                    Width = 150
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "status",
                    HeaderText = "Status",
                    DataPropertyName = "status",
                    Width = 100
                });

                dgv.Columns.Add(new DataGridViewImageColumn()
                {
                    Name = "imagem",
                    HeaderText = "Imagem",
                    DataPropertyName = "imagem",
                    Visible = false
                });

                dgv.RowTemplate.Height = 30;
                dgv.ColumnHeadersHeight = 35;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 66, 80);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
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
                        DataPend.DataSource = dt;
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
            if (DataPend.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma solicitação pendente.");
                return;
            }

            int id = Convert.ToInt32(DataPend.SelectedRows[0].Cells["id"].Value);

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
            DataPend.Columns.Clear();

            DataPend.Columns.Add("ID", "ID");
            DataPend.Columns["ID"].Visible = false;

            DataPend.Columns.Add("Cliente", "Cliente");

            DataPend.Columns.Add("DataSolicitacao", "Data da Solicitação");

            DataPend.Columns.Add("Status", "Status");

            // Coluna de imagem (oculta)
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Imagem";
            imgCol.HeaderText = "Imagem";
            imgCol.Visible = false;
            DataPend.Columns.Add(imgCol);
        }

        private void userControlMP_Load(object sender, EventArgs e)
        {
            CarregarSolicitacoes();
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
            if (DataPend.SelectedRows.Count > 0)
            {
                var imagemBytes = DataPend.SelectedRows[0].Cells["Imagem"].Value as byte[];

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
