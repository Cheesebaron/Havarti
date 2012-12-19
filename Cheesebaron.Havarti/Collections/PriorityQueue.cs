using System;
using System.Collections;
using System.Collections.Generic;

namespace Cheesebaron.Havarti.Collections
{
    public interface IPriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        bool IsEmpty { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }

    /// <summary>
    /// Borrowed from: http://www.remondo.net/generic-priority-queue-example-csharp/
    /// </summary>
    public class PriorityQueueLinkedList<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        private readonly LinkedList<T> _items;

        public PriorityQueueLinkedList()
        {
            _items = new LinkedList<T>();
        }
 
        public void Enqueue(T item)
        {
            if (IsEmpty)
            {
                _items.AddFirst(item);
                return;
            }
 
            var existingItem = _items.First;
 
            while (existingItem != null && existingItem.Value.CompareTo(item) < 0)
            {
                existingItem = existingItem.Next;
            }
 
            if (existingItem == null)
                _items.AddLast(item);
            else
            {
                _items.AddBefore(existingItem, item);
            }
        }
 
        public T Dequeue()
        {
            var value = _items.First.Value;
            _items.RemoveFirst();
 
            return value;
        }
 
        public T Peek()
        {
            return _items.First.Value;
        }
 
        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }
 
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Based on the idea from: http://www.remondo.net/generic-priority-queue-example-csharp/#comment-4145
    /// </summary>
    public class PriorityQueueBinaryTree<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        /// <summary>
        /// Enum determining how to prioritize.
        /// Either by Lowest value or highest value being highest priority.
        /// </summary>
        public enum PriorityMode
        {
            Lowest = 0,
            Highest = 1
        }

        private readonly BinaryTree<T> _items;

        public PriorityMode Mode { get; set; }

        public PriorityQueueBinaryTree()
        {
            _items = new BinaryTree<T>();
            Mode = PriorityMode.Lowest;
        }

        /// <summary>
        /// Enqueue (push) item in Priority Queue.
        /// </summary>
        /// <param name="item">IComparable item of type T</param>
        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Dequeue (pop) item according to PriorityMode.
        /// </summary>
        /// <returns>IComparable item of type T</returns>
        public T Dequeue()
        {
            T value;
            switch (Mode)
            {
                case PriorityMode.Highest:
                    value = _items.RightMost.Data;
                    break;
                case PriorityMode.Lowest:
                default:
                    value = _items.LeftMost.Data;
                    break;
            }
            _items.Remove(value);

            return value;
        }

        /// <summary>
        /// Lets us see which item has the highest priority according to the PriorityMode.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            switch (Mode)
            {
                case PriorityMode.Highest:
                    return _items.RightMost.Data;
                case PriorityMode.Lowest:
                default:
                    return _items.LeftMost.Data;
            }
        }

        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
