using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Map
    {
        public HashSet<Pos> Containers = new HashSet<Pos>();
        public HashSet<Pos> Walls = new HashSet<Pos>();
        public Pos Start = new Pos(0, 0);

        public void Init()
        {
            this.Width = Walls.Max(w => w.X) + 1;
            this.Height = Walls.Max(w => w.Y) + 1;
        }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public void Display()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var pos = new Pos(x, y);
                    if (Start.Equals(pos))
                    {
                        Console.Write("@");
                    }
                    else if (Walls.Contains(pos))
                    {
                        Console.Write("#");
                    }
                    else if (Containers.Contains(pos))
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }

        public void DisplayDirection(int direction)
        {
            var ch = new Char[] { '^', '>', 'v', '<' };
            Console.WriteLine($"Move to {ch[direction]}");
        }
    }
}
