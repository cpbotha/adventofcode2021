using aoclib;

// split on double \n to split on blank line between passports
var boardingPasses = Bleh.GetTrimmedInput(@"input.txt").Split("\n");

Console.WriteLine(boardingPasses.Select(GetSeatId).Max());

//Console.WriteLine(GetSeatId("FBFBBFFRLR"));

int GetSeatId(string seatCode)
{
    // surprisingly, I got this right at the second try
    int rowNum = seatCode.Substring(0, 7)
        // only F represents 1 bit; shift left so that you would get 64 for the first one, and so on
        .Select((rowChar, idx) => rowChar switch { 'B' => 1 << 6 - idx, _ => 0 })
        // OR everything together
        .Aggregate((x, y) => x | y);

    int colNum = seatCode.Substring(7, 3)
        .Select((rowChar, idx) => rowChar switch { 'R' => 1 << 2 - idx, _ => 0 })
        .Aggregate((x,y) => x | y);

    return rowNum * 8 + colNum;

}
