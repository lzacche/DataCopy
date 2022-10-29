using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes
{
    public enum Sql2016DataType // : Sql2011DataTypeEnum
    {
        [Description("Decimal floating-point")] DecFloat,
        [Description("")] Json,
    }
}
