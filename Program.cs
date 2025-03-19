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
