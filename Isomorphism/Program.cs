using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isomorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] tab = new int[,] { { 0, 1 }, { 1, 0 } };
            int[,] fullGraph = new int[,] { { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };

            int[,] isofirst = new int[,] { { 0, 0, 1, 0, 0, 0, 0 },
                                           { 0, 0, 1, 0, 0, 0, 0 },
                                           { 1, 1, 0, 1, 0, 0, 0 },
                                           { 0, 0, 1, 0, 1, 0, 1 },
                                           { 0, 0, 0, 1, 0, 1, 0 },
                                           { 0, 0, 0, 0, 1, 0, 0 },
                                           { 0, 0, 0, 1, 0, 0, 0 } };
            int[,] isosecond = new int[,] { { 0, 1, 0, 0, 0, 0, 0 }, 
                                             { 1, 0, 0, 0, 0, 1, 0 }, 
                                             { 0, 0, 0, 0, 0, 1, 0 }, 
                                             { 0, 0, 0, 0, 0, 0, 1 }, 
                                             { 0, 0, 0, 0, 0, 0, 1 }, 
                                             { 0, 1, 1, 0, 0, 0, 1 }, 
                                             { 0, 0, 0, 1, 1, 1, 0 } };

            Graph G = new Graph(fullGraph);
            Graph first = new Graph(isofirst);
            Graph second = new Graph(isosecond);
            bool t = FullIsomorphismChecker.AreTheyIsomorphic(first, second);
        }
    }
}