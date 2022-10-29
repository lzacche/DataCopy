using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.Utils;
using LeoZacche.DataTools.DataCopy.Contracts.SqlAnsiDataTypes;


namespace LeoZacche.DataTools.DataCopy.Contracts.Extensions
{
    public static class Sql1992DataTypeExtensions
    {
        public static Sql1992DataType ConvertFrom(string sql1992TypeName)
        {
            Sql1992DataType x = TypeUtil.ConvertTo<Sql1992DataType>(sql1992TypeName);
            return x;
        }
        public static Type GetType(string sql1992TypeName)
        {
            Type type = null;
            var sqltype = Sql1992DataTypeExtensions.ConvertFrom(sql1992TypeName);
            switch (sqltype){

                case Sql1992DataType.Char:
                case Sql1992DataType.Varchar:
                case Sql1992DataType.CLob:
                case Sql1992DataType.NChar:
                case Sql1992DataType.NCharVarying:
                case Sql1992DataType.NCLob:
                    type = typeof(string);
                    break;

                case Sql1992DataType.SmallInt:
                case Sql1992DataType.Integer:
                case Sql1992DataType.BigInt:
                    type = typeof(int);
                    break;

                case Sql1992DataType.Binary:
                case Sql1992DataType.VarBinary:
                case Sql1992DataType.BLob:
                    type = typeof(byte[]);
                    break;

                case Sql1992DataType.Decimal:
                case Sql1992DataType.Numeric: // TODO: avaliar escala e precisão, pois poderia ser convertido para int
                    type = typeof(decimal);
                    break;

                case Sql1992DataType.Float:
                case Sql1992DataType.Real:
                case Sql1992DataType.DoublePrecision:
                    type = typeof(float);
                    break;

                case Sql1992DataType.Date:
                case Sql1992DataType.Time:
                case Sql1992DataType.TimeStamp:
                    type = typeof(DateTime);
                    break;

                case Sql1992DataType.Interval:
                    type = typeof(TimeSpan);
                    break;

                default:
                    throw new UnknownSqlTypeException(sqltype);
            }
            return type;
        }
    }
}
