using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    public static class CreateExampleGraphs
    {
        public static Graph CreateFromFile(string path)
        {
            var lines = File.ReadAllLines(path);    
            int size = Convert.ToInt32(lines[0]);
            Graph G = new Graph(size);
            for(int i=1; i<lines.Length; i++)
            {
                var t = lines[i]
                        .Split(',')
                        .Select(x => Convert.ToInt32(x))
                        .ToArray();
                G.CreateAndAddEdge(t[0], t[1]);
            }
            return G;
        }
    }
}
