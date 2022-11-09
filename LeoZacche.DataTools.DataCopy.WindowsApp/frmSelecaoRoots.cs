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
                    if (nodeClicked.Nodes.Count == 0)//somente colunas da PK não contêm nós filhos 
                    {
                        nodeBeingEdited = nodeClicked;

                        var padding = (tvwRegistros.Bounds.Width - tvwRegistros.ClientSize.Width) / 2;
                        var textWidth = nodeTextWidth(nodeClicked);


                        this.txtColumnValue.Font = nodeClicked.NodeFont ?? nodeClicked.TreeView.Font;
                        this.txtColumnValue.Top = nodeClicked.Bounds.Top + nodeClicked.Parent.Bounds.Top;
                        this.txtColumnValue.Left = nodeClicked.Bounds.Left + textWidth;
                        this.txtColumnValue.Width = nodeClicked.Bounds.Width - textWidth;
                        this.txtColumnValue.Height = nodeClicked.Bounds.Height;

                        this.txtColumnValue.Top = tvwRegistros.Top + nodeClicked.Bounds.Top;
                        this.txtColumnValue.Left = nodeClicked.Bounds.Left + textWidth;// + padding;
                        this.txtColumnValue.Width = tvwRegistros.Width - this.txtColumnValue.Left + tvwRegistros.Left;

                        var rowNode = nodeBeingEdited.Parent;
                        var tableNode = rowNode.Parent;
                        var tabname = tableNode.Name;
                        var colname = nodeBeingEdited.Name;
                        var theTable = this.TablesToCopy.First(t => t.Name == tabname);
                        var theRow = theTable.RowsToCopy.ElementAt(rowNode.Index);

                        var valueToEdit = Convert.ToString(theRow.PrimaryKeyColumnsValues[colname]);
                        this.txtColumnValue.Text = valueToEdit;


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
                    var rowNode = nodeBeingEdited.Parent;
                    var tableNode = rowNode.Parent;
                    var tabname = tableNode.Name;
                    var colname = nodeBeingEdited.Name;
                    var theTable = this.TablesToCopy.First(t => t.Name == tabname);
                    var theCol = theTable.PrimaryKey.Columns.First(c => c.Name == colname);
                    var type = theCol.DataType;

                    var text = txtColumnValue.Text;
                    Convert.ChangeType(text, type);
                    e.Cancel = false; // controle pode perder o foco!
                }
            }
            catch 
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
            string nodekey, nodeText;

            ITable theTable = this.TablesToCopy.FirstOrDefault(t => t.Name == tablename);
            if (theTable == null)
            {
                theTable = this.getTable(tablename);

                var emptyRow = new Row();
                foreach (var col in theTable.PrimaryKey.Columns) 
                {
                    var colname = col.Name;
                    var defaultValue = col.DataType.GetDefaultValue();
                    emptyRow.PrimaryKeyColumnsValues.Add(colname, defaultValue);
                }

                theTable.RowsToCopy.Add(emptyRow);
                this.TablesToCopy.Add(theTable);
            }


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

            var pkColumns = theTable.PrimaryKey.Columns;
            foreach (var col in pkColumns)
            {
                nodekey = $"{col.Name}";
                nodeText = textForNode(col.Name, col.DatabaseSpecificDataType, "<dbl-click to edit>");
                rowNode.Nodes.Add(nodekey, nodeText);
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

            throw new InvalidOperationException("Encontrado mais um nó com a mesma chave!!!");
        }
        private ITable getTable(string tablename)
        {
            var table = this.theConnection.GetTable(tablename);
            return table;
        }

        private IList<IColumn> getAllColumns(string tablename)
        {
            var list = this.theConnection.GetAllColumns_NEW(tablename);

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
                            var rowNode = nodeBeingEdited.Parent;
                            var tableNode = rowNode.Parent;
                            var tabname = tableNode.Name;
                            var colname = nodeBeingEdited.Name;
                            var theTable = this.TablesToCopy.First(t => t.Name == tabname);
                            var theRow = theTable.RowsToCopy.ElementAt(rowNode.Index);
                            var theCol = theTable.PrimaryKey.Columns.First(c => c.Name == colname);
                            var type = theCol.DataType;
                            var sqlTypename = theCol.DatabaseSpecificDataType;
                            var newValue = Convert.ChangeType(txtColumnValue.Text, type);

                            theRow.PrimaryKeyColumnsValues[colname] = newValue;

                            var newText = textForNode(colname, sqlTypename, txtColumnValue.Text);
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
        private string textForNode(IColumn column, dynamic value)
        {
            var typename = column.DataType.Name;
            var text = $"{column.Name} ({typename}): {column.Value}";
            return text;
        }
        private string textForNode(string columnName, string typename, string value)
        {
            var text = $"{columnName} ({typename}): {value}";
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        internal IList<ITable> buildTablesToCopyList(TreeNodeCollection listOfTableNodes)
        {
            IList<ITable> theList = new List<ITable>();

            foreach (TreeNode tableNode in listOfTableNodes)
            {
                var tableToCopy = new Table() { Name = tableNode.Text };
                tableToCopy.PrimaryKey.ConstraintName = (string)tableNode.Tag;

                // assembling a list of rows tro copy
                foreach (TreeNode rowNode in tableNode.Nodes)
                {
                    var row = new Row();
                    var buildPk = (tableToCopy.PrimaryKey.Columns.Count == 0);

                    foreach (TreeNode colNode in rowNode.Nodes) // these cols are the primary key
                    {
                        var colDefinition = colNode.ConvertTagTo<DataColumn>();
                        var columnName = colDefinition.ColumnName;
                        dynamic columnValue = colDefinition.ExtendedProperties[COLUMN_VALUE];
                        row.PrimaryKeyColumnsValues.Add(columnName, columnValue);

                        // if the PK of the table is not yet built... build it!
                        if (buildPk)
                        {
                            var newPkCol = new Column
                            {
                                Name = columnName,
                                AllowNull = false, // every column that is part of PK is not null by design
                                DataType = colDefinition.DataType
                                // TODO: outras props são necessárias?
                            };
                            tableToCopy.PrimaryKey.Columns.Add(newPkCol);
                        }
                    }

                    tableToCopy.RowsToCopy.Add(row);
                }

                // loading the table definition
                var loadedColumns = getAllColumns(tableToCopy.Name);
                tableToCopy.Columns.CloneFrom(loadedColumns);


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
#pragma warning disable S2486 // Generic exceptions should not be ignored
            catch { }
#pragma warning restore S2486 // Generic exceptions should not be ignored
#pragma warning restore S108 // Nested blocks of code should not be left empty

            return resultado;
        }
    }


}
