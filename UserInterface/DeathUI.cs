using Spectre.Console;
using TextAdventure.Concepts;

namespace TextAdventure.UserInterface;

public class DeathUI: ITextUserInterface
{
    private readonly string? _comment;
    public DeathUI(string? comment)
    {
        _comment = comment;
    }

    public GameState Execute()
    {
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

        return userInput switch {
            ConsoleKey.D1 => GameState.Main,
            ConsoleKey.D2 => GameState.Quit,
            _ => throw new Exception($"Unsupported case '{userInput}' in death UI input")
        };
       
    }
            
}