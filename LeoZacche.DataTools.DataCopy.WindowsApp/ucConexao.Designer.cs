
namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    partial class ucConexao
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpControles = new System.Windows.Forms.GroupBox();
            this.btnAbrirFechar = new System.Windows.Forms.Button();
            this.btnTestar = new System.Windows.Forms.Button();
            this.grpSeparator = new System.Windows.Forms.GroupBox();
            this.cboDatabseOrSchema = new System.Windows.Forms.ComboBox();
            this.lblDatabaseOrSchema = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.rbAuthNomeada = new System.Windows.Forms.RadioButton();
            this.rbAuthIntegrada = new System.Windows.Forms.RadioButton();
            this.lblAutenticacao = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.cboTipoConexao = new System.Windows.Forms.ComboBox();
            this.lblTipoConexao = new System.Windows.Forms.Label();
            this.grpControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControles
            // 
            this.grpControles.Controls.Add(this.btnAbrirFechar);
            this.grpControles.Controls.Add(this.btnTestar);
            this.grpControles.Controls.Add(this.grpSeparator);
            this.grpControles.Controls.Add(this.cboDatabseOrSchema);
            this.grpControles.Controls.Add(this.lblDatabaseOrSchema);
            this.grpControles.Controls.Add(this.txtSenha);
            this.grpControles.Controls.Add(this.lblSenha);
            this.grpControles.Controls.Add(this.txtUsuario);
            this.grpControles.Controls.Add(this.lblUsuario);
            this.grpControles.Controls.Add(this.rbAuthNomeada);
            this.grpControles.Controls.Add(this.rbAuthIntegrada);
            this.grpControles.Controls.Add(this.lblAutenticacao);
            this.grpControles.Controls.Add(this.txtServidor);
            this.grpControles.Controls.Add(this.lblServidor);
            this.grpControles.Controls.Add(this.cboTipoConexao);
            this.grpControles.Controls.Add(this.lblTipoConexao);
            this.grpControles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControles.Location = new System.Drawing.Point(0, 0);
            this.grpControles.Name = "grpControles";
            this.grpControles.Size = new System.Drawing.Size(470, 463);
            this.grpControles.TabIndex = 1;
            this.grpControles.TabStop = false;
            this.grpControles.Text = "Conexão de Origem";
            // 
            // btnAbrirFechar
            // 
            this.btnAbrirFechar.Location = new System.Drawing.Point(155, 361);
            this.btnAbrirFechar.Name = "btnAbrirFechar";
            this.btnAbrirFechar.Size = new System.Drawing.Size(179, 40);
            this.btnAbrirFechar.TabIndex = 15;
            this.btnAbrirFechar.Text = "Conectar / Desconectar";
            this.btnAbrirFechar.UseVisualStyleBackColor = true;
            this.btnAbrirFechar.Click += new System.EventHandler(this.btnAbrirFechar_Click);
            // 
            // btnTestar
            // 
            this.btnTestar.Location = new System.Drawing.Point(155, 305);
            this.btnTestar.Name = "btnTestar";
            this.btnTestar.Size = new System.Drawing.Size(179, 40);
            this.btnTestar.TabIndex = 14;
            this.btnTestar.Text = "Testar Conexão";
            this.btnTestar.UseVisualStyleBackColor = true;
            this.btnTestar.Click += new System.EventHandler(this.btnTestar_Click);
            // 
            // grpSeparator
            // 
            this.grpSeparator.Location = new System.Drawing.Point(25, 269);
            this.grpSeparator.Name = "grpSeparator";
            this.grpSeparator.Size = new System.Drawing.Size(398, 11);
            this.grpSeparator.TabIndex = 13;
            this.grpSeparator.TabStop = false;
            // 
            // cboDatabseOrSchema
            // 
            this.cboDatabseOrSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabseOrSchema.FormattingEnabled = true;
            this.cboDatabseOrSchema.Items.AddRange(new object[] {
            "Microsoft SQL Server",
            "Oracle Database Server",
            "Oracle MySQL",
            "Maria DB",
            "PostgreSQL"});
            this.cboDatabseOrSchema.Location = new System.Drawing.Point(155, 225);
            this.cboDatabseOrSchema.Name = "cboDatabseOrSchema";
            this.cboDatabseOrSchema.Size = new System.Drawing.Size(268, 23);
            this.cboDatabseOrSchema.TabIndex = 12;
            this.cboDatabseOrSchema.SelectedIndexChanged += new System.EventHandler(this.cboDatabseOrSchema_SelectedIndexChanged);
            // 
            // lblDatabaseOrSchema
            // 
            this.lblDatabaseOrSchema.AutoSize = true;
            this.lblDatabaseOrSchema.Location = new System.Drawing.Point(25, 228);
            this.lblDatabaseOrSchema.Name = "lblDatabaseOrSchema";
            this.lblDatabaseOrSchema.Size = new System.Drawing.Size(105, 15);
            this.lblDatabaseOrSchema.TabIndex = 11;
            this.lblDatabaseOrSchema.Text = "Databse / Schema:";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(155, 183);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(268, 23);
            this.txtSenha.TabIndex = 10;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(25, 186);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(42, 15);
            this.lblSenha.TabIndex = 9;
            this.lblSenha.Text = "Senha:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(155, 145);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(268, 23);
            this.txtUsuario.TabIndex = 8;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(25, 148);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(50, 15);
            this.lblUsuario.TabIndex = 7;
            this.lblUsuario.Text = "Usuário:";
            // 
            // rbAuthNomeada
            // 
            this.rbAuthNomeada.AutoSize = true;
            this.rbAuthNomeada.Location = new System.Drawing.Point(346, 109);
            this.rbAuthNomeada.Name = "rbAuthNomeada";
            this.rbAuthNomeada.Size = new System.Drawing.Size(77, 19);
            this.rbAuthNomeada.TabIndex = 6;
            this.rbAuthNomeada.Text = "Nomeada";
            this.rbAuthNomeada.UseVisualStyleBackColor = true;
            // 
            // rbAuthIntegrada
            // 
            this.rbAuthIntegrada.AutoSize = true;
            this.rbAuthIntegrada.Location = new System.Drawing.Point(155, 109);
            this.rbAuthIntegrada.Name = "rbAuthIntegrada";
            this.rbAuthIntegrada.Size = new System.Drawing.Size(154, 19);
            this.rbAuthIntegrada.TabIndex = 5;
            this.rbAuthIntegrada.Text = "Integrada com Windows";
            this.rbAuthIntegrada.UseVisualStyleBackColor = true;
            this.rbAuthIntegrada.CheckedChanged += new System.EventHandler(this.rbAuthIntegrada_CheckedChanged);
            // 
            // lblAutenticacao
            // 
            this.lblAutenticacao.AutoSize = true;
            this.lblAutenticacao.Location = new System.Drawing.Point(25, 113);
            this.lblAutenticacao.Name = "lblAutenticacao";
            this.lblAutenticacao.Size = new System.Drawing.Size(80, 15);
            this.lblAutenticacao.TabIndex = 4;
            this.lblAutenticacao.Text = "Autenticação:";
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(155, 68);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(268, 23);
            this.txtServidor.TabIndex = 3;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(25, 71);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(111, 15);
            this.lblServidor.TabIndex = 2;
            this.lblServidor.Text = "Servidor / Instância:";
            // 
            // cboTipoConexao
            // 
            this.cboTipoConexao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoConexao.FormattingEnabled = true;
            this.cboTipoConexao.Items.AddRange(new object[] {
            "Microsoft SQL Server",
            "Oracle Database Server",
            "Oracle MySQL",
            "Maria DB",
            "PostgreSQL"});
            this.cboTipoConexao.Location = new System.Drawing.Point(155, 29);
            this.cboTipoConexao.Name = "cboTipoConexao";
            this.cboTipoConexao.Size = new System.Drawing.Size(268, 23);
            this.cboTipoConexao.TabIndex = 1;
            this.cboTipoConexao.SelectedIndexChanged += new System.EventHandler(this.cboTipoConexao_SelectedIndexChanged);
            // 
            // lblTipoConexao
            // 
            this.lblTipoConexao.AutoSize = true;
            this.lblTipoConexao.Location = new System.Drawing.Point(25, 32);
            this.lblTipoConexao.Name = "lblTipoConexao";
            this.lblTipoConexao.Size = new System.Drawing.Size(99, 15);
            this.lblTipoConexao.TabIndex = 0;
            this.lblTipoConexao.Text = "Tipo de Conexão:";
            // 
            // ucConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpControles);
            this.Name = "ucConexao";
            this.Size = new System.Drawing.Size(470, 463);
            this.Load += new System.EventHandler(this.ucConexao_Load);
            this.grpControles.ResumeLayout(false);
            this.grpControles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpControles;
        private System.Windows.Forms.ComboBox cboTipoConexao;
        private System.Windows.Forms.Label lblTipoConexao;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.ComboBox cboDatabseOrSchema;
        private System.Windows.Forms.Label lblDatabaseOrSchema;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.RadioButton rbAuthNomeada;
        private System.Windows.Forms.RadioButton rbAuthIntegrada;
        private System.Windows.Forms.Label lblAutenticacao;
        private System.Windows.Forms.Button btnAbrirFechar;
        private System.Windows.Forms.Button btnTestar;
        private System.Windows.Forms.GroupBox grpSeparator;
    }
}
