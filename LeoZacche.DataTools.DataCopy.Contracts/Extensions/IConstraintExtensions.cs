using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace LeoZacche.DataTools.DataCopy.Contracts.Extensions
{
    public static class IConstraintExtensions
    {

        public static void CloneFrom(this IList<IConstraintPrimaryKey> listTo, IList<IConstraintPrimaryKey> listfrom)
        {
            listTo.Clear();

            foreach (var fromCol in listfrom)
            {
                var newCol = (IConstraintPrimaryKey)fromCol.Clone();

                listTo.Add(newCol);
            }
        }
        public static void CloneFrom(this IList<IConstraintUniqueKey> listTo, IList<IConstraintUniqueKey> listfrom)
        {
            listTo.Clear();

            foreach (var fromCol in listfrom)
            {
                var newCol = (IConstraintUniqueKey)fromCol.Clone();

                listTo.Add(newCol);
            }
        }
        public static void CloneFrom(this IList<IConstraintCheck> listTo, IList<IConstraintCheck> listfrom)
        {
            listTo.Clear();

            foreach (var fromCol in listfrom)
            {
                var newCol = (IConstraintCheck)fromCol.Clone();

                listTo.Add(newCol);
            }
        }
        public static void CloneFrom(this IList<IConstraintForeignKey> listTo, IList<IConstraintForeignKey> listfrom)
        {
            listTo.Clear();

            foreach (var fromCol in listfrom)
            {
                var newCol = (IConstraintForeignKey)fromCol.Clone();

                listTo.Add(newCol);
            }
        }
    }
}
