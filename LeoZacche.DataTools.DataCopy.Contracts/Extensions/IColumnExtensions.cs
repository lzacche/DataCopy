using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts.Extensions
{
    public static class IColumnExtensions
    {
        public static IList<IColumn> GetPrimaryKeyColumns(this IList<IColumn> lista)
        {
            var filtrado = lista.Where(c => c.IsPartOfPrimaryKey).ToList();

            return filtrado;
        }
        public static void CloneFrom(this IList<IColumn> listTo, IList<IColumn> listfrom)
        {
            listTo.Clear();

            foreach (var fromCol in listfrom)
            {
                var newCol = fromCol.Clone();

                listTo.Add(newCol);
            }
        }
    }
}
