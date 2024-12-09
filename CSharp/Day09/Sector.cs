using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    [DebuggerDisplay("{HasFile}-{FileId}")]
    internal record Sector (bool HasFile, int FileId)
    {
    }


    [DebuggerDisplay("{HasFile}-[{FileId}]-{Length}")]
    internal class Block(bool HasFile, int FileId, int Length)
    {
        public bool HasFile { get; set; } = HasFile;
        public int FileId { get; set; } = FileId;
        public int Length { get; set; } = Length;

        public void Deconstruct(out bool HasFile, out int FileId, out int Length)
        {
            HasFile = this.HasFile;
            FileId = this.FileId;
            Length = this.Length;
        }
    }
}
