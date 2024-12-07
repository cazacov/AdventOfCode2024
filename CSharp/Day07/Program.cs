namespace Day07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 7 Part 1: {Part1(input)}");
            Console.WriteLine($"Day 7 Part 2: {Part2(input)}");
        }

        private static string Part1(List<Equation> input)
        {
            Int64 result = 0;
            foreach (var equation in input)
            {
                if (CanBeFixed(equation))
                {
                    result += equation.Result;
                }
            }
            return result.ToString();
        }

        private static string Part2(List<Equation> input)
        {
            Int64 result = 0;
            foreach (var equation in input)
            {

                if (CanBeFixed(equation) || CanBeFixed2(equation))
                {
                    result += equation.Result;
                }
            }
            return result.ToString();
        }

        private static bool CanBeFixed(Equation equation)
        {
            var operations = equation.Values.Count - 1;

            for (long n = 0; n < 1 << operations; n++)
            {
                var res = equation.Values[0];
                for (var i = 0; i < operations; i++)
                {
                    var value = equation.Values[i+1];
                    long operation = (n >> i) & 1;
                    if (operation == 0)
                    {
                        res += value;
                    }
                    else
                    {
                        res *= value;
                    }
                }

                if (res == equation.Result)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool CanBeFixed2(Equation equation)
        {
            var operations = equation.Values.Count - 1;
            var opcodes = new int[operations];
            for (var i = 0; i < operations; i++)
            {
                opcodes[i] = 0;
            }

            while (true) 
            {
                var res = equation.Values[0];
                for (int i = 0; i < operations; i++)
                {
                    var value = equation.Values[i + 1];
                    if (opcodes[i] == 0)
                    {
                        res += value;
                    }
                    else if (opcodes[i] == 1)
                    {
                        res *= value;
                    }
                    else
                    {
                        res = long.Parse(res.ToString() + value.ToString());
                    }
                    if (res > equation.Result)
                    {
                        break;
                    }
                }

                if (res == equation.Result)
                {
                    return true;
                }

                // next opcode
                var carry = 1;
                for (var i = 0; i < operations; i++)
                {
                    if (opcodes[i] == 2)
                    {
                        opcodes[i] = 0;
                        carry = 1;
                    }
                    else
                    {
                        opcodes[i]++;
                        carry = 0;
                        break;
                    }
                }
                if (carry == 1)
                {
                    break;
                }
            }
            return false;
        }

        private static List<Equation> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");

            var result = new List<Equation>();

            foreach (var line in lines)
            {
                var equation = new Equation
                {
                    Result = long.Parse(line[..line.IndexOf(":")])
                };

                var values = line[(line.IndexOf(":") + 2)..];
                var parts = values.Split(" ");
                equation.Values.AddRange(parts.Select(long.Parse));
                result.Add(equation);
            }

            return result;
        }
    }
}
