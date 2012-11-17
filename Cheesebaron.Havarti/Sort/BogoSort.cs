#region Copyright
// <copyright file="BogoSort.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cheesebaron.Havarti.Sort
{
    /// <summary>
    /// Bogosort also known as stupid sort or slowsort is a very ineffective sorting
    /// algorithm. It is not very useful and works by shuffling the List of elements
    /// until it gets sorted.
    /// Imagine a deck of cards, to sort the deck you throw it in the air and pick the
    /// cards up, then check if they are all in the correct order, if not you just throw
    /// it again and keep doing that until they are sorted.
    /// 
    /// This algorithm takes very long time to complete, even on small data sets with only
    /// 30 or less elements, so be patient!
    /// 
    /// Worst case performance: Unbounded
    /// Best case performance: O(n)
    /// Average case performance: O(n x n!)
    /// </summary>
    public static class BogoSort
    {
        /// <summary>
        /// Bogosort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            if (!values.Any()) return;

            do
            {
                var random = new Random();

                for (var i = 0; i < values.Count; i++)
                {
                    var randomPosition = random.Next(values.Count);

                    var temp = values[i];
                    values[i] = values[randomPosition];
                    values[randomPosition] = temp;
                }
            } while (!IsSorted(values));
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
