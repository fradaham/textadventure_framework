using Blajan.GameData;
using Nai.TextAdventure;

namespace Blajan;

public class Program
{
    public static void Main(string[] arg)
    {
        TextGameEngine textGameEngine = new TextGameEngine(new GameSetup());
        textGameEngine.Run();
    }
}
