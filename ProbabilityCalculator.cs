namespace task3_DiceGame;

public static class ProbabilityCalculator
{
    public static double CalculateWinProbability(Dice userDice, Dice opponentDice)
    {
        var total = userDice.Faces.Count * opponentDice.Faces.Count;
        var wins = (from u in userDice.Faces from o in opponentDice.Faces where u > o select u).Count();
        return (double)wins / total;
    }
}