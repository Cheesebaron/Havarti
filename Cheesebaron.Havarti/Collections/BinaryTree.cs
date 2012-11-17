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
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public T Data { get; set; }
        }

        public Node Root { get; private set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Add(T value)
        {
            var child = new Node { Data = value };

            if (null == Root)
            {
                Root = child;
            }
            else
            {
                var iterator = Root;
                while(true)
                {
                    var compare = value.CompareTo(iterator.Data);
                    if (compare <= 0)
                    {
                        if (iterator.Left != null)
                        {
                            // Travel further left
                            iterator = iterator.Left;
                            continue;
                        }

                        // An empty left leg, add the new node on the left leg
                        iterator.Left = child;
                        child.Parent = iterator;
                        break;
                    }
                    if (compare > 0)
                    {
                        if (iterator.Right != null)
                        {
                            // Continue to travel right
                            iterator = iterator.Right;
                            continue;
                        }
                       
                        // Add the child to the right leg
                        iterator.Right = child;
                        child.Parent = iterator;
                        break;
                    }
                }
            }
        }

        public void Remove(T value)
        {
            //TODO !
        }

        public bool Find(T value)
        {
            var iterator = Root;
            while (iterator != null)
            {
                var compare = value.CompareTo(iterator.Data);
                // Did we find the value ?
                if (compare == 0) return true;
                if (compare < 0)
                {
                    // Travel left
                    iterator = iterator.Left;
                    continue;
                }
                // Travel right
                iterator = iterator.Right;
            }
            return false;
        }

        private static Node FindMostLeft(Node start)
        {
            var node = start;
            while (true)
            {
                if (node.Left != null)
                {
                    node = node.Left;
                    continue;
                }
                break;
            }
            return node;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator(this);
        }

        class BinaryTreeEnumerator : IEnumerator<T>
        {
            private Node _current;
            private readonly BinaryTree<T> _tree;

            public BinaryTreeEnumerator(BinaryTree<T> tree)
            {
                _tree = tree;
                _current = null;
            }

            /// <summary>
            /// The MoveNext function traverses the tree in sorted order.
            /// </summary>
            /// <returns>True if we found a valid entry, False if we have reached the end</returns>
            public bool MoveNext()
            {
                // For the first entry, find the lowest valued node in the tree
                if (_current == null)
                    _current = FindMostLeft(_tree.Root);
                else
                {
                    // Can we go right-left?
                    if (_current.Right != null)
                        _current = FindMostLeft(_current.Right);
                    else
                    {
                        // Note the value we have found
                        var currentValue = _current.Data;

                        // Go up the tree until we find a value larger than the largest we have
                        // already found (or if we reach the root of the tree)
                        while (_current != null)
                        {
                            _current = _current.Parent;
                            if (_current != null)
                            {
                                var compare = _current.Data.CompareTo(currentValue);
                                if (compare < 0) continue;
                            }
                            break;
                        }

                    }
                }
                return (_current != null);
            }

            public T Current
            {
                get
                {
                    if (_current == null)
                        throw new InvalidOperationException();
                    return _current.Data;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_current == null)
                        throw new InvalidOperationException();
                    return _current.Data;
                }
            }

            public void Dispose() { }
            public void Reset() { _current = null; }
        }
    }
}
