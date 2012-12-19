using System;
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
            var ints = new int[5000];

            for (var i = 0; i < ints.Length; i++)
            {
                ints[i] = Random.Next(-ints.Length, ints.Length);
                tree.Add(ints[i]);
            }

            foreach (var i in ints)
                Assert.IsTrue(tree.Contains(i));

            //TODO Make more tests!!
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
}
