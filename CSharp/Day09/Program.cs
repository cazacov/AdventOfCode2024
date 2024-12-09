namespace Day09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 9 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 9 Part 2: {Part2(input)}");
        }

        private static string Part1(List<string> input)
        {
            var sectors = new List<Sector>();

            var filemap = input[0];
            bool isFile = true;
            int fileId = 0;
            for (int i = 0; i < filemap.Length; i++)
            {
                var len = filemap[i] - '0';
                if (isFile)
                {
                    for (int j = 0; j < len; j++)
                    {
                        sectors.Add(new Sector(true, fileId));
                    }
                    fileId++;
                }
                else
                {
                    for (int j = 0; j < len; j++)
                    {
                        sectors.Add(new Sector(false, -1));
                    }
                }
                isFile = !isFile;
            }
            Defragment(sectors);

            var checksum = 0L;
            int pos = 0;
            while (pos < sectors.Count)
            {
                if (sectors[pos].HasFile)
                {
                    checksum +=  pos * sectors[pos].FileId;
                }
                else
                {
                    break;
                }
                pos++;
            }
            return checksum.ToString();
        }

        private static void Defragment(List<Sector> sectors)
        {
            int firstEmpty = 0;
            for (int i = 0; i < sectors.Count; i++)
            {
                if (!sectors[i].HasFile)
                {
                    firstEmpty = i;
                    break;
                }
            }

            int lastFile = 0;
            for (int i = sectors.Count-1; i >0; i--)
            {
                if (sectors[i].HasFile)
                {
                    lastFile = i;
                    break;
                }
            }

            do
            {
                while (sectors[firstEmpty].HasFile)
                {
                    firstEmpty++;
                }
                while (!sectors[lastFile].HasFile)
                {
                    lastFile--;
                }
                if (firstEmpty >= lastFile)
                {
                    break;
                }
                sectors[firstEmpty] = sectors[lastFile];
                sectors[lastFile] = new Sector(false, -1);
                firstEmpty++;
                lastFile--;
            } while (firstEmpty < lastFile);
        }

        private static string Part2(List<string> input)
        {
            var blocks = new List<Block>();

            var filemap = input[0];
            bool isFile = true;
            int fileId = 0;
            for (int i = 0; i < filemap.Length; i++)
            {
                var len = filemap[i] - '0';
                if (isFile)
                {

                    blocks.Add(new Block(true, fileId, len));
                    fileId++;
                }
                else
                {
                    blocks.Add(new Block(false, -1, len));
                }
                isFile = !isFile;
            }
            Defragment2(blocks);
            return CheckSum(blocks).ToString();
        }

        private static void Defragment2(List<Block> blocks)
        {
            var fileId = blocks.Max(b => b.FileId);
            //PrintFat(blocks);

            while (fileId > 0)
            {
                var filePos = blocks.FindIndex(b => b.HasFile && b.FileId == fileId);
                var firstFree = blocks.FindIndex(b => !b.HasFile && b.Length >= blocks[filePos].Length);

                if (firstFree > 0 && firstFree < filePos)
                {
                    var file = blocks[filePos];
                    var empty = blocks[firstFree];
                    blocks.Insert(firstFree, new Block(true, file.FileId, file.Length));
                    blocks[firstFree + 1].Length -= file.Length;
                    
                    filePos += 1;
                    blocks[filePos].HasFile = false;
                    blocks[filePos].FileId = -1;
                    while (filePos > 0 && !blocks[filePos-1].HasFile)
                    {
                        filePos--;
                    }
                    MergeSpaces(blocks, filePos);
                }

                MergeSpaces(blocks, firstFree+1);
//                Console.Write($"{fileId}  ");
                //PrintFat(blocks);
                fileId--;

                
                //Console.ReadLine();

            }
        }

        private static void PrintFat(List<Block> blocks)
        {
            var sectors = new List<Sector>();

            foreach (var block in blocks)
            {
                if (block.HasFile)
                {
                    for (int i = 0; i < block.Length; i++)
                    {
                        sectors.Add(new Sector(true, block.FileId));
                    }
                }
                else
                {
                    for (int i = 0; i < block.Length; i++)
                    {
                        sectors.Add(new Sector(false, -1));
                    }
                }
            }

            for (int i = 0; i < sectors.Count; i++)
            {
                if (sectors[i].HasFile)
                {
                    Console.Write(sectors[i].FileId);
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }

        private static long CheckSum(List<Block> blocks)
        {
            var sectors = new List<Sector>();

            foreach (var block in blocks)
            {
                if (block.HasFile)
                {
                    for (int i = 0; i < block.Length; i++)
                    {
                        sectors.Add(new Sector(true, block.FileId));
                    }
                }
                else
                {
                    for (int i = 0; i < block.Length; i++)
                    {
                        sectors.Add(new Sector(false, -1));
                    }
                }
            }

            var checkSum = 0L;
            for (int i = 0; i < sectors.Count; i++)
            {
                if (sectors[i].HasFile)
                {
                    checkSum += i * sectors[i].FileId;
                }
            }
            return checkSum;
        }

        private static void MergeSpaces(List<Block> blocks, int filePos)
        {
            while (filePos < blocks.Count && filePos + 1 < blocks.Count && !blocks[filePos].HasFile && !blocks[filePos+1].HasFile)
            {
                blocks[filePos].Length += blocks[filePos + 1].Length;
                blocks.RemoveAt(filePos + 1);
            }
        }

        private static List<string> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            return lines.ToList();
        }
    }
}
