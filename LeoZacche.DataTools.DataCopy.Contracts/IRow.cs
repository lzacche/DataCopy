using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IRow
    {
        public IDictionary<string, dynamic> PrimaryKeyColumnsValues { get; }
    }
}
