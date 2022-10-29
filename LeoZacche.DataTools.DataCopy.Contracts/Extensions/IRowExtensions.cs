using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.Utils;


namespace LeoZacche.DataTools.DataCopy.Contracts.Extensions
{
    public static class IRowExtensions
    {
        public static void CloneFrom(this IList<IRow> listTo, IList<IRow> listfrom)
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
