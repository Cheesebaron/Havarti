using System;
using System.Linq;
using Cheesebaron.Havarti.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cheesebaron.Havarti.Test
{
    [TestClass]
    public class CollectionsTest
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        [TestMethod]
        public void BinaryTreeTest()
        {
            var tree = new BinaryTree<int>();
            var ints = new int[20];

            for (var i = 0; i < ints.Length; i++)
            {
                ints[i] = Random.Next(-ints.Length, ints.Length);
                tree.Add(ints[i]);
                System.Diagnostics.Debug.WriteLine("\n" + tree.ToString());
            }

            foreach (var i in ints)
                Assert.IsTrue(tree.Contains(i));
        }

        [TestMethod]
        public void RedBlackTreeTest()
        {

            /*
         * var rbTree = new RedBlackTree<int>();

            rbTree.Insert(10);
            rbTree.Insert(25);
            rbTree.Insert(7);
            rbTree.Insert(3);
            rbTree.Insert(19);
            rbTree.Insert(42);
            rbTree.Insert(1);
            rbTree.Insert(14);
            rbTree.Insert(31);
            rbTree.Insert(13);
            rbTree.Insert(2);
            rbTree.Insert(9);
            rbTree.Insert(17);
            rbTree.Insert(6);
            rbTree.Insert(11);
            rbTree.Insert(18);
            rbTree.Insert(26);
            rbTree.Insert(16);
            rbTree.Insert(27);

            Console.WriteLine(rbTree);

            Console.WriteLine();
            Console.WriteLine("Testing IEnumerable:");
            foreach (var val in rbTree)
                Console.WriteLine(val.ToString());
         * */
        }
    }

    #region TestClass from: http://www.remondo.net/generic-priority-queue-example-csharp/
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void GivenIntegersUnordered()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();
            var expected = new[] { 3, 21, 100 };

            // act
            queue.Enqueue(3);
            queue.Enqueue(100);
            queue.Enqueue(21);

            Assert.IsTrue(queue.ToArray().SequenceEqual(expected));

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();

            // act
            queue2.Enqueue(3);
            queue2.Enqueue(100);
            queue2.Enqueue(21);

            Assert.IsTrue(queue2.ToArray().SequenceEqual(expected));
        }

        [TestMethod]
        public void DequeueTest()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();
            const int expected = 21;

            // act
            queue.Enqueue(21);
            queue.Enqueue(100);
            queue.Enqueue(3);

            queue.Dequeue();
            var result = queue.Dequeue();

            Assert.AreEqual(expected, result);

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();

            // act
            queue2.Enqueue(21);
            queue2.Enqueue(100);
            queue2.Enqueue(3);

            queue2.Dequeue();
            result = queue2.Dequeue();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsEmptyTestWithNoItems()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();

            Assert.IsTrue(queue.IsEmpty);

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();

            Assert.IsTrue(queue2.IsEmpty);
        }

        [TestMethod]
        public void IsEmptyTestWithItems()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();

            // act
            queue.Enqueue(1);

            Assert.IsFalse(queue.IsEmpty);

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();

            // act
            queue2.Enqueue(1);

            Assert.IsFalse(queue2.IsEmpty);
        }

        [TestMethod]
        public void PeekTest()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();
            const int expected = 3;
            queue.Enqueue(21);
            queue.Enqueue(100);
            queue.Enqueue(3);

            // act
            var result = queue.Peek();

            Assert.AreEqual(expected, result);

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();
            queue2.Enqueue(21);
            queue2.Enqueue(100);
            queue2.Enqueue(3);

            // act
            result = queue.Peek();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GivenAnArrayOfObjectsUnordered()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<DummyStringComparer2>();

            var t1 = new DummyStringComparer2 { StringOfX = "xxx" };
            var t2 = new DummyStringComparer2 { StringOfX = "x" };
            var t3 = new DummyStringComparer2 { StringOfX = "xxxxx" };
            var t4 = new DummyStringComparer2 { StringOfX = "xx" };
            var t5 = new DummyStringComparer2 { StringOfX = "xxxx" };

            DummyStringComparer2[] expected = { t2, t4, t1, t5, t3 };
            queue.Enqueue(t1);
            queue.Enqueue(t2);
            queue.Enqueue(t3);
            queue.Enqueue(t4);
            queue.Enqueue(t5);

            // act/assert
            Assert.IsTrue(queue.ToArray().SequenceEqual(expected));

            // arrange
            var queue2 = new PriorityQueueBinaryTree<DummyStringComparer2>();

            queue2.Enqueue(t1);
            queue2.Enqueue(t2);
            queue2.Enqueue(t3);
            queue2.Enqueue(t4);
            queue2.Enqueue(t5);

            // act/assert
            Assert.IsTrue(queue2.ToArray().SequenceEqual(expected));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DequeueWithNoItemsShouldThrowException()
        {
            // arrange
            var queue = new PriorityQueueLinkedList<int>();

            // act
            queue.Dequeue();

            // arrange
            var queue2 = new PriorityQueueBinaryTree<int>();

            // act
            queue2.Dequeue();
        }
    }

    internal class DummyStringComparer2 : IComparable<DummyStringComparer2>
    {
        public string StringOfX { get; set; }
 
        #region IComparable<TestCompareClass> Members
 
        public int CompareTo(DummyStringComparer2 other)
        {
            // Return values:
            // <0: This instance smaller than obj.
            //  0: This instance occurs in the same position as obj.
            // >0: This instance larger than obj.
 
            return StringOfX.Length.CompareTo(other.StringOfX.Length);
        }
 
        #endregion
    }
    #endregion
}
