#region Copyright
// <copyright file="VertexList.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

using System.Collections;
using System.Linq;

namespace Cheesebaron.Havarti.Graphs
{
    public class VertexList : IEnumerable
    {
        private readonly Hashtable _vertices = new Hashtable();

        public virtual void Add(IVertex n)
        {
            _vertices.Add(n.Id, n);
        }

        public virtual bool Remove(IVertex n)
        {
            if (!ContainsKey(n.Id))
                return false;

            _vertices.Remove(n.Id);
            return true;
        }

        public virtual bool ContainsKey(string id)
        {
            return _vertices.ContainsKey(id);
        }

        public virtual void Clear()
        {
            _vertices.Clear();
        }

        public virtual IVertex this[string id]
        {
            get
            {
                return (IVertex)_vertices[id];
            }
        }

        public VertexList Clone()
        {
            var vertexList = new VertexList();
            foreach (var vertex in from DictionaryEntry entry in _vertices select (IVertex)entry.Value)
            {
                vertexList.Add(vertex.Clone());
            }
            return vertexList;
        }

        public int Count
        {
            get { return _vertices.Count; }
        }

        public IEnumerator GetEnumerator()
        {
            return _vertices.GetEnumerator();
        }
    }
}
