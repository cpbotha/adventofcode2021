// this was the opposite of fun for me

// What I learned (often much too late)
// - once ANY row or column on a board has been found, that board has "won" and you should calculate the checksum.
//   you can now forget about that board!

using System.Text.RegularExpressions;
using aoclib;

var lines = Utils.GetTrimmedInput(@"input.txt").Split("\n\n");
// first line with numbers that are called out
var nums = lines.First().Split(',').Select(int.Parse);

// string.Split() will give you empty elements, i.e. although it splits on space or \n, it does not merge them (empty elems)
// inner bt.Trim() was also required not to get empty elems between boards
var rx = new Regex(@"\s+", RegexOptions.Compiled);
var boards = lines.Skip(1).Select(bt => rx.Split(bt.Trim()).Select(int.Parse).ToList()).ToList();

// multi-dimensional array:
//int[,] masks = new int[boards.Count, 25];
// went for jagged arrays so I can get out the mask matching a specific board
int[][] masks = new int[boards.Count][];
for (var bi = 0; bi < boards.Count; bi++)
{
    masks[bi] = new int[25];
}

// before I realised that I can ignore boards that have won, I kept track of the called rows / columns
// board, 0 for row / 1 for col, rowOrCol
// var called = new HashSet < (int,int,int) >();
// now just keep track of boards that are not part of the fun anymore
var boardsWon = new HashSet<int>();
foreach (var num in nums)
{
    // update all boards with this called number
    for (var bi = 0; bi < boards.Count; bi++)
    {
        if (boardsWon.Contains(bi)) continue;
        var board = boards[bi];
        var mask = masks[bi];
        // find num in board
        for (var ei = 0; ei < board.Count; ei++)
        {
            if (board[ei] == num)
            {
                // TODO: record that this board should be checked, currently we check each one
                mask[ei] = 1;
            }
        }
    }

    // now scan all boards that have not yet won for a row or column
    for (var bi = 0; bi < boards.Count(); bi++)
    {
        // we ignore boards that have already won
        if (boardsWon.Contains(bi)) continue;
        var board = boards[bi];
        var mask = masks[bi];
        
        // check board for bingo via mask
        // first find row with 5
        var bingoRR = mask.Chunk(5).Select((c, ri) => new { c, ri }).FirstOrDefault(pair => pair.c.Sum() == 5);

        if (bingoRR != null)
        {
            boardsWon.Add(bi);
            var sum = board.Zip(mask).Where(bm => bm.Second == 0).Select(bm => bm.First).Sum();
            Console.WriteLine($"{num}, board {bi}, row {bingoRR.ri}, {sum}, {num * sum}");                    
        } 

        if (bingoRR == null)
        {
            for (int ci = 0; ci < 5; ci++)
            {
                if (mask.Skip(ci).Where((x, i) => i % 5 == 0).Sum() == 5)
                {
                    boardsWon.Add(bi);
                    var sum = board.Zip(mask).Where(bm => bm.Second == 0).Select(bm => bm.First).Sum();
                    Console.WriteLine($"{num}, board {bi}, col {ci}, {sum}, {num * sum}");
                    
                    // we found a bingo column, so we can stop
                    break;
                }
            }
        }
    }
}

