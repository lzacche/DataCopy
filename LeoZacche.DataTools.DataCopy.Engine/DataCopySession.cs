using System;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.DataTools.DataCopy.Contracts.Extensions;

namespace LeoZacche.DataTools.DataCopy.Engine
{
    public class DataCopySession
    {
        public DataConnection ConnectionSource { get; private set; }
        public DataConnection ConnectionDestination { get; private set; }
        public IList<ITable> TablesToCopy { get; private set; }


        public DataCopySession()
        {
            this.ConnectionSource = new DataConnection();
            this.ConnectionDestination = new DataConnection();
            this.TablesToCopy = new List<ITable>();
        }

        #region Manipuladores de Eventos
        public delegate void TableCopyEventHandler(object? sender, TableCopyEventArgs e);
        public delegate void RowCopyEventHandler(object? sender, RowCopyEventArgs e);
        #endregion

        #region Eventos
        public event EventHandler OnCopyPreCheckStarted;
        public event EventHandler OnCopyPreCheckEnded;
        public event EventHandler OnCopyStarted;
        public event EventHandler OnCopyEnded;

        public event EventHandler OnTablePreCheckStarted; 
        public event TableCopyEventHandler OnTableCopyStarted;
        public event TableCopyEventHandler OnTableCopyEnded;
        
        public event RowCopyEventHandler OnRowCopyStarted;
        public event RowCopyEventHandler OnRowCopyEnded;
        #endregion



        public void ExecuteCopy()
        {
            checkCopyPreRequisites();

            if (OnCopyStarted != null)
                OnCopyStarted(this, new EventArgs());




            int currentTable = 1;
            foreach (var table in this.TablesToCopy)
            {
                copyTable(table, currentTable++, this.TablesToCopy.Count);
            }

            if (OnCopyEnded != null)
                OnCopyEnded(this, new EventArgs());
        }

        private void copyTable(ITable table, int thisTableNumber, int totalTables)
        {
            var eventArgs = new TableCopyEventArgs(table, thisTableNumber, totalTables);

            if (OnTableCopyStarted != null)
                OnTableCopyStarted(this, eventArgs);

            checkTablePreRequisites(this.ConnectionSource, this.ConnectionDestination, table);

            int currentRow = 1;
            foreach (var row in table.RowsToCopy)
            {
                copyRow(row, currentRow++, table.RowsToCopy.Count);
            }


            if (OnTableCopyEnded != null)
                OnTableCopyEnded(this, eventArgs);
        }

        private void copyRow(IRow row, int thisRowNumber, int totalRows)
        {
            var eventArgs = new RowCopyEventArgs(row, thisRowNumber, totalRows);

            if (OnRowCopyStarted != null)
                OnRowCopyStarted(this, eventArgs);


            // select na conexao de origem
            // select * from table where row = x

            System.Threading.Thread.Sleep(2000);


            // tabela existe no destino?
            // se nao existir, create table
            // se existir, verificar a estrutura



            if (OnRowCopyEnded != null)
                OnRowCopyEnded(this, eventArgs);
        }


        private void checkCopyPreRequisites()
        {
            if (OnCopyPreCheckStarted != null)
                OnCopyPreCheckStarted(this, new EventArgs());

            // conexao de origem está aberta? se nao, exception.
            // conexao de destino está aberta? se nao, exception.

            if (OnCopyPreCheckEnded != null)
                OnCopyPreCheckEnded(this, new EventArgs());
        }

        private void checkTablePreRequisites(DataConnection source, DataConnection destination, ITable table)
        {
            if (OnTablePreCheckStarted != null)
                OnTablePreCheckStarted(this, new EventArgs());

            // Tabela existe na conexão destino?
            // Estrutura da Tabela destino é Ok?

            var tablesOnDestination = destination.GetAllTableNames();
            var tableFound = tablesOnDestination.Contains(table.Name);

            //var tableColumnsOnSource = source.GetAllColumns(table.Name);
            var tableColumnsOnSource = source.GetAllColumns_NEW(table.Name);

            if (tableFound)
            {
                destination.EnsureTableStructure(table);
            }
            else
            {
                // tabela não existe na conexão destino. Criar!
                var tableToCreate = new Table { Name = table.Name };
                tableToCreate.Columns.CloneFrom(tableColumnsOnSource);

                destination.CreateTable(tableToCreate);
            }


            


            /*
            if (OnTablePreCheckEnded != null)
                OnTablePreCheckEnded(this, new EventArgs());
            */
        }
    }
}
