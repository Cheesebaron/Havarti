using System;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Sort
{
    /// <summary>
    /// Merge sort is a divide and conquer algorithm and was invented by John
    /// von Neuman in 1945. It works by dividing the unsorted List into N sub-
    /// lists, each with 1 element. Then repeatedly merge sub-lists to make new
    /// sub-lists until there is only 1 remaining, which is the sorted list.
    /// 
    /// Imagine it as kind a zipper. The teeth are the unsorted items, and the slider
    /// is the sorting algorithm. First when you open the zipper it divides the,
    /// unsorted items. Sliding it back orderes the items merging the teeth together.
    /// 
    /// Worst case performance: O(n log n)
    /// Best case performance: O(n log n)
    /// Average case performance: O(n log n)
    /// </summary>
    public static class MergeSort
    {
        /// <summary>
        /// Merge Sort
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        public static void Sort<T>(IList<T> values) where T : IComparable<T>
        {
            MSort(values, 0, values.Count - 1);
        }

        /// <summary>
        /// Method that divides the input list into sub-lists recursively and calls
        /// Merge to merge the sub-list together again.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void MSort<T>(IList<T> values, int left, int right) where T : IComparable<T>
        {
            if (right <= left) return;

            var mid = (right + left)/2;
            MSort(values, left, mid);
            MSort(values, (mid + 1), right);

            Merge(values, left, (mid + 1), right);
        }

        /// <summary>
        /// This function merges the left and right sub lists
        /// </summary>
        /// <typeparam name="T">Element that implements IComparable</typeparam>
        /// <param name="values">List of elements that implements IList</param>
        /// <param name="left">Index of the left element</param>
        /// <param name="mid">Index of the mid element</param>
        /// <param name="right">Index of the right element</param>
        private static void Merge<T>(IList<T> values, int left, int mid, int right) where T : IComparable<T>
        {
            var leftEnd = (mid - 1);
            var tempPos = left;
            var numElements = (right - left + 1);
            var temp = new T[values.Count];

            while((left <= leftEnd) && (mid <= right))
            {
                if (values[left].CompareTo(values[mid]) <= 0)
                    temp[tempPos++] = values[left++];
                else
                    temp[tempPos++] = values[mid++];
            }

            while (left <= leftEnd)
                temp[tempPos++] = values[left++];

            while (mid <= right)
                temp[tempPos++] = values[mid++];

            for (var i = 0; i < numElements; i++)
            {
                values[right] = temp[right];
                right--;
            }
        }
    }
}
