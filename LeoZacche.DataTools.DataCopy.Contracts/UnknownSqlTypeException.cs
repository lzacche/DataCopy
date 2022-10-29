using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes;


namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class UnknownSqlTypeException : Exception
    {
        public Sql1992DataType SqlType { get; private set; }

        public UnknownSqlTypeException(Sql1992DataType type) : base($"Não sei como tratar o tipo SQL {type}.")
        {
            this.SqlType = type;
        }
    }
}
