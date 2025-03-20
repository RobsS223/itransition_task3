namespace task3_DiceGame;

class Program
{
    static void Main(string[] args)
    {
        var diceList = DiceParser.Parse(args);
        var game = new Game(diceList);
        game.Start();
    }
} 

// dotnet run 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6 1,2,3,4,5,6 - Valid Launch with Four Identical Dice
// dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7 - Valid Launch with Three Dice
// dotnet run - Incorrect Input – No Dice Provided
// dotnet run 1,2,3,4,5,6 7,8,9,10,11,12 - Incorrect Input – Only Two Dice Provided
// dotnet run 1,2,3,4,5 1,2,three,4,5,6 7,8,9,10,11,12 - Incorrect Input – Invalid Dice Configuration