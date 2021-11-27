using System.Text.RegularExpressions;
using aoclib;

// split on double \n to split on blank line between passports
var passports = Bleh.GetTrimmedInput(@"input.txt").Split("\n\n");

string[] requiredFields = {
    "byr",
    "iyr",
    "eyr",
    "hgt",
    "hcl",
    "ecl",
    "pid"
};

var requiredFieldsHashSet = requiredFields.ToHashSet();

var validPassports = 0;
foreach (var passport in passports)
{
    // Split() without arg splits on all whitespace, which is what we need for multi-line passports
    // https://stackoverflow.com/a/6111355/532513
    var fields = passport.Split().Select(elem => elem.Split(":")[0]);
    // this works on lists or hashsets, or even a mix of them.
    // https://stackoverflow.com/a/3274816/532513
    if (requiredFieldsHashSet.Intersect(fields).Count() == requiredFields.Count()) validPassports++;
}

Console.WriteLine($"Part 1 valid passports: {validPassports}");

var hclre = new Regex(@"#[a-f0-9]{6}");
string[] validEcl = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

validPassports = 0;
foreach (var passport in passports)
{
    // we do the part 1 check as well
    var fields = passport.Split().Select(elem => elem.Split(":")[0]);
    if (requiredFieldsHashSet.Intersect(fields).Count() == requiredFields.Count())
    {
        if (passport.Split().Select(elem => elem.Split(":")).All(CheckValid)) validPassports++;

    }
}

Console.WriteLine($"Part 2 valid passports: {validPassports}");

bool HandleHgt(string val) {
    var num = int.Parse(val.Substring(0, val.Length-2));
    return num switch {
        >= 150 and <= 193 when val.EndsWith("cm") => true,
        >= 59 and <= 76 when val.EndsWith("in") => true,
        _ => false
    };
}

bool CheckValid(string[] fv)
{
    var field = fv[0];
    var val = fv[1];

    // https://timdeschryver.dev/blog/pattern-matching-examples-in-csharp#relational-patterns
    return field switch
    {
        // "is" here is also pattern matching
        "byr" => int.Parse(val) is >= 1920 and <= 2002,
        "iyr" => int.Parse(val) is >= 2010 and <= 2020,
        "eyr" => int.Parse(val) is >= 2020 and <= 2030,
        "hgt" => HandleHgt(val),
        "hcl" => hclre.IsMatch(val),
        "ecl" => validEcl.Contains(val),
        // IsMatch finds a match *within the string*, so it can for example be true on 10 characters
        // hence the need for ^ and $ 
        "pid" => new Regex(@"^[0-9]{9}$").IsMatch(val),
        "cid" => true,
        _ => throw new ArgumentException("Invalid field name", fv[0]),
    };
}