namespace task3_DiceGame;

public static class FairProtocol
{
    public static int GetUserInput(string prompt, int range, Action? onHelp = null)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            if (input.Trim().Equals("X", StringComparison.CurrentCultureIgnoreCase))
                Environment.Exit(0);
            if (input.Trim().Equals("?", StringComparison.CurrentCultureIgnoreCase))
            {
                onHelp?.Invoke();
                continue;
            }
            if (int.TryParse(input, out var val) && val >= 0 && val < range)
                return val;
            Console.WriteLine("Invalid input. Try again.");
        }
    }
}