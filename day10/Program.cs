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

Console.WriteLine($"Part 1 illegal closing character points: {lines.Select(IllegalPoints).Sum()}");

int IllegalPoints(string line)
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
            Console.WriteLine($"{line} -- {c}");
            return pts.GetValueOrDefault(c);
        }
    }

    return 0;
}
