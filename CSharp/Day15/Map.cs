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

        public void DisplayDirection(int direction, int step)
        {
            var ch = new Char[] { '^', '>', 'v', '<' };
            Console.Write($"Step {step} Move {ch[direction]}: ");
        }

        public void Upscale()
        {
            this.Start = new Pos(this.Start.X *2, this.Start.Y);
            var walls = new List<Pos>(Walls);
            this.Walls.Clear();
            foreach (var wall in walls)
            {
                this.Walls.Add(new Pos(wall.X * 2, wall.Y));
                this.Walls.Add(new Pos(wall.X * 2 + 1, wall.Y));
            }

            var containers = new List<Pos>(Containers);
            this.Containers.Clear();
            foreach (var container in containers)
            {
                this.Containers.Add(new Pos(container.X * 2, container.Y));
            }
            this.Width *= 2;
        }

        public Pos ContainerInPos(Pos nextPos)
        {
            var pos1 = this.Containers.FirstOrDefault(c => c.Equals(nextPos));
            if (pos1 != null)
            {
                return pos1;
            }

            var pos2 = this.Containers.FirstOrDefault(c => c.Equals(new Pos(nextPos.X-1, nextPos.Y)));
            if (pos2 != null)
            {
                return pos2;
            }

            return null;
        }

        public void Display2()
        {

            Console.Write("    ");
            for (var x = 0; x < Width; x++)
            {
                Console.Write(x%10);
            }
            Console.WriteLine();
            for (var y = 0; y < Height; y++)
            {
                Console.Write(String.Format("{0,3}",y));
                Console.Write(" ");

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
                        Console.Write("[");
                    }
                    else if (Containers.Contains(new Pos(pos.X-1, pos.Y)))
                    {
                        Console.Write("]");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
