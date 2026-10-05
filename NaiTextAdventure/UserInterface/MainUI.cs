using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;
using System.Collections.Immutable;
using Nai.TextAdventure.Sound;

namespace Nai.TextAdventure.UserInterface;
internal sealed class MainUI : ITextUserInterface, IPrintToUser
{
    private readonly Layout rootLayout;
    private readonly Layout storyLayout;
    private readonly Layout bodyLayout;
    private readonly Layout headerLayout;
    private readonly Layout inventoryLayout;

    private readonly Layout footerLayout;

    private int cursorPos = 0;

    public Layout Layout { get { return rootLayout; }}

    public void AppendToLog(string text)
    {
        textPanel.Append($"\n{text}\n");
    }

    private bool inventoryActive = false;
    private int invIndex = 0;

    private RollingTextPanel textPanel;

    private string promptInput = "";

    public string UserCommand { get; internal set;} = "";

    private readonly string? _gameMusicPath;

    private readonly MainGameEngine _gameEngine;

    public MainUI(MainGameEngine gameEngine, string? gameMusic)
    {
        _gameMusicPath = gameMusic;
        _gameEngine = gameEngine;
        rootLayout = new("Root");
        headerLayout = new("Header");
        headerLayout.Size = 3;
        bodyLayout = new("Body");
        storyLayout = new("Story");
        inventoryLayout = new("Inventory");
        inventoryLayout.Size = 18;
        footerLayout = new("Footer");
        footerLayout.Size = 3;
        bodyLayout.SplitColumns(storyLayout, inventoryLayout);
        rootLayout.SplitRows(headerLayout, bodyLayout, footerLayout);
        textPanel = new("", "[bold] LOGGBOK [/]", (AnsiConsole.Profile.Width - inventoryLayout.Size - 4).Value, (AnsiConsole.Profile.Height - headerLayout.Size - footerLayout.Size - 2).Value);
    }

    public void PrintMessage(string text)
    {
        AppendToLog(text);
    }

    public void Reset()
    {
        textPanel.Reset();
    }

    public TuiResult Execute(Context context, ActionResult? incomingActionResult)
    {
        TuiResult? exitState = null;
        AnsiConsole.Live(rootLayout).Start(ctx =>
        {
            if (incomingActionResult != null)
            {
                exitState = _gameEngine.ProcessActionResult(incomingActionResult, context.Player, this);
            }

            Update(context);
            ctx.Refresh();

            SoundPlayer? soundPlayer = null;
            if (_gameMusicPath != null)
            {
                soundPlayer = new(_gameMusicPath);
                soundPlayer.Play();
            }

            while (exitState == null)
            {
                ConsoleKeyInfo input = Console.ReadKey(intercept: true);
                if (input.Key == ConsoleKey.F12)
                {
                    exitState = new TuiResult(GameState.Quit);
                }
                else if(input.Key == ConsoleKey.F11)
                {
                    exitState = new TuiResult(GameState.Title);
                }
                else
                {
                    UserInput(input, context);
                }

                if (input.Key == ConsoleKey.Enter)
                {
                    string userInput = UserCommand;
                    exitState = _gameEngine.ProcessUserCommand(userInput, context.Player, this);
                }
                Update(context);
                ctx.Refresh();
            }

            if (exitState != null && (exitState.TargetView == GameState.Fight || exitState.TargetView == GameState.Completed || exitState.TargetView == GameState.Death))
            {
                if (exitState.TargetView == GameState.Fight)
                {
                    AppendToLog($"Tryck valfri tangent för för att börja bulta på {exitState.PassedOnActionResult?.Fight?.Name ?? "NoName"}.");
                }
                else if (exitState.TargetView == GameState.Completed || exitState.TargetView == GameState.Death)
                {
                   AppendToLog($"Tryck valfri tangent...");
                }
                Update(context);
                ctx.Refresh();
                _ = Console.ReadKey(intercept: true);
            }

            soundPlayer?.Stop();
        });

        return exitState!;
    }

