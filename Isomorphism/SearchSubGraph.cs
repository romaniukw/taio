using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class SearchSubGraph
    {
        public Graph G { get; private set; }
        public Graph H { get; private set; }
        public List<int> VerticesFromGraphG { get; private set; } // Lista wierzchołków z pierwszego grafu
        public List<int> VerticesFromGraphH { get; private set; } // Lista wierzchołków z drugiego grafu
        public List<int[]> BestMapping { get; private set; } // Mapowanie

        public SearchSubGraph(Graph g1, Graph g2)
        {
            if(g1.Edges.Count==0 || g2.Edges.Count==0)
            {
                BestMapping = new List<int[]>();
                BestMapping.Add(new int[] { g1.Vertices[0].Index });
                BestMapping.Add(new int[] { g2.Vertices[0].Index });
                return;

            }
            if (g1.Edges.Count == 1 || g2.Edges.Count == 1)
            {
                BestMapping = new List<int[]>();
                BestMapping.Add(new int[] { g1.Edges[0].From, g1.Edges[0].To });
                BestMapping.Add(new int[] { g2.Edges[0].From, g2.Edges[0].To });
                return;

            }

            if (g1.Vertices.Length <= g2.Vertices.Length)
            {
                G = g1;
                H = g2;
            }
            else
            {
                G = g2;
                H = g1;
            }
            VerticesFromGraphG = new List<int>();
            VerticesFromGraphH = new List<int>();
            createGraphTwoVertices();

            List<int[]> tmpMap;
            if (g1.Vertices.Length <= g2.Vertices.Length)
            {
                FullIsomorphismChecker.AreTheyIsomorphic(createGraph(VerticesFromGraphG), createGraphH(VerticesFromGraphH), out tmpMap);
                int[] tmpTable0 = new int[tmpMap[0].Length];
                int[] tmpTable1 = new int[tmpMap[1].Length];
                for (int i = 0; i < tmpMap[0].Length; i++)
                {
                    tmpTable0[i] = VerticesFromGraphG[tmpMap[0][i]];
                }
                for (int i = 0; i < tmpMap[1].Length; i++)
                {
                    tmpTable1[i] = VerticesFromGraphH[tmpMap[1][i]];
                }
                BestMapping = new List<int[]>();
                BestMapping.Add(tmpTable0);
                BestMapping.Add(tmpTable1);
            }
            else
            {
                FullIsomorphismChecker.AreTheyIsomorphic(createGraphH(VerticesFromGraphH), createGraph(VerticesFromGraphG), out tmpMap);
                int[] tmpTable0 = new int[tmpMap[0].Length];
                int[] tmpTable1 = new int[tmpMap[1].Length];
                for (int i = 0; i < tmpMap[0].Length; i++)
                {
                    tmpTable0[i] = VerticesFromGraphH[tmpMap[0][i]];
                }
                for (int i = 0; i < tmpMap[1].Length; i++)
                {
                    tmpTable1[i] = VerticesFromGraphG[tmpMap[1][i]];
                }
                BestMapping = new List<int[]>();
                BestMapping.Add(tmpTable0);
                BestMapping.Add(tmpTable1);
            }
            
        }

        private void createGraphTwoVertices() 
        {
            List<int> verticesSubGraphH = new List<int>();
            verticesSubGraphH.Add(H.Edges[0].From);
            verticesSubGraphH.Add(H.Edges[0].To);
            List<int> commonVertices = new List<int>();
            Graph subGraph = new Graph(0);
            int[,] tab = new int[,] { { 0, 1 }, { 1, 0 } };
            for (int i = 0; i < G.Vertices.Length; i++)
            {
                for (int j = i + 1; j < G.Vertices.Length; j++)
                {
                    if (G.Edges.Select(x => x).Where(x => x.From == i && x.To == j).Any())
                    {
                        List<int> verticesSubGraph = new List<int>();
                        verticesSubGraph.Add(i);
                        verticesSubGraph.Add(j);
                        foreach (var e in G.Vertices[i].Neighbors)
                        {
                            if (e.Index > j && e.Index > i)
                            {
                                commonVertices.Add(e.Index);
                            }
                        }
                        foreach (var e in G.Vertices[j].Neighbors)
                        {
                            if (e.Index > i && e.Index > j && !commonVertices.Contains(e.Index))
                            {
                                commonVertices.Add(e.Index);
                            }
                        }
                        if (commonVertices.Count > 0)
                        {
                            searchSubGraph(verticesSubGraph, commonVertices, verticesSubGraphH, new List<int[]>());
                        }
                    }
                    commonVertices = new List<int>();
                }
            }
        }

        private void searchSubGraph(List<int> verticesSubGraph, List<int> commonVertices, List<int> verticesSubGraphH, List<int[]> tmpBestMapping)
        {
            List<int> tmpCommonVertices = new List<int>();
            Graph tmpGraph = createGraph(verticesSubGraph);
            foreach (var e in commonVertices)
                tmpCommonVertices.Add(e);
            //Rekurencyjne tworzenie wszystkich pod grafów
            List<List<int>> allSubGraphList = createAllSubGraphContainsNVertices(verticesSubGraph.Count + 1, new List<List<int>>(), 0, 0, new List<int>());
            for (int i = 0; i < commonVertices.Count; i++)
            {
                var tmpVertic = commonVertices[i];
                verticesSubGraph.Add(tmpVertic);
                tmpCommonVertices.Remove(tmpVertic);

                foreach (var verti in allSubGraphList)
                {
                    List<int[]> tmpMapping;
                    if (FullIsomorphismChecker.AreTheyIsomorphic(createGraph(verticesSubGraph), createGraphH(verti), out tmpMapping))
                    {
                        foreach (var e in G.Vertices[tmpVertic].Neighbors)
                        {
                            if (e.Index > verticesSubGraph.Max() && !commonVertices.Contains(e.Index))
                            {
                                tmpCommonVertices.Add(e.Index);
                            }
                        }
                        searchSubGraph(verticesSubGraph, tmpCommonVertices, verti, tmpMapping);
                        break;
                    }
                    else
                    {
                        if (VerticesFromGraphG.Count + createGraph(VerticesFromGraphG).Edges.Count < tmpGraph.Vertices.Length + tmpGraph.Edges.Count)
                        {
                            VerticesFromGraphG = new List<int>();
                            foreach (var p in verticesSubGraph)
                                VerticesFromGraphG.Add(p);
                            VerticesFromGraphG.Remove(tmpVertic);
                            VerticesFromGraphH = new List<int>();
                            foreach (var p in verticesSubGraphH)
                                VerticesFromGraphH.Add(p);
                            BestMapping = new List<int[]>();
                            foreach(var e in tmpBestMapping)
                            {
                                int[] tmpTable = new int[e.Length];
                                for (int p = 0; p < e.Length; p++)
                                    tmpTable[p] = e[p];
                                BestMapping.Add(tmpTable);
                            }
                        }
                    }
                    tmpCommonVertices = new List<int>();
                    foreach (var e in commonVertices)
                        tmpCommonVertices.Add(e);
                    tmpCommonVertices.Remove(tmpVertic);
                }
                verticesSubGraph.Remove(tmpVertic);
                tmpCommonVertices.Add(tmpVertic);
            }
            if (commonVertices.Count == 0)
                if (VerticesFromGraphG.Count + createGraph(VerticesFromGraphG).Edges.Count < tmpGraph.Vertices.Length + tmpGraph.Edges.Count)
                {
                    VerticesFromGraphG = new List<int>();
                    foreach (var p in verticesSubGraph)
                        VerticesFromGraphG.Add(p);
                    VerticesFromGraphH = new List<int>();
                    foreach (var p in verticesSubGraphH)
                        VerticesFromGraphH.Add(p);
                    BestMapping = new List<int[]>();
                    foreach (var e in tmpBestMapping)
                    {
                        int[] tmpTable = new int[e.Length];
                        for (int p = 0; p < e.Length; p++)
                            tmpTable[p] = e[p];
                        BestMapping.Add(tmpTable);
                    }
                }
        }

        private List<List<int>> createAllSubGraphContainsNVertices(int n, List<List<int>> list, int howMuchNumberAllredyHave, int index, List<int> actualList)
        {
            if (howMuchNumberAllredyHave == n)
            {
                List<int> tmpList = new List<int>();
                foreach (var e in actualList)
                    tmpList.Add(e);
                list.Add(tmpList);
                return list;
            }
            for (int i = index; i < H.Vertices.Length; i++)
            {
                howMuchNumberAllredyHave++;
                actualList.Add(i);
                list = createAllSubGraphContainsNVertices(n, list, howMuchNumberAllredyHave, i + 1, actualList);
                actualList.Remove(i);
                howMuchNumberAllredyHave--;
            }
            return list;
        }
        public Graph createGraph(List<int> verticesSubGraph)
        {
            int[,] matrix = new int[verticesSubGraph.Count, verticesSubGraph.Count];
            verticesSubGraph.Sort();
            for (int i = 0; i < verticesSubGraph.Count - 1; i++)
            {
                for (int j = i + 1; j < verticesSubGraph.Count; j++)
                {
                    foreach (var e in G.Edges)
                    {
                        if (e.From == verticesSubGraph[i] && e.To == verticesSubGraph[j])
                        {
                            matrix[i, j] = 1;
                            matrix[j, i] = 1;
                            break;
                        }
                    }
                }
            }
            return new Graph(matrix);
        }
        public Graph createGraphH(List<int> verticesSubGraph)
        {
            int[,] matrix = new int[verticesSubGraph.Count, verticesSubGraph.Count];
            verticesSubGraph.Sort();
            for (int i = 0; i < verticesSubGraph.Count - 1; i++)
            {
                for (int j = i + 1; j < verticesSubGraph.Count; j++)
                {
                    foreach (var e in H.Edges)
                    {
                        if (e.From == verticesSubGraph[i] && e.To == verticesSubGraph[j])
                        {
                            matrix[i, j] = 1;
                            matrix[j, i] = 1;
                            break;
                        }
                    }
                }
            }
            return new Graph(matrix);
        }
    }
}
