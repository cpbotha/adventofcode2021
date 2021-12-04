// this was the opposite of fun for me

using System.Text.RegularExpressions;
using aoclib;

// - find all boards with num
// - mark num everywhere
// - on all boards that have been marked, search for rows / columns

var lines = Utils.GetTrimmedInput(@"input.txt").Split("\n\n");
// first line with numbers that are called out
var nums = lines.First().Split(',').Select(int.Parse);

// string.Split() will give you empty elements, i.e. although it splits on space or \n, it does not merge them (empty elems)
// inner bt.Trim() was also required not to get empty elems between boards
var rx = new Regex(@"\s+", RegexOptions.Compiled);
var boards = lines.Skip(1).Select(bt => rx.Split(bt.Trim()).Select(int.Parse).ToList()).ToList();

// this matches boards (initialised to 0 thanks)
//int[,] masks = new int[boards.Count, 25];
int[][] masks = new int[boards.Count][];
for (var bi = 0; bi < boards.Count; bi++)
{
    masks[bi] = new int[25];
}

bool bingo = false;

// board, 0 for row / 1 for col, rowOrCol
var called = new HashSet < (int,int,int) >();
var boardsWon = new HashSet<int>();
foreach (var num in nums)
{
    for (var bi = 0; bi < boards.Count; bi++)
    {
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

    // for part 2, we do this AFTER we have updated the mask
    for (var bi = 0; bi < boards.Count(); bi++)
    {
        var board = boards[bi];
        var mask = masks[bi];
        
        // check board for bingo via mask
        // first find row with 5
        var bingoRR = mask.Chunk(5).Select((c, ri) => new { c, ri }).FirstOrDefault(pair => pair.c.Sum() == 5);

        if (bingoRR != null)
        {
            var t = (bi, 0, bingoRR.ri);
            if (!called.Contains(t))
            {
                called.Add(t);
                if (!boardsWon.Contains(bi))
                {
                    boardsWon.Add(bi);
                    var sum = board.Zip(mask).Where(bm => bm.Second == 0).Select(bm => bm.First).Sum();
                    Console.WriteLine($"{num}, board {bi}, row {bingoRR.ri}, {sum}, {num * sum}");                    
                }

            }
        } 

        // find first column with 5
        int bingoC = -1;

        // part 2: even if we have just called out a row, we should also find columns?
        if (bingoRR == null)
        {
            for (int ci = 0; ci < 5; ci++)
            {
                if (mask.Skip(ci).Where((x, i) => i % 5 == 0).Sum() == 5)
                {
                    bingoC = ci;
                    
                    var t = (bi, 1, ci);
                    if (!called.Contains(t))
                    {
                        called.Add(t);
                        if (!boardsWon.Contains(bi))
                        {
                            boardsWon.Add(bi);
                            var sum = board.Zip(mask).Where(bm => bm.Second == 0).Select(bm => bm.First).Sum();
                            Console.WriteLine($"{num}, board {bi}, col {ci}, {sum}, {num * sum}");
                        }
                    }                    
                    
                    break;
                }
            }
        }
        
    }
}

