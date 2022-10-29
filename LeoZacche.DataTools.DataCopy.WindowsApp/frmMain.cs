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
using LeoZacche.DataTools.DataCopy.Contracts.Extensions;
using static LeoZacche.DataTools.DataCopy.Engine.DataCopySession;

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
            //grpExecucao.Visible = false;
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


            int margemHorizontal = 10;
            lblOverall.Left = margemHorizontal;
            lblCurrentTable.Left = margemHorizontal;
            lblDetalhes.Left = margemHorizontal;

            int margemVertical = 4;
            pbOverall.Top = 17 + margemVertical;
            pbCurrentTable.Top = pbOverall.Bottom + margemVertical;
            lblDetalhesContent.Top = pbCurrentTable.Bottom + margemVertical;


            grpExecucao.Height = 105;
            int largulaLabels = 120;

            lblOverall.Width = largulaLabels;
            lblCurrentTable.Width = largulaLabels;
            lblDetalhes.Width = largulaLabels;

            lblOverall.Height = pbOverall.Height;
            lblCurrentTable.Height = pbCurrentTable.Height;
            lblDetalhes.Height = lblDetalhesContent.Height;

            pbOverall.Left = margemHorizontal + largulaLabels + margemHorizontal;
            pbCurrentTable.Left = pbOverall.Left;
            lblDetalhesContent.Left = pbCurrentTable.Left;

            pbOverall.Width = grpExecucao.ClientSize.Width - pbOverall.Left - margemHorizontal;
            pbCurrentTable.Width = pbOverall.Width;
            lblDetalhesContent.Width = pbCurrentTable.Width;

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

            enableButtons();
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

            enableButtons();
        }

        private void btnPasso5_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;

            this.btnPasso1.Enabled = false;
            this.btnPasso2.Enabled = false;
            this.btnPasso3.Enabled = false;
            this.btnPasso4.Enabled = false;
            this.btnPasso5.Enabled = false;

            grpExecucao.Visible = true;
            if (!bwCopy.IsBusy)
                bwCopy.RunWorkerAsync();
        }

        #endregion

        private void bwCopy_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            e.Result = executeCopy(worker);
        }
        private void bwCopy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                var uiState = (ProgressState)e.UserState;

                if (uiState.OverallProgressMinimum != null)
                    this.pbOverall.Minimum = uiState.OverallProgressMinimum.Value;

                if (uiState.OverallProgressMaximum != null)
                    this.pbOverall.Maximum = uiState.OverallProgressMaximum.Value;

                if (uiState.OverallProgressValue != null)
                    this.pbOverall.Value = uiState.OverallProgressValue.Value;

                if (uiState.CurrentTableProgressMinimum != null)
                    this.pbCurrentTable.Minimum = uiState.CurrentTableProgressMinimum.Value;

                if (uiState.CurrentTableProgressMaximum != null)
                    this.pbCurrentTable.Maximum = uiState.CurrentTableProgressMaximum.Value;

                if (uiState.CurrentTableProgressValue != null)
                    this.pbCurrentTable.Value = uiState.CurrentTableProgressValue.Value;

                if (uiState.CurrentTableName != null)
                    this.lblCurrentTable.Text = uiState.CurrentTableName;

                if (uiState.Details != null)
                    this.lblDetalhesContent.Text = uiState.Details;

                this.Refresh();
            }
        }

        private void bwCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enableButtons();
            //grpExecucao.Visible = false;
            this.UseWaitCursor = false;
        }

        #region Eventos da Sessão de Cópia

        private void TheSession_OnTableCopyStarted(object sender, TableCopyEventArgs e, BackgroundWorker worker)
        {
            var ui = new ProgressState
            {
                Details = "",
                CurrentTableName = e.Table.Name,
                CurrentTableProgressMinimum = 0,
                CurrentTableProgressMaximum = e.Table.RowsToCopy.Count,
                CurrentTableProgressValue = 0,
            };
            worker.ReportProgress(1, ui);
        }
        private void TheSession_OnTableCopyEnded(object sender, TableCopyEventArgs e, BackgroundWorker worker)
        {
            var ui = new ProgressState
            {
                Details = "",
                OverallProgressValue = e.ThisTableNumber,
            };
            worker.ReportProgress(1, ui);
        }
        private void TheSession_OnRowCopyStarted(object sender, RowCopyEventArgs e, BackgroundWorker worker)
        {
            var ui = new ProgressState
            {
                Details = $"Copiando o registro {e.ThisRowNumber}...",
            };
            worker.ReportProgress(1, ui);
        }
        private void TheSession_OnRowCopyEnded(object sender, RowCopyEventArgs e, BackgroundWorker worker)
        {
            var ui = new ProgressState
            {
                Details = "",
                CurrentTableProgressValue = e.ThisRowNumber,
            };
            worker.ReportProgress(1, ui);
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

            enableButtons();
        }

        private void enableButtons()
        {
            bool sourceConnIsOpen = this.theSession.ConnectionSource.State == ConnectionState.Open;
            bool anyTableSelected = this.theSession.TablesToCopy.Any();

            this.btnPasso1.Enabled = true;
            this.btnPasso2.Enabled = sourceConnIsOpen;
            this.btnPasso3.Enabled = anyTableSelected;
            this.btnPasso4.Enabled = anyTableSelected;
            this.btnPasso5.Enabled = anyTableSelected;
        }

        private bool executeCopy(BackgroundWorker worker)
        {
            bool resultado = true;
            TableCopyEventHandler funcTableStarted = (sender, args) => TheSession_OnTableCopyStarted(sender, args, worker);
            TableCopyEventHandler funcTableEnded = (sender, args) => TheSession_OnTableCopyEnded(sender, args, worker);
            RowCopyEventHandler funcRowStarted = (sender, args) => TheSession_OnRowCopyStarted(sender, args, worker);
            RowCopyEventHandler funcRowEnded = (sender, args) => TheSession_OnRowCopyEnded(sender, args, worker);

            try
            {
                this.theSession.OnTableCopyStarted += funcTableStarted;
                this.theSession.OnTableCopyEnded += funcTableEnded;
                this.theSession.OnRowCopyStarted += funcRowStarted;
                this.theSession.OnRowCopyEnded += funcRowEnded;

                var ui = new ProgressState
                {
                    Details = "",
                    CurrentTableName = "",
                    OverallProgressMinimum = 0,
                    OverallProgressMaximum = this.theSession.TablesToCopy.Count,
                    OverallProgressValue = 0,
                    CurrentTableProgressMinimum = 0,
                    CurrentTableProgressMaximum = 0,
                    CurrentTableProgressValue = 0,
                };
                worker.ReportProgress(1, ui);

                this.theSession.ExecuteCopy();
                resultado = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Executing...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultado = false;
            }
            finally
            {
                this.theSession.OnTableCopyStarted -= funcTableStarted;
                this.theSession.OnTableCopyEnded -= funcTableEnded;
                this.theSession.OnRowCopyStarted -= funcRowStarted;
                this.theSession.OnRowCopyEnded -= funcRowEnded;
            }
            return resultado;
        }



        private sealed class ProgressState
        {
            public int? OverallProgressMinimum { get; set; }
            public int? OverallProgressMaximum { get; set; }
            public int? OverallProgressValue { get; set; }
            public int? CurrentTableProgressMinimum { get; set; }
            public int? CurrentTableProgressMaximum { get; set; }
            public int? CurrentTableProgressValue { get; set; }
            public string CurrentTableName { get; set; }
            public string Details { get; set; }
            
            public ProgressState()
            {
                this.CurrentTableName = null;
                this.Details = null;
            }
        }
    }

}
