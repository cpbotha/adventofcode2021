using aoclib;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();

var fish = Utils.GetTrimmedInput(@"input.txt").Split(",").Select(int.Parse).ToList();

// the trick we employ here is to work on the histogram domain
var fishDict = fish.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());

// set to 80 for part 1, 256 for part 2
var lastDay = 256;
for (var day = 1; day <= lastDay; day++)
{
    // naive part 1
    //fish = MakeFish(fish);

    var outDict = new Dictionary<int, long>();

    // make some new fishies
    if (fishDict.GetValueOrDefault(0) > 0)
    {
        // each of these fish will produce a new fish
        outDict[8] = fishDict[0];
        // all of the 0 fishies will now move to 6
        outDict[6] = fishDict[0];
    }

    // age the rest
    for (int fishNum = 1; fishNum <= 8; fishNum++)
    {
        if (fishDict.GetValueOrDefault(fishNum) > 0)
        {
            // move to fishNum - 1
            //if (!outDict.ContainsKey(fishNum - 1)) outDict[fishNum - 1] = 0;
            // TryAdd effectively does the same as the commented-out check and set above
            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.tryadd?view=net-5.0
            outDict.TryAdd(fishNum - 1, 0);
            outDict[fishNum - 1] += fishDict[fishNum];
        }
    }

    fishDict = outDict;
    
    //Console.WriteLine($"day {day} -- {fishDict.Values.Sum()} fishies");// -- {string.Join(",", fish)}");
}

watch.Stop();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"day {lastDay} -- {fishDict.Values.Sum()} fishies");// -- {string.Join(",", fish)}");

List<int> MakeFish(List<int> inputFish)
{
    int newFish = 0;

    int TFish(int f)
    {
        if (f == 0)
        {
            newFish += 1;
            //Console.WriteLine(newFish);
            return 6;
        }

        return --f;
    }

    // ARGH why did this never manage to modify the closed newFish?
    // the TFish inside the select would change its version of the newFish var, but not the outside one
    // could be https://stackoverflow.com/questions/11287070/c-sharp-select-query-not-modifying-external-variable
    //var outputFish = inputFish.Select(TFish);

    for (int i = 0; i < inputFish.Count; i++)
    {
        inputFish[i] = TFish(inputFish[i]);
    }

    //Console.WriteLine($" bleh {newFish}");
    return inputFish.Concat(Enumerable.Repeat(8, newFish)).ToList();

}