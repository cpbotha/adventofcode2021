using aoclib;

var numbers = Utils.GetTrimmedInput(@"input.txt").Split(",").Select(int.Parse).ToList();

Console.WriteLine(numbers.Sum() / numbers.Count);
numbers.Sort();

// doing part 1 late; in spite of avoiding the usual channels today, I heard
// something about means and medians so taking a chance:
// numbers.Count is even (1000), so median is just Count / 2
var median = numbers[numbers.Count / 2];

var fuel = numbers.Select(num => Math.Abs(num - median)).Sum();
Console.WriteLine($"part 1 fuel required: {fuel}");

// it sounds like fuel required is the nth triangle number
// https://math.stackexchange.com/a/593323
// e.g. 16 to 5 == 11 steps, but the 11th triangle number is 1+2+3..+11
// n(n+1)/2 = 11*12/2 = 66 just like in the example

// let's assume for part two that the sweet spot is also going to be greater than the median,
// so we build this giant linq to:  

// count up from median to max
// for each candidate sweetspot "cand"
var bleh = Enumerable.Range(median, numbers.Max()).Select(cand => new
{
    // calculate the total fuel for this candidate using the nth triangle number thingy
    cand, fuel = numbers.Select(num =>
    {
        var d = Math.Abs(num - cand);
        return d * (d + 1) / 2;
    }).Sum()
}).MinBy(thing => thing.fuel); // and finally just pick the candidate position with the lowest fuel

Console.WriteLine(bleh);