    public void Update(Context context)
    {
        int consoleWidth = AnsiConsole.Profile.Width;
        int consoleHeight = AnsiConsole.Profile.Height;
        int storyHeight = (consoleHeight - headerLayout.Size! - footerLayout.Size! - 2).Value;
        int storyWidth = (consoleWidth - inventoryLayout.Size! - 4).Value;

        textPanel.Height = storyHeight;
        textPanel.Width = storyWidth;

        storyLayout.Update(textPanel.InnerPanel);

        BarChart healthBar = new BarChart()
            .WithMaxValue(context.Player.MaxHealth)
            .AddItem(   $"Hälsa ({context.Player.MaxHealth})"
                        ,context.Player.Health
                        ,(((double)context.Player.Health)/context.Player.MaxHealth) switch
                        {
                            > 0.75 => Color.Green,
                            > 0.25 and <= 0.75 => Color.Yellow,
                            _ => Color.Red
                        });


        BarChart spBar = new BarChart()
            .WithMaxValue(15)
            .AddItem($"Stridsförmåga ({context.Player.MaxFightingSkill})", context.Player.FightingSkill, Color.LightCyan3);

        Grid grid = new();
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Left, NoWrap = true });
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Center, NoWrap = true });
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Right, NoWrap = true });
        // grid.AddRow(healthBar, new Markup($"[yellow]Guldmynt:[/] {player.Gold}"), new Markup($"[blue]Plats:[/] Källare"));
        // grid.AddRow(spBar);
        grid.AddRow(new Markup($"Hälsa: {context.Player.Health}"), new Markup($"Stridsförmåga: {context.Player.FightingSkill}"), new Markup($"[blue]Plats:[/] Källare"));

        // Columns headerContent = new(healthBar, spBar, new Markup($"[red]Kroppspoäng:[/] {player.Health}/100   |   [yellow]Guldmynt:[/] {player.Gold}   |   [blue]Plats:[/] {player.Location.Name}}}"));
        // headerContent.Expand();

        //headerLayout.Update(new Panel(Align.Center(new Markup($"[red]Kroppspoäng:[/] {health}/100   |   [yellow]Guldmynt:[/] {gold}   |   [blue]Plats:[/] Källare")))
        headerLayout.Update(new Panel(grid)
        .BorderColor(Color.Red)
        .Header("[b] SPELARSTATUS (S) [/]"));


        // debugLayout.Update(new Panel(new Markup($"Lines: {storyMarkup.Lines} | Length: {storyMarkup.Length} | S_height: {storyHeight} | S_width: {storyWidth}  | allLines: {allLines.Count()}  | visLines: {visibleLines.Count()}  | currL: {currentLine}"))
        //     .Expand()
        //     .BorderColor(Color.Green).Header("[bold] DEBUG [/]"));

        string inventoryMarkup = string.Join("\n", context.Player.Inventory.Select((item, index) => $"{((inventoryActive && index == invIndex)? "[bold black on cyan]":"")}-{item.Name.Name}{((inventoryActive && index == invIndex)? "[/]" : "")}"));
        Markup invMarkup = new Markup(inventoryMarkup);
        invMarkup.Overflow = Overflow.Ellipsis;
        Panel invPanel = new(invMarkup)
        {
            Width = inventoryLayout.Size,
            Height = storyHeight + 2,
        };
        invPanel.BorderColor(Color.Blue);
        invPanel.Header("[b] PACKNING (P) [/]");
        inventoryLayout.Update(
            invPanel
        );

        string promptToShow = promptInput + " ";
        Markup promptMarkup = new Markup($"> {promptToShow[..cursorPos]}[invert]{promptToShow[cursorPos]}[/]{promptToShow[(cursorPos + 1)..]}");
        footerLayout.Update(
            new Panel(Align.Left(promptMarkup))
            .BorderColor(Color.Grey)
        );
    }

    public void UserInput(ConsoleKeyInfo input, Context context)
    {
        if (input.Key == ConsoleKey.Enter)
        {
            UserCommand = new string(promptInput);
            textPanel.Append($"\n> {promptInput}\n");
            promptInput = "";
            cursorPos = 0;
        }
        else if (input.Key == ConsoleKey.Backspace)
        {
            if (cursorPos > 0)
            {
                cursorPos--;
                promptInput = promptInput[0..cursorPos] + promptInput[(cursorPos + 1)..];//promptInput[0..(promptInput.Length - 1)];
            }
        }
        else if ("abcdefghijklmnopqrstuvxyzåäöABCDEFGHIJKLMNOPQRSTUVXYZÅÄÖ ".Contains(input.KeyChar))
        {
            promptInput = promptInput[0..cursorPos] + input.KeyChar + (cursorPos < promptInput.Length? promptInput[cursorPos..]: "");
            cursorPos++;
        }
        if (input.Key == ConsoleKey.F1)
        {
            inventoryActive = !inventoryActive;
            if (inventoryActive)
            {
                invIndex = 0;
            }
        }
        else if(input.Key == ConsoleKey.UpArrow)
        {
            if (inventoryActive)
            {
                invIndex--;
                if (invIndex < 0)
                {
                    invIndex = context.Player.Inventory.Count() - 1;
                }
            }
            else
            {
                textPanel.ScrollUp();
            }
        }
        else if(input.Key == ConsoleKey.DownArrow)
        {
            if (inventoryActive)
            {
                invIndex++;
                if (invIndex >= context.Player.Inventory.Count())
                {
                    invIndex = 0;
                }
            }
            else
            {
                textPanel.ScrollDown();
            }
        }
        else if(input.Key == ConsoleKey.RightArrow)
        {
            if (cursorPos < promptInput.Length)
            {
                cursorPos++;
            }
        }
        else if(input.Key == ConsoleKey.LeftArrow)
        {
            if (cursorPos > 0)
            {
                cursorPos--;
            }
        }
    }
}