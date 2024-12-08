namespace Day06
{
    internal record Antenna (int X, int Y, char Frequency) 
    {
        public string PosKey => $"{X}-{Y}";


        public bool IsOutOfMap(int width, int height)
        {
            return X < 0 || Y < 0 || X >= width || Y >= height;
        }
    }

}
