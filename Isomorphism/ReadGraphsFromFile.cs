using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Isomorphism
{
    public static class ReadGraphsFromFile
    {
        public static void GetGraphs(string path, out Graph G, out Graph H)
        {
            StreamReader sr = new StreamReader(path);
            var s = sr.ReadToEnd();
            var t = s.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);

            ReadGraph(t[0], out G);
            ReadGraph(t[1], out H);
        }

        private static void ReadGraph(string str, out Graph G)
        {
            var gtab = str.Split(new[] { "\r\n" }, StringSplitOptions.None)
                          .Select(x => x.Split(','))
                          .Select(x => Array.ConvertAll(x, int.Parse))
                          .ToArray();

            var twoD = new int[gtab.Length, gtab[0].Length];
            for (int i = 0; i != gtab.Length; i++)
                for (int j = 0; j != gtab[0].Length; j++)
                    twoD[i, j] = gtab[i][j];

            G = new Graph(twoD);
        }

    }
}
