using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class Column : IColumn
    {
        public int Ordinal { get; set; }
        public string Name { get; set; }
        public Type DataType { get; set; }
        public bool AllowNull { get; set; }
        public bool IsAutoIncrement { get; set; }
        public Sql1992DataType SqlType { get; set; }
        public string DatabaseSpecificDataType { get; set; }
        public int? MaxLength { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
        public dynamic Value { get; set; }
        public bool IsPartOfPrimaryKey { get; set; }

        public IColumn Clone()
        {
            var newCol = new Column
            {
                Ordinal = this.Ordinal,
                Name = this.Name,
                DataType = this.DataType,
                AllowNull = this.AllowNull,
                IsAutoIncrement = this.IsAutoIncrement,
                SqlType = this.SqlType,
                DatabaseSpecificDataType = this.DatabaseSpecificDataType,
                MaxLength = this.MaxLength,
                Precision = this.Precision,
                Scale = this.Scale,
                Value = this.Value,
                IsPartOfPrimaryKey = this.IsPartOfPrimaryKey,
            };

            return newCol;
        }
    }
}
