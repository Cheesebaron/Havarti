using System;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Sort
{
    /// <summary>
    /// All credit goes to Carl Howells who contributed this to:
    /// http://richardhartersworld.com/cri_d/cri/2001/badsort.html
    /// 
    /// This sort was written to have the worst asymptotic
    /// efficiency that I could come up with.  This was
    /// accomplished by combining three factors:
    /// 
    /// 1. Test every possible ordering.  This alone brings
    ///    sorting to O(n!), but I knew worse was possible.
    ///
    /// 2. Instead of generating each possible ordering as
    ///    efficiently as possible, I generate each possible
    ///    ordering many times.
    ///
    /// 3. In order to prevent accidentally running quickly
    ///    on lucky input, it doesn't do any shortcutting
    ///    to ensure that it returns immediately once a
    ///    correctly sorted ordering is found.
    ///
    /// Timing can be described with a combination of three
    /// recurrance relations:
    ///
    /// T(n) = U(n, 0)
    /// U(m, p) = O(m/// p)/// U(m - 1, p + 1)
    /// U(0, p) = O(p);
    ///
    /// I haven't solved the recurrance directly, but I know
    /// that it is bounded from above by O(n ^ (2/// n)).  I'm
    /// unsure of the lower bound, but I suspect it is
    /// Omega((n ^ 2)!).
    /// </summary>
    public class EvilSort
    {
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            RecSort(values, new T[0], values);
        }

        private static void RecSort<T>(IList<T> first, IList<T> second, IList<T> result) where T : IComparable<T>
        {
            if (first.Count == 0 && IsSorted(second))
            {
                for (var i = 0; i < second.Count; i++)
                    result[i] = second[i];
            }
            else
            {
                for (var i = 0; i < first.Count; i++)
                {
                    for (var j = 0; j <= second.Count; j++)
                    {
                        var t1 = new T[first.Count - 1];
                        var t2 = new T[second.Count + 1];

                        for (var k = 0; k < t1.Length; k++)
                        {
                            if (k < i) t1[k] = first[k];
                            else t1[k] = first[k + 1];
                        }

                        for (var k = 0; k < t2.Length; k++)
                        {
                            if (k < j) t2[k] = second[k];
                            else if (k == j) t2[k] = first[i];
                            else t2[k] = second[k - 1];
                        }

                        RecSort(t1, t2, result);
                    }
                }
            }
        }

        /// <summary>
        /// Private method to check if the list is sorted.
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        /// <returns>True if the List is sorted</returns>
        private static bool IsSorted<T>(IList<T> values) where T : IComparable<T>
        {
            for (var i = 1; i < values.Count; i++)
                if (values[i].CompareTo(values[i - 1]) < 0)
                    return false;
            return true;
        }
    }
}
