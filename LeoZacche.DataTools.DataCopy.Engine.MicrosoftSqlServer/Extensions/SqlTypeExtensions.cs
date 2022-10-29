using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using LeoZacche.Utils;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.Extensions
{
    public static class SqlTypeExtensions
    {
        public static SqlDbType GeSqlTypeFromTitle(string typeTitle)
        {
            return TypeUtil.ConvertTo<SqlDbType>(typeTitle);
            //SqlDbType resultado = null;

            //switch (typeTitle)
            //{

            //    case "smallint":
            //        resultado = typeof(short);
            //        break;

            //    case "int":
            //        resultado = typeof(int);
            //        break;

            //    case "bigint":
            //        resultado = typeof(long);
            //        break;

            //    case "date":
            //    case "datetime":
            //        resultado = typeof(DateTime);
            //        break;

            //    case "varchar":
            //        resultado = typeof(string);
            //        break;

            //    case "numeric":
            //        if (scale != null && scale > 0)
            //            resultado = typeof(decimal);
            //        else
            //        {
            //            if (precision != null && precision > 5)
            //                resultado = typeof(long);
            //            else
            //                resultado = typeof(int);
            //        }
            //        break;


            //    default:
            //        throw new Exception($"Não sei como tratar o tipo '{databaseType}'.");
            //}

            //return resultado;
        }

        public static Type ConvertoToType(string databaseType)
        {
            Type resultado = null;
            int? precision = null,
                 scale = null;

            var parentesesPosition = databaseType.IndexOf('(');
            var temParenteses = parentesesPosition >= 0;

            var typeTitle = temParenteses ? databaseType.Substring(0, parentesesPosition).Trim() : databaseType;
            if (temParenteses)
            {
                var length = databaseType.Substring(parentesesPosition).Replace("(", "").Replace(")", "");
                var partes = length.Split(new char[] { ',' });
                precision = TypeUtil.ConvertTo<int?>(partes[0]);
                if (partes.Length > 1)
                    scale = TypeUtil.ConvertTo<int?>(partes[1]);
            }

            switch (typeTitle)
            {

                case "smallint":
                    resultado = typeof(short);
                    break;

                case "int":
                    resultado = typeof(int);
                    break;

                case "bigint":
                    resultado = typeof(long);
                    break;

                case "date":
                case "datetime":
                    resultado = typeof(DateTime);
                    break;

                case "varchar":
                    resultado = typeof(string);
                    break;

                case "numeric":
                    if (scale != null && scale > 0)
                        resultado = typeof(decimal);
                    else
                    {
                        if (precision != null && precision > 5)
                            resultado = typeof(long);
                        else
                            resultado = typeof(int);
                    }
                    break;


                default:
                    throw new Exception($"Não sei como tratar o tipo '{databaseType}'.");
            }

            return resultado;
        }

    }
}
