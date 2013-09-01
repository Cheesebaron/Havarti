#region Copyright
// <copyright file="GraphExtensions.cs">
// (c) Copyright 2013 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Cheesebaron.Havarti.Graphs
{
    public static partial class GraphExtensions
    {
        /*
         * DFS and BFS are quite similar. Difference is that one uses a Stack and the other a Queue
         * for adding vertices to visit.
         */

        /// <summary>
        /// Depth First Search. Finds the deepest vertex first and expands from there.
        /// </summary>
        /// <param name="start">Vertex to perform search from</param>
        /// <returns>Returns List of vertex Id's</returns>
        public static List<string> DepthFirstSearch(this Graph graph, IVertex start)
        {
            var result = new List<string>();

            var unvisted = graph.Vertices.Clone();
            var stack = new Stack<IVertex>();

            unvisted.Remove(start);
            stack.Push(start);
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                result.Add(top.Id);
                foreach (var neighbor in graph.GetNeighbors(top).Where(unvisted.Remove))
                    stack.Push(neighbor);
            }
            return result;
        }

        /// <summary>
        /// Breadth First Search. Finds the widest vertices
        /// </summary>
        /// <param name="start">Vertex to perform search from</param>
        /// <returns>Returns List of vertex Id's</returns>
        public static List<string> BreathFirstSearch(this Graph graph, IVertex start)
        {
            var result = new List<string>();

            var unvisted = graph.Vertices.Clone();
            var queue = new Queue<IVertex>();
            unvisted.Remove(start);

            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                result.Add(head.Id);
                foreach (var neighbor in graph.GetNeighbors(head).Where(unvisted.Remove))
                    queue.Enqueue(neighbor);
            }
            return result;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Dictionary<IVertex, double> WidestPath(this Graph graph, IVertex from, IVertex to)
        {
            var width = new Dictionary<IVertex, double>();
            var previous = new Dictionary<IVertex, IVertex>();
            foreach (var vertex in from DictionaryEntry dictEntry in graph.Vertices select (IVertex)dictEntry.Value)
            {
                width.Add(vertex, double.NegativeInfinity);
                previous.Add(vertex, null);
            }

            width[from] = double.PositiveInfinity;
            var q = (from DictionaryEntry dictEntry in graph.Vertices select (IVertex)dictEntry.Value).ToList();

            while (q.Count > 0)
            {
                var u = q.MaxBy(x => width[x]);
                q.Remove(u);

                if (double.IsNegativeInfinity(width[u]) || u == to)
                    break;

                foreach (var neighbor in graph.GetNeighbors(u))
                {
                    var alt = Math.Max(width[neighbor], Math.Min(width[u], graph.GetEdgeCost(u, neighbor)));

                    if (!(alt > width[neighbor])) continue;
                    width[neighbor] = alt;
                    previous[neighbor] = u;
                }
            }

            return width;
        }
    }
}
