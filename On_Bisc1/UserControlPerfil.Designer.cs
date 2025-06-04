namespace On_Bisc1
{
    partial class userControlPerfil
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.PictureBoxPerfil = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.LabelBairro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.LabelNomeCompleto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.LabelProvinciaMuni = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.BotãoEditarPerfil = new Guna.UI2.WinForms.Guna2Button();
            this.LabelIDPrestador = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.LabelOficio = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnSobre = new Guna.UI2.WinForms.Guna2Button();
            this.btnPublicacoes = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderRadius = 60;
            this.guna2Panel1.Controls.Add(this.PictureBoxPerfil);
            this.guna2Panel1.Controls.Add(this.LabelBairro);
            this.guna2Panel1.Controls.Add(this.LabelNomeCompleto);
            this.guna2Panel1.Controls.Add(this.LabelProvinciaMuni);
            this.guna2Panel1.Controls.Add(this.BotãoEditarPerfil);
            this.guna2Panel1.Controls.Add(this.LabelIDPrestador);
            this.guna2Panel1.Controls.Add(this.LabelOficio);
            this.guna2Panel1.ForeColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(15, 7);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(861, 88);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // PictureBoxPerfil
            // 
            this.PictureBoxPerfil.BackColor = System.Drawing.Color.Transparent;
            this.PictureBoxPerfil.FillColor = System.Drawing.Color.Black;
            this.PictureBoxPerfil.ImageRotate = 0F;
            this.PictureBoxPerfil.Location = new System.Drawing.Point(26, 5);
            this.PictureBoxPerfil.Name = "PictureBoxPerfil";
            this.PictureBoxPerfil.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.PictureBoxPerfil.Size = new System.Drawing.Size(83, 79);
            this.PictureBoxPerfil.TabIndex = 1;
            this.PictureBoxPerfil.TabStop = false;
            this.PictureBoxPerfil.Click += new System.EventHandler(this.PictureBoxPerfil_Click);
            // 
            // LabelBairro
            // 
            this.LabelBairro.AutoSize = false;
            this.LabelBairro.BackColor = System.Drawing.Color.Transparent;
            this.LabelBairro.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.LabelBairro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.LabelBairro.Location = new System.Drawing.Point(406, 36);
            this.LabelBairro.Name = "LabelBairro";
            this.LabelBairro.Size = new System.Drawing.Size(224, 15);
            this.LabelBairro.TabIndex = 8;
            this.LabelBairro.Text = "Futungo";
            // 
            // LabelNomeCompleto
            // 
            this.LabelNomeCompleto.AutoSize = false;
            this.LabelNomeCompleto.BackColor = System.Drawing.Color.Transparent;
            this.LabelNomeCompleto.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LabelNomeCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.LabelNomeCompleto.Location = new System.Drawing.Point(111, 16);
            this.LabelNomeCompleto.Name = "LabelNomeCompleto";
            this.LabelNomeCompleto.Size = new System.Drawing.Size(240, 21);
            this.LabelNomeCompleto.TabIndex = 2;
            this.LabelNomeCompleto.Text = "Weza Mahula";
            // 
            // LabelProvinciaMuni
            // 
            this.LabelProvinciaMuni.AutoSize = false;
            this.LabelProvinciaMuni.BackColor = System.Drawing.Color.Transparent;
            this.LabelProvinciaMuni.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LabelProvinciaMuni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.LabelProvinciaMuni.Location = new System.Drawing.Point(406, 13);
            this.LabelProvinciaMuni.Name = "LabelProvinciaMuni";
            this.LabelProvinciaMuni.Size = new System.Drawing.Size(224, 17);
            this.LabelProvinciaMuni.TabIndex = 7;
            this.LabelProvinciaMuni.Text = "Luanda - Talatona";
            // 
            // BotãoEditarPerfil
            // 
            this.BotãoEditarPerfil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BotãoEditarPerfil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BotãoEditarPerfil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BotãoEditarPerfil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BotãoEditarPerfil.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BotãoEditarPerfil.ForeColor = System.Drawing.Color.White;
            this.BotãoEditarPerfil.Location = new System.Drawing.Point(514, 64);
            this.BotãoEditarPerfil.Name = "BotãoEditarPerfil";
            this.BotãoEditarPerfil.Size = new System.Drawing.Size(118, 23);
            this.BotãoEditarPerfil.TabIndex = 5;
            this.BotãoEditarPerfil.Text = "Editar Perfil";
            this.BotãoEditarPerfil.Click += new System.EventHandler(this.BotãoEditarPerfil_Click);
            // 
            // LabelIDPrestador
            // 
            this.LabelIDPrestador.AutoSize = false;
            this.LabelIDPrestador.BackColor = System.Drawing.Color.Transparent;
            this.LabelIDPrestador.Font = new System.Drawing.Font("Segoe UI", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.LabelIDPrestador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.LabelIDPrestador.Location = new System.Drawing.Point(110, 61);
            this.LabelIDPrestador.Name = "LabelIDPrestador";
            this.LabelIDPrestador.Size = new System.Drawing.Size(276, 22);
            this.LabelIDPrestador.TabIndex = 4;
            this.LabelIDPrestador.Text = "Prestador ID: 10";
            // 
            // LabelOficio
            // 
            this.LabelOficio.AutoSize = false;
            this.LabelOficio.BackColor = System.Drawing.Color.Transparent;
            this.LabelOficio.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LabelOficio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.LabelOficio.Location = new System.Drawing.Point(113, 40);
            this.LabelOficio.Name = "LabelOficio";
            this.LabelOficio.Size = new System.Drawing.Size(203, 17);
            this.LabelOficio.TabIndex = 3;
            this.LabelOficio.Text = "Eletricista";
            // 
            // btnSobre
            // 
            this.btnSobre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSobre.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSobre.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSobre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSobre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSobre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSobre.ForeColor = System.Drawing.Color.Black;
            this.btnSobre.Location = new System.Drawing.Point(135, 95);
            this.btnSobre.Name = "btnSobre";
            this.btnSobre.Size = new System.Drawing.Size(163, 25);
            this.btnSobre.TabIndex = 1;
            this.btnSobre.Text = "Sobre";
            this.btnSobre.Click += new System.EventHandler(this.btnSobre_Click);
            // 
            // btnPublicacoes
            // 
            this.btnPublicacoes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPublicacoes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPublicacoes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPublicacoes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPublicacoes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPublicacoes.ForeColor = System.Drawing.Color.Black;
            this.btnPublicacoes.Location = new System.Drawing.Point(400, 97);
            this.btnPublicacoes.Name = "btnPublicacoes";
            this.btnPublicacoes.Size = new System.Drawing.Size(166, 23);
            this.btnPublicacoes.TabIndex = 2;
            this.btnPublicacoes.Text = "Publicações";
            this.btnPublicacoes.Click += new System.EventHandler(this.btnPublicacoes_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel2.Location = new System.Drawing.Point(15, 95);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(861, 26);
            this.guna2Panel2.TabIndex = 3;
            // 
            // userControlPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(139)))));
            this.Controls.Add(this.btnPublicacoes);
            this.Controls.Add(this.btnSobre);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Panel2);
            this.Name = "userControlPerfil";
            this.Size = new System.Drawing.Size(920, 603);
            this.Load += new System.EventHandler(this.userControlPerfil_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPerfil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CirclePictureBox PictureBoxPerfil;
        private Guna.UI2.WinForms.Guna2Button BotãoEditarPerfil;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelIDPrestador;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelOficio;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelNomeCompleto;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelProvinciaMuni;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelBairro;
        private Guna.UI2.WinForms.Guna2Button btnSobre;
        private Guna.UI2.WinForms.Guna2Button btnPublicacoes;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
    }
}
