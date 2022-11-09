using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
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
