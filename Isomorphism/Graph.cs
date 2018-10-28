using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class Graph
    {
        public Vertex[] Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Graph(int verticesCount)
        {
            Vertices = new Vertex[verticesCount];
            Edges = new List<Edge>();
            CreateVertices();
        }
        private void CreateVertices()
        {
            for (int i = 0; i < Vertices.Length; i++) Vertices[i] = new Vertex(i);
        }
        public Graph(int[,] matrix)
        {
            Vertices = new Vertex[matrix.GetLength(0)];
            Edges = new List<Edge>();
            CreateVertices();
            this.GenerateGraphFromMatrix(matrix);
        }

        private void GenerateGraphFromMatrix(int[,] matrix)
        {
            for( int i=0; i<matrix.GetLength(0); i++)
            {
                for(int j=i+1; j<matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 1) this.CreateAndAddEdge(i, j);
                }
            }
        }
        public void CreateAndAddEdge(int from, int to)
        {
            Edge e = new Edge(from, to);
            Vertices[from].AddNeighbor(Vertices[to]);
            Vertices[to].AddNeighbor(Vertices[from]);
            Edges.Add(e);
        }
        public void AddEdge(Edge e)
        {
            Edges.Add(e);
        }
        public Graph Clone()
        {
            Graph H = new Graph(this.Vertices.Length);
            foreach( var e in this.Edges)
            {
                H.Vertices[e.From].AddNeighbor(Vertices[e.To]);
                H.Vertices[e.To].AddNeighbor(Vertices[e.From]);
                H.AddEdge(e);
            }
            return H;
        }
    }
}
