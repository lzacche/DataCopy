using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes;


namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IColumn
    {
        public int Ordinal { get; }
        public string Name { get; set; }
        public Type DataType { get; set; }
        public bool AllowNull { get; set; }


        public bool IsAutoIncrement { get; set; }
        public Sql1992DataType SqlType { get; set; }
        public string DatabaseSpecificDataType { get; set; }
        public int? MaxLength { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }

        public bool IsPartOfPrimaryKey { get; set; }

        public dynamic Value { get; set; }

        public IColumn Clone();
    }
}
