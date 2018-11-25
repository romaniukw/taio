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
        
        public static Graph CreateFromCSVFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            var s = sr.ReadToEnd();
            var tab = s.Split(new[] { "\r\n","\n" }, StringSplitOptions.None).ToList();
            tab.RemoveAt(tab.Count()-1);
            var gtab=tab.Select(x => x.Split(',')).Select(x => Array.ConvertAll(x, int.Parse)).ToArray();

            var twoD = new int[gtab.Length, gtab[0].Length];
            for (int i = 0; i != gtab.Length; i++)
                for (int j = 0; j != gtab[0].Length; j++)
                    twoD[i, j] = gtab[i][j];

            return new Graph(twoD);
        }
    }
}
