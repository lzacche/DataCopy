using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Engine;
using LeoZacche.DataTools.DataCopy.Engine.Extensions;

namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    public partial class frmMain : Form
    {
        private DataCopySession theSession = null;
        private frmConexoes formConexoes = null;
        private frmSelecaoRoots formRoots = null;

        public frmMain()
        {
            InitializeComponent();
        }

        #region Eventos do Form

        private void frmMain_Load(object sender, EventArgs e)
        {
            createNew();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            // separador 2 larguras
            // botao 1
            // separador 1 largura
            // botao 2
            // separador 1 largura
            // botao 3
            // separador 1 largura
            // botao 4
            // separador 1 largura
            // botao 5
            // separador 2 largura

            // total de larguras: 8, sendo as extremidades, duplas; as entre botões, simples.
            // total de botões: 5

            int larguraTotalBruta = this.ClientSize.Width;
            decimal descontoTotalLargura = 0.2m; // 20%
            int larguraTotalDescontos = Convert.ToInt32(Math.Round(larguraTotalBruta * descontoTotalLargura, 0));
            int larguraTotalUtil = larguraTotalBruta - larguraTotalDescontos;

            int umaLarguraSeparador = larguraTotalDescontos / 8; // total: 8 larguras
            int larguraBotao = larguraTotalUtil / 5; // total: 5 botoes

            // posicoes horizontais
            btnPasso1.Left = umaLarguraSeparador * 2;
            btnPasso1.Width = larguraBotao;

            btnPasso2.Left = btnPasso1.Right + umaLarguraSeparador;
            btnPasso2.Width = larguraBotao;

            btnPasso3.Left = btnPasso2.Right + umaLarguraSeparador;
            btnPasso3.Width = larguraBotao;

            btnPasso4.Left = btnPasso3.Right + umaLarguraSeparador;
            btnPasso4.Width = larguraBotao;

            btnPasso5.Left = btnPasso4.Right + umaLarguraSeparador;
            btnPasso5.Width = larguraBotao;
        }
        
        #endregion

        #region Eventos do Menu Arquivo

        private void mnuArquivoSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region Eventos dos Botões

        private void btnPasso1_Click(object sender, EventArgs e)
        {
            var frmConnEditor = new frmConexoes(this.theSession.ConnectionSource, this.theSession.ConnectionDestination);
            frmConnEditor.ShowDialog();

            frmConnEditor.Dispose();

            this.btnPasso2.Enabled = this.theSession.ConnectionSource.State == ConnectionState.Open;
        }

        private void btnPasso2_Click(object sender, EventArgs e)
        {
            if (this.theSession.ConnectionSource.DatabaseOrSchema == null)
            {
                var databaseOrSchemaTitle = this.theSession.ConnectionSource.DatabaseOrSchemaTitle;
                MessageBox.Show($"{databaseOrSchemaTitle} não foi selecionado na origem.", "Conexão de Origem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var frmRootsEditor = new frmSelecaoRoots(this.theSession.ConnectionSource, this.theSession.TablesToCopy);
            var resultEdicao = frmRootsEditor.ShowDialog();

            if (resultEdicao == DialogResult.OK)
            {
                this.theSession.TablesToCopy.Clear();
                this.theSession.TablesToCopy.CloneFrom(frmRootsEditor.TablesToCopy);
            }

            frmRootsEditor.Dispose();

            bool anyTableSelected = this.theSession.TablesToCopy.Any();
            btnPasso3.Enabled = anyTableSelected;
            btnPasso4.Enabled = anyTableSelected;
            btnPasso5.Enabled = anyTableSelected;
        }

        #endregion


        private void createNew()
        {
            this.theSession = new DataCopySession();

            #region Para Ajudar no DEBUG/DESENV

            this.theSession.ConnectionSource.ConnectionType = ConnectionTypeEnum.MicrosoftSqlServer;
            this.theSession.ConnectionSource.Server = @"SFDATDB05\DATALAKE";
            this.theSession.ConnectionSource.Server = @"localhost\SqlExpress2014";
            this.theSession.ConnectionSource.Authentication = Contracts.ConnectionAuthenticationEnum.WindowsIntegrated;

            this.theSession.ConnectionDestination.ConnectionType = ConnectionTypeEnum.MicrosoftSqlServer;
            this.theSession.ConnectionDestination.Server = @"HSVSQL04\ESIM";
            this.theSession.ConnectionDestination.Server = @"localhost\Sql2017Developer";
            this.theSession.ConnectionDestination.Authentication = Contracts.ConnectionAuthenticationEnum.WindowsIntegrated;

            #endregion


            //this.formConexoes = new frmConexoes(this.theSession.ConnectionSource, this.theSession.ConnectionDestination);
            //this.formConexoes.MdiParent = this;
            //this.formConexoes.Show();

            //this.formRoots = new frmSelecaoRoots();
            //this.formRoots.Show();

            btnPasso5.Enabled = false;
            btnPasso4.Enabled = false;
            btnPasso3.Enabled = false;
            btnPasso2.Enabled = false;
            btnPasso1.Enabled = true;
        }

        private void btnPasso5_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;

                this.theSession.OnPreCheckStarted += TheSession_OnPreCheckStarted;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Executing...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.theSession.OnPreCheckStarted -= TheSession_OnPreCheckStarted;
                this.UseWaitCursor = false;
            }
        }

        private void TheSession_OnPreCheckStarted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
