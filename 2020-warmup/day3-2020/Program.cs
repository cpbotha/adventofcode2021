// get my aoclib stuff 
using aoclib;

// only doing part 1 because part 2 is just another nested loop
// only interesting thing is using params as in encse's solution

var lines = Utils.GetTrimmedInput(@"input.txt").Split("\n");
//var lines = File.ReadLines(@"input.txt").Where(l => l != "").ToList();
var numrows = lines.Length;
var numcols = lines[0].Length;

Console.WriteLine($"Part 1 Number of trees: {CountTrees(1, 3)}");

int CountTrees(int rv, int cv)
{
    var r = 0;
    var c = 0;
    var numTrees = 0;

    while (r < numrows)
    {
        // modulus takes care of the horizontal wrapping
        if (lines[r][c % numcols] == '#') numTrees++;
        (r, c) = (r + rv, c + cv);
    }

    return numTrees;
}
