using ConsoleTables;

namespace task3_DiceGame;

public static class TablePrinter
{
    public static void PrintProbabilityTable(List<Dice> diceList)
    {
        Console.WriteLine("Probability of the win for the user:");
        var headers = new List<string> { "User dice v" };
        headers.AddRange(diceList.Select(d => d.ToString()));
        var table = new ConsoleTable(headers.ToArray());

        for (var i = 0; i < diceList.Count; i++)
        {
            var row = new List<string> { diceList[i].ToString() };
            row.AddRange(diceList.Select((t, j) =>
                i == j ? $"- ({ProbabilityCalculator.CalculateWinProbability(diceList[i], t):F4})"
                    : ProbabilityCalculator.CalculateWinProbability(diceList[i], t).ToString("F4")));
            table.AddRow(row.Cast<object>().ToArray());
        }
        table.Write();
    }
}