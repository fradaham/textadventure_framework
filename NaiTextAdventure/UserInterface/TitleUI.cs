using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Sound;

namespace Nai.TextAdventure.UserInterface;

public class TitleUI: ITextUserInterface
{
    private readonly Layout rootLayout;
    private readonly string _title;
    private readonly string _subTitle;

    private readonly string? _madeBy;
    private readonly GameLanguage _language;

    private readonly string? _titleMusic;
    public TitleUI(string title, string subTitle, string? madeBy, string? titleMusic, GameLanguage language)
    {
        rootLayout = new Layout("Root");
        _title = title;
        _subTitle = subTitle;
        _madeBy = madeBy;
        _titleMusic = titleMusic;
        _language = language;
    }

    public TuiResult Execute(Context context, ActionResult? incomingActionResult = null)
    {
        AnsiConsole.Clear();
        FigletText title = new(_title);
        title.Color(Color.SandyBrown);
        title.Justification = Justify.Left;
        Text subTitle = new Text(_subTitle, new Style(Color.RosyBrown))
        {
            Justification = Justify.Center
        };  
        
        Text instructions = new Text($"(1) - {UiText.StartGame(_language)}\n(2) - {UiText.AboutOption(_language)}\n(3) - {UiText.QuitOption(_language)}", new Style(Color.RosyBrown));

        Align? creatorAligned = null;
        if (_madeBy != null)
        {
            Text creator = new Text(UiText.PresentedBy(_language, _madeBy), new Style(Color.Black, Color.Blue));
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
        
        SoundPlayer? player = null;
        if (_titleMusic != null)
        {
            player = new(_titleMusic);
            player.Play();
        }

        ConsoleKey userInput;
        do
        {
            userInput = Console.ReadKey(intercept: true).Key;
        }
        while (userInput != ConsoleKey.D1 && userInput != ConsoleKey.D2 && userInput != ConsoleKey.D3);   
        
        player?.Stop();

        return userInput switch {
            ConsoleKey.D1 => new TuiResult(GameState.Main),
            ConsoleKey.D2 => new TuiResult(GameState.About),
            ConsoleKey.D3 => new TuiResult(GameState.Quit),
            _ => new TuiResult(GameState.Title)
        };
       
    }

    public void Reset()
    {}
            
}