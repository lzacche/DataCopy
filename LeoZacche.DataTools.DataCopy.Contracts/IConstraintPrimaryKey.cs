using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IConstraintPrimaryKey : IConstraintBase
    {
        IList<IColumn> Columns { get; }
    }
}
