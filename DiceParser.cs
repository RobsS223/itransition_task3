namespace task3_DiceGame;

public static class DiceParser
{
    public static List<Dice> Parse(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Error: At least 3 dice configurations required.");
            Console.WriteLine("Example: dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7");
            Environment.Exit(1);
        }
        var diceList = new List<Dice>();
        foreach (var arg in args)
        {
            var parts = arg.Split(',');
            if (parts.Length < 1 || !parts.All(p => int.TryParse(p, out _)))
            {
                Console.WriteLine($"Invalid dice configuration: {arg}");
                Console.WriteLine("Example: 2,2,4,4,9,9");
                Environment.Exit(1);
            }
            var faces = parts.Select(int.Parse).ToList();
            diceList.Add(new Dice(faces));
        }
        return diceList;
    }
}