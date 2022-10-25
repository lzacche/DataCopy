using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Engine.Extensions
{
    public static class TableExtensions
    {
        public static void CloneFrom(this IList<Table> listTo, IList<Table> listfrom)
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
