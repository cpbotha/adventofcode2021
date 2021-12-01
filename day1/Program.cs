using aoclib;

var numbers = Utils.GetTrimmedInput(@"input.txt").Split("\n").Select(int.Parse).ToList();

// part 1 submitted:
int numDeeper = 0;
for (var i = 1; i < numbers.Count(); i++) {
    if (numbers[i] - numbers[i-1] > 0) numDeeper++;
}

Console.WriteLine(numDeeper);

// part 2 submitted
// in pandas I would use .rolling() -> .shift() -> subtract and count
// f# has .windowed()
// c# nothing built-in for this: https://stackoverflow.com/questions/8874901/is-there-an-equivalent-to-the-f-seq-windowed-in-c
int numDeeper2 = 0;
for (var i = 2; i < numbers.Count() - 1; i++) {
    var cur = numbers[i-1] + numbers[i] + numbers[i+1];
    var prev = numbers[i-2] + numbers[i-1] + numbers[i];
    if (cur - prev > 0) numDeeper2++;
}

Console.WriteLine(numDeeper2);

// BONUS round starts here =============================================================================================

// part 1 after finding https://stackoverflow.com/a/3874078/532513
Console.WriteLine($"p1 v2: {numbers.Zip(numbers.Skip(1), (current, next) => next - current).Count(e => e > 0)}");

// zip three sequences to calculate the rolling sum
// consider numbers.Select((num, idx) => use idx to sum shifted, taking care of overflows) and so on...
var summedDepth = numbers.Zip(numbers.Skip(1), numbers.Skip(2)).Select((x) => x.First+x.Second+x.Third);
// zip the rolling sum with a shifted version of itself to calculate the depth gradient
var numDeeper2V2 = summedDepth.Zip(summedDepth.Skip(1), (current, next) => next - current).Count(e => e > 0);
Console.WriteLine($"p2 v2: {numDeeper2V2}");

// Ross MacArthur lets us know that you don't even need to sum in part 2!
// (b + c + d) - (a + b + c) is the same as d - a
// so if i+3th > i that means the group is deeper
// in other words:
Console.WriteLine($"p2 v3: {numbers.Zip(numbers.Skip(3), (current, next) => next - current).Count(e => e > 0)}");

