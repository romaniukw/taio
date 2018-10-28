using System.Collections.Generic;

namespace Isomorphism
{
    internal class Vertex
    {
        public int Index { get; private set; }
        public int Degree { get; private set; }
        public List<Vertex> Neighbors { get; private set; }

        public Vertex(int i)
        {
            Degree = 0;
            Neighbors = new List<Vertex>();
            Index = i;
        }
        public void AddNeighbor(Vertex e)
        {
            Degree++;
            Neighbors.Add(e);
        }
    }
}