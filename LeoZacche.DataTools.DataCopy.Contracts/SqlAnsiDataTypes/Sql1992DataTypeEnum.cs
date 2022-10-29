using System;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes
{
    public enum Sql1992DataType
    {
        NaoDefinido = 0,

        // Character types
        [Description("Character")] Char = 1,
        [Description("Character varying")] Varchar,
        [Description("Character large object")] CLob,

        //National character types
        [Description("National character")] NChar,
        [Description("National character varying")] NCharVarying,
        [Description("National character large object")] NCLob,

        //Binary types
        [Description("Binary")] Binary,
        [Description("Binary varying")] VarBinary,
        [Description("Binary large object")] BLob,


        //Numeric types

        //Exact numeric types (
        [Description("")] Numeric,
        [Description("")] Decimal,
        [Description("")] SmallInt,
        [Description("")] Integer,
        [Description("")] BigInt,

        //Approximate numeric types
        [Description("")] Float,
        [Description("")] Real,
        [Description("")] DoublePrecision,




        //Datetime types
        [Description("")] Date,
        [Description("")] Time,
        [Description("")] TimeStamp,


        // Outros
        [Description("Interval type")] Interval,

    }
}
