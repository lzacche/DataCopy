using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;

namespace LeoZacche.DataTools.DataCopy.Engine
{

    public class RowCopyEventArgs : EventArgs
    {
        public RowCopyEventArgs(IRow row, int thisRowNumber, int totalRows)
        {
            this.Row = row;
            this.ThisRowNumber = thisRowNumber;
            this.TotalRows = TotalRows;
        }

        public IRow Row { get; private set; }
        public int ThisRowNumber { get; private set; }
        public int TotalRows { get; private set; }
    }
}
