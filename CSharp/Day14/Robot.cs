using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    [DebuggerDisplay("{Position.X},{Position.Y} -> {Speed.X},{Speed.Y}")]
    internal record Robot (Pos Position, Pos Speed)
    {
    }
}
