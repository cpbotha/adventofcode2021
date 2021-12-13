using aoclib;

// ): 3 points.
// ]: 57 points.
// }: 1197 points.
// >: 25137 points.

var lines = Utils.GetTrimmedInput(@"input.txt").Split("\n").ToList();

//var o = new HashSet<char>() {'(', '[', '{', '<'};
// dictionary mapping  from opening char to closing char
var o = new Dictionary<char, char>() {{'(', ')'}, { '[', ']' }, { '{', '}' }, { '<', '>' }};
var pts = new Dictionary<char, int>() {{')', 3}, { ']', 57 }, { '}', 1197 }, { '>', 25137}};

var pts2 = new Dictionary<char, int>() {{')', 1}, { ']', 2 }, { '}', 3 }, { '>', 4}};

var icScores = lines.Select(IllegalOrCompletionPoints);

Console.WriteLine($"Part 1 illegal closing character points: {icScores.Select(ic => ic.Item1).Sum()}");

var cscores = icScores.Select(ic => ic.Item2).Where(score => score > 0).ToList();
cscores.Sort();
//  426952212 too low
//  537974491 too low
// 2421222841 just right
Console.WriteLine($"Part 2 completion points: {cscores[cscores.Count / 2]}");

(long, long) IllegalOrCompletionPoints(string line)
{
    var s = new Stack<char>();
    foreach  (var c in line) {
        // is this an opening paren?
        if (o.ContainsKey(c))
        {
            // stick it in the stack
            s.Push(c);
        }
        // does the new character c match the opening character at the end of the stack?
        else if (o.GetValueOrDefault(s.Peek(), ' ') == c)
        {
            s.Pop();
        }
        // illegal closing character c, calculate points!
        else
        {
            //Console.WriteLine($"{line} -- {c}");
            // return illegal closing character points as negative
            return (pts.GetValueOrDefault(c), 0);
        }
    }

    // if we arrive here, there have been no illegal characters, we just need to close everything down
    // durnit! var here gave me an int, which silently overflowed for the large problem
    // seems you have to enable checked() for this, but that'll slow things down
    // https://stackoverflow.com/questions/19729064/why-does-c-sharp-let-me-overflow-without-any-error-or-warning-when-casting-an-in
    long cpoints = 0;
    while (s.Count > 0)
    {
        // top of stack always has opening character
        var closing = o.GetValueOrDefault(s.Pop(), ' ');
        var p = pts2.GetValueOrDefault(closing, 0);
        if (p < 1) throw new Exception("BLEH");
        cpoints = 5 * cpoints + p;
        //Console.Write(closing);
    }

    return (0, cpoints);
}
