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

            Graph G = new Graph(fullGraph);         
        }
    }
}
