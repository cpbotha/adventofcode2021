using aoclib;

var nums = Utils.GetTrimmedInput(@"input.txt").Split("\n").ToList();
var xlen = nums[0].Length;
var ylen = nums.Count;

// part 1 submitted
// count all the bits in each column
var freqs = nums.Select(num => num.Select(b => int.Parse(b.ToString()))).Aggregate((a, b) => a.Zip(b, (x, y) => x + y));
var gamma = freqs.Select((f,idx) => f > ylen / 2 ? 1 << xlen - 1 - idx : 0).Aggregate((x,y) => x | y);
// lazy, so just flipping the 1-counts to get the 0-counts
var epsilon = freqs.Select((f,idx) => f < ylen / 2 ? 1 << xlen - 1 - idx : 0).Aggregate((x,y) => x | y);

Console.WriteLine($"{gamma}, {epsilon}, {gamma*epsilon}");

// part 2 submitted

// count bits in column K
// filter
// repeat

var newNums = nums;
for (int bitIdx = 0; bitIdx < xlen; bitIdx++)
{
    // we have >= because 1 wins if they are equally common
    // not only that, but also floating point comparison so e.g. 3 x 1 does not win out of 7 total
    var winningBit = newNums.Select(num => num[bitIdx]).Count(c => c == '1') >= newNums.Count() / 2.0 ? '1' : '0';
    newNums = newNums.Where(num => num[bitIdx] == winningBit).ToList();
    if (newNums.Count == 1) break;
}

var oxygen = newNums[0];

newNums = nums;
for (int bitIdx = 0; bitIdx < xlen; bitIdx++)
{
    // we have <= because 0 wins if they are equally common
    // not only that, but also floating point comparison for uneven numbers of candidates
    var leastCommonBit = newNums.Select(num => num[bitIdx]).Count(c => c == '0') <= newNums.Count() / 2.0 ? '0' : '1';
    newNums = newNums.Where(num => num[bitIdx] == leastCommonBit).ToList();
    if (newNums.Count == 1) break;
}

var co2 = newNums[0];

Console.WriteLine($"{oxygen}, {co2}, {Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)}");
