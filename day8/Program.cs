using System.Diagnostics;
using System.Security.Cryptography;
using aoclib;

// multiple 4-digit displays
// for each display note the 10 unique signal patterns

// 1 - 2 segs
// 4 - 4 segs
// 7 - 3 segs
// 8 - 7 segs
// 2,3,5  - 5 segs
// 0,6,9  - 6 segs
// iow in part 1: count the number of outputs with 2,4,3 or 9 segs

var lines = Utils.GetTrimmedInput(@"input.txt").Split("\n");
var insOuts = lines.Select(l => l.Split(" | "));
var outs = insOuts.Select(o => o[1].Split(" "));
int[] u = { 2, 3, 4, 7 };
var sumU = outs.Select(comb => comb.Count(s => u.Contains(s.Length))).Sum();

Console.WriteLine($"part 1: {sumU}");
//Debug.Assert(sumU == 26);

// 10 null references
//var ipd = new HashSet<char>[10];
double total = 0.0;
foreach (var inOut in insOuts)
{
    var ipd = Enumerable.Range(0,10).Select(i => new HashSet<char>()).ToArray();
    var inPats = inOut[0].Split(" ");

    // pass 1: pick out the digits that are unique w.r.t. number of segments
    foreach (var inPat in inPats)
    {
        if (u.Contains(inPat.Length))
        {
            ipd[inPat.Length switch { 2 => 1, 4 => 4, 3 => 7, 7 => 8 }] = inPat.ToHashSet();
        }
    }

    // pass 2: figure out the other non-unique patterns based on relations to the unique ones
    foreach (var inPat0 in inPats)
    {
        var inPat = inPat0.ToHashSet();
        if (!u.Contains(inPat.Count()))
        {
            if (inPat.Count() == 5)
            {
                if (inPat.IsSupersetOf(ipd[1]))
                {
                    // 3 contains 1
                    ipd[3] = inPat;
                }
                else if (inPat.Intersect(ipd[4]).Count() == 3)
                {
                    // 5 contains 3 out of 4 of 4's segments
                    ipd[5] = inPat;
                }
                else
                {
                    ipd[2] = inPat;
                }
            }
            else if (inPat.Count() == 6)
            {
                if (inPat.IsSupersetOf(ipd[4]))
                {
                    // 9 contains 4
                    ipd[9] = inPat;
                }
                else if (inPat.IsSupersetOf(ipd[7]))
                {
                    // between 0 and 6, only 0 contains 7
                    ipd[0] = inPat;
                }
                else
                {
                    ipd[6] = inPat;
                }
            }
        }
    }
    
    // now we have the decoder ring IPD!
    
    // go through each of the output digit patterns, compare with stored patterns in decoder ring,
    // index position is the digit
    var bleh = inOut[1].Split(" ").Select(pat =>
        ipd.Select((kpat, idx) => new { kpat, idx }).First(pair => pat.ToHashSet().SetEquals(pair.kpat))
            .idx).Select((dig, c) => dig * Math.Pow(10,3-c)).Aggregate((x,y) => x+y);
    total += bleh;
    //Console.WriteLine($"{inOut[1]} -- {bleh}");
}

Console.WriteLine($"Part 2 total: {total}");


// part 2
// 5 segs
// - if contains 1 then 3 elif intersect w 4 == 3 then 5 else 2
// 6 segs
// - if contains 4 then 9 elif contains 7 then 0 else 6

