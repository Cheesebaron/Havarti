#region Copyright
// <copyright file="Edge.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

namespace Cheesebaron.Havarti.Graphs
{
    public class Edge
    {
        public string Label { get; set; }
        public IVertex From { get; set; }
        public IVertex To { get; set; }
        public double Cost { get; set; }

        public override string ToString()
        {
            return string.Format("Edge: {0}\n\tFrom: {1}\n\tTo: {2}\n\tCost: {3}", Label, From, To, Cost);
        }
    }
}
