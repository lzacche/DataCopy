using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface ITable
    {
        string Name { get; set; }
        public IList<IRow> RowsToCopy { get; }
        public IList<IColumn> Columns { get; }
        public string PrimaryKeyConstraintName { get; set; }
    }
}
