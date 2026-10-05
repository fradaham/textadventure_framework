using Spectre.Console;
using Nai.TextAdventure.Concepts;
using System.Reflection.Metadata.Ecma335;
using Nai.TextAdventure.Sound;

namespace Nai.TextAdventure.UserInterface;

public class AboutUI: ITextUserInterface
{
    private readonly Layout rootLayout;
    private readonly Layout logLayout;
    private readonly Layout headerLayout;
    private readonly Layout footerLayout;
    private readonly string? _freeText;
    private RollingTextPanel textPanel;
    public AboutUI(string? freeText)
    {
        _freeText = freeText;
        rootLayout = new("Root");
        headerLayout = new("Header");
        footerLayout = new("Footer");
        headerLayout.Size = 6;
        footerLayout.Size = 1;
        logLayout = new("Log");
        rootLayout.SplitRows(headerLayout, logLayout, footerLayout);
        textPanel = new(_freeText ?? "", "", AnsiConsole.Profile.Width - 2, (AnsiConsole.Profile.Height - headerLayout.Size).Value);
    }

    public TuiResult Execute(Context context, ActionResult? incomingActionResult = null)
    {
        AnsiConsole.Clear();
        TuiResult? exitState = null;
        AnsiConsole.Live(rootLayout).Start(ctx =>
        {
            Update();
            ctx.Refresh();
            while (exitState == null)
            {
                ConsoleKeyInfo input = Console.ReadKey(intercept: true);
                if (input.Key == ConsoleKey.D2)
                {
                    exitState = new TuiResult(GameState.Quit);
                }
                else if(input.Key == ConsoleKey.D1)
                {
                    exitState = new TuiResult(GameState.Title);
                }
                else if(input.Key == ConsoleKey.UpArrow)
                {
                    textPanel.ScrollUp();
                }
                else if(input.Key == ConsoleKey.DownArrow)
                {
                    textPanel.ScrollDown();
                }

                Update();
                ctx.Refresh();
            }
        });
        
        return exitState!;
    }

    public void Update()
    {
        int consoleWidth = AnsiConsole.Profile.Width;
        int consoleHeight = AnsiConsole.Profile.Height;
        int logHeight = (consoleHeight - headerLayout.Size! - footerLayout.Size! - 2).Value;
        int logWidth = consoleWidth - 2;

        textPanel.Height = logHeight;
        textPanel.Width = logWidth;
        
        logLayout.Update(textPanel.InnerPanel);

        FigletText aboutTitle = new("OM SPELET");
        aboutTitle.Color(Color.Yellow);
        aboutTitle.Justification = Justify.Center;

        headerLayout.Update(aboutTitle);
        
        Text instructions = new Text($"(1) - tillbaka till titelskärmen (2) - Avsluta (\u2191) - text uppåt (\u2193) - text nedåt", new Style(Color.RosyBrown));
        Align footerAligned = Align.Center(instructions);

        footerLayout.Update(footerAligned);
    }

    public void Reset()
    {}
            
}