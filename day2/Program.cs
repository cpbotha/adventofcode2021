using System.Diagnostics;
using aoclib;

// start at hpos, depth = 0,0
// follow forward, down instructions (convert to dx,dy)
// multiply final pos x depth

var instrs = Utils.GetTrimmedInput(@"input.txt").Split("\n").ToList();

var posh = 0;
var posd = 0;
int dh;
int dd;

foreach (var instr in instrs)
{
    var nd = instr.Split();
    var val = int.Parse(nd[1]);
    (dh, dd) = nd[0] switch
    {
        "forward" => (val, 0),
        "up" => (0, -val),
        "down" => (0, val)
    };
        
    posh += dh;
    posd += dd;
}

Console.WriteLine($"Part 1: {posh}, {posd}, {posh*posd}");

// part 2, track aim as well
var aim = 0;
int daim;
posh = 0;
posd = 0;

foreach (var instr in instrs)
{
    var nd = instr.Split();
    var val = int.Parse(nd[1]);
    (dh, dd, daim) = nd[0] switch
    {
        "forward" => (val, aim * val, 0),
        "up" => (0, 0, -val),
        "down" => (0, 0, val)
    };
        
    posh += dh;
    posd += dd;
    aim += daim;
}


Console.WriteLine($"Part 2: {posh}, {posd}, {posh*posd}");