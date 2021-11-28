using System.Linq;

// linq formulation 1:
//var numbers = (from line in System.IO.File.ReadLines(@"input.txt") select int.Parse(line)).ToHashSet<int>;

// linq formulation 2:
// convert to hashset because we're going to do a ton of Contains() checks
var numbers = System.IO.File.ReadLines(@"input.txt").Select(int.Parse).ToHashSet<int>();

// Part 1: 471019
// encse's solution, I've just ported to my dumb CLI-per-exercise setup
Console.WriteLine((from x in numbers let y = 2020 - x where numbers.Contains(y) select x * y).First());

// Part 2: 103927824
// encse's solution, ported to my dumb setup. His solution is not 100% watertight, because does not check for x==y
Console.WriteLine((
                from x in numbers
                from y in numbers
                let z = 2020 - x - y
                where numbers.Contains(z)
                select x * y * z
            ).First());

// foreach (var number in numbers2) {
//     Console.Write(number);
// }

