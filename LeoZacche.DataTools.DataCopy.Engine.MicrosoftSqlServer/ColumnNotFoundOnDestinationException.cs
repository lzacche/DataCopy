using System;
using System.Collections.Generic;
using System.Text;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer
{
    public class ColumnNotFoundException : Exception
    {
        public string ColumnName { get; private set; }

        public ColumnNotFoundException(string columnName) : base($"The column '{columnName}' was not found.")
        {
            this.ColumnName = columnName;
        }
    }
}
