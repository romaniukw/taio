using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class FindGraphByApproximationAlgorithm
    {
        public static List<int[]> Search(Graph G, Graph H)
        {
            List<int[]> result;

            if (CheckSimpleCases(G, H, out result)) return result;

            var GSubGraphs = GenerateSubGraphs(G);
            var HSubGraphs = GenerateSubGraphs(H);
            
            foreach(var gsub in GSubGraphs)
            {
                foreach(var hsub in HSubGraphs)
                {
                    var res = GenerateMaxIsomorphism(gsub, hsub);
                }
            }
            return null;
        }

        private static bool CheckSimpleCases(Graph G, Graph H, out List<int[]> result)
        {
            result = null;
            if(G.Vertices.Length==0 || H.Vertices.Length == 0)
            {
                return true; //graf G lub H jest grafem pustym
            }

            if(G.Edges.Count==0 || H.Edges.Count==0)
            {
                result.Add(new int[] { G.Vertices[0].Index, H.Vertices[0].Index });
                return true;
            }

            if(G.Edges.Count == 1 || H.Edges.Count==1)
            {
                result.Add(new int[] { G.Edges[0].From, H.Edges[0].From });
                result.Add(new int[] { G.Edges[0].To, H.Edges[0].To });
                return true;
            }
            return false;
        }

        private static List<SubGraph> GenerateSubGraphs(Graph G)
        {
             List<SubGraph> subGraphs = new List<SubGraph>();
            foreach(var v1 in G.Vertices)
            {

                foreach(var v2 in v1.Neighbors)
                {
                    foreach(var v3 in v2.Neighbors)
                    {
                        if(v1.Index<v2.Index && v2.Index<v3.Index)
                        {
                            var vertexes = new List<Vertex>() { v1, v2, v3 };                            
                            var neighbours = new List<Vertex>();
                            neighbours.AddRange(v1.Neighbors.Where(x => x != v2 && x != v3));
                            neighbours.AddRange(v2.Neighbors.Where(x => x != v1 && x != v3));
                            neighbours.AddRange(v3.Neighbors.Where(x => x != v2 && x != v1));
                            neighbours = neighbours.Distinct().ToList();
                            var edges = new List<Edge>();
                            foreach(var e in G.Edges)
                            {
                                if(e.From == v1.Index && (e.To == v2.Index || e.To == v3.Index))
                                {
                                    edges.Add(e);
                                }
                                if(e.From == v2.Index && e.To == v3.Index)
                                {
                                    edges.Add(e);
                                }
                            }
                            subGraphs.Add(new SubGraph(neighbours, vertexes, edges, G));
                        }
                    }
                }
            }
            return subGraphs;
        }
        private static List<int[]> GenerateMaxIsomorphism(SubGraph subG, SubGraph subH)
        {
            if (subG.Edges.Count != subH.Edges.Count) return null;  //na pewno null? czy mapowanie lini?
            foreach(var mapG in subG.Maps)
            {
                foreach(var mapH in subH.Maps)
                {

                    MappingGeneratorAndChecker mac = new MappingGeneratorAndChecker(subG, subH, mapG, mapH, G, H);
                    var result = mac.GetMaxMapping();
                }
            }

            return null;
        }        
    }
}
