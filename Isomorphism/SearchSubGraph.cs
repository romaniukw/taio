using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class SearchSubGraph
    {
        public Graph G {get; private set; }
        public Graph H { get; private set; }


        public SearchSubGraph(Graph g1, Graph g2)
        {
            G = g1;
            H = g2;
        }

        private void createGraphTwoVertices() //Uzupełnić typ
        {
            int[,] commonVertices = new int[G.Vertices.Length, 1];
            int indexCommonVertices = 0;
            int[,] tab = new int[,] { { 0, 1 }, { 1, 0 } };
            for (int i = 0; i < G.Vertices.Length; i++)
                for (int j = i + 1; j < G.Vertices.Length; j++)
                    if (G.Edges.Contains(new Edge(i, j)))
                    {
                        List<int> verticesSubGraph = new List<int>();
                        verticesSubGraph.Add(i);
                        verticesSubGraph.Add(j);
                        foreach (var e in G.Vertices[i].Neighbors)
                        {
                            if (e.Index > j && e.Index > i)
                            {
                                commonVertices[indexCommonVertices, 0] = e.Index;
                                commonVertices[indexCommonVertices, 1] = 1;
                                indexCommonVertices++;
                            }
                        }
                        foreach (var e in G.Vertices[j].Neighbors)
                        {
                            if (e.Index > i && e.Index > j)
                            {
                                bool existInCommonVertices = false;
                                for (int k = 0; k < indexCommonVertices; k++)
                                {
                                    if (commonVertices[k, 1] == e.Index)
                                    {
                                        commonVertices[k, 2]++;
                                        existInCommonVertices = true;
                                        break;
                                    }
                                }
                                if (!existInCommonVertices)
                                {
                                    commonVertices[indexCommonVertices, 0] = e.Index;
                                    commonVertices[indexCommonVertices, 1] = 1;
                                    indexCommonVertices++;
                                }
                            }
                        }
                        searchSubGraph(verticesSubGraph, commonVertices, indexCommonVertices);
                    }
        }

        private void searchSubGraph(List<int> verticesSubGraph, int[,] commonVertices, int indexCommonVertices) // Uzupełnić typ
        {
            int[,] tmpCommonVertices = new int[commonVertices.Length,2];
            for(int i=0; i<commonVertices.Length; i++)
                for(int j=0; j<2; j++)
                    tmpCommonVertices[i, j] = commonVertices[i, j];
            int tmpIndexCommonVertices = indexCommonVertices;
            for(int i=0; i<indexCommonVertices; i++)
            {
                verticesSubGraph.Add(commonVertices[i, 0]);
                // Sprawdzanie izomorfizmu teraz
            }
        }

        private Graph createGraph(List<int> verticesSubGraph)
        {
            return new Graph(0);
        }
    }
}
