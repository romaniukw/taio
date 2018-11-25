using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

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

        private static void GetPictureWithGraphs(Graph G, Graph H, List<int[]> mapp, string name)
        {
            Bitmap bmp = new Bitmap(1200, 600);
            for(int i=0; i<bmp.Width; i++)
            {
                for(int j=0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.AliceBlue);
                }
            }
            Graphics g = Graphics.FromImage(bmp);
            
            Point sg = new Point(300, 300);
            Point sh = new Point(900, 300);
            int r = 250;

            DrawOneGraph(G, sg, r, mapp[0], g);
            DrawOneGraph(H, sh, r, mapp[1], g);
            bmp.Save($"{name}.png", ImageFormat.Png);
        }
   
        public static void DrawOneGraph(Graph G, Point sg, int r, int[] mapping, Graphics g)
        {
            Point[] gvert = new Point[G.Vertices.Length];
            Pen black = new Pen(Brushes.Black, 1);
            Pen red = new Pen(Brushes.Red, 2);
            for (int i = 1; i<=G.Vertices.Length; i++)
            {
                double angle = i * (360 / G.Vertices.Length);
                float x = (float)(sg.X + r * Math.Sin(Math.PI * angle / 180.0));
                float y = (float)(sg.Y + r * Math.Cos(Math.PI * angle / 180.0));
                g.DrawString($"{i-1}", new Font("Arial", 13), Brushes.Black,x,y);
                if(mapping.Contains(G.Vertices[i-1].Index))
                {
                    g.FillEllipse(Brushes.Red, x - 2, y - 2, 4, 4);
                }
                else
                {
                    g.FillEllipse(Brushes.Black, x - 2, y - 2, 4, 4);
                }
                gvert[G.Vertices[i-1].Index] = new Point((int)x,(int) y);
            }

            foreach(var e in G.Edges)
            {
                if(mapping.Contains(e.From)&& mapping.Contains(e.To))
                {
                    g.DrawLine(red, gvert[e.From], gvert[e.To]);
                }
                else
                {
                    g.DrawLine(black, gvert[e.From], gvert[e.To]);
                }
            }
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
                GetPictureWithGraphs(G, H, map, "dokladny");
                Console.WriteLine("Sprawdź wizualizację w pliku 'dokladny.png' ");
            }
            sw.Reset();
            if(i==2 ||i==3)
            {
                sw.Start();
                var map = FindGraphByApproximationAlgorithm.Search(G, H);
                sw.Stop();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Algorytm aproksymacyjny: ");
                ShowMapping(map, sw.ElapsedTicks);
                if(G.Vertices.Length<20 && H.Vertices.Length<20)
                {
                    GetPictureWithGraphs(G, H, map, "aproksymacyjny");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Sprawdź wizualizację w pliku 'aproksymacyjny.png' ");
                }
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
