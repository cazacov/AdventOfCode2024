
namespace Day12
{
    internal class Region
    {
        public char plant;
        public HashSet<Pos> plots = new HashSet<Pos>();
        public List<Fence> fence = new List<Fence>();
    }
}
