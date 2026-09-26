using Spectre.Console;
using Nai.TextAdventure.Concepts;
using System.Reflection.Metadata.Ecma335;
using Nai.TextAdventure.Sound;

namespace Nai.TextAdventure.UserInterface;

public class DeathUI: ITextUserInterface
{
    private readonly string? _comment;
    private readonly string? _deathMusicPath;
    public DeathUI(string? comment, string? musicPath)
    {
        _comment = comment;
        _deathMusicPath = musicPath;
    }

    public TuiResult Execute(Context context)
    {
        SoundPlayer? soundPlayer = null;
        if (_deathMusicPath != null)
        {
            soundPlayer = new(_deathMusicPath);
            soundPlayer.Play();
        }
        AnsiConsole.Clear();
        FigletText deathText = new("DU DOG!");
        deathText.Color(Color.Red1);
        deathText.Justification = Justify.Left;
        
        Text instructions = new Text($"1 - starta om spelet\n2 - Avsluta", new Style(Color.RosyBrown));
        
        Align deathTextAligned = Align.Center(deathText, VerticalAlignment.Bottom).Height(AnsiConsole.Profile.Height/3).Width(AnsiConsole.Profile.Width);
        AnsiConsole.Write(deathTextAligned);

        Align? commentAligned = null;
        Align? decoration = null;
        if (_comment != null)
        {
            Text comment = new Text($"{_comment}", new Style(Color.Red3));
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
            ConsoleKey.D1 => new TuiResult(GameState.Main),
            ConsoleKey.D2 => new TuiResult(GameState.Quit),
            _ => throw new Exception($"Unsupported case '{userInput}' in death UI input")
        };
       
    }
            
}