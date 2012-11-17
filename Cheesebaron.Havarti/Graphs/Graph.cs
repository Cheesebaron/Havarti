#region Copyright
// <copyright file="Graph.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cheesebaron.Havarti.Graphs
{
    public class Graph
    {
        #region Properties

        public VertexList Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }
        public GraphType Type { get; private set; }

        #endregion

        #region Ctor

        public Graph(GraphType type)
        {
            Type = type;
            Vertices = new VertexList();
            Edges = new List<Edge>();
        }

        #endregion

        #region Vertex Methods

        public void AddVertex(IVertex vertex)
        {
            if (Vertices.ContainsKey(vertex.Id))
                throw new ArgumentOutOfRangeException("vertex", 
                    string.Format("Vertex with Id {0} already exists", vertex.Id));

            Vertices.Add(vertex);
        }

        public void RemoveVertex(string id)
        {
            var vert = Vertices[id];

            foreach (var edge in Edges.Where(edge => edge.From == vert || edge.To == vert))
            {
                Edges.Remove(edge);
            }

            Vertices.Remove(vert);
        }

        #endregion

        #region Edge Methods

        public void AddEdge(IVertex from, IVertex to, string label, double cost = 0.0)
        {
            switch (Type)
            {
                case GraphType.Directed:
                    Edges.Add(new Edge{ Cost = cost, From = from, To = to, Label = label});
                    break;
                case GraphType.Undirected:
                    Edges.Add(new Edge { Cost = cost, From = from, To = to, Label = label });
                    Edges.Add(new Edge { Cost = cost, From = to, To = from, Label = label });
                    break;
            }
        }

        public void RemoveEdge(Edge edge)
        {
            //TODO Do I need to do anything else here?
            Edges.Remove(edge);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets neighboring nodes to a vertex
        /// </summary>
        /// <param name="vertex">Vertex to get neighbors to</param>
        /// <returns>IEnumerable of vertices</returns>
        private IEnumerable<IVertex> GetNeighbors(IVertex vertex)
        {
            var returnList = new List<IVertex>();

            foreach (var edge in Edges)
            {
                switch (Type)
                {
                    case GraphType.Directed:
                        if (edge.From == vertex)
                            returnList.Add(edge.To);
                        break;
                    case GraphType.Undirected:
                        if (edge.From == vertex)
                            returnList.Add(edge.To);
                        if (edge.To == vertex)
                            returnList.Add(edge.From);
                        break;
                }
                
            }

            return returnList;
        }

        #endregion

        #region Search Algorithms

        /*
         * DFS and BFS are quite similar. Difference is that one uses a Stack and the other a Queue
         * for adding vertices to visit.
         */

        /// <summary>
        /// Depth First Search. Finds the deepest vertex first and expands from there.
        /// </summary>
        /// <param name="start">Vertex to perform search from</param>
        /// <returns>Returns List of vertex Id's</returns>
        public List<string> DepthFirstSearch(IVertex start)
        {
            var result = new List<string>();
            
            var unvisted = Vertices.Clone();
            var stack = new Stack<IVertex>();

            unvisted.Remove(start);
            stack.Push(start);
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                result.Add(top.Id);
                foreach (var neighbor in GetNeighbors(top).Where(unvisted.Remove))
                    stack.Push(neighbor);
            }
            return result;
        }

        /// <summary>
        /// Breadth First Search. Finds the widest vertices
        /// </summary>
        /// <param name="start">Vertex to perform search from</param>
        /// <returns>Returns List of vertex Id's</returns>
        public List<string> BreathFirstSearch(IVertex start)
        {
            var result = new List<string>();

            var unvisted = Vertices.Clone();
            var queue = new Queue<IVertex>();
            unvisted.Remove(start);

            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                result.Add(head.Id);
                foreach (var neighbor in GetNeighbors(head).Where(unvisted.Remove))
                    queue.Enqueue(neighbor);
            }
            return result;
        }

        #endregion
    }
}
