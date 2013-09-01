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

        public double GetEdgeCost(IVertex from, IVertex to)
        {
            foreach (var edge in Edges.Where(edge => edge.From == @from && edge.To == to))
            {
                return edge.Cost;
            }

            return double.MinValue;
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
        public IEnumerable<IVertex> GetNeighbors(IVertex vertex)
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
    }
}
