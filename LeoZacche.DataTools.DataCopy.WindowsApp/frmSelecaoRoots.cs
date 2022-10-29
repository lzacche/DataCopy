using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using LeoZacche.Utils;
using LeoZacche.DataTools.DataCopy.Engine;
using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.DataTools.DataCopy.Contracts.Extensions;

namespace LeoZacche.DataTools.DataCopy.WindowsApp
{
    public partial class frmSelecaoRoots : Form
    {
        const string COLUMN_VALUE = "COLUMN_VALUE";

        private DataConnection theConnection = null;
        private IList<string> tableList = null;
        private bool isEditingColumnValue = false;
        private TreeNode nodeBeingEdited = null;
        private bool columnEditEndingIsAlreadyRunning = false;

        public IList<ITable> TablesToCopy { get; internal set; }

        public frmSelecaoRoots()
        {
            InitializeComponent();
            this.TablesToCopy = new List<ITable>();
        }

        public frmSelecaoRoots(DataConnection dataConnection, IList<ITable> tablesToCopy) : this()
        {
            this.theConnection = dataConnection;
            this.TablesToCopy.Clear();
            foreach (var t in tablesToCopy)
                this.TablesToCopy.Add(t);
        }


        #region Eventos do Form

        private void frmSelecaoRoots_Activated(object sender, EventArgs e)
        {
            layoutControls();
        }
        private void frmSelecaoRoots_Resize(object sender, EventArgs e)
        {
            layoutControls();
        }
        private void frmSelecaoRoots_Load(object sender, EventArgs e)
        {
            /*
            if (this.theConnection == null)
                throw new Exception("Conexão não foi informada.");

            if (this.theConnection.DatabaseOrSchema == null)
                throw new Exception("Database or Schema não foi informado.");
            */

            this.txtColumnValue.Visible = false;
            if (this.tableList == null)
            {
                var tables = GetTables();
                tables = tables.OrderBy(t => t).ToList();

                if (tables != null)
                {
                    this.tableList = tables;
                    var filter = txtFiltroTabela.Text;
                    showFilteredTableList(tables, filter);
                }
            }
            this.tvwRegistros.Nodes.Clear();

        }

        #endregion


        private void txtFiltroTabela_TextChanged(object sender, EventArgs e)
        {
            var filter = txtFiltroTabela.Text;
            showFilteredTableList(this.tableList, filter);
        }
        private void lstTabelas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.UseWaitCursor = true;
            this.Refresh();

            int index = this.lstTabelas.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var tablename = (string)this.lstTabelas.Items[index];
                addTableToSelected(tablename);
            }

