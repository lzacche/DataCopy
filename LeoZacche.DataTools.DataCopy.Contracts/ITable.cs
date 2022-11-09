using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface ITable
    {
        string Name { get; set; }
        IList<IRow> RowsToCopy { get; }
        IList<IColumn> Columns { get; }
        IConstraintPrimaryKey PrimaryKey { get; }
        //IList<IConstraintUniqueKey> UniqueConstraints { get; }
        //IList<IConstraintCheck> CheckConstraints { get; }
        //IList<IConstraintForeignKey> ForeignKeyConstraints { get; }

        ITable Clone();
    }
}
