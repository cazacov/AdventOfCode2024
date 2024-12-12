using System.Data;

namespace Day12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 12 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 12 Part 2: {Part2(input)}");
        }

        private static string Part1(char[,] input)
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);

            var regions = new List<Region>();
            var visited = new HashSet<Pos>();

            var result = 0;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var current = new Pos(x, y);
                    if (visited.Contains(current))
                    {
                        continue;
                    }
                    var region = new Region();
                    region.plant = input[y, x];
                    region.plots = FindRegion(input[y, x], current, input, width, height);
                    visited.UnionWith(region.plots);

                    var area = region.plots.Count;
                    var perimeter = Perimeter(region.plots);
                    var price = area * perimeter;
                    result += price;
                    regions.Add(region);
                }
            }

            return result.ToString();
        }

        private static int Perimeter(HashSet<Pos> regionPlots)
        {
            int result = 0;
            foreach (var plot in regionPlots)
            {
                foreach (var neighbor in plot.AllNeighbors())
                {
                    if (!regionPlots.Contains(neighbor))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private static HashSet<Pos> FindRegion(char plant, Pos current, char[,] input, int width, int height)
        {
            var result = new List<Pos>();
            result.Add(current);

            var visited = new HashSet<Pos>();

            var queue = new List<Pos>(){current};

            while (queue.Any())
            {
                var next = queue.First();
                queue.RemoveAt(0);
                visited.Add(next);
                foreach (var neighbor in next.GetNeighbors(width, height))
                {
                    if (input[neighbor.Y, neighbor.X] != plant)
                    {
                        continue;
                    }
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    if (queue.Contains(neighbor))
                    {
                        continue;
                    }
                    queue.Add(neighbor);
                }
            }
            return visited;
        }

        private static string Part2(char[,] input)
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);

            var regions = new List<Region>();
            var visited = new HashSet<Pos>();

            var result = 0L;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var current = new Pos(x, y);
                    if (visited.Contains(current))
                    {
                        continue;
                    }

                    var region = new Region
                    {
                        plant = input[y, x],
                        plots = FindRegion(input[y, x], current, input, width, height)
                    };
                    visited.UnionWith(region.plots);

                    long area = region.plots.Count;

                    if (x ==121 && y == 100)
                    {
                        Console.WriteLine($"A region of {region.plant} plants with area {area} at {x},{y}");
                    }

                    long sideCount = SideCount(region.plots);

                    if (sideCount % 2 != 0)
                    {
                        Console.WriteLine($"A region of {region.plant} plants with area {area} and sides {sideCount} at {x},{y}");
                    }

                    var price = area * sideCount;

                    //Console.WriteLine($"A region of {region.plant} plants with price {area} * {sideCount} = {price}");
                    result += price;
                    regions.Add(region);
                }
            }

            return result.ToString();
        }

        private static int SideCount(HashSet<Pos> regionPlots)
        {
            var fences = new List<Fence>();
            foreach (var plot in regionPlots)
            {
                var neighbors = plot.AllNeighbors();
                for (var dir = 0; dir < 4; dir++)
                {
                    var neighbor = neighbors[dir];
                    if (!regionPlots.Contains(neighbor))
                    {
                        var fence = new Fence()
                        {
                            Dir = dir,
                            X1 = plot.X,
                            Y1 = plot.Y,
                            X2 = plot.X,
                            Y2 = plot.Y
                        };
                        fences.Add(fence);
                    }
                }
            }

            var maxX = regionPlots.Max(p => p.X);
            var maxY = regionPlots.Max(p => p.Y);

            for (var y = 0; y <= maxY; y++) {
                for (var x = 0; x <= maxX; x++) {
                    for (var dir = 0; dir < 4; dir++)
                    {
                        var current = fences.FirstOrDefault(f => f.X1 == x && f.Y1 == y && f.Dir == dir);
                        if (current == null)
                        {
                            continue;
                        }

                        while (true)
                        {
                            if (current.Dir is 0 or 3)
                            {
                                var next = fences.FirstOrDefault(f =>
                                    f.Dir == current.Dir && f.X1 == current.X2 + 1 && f.Y1 == current.Y1);
                                if (next == null)
                                {
                                    break;
                                }

                                current.X2 = next.X2;
                                fences.Remove(next);
                            }

                            if (current.Dir is 1 or 2)
                            {
                                var next = fences.FirstOrDefault(f =>
                                    f.Dir == current.Dir && f.Y1 == current.Y2 + 1 && f.X1 == current.X1);
                                if (next == null)
                                {
                                    break;
                                }

                                current.Y2 = next.Y2;
                                fences.Remove(next);
                            }
                        }
                    }
                }
            }
            return fences.Count;
        }

        private static char[,] ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var result = new char[lines.Length, lines[0].Length];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Length; j++)
                {
                    result[i, j] = lines[i][j];
                }
            }

            return result;
        }
    }
}
