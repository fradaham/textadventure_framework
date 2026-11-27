using Nai.TextAdventure;
using OldMine.GameData;

namespace OldMine;

public class Program
{
    public static void Main(string[] args)
    {
        TextGameEngine textGameEngine = new TextGameEngine(new GameSetup());
        textGameEngine.Run();
    }
}
