using Spectre.Console;
using Nai.TextAdventure.Concepts;

namespace Nai.TextAdventure.UserInterface;

public class TitleUI: ITextUserInterface
{
    private readonly Layout rootLayout;
    private readonly string _title;
    private readonly string _subTitle;

    private readonly string? _madeBy;
    public TitleUI(string title, string subTitle, string? madeBy)
    {
        rootLayout = new Layout("Root");
        _title = title;
        _subTitle = subTitle;
        _madeBy = madeBy;
    }

    public GameState Execute()
    {
        AnsiConsole.Clear();
        FigletText title = new(_title);
        title.Color(Color.SandyBrown);
        title.Justification = Justify.Left;
        Text subTitle = new Text(_subTitle, new Style(Color.RosyBrown))
        {
            Justification = Justify.Center
        };  
        
        Text instructions = new Text($"1 - starta spelet\n2 - Avsluta\n3 - Om", new Style(Color.RosyBrown));

        Align? creatorAligned = null;
        if (_madeBy != null)
        {
            Text creator = new Text($"{_madeBy} presenterar", new Style(Color.Black, Color.Blue));
            creatorAligned = Align.Center(creator, VerticalAlignment.Top).Height(3);
            AnsiConsole.Write(creatorAligned);
            AnsiConsole.Write(Align.Center(new Text("--:-=<|>=-:--", new Style(Color.Aqua))));
        }

        Align mainTitleAligned = Align.Center(title, VerticalAlignment.Bottom).Height(AnsiConsole.Profile.Height/3).Width(AnsiConsole.Profile.Width);
        Align subTitleAligned = Align.Center(subTitle).Height(1);
        Align instructionsAligned = Align.Center(instructions, VerticalAlignment.Bottom).Height(AnsiConsole.Profile.Height - mainTitleAligned.Height - subTitleAligned.Height - 1 - (creatorAligned != null? creatorAligned.Height + 1 : 0));

        AnsiConsole.Write(mainTitleAligned);
        AnsiConsole.Write(subTitle);
        AnsiConsole.Write(instructionsAligned);

        ConsoleKey userInput = Console.ReadKey(intercept: true).Key;

        return userInput switch {
            ConsoleKey.D1 => GameState.Main,
            ConsoleKey.D2 => GameState.Quit,
            _ => GameState.Title
        };
       
    }
            
}