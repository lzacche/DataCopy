using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.DataTools.DataCopy.Engine;


namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    public partial class ucConexao : UserControl
    {
        private DataConnection _dataConnection = null;
        private string savedUsername = null;
        private string savedPassword = null;
        private bool controlsAreLoaded = false;

        #region Eventos

        //public delegate void EventHandler(object? sender, EventArgs e);
        [Description("Disparado antes do início do teste de conexão."), Category("Comportamento")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event EventHandler Test_Started;

        [Description("Disparado após o encerramento do teste de conexão."), Category("Comportamento")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event EventHandler Test_Ended;

        [Description("Disparado antes do início do teste de conexão."), Category("Comportamento")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event EventHandler Connection_Opening;

        [Description("Disparado após o encerramento do teste de conexão."), Category("Comportamento")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public event EventHandler Connection_Opened;
        #endregion

        //private int instance = 0;
        //private static int instCount = 0;

        public ucConexao()
        {
            InitializeComponent();

            //this.instance = ++instCount;
            //System.Diagnostics.Debug.WriteLine($"ctor default: {this.instance} [{this.Name}]");
        }
        //public ucConexao(DataConnection dataConnection)
        //{
        //    InitializeComponent();

        //    this.instance = ++instCount;
        //    this._dataConnection = dataConnection;

        //    System.Diagnostics.Debug.WriteLine($"ctor param: {this.instance} [{this.Name}]");
        //    System.Diagnostics.Debug.WriteLine($"Param is null? {dataConnection == null}");
        //}
        //~ucConexao()
        //{
        //    System.Diagnostics.Debug.WriteLine($"destrutor: {this.instance} [{this.Name}]");
        //}


        #region Propriedads

        //public DataConnection DataConnection { get; private set; }
        public DataConnection DataConnection
        {
            get { return this._dataConnection; }
            set
            {
                if (this._dataConnection == null)
                {
                    if (value != null)
                    {
                        if (!this.controlsAreLoaded)
                            loadControls();

                        this._dataConnection = value;
                        setControlsData(this._dataConnection);
                        setControlsEnabled(this._dataConnection.State);
                    }
                }
                else
                    throw new PropertyAlreadySetException("DataConnection");
            }
        }

        [Description("Texto exibido no grupo de controles"), Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => this.grpControles.Text;
            set => this.grpControles.Text = value;
        }

        #endregion


        //private void ucConexao_Load(object sender, EventArgs e)
        //{
        //    //System.Diagnostics.Debug.WriteLine($"load: {this.instance} [{this.Name}]");
        //    //if (this._dataConnection != null)
        //    //{
        //    //    FillControls(this._dataConnection);
        //    //}
        //}


        #region Eventos dos Controles

        private void ucConexao_Load(object sender, EventArgs e)
        {
            if (!this.controlsAreLoaded)
                loadControls();
        }

        private void cboTipoConexao_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (KeyValuePair<ConnectionTypeEnum, string>)this.cboTipoConexao.SelectedItem;
            ConnectionTypeEnum type = selected.Key;

            var serverTitle= this._dataConnection.ServerTitle;
            var databaseTitle = this._dataConnection.DatabaseOrSchemaTitle;

            this.lblServidor.Text = $"{serverTitle}:";
            this.lblDatabaseOrSchema.Text = $"{databaseTitle}:";
        }

        private void rbAuthIntegrada_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAuthIntegrada.Checked)
            {
                this.savedUsername = this.txtUsuario.Text;
                this.savedPassword = this.txtSenha.Text;

                this.txtUsuario.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                this.txtSenha.Text = "<automática>";
                this.txtSenha.PasswordChar = (char)0;
            }
            else
            {
                this.txtUsuario.Text = this.savedUsername;
                this.txtSenha.Text = this.savedPassword;
                this.txtSenha.PasswordChar = '*';
            }

            this.txtUsuario.Enabled = !rbAuthIntegrada.Checked;
            this.txtSenha.Enabled = !rbAuthIntegrada.Checked;
        }
        private void cboDatabseOrSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._dataConnection.DatabaseOrSchema = (string)this.cboDatabseOrSchema.SelectedItem;  
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            testConnection();
        }

        private void btnAbrirFechar_Click(object sender, EventArgs e)
        {
            switch (this._dataConnection.State)
            {
                case ConnectionState.Closed:
                    openConnection();
                    break;

                case ConnectionState.Open:
                    closeConnection();
                    break;

                default:
                    throw new Exception("Not at the moment!");
            }
        }

        #endregion


        #region Funções Privadas

        private void loadControls()
        {
            loadComboConnectionTypes();
            btnAbrirFechar.Text = "Abrir Conexão";
            this.controlsAreLoaded = true;
        }

        private void loadComboConnectionTypes()
        {
            this.cboTipoConexao.Items.Clear();

            var lista = Enum.GetValues(typeof(ConnectionTypeEnum));

            foreach (ConnectionTypeEnum item in lista)
            {
                var descricao = Description((ConnectionTypeEnum)item);
                this.cboTipoConexao.Items.Add(new KeyValuePair<ConnectionTypeEnum, string>(item, descricao));
            }
            if (lista.GetLength(0) > 0)
            {
                this.cboTipoConexao.DisplayMember = "Value";
                this.cboTipoConexao.ValueMember = "Key";
            }
        }
        private void loadComboDatabases(IList<string> databaseList)
        {
            this.cboDatabseOrSchema.Items.Clear();

            foreach(var dbName in databaseList)
            {
                this.cboDatabseOrSchema.Items.Add(dbName);
            }

        }
        private void setControlsData(DataConnection dataConnection)
        {
            foreach (KeyValuePair<ConnectionTypeEnum, string> item in this.cboTipoConexao.Items)
            {
                if (item.Key == dataConnection.ConnectionType)
                {
                    this.cboTipoConexao.SelectedItem = item;
                    break;
                }
            }

            this.txtServidor.Text = dataConnection.Server;
            this.txtUsuario.Text = dataConnection.Username;
            this.txtSenha.Text = dataConnection.Password;
            if (dataConnection.Authentication == ConnectionAuthenticationEnum.WindowsIntegrated)
            {
                this.rbAuthIntegrada.Checked = true;
                this.rbAuthNomeada.Checked = false;
            }
            else
            {
                this.rbAuthIntegrada.Checked = false;
                this.rbAuthNomeada.Checked = true;
            }
            //this.rbAuthIntegrada_CheckedChanged(this.rbAuthIntegrada, new EventArgs());

            this.btnAbrirFechar.Text = (this._dataConnection.State == ConnectionState.Closed ? "Abrir Conexão" : "Fechar Conexão");

            if (this._dataConnection.State == ConnectionState.Open)
            {
                loadComboDatabases(this._dataConnection.GetDatabaseNames());
                this.cboDatabseOrSchema.SelectedItem = this._dataConnection.DatabaseOrSchema;
            }
        }
        private void setControlsEnabled(ConnectionState state)
        {
            this.cboTipoConexao.Enabled = (state == ConnectionState.Closed);
            this.txtServidor.Enabled = (state == ConnectionState.Closed);
            this.rbAuthIntegrada.Enabled = (state == ConnectionState.Closed);
            this.rbAuthNomeada.Enabled = (state == ConnectionState.Closed);
            this.txtUsuario.Enabled = (state == ConnectionState.Closed) && this.rbAuthNomeada.Checked;
            this.txtSenha.Enabled = (state == ConnectionState.Closed) && this.rbAuthNomeada.Checked;
            this.cboDatabseOrSchema.Enabled = (state == ConnectionState.Open);
            this.btnTestar.Enabled = (state == ConnectionState.Closed);
            this.btnAbrirFechar.Enabled = (state == ConnectionState.Closed || state == ConnectionState.Open);
        }

        private void testConnection()
        {
            if (this.Test_Started != null)
            {
                this.Test_Started(this, new EventArgs());
                Application.DoEvents();
            }

            string messageText = null;
            MessageBoxIcon messageIcon = MessageBoxIcon.None;

            try
            {
                setControlsEnabled(ConnectionState.Connecting);

                //this.UseWaitCursor = true;
                var selectedType = (KeyValuePair<ConnectionTypeEnum, string>)cboTipoConexao.SelectedItem;
                var tmpConn = new DataConnection()
                {
                    ConnectionType = selectedType.Key,
                    Server = txtServidor.Text,
                    Username = txtUsuario.Text,
                    Password = txtSenha.Text,
                };
                tmpConn.Test();
                messageText = "Conexão realizada com sucesso!";
                messageIcon = MessageBoxIcon.Information;
            }
            catch (Exception ex)
            {
                messageText = ex.Message;
                messageIcon = MessageBoxIcon.Error;
            }
            finally
            {
                MessageBox.Show(messageText, "Teste de Conexão", MessageBoxButtons.OK, messageIcon);
                //this.UseWaitCursor = false;

                if (this.Test_Ended != null)
                {
                    this.Test_Ended(this, new EventArgs());
                    Application.DoEvents();
                }

                setControlsEnabled(this._dataConnection.State);
            }
        }
        private void openConnection()
        {
            if (this.Connection_Opening != null)
            {
                this.Connection_Opening(this, new EventArgs());
                Application.DoEvents();
            }

            string messageText = null;
            MessageBoxIcon messageIcon = MessageBoxIcon.None;

            try
            {
                setControlsEnabled(ConnectionState.Connecting);

                //this.UseWaitCursor = true;
                var selectedType = (KeyValuePair<ConnectionTypeEnum, string>)cboTipoConexao.SelectedItem;

                this._dataConnection.ConnectionType = selectedType.Key;
                this._dataConnection.Server = txtServidor.Text;
                this._dataConnection.Username = txtUsuario.Text;
                this._dataConnection.Password = txtSenha.Text;

                this._dataConnection.Open();
                var databases = this._dataConnection.GetDatabaseNames();
                loadComboDatabases(databases);

                btnAbrirFechar.Text = "Fechar Conexão";

                messageText = "Conexão estabelecida com sucesso!";
                messageIcon = MessageBoxIcon.Information;
            }
            catch (Exception ex)
            {
                messageText = ex.Message;
                messageIcon = MessageBoxIcon.Error;
            }
            finally
            {
                MessageBox.Show(messageText, "Abertura de Conexão", MessageBoxButtons.OK, messageIcon);
                //this.UseWaitCursor = false;

                if (this.Connection_Opened != null)
                {
                    this.Connection_Opened(this, new EventArgs());
                    Application.DoEvents();
                }

                setControlsEnabled(this._dataConnection.State);
            }
        }
        private void closeConnection()
        {
            this._dataConnection.Close();
            btnAbrirFechar.Text = "Abrir Conexão";
            setControlsEnabled(this._dataConnection.State);
        }

        #endregion



        public static string Description(ConnectionTypeEnum enumValue)
        {
            var descriptionAttribute = enumValue.GetType()
            .GetField(enumValue.ToString())
            .GetCustomAttributes(false)
            .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            // return description
            return descriptionAttribute?.Description ?? "";
        }


    }
}