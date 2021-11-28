using System.Linq;

// trying out expression lambda
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
// ^ is logical XOR
var IsValidP2 = (PasswordEntry pe) => (pe.password[pe.a - 1] == pe.ch) ^ (pe.password[pe.b - 1] == pe.ch);

var passwords = System.IO.File.ReadLines(@"input.txt").Select(ParseLine).ToList();

Console.WriteLine($"part 1 valid passwords: {passwords.Count(IsValidP1)}");
Console.WriteLine($"part 2 valid passwords: {passwords.Count(IsValidP2)}");

PasswordEntry ParseLine(string line) {
    // body taken from encse's solution
    var parts = line.Split(' ');
    // NOTE: split string, transform components, return array
    var range = parts[0].Split('-').Select(int.Parse).ToArray();
    var ch = parts[1][0];
    return new PasswordEntry(range[0], range[1], ch, parts[2]);    
}

Boolean IsValidP1(PasswordEntry pe) {
    // body taken from encse's solutions
    var count = pe.password.Count(ch => ch == pe.ch);
    return pe.a <= count && count <= pe.b;    
}


/// <summary>stuff in here including a b ch and password</summary>
record PasswordEntry(int a, int b, char ch, string password);

