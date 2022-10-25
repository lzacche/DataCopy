using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Engine
{

    public class TableCopyEventArgs : EventArgs
    {
        public TableCopyEventArgs(Table table, int thisTableNumber, int totalTables)
        {
            this.Table = table;
            this.ThisTableNumber = thisTableNumber;
            this.TotalTables = totalTables;
        }

        public Table Table { get; private set; }
        public int ThisTableNumber { get; private set; }
        public int TotalTables { get; private set; }
    }
}
