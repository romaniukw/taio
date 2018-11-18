using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class MappingGeneratorAndChecker
    {
        private Graph gBig;
        private Graph hBig;

        private List<Vertex> gVertices;
        private List<Vertex> gNeighbours;
        private List<Edge> gEdges;

        private List<Vertex> hVertices;
        private List<Vertex> hNeighbours;
        private List<Edge> hEdges;

        private List<int[]> mapping;

        public MappingGeneratorAndChecker(SubGraph G, SubGraph H, int[] Gmap, int[] Hmap)
        {
            gBig = G.BaseGraph;
            gVertices = H.Vertexes;
            gNeighbours = H.Neighbours;
            gEdges = H.Edges;
            hBig = H.BaseGraph;
            hVertices = H.Vertexes;
            hNeighbours = H.Neighbours;
            hEdges = H.Edges;

            var mapping = new List<int[]> { new int[] { Gmap[0], Hmap[0] },
                                            new int[] { Gmap[1], Hmap[1] },
                                            new int[] { Gmap[1], Hmap[1] } };
        }

        public List<int[]> GetMaxMapping()
        {
            var gv = gNeighbours.Aggregate((i1, i2) => i1.Degree > i2.Degree ? i1 : i2);
            gVertices.Add(gv); //dodaj do wierzchołków
            //dodaj krawędzie
            //sprawdż mapowanie jeżeli ok to jak nie to return mapping
            gNeighbours.Remove(gv); //usuń z sąsiadów
            //dodaj sąsiadów

            return null;
        }
    }
}
