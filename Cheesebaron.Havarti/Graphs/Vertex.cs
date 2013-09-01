#region Copyright
// <copyright file="Vertex.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

namespace Cheesebaron.Havarti.Graphs
{
    public class Vertex<T> 
        : IVertex
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public double Cost { get; set; }
        public bool Visited { get; set; }
        public IVertex Previous { get; set; }
        public T Data { get; set; }

        public IVertex Clone()
        {
            return new Vertex<T>
                {
                    Data = Data,
                    Id = Id,
                    Label = Label,
                    Cost = Cost,
                    Previous = Previous,
                    Visited = Visited
                };
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Label: {1}, Data: {2}, Cost: {3}, Visited: {4}, Previous: [{5}]", Id, Label,
                                 Data, Cost, Visited, Previous);
        }
    }
}
