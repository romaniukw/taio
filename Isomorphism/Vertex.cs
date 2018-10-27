using System.Collections.Generic;

namespace Isomorphism
{
    internal class Vertex
    {
        public int Degree { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Vertex()
        {
            Degree = 0;
            Edges = new List<Edge>();
        }
        public void AddEdge(Edge e)
        {
            Degree++;
            Edges.Add(e);
        }
    }
}