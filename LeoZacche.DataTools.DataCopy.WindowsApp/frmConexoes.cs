using LeoZacche.DataTools.DataCopy.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    public partial class frmConexoes : Form
    {
        private DataConnection connSource = null;
        private DataConnection connDestination = null;
        //private bool Ok = false;


        public frmConexoes()
        {
            InitializeComponent();
        }

        public frmConexoes(DataConnection connSource, DataConnection connDestination) : this()
        {
            //ucConnDestino.Dock = DockStyle.Fill;

            this.connSource = connSource;
            this.connDestination = connDestination;

            //this.ucConnOrigem.Name = "ucConnOrigem";
            this.ucConnOrigem.DataConnection = this.connSource;

            //this.ucConnDestino.Name = "ucConnOrigem";
            this.ucConnDestino.DataConnection = this.connDestination;

            /*
            //this.splitContainer.Panel1.Controls.Remove(this.ucConnOrigem);
            this.ucConnOrigem.Dispose();
            this.ucConnOrigem = new LeoZacche.DataTools.DataCopy.WindowsApp.ucConexao(this.connSource);
            //this.splitContainer.Panel1.Controls.Add(this.ucConnOrigem);
            this.Controls.Add(this.ucConnOrigem);

            this.ucConnOrigem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucConnOrigem.Location = new System.Drawing.Point(10, 10);
            this.ucConnOrigem.Name = "ucConnOrigem";
            this.ucConnOrigem.Size = new System.Drawing.Size(428, 558);
            this.ucConnOrigem.TabIndex = 0;



            //this.splitContainer.Panel2.Controls.Remove(this.ucConnDestino);
            this.ucConnDestino.Dispose();
            this.ucConnDestino = new LeoZacche.DataTools.DataCopy.WindowsApp.ucConexao(this.connDestination);
            //this.splitContainer.Panel2.Controls.Add(this.ucConnDestino);
            this.Controls.Add(this.ucConnDestino);


            this.ucConnDestino.Location = new System.Drawing.Point(73, 83);
            this.ucConnDestino.Name = "ucConnDestino";
            this.ucConnDestino.Size = new System.Drawing.Size(470, 463);
            this.ucConnDestino.TabIndex = 0;
            */


        }



        private void ucConnOrigem_Test_Started(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
        }
        private void ucConnOrigem_Test_Ended(object sender, EventArgs e)
        {
            this.UseWaitCursor = false;
        }

        private void frmConexoes_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //this.Ok = true;
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.Ok = false;
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void frmConexoes_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.Ok)
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.connSource.State != ConnectionState.Open)
                {
                    MessageBox.Show("A conexão de origem não está aberta!", "Conexão de Origem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                if (this.connDestination.State != ConnectionState.Open)
                {
                    MessageBox.Show("A conexão de destino não está aberta!", "Conexão de Destino", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }

                if (e.Cancel)
                    this.Show();
            }

        }


    }
}
