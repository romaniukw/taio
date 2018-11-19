using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Isomorphism
{
    public static class ConsoleFullCheckerApp
    {
        public static void RunApp()
        {
            Console.WriteLine("Co chcesz zrobić?");
            Console.WriteLine("Uruchom wszystkie: Wybierz 0");
           // Console.WriteLine("Uruchom jeden: Wybierz 1-10");
            Console.WriteLine("Wyłącz: Wybierz X");
            var t = Console.ReadLine();
            switch (t)
            {
                case "0":
                    RunAll();
                    break;
                case "x":
                    return;
            }
            RunApp();
        }

        public static void RunAll()
        {
            for (int i = 1; i <= 10; i++)
            {
                RunOne(i);
            }
        }
        public static void RunOne(int index)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Test {index}: ");
            Graph G = CreateExampleGraphs.CreateFromFile($"../../Data/example{index}a.txt");
            Graph H = CreateExampleGraphs.CreateFromFile($"../../Data/example{index}a.txt");
            sw.Start();
            List<int[]> mapping;
            FullIsomorphismChecker.AreTheyIsomorphic(G, H, out mapping);//tu sprawdzać całość
            sw.Stop();
            Console.Write($"{sw.ElapsedTicks}");
            Console.WriteLine();
        }
    }
}
