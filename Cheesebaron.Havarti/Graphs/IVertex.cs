#region Copyright
// <copyright file="IVertex.cs">
// (c) Copyright 2012 Tomasz Cielecki. http://ostebaronen.dk
// This source is subject to the Microsoft Public License (Ms-PL)
// Please see license.txt on http://opensource.org/licenses/ms-pl.html
// All other rights reserved.
// </copyright>
#endregion

namespace Cheesebaron.Havarti.Graphs
{
    public interface IVertex
    {
        string Id { get; set; }
        string Label { get; set; }

        double Cost { get; set; }
        bool Visited { get; set; }

        IVertex Previous { get; set; }

        IVertex Clone();
    }
}
