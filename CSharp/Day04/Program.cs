namespace Day04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 4: Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 4: Part 2: {Part2(input)}");
        }

        private static List<string> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            return lines.ToList();
        }

        private static string Part1(List<string> input)
        {
            var maxX = input[0].Length;
            var maxY = input.Count;
            var map = new bool[maxY, maxX];

            var count = 0;
            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    if (input[y][x] == 'X')
                    {
                        count += isXMAS(input, maxX, maxY, x, y, "XMAS", map);
                    }
                }
            }
            ShowMap(input, map);
            return count.ToString();
        }

        
        private static int isXMAS(List<string> input, int maxX, int maxY, int x, int y, string word, bool[,] map)
        {
            const int DIR_COUNT = 8;

            int[] dx = new int[DIR_COUNT] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = new int[DIR_COUNT] { 0, 1, 1, 1, 0, -1, -1, -1 };

            var occurrences = 0;

            for (var dir = 0; dir < DIR_COUNT; dir++)
            {
                for (var l = 0; l < word.Length; l++)
                {
                    var xx = x + l * dx[dir];
                    var yy = y + l * dy[dir];
                    if (xx < 0 || xx >= maxX || yy < 0 || yy >= maxY)
                    {
                        goto next;
                    }

                    if (input[yy][xx] != word[l])
                    {
                        goto next;
                    }
                }

                FillMap1(x, y, word, map, dx, dir, dy);
                occurrences++;
next:           ;
            }
            return occurrences;
        }

        private static string Part2(List<string> input)
        {
            var maxX = input[0].Length;
            var maxY = input.Count;
            var map = new bool[maxY, maxX];

            var count = 0;
            for (int y = 1; y < maxY-1; y++)
            {
                for (int x = 1; x < maxX-1; x++)
                {
                    if (input[y][x] == 'A')
                    {
                        if (isX_MAS(input, x, y, map))
                        {
                            count++;
                        }
                    }
                }
            }
            ShowMap(input, map);
            return count.ToString();
        }

        private static bool isX_MAS(List<string> input, int x, int y, bool[,] map)
        {
            var words = new string[] { "MSMS", "MMSS", "SSMM", "SMSM" };

            foreach (var word in words)
            {

                var isOk = true;
                var idx = 0;
                for (var yy = y - 1; yy <= y + 1; yy++)
                {
                    for (var xx = x - 1; xx <= x + 1; xx++)
                    {
                        if (xx == x || yy == y)
                        { 
                            continue;
                        }
                        if (input[yy][xx] != word[idx])
                        {
                            goto next;
                        }

                        idx++;
                    }
                }

                FillMap2(x, y, map);
                return true;
next:           ;
            }
            return false;
        }

        private static void ShowMap(List<string> input, bool[,] map)
        {
            var color = Console.ForegroundColor;

            for (var y = 0; y < map.GetLength(0); y++)
            {
                for (var x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x])
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    Console.Write(input[y][x]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = color;
        }

        private static void FillMap1(int x, int y, string word, bool[,] map, int[] dx, int dir, int[] dy)
        {
            for (var l = 0; l < word.Length; l++)
            {
                var xx = x + l * dx[dir];
                var yy = y + l * dy[dir];
                map[yy, xx] = true;
            }
        }

        private static void FillMap2(int x, int y, bool[,] map)
        {
            for (var yy = y - 1; yy <= y + 1; yy++)
            {
                for (var xx = x - 1; xx <= x + 1; xx++)
                {
                    if (xx == x || yy == y)
                    {
                        continue;
                    }
                    map[yy, xx] = true;
                }
            }

            map[y, x] = true;
        }
    }
}
