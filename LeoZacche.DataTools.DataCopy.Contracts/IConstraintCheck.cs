using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IConstraintCheck : IConstraintBase
    {
        string Condition { get; set; }
    }
}
