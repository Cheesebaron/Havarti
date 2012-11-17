using System;
using System.Linq;
using System.Text;
using Cheesebaron.Havarti.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cheesebaron.Havarti.Test
{
    [TestClass]
    public class SortTest
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        private readonly int[] _intArray;
        private readonly int[] _sortedIntArray;

        private readonly string[] _stringArray;
        private readonly string[] _sortedStringArray;

        public SortTest()
        {
            _intArray = new int[5000];

            for (var i = 0; i < _intArray.Length; i++)
                _intArray[i] = Random.Next(-_intArray.Length, _intArray.Length);

            _sortedIntArray = _intArray.ToArray();
            Array.Sort(_sortedIntArray);

            _stringArray = new string[5000];

            for (var i = 0; i < _stringArray.Length; i++)
                _stringArray[i] = RandomString(Random.Next(3, 10));

            _sortedStringArray = _stringArray.ToArray();
            Array.Sort(_sortedStringArray);
        }

        private static string RandomString(int size)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        [TestMethod]
        public void BubbleSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            BubbleSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            BubbleSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void HeapSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            HeapSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            HeapSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void InsertionSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            InsertionSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            InsertionSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void QuickSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            QuickSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            QuickSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void SelectionSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            SelectionSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            SelectionSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void ShellSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            ShellSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            ShellSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        [TestMethod]
        public void MergeSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            MergeSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            MergeSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }

        /*
        [TestMethod]
        public void BogoSortTest()
        {
            var sortedIntArray = _intArray.ToArray();
            BogoSort.Sort(sortedIntArray);
            for (var i = 0; i < sortedIntArray.Length; i++)
                Assert.AreEqual(_sortedIntArray[i], sortedIntArray[i]);

            var sortedStringArray = _stringArray.ToArray();
            BogoSort.Sort(sortedStringArray);
            for (var i = 0; i < sortedStringArray.Length; i++)
                Assert.AreEqual(_sortedStringArray[i], sortedStringArray[i]);
        }
        */
    }
}
