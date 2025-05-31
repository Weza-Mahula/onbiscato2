using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace On_Bisc1
{
    public partial class NovaSolitaçãoV2 : Form
    {
        public NovaSolitaçãoV2()
        {
            InitializeComponent();
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.Size = new Size(400, 50); // Largura e altura desejadas
            guna2HtmlLabel1.TextAlignment = ContentAlignment.MiddleLeft;

            // Se quiser ajustar a segunda também
            guna2HtmlLabel2.AutoSize = false;
            guna2HtmlLabel2.Size = new Size(400, 50);
            guna2HtmlLabel2.Text = "Encontre aqui!!.";
        }

        private void NovaSolitaçãoV2_Load(object sender, EventArgs e)
        {
            dgvResultadosBusca.AutoGenerateColumns = false;
            dgvResultadosBusca.Columns.Clear();

            // Visibilidade dos cabeçalhos
            dgvResultadosBusca.ColumnHeadersVisible = true;
            dgvResultadosBusca.EnableHeadersVisualStyles = false;

            // Cor personalizada (0, 124, 139)
            Color azulPetroleo = Color.FromArgb(0, 124, 139);

            // Estilo dos cabeçalhos
            dgvResultadosBusca.ColumnHeadersDefaultCellStyle.BackColor = azulPetroleo;
            dgvResultadosBusca.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResultadosBusca.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvResultadosBusca.ColumnHeadersHeight = 40;

            // Estilo das células
            dgvResultadosBusca.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvResultadosBusca.DefaultCellStyle.BackColor = Color.White;
            dgvResultadosBusca.DefaultCellStyle.ForeColor = Color.Black;
            dgvResultadosBusca.DefaultCellStyle.SelectionBackColor = azulPetroleo;
            dgvResultadosBusca.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvResultadosBusca.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Estilo geral
            dgvResultadosBusca.BorderStyle = BorderStyle.None;
            dgvResultadosBusca.GridColor = Color.LightGray;
            dgvResultadosBusca.RowTemplate.Height = 40;
            dgvResultadosBusca.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 250);

            // Colunas
            dgvResultadosBusca.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nome",
                DataPropertyName = "nome",
                Name = "nome",
                Width = 150
            });

            dgvResultadosBusca.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ofício",
                DataPropertyName = "oficio",
                Name = "oficio",
                Width = 120
            });

            dgvResultadosBusca.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Localização",
                DataPropertyName = "localizacao",
                Name = "localizacao",
                Width = 180
            });

            dgvResultadosBusca.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "servico_id",
                Name = "servico_id",
                Visible = false
            });

            dgvResultadosBusca.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colSolicitar",
                HeaderText = "Ação",
                Text = "Solicitar",
                UseColumnTextForButtonValue = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = azulPetroleo,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(0, 102, 115)
                }
            });
        }

        private void dgvResultadosBusca_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvResultadosBusca.Columns[e.ColumnIndex].Name == "colSolicitar")
            {
                int servicoId = Convert.ToInt32(dgvResultadosBusca.Rows[e.RowIndex].Cells["servico_id"].Value);
                int clienteId = SessaoUsuario.UsuarioId;

                try
                {
                    using (var conn = Conexao.Conectar())
                    {
                        // Buscar o prestador_id do serviço
                        string buscarPrestador = "SELECT prestador_id FROM servico WHERE id = @id";
                        int prestadorId;

                        using (var cmdBuscar = new MySqlCommand(buscarPrestador, conn))
                        {
                            cmdBuscar.Parameters.AddWithValue("@id", servicoId);
                            prestadorId = Convert.ToInt32(cmdBuscar.ExecuteScalar());
                        }

                        // Inserir a solicitação
                        string sql = @"INSERT INTO solicitacoes (cliente_id, servico_id, prestador_id, status)
                               VALUES (@cliente, @servico, @prestador, 'pendente')";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@cliente", clienteId);
                            cmd.Parameters.AddWithValue("@servico", servicoId);
                            cmd.Parameters.AddWithValue("@prestador", prestadorId);

                            cmd.ExecuteNonQuery();
                        }

                        // ✅ Mensagem de sucesso estilizada + fechamento
                        DialogResult result = MessageBox.Show(
                            "Solicitação enviada com sucesso!",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao enviar solicitação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string termo = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(termo))
            {
                CarregarPrestadores(termo);
            }
            else
            {
                dgvResultadosBusca.DataSource = null;
            }
        }
        private void CarregarPrestadores(string termo)
        {
            using (var conn = Conexao.Conectar())
            {
                string sql = @"
            SELECT 
                usuario.nome, 
                usuario.oficio,
                CONCAT(usuario.bairro, ', ', usuario.municipio, ', ', usuario.provincia) AS localizacao,
                servico.id AS servico_id
            FROM usuario
            JOIN servico ON servico.prestador_id = usuario.id
            WHERE usuario.tipo = 'prestador' AND
                (usuario.nome LIKE @termo OR usuario.oficio LIKE @termo 
                 OR usuario.bairro LIKE @termo OR usuario.municipio LIKE @termo 
                 OR usuario.provincia LIKE @termo)";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@termo", "%" + termo + "%");

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvResultadosBusca.DataSource = dt;
                    }
                }
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
