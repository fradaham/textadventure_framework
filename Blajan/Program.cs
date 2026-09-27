using Blajan.GameData;
using Nai.TextAdventure;

namespace Blajan;

public class Program
{
    public static void Main(string[] arg)
    {
        MainGameEngine naiTextGameEngine = new MainGameEngine(new GameSetup());
        naiTextGameEngine.Run();
    }
}
