﻿
namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    partial class frmSelecaoRoots
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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("pk column: valor");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("row", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Tabela", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.grpTabelasOrigem = new System.Windows.Forms.GroupBox();
            this.lstTabelas = new System.Windows.Forms.ListBox();
            this.txtFiltroTabela = new System.Windows.Forms.TextBox();
            this.grpRegistrosCopiar = new System.Windows.Forms.GroupBox();
            this.txtColumnValue = new System.Windows.Forms.TextBox();
            this.tvwRegistros = new System.Windows.Forms.TreeView();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpTabelasOrigem.SuspendLayout();
            this.grpRegistrosCopiar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTabelasOrigem
            // 
            this.grpTabelasOrigem.Controls.Add(this.lstTabelas);
            this.grpTabelasOrigem.Controls.Add(this.txtFiltroTabela);
            this.grpTabelasOrigem.Location = new System.Drawing.Point(50, 70);
            this.grpTabelasOrigem.Name = "grpTabelasOrigem";
            this.grpTabelasOrigem.Size = new System.Drawing.Size(243, 280);
            this.grpTabelasOrigem.TabIndex = 0;
            this.grpTabelasOrigem.TabStop = false;
            this.grpTabelasOrigem.Text = "Tabelas (na Origem)";
            // 
            // lstTabelas
            // 
            this.lstTabelas.FormattingEnabled = true;
            this.lstTabelas.ItemHeight = 15;
            this.lstTabelas.Location = new System.Drawing.Point(56, 105);
            this.lstTabelas.Name = "lstTabelas";
            this.lstTabelas.Size = new System.Drawing.Size(120, 94);
            this.lstTabelas.TabIndex = 6;
            this.lstTabelas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTabelas_MouseDoubleClick);
            // 
            // txtFiltroTabela
            // 
            this.txtFiltroTabela.Location = new System.Drawing.Point(39, 36);
            this.txtFiltroTabela.Name = "txtFiltroTabela";
            this.txtFiltroTabela.Size = new System.Drawing.Size(91, 23);
            this.txtFiltroTabela.TabIndex = 5;
            this.txtFiltroTabela.TextChanged += new System.EventHandler(this.txtFiltroTabela_TextChanged);
            // 
            // grpRegistrosCopiar
            // 
            this.grpRegistrosCopiar.Controls.Add(this.txtColumnValue);
            this.grpRegistrosCopiar.Controls.Add(this.tvwRegistros);
            this.grpRegistrosCopiar.Location = new System.Drawing.Point(390, 85);
            this.grpRegistrosCopiar.Name = "grpRegistrosCopiar";
            this.grpRegistrosCopiar.Size = new System.Drawing.Size(314, 248);
            this.grpRegistrosCopiar.TabIndex = 2;
            this.grpRegistrosCopiar.TabStop = false;
            this.grpRegistrosCopiar.Text = "Registros para Copiar";
            // 
            // txtColumnValue
            // 
            this.txtColumnValue.Location = new System.Drawing.Point(68, 45);
            this.txtColumnValue.Name = "txtColumnValue";
            this.txtColumnValue.Size = new System.Drawing.Size(91, 23);
            this.txtColumnValue.TabIndex = 6;
            this.txtColumnValue.VisibleChanged += new System.EventHandler(this.txtColumnValue_VisibleChanged);
            this.txtColumnValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtColumnValue_KeyDown);
            this.txtColumnValue.Leave += new System.EventHandler(this.txtColumnValue_Leave);
            this.txtColumnValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtColumnValue_Validating);
            // 
            // tvwRegistros
            // 
            this.tvwRegistros.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvwRegistros.LabelEdit = true;
            this.tvwRegistros.Location = new System.Drawing.Point(47, 90);
            this.tvwRegistros.Name = "tvwRegistros";
            treeNode4.Checked = true;
            treeNode4.Name = "Node2";
            treeNode4.Text = "pk column: valor";
            treeNode5.Name = "Node1";
            treeNode5.Text = "row";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Tabela";
            this.tvwRegistros.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.tvwRegistros.Size = new System.Drawing.Size(217, 123);
            this.tvwRegistros.TabIndex = 0;
            this.tvwRegistros.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvwRegistros_AfterLabelEdit);
            this.tvwRegistros.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwRegistros_NodeMouseDoubleClick);
            this.tvwRegistros.Resize += new System.EventHandler(this.tvwRegistros_Resize);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFechar.Location = new System.Drawing.Point(723, 435);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(98, 35);
            this.btnFechar.TabIndex = 8;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.Location = new System.Drawing.Point(597, 423);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(98, 35);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSelecaoRoots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(819, 470);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpRegistrosCopiar);
            this.Controls.Add(this.grpTabelasOrigem);
            this.Name = "frmSelecaoRoots";
            this.Text = "frmSelecaoRoots";
            this.Activated += new System.EventHandler(this.frmSelecaoRoots_Activated);
            this.Load += new System.EventHandler(this.frmSelecaoRoots_Load);
            this.Resize += new System.EventHandler(this.frmSelecaoRoots_Resize);
            this.grpTabelasOrigem.ResumeLayout(false);
            this.grpTabelasOrigem.PerformLayout();
            this.grpRegistrosCopiar.ResumeLayout(false);
            this.grpRegistrosCopiar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTabelasOrigem;
        private System.Windows.Forms.GroupBox grpRegistrosCopiar;
        private System.Windows.Forms.TextBox txtFiltroTabela;
        private System.Windows.Forms.ListBox lstTabelas;
        private System.Windows.Forms.TreeView tvwRegistros;
        private System.Windows.Forms.TextBox txtColumnValue;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnOk;
    }
}