#region Copyright
// <copyright file="ShellSort.cs">
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
    /// Shellsort is a sorting algorithm named after its creator, Donald Shell, who
    /// published it in 1959. Shellsort is practically an insertion sort with the
    /// difference in starting with elements that are far away (defined by the gap),
    /// and decreasing the gap on each iteration. The algorithm depends very much
    /// on the gap sequence used.
    /// 
    /// Worst case performance: depends on gap
    /// Best case performance: depends on gap
    /// Average case performance: depends on gap
    /// </summary>
    public static class ShellSort
    {
        /// <summary>
        /// Shellsort with Shell's simple gap sequence N/2^k. See: 
        /// http://en.wikipedia.org/wiki/Shellsort#Gap_sequences for other gap
        /// sequences.
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        /// <param name="gap">Starting gap</param>
        public static void Sort<T>(IList<T> values, int gap = 3) where T : IComparable<T>
        {
            while (gap > 0)
            {
                for (var i = 0; i < values.Count; i++)
                {
                    var j = i;
                    var temp = values[i];

                    while ((j >= gap) && (values[j - gap].CompareTo(temp) > 0))
                    {
                        values[j] = values[j - gap];
                        j = j - gap;
                    }

                    values[j] = temp;
                }

                if (gap / 2 != 0)
                    gap = gap / 2;
                else if (gap == 1)
                    gap = 0;
                else
                    gap = 1;
            }
        }
    }
}
