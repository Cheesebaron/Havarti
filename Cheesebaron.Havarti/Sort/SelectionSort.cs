#region Copyright
// <copyright file="SelectionSort.cs">
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
    /// Selection sort is similar to the Insertion sort algorithm. It works by
    /// finding the minimum value in the list, swap it with the value in the first
    /// position. Repeat that for the remaining elements in the list.
    /// 
    /// Worst case performance: O(n^2)
    /// Best case performance: O(n^2)
    /// Average case performance: O(n^2)
    /// </summary>
    public static class SelectionSort
    {
        /// <summary>
        /// Selection Sort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            // Go through all elements in the list
            for (var i = 0; i < values.Count - 1; i++)
            {
                // Assume first element is the min
                var min = i;

                // Test against elements after i to find the smallest
                for (var j = i + 1; j < values.Count; j++)
                    // If this element is less, then it is the new minimum
                    if (values[j].CompareTo(values[min]) < 0)
                        min = j;

                // If min is i continue
                if (min == i) continue;

                // Otherwise swap min with i
                var temp = values[i];
                values[i] = values[min];
                values[min] = temp;
            }
        }
    }
}
