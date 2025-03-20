namespace task3_DiceGame;

public class Game(List<Dice> diceList)
{
    private List<Dice> _diceList = diceList;
    private Dice? _userDice;
    private Dice? _computerDice;

    public void Start()
    {
        Console.WriteLine("Let's determine who makes the first move.");
        var fairGen = new FairRandomGenerator();
        fairGen.Generate(2);
        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={fairGen.Hmac}).");
        Console.WriteLine("Try to guess my selection.");
        Console.WriteLine("0 - 0\n1 - 1\nX - exit\n? - help");
        var userChoice = FairProtocol.GetUserInput("Your selection: ", 2);
        fairGen.Reveal();
        var firstResult = (fairGen.Number + userChoice) % 2;
        if (firstResult == 0)
        {
            Console.WriteLine("You make the first move.");
            UserFirstMove();
        }
        else
        {
            Console.WriteLine("Computer makes the first move.");
            ComputerFirstMove();
        }
    }

    private void UserFirstMove()
    {
        Console.WriteLine("Available dice:");
        for (var i = 0; i < _diceList.Count; i++)
            Console.WriteLine($"{i} - {_diceList[i]}");
        var selection = FairProtocol.GetUserInput("Choose your dice (index, ? for help): ", _diceList.Count, 
            () => TablePrinter.PrintProbabilityTable(_diceList));
        _userDice = _diceList[selection];
        _diceList.RemoveAt(selection);

        var bestIdx = 0;
        var bestAvg = -1.0;
        for (var i = 0; i < _diceList.Count; i++)
        {
            var avg = GetAverage(_diceList[i]);
            if (avg > bestAvg)
            {
                bestAvg = avg;
                bestIdx = i;
            }
        }
        _computerDice = _diceList[bestIdx];
        _diceList.RemoveAt(bestIdx);

        Console.WriteLine($"You chose: {_userDice}");
        Console.WriteLine($"Computer chose: {_computerDice}");
        PlayDiceRolls();
    }

    private void ComputerFirstMove()
    {
        var bestIdx = 0;
        var bestAvg = -1.0;
        for (var i = 0; i < _diceList.Count; i++)
        {
            var avg = GetAverage(_diceList[i]);
            if (avg > bestAvg)
            {
                bestAvg = avg;
                bestIdx = i;
            }
        }
        _computerDice = _diceList[bestIdx];
        _diceList.RemoveAt(bestIdx);
        Console.WriteLine($"Computer chose: {_computerDice}");
        Console.WriteLine("Available dice:");
        for (var i = 0; i < _diceList.Count; i++)
            Console.WriteLine($"{i} - {_diceList[i]}");
        var selection = FairProtocol.GetUserInput("Choose your dice (index, ? for help): ", _diceList.Count, 
            () => TablePrinter.PrintProbabilityTable(_diceList));
        _userDice = _diceList[selection];
        _diceList.RemoveAt(selection);
        Console.WriteLine($"You chose: {_userDice}");
        PlayDiceRolls();
    }

    private void PlayDiceRolls()
    {
        Console.WriteLine("---- Computer's roll ----");
        var computerRoll = PerformFairRoll(_computerDice);

        Console.WriteLine("---- Your roll ----");
        var userRoll = PerformFairRoll(_userDice);

        Console.WriteLine($"Computer roll result: {computerRoll}");
        Console.WriteLine($"Your roll result: {userRoll}");

        if (userRoll > computerRoll)
            Console.WriteLine($"You win! ({userRoll} > {computerRoll})");
        else if (computerRoll > userRoll)
            Console.WriteLine($"Computer wins! ({computerRoll} > {userRoll})");
        else
            Console.WriteLine("It's a tie!");
    }

    private int PerformFairRoll(Dice? dice)
    {
        if (dice is null)
            throw new Exception("Dice was not selected.");

        var facesCount = dice.Faces.Count;
        var fairGen = new FairRandomGenerator();
        fairGen.Generate(facesCount);
        
        Console.WriteLine($"I selected a random value in the range 0..{facesCount - 1} (HMAC={fairGen.Hmac}).");
        Console.WriteLine("Please, enter your number to ensure fairness:");
        for (var i = 0; i < facesCount; i++)
            Console.WriteLine($"{i} - {i}");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");

        var userInput = FairProtocol.GetUserInput("Your selection: ", facesCount,
            () => Console.WriteLine($"Enter number 0 to {facesCount - 1}"));

        fairGen.Reveal();

        var finalResult = (fairGen.Number + userInput) % facesCount;
        Console.WriteLine($"The fair number generation result is {fairGen.Number} + {userInput} = {finalResult} (mod {facesCount}).");
        
        var faceValue = dice.Faces[finalResult];
        Console.WriteLine($"Dice face value: {faceValue}");
        return faceValue;
    }

    private double GetAverage(Dice dice) => dice.Faces.Average();
}
