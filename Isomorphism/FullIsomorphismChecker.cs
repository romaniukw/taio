using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    public class FullIsomorphismChecker
    {
        public static bool AreTheyIsomorphic(Graph G, Graph H, out List<int[]> mapping)
        {
            mapping = null;
            if(G.Vertices.Length != H.Vertices.Length)
            {
                return false;
            }

            if(G.Edges.Count != G.Edges.Count)
            {
                return false;
            }

            var gDegreeSequence = G.Vertices.Select(x => x.Degree).OrderBy(x => x).ToList();
            var hDegreeSequence = H.Vertices.Select(x => x.Degree).OrderBy(x => x).ToList();
           
            bool areEqual = gDegreeSequence.SequenceEqual(hDegreeSequence);
            if (!areEqual)
            {
                return false;
            }

            return CheckIsomorphism(G, H, out mapping);
        }

        private static List<Vertex> gDegreesList;

        private static bool CheckIsomorphism(Graph G, Graph H, out List<int[]> mapping)
        {
            mapping = null;
            gDegreesList = G.Vertices
                .OrderByDescending(x => x.Degree)
                .ToList();

            var hDegreesList = H.Vertices
                .GroupBy(x => x.Degree)
                .Select(x => x.ToList())
                .OrderByDescending(x => x[0].Degree)
                .ToList();
            
            var gtab = gDegreesList.Select(x => x.Index).ToArray();
            var htab = Perm(new List<Vertex>(), hDegreesList);
            if(htab==null)
            {                
                return false;
            }
            mapping = new List<int[]>();
            mapping.Add(gtab);
            mapping.Add(htab.Select(x => x.Index).ToArray());
            return true;
        }

        public static List<Vertex> Perm(List<Vertex> currentPerm, List<List<Vertex>> sequences, int currentSequenceIndex = 0)
        {
            var currentSequence = sequences[currentSequenceIndex];

            foreach (var elem in currentSequence)
            {
                if (currentPerm.Count > 0)
                {
                    var elemG = gDegreesList[currentPerm.Count];

                    var didBreak = false;

                    for (int i = 0; i < currentPerm.Count; i++)
                    {
                        var g = gDegreesList[i];
                        var h = currentPerm[i];

                        if (g.Neighbors.Contains(elemG) != h.Neighbors.Contains(elem))
                        {
                            didBreak = true;
                            break;
                        }
                    }

                    if (didBreak)
                    {
                        continue;
                    }
                }

                var currentLocalPerm = currentPerm.Concat(new[] { elem }).ToList();

                if (currentLocalPerm.Count == gDegreesList.Count)
                {
                    //Console.WriteLine(string.Join(", ", currentLocalPerm.Select(v => v.Index)));
                    return currentLocalPerm;
                }

                var localSequences = sequences.ToList();
                localSequences[currentSequenceIndex] = localSequences[currentSequenceIndex].ToList();
                localSequences[currentSequenceIndex].Remove(elem);

                var nextSequenceIndex = currentSequenceIndex;
                if (!localSequences[currentSequenceIndex].Any())
                {
                    nextSequenceIndex++;
                }

                if (nextSequenceIndex >= sequences.Count)
                {
                    return null;
                }

                var result = Perm(currentLocalPerm, localSequences, nextSequenceIndex);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
