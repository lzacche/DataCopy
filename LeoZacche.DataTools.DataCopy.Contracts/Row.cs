using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class Row : IRow
    {
        public IDictionary<string, dynamic> PrimaryKeyColumnsValues { get; private set; }

        public Row()
        {
            this.PrimaryKeyColumnsValues = new Dictionary<string, dynamic>();
        }
    }
}
