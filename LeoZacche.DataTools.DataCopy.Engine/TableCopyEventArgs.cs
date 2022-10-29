using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;

namespace LeoZacche.DataTools.DataCopy.Engine
{

    public class TableCopyEventArgs : EventArgs
    {
        public TableCopyEventArgs(ITable table, int thisTableNumber, int totalTables)
        {
            this.Table = table;
            this.ThisTableNumber = thisTableNumber;
            this.TotalTables = totalTables;
        }

        public ITable Table { get; private set; }
        public int ThisTableNumber { get; private set; }
        public int TotalTables { get; private set; }
    }
}
