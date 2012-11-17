#region Copyright
// <copyright file="InsertionSort.cs">
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
    /// Insertion sort is a simple sorting algorithm. A lot similar to the method
    /// used when humans manually sort something. I.e. a deck of cards.
    /// The algorithm runs through all elements in the list. An element which is
    /// currently being sorted is compared to the value of the element left to it.
    /// If the value of the element is smaller than the one to the left of it, their
    /// positions are swapped. This is repeated until no smaller value is found left
    /// to it, and repeated on the rest of the list.
    /// 
    /// Watch http://www.youtube.com/watch?v=ROalU379l3U for a visualization of the
    /// algorithm in action.
    /// 
    /// Worst case performance: O(n^2)
    /// Best case performance: O(n)
    /// Average case performance: O(n^2)
    /// </summary>
    public static class InsertionSort
    {
        /// <summary>
        /// Insertion Sort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values)  where T : IComparable<T>
        {
            for (var i = 1; i < values.Count; i++)
            {
                var j = i;
                var temp = values[i];
                
                while ((j > 0) && (values[j - 1].CompareTo(temp) > 0))
                {
                    values[j] = values[j - 1];
                    j--;
                }

                values[j] = temp;
            }
        }
    }
}
