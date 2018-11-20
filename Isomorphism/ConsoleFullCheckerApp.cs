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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Co chcesz zrobić?");
            Console.WriteLine("Przetestuj algorytm dokładny: Wybierz D");
            Console.WriteLine("Przetestuj algorytm aproksymacyjny: Wybierz A");
            Console.WriteLine("Porównaj oba algorytmy dla tych samych grafów: Wybierz P");
            Console.WriteLine("Wyłącz: Wybierz X");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var t = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (t)
            {
                case "D":
                    RunWithFile(1);
                    break;
                case "A":
                    RunWithFile(2);
                    break;
                case "P":
                    RunWithFile(3);
                    break;
                case "x":
                    return;
            }
            RunApp();
        }
   
        public static void RunWithFile(int i)
        {
            Graph G = GetGraph("G");
            Graph H = GetGraph("H");
            Stopwatch sw = new Stopwatch();
            if(i==1 || i==3)
            {
                sw.Start();
                SearchSubGraph subGraph = new SearchSubGraph(G, H);
                var map = subGraph.BestMapping;
                sw.Stop();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Algorytm dokładny: ");
                ShowMapping(map, sw.ElapsedTicks);

            }
            sw.Reset();
            if(i==2 ||i==3)
            {
                sw.Start();
                var map = FindGraphByApproximationAlgorithm.Search(G, H);
                sw.Stop();
                Console.WriteLine("Algorytm aproksymacyjny: ");
                ShowMapping(map, sw.ElapsedTicks);
            }
        }

        public static void ShowMapping(List<int[]> mapp, long Ticks)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int j = 0; j < mapp[0].Length; j++)
            {
                Console.Write($"{mapp[0][j]}->{mapp[1][j]}  ");
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"  {Ticks}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static Graph GetGraph(string name)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Wprowadź ścieżkę do grafu {name}:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string path = Console.ReadLine();

            try
            {
                return CreateExampleGraphs.CreateFromCSVFile(path);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błędna ścieżka lub plik");
                Console.ForegroundColor = ConsoleColor.White;
                return GetGraph(name);
            }
        }
    }
}
