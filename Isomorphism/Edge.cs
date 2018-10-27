namespace Isomorphism
{
    internal class Edge
    {
        public int From { get; private set; }
        public int To { get; private set; }

        public Edge(int from, int to)
        {
            this.From = from;
            this.To = to;
        }
    }
}