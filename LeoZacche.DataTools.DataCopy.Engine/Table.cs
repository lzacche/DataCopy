using System;
using System.Collections.Generic;
using System.Text;

namespace LeoZacche.DataTools.DataCopy.Engine
{
    public class Table
    {
        public string Name { get; set; }
        //public IList<DataColumn> Columns { get; set; }
        public IList<Row> RowsToCopy { get; set; }

        public Table()
        {
            this.RowsToCopy = new List<Row>();
        }
    }
}
