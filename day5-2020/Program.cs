using aoclib;

var boardingPasses = Bleh.GetTrimmedInput(@"input.txt").Split("\n");

// we're going to need this again, so materialise
var allIds = boardingPasses.Select(GetSeatId).ToHashSet();
var maxId = allIds.Max();

Console.WriteLine($"Part 1 highest seat ID: {maxId}");

// imperative form:
int mySeatId = -1;
for (var seatId = allIds.Min()+1; seatId < maxId; seatId++) {
    if (!allIds.Contains(seatId) && allIds.Contains(seatId-1) && allIds.Contains(seatId+1)) {
        mySeatId = seatId;
        break;
    }
}

// more functional:
var mySeatId2 = Enumerable.Range(allIds.Min() + 1, allIds.Count - 1).First(seatId =>
    !allIds.Contains(seatId) && allIds.Contains(seatId - 1) && allIds.Contains(seatId + 1));

Console.WriteLine($"Part 2 my seat ID: {mySeatId2}");

int GetSeatId(string seatCode)
{
    // surprisingly, I got this linq query right at the second try
    int rowNum = seatCode.Substring(0, 7)
        // only F represents 1 bit; shift left so that you would get 64 for the first one, and so on
        .Select((rowChar, idx) => rowChar == 'B' ? 1 << 6 - idx : 0)
        // OR everything together
        .Aggregate((x, y) => x | y);

    int colNum = seatCode.Substring(7, 3)
        .Select((rowChar, idx) => rowChar == 'R' ? 1 << 2 - idx : 0)
        .Aggregate((x,y) => x | y);

    return rowNum * 8 + colNum;
}
