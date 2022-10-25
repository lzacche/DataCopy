using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.Utils;

namespace LeoZacche.DataTools.DataCopy.Engine.Extensions
{
    public static class RowExtensions
    {
        public static void CloneFrom(this IList<Row> listTo, IList<Row> listfrom)
        {
            listTo.Clear();

            foreach(var fromRow in listfrom)
            {
                var newRow = new Row();
                newRow.PrimaryKeyColumnsValues.CloneFrom(fromRow.PrimaryKeyColumnsValues);

                listTo.Add(newRow);
            }

        }
    }
}
