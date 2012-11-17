#region Copyright
// <copyright file="RedBlackTree.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
//
// Implementation based on Jack Altieres Red-Black Tree
// http://www.jaltiere.com/index.php/2008/12/08/implementing-a-red-black-tree-in-c/
// http://www.jaltiere.com/index.php/2008/12/22/red-black-tree-revisited-deleting-nodes/
//
// </copyright>
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Collections
{
    public class RedBlackTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public NodeColor Color { get; set; }
            public T Data { get; set; }
            public bool IsDeleted { get; set; }

            public NodeDirection ParentDirection
            {
                get
                {
                    if (null == Parent || Data.CompareTo(Parent.Data) > 0)
                        return NodeDirection.Left;
                    return NodeDirection.Right;
                }
            }
            
            public bool IsRoot
            {
                get { return Parent == null; }
            }

            public bool IsLeaf
            {
                get { return Left == null && Right == null; }
            }
        }

        public enum NodeDirection
        {
            Left = 0,
            Right = 1
        }

        public enum NodeColor
        {
            Red = 0,
            Black = 1
        }

        public Node Root { get; private set; }
        public int NodeCount { get; private set; }
        public int DeletedNodesCount { get; private set; }
        public bool IsEmpty { get { return null == Root; } }

        public Node LeftMostNode
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("Error: Cannot determine leftmost node of an empty tree");

                var node = Root;
                while (null != node.Left)
                    node = node.Left;

                return node;
            }
        }

        public Node RightMostNode
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("Error: Cannot determine leftmost node of an empty tree");

                var node = Root;
                while (null != node.Right)
                    node = node.Right;

                return node;
            }
        }

        public bool Contains(T value)
        {
            if (IsEmpty)
                return false;

            var current = Root;
            while (null != current)
            {
                switch (value.CompareTo(current.Data))
                {
                    case -1:
                        current = current.Left;
                        break;
                    case 1:
                        current = current.Right;
                        break;
                    default:
                        return !current.IsDeleted;
                }
            }

            //Value wasn't found
            return false;
        }

        public void Insert(T value)
        {
            if (null == Root)
            {
                var node = new Node
                               {
                                   Parent = null, 
                                   Color = NodeColor.Black, 
                                   Data = value
                               };
                Root = node;
                NodeCount++;
            }
            else
            {
                Insert(value, Root);
            }
        }

        private void Insert(T value, Node current)
        {
            if (value.CompareTo(current.Data) == -1)
            {
                if (null == current.Left)
                {
                    var node = new Node
                                   {
                                       Color = NodeColor.Red,
                                       Parent = current,
                                       Data = value
                                   };
                    current.Left = node;
                    NodeCount++;
                }
                else
                {
                    Insert(value, current.Left);
                    return;
                }
            }
            else if (value.CompareTo(current.Data) == 1)
            {
                if (null == current.Right)
                {
                    var node = new Node
                                   {
                                       Color = NodeColor.Red,
                                       Parent = current,
                                       Data = value
                                   };
                    current.Right = node;
                    NodeCount++;
                }
                else
                {
                    Insert(value, current.Right);
                    return;
                }
            }

            CheckNode(current);
            Root.Color = NodeColor.Black;
        }

        public void DeleteNode(T value)
        {
            // Can't delete from an empty tree.
            if (IsEmpty)
                return;

            var current = Root;
            var valid = true;
            while (valid && current != null)
            {
                switch (value.CompareTo(current.Data))
                {
                    case -1:
                        current = current.Left;
                        break;
                    case 1:
                        current = current.Right;
                        break;
                    default:
                        SoftDelete(current);
                        valid = false;
                        break;
                }
            }
        }

        private void SoftDelete(Node current)
        {
            // Mark the node as deleted.
            current.IsDeleted = true;
            DeletedNodesCount++;
            NodeCount--;

            // Trim the tree if needed.
            if (DeletedNodesCount >= NodeCount / 2)
                CleanupTree();
        }

        private void CleanupTree()
        {
            // This will be a list of all non-deleted nodes used to rebuild the tree.
            var validNodes = new List<Node>();

            // This is how I will make sure I visit every node. 
            var nodeQueue = new Queue<Node>();
            nodeQueue.Enqueue(Root);

            while (nodeQueue.Count > 0)
            {
                var current = nodeQueue.Dequeue();
                // First, add the children of this node to the queue.
                if (current.Left != null)
                    nodeQueue.Enqueue(current.Left);

                if (current.Right != null)
                    nodeQueue.Enqueue((current.Right));

                // Add the node to the list of valid nodes if it has not been deleted.
                if (!current.IsDeleted)
                    validNodes.Add(current);
            }

            // The queue is empty at this point, so we have visisted every node.
            // Now we need to rebuild the tree.
            Root = null;
            NodeCount = 0;
            DeletedNodesCount = 0;
            foreach (var node in validNodes)
                Insert(node.Data);
        }

        private void CheckNode(Node current)
        {
            if (null == current)
                return;

            if (current.Color != NodeColor.Red)
                return;

            var uncleNode = GetSiblingNode(current);
            if (null != uncleNode && uncleNode.Color == NodeColor.Red)
            {
                uncleNode.Color = NodeColor.Black;
                current.Color = NodeColor.Black;
                current.Parent.Color = NodeColor.Red;

                if (null != current.Parent.Parent &&
                    current.Parent.Parent.Data.CompareTo(Root.Data) != 0)
                {
                    var node = current.Parent.Parent;
                    CheckNode(node);
                }
            }
            else
            {
                var redChild =
                    (null != current.Left && current.Left.Color == NodeColor.Red) 
                    ? NodeDirection.Left : NodeDirection.Right;

                if (NodeDirection.Left == redChild)
                {
                    if (NodeDirection.Right == current.ParentDirection)
                    {
                        RotateLeftChildRightParent(current);
                    }
                    else
                    {
                        RotateLeftChildLeftParent(current);
                    }
                }
                else
                {
                    if (NodeColor.Red == current.Right.Color)
                    {
                        if (NodeDirection.Right == current.ParentDirection)
                        {
                            RotateRightChildRightParent(current);
                        }
                        else
                        {
                            RotateRightChildLeftParent(current);
                        }
                    }
                }
            }
        }

        private static Node GetSiblingNode(Node current)
        {
            if (null == current || null == current.Parent)
                return null;

            if (null != current.Parent.Left
                && current.Parent.Left.Data.CompareTo(current.Data) == 0)
                return current.Parent.Right;

            return current.Parent.Left;
        }

        private static void FixChildColors(Node current)
        {
            if (current.Color != NodeColor.Red) return;

            if (null != current.Left && current.Left.Color == NodeColor.Black)
            {
                current.Left.Color = NodeColor.Red;
                current.Color = NodeColor.Black;
            }
            else if (null != current.Right && current.Right.Color == NodeColor.Black)
            {
                current.Right.Color = NodeColor.Red;
                current.Color = NodeColor.Black;
            }
        }

        private void RotateRightChildRightParent(Node current)
        {
            if (current.IsRoot)
                return;

            var tmpNode = current.Right.Left;
            current.Right.Parent = current.Parent;
            current.Parent.Left = current.Right;
            current.Parent = current.Right;
            current.Right.Left = current;

            if (null != tmpNode)
            {
                current.Right = tmpNode;
                tmpNode.Parent = current;
            }
            else
                current.Right = tmpNode;

            var newCurrent = current.Parent;
            CheckNode(newCurrent);
        }

        private void RotateLeftChildLeftParent(Node current)
        {
            // Don't rotate on the root.
            if (current.IsRoot)
                return;

            var tmpNode = current.Left.Right;
            current.Left.Parent = current.Parent;
            current.Parent.Right = current.Left;
            current.Parent = current.Left;
            current.Left.Right = current;

            if (tmpNode != null)
            {
                current.Left = tmpNode;
                tmpNode.Parent = current;
            }
            else
                current.Left = tmpNode;

            // The new node to check is the parent node.
            var newCurrent = current.Parent;
            CheckNode(newCurrent);
        }

        private void RotateLeftChildRightParent(Node current)
        {
            // Don't rotate on the root.
            if (current.IsRoot)
                return;

            if (current.Right != null)
            {
                current.Parent.Left = current.Right;
                current.Right.Parent = current.Parent;
            }
            else
                current.Parent.Left = current.Right;

            var tmpNode = current.Parent.Parent;
            current.Right = current.Parent;
            current.Parent.Parent = current;

            if (tmpNode == null)
            {
                Root = current;
                current.Parent = null;
            }
            else
            {
                current.Parent = tmpNode;

                // Make sure we have the pointer from the parent.
                if (tmpNode.Data.CompareTo(current.Data) > 0)
                    tmpNode.Left = current;
                else
                    tmpNode.Right = current;
            }

            FixChildColors(current);

            // The new node to check is the parent node.
            var newCurrent = current.Parent;
            CheckNode(newCurrent);
        }

        private void RotateRightChildLeftParent(Node current)
        {
            // Don't rotate on the root.
            if (current.IsRoot)
                return;

            if (current.Left != null)
            {
                current.Parent.Right = current.Left;
                current.Left.Parent = current.Parent;
            }
            else
            {
                current.Parent.Right = current.Left;
            }

            var tmpNode = current.Parent.Parent;
            current.Left = current.Parent;
            current.Parent.Parent = current;

            if (tmpNode == null)
            {
                Root = current;
                current.Parent = null;
            }
            else
            {
                current.Parent = tmpNode;

                // Make sure we have the pointer from the parent.
                if (tmpNode.Data.CompareTo(current.Data) > 0)
                    tmpNode.Left = current;
                else
                    tmpNode.Right = current;
            }

            FixChildColors(current);

            // The new node to check is the parent node.
            var newCurrent = current.Parent;
            CheckNode(newCurrent);
        }

        private static IEnumerable<T> InOrderTraversal(Node node)
        {
            if (node.Left != null)
            {
                foreach (var nodeVal in InOrderTraversal(node.Left))
                    yield return nodeVal;
            }

            yield return node.Data;

            if (node.Right != null)
            {
                foreach (var nodeVal in InOrderTraversal(node.Right))
                    yield return nodeVal;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static string PrintSubTree(Node node, string prefixNode, string prefixChildren)
        {
            if (node == null)
                return "";

            // Red nodes marked as "@@", black nodes as "..".
            var returnString = string.Format("{0}{1} {2}", prefixNode, node.Color == NodeColor.Red ? "@@" : "..", node.Data);

            var left = PrintSubTree(node.Left, prefixChildren + "|-L-", prefixChildren + "|  ");
            var right = PrintSubTree(node.Right, prefixChildren + "|-R-", prefixChildren + "   ");

            returnString += !string.IsNullOrEmpty(left) ? "\n" + left : "";
            returnString += !string.IsNullOrEmpty(right) ? "\n" + right : "";

            return returnString;
        }

        public override string ToString()
        {
            return PrintSubTree(Root, "", "");
        }
    }
}
