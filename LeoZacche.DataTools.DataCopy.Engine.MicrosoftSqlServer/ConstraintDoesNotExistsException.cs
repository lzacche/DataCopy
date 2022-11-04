using System;
using System.Collections.Generic;
using System.Text;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer
{
    public class ConstraintDoesNotExistsException : Exception
    {
        public string ConstraintName { get; private set; }

        public ConstraintDoesNotExistsException(string constraintName) : base($"A constraint '{constraintName}' não existe")
        {
            this.ConstraintName = constraintName;
        }
    }
}