            this.UseWaitCursor = false;
        }
        private void tvwRegistros_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!isEditingColumnValue)
            {
                var nodeClicked = this.tvwRegistros.GetNodeAt(e.Location);
                if (nodeClicked != null)
                {
                    var colDefinition = nodeClicked.ConvertTagTo<DataColumn>();
                    if (colDefinition != null)
                    {
                        nodeBeingEdited = nodeClicked;

                        var padding = (tvwRegistros.Bounds.Width - tvwRegistros.ClientSize.Width) / 2;
                        var textWidth = nodeTextWidth(nodeClicked);


                        this.txtColumnValue.Font = nodeClicked.NodeFont ?? nodeClicked.TreeView.Font;
                        this.txtColumnValue.Top = nodeClicked.Bounds.Top + nodeClicked.Parent.Bounds.Top;
                        this.txtColumnValue.Left = nodeClicked.Bounds.Left + textWidth;
                        this.txtColumnValue.Width = nodeClicked.Bounds.Width - textWidth;
                        this.txtColumnValue.Height = nodeClicked.Bounds.Height;

                        /*
                        tvwRegistros.LabelEdit = true;
                        nodeClicked.BeginEdit();
                        this.txtColumnValue.Top = 0; //nodeClicked.Bounds.Top;
                        this.txtColumnValue.Left = 0;// nodeClicked.Bounds.Left + tvwRegistros.;
                        this.txtColumnValue.Left = tvwRegistros.Left;
                        this.txtColumnValue.Left = tvwRegistros.Left + tvwRegistros.Margin.Left + tvwRegistros.Nodes[0].Bounds.Left;
                        this.txtColumnValue.Left = tvwRegistros.Left + tvwRegistros.Margin.Left + tvwRegistros.Nodes[0].Nodes[0].Bounds.Left;
                        this.txtColumnValue.Left = tvwRegistros.Left + tvwRegistros.Margin.Left + nodeClicked.Bounds.Left + textWidth;
                        this.txtColumnValue.Left = tvwRegistros.Left + nodeClicked.Bounds.Left + textWidth;
                        */

                        this.txtColumnValue.Top = tvwRegistros.Top + nodeClicked.Bounds.Top;
                        this.txtColumnValue.Left = nodeClicked.Bounds.Left + textWidth;// + padding;
                        this.txtColumnValue.Width = tvwRegistros.Width - this.txtColumnValue.Left + tvwRegistros.Left;

                        this.txtColumnValue.Text = Convert.ToString(colDefinition.ExtendedProperties[COLUMN_VALUE]);


                        this.txtColumnValue.Show();
                        this.txtColumnValue.Focus();
                    }
                }
            }
        }
        private void tvwRegistros_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            this.txtColumnValue.Hide();
        }
        private void tvwRegistros_Resize(object sender, EventArgs e)
        {

        }
        private void txtColumnValue_Leave(object sender, EventArgs e)
        {
            endsColumnsValueEditing(false);
        }
        private void txtColumnValue_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    endsColumnsValueEditing(true);
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    endsColumnsValueEditing(false);
                    e.Handled = true;
                    break;

                default:
                    e.Handled = false;
                    break;
            }
        }
        private void txtColumnValue_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (nodeBeingEdited != null)
                {
                    var colDefinition = nodeBeingEdited.ConvertTagTo<DataColumn>();
                    var text = txtColumnValue.Text;// nodeBeingEdited.Text;
                    var type = colDefinition.DataType;
                    var value = Convert.ChangeType(text, type);
                    e.Cancel = false; // controle pode perder o foco!
                }
            }
            catch //(Exception ex)
            {
                e.Cancel = true; // controle NÃO PODE perder o foco!
                // TODO: pensar em uma forma de feedback
            }
        }

        private void layoutControls()
        {
            int margem = 10;
            decimal divisaoVertical = 0.6m;

            // tamanho e posição dos botões
            btnFechar.Left = this.ClientSize.Width - margem - btnFechar.Width;
            btnOk.Left = btnFechar.Left - margem - btnOk.Width;
            btnOk.Top = this.ClientSize.Height - margem - btnOk.Height;
            btnFechar.Top = btnOk.Top;

            int larguraUtil = this.ClientSize.Width - (3 * margem);
            int alturaUtil = this.ClientSize.Height - (2 * margem);
            // espaço para os botões
            alturaUtil -= (btnOk.Height + margem);

            grpTabelasOrigem.Top = margem;
            grpTabelasOrigem.Height = alturaUtil;
            grpTabelasOrigem.Left = margem;
            grpTabelasOrigem.Width = Convert.ToInt32(larguraUtil * divisaoVertical);

            grpRegistrosCopiar.Top = margem;
            grpRegistrosCopiar.Height = alturaUtil;
            grpRegistrosCopiar.Left = grpTabelasOrigem.Right + margem;
            grpRegistrosCopiar.Width = this.ClientSize.Width - grpRegistrosCopiar.Left - margem;


            txtFiltroTabela.Top = 2 * margem;
            txtFiltroTabela.Left = margem;
            //txtFiltroTabela.Height= grpTabelasOrigem.ClientSize.Height - 
            txtFiltroTabela.Width = grpTabelasOrigem.ClientSize.Width - (2 * margem);

            lstTabelas.Left = margem;
            lstTabelas.Top = txtFiltroTabela.Bottom + margem;
            lstTabelas.Width = txtFiltroTabela.Width;
            lstTabelas.Height = grpTabelasOrigem.ClientSize.Height - lstTabelas.Top - margem;

            tvwRegistros.Top = 2 * margem;
            tvwRegistros.Left = margem;
            tvwRegistros.Height = grpRegistrosCopiar.ClientSize.Height - tvwRegistros.Top - margem;
            tvwRegistros.Width = grpRegistrosCopiar.ClientSize.Width - (2 * margem);
        }
        private IList<string> GetTables()
        {
            this.UseWaitCursor = true;
            this.Refresh();

            var list = this.theConnection.GetAllTableNames();

            this.UseWaitCursor = false;
            this.Refresh();

            return list;
        }
        private void showFilteredTableList(IList<string> originalList, string filter)
        {
            this.lstTabelas.Items.Clear();

            var filteredList = originalList.Where(t => t.Contains(filter, StringComparison.InvariantCultureIgnoreCase));

            foreach (var table in filteredList)
                this.lstTabelas.Items.Add(table);
        }



        private void addTableToSelected(string tablename)
        {
            TreeNode node;
            string nodekey, nodeText;
            DataColumn colDefinition;

            var tableNode = getNodeByName(tablename);
            if (tableNode == null)
            {
                nodekey = $"{tablename}";
                nodeText = $"{tablename}";
                tableNode = this.tvwRegistros.Nodes.Add(nodekey, nodeText);
            }

            var newRowNumber = tableNode.Nodes.Count + 1;
            var rowNodeKey = $"{tablename}#{newRowNumber}";
            var rowNode = tableNode.Nodes.Add(rowNodeKey, $"Row {newRowNumber:00}");
            tableNode.Expand();

            var pkColumns = getPkColumns(tablename);
            foreach (var col in pkColumns)
            {
                nodekey = $"{col.ColumnName}";
                //nodeText = $"{col.ColumnName}: <dbl-click to edit>";
                nodeText = textForNode(col, "<dbl-click to edit>");
                node = rowNode.Nodes.Add(nodekey, nodeText);

                colDefinition = new DataColumn()
                {
                    ColumnName = col.ColumnName,
                    DataType = col.DataType,
                };

                colDefinition.ExtendedProperties.Add(COLUMN_VALUE, col.DataType.GetDefaultValue());
                node.Tag = colDefinition;
                rowNode.Expand();
            }

        }
        private TreeNode getNodeByName(string name)
        {
            var list = this.tvwRegistros.Nodes.Find(name, false);

            if (list.Count() == 0)
                return null;

            if (list.Count() == 1)
                return list[0];

            throw new Exception("Encontrado mais um nó com a mesma chave!!!");
        }
        private IList<DataColumn> getPkColumns(string tablename)
        {
            var list = this.theConnection.GetPrimaryKeyColumns(tablename);

            return list;
        }
        private string tableNodeKey(string tablename)
        {
            var key = $"[TABLE]{tablename}";

            return key;
        }
        private int nodeTextWidth(TreeNode node)
        {
            const int kerning = 9; // pequeno ajuste/espaçamento no início e no fim da string

            var doisPontos = node.Text.IndexOf(":");
            var texto = doisPontos > 0 ? node.Text.Substring(0, doisPontos + 2) : node.Text; // o "+2" é para incluir o dois pontos e o espaço ao final do nome da coluna

            var grp = Graphics.FromHwnd(node.TreeView.Handle);
            var measure = grp.MeasureString(texto, node.NodeFont ?? node.TreeView.Font);
            var width = (int)Math.Round(measure.Width, 0, MidpointRounding.ToPositiveInfinity);
            width += kerning; // ajuste fino
            return width;
        }
        private void endsColumnsValueEditing(bool confirm)
        {
            System.Diagnostics.Debug.WriteLine("endsColumnsValueEditing");
            if (!columnEditEndingIsAlreadyRunning)
            {
                columnEditEndingIsAlreadyRunning = true;
                if (nodeBeingEdited != null)
                {
                    if (confirm)
                    {
                        if (this.ValidateChildren())
                        {
                            var colDefinition = nodeBeingEdited.ConvertTagTo<DataColumn>();

                            var text = txtColumnValue.Text;
                            var type = colDefinition.DataType;
                            var newValue = Convert.ChangeType(text, type);

                            colDefinition.ExtendedProperties[COLUMN_VALUE] = newValue;

                            var newText = textForNode(colDefinition, newValue);
                            nodeBeingEdited.Text = newText;
                            txtColumnValue.Hide();
                            nodeBeingEdited = null;
                        }
                    }
                    else
                    {
                        txtColumnValue.Hide();
                        nodeBeingEdited = null;
                    }
                }
                columnEditEndingIsAlreadyRunning = false;
            }
        }
        private bool valueIsValidForType(string text, Type type)
        {
            var value = Convert.ChangeType(text, type);
            return true;
        }
        private string textForNode(DataColumn columnDefinition, dynamic value)
        {
            var typename = columnDefinition.DataType.Name;
            var text = $"{columnDefinition.ColumnName} ({typename}): {value}";
            return text;
        }

        private void txtColumnValue_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var listToCopy = buildTablesToCopyList(tvwRegistros.Nodes);
            this.TablesToCopy.CloneFrom(listToCopy);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        internal IList<ITable> buildTablesToCopyList(TreeNodeCollection listOfTableNodes)
        {
            IList<ITable> theList = new List<ITable>();

            foreach (TreeNode tableNode in listOfTableNodes)
            {
                var tableToCopy = new Table() { Name = tableNode.Text };

                foreach (TreeNode rowNode in tableNode.Nodes)
                {
                    var row = new Row();

                    foreach (TreeNode colNode in rowNode.Nodes)
                    {
                        var colDefinition = colNode.ConvertTagTo<DataColumn>();
                        var columnName = colDefinition.ColumnName;
                        dynamic vaolumnValue = colDefinition.ExtendedProperties[COLUMN_VALUE];
                        row.PrimaryKeyColumnsValues.Add(columnName, vaolumnValue);
                    }

                    tableToCopy.RowsToCopy.Add(row);
                }

                theList.Add(tableToCopy);
            }

            return theList;
        }
    }

    internal static class TreeViewExtesions
    {
        public static T ConvertTagTo<T>(this TreeNode node)
        {
            T resultado = default(T);
            try
            {
                resultado = (T)node.Tag;
            }
#pragma warning disable S108 // Nested blocks of code should not be left empty
            catch { }
#pragma warning restore S108 // Nested blocks of code should not be left empty

            return resultado;
        }
    }


}
