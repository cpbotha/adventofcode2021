using aoclib;

var nums = Utils.GetTrimmedInput(@"input.txt").Split("\n").ToList();
var xlen = nums[0].Length;
var ylen = nums.Count;

// part 1 submitted
// count all the bits in each column; construct a new binary number with at each position the most / least common bit
var freqs = nums.Select(num => num.Select(b => int.Parse(b.ToString()))).Aggregate((a, b) => a.Zip(b, (x, y) => x + y));
var gamma = freqs.Select((f,idx) => f > ylen / 2 ? 1 << xlen - 1 - idx : 0).Aggregate((x,y) => x | y);
// lazy, so just flipping the 1-counts to get the 0-counts
var epsilon = freqs.Select((f,idx) => f < ylen / 2 ? 1 << xlen - 1 - idx : 0).Aggregate((x,y) => x | y);

Console.WriteLine($"{gamma}, {epsilon}, {gamma*epsilon}");

// part 2 submitted

// calculate most common / least common bit in column K
// filter out numbers that don't have that bit in column K
// K+1 and repeat until only one number left

var newNums = nums;
for (int bitIdx = 0; bitIdx < xlen; bitIdx++)
{
    // we have >= because 1 wins if they are equally common
    // not only that, but also floating point comparison so e.g. 3 x 1 does not win out of 7 total
    var mostCommonBit = newNums.Select(num => num[bitIdx]).Count(c => c == '1') >= newNums.Count() / 2.0 ? '1' : '0';
    newNums = newNums.Where(num => num[bitIdx] == mostCommonBit).ToList();
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

// BONUS ROUND =========================================================================================================
// part 2 recursion because fun, but is not much better
string RecOxyCO2(List<string> nums, int mostOrLeast=0, int bitIdx=0)
{
    var mostCommonBit = nums.Select(num => num[bitIdx]).Count(c => c == '1') >= nums.Count() / 2.0 ? '1' : '0';
    // for CO2 we want the least common bit (signalled by mostOrLeast=1), which is just !mostCommonBit
    var winningBit = mostOrLeast == 0 ? mostCommonBit : mostCommonBit == '1' ? '0' : '1';
    nums = nums.Where(num => num[bitIdx] == winningBit).ToList();
    return (nums.Count == 1) ? nums[0] : RecOxyCO2(nums, mostOrLeast, ++bitIdx); 
}

var roxy = RecOxyCO2(nums);
var rco2 = RecOxyCO2(nums, 1);
Console.WriteLine($"part 2 v2: oxygen {roxy}, CO2 {rco2}, bleh {Convert.ToInt32(roxy, 2) * Convert.ToInt32(rco2, 2)}");