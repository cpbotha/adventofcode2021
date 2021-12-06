using aoclib;
var fish = Utils.GetTrimmedInput(@"test_input.txt").Split(",").Select(int.Parse).ToList();

for (var day = 1; day <= 256; day++)
{
    fish = MakeFish(fish);
    Console.WriteLine($"day {day} -- {fish.Count} fishies");// -- {string.Join(",", fish)}");
}



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
    //var outputFish = inputFish.Select(TFish);

    for (int i = 0; i < inputFish.Count; i++)
    {
        inputFish[i] = TFish(inputFish[i]);
    }

    //Console.WriteLine($" bleh {newFish}");
    return inputFish.Concat(Enumerable.Repeat(8, newFish)).ToList();

}