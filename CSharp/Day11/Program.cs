namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 11 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 11 Part 2: {Part2(input)}");
        }

        private static string Part1(List<Int128> input)
        {
            for (int i = 0; i < 25; i++)
            {
                ApplyRules(input);
            }
            return input.Count().ToString();
        }

        private static void ApplyRules(List<Int128> input)
        {

            var inserts = new List<Tuple<int, Int128, Int128>>();
            for (var i = 0; i < input.Count(); i++)
            {
                if (input[i] == 0)
                {
                    input[i] = 1;
                }
                else if (input[i].ToString().Length % 2 == 0)
                {
                    var s = input[i].ToString();
                    var s1 = s.Substring(0, s.Length / 2);
                    var s2 = s.Substring(s.Length / 2);
                    inserts.Add(new Tuple<int, Int128, Int128>(i, Int128.Parse(s1), Int128.Parse(s2)));
                }
                else
                {
                    input[i] *= 2024;
                }
            }

            var offset = 0;
            for (var i = 0; i < inserts.Count(); i++)
            {
                input[inserts[i].Item1 + offset]  = inserts[i].Item2;
                input.Insert(inserts[i].Item1 + offset + 1, inserts[i].Item3);
                offset += 1;
            }
        }

        private static string Part2(List<Int128> input)
        {
            Int128 result = 0;
            foreach (var t in input)
            {
                result += Calculate(t, 75);
            }
            return result.ToString();
        }

        private static Dictionary<int, Dictionary<Int128, Int128>> cache = new Dictionary<int, Dictionary<Int128, Int128>>();

        private static Int128 Calculate(Int128 n, int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            if (cache.ContainsKey(depth))
            {
                if (cache[depth].ContainsKey(n))
                {
                    return cache[depth][n];
                }
            }
            else
            {
                cache[depth] = new Dictionary<Int128, Int128>();
            }


            Int128 result;
            if (n == 0)
            {
                result = Calculate(1, depth - 1);

            }
            else
            {
                var s = n.ToString();
                if (s.Length % 2 == 0)
                {
                    var s1 = s.Substring(0, s.Length / 2);
                    var s2 = s.Substring(s.Length / 2);
                    result = Calculate(Int128.Parse(s1), depth - 1) + Calculate(Int128.Parse(s2), depth - 1);
                }
                else
                {
                    result =  Calculate(n * 2024, depth - 1);
                }
            }
            cache[depth][n] = result;
            return result;
        }
        

        private static List<Int128> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var values = lines[0].Split(' ');
            return values.Select(x => Int128.Parse(x)).ToList();
        }
    }
}
