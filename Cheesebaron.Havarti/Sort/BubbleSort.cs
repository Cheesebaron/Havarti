#region Copyright
// <copyright file="BubbleSort.cs">
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
    /// Bubble Sort is a very simple sorting algorithm, which works by iterating through
    /// the list of elements to be sorted. Each of the elements are compared to all the 
    /// other elements in the list and is swapped into the correct position.
    /// 
    /// Worst case performance: O(n^2)
    /// Best case performance: O(n)
    /// Average case performance: O(n^2)
    /// </summary>
    public static class BubbleSort
    {
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            foreach (var t in values)
            {
                for (var j = 0; j < values.Count - 1; j++)
                {
                    if (values[j].CompareTo(values[j + 1]) <= 0) continue;

                    // Swap values
                    var hold = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = hold;
                }
            }
        }
    }
}
