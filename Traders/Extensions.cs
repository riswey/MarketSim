using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traders
{

    public static class Extensions
    {
        public static List<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /*
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }
        */
        public static double Median(this IEnumerable<double> source)
        {
            if (source.Count() == 0)
            {
                throw new InvalidOperationException("Cannot compute median for an empty set.");
            }

            var sortedList = from number in source
                                orderby number
                                select number;

            int itemIndex = (int)sortedList.Count() / 2;

            if (sortedList.Count() % 2 == 0)
            {
                // Even number of items.  
                return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
            }
            else
            {
                // Odd number of items.  
                return sortedList.ElementAt(itemIndex);
            }
        }

        public static double Median<T>(this IEnumerable<T> numbers, Func<T, double> selector)
        {
            return (from num in numbers select selector(num)).Median();
        }

        public static List<Tuple<T,T>> AllPairs<T>(this List<T> list)
        {
            var ret = new List<Tuple<T, T>>();

            list.ForEach(e1 =>
            {
                list.ForEach(e2 =>
                {
                    if (!e1.Equals(e2)) ret.Add(new Tuple<T,T>(e1, e2) );
                });
            });

            return ret;
        }

        public static List<Tuple<T, T>> AllPairsTriangle<T>(this List<T> list)
        {
            var ret = new List<Tuple<T, T>>();

            for(int i = 0; i<list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    ret.Add(new Tuple<T, T>(list[i], list[j]));
                }
            }

            return ret;
        }

        //Warning. In function programming do not want side-effects. This has side-effects.
        public static void Loop(int repeatCount, Action<int> action)
        {
            for (int i = 0; i < repeatCount; i++)
                action(i);
        }

    }
}
