using System;
using System.Collections.Generic;
using System.Text;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class Table : ITable
    {
        public string Name { get; set; }
        public IList<IColumn> Columns { get; private set; }
        public IList<IRow> RowsToCopy { get; private set; }
        public string PrimaryKeyConstraintName { get; set; }

        public Table()
        {
            this.Columns = new List<IColumn>();
            this.RowsToCopy = new List<IRow>();
        }
    }
}
