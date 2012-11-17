#region Copyright
// <copyright file="HeapSort.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

using System;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Sort
{
    /// <summary>
    /// TODO summary
    /// 1. Build heap
    /// 2. Sort
    /// 
    /// Worst case performance: O(n log n)
    /// Best case performance: O(n log n)
    /// Average case performance: O(n log n)
    /// </summary>
    public static class HeapSort
    {
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            for (var i = (values.Count / 2) - 1; i >= 0; i--)
                SiftDown(values, i, values.Count - 1);

            for (var i = values.Count - 1; i >= 1; i--)
            {
                var temp = values[0];
                values[0] = values[i];
                values[i] = temp;
                SiftDown(values, 0, i - 1);
            }
        }

        private static void SiftDown<T>(IList<T> values, int root, int bottom) where T : IComparable<T>
        {
            var done = false;

            while ((root*2 <= bottom) && (!done))
            {
                int maxChild;
                if (root*2 == bottom)
                    maxChild = root*2;
                else if (values[root * 2].CompareTo(values[root * 2 + 1]) > 0)
                    maxChild = root*2;
                else
                    maxChild = root*2 + 1;

                if (values[root].CompareTo(values[maxChild]) < 0)
                {
                    var temp = values[root];
                    values[root] = values[maxChild];
                    values[maxChild] = temp;
                    root = maxChild;
                }
                else
                    done = true;
            }
        }
    }
}
