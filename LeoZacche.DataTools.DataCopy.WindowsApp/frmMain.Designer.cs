
namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArquivoSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuArquivoSair = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProcesso = new System.Windows.Forms.ToolStripMenuItem();
            this.passo1ConexõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passo2SeleçãoRootsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passo3DependênciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passo4ComposiçõesregistrosDescendentesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passo5ExecuçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPasso1 = new System.Windows.Forms.Button();
            this.btnPasso2 = new System.Windows.Forms.Button();
            this.btnPasso3 = new System.Windows.Forms.Button();
            this.btnPasso4 = new System.Windows.Forms.Button();
            this.btnPasso5 = new System.Windows.Forms.Button();
            this.tabSteps = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.mnuStrip.SuspendLayout();
            this.tabSteps.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuStrip
            // 
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArquivo,
            this.mnuProcesso});
            this.mnuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(994, 24);
            this.mnuStrip.TabIndex = 0;
            this.mnuStrip.Text = "menuStrip1";
            // 
            // mnuArquivo
            // 
            this.mnuArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArquivoNovo,
            this.mnuArquivoAbrir,
            this.mnuArquivoSalvar,
            this.mnuArquivoSep1,
            this.mnuArquivoSair});
            this.mnuArquivo.Name = "mnuArquivo";
            this.mnuArquivo.Size = new System.Drawing.Size(61, 20);
            this.mnuArquivo.Text = "&Arquivo";
            // 
            // mnuArquivoNovo
            // 
            this.mnuArquivoNovo.Name = "mnuArquivoNovo";
            this.mnuArquivoNovo.Size = new System.Drawing.Size(208, 22);
            this.mnuArquivoNovo.Text = "&Nova Definição de Cópia";
            // 
            // mnuArquivoAbrir
            // 
            this.mnuArquivoAbrir.Name = "mnuArquivoAbrir";
            this.mnuArquivoAbrir.Size = new System.Drawing.Size(208, 22);
            this.mnuArquivoAbrir.Text = "&Abrir Definição de Cópia";
            // 
            // mnuArquivoSalvar
            // 
            this.mnuArquivoSalvar.Name = "mnuArquivoSalvar";
            this.mnuArquivoSalvar.Size = new System.Drawing.Size(208, 22);
            this.mnuArquivoSalvar.Text = "&Salvar Definição de Cópia";
            // 
            // mnuArquivoSep1
            // 
            this.mnuArquivoSep1.Name = "mnuArquivoSep1";
            this.mnuArquivoSep1.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuArquivoSair
            // 
            this.mnuArquivoSair.Name = "mnuArquivoSair";
            this.mnuArquivoSair.Size = new System.Drawing.Size(208, 22);
            this.mnuArquivoSair.Text = "&Sair";
            this.mnuArquivoSair.Click += new System.EventHandler(this.mnuArquivoSair_Click);
            // 
            // mnuProcesso
            // 
            this.mnuProcesso.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passo1ConexõesToolStripMenuItem,
            this.passo2SeleçãoRootsToolStripMenuItem,
            this.passo3DependênciasToolStripMenuItem,
            this.passo4ComposiçõesregistrosDescendentesToolStripMenuItem,
            this.passo5ExecuçãoToolStripMenuItem});
            this.mnuProcesso.Name = "mnuProcesso";
            this.mnuProcesso.Size = new System.Drawing.Size(66, 20);
            this.mnuProcesso.Text = "&Processo";
            // 
            // passo1ConexõesToolStripMenuItem
            // 
            this.passo1ConexõesToolStripMenuItem.Name = "passo1ConexõesToolStripMenuItem";
            this.passo1ConexõesToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.passo1ConexõesToolStripMenuItem.Text = "Passo &1 - Conexões";
            // 
            // passo2SeleçãoRootsToolStripMenuItem
            // 
            this.passo2SeleçãoRootsToolStripMenuItem.Name = "passo2SeleçãoRootsToolStripMenuItem";
            this.passo2SeleçãoRootsToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.passo2SeleçãoRootsToolStripMenuItem.Text = "Passo &2 - Seleção Roots (registros raízes)";
            // 
            // passo3DependênciasToolStripMenuItem
            // 
            this.passo3DependênciasToolStripMenuItem.Name = "passo3DependênciasToolStripMenuItem";
            this.passo3DependênciasToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.passo3DependênciasToolStripMenuItem.Text = "Passo &3 - Dependências (registros ascendentes)";
            // 
            // passo4ComposiçõesregistrosDescendentesToolStripMenuItem
            // 
            this.passo4ComposiçõesregistrosDescendentesToolStripMenuItem.Name = "passo4ComposiçõesregistrosDescendentesToolStripMenuItem";
            this.passo4ComposiçõesregistrosDescendentesToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.passo4ComposiçõesregistrosDescendentesToolStripMenuItem.Text = "Passo &4 - Composições (registros descendentes)";
            // 
            // passo5ExecuçãoToolStripMenuItem
            // 
            this.passo5ExecuçãoToolStripMenuItem.Name = "passo5ExecuçãoToolStripMenuItem";
            this.passo5ExecuçãoToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.passo5ExecuçãoToolStripMenuItem.Text = "Passo &5 - Execução";
            // 
            // btnPasso1
            // 
            this.btnPasso1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasso1.Location = new System.Drawing.Point(51, 133);
            this.btnPasso1.Name = "btnPasso1";
            this.btnPasso1.Size = new System.Drawing.Size(128, 67);
            this.btnPasso1.TabIndex = 2;
            this.btnPasso1.Text = "Passo 1 Conexões";
            this.btnPasso1.UseVisualStyleBackColor = true;
            this.btnPasso1.Click += new System.EventHandler(this.btnPasso1_Click);
            // 
            // btnPasso2
            // 
            this.btnPasso2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasso2.Location = new System.Drawing.Point(201, 160);
            this.btnPasso2.Name = "btnPasso2";
            this.btnPasso2.Size = new System.Drawing.Size(128, 67);
            this.btnPasso2.TabIndex = 3;
            this.btnPasso2.Text = "Passo 2 Seleção de Roots";
            this.btnPasso2.UseVisualStyleBackColor = true;
            this.btnPasso2.Click += new System.EventHandler(this.btnPasso2_Click);
            // 
            // btnPasso3
            // 
            this.btnPasso3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasso3.Location = new System.Drawing.Point(352, 192);
            this.btnPasso3.Name = "btnPasso3";
            this.btnPasso3.Size = new System.Drawing.Size(128, 67);
            this.btnPasso3.TabIndex = 4;
            this.btnPasso3.Text = "Passo 3 Dependências";
            this.btnPasso3.UseVisualStyleBackColor = true;
            // 
            // btnPasso4
            // 
            this.btnPasso4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasso4.Location = new System.Drawing.Point(509, 160);
            this.btnPasso4.Name = "btnPasso4";
            this.btnPasso4.Size = new System.Drawing.Size(128, 67);
            this.btnPasso4.TabIndex = 5;
            this.btnPasso4.Text = "Passo 4 Composições";
            this.btnPasso4.UseVisualStyleBackColor = true;
            // 
            // btnPasso5
            // 
            this.btnPasso5.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasso5.Location = new System.Drawing.Point(679, 239);
            this.btnPasso5.Name = "btnPasso5";
            this.btnPasso5.Size = new System.Drawing.Size(128, 67);
            this.btnPasso5.TabIndex = 6;
            this.btnPasso5.Text = "Passo 5 Execução";
            this.btnPasso5.UseVisualStyleBackColor = true;
            this.btnPasso5.Click += new System.EventHandler(this.btnPasso5_Click);
            // 
            // tabSteps
            // 
            this.tabSteps.Controls.Add(this.tabPage1);
            this.tabSteps.Controls.Add(this.tabPage2);
            this.tabSteps.Controls.Add(this.tabPage3);
            this.tabSteps.Controls.Add(this.tabPage4);
            this.tabSteps.Controls.Add(this.tabPage5);
            this.tabSteps.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabSteps.ItemSize = new System.Drawing.Size(61, 40);
            this.tabSteps.Location = new System.Drawing.Point(0, 24);
            this.tabSteps.Name = "tabSteps";
            this.tabSteps.SelectedIndex = 0;
            this.tabSteps.Size = new System.Drawing.Size(994, 100);
            this.tabSteps.TabIndex = 7;
            this.tabSteps.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 44);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(986, 52);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Passo 1 - Conexões";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 44);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(986, 52);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Passo 2 - Seleção de Roots";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 44);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(986, 52);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Passo 3 - Dependências (Registros Ascententes)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 44);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(986, 52);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Passo 4 - Composição (registros Descententes)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 44);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(986, 52);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Passo 5 - Execução";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 651);
            this.Controls.Add(this.tabSteps);
            this.Controls.Add(this.btnPasso5);
            this.Controls.Add(this.btnPasso4);
            this.Controls.Add(this.btnPasso3);
            this.Controls.Add(this.btnPasso2);
            this.Controls.Add(this.btnPasso1);
            this.Controls.Add(this.mnuStrip);
            this.MainMenuStrip = this.mnuStrip;
            this.Name = "frmMain";
            this.Text = "Data Copy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.tabSteps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivo;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoSair;
        private System.Windows.Forms.ToolStripSeparator mnuArquivoSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuProcesso;
        private System.Windows.Forms.ToolStripMenuItem passo1ConexõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passo2SeleçãoRootsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passo3DependênciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passo4ComposiçõesregistrosDescendentesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passo5ExecuçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoNovo;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoAbrir;
        private System.Windows.Forms.ToolStripMenuItem mnuArquivoSalvar;
        private System.Windows.Forms.Button btnPasso1;
        private System.Windows.Forms.Button btnPasso2;
        private System.Windows.Forms.Button btnPasso3;
        private System.Windows.Forms.Button btnPasso4;
        private System.Windows.Forms.Button btnPasso5;
        private System.Windows.Forms.TabControl tabSteps;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
    }
}

