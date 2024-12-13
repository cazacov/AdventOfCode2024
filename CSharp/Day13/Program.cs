using System.Text.RegularExpressions;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 13 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 13 Part 2: {Part2(input)}");
        }

        private static string Part1(List<Machine> machines)
        {
            var score = 0L;

            foreach (var machine in machines)
            {
                score += MachineScore(machine);
            }
            return score.ToString();
        }

        private static string Part2(List<Machine> machines)
        {
            var score = 0L;

            foreach (var machine in machines)
            {
                var m = new Machine(machine.Ax, machine.Ay, machine.Bx, machine.By, machine.PrizeX + 10000000000000L,
                    machine.PrizeY + 10000000000000L);
                score += MachineScore2(m);
            }
            return score.ToString();
        }

        private static Int64 MachineScore(Machine machine)
        {
            var score = Int64.MaxValue;
            var maxA = Math.Min(machine.PrizeX / machine.Ax, machine.PrizeY / machine.Ay);

            for (var i = 0; i <= maxA; i++)
            {
                var stepX = machine.Ax * i;
                if ((machine.PrizeX - stepX) % machine.Bx != 0)
                {
                    continue;
                }

                var stepY = machine.Ay * i;
                if ((machine.PrizeY - stepY) % machine.By != 0)
                {
                    continue;
                }

                if ((machine.PrizeX - stepX) / machine.Bx != (machine.PrizeY - stepY) / machine.By)
                {
                    continue;
                }

                var res = i *3 + (machine.PrizeX - stepX) / machine.Bx;
                if (res < score)
                {
                    score = res;
                }
            }

            if (score == Int64.MaxValue)
            {
                return 0;
            }
            else
            {
                return score;
            }
        }

        private static long MachineScore2(Machine machine)
        {
            var d = machine.Ax * machine.By - machine.Ay * machine.Bx;

            if (d != 0)
            {
                var dk = (machine.PrizeX * machine.By - machine.PrizeY * machine.Bx);
                var dl = (machine.Ax* machine.PrizeY - machine.Ay * machine.PrizeX);

                if (dk % d != 0 || dl % d != 0)
                {
                    return 0;
                }
                return 3 * dk/d + dl/d;
            }
            else
            {
                // both machines define the same equation
                var g = Gcd(machine.Ax, machine.Bx);
                if (g > 1 && machine.PrizeX % g != 0)
                {
                    return 0;
                }
                
                var k = 0;
                while ((machine.PrizeX - k * machine.Ax) % machine.Bx != 0)
                {
                    k++;
                }
                return 3 * k + (machine.PrizeX - k * machine.Ax) / machine.Bx;
            }
        }

        private static List<Machine> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var result = new List<Machine>();
            int i = 0;
            while (i < lines.Length)
            {
                var m1 = Regex.Match(lines[i], "Button A: X.(\\d*), Y.(\\d*)");
                if (!m1.Success)
                {
                    Console.WriteLine("oups");
                    break;
                }
                var m2 = Regex.Match(lines[i+1], "Button B: X.(\\d*), Y.(\\d*)");
                var m3 = Regex.Match(lines[i + 2], "Prize: X=(\\d*), Y=(\\d*)");

                result.Add(new Machine(
                    int.Parse(m1.Groups[1].Value),
                    int.Parse(m1.Groups[2].Value),
                    int.Parse(m2.Groups[1].Value),
                    int.Parse(m2.Groups[2].Value),
                    int.Parse(m3.Groups[1].Value),
                    int.Parse(m3.Groups[2].Value)
                    ));
                i += 4;
            }


            return result;
        }

        private static long Gcd(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0)
            {
                return b;
            }
            if (b == 0)
            {
                return a;
            }

            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a + b;
        }
    }
}
