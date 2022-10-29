using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;

namespace LeoZacche.DataTools.DataCopy.Contracts.Extensions
{
    public static class ITableExtensions
    {
        public static void CloneFrom(this IList<ITable> listTo, IList<ITable> listfrom)
        {
            listTo.Clear();

            foreach(var fromTable in listfrom)
            {
                var newTable = new Table() { Name = fromTable.Name };
                newTable.RowsToCopy.CloneFrom(fromTable.RowsToCopy);

                listTo.Add(newTable);
            }

        }
    }
}
