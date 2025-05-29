namespace On_Bisc1
{
    partial class FormFotoGrande
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxGrande = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrande)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGrande
            // 
            this.pictureBoxGrande.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGrande.ImageRotate = 0F;
            this.pictureBoxGrande.Location = new System.Drawing.Point(42, 47);
            this.pictureBoxGrande.Name = "pictureBoxGrande";
            this.pictureBoxGrande.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pictureBoxGrande.Size = new System.Drawing.Size(232, 242);
            this.pictureBoxGrande.TabIndex = 0;
            this.pictureBoxGrande.TabStop = false;
            this.pictureBoxGrande.Click += new System.EventHandler(this.pictureBoxGrande_Click);
            // 
            // FormFotoGrande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(323, 337);
            this.Controls.Add(this.pictureBoxGrande);
            this.Name = "FormFotoGrande";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.pictureBoxGrande_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrande)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox pictureBoxGrande;
    }
}