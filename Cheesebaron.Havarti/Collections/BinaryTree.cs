#region Copyright
// <copyright file="BinaryTree.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Collections
{
    public class BinaryTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// BinaryTree node, this consists of a reference to its Left and Right child nodes
        /// and its Parent node, along with a Data representation, which is IComparable.
        /// </summary>
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public T Data { get; set; }

            /// <summary>
            /// Returns if this node is a Leaf node, meaning it does not have
            /// any childen.
            /// </summary>
            public bool IsLeaf { get { return null == Right && null == Left; } }

            /// <summary>
            /// Returns if this node is a left node of its parent.
            /// </summary>
            public bool IsLeft { get { return null != Parent && Parent.Left == this; }}

            /// <summary>
            /// Returns if this node is a right node if its parent.
            /// </summary>
            public bool IsRight { get { return null != Parent && Parent.Right == this; }}
        }

        /// <summary>
        /// Specifies the mode of scanning through the tree
        /// </summary>
        public enum TraversalMode
        {
            InOrder = 0,
            PostOrder = 1,
            PreOrder = 2
        }

        public TraversalMode TraversalOrder { get; set; }

        /// <summary>
        /// Returns the count of nodes in the tree.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Returns the root node of the tree.
        /// </summary>
        public Node Root { get; private set; }

        /// <summary>
        /// Returns the left most node in the tree (smallest value).
        /// </summary>
        public Node LeftMost
        {
            get { return null != Root ? FindMostLeft(Root) : null; }
        }

        /// <summary>
        /// Returns the right most node in the tree (biggest value).
        /// </summary>
        public Node RightMost
        {
            get { return null != Root ? FindMostRight(Root) : null; }
        }

        public BinaryTree()
        {
            Root = null;
            Count = 0;
            TraversalOrder = TraversalMode.InOrder;
        }

        /// <summary>
        /// Add a node in the tree.
        /// </summary>
        /// <param name="value">Value of the node</param>
        public void Add(T value)
        {
            var node = new Node { Data = value };
            Add(node);
        }

        /// <summary>
        /// Add a node in the tree.
        /// </summary>
        /// <param name="node">A node of type BinaryTree.Node</param>
        public void Add(Node node)
        {
            if (null == Root) //First element being added!
            {
                Root = node;
                Count++;
            }
            else
            {
                if (null == node.Parent)
                    node.Parent = Root; //Start at root

                //Insert on the left side if Node is smaller or equal to the Parent
                var insertLeftSide = node.Data.CompareTo(node.Parent.Data) <= 0;

                if (insertLeftSide) //insert on the left
                {
                    if (node.Parent.Left == null)
                    {
                        node.Parent.Left = node; //insert in left
                        Count++;
                    }
                    else
                    {
                        node.Parent = node.Parent.Left; //scan down to left child
                        Add(node); //recursive call
                    }
                }
                else //insert on the right
                {
                    if (node.Parent.Right == null)
                    {
                        node.Parent.Right = node; //insert in right
                        Count++;
                    }
                    else
                    {
                        node.Parent = node.Parent.Right;
                        Add(node);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a value from the tree and returns whether the removal was successful.
        /// </summary>
        public bool Remove(T value)
        {
            var removeNode = Find(value);

            return Remove(removeNode);
        }

        /// <summary>
        /// Removes a node from the tree and returns whether the removal was successful.
        /// </summary>>
        public bool Remove(Node removeNode)
        {
            if (null == removeNode) return false; //value doesn't exist or not of this tree

            //Note whether the node to be removed is the root of the tree
            bool wasHead = (removeNode == Root);

            if (1 == Count)
            {
                Root = null;
                Count--;
            }
            else if (removeNode.IsLeaf) //Case 1: No Children
            {
                //Remove node from its parent
                if (removeNode.IsLeft)
                    removeNode.Parent.Left = null;
                else
                    removeNode.Parent.Right = null;

                removeNode.Parent = null;

                Count--; //decrease total element count
            }
            else if ((null != removeNode.Right && null == removeNode.Left) 
                || (null != removeNode.Left && null == removeNode.Right)) //Case 2: One Child
            {
                if (null != removeNode.Left)
                {
                    //Put left child node in place of the node to be removed
                    removeNode.Left.Parent = removeNode.Parent; //update parent

                    if (wasHead)
                        Root = removeNode.Left; //update root reference if needed

                    if (removeNode.IsLeft) //update the parent's child reference
                        removeNode.Parent.Left = removeNode.Left;
                    else
                        removeNode.Parent.Right = removeNode.Left;
                }
                else //Has right child
                {
                    //Put left node in place of the node to be removed
                    if (removeNode.Right != null)
                    {
                        removeNode.Right.Parent = removeNode.Parent; //update parent

                        if (wasHead)
                            Root = removeNode.Right; //update root reference if needed

                        if (removeNode.IsLeft) //update the parent's child reference
                            removeNode.Parent.Left = removeNode.Right;
                        else
                            removeNode.Parent.Right = removeNode.Right;
                    }
                }

                removeNode.Parent = null;
                removeNode.Left = null;
                removeNode.Right = null;

                Count--; //decrease total element count
            }
            else //Case 3: Two Children
            {
                //Find inorder predecessor (right-most node in left subtree)
                var successorNode = removeNode.Left;
                if (null != successorNode)
                {
                    while (successorNode.Right != null)
                    {
                        successorNode = successorNode.Right;
                    }

                    removeNode.Data = successorNode.Data; //replace value

                    Remove(successorNode); //recursively remove the inorder predecessor
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the first node in the tree with the value
        /// </summary>
        public Node Find(T value)
        {
            var iterator = Root;
            while (iterator != null)
            {
                var compare = value.CompareTo(iterator.Data);
                // Did we find the value ?
                if (compare == 0) return iterator;
                if (compare < 0)
                {
                    // Travel left
                    iterator = iterator.Left;
                    continue;
                }
                // Travel right
                iterator = iterator.Right;
            }
            return null;
        }

        /// <summary>
        /// Returns whether a value is stored in the tree
        /// </summary>
        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        private static Node FindMostLeft(Node start)
        {
            var node = start;
            while (true)
            {
                if (null != node.Left)
                {
                    node = node.Left;
                    continue;
                }
                break;
            }
            return node;
        }

        private static Node FindMostRight(Node start)
        {
            var node = start;
            while (true)
            {
                if (null != node.Right)
                {
                    node = node.Right;
                    continue;
                }
                break;
            }
            return node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            switch (TraversalOrder)
            {
                case TraversalMode.PostOrder:
                    return new PostOrderEnumerator(this);
                case TraversalMode.PreOrder:
                    return new PreOrderEnumerator(this);
                default:
                    return new InOrderEnumerator(this);
            }
        }

        /// <summary>
        /// Returns an inorder-traversal enumerator for the tree values
        /// 
        /// An inorder traversal visits the left child first (recursively), 
        /// then visists the parent node, and visits the right child last.
        /// This gives the values in an ascending order.
        /// </summary>
        class InOrderEnumerator : IEnumerator<T>
        {
            private Node _current;
            private BinaryTree<T> _tree;
            private readonly Queue<Node> _traverseQueue;

            public InOrderEnumerator(BinaryTree<T> tree)
            {
                _tree = tree;

                //Build queue
                _traverseQueue = new Queue<Node>();
                VisitNode(_tree.Root);
            }

            private void VisitNode(Node node)
            {
                if (node == null)
                    return;

                VisitNode(node.Left);
                _traverseQueue.Enqueue(node);
                VisitNode(node.Right);
            }

            public T Current
            {
                get { return _current.Data; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _current = null;
                _tree = null;
            }

            public void Reset()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _traverseQueue.Count > 0 ? _traverseQueue.Dequeue() : null;

                return (_current != null);
            }
        }

        /// <summary>
        /// Returns a postorder-traversal enumerator for the tree's values
        /// 
        /// A preorder traversal visits the parent node first, then left child,
        /// and then the right child.
        /// </summary>
        class PostOrderEnumerator : IEnumerator<T>
        {
            private Node _current;
            private BinaryTree<T> _tree;
            private readonly Queue<Node> _traverseQueue;

            public PostOrderEnumerator(BinaryTree<T> tree)
            {
                _tree = tree;

                //Build queue
                _traverseQueue = new Queue<Node>();
                VisitNode(_tree.Root);
            }

            private void VisitNode(Node node)
            {
                if (node == null)
                    return;

                VisitNode(node.Left);
                VisitNode(node.Right);
                _traverseQueue.Enqueue(node);
            }

            public T Current
            {
                get { return _current.Data; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _current = null;
                _tree = null;
            }

            public void Reset()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _traverseQueue.Count > 0 ? _traverseQueue.Dequeue() : null;

                return (_current != null);
            }
        }

        /// <summary>
        /// Returns an preorder-traversal enumerator for the tree values.
        /// 
        /// A postorder traversal visits the left child first, then the right child,
        /// and then the parent node.
        /// </summary>
        class PreOrderEnumerator : IEnumerator<T>
        {
            private Node _current;
            private BinaryTree<T> _tree;
            readonly Queue<Node> _traverseQueue;

            public PreOrderEnumerator(BinaryTree<T> tree)
            {
                _tree = tree;

                //Build queue
                _traverseQueue = new Queue<Node>();
                VisitNode(_tree.Root);
            }

            private void VisitNode(Node node)
            {
                if (node == null)
                    return;
                _traverseQueue.Enqueue(node);
                VisitNode(node.Left);
                VisitNode(node.Right);
            }

            public T Current
            {
                get { return _current.Data; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _current = null;
                _tree = null;
            }

            public void Reset()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _traverseQueue.Count > 0 ? _traverseQueue.Dequeue() : null;

                return (_current != null);
            }
        }
    }
}
