using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class MappingGeneratorAndChecker
    {
        private Graph gBase;
        private Graph hBase;

        private List<Vertex> gVertices;
        private List<Vertex> gNeighbours;
        private List<Edge> gEdges;

        private List<Vertex> hVertices;
        private List<Vertex> hNeighbours;
        private List<Edge> hEdges;

        private List<int[]> mapping;
        private int[] tabmapping;

        public MappingGeneratorAndChecker(SubGraph G, SubGraph H, int[] Gmap, int[] Hmap)
        {
            gBase = G.BaseGraph;
            gVertices = new List<Vertex>(G.Vertexes);
            gNeighbours = new List<Vertex>(G.Neighbours);
            gEdges = new List<Edge>(G.Edges);
            hBase = H.BaseGraph;
            hVertices = new List<Vertex>(H.Vertexes);
            hNeighbours = new List<Vertex>(H.Neighbours);
            hEdges = new List<Edge>(H.Edges);

            mapping = new List<int[]> {Gmap, Hmap};
            tabmapping = new int[gBase.Vertices.Count()];
            for(int i=0; i<tabmapping.Length; i++)
            {
                tabmapping[i] = -1;
            }
            for (int i=0; i<Gmap.Length; i++)
            {
                tabmapping[Gmap[i]] = Hmap[i];
            }
        }

        public List<int[]> GetMaxMapping()
        {
            if(mapping[0].Length==gBase.Vertices.Length || mapping[1].Length == hBase.Vertices.Length)
            {
                return mapping;
            }
            var gv = gNeighbours.Aggregate((i1, i2) => i1.Degree > i2.Degree ? i1 : i2); 
            var newgedges = gBase.Edges.Where(x => (x.From == gv.Index && x.To > gv.Index && mapping[0].Contains(x.To))
                            || (x.To == gv.Index && x.From < gv.Index && mapping[0].Contains(x.From))).ToList();
            
            var hv = hNeighbours.Aggregate((i1, i2) => i1.Degree > i2.Degree ? i1 : i2);
            var newhedges = hBase.Edges.Where(x => (x.From == hv.Index && x.To > hv.Index && mapping[1].Contains(x.To))
                            || (x.To == hv.Index && x.From < hv.Index && mapping[1].Contains(x.From))).ToList();

            tabmapping[gv.Index] = hv.Index;
            if(newgedges.Count!=newhedges.Count) return mapping;
            foreach(var e in newgedges)
            {
                if(!newhedges.Select(x=>x).Where(x=> (x.From==tabmapping[e.From] && x.To== tabmapping[e.To]) || (x.To== tabmapping[e.From] && x.From == tabmapping[e.To])).Any())
                {
                    return mapping;
                }
            }

            var temp = mapping[0].ToList();
            temp.Add(gv.Index);
            mapping[0] = temp.ToArray();

            temp = mapping[1].ToList();
            temp.Add(hv.Index);
            mapping[1] = temp.ToArray();

            gVertices.Add(gv);
            gEdges.AddRange(newgedges);
            gNeighbours.Remove(gv); 

            hVertices.Add(hv); 
            hEdges.AddRange(newhedges);
            hNeighbours.Remove(hv);

            gNeighbours.AddRange(gv.Neighbors.Select(x=>x).Where(x => !gNeighbours.Contains(x) && !gVertices.Contains(x)).ToList());
            hNeighbours.AddRange(hv.Neighbors.Select(x => x).Where(x => !hNeighbours.Contains(x) && !hVertices.Contains(x)).ToList());

            return GetMaxMapping();
        }
    }
}
