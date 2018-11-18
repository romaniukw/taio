using System.Collections.Generic;

namespace Isomorphism
{
    public class SubGraph
    {
        public List<Vertex> Neighbours { get; private set; }
        public List<Vertex> Vertexes { get; private set; }
        public List<Edge> Edges { get; private set; }
        public Graph BaseGraph { get; private set; }
        public List<int[]> Maps { get; private set; }  

        public SubGraph(List<Vertex> neighbours, List<Vertex> vertexes, List<Edge> edges, Graph baseGraph)
        {
            Neighbours = neighbours;
            Vertexes=vertexes;
            Edges = edges;
            BaseGraph = baseGraph;
            GenerateMaps();
        }

        private void GenerateMaps()
        {
            Maps = new List<int[]>();
            if (Edges.Count == 3)
            {
                Maps.Add(new int[] { Vertexes[0].Index, Vertexes[1].Index, Vertexes[2].Index });
                Maps.Add(new int[] { Vertexes[0].Index, Vertexes[2].Index, Vertexes[1].Index });
                Maps.Add(new int[] { Vertexes[1].Index, Vertexes[0].Index, Vertexes[2].Index });
                Maps.Add(new int[] { Vertexes[1].Index, Vertexes[2].Index, Vertexes[0].Index });
                Maps.Add(new int[] { Vertexes[2].Index, Vertexes[0].Index, Vertexes[1].Index });
                Maps.Add(new int[] { Vertexes[2].Index, Vertexes[1].Index, Vertexes[0].Index });
            }

            if (Edges.Count == 2)
            { 
                
                if (Edges[0].From == Edges[1].To)
                {
                    Maps.Add(new int[] { Edges[0].To, Edges[1].To, Edges[1].From });
                    Maps.Add(new int[] { Edges[1].From, Edges[1].To, Edges[0].To });
                }
                else
                {
                    Maps.Add(new int[] { Edges[0].From, Edges[1].From, Edges[1].To });
                    Maps.Add(new int[] { Edges[1].To, Edges[1].From, Edges[0].From });
                }
            }
        }
    }
    
}
