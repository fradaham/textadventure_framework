using Spectre.Console;
using TextAdventure.Concepts;
using TextAdventure.GameData;
using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure;

public class Program
{
    public static void Main(string[] arg)
    {
        GameEngine engine = new(new GameSetup());
        engine.Run();
        
        // IRoom startingRoom = GameSetup.World.GetRoom("Källare")!;
        // Player player = new Player("Torleif", 25, 25, 15, 15, 3, startingRoom);
        // player.Inventory = GameSetup.InitialPlayerInventory.ToList();
        // IInterpreter interpreter = new SwedishInterpreter();
        // GameEngine engine = new(GameSetup.World, player, interpreter);
        // AnsiConsole.Clear();
        // FigletText title = new("BLAJAN");
        // title.Color(Color.SandyBrown);
        // title.Justification = Justify.Center;
        // Text subTitle = new Text("Ett brunt retroäventyr producerat helt utan fördummande AI", new Style(Color.RosyBrown))
        // {
        //     Justification = Justify.Center
        // };
        // AnsiConsole.Write(title);
        // AnsiConsole.Write(subTitle);
        // string dummy = AnsiConsole.Prompt(new TextPrompt<string>("Tryck enter för att börja"));
        // FinishCode finishCode = engine.Run(); 
        // if (finishCode == FinishCode.Fail)
        // {
        //     AnsiConsole.MarkupLine("[bold red]Du dog![/]");
        // }
        // else if (finishCode == FinishCode.Success)
        // {
        //     AnsiConsole.MarkupLine("[bold green]Du klarade spelet! Grattis![/]");
        // }

        // if (finishCode != FinishCode.Quit)
        // {
        //     dummy = AnsiConsole.Prompt(new TextPrompt<string>("Tryck enter för att avsluta"));
        // }
        // AnsiConsole.Clear();
        // AnsiConsole.MarkupLine("[bold yellow]AI-Pan senior härskar! Bränn-bränn-bränn![/]");
    }
}
