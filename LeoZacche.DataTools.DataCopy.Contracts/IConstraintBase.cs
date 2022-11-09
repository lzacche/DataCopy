using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IConstraintBase
    {
        string ConstraintName { get; set; }
        string TableName { get; set; }

        IConstraintBase Clone();
    }
}
