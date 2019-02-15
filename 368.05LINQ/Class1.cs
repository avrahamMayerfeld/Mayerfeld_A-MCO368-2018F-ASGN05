using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._05LINQ
{
    public static class Class1
    {
        //in this method, i saw no way other than iterating through the entire list to be able to get max 
        //over previous, but it is conceivable that the user doesn't need all of the results, 
        //so the deferred execution of yield return
        //allows the user to get one at a time instead of again iterating through the entire result list.
        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> comp)
            where T : IComparable<T>
        {
            T ifMax = default(T);
            foreach (T elem in comp)
            {
                if (elem.Equals(comp.First()) || elem.CompareTo(ifMax) > 0)
                {
                    ifMax = elem;
                    yield return ifMax;
                }
            }
        }

        public static IEnumerable<T> MaxOverPrevious<T>(this IEnumerable<T> comp, Func<T, IComparable> func)
        {
            IComparable ifMax = default(IComparable);
            foreach (T elem in comp)
            {
                IComparable transformedElem = func(elem);
                if (elem.Equals(comp.First()) || transformedElem.CompareTo(ifMax) > 0)
                {
                    ifMax = transformedElem;
                    yield return elem;
                }

            }
        }
        //See first comment
        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> comp) where T : IComparable<T>
        {
            int i = 0;
            bool first = comp.ElementAt(i).CompareTo(comp.ElementAt(i + 1)) > 0;
            bool last = comp.ElementAt(i).CompareTo(comp.ElementAtOrDefault(i - 1)) > 0;
            bool middle = first && last;

            while (i < comp.Count())
            {
                if ((i == 0 && first) || (i == comp.Count() - 1 && last) || middle)
                    yield return comp.ElementAt(i);

                i++;
            }
        }

        public static IEnumerable<T> LocalMaxima<T>(this IEnumerable<T> comp, Func<T, IComparable> func)
        {
            int i = 0;
            while (i < comp.Count())
            {
                var Fn1 = func(comp.ElementAtOrDefault(i - 1));
                var F0 = func(comp.ElementAt(i));
                var F1 = func(comp.ElementAtOrDefault(i + 1));

                bool first = F0.CompareTo(F1) > 0;
                bool last = F0.CompareTo(Fn1) > 0;
                bool middle = first && last;

                if ((i == 0 && first) || (i == comp.Count() - 1 && last) || middle)
                {
                    yield return comp.ElementAt(i);
                }
                i++;

            }
        } 

        public static bool AtLeastK<T>(this IEnumerable<T> comp, int k, Func<T, bool> ALK)
        {
            return comp.Count(e => ALK(e)) >= k;
        }
            
        //here i used the Count() extension method, because at least when the IEnumerable implements ICollection,
        //the result will be obtained in 0(1) using ICollection's Count property.
        public static bool AtLeastHalf<T>(this IEnumerable<T> comp, Func<T, bool> ALH)
        {
            return comp.Count(e => ALH(e)) >= comp.Count() / 2;
        }    

    }
}
