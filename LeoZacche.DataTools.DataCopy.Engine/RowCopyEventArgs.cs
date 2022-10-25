using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Engine
{

    public class RowCopyEventArgs : EventArgs
    {
        public RowCopyEventArgs(Row row, int thisRowNumber, int totalRows)
        {
            this.Row = row;
            this.ThisRowNumber = thisRowNumber;
            this.TotalRows = TotalRows;
        }

        public Row Row { get; private set; }
        public int ThisRowNumber { get; private set; }
        public int TotalRows { get; private set; }
    }
}
