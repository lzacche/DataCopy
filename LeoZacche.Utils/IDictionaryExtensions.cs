using System;
using System.Text;
using System.Collections.Generic;

namespace LeoZacche.Utils
{
    public static class IDictionaryExtensions
    {
        public static void CloneFrom<TKey, TValue>(this IDictionary<TKey, TValue> listTo, IDictionary<TKey, TValue> listfrom)
        {
            listTo.Clear();

            foreach (var fromItem in listfrom)
            {
                TKey newKey = fromItem.Key;
                TValue newValue = fromItem.Value;

                listTo.Add(newKey, newValue);
            }

        }
    }
}
