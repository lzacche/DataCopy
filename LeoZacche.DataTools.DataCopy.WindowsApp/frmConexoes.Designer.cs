
namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    partial class frmConexoes
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
            this.ucConnDestino = new LeoZacche.DataTools.DataCopy.WindowsApp.ucConexao();
            this.ucConnOrigem = new LeoZacche.DataTools.DataCopy.WindowsApp.ucConexao();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucConnDestino
            // 
            this.ucConnDestino.DataConnection = null;
            this.ucConnDestino.Location = new System.Drawing.Point(468, 12);
            this.ucConnDestino.Name = "ucConnDestino";
            this.ucConnDestino.Size = new System.Drawing.Size(439, 426);
            this.ucConnDestino.TabIndex = 3;
            this.ucConnDestino.Text = "Conexão de Destino";
            // 
            // ucConnOrigem
            // 
            this.ucConnOrigem.DataConnection = null;
            this.ucConnOrigem.Location = new System.Drawing.Point(12, 12);
            this.ucConnOrigem.Name = "ucConnOrigem";
            this.ucConnOrigem.Size = new System.Drawing.Size(439, 426);
            this.ucConnOrigem.TabIndex = 4;
            this.ucConnOrigem.Text = "Conexão de Origem";
            this.ucConnOrigem.Test_Started += new System.EventHandler(this.ucConnOrigem_Test_Started);
            this.ucConnOrigem.Test_Ended += new System.EventHandler(this.ucConnOrigem_Test_Ended);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(704, 450);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(98, 35);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.Location = new System.Drawing.Point(808, 450);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(98, 35);
            this.btnFechar.TabIndex = 6;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmConexoes
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(918, 492);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.ucConnOrigem);
            this.Controls.Add(this.ucConnDestino);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConexoes";
            this.Text = "frmConexoes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConexoes_FormClosing);
            this.Load += new System.EventHandler(this.frmConexoes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucConexao ucConnDestino;
        private ucConexao ucConnOrigem;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnFechar;
    }
}