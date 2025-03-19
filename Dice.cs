namespace task3_DiceGame;

public class Dice(List<int> faces)
{
    public List<int> Faces { get; } = faces;
    public override string ToString() => string.Join(",", Faces); 
}