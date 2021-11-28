// https://adventofcode.com/2020/day/6
// multiline group answers: part 1 - anyone answered; part 2 - group consensus

using aoclib;

// split on double \n to split on blank line between passports
var groups = Utils.GetTrimmedInput(@"input.txt").Split("\n\n");

// split->join to get rid of \n, then get unique characters for every group, then add them up
var sumCounts = groups.Select(g => String.Join(null, g.Split()).ToHashSet().Count).Sum();

Console.WriteLine($"Part 1 answer sum of per-group unique answer counts: {sumCounts}");

// version 1 of part 2 answer:
int totalGroupConsensusAnswers = 0;
foreach (var group in groups)
{
    var numPeople = group.Split().Length;
    // for each group, count the number of characters (questions) with count == num_newlines (answered by all group members)
    // (x.Key, x.Count)
    totalGroupConsensusAnswers += group.GroupBy(x => x).Where(x => x.Key != '\n' && x.Count() == numPeople).Count();
}

// version 2: yes, you can stuff it all into a LINQ query
var totalGroupConsensusAnswers2 = groups.Select(
    // project the count of consensus answers of the group
    group => group.GroupBy(x => x).Where(x => x.Key != '\n' && x.Count() == group.Split().Length).Count()
).Sum(); // add 'em all up

Console.WriteLine($"Part 2 answer sum of all questions answered by group consensus: {totalGroupConsensusAnswers2}");
