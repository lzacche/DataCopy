using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer
{
    public class UnknownSqlTypeException : Exception
    {
        public string Typename { get; set; }

        public UnknownSqlTypeException(string typename): base($"O tipo '{typename} não é conhecido.")
        {
            this.Typename = typename;
        }
    }
}
