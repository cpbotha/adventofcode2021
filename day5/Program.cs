using aoclib;

var part1 = false;

var ftCoords = Utils.GetTrimmedInput(@"input.txt")
    .Split("\n")
    .Select(l => l.Split(" -> "))
    .Select(ft => (ParsePair(ft[0]), ParsePair(ft[1]))).ToList();

var maxY = ftCoords.Select(ftc => Math.Max(ftc.Item1.y, ftc.Item2.y)).Max();
var maxX = ftCoords.Select(ftc => Math.Max(ftc.Item1.x, ftc.Item2.x)).Max();
int height = maxY + 1;
int width = maxX + 1;

// again I can't use a two-dim array:
// https://stackoverflow.com/questions/275073/why-do-c-sharp-multidimensional-arrays-not-implement-ienumerablet
//int[,] field = new int[maxR, maxC];
int[] field = new int[height * width];

foreach (var ftc in ftCoords)
{
    // part 1 all lines are straight, so we're either going to paint a row or a column
    // ARGH: in part 1, you actually have to throw out the diagonal lines
    if (part1 && !(ftc.Item1.x == ftc.Item2.x || ftc.Item1.y == ftc.Item2.y)) continue;

    var ydir = Math.Sign(ftc.Item2.y - ftc.Item1.y);
    var xdir = Math.Sign(ftc.Item2.x - ftc.Item1.x);

    for (var pos = (ftc.Item1.x, ftc.Item1.y);;)
    {
        field[pos.y * width + pos.x] += 1;
        if (pos.x == ftc.Item2.x && pos.y == ftc.Item2.y) break;
        pos.x += xdir;
        pos.y += ydir;
    };

    //PrintField(field);
    //Console.WriteLine("brle");
}

//PrintField(field);

var numDangerousAreas = field.Count(x => x >= 2);
var p2q = part1 ? "part 1, straight only" : "part 2, diagonal included";
Console.WriteLine($"number of positions with >=2 crossings: {numDangerousAreas} ({p2q})");

void PrintField(int[] field)
{
    for (int y1 = 0; y1 < height; y1++)
    {
        Console.WriteLine(string.Join( " ", field[(y1*width) .. (y1*width+width)]));
    }
}

// part 1: at how many points do at least 2 lines overlap?
Coord ParsePair(string pair)
{
    var pairC = pair.Split(',').Select(int.Parse).ToList();
    return new Coord(pairC[0], pairC[1]);
}

readonly record struct Coord(int x, int y);
    