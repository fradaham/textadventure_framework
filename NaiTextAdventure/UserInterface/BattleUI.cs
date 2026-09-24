using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.UserInterface;
internal sealed class BattleUserInterface : ITextUserInterface
{
    private readonly Layout rootLayout;
    private readonly Layout logLayout;
    private readonly Layout opponentsLayout;
    private readonly Layout playerLayout;
    private readonly Layout headerLayout;
    private readonly Layout enemyLayout;

    public void AppendToLog(string text)
    {
        textPanel.Append($"\n{text}\n");
    }

    private int enemyHealth;

    private RollingTextPanel textPanel;

    public BattleUserInterface()
    {
        rootLayout = new("Root");
        headerLayout = new("Header");
        headerLayout.Size = 6;
        opponentsLayout = new("Opponents");
        logLayout = new("Log");
        //storyLayout = new("Story");
        enemyLayout = new("enemy");
        playerLayout = new("player");
        opponentsLayout.Size = 5;
        opponentsLayout.SplitColumns(playerLayout, enemyLayout);
        rootLayout.SplitRows(headerLayout, opponentsLayout, logLayout);
        textPanel = new("", "[bold] STRIDSLOGG [/]", AnsiConsole.Profile.Width - 2, (AnsiConsole.Profile.Height - headerLayout.Size - opponentsLayout.Size - 2).Value);
    }

    public TuiResult Execute(Context context)
    {
        Opponent opponent = context.ActionResult?.Fight ?? throw new Exception("FightUI is engaged without an opponent included in context.");
        Player player = context.Player ?? throw new Exception("FightUI is engaged without an player included in context.");;
        enemyHealth = opponent.Health;
        TuiResult? exitState = null;
        int round = 1;
        //AppendToLog("Korv");
        AnsiConsole.Clear();
        AnsiConsole.Live(rootLayout).Start(ctx =>
        {
            AppendToLog($"Tryck på valfri tangent för att börja");
            Update(player, opponent);
            ctx.Refresh();
            while (exitState == null && player.Health > 0 && enemyHealth > 0)
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
                else if(input.Key == ConsoleKey.UpArrow)
                {
                    textPanel.ScrollUp();
                }
                else if(input.Key == ConsoleKey.DownArrow)
                {
                    textPanel.ScrollDown();
                }
                else
                {
                    Random randomGen = new Random();
                    int damage = (int)(randomGen.NextDouble()*(player.FightingSkill - opponent.FightingSkill) + randomGen.Next(-3,+3));
                    if (damage > 0)
                    {
                        enemyHealth -= damage;
                        AppendToLog($"Fienden tog {damage} i skada. Hähä!");
                    }
                    else if (damage < 0)
                    {
                        player.Health += damage;
                        AppendToLog($"{player.Name} tog {-damage} i skada. Urk!");
                    }
                    else
                    {
                        AppendToLog($"Jämnstark rond. Ingen tog skada.");
                    }
                    round++;
                    
                }

                if (player.Health <= 0)
                {
                    GameState targetView = opponent.FailEvent?.GameResult switch
                    {
                        GameResult.Success => GameState.Completed,
                        GameResult.Fail => GameState.Death,
                        _ => GameState.Main
                    };
                    AppendToLog("Nederlag!");
                    AppendToLog("Tryck valfri tangent för att fortsätta.");
                    exitState = new TuiResult(targetView, new Context(context.World, player, opponent.FailEvent));
                }
                else if (enemyHealth <= 0)
                {
                    GameState targetView = opponent.SuccessEvent?.GameResult switch
                    {
                        GameResult.Success => GameState.Completed,
                        GameResult.Fail => GameState.Death,
                        _ => GameState.Main
                    };
                    exitState = new TuiResult(targetView, new Context(context.World, player, opponent.SuccessEvent));
                    AppendToLog("Seger!");
                    AppendToLog("Tryck valfri tangent för att fortsätta.");
                }
                else
                {
                   AppendToLog($"Tryck på valfri tangent för rond {round}."); 
                }

                Update(player, opponent);
                ctx.Refresh();
            }

            if (player.Health <= 0 || enemyHealth <= 0)
            {
                 _ = Console.ReadKey(intercept: true);
            }
        });
        
        return exitState!;
    }

    public void Update(Player player, Opponent opponent)
    {
        int consoleWidth = AnsiConsole.Profile.Width;
        int consoleHeight = AnsiConsole.Profile.Height;
        int logHeight = (consoleHeight - headerLayout.Size! - opponentsLayout.Size! - 2).Value;
        int logWidth = consoleWidth - 2;

        textPanel.Height = logHeight;
        textPanel.Width = logWidth;
        
        logLayout.Update(textPanel.InnerPanel);

        BarChart healthBar = new BarChart()
            .WithMaxValue(player.MaxHealth)
            .AddItem(   $"Hälsa ({player.MaxHealth})"
                        ,player.Health
                        ,(((double)player.Health)/player.MaxHealth) switch 
                        {
                            > 0.75 => Color.Green,
                            > 0.25 and <= 0.75 => Color.Yellow,
                            _ => Color.Red
                        });
            

        BarChart spBar = new BarChart()
            .WithMaxValue(15)
            .AddItem($"Stridsförmåga ({player.MaxFightingSkill})", player.FightingSkill, Color.LightCyan3);
        FigletText headerText = new("STRID!");
        headerText.Color(Color.Purple);
        headerText.Justification = Justify.Center;
        
        // Columns headerContent = new(healthBar, spBar, new Markup($"[red]Kroppspoäng:[/] {player.Health}/100   |   [yellow]Guldmynt:[/] {player.Gold}   |   [blue]Plats:[/] {player.Location.Name}}}"));
        // headerContent.Expand();
        //headerLayout.Update(new Panel(Align.Center(new Markup($"[red]Kroppspoäng:[/] {health}/100   |   [yellow]Guldmynt:[/] {gold}   |   [blue]Plats:[/] Källare")))
        headerLayout.Update(headerText);

        // debugLayout.Update(new Panel(new Markup($"Lines: {storyMarkup.Lines} | Length: {storyMarkup.Length} | S_height: {storyHeight} | S_width: {storyWidth}  | allLines: {allLines.Count()}  | visLines: {visibleLines.Count()}  | currL: {currentLine}"))
        //     .Expand()
        //     .BorderColor(Color.Green).Header("[bold] DEBUG [/]"));
        Text enemyStats = new Text($"Fiende: {opponent.Name}\nStridspoäng: {opponent.FightingSkill}\nHälsa: {enemyHealth}");    
        enemyLayout.Update(new Panel(enemyStats).BorderColor(Color.Red).Header("Fiende").Expand());
        Text playerStats = new Text($"Spelare: {player.Name}\nStridspoäng: {player.FightingSkill}\nHälsa: {player.Health}");    
        playerLayout.Update(new Panel(playerStats).BorderColor(Color.Green).Header("Spelare").Expand());
    }
}