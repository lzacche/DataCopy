using System;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Engine
{
    public class DataCopySession
    {
        public DataConnection ConnectionSource { get; private set; }
        public DataConnection ConnectionDestination { get; private set; }
        public IList<Table> TablesToCopy {get; private set;}


        public DataCopySession()
        {
            this.ConnectionSource = new DataConnection();
            this.ConnectionDestination = new DataConnection();
            this.TablesToCopy = new List<Table>();
        }

        #region Manipuladores de Eventos
        public delegate void TableCopyEventHandler(object? sender, TableCopyEventArgs e);
        public delegate void RowCopyEventHandler(object? sender, RowCopyEventArgs e);
        #endregion

        #region Eventos
        public event EventHandler OnPreCheckStarted;
        public event EventHandler OnPreCheckEnded;
        public event EventHandler OnCopyStarted;
        public event EventHandler OnCopyEnded;
        public event TableCopyEventHandler OnTableCopyStarted;
        public event TableCopyEventHandler OnTableCopyEnded;
        public event RowCopyEventHandler OnRowCopyStarted;
        public event RowCopyEventHandler OnRowCopyEnded;
        #endregion



        public void ExecuteCopy()
        {
            checkPreRequisites();

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
        private void checkPreRequisites()
        {
            if (OnPreCheckStarted != null)
                OnPreCheckStarted(this, new EventArgs());

            // conexao de origem está aberta? se nao, exception.
            // conexao de destino está aberta? se nao, exception.

            if (OnPreCheckEnded != null)
                OnPreCheckEnded(this, new EventArgs());
        }

        private void copyTable(Table table, int thisTableNumber, int totalTables)
        {
            var eventArgs = new TableCopyEventArgs(table, thisTableNumber, totalTables);

            if (OnTableCopyStarted != null)
                OnTableCopyStarted(this, eventArgs);

            int currentRow = 1;
            foreach (var row in table.RowsToCopy)
            {
                copyRow(row, currentRow++, table.RowsToCopy.Count);
            }


            if (OnTableCopyEnded != null)
                OnTableCopyEnded(this, eventArgs);
        }

        private void copyRow(Row row, int thisRowNumber, int totalRows)
        {
            var eventArgs = new RowCopyEventArgs(row, thisRowNumber, totalRows);

            if (OnRowCopyStarted != null)
                OnRowCopyStarted(this, eventArgs);


            // select na conexao de origem
            // select * from table where row = x


            // tabela existe no destino?
            // se nao existir, create table
            // se existir, verificar a estrutura



            if (OnRowCopyEnded != null)
                OnRowCopyEnded(this, eventArgs);
        }
    }
}
