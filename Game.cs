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
        Console.WriteLine("0 - 0");
        Console.WriteLine("1 - 1");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");
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
        var selection = FairProtocol.GetUserInput(
            "Choose your dice (index, ? for help): ",
            _diceList.Count,
            () => TablePrinter.PrintProbabilityTable(_diceList)
        );
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
        var selection = FairProtocol.GetUserInput(
            "Choose your dice (index, ? for help): ",
            _diceList.Count,
            () => TablePrinter.PrintProbabilityTable(_diceList)
        );
        _userDice = _diceList[selection];
        _diceList.RemoveAt(selection);
        Console.WriteLine($"You chose: {_userDice}");
        PlayDiceRolls();
    }

    private void PlayDiceRolls()
    {
        if (_computerDice is null)
            throw new Exception("Computer dice is not selected.");
        if (_userDice is null)
            throw new Exception("User dice is not selected.");
            
        Console.WriteLine("---- Computer's roll ----");
        var compResult = PerformRoll(_computerDice, false);
        Console.WriteLine("---- Your roll ----");
        var userResult = PerformRoll(_userDice, true);
        Console.WriteLine($"Computer roll result: {compResult}");
        Console.WriteLine($"Your roll result: {userResult}");
        if (userResult > compResult)
            Console.WriteLine($"You win ({userResult} > {compResult})!");
        else if (compResult > userResult)
            Console.WriteLine($"Computer wins ({compResult} > {userResult})!");
        else
            Console.WriteLine("It's a tie!");
    }

    private int PerformRoll(Dice dice, bool userRoll)
    {
        var range = dice.Faces.Count;
        var fairGen = new FairRandomGenerator();
        fairGen.Generate(range);
        Console.WriteLine($"I selected a random value in the range 0..{range - 1} (HMAC={fairGen.Hmac}).");
        if (userRoll)
        {
            Console.WriteLine("Add your number modulo " + range + ".");
            for (var i = 0; i < range; i++)
                Console.WriteLine($"{i} - {i}");
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            var userInput = FairProtocol.GetUserInput("Your selection: ", range, 
                () => Console.WriteLine($"Enter an integer between 0 and {range - 1}."));
            fairGen.Reveal();
            var result = (fairGen.Number + userInput) % range;
            Console.WriteLine($"The fair number generation result is {fairGen.Number} + {userInput} = {result} (mod {range}).");
            var faceValue = dice.Faces[result];
            Console.WriteLine($"Dice face value: {faceValue}");
            return faceValue;
        }
        else
        {
            fairGen.Reveal();
            var result = fairGen.Number % range;
            Console.WriteLine($"The fair number generation result is {fairGen.Number} (mod {range}) = {result}.");
            var faceValue = dice.Faces[result];
            Console.WriteLine($"Dice face value: {faceValue}");
            return faceValue;
        }
    }

    private static double GetAverage(Dice dice)
    {
        return dice.Faces.Average();
    }
}
