using Blajan.GameData;
using Nai.TextAdventure;

namespace Blajan;

public class Program
{
    public static void Main(string[] arg)
    {
        TextAdventureRunner runner = new(new GameSetup());
        runner.Run();
    }
}
