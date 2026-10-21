using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Sound;
using Nai.TextAdventure.Language.General;

namespace Nai.TextAdventure.UserInterface.Default;

public class DeathUI: ITextUserInterface
{
    private readonly string? _comment;
    private readonly string? _deathMusicPath;
    private readonly GameLanguage _language;
    public DeathUI(string? comment, string? musicPath, GameLanguage language)
    {
        _comment = comment;
        _deathMusicPath = musicPath;
        _language = language;
    }

    public TuiResult Execute(Context context, ActionResult? incomingActionResult = null)
    {
        SoundPlayer? soundPlayer = null;
        if (_deathMusicPath != null)
        {
            soundPlayer = new(_deathMusicPath);
            soundPlayer.Play();
        }
        AnsiConsole.Clear();
        FigletText deathText = new(UserText.YouDied(_language));
        deathText.Color(Color.Red1);
        deathText.Justification = Justify.Left;
        
        Text instructions = new Text($"1 - {UserText.RestartGame(_language)}\n2 - {UserText.QuitOption(_language)}", new Style(Color.RosyBrown));
        
        Align deathTextAligned = Align.Center(deathText, VerticalAlignment.Bottom).Height(AnsiConsole.Profile.Height/3).Width(AnsiConsole.Profile.Width);
        AnsiConsole.Write(deathTextAligned);

        Align? commentAligned = null;
        Align? decoration = null;
        if (_comment != null)
        {
            Text comment = new Text($"{incomingActionResult?.Message ??_comment }", new Style(Color.Red3));
            commentAligned = Align.Center(comment).Height(1);
            decoration = Align.Center(new Text("--<|>--", new Style(Color.Aqua)), VerticalAlignment.Top).Height(2);
            AnsiConsole.Write(decoration);
            AnsiConsole.Write(commentAligned);
        }

        Align instructionsAligned = Align.Center(instructions, VerticalAlignment.Bottom).Height(AnsiConsole.Profile.Height - deathTextAligned.Height - 1 - (commentAligned != null? commentAligned.Height + 1 : 0) - (decoration != null? decoration.Height + 1 : 0));
        AnsiConsole.Write(instructionsAligned);

        ConsoleKey userInput;
        do
        {
            userInput = Console.ReadKey(intercept: true).Key;
        }
        while (userInput != ConsoleKey.D1 && userInput != ConsoleKey.D2);   

        soundPlayer?.Stop();

        return userInput switch {
            ConsoleKey.D1 => new TuiResult(GameState.Title),
            ConsoleKey.D2 => new TuiResult(GameState.Quit),
            _ => throw new Exception($"Unsupported case '{userInput}' in death UI input")
        };
       
    }

    public void Reset()
    {}
            
}