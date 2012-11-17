#region Copyright
// <copyright file="QuickSort.cs">
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
    /// Quicksort is also known as partition-exchange sort and is a divide and conquer
    /// algorithm. It starts by dividing the list to be sorted into two smaller sub-lists
    /// these are then recursively sorted.
    /// 
    /// Worst case performance: O(n^2)
    /// Best case performance: O(n)
    /// Average case performance: O(n log n)
    /// </summary>
    public static class QuickSort
    {
        /// <summary>
        /// Quick Sort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            Sorting(values, 0, values.Count - 1);
        }

        private static void Sorting<T>(IList<T> values, int left, int right) where T : IComparable<T>
        {
            int i = left, j = right;
            var pivot = values[(left + right) / 2];

            while (i <= j)
            {
                while (values[i].CompareTo(pivot) < 0)
                    i++;

                while (values[j].CompareTo(pivot) > 0)
                    j--;

                if (i > j) continue;

                // Swap
                var tmp = values[i];
                values[i] = values[j];
                values[j] = tmp;

                i++;
                j--;
            }

            // Recursive calls
            if (left < j)
                Sorting(values, left, j);

            if (i < right)
                Sorting(values, i, right);
        }
    }
}
