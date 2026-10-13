using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;
using Nai.TextAdventure.Sound;

namespace Nai.TextAdventure.UserInterface.Default;
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

    private readonly string? _battleMusicPath;
    private readonly GameLanguage _language;

    private RollingTextPanel textPanel;

    public BattleUserInterface(string? battleMusicPath, GameLanguage language)
    {
        _battleMusicPath = battleMusicPath;
        _language = language;
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
        textPanel = new("", $"[bold] {UiText.Logbook(_language)} [/]", AnsiConsole.Profile.Width - 2, (AnsiConsole.Profile.Height - headerLayout.Size - opponentsLayout.Size - 2).Value);
    }

    public TuiResult Execute(Context context, ActionResult? incomingActionResult)
    {
        SoundPlayer? soundPlayer = null;
        if (_battleMusicPath != null)
        {
            soundPlayer = new(_battleMusicPath);
            soundPlayer.Play();
        }

        Opponent opponent = incomingActionResult?.Fight ?? throw new Exception("FightUI is engaged without an opponent included in context.");
        Player player = context.Player ?? throw new Exception("FightUI is engaged without an player included in context.");;
        enemyHealth = opponent.Health;
        TuiResult? exitState = null;
        int round = 1;
        //AppendToLog("Korv");
        AnsiConsole.Clear();
        AnsiConsole.Live(rootLayout).Start(ctx =>
        {
            AppendToLog(UiText.PressAnyKeyToStart(_language));
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
                        AppendToLog(UiText.EnemyTookDamage(_language, damage));
                    }
                    else if (damage < 0)
                    {
                        player.Health += damage;
                        AppendToLog(UiText.PlayerTookDamage(_language, player.Name, -damage));
                    }
                    else
                    {
                        AppendToLog(UiText.EvenRound(_language));
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
                    AppendToLog(UiText.Defeat(_language));
                    AppendToLog(UiText.PressAnyKeyToContinue(_language));
                    exitState = new TuiResult(targetView, opponent.FailEvent);
                }
                else if (enemyHealth <= 0)
                {
                    GameState targetView = opponent.SuccessEvent?.GameResult switch
                    {
                        GameResult.Success => GameState.Completed,
                        GameResult.Fail => GameState.Death,
                        _ => GameState.Main
                    };
                    exitState = new TuiResult(targetView, opponent.SuccessEvent);
                    AppendToLog(UiText.Victory(_language));
                    AppendToLog(UiText.PressAnyKeyToContinue(_language));
                }
                else
                {
                   AppendToLog(UiText.PressAnyKeyForRound(_language, round)); 
                }

                Update(player, opponent);
                ctx.Refresh();
            }

            if (player.Health <= 0 || enemyHealth <= 0)
            {
                 _ = Console.ReadKey(intercept: true);
            }
        });
        
        soundPlayer?.Stop();

        return exitState!;
    }

    public void Reset()
    {
        textPanel.Reset();
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
            .AddItem(   $"{UiText.Health(_language)} ({player.MaxHealth})"
                        ,player.Health
                        ,(((double)player.Health)/player.MaxHealth) switch 
                        {
                            > 0.75 => Color.Green,
                            > 0.25 and <= 0.75 => Color.Yellow,
                            _ => Color.Red
                        });
            

        BarChart spBar = new BarChart()
            .WithMaxValue(15)
            .AddItem($"{UiText.FightingSkill(_language)} ({player.MaxFightingSkill})", player.FightingSkill, Color.LightCyan3);
        FigletText headerText = new(UiText.FightTitle(_language));
        headerText.Color(Color.Purple);
        headerText.Justification = Justify.Center;
        
        // Columns headerContent = new(healthBar, spBar, new Markup($"[red]Kroppspoäng:[/] {player.Health}/100   |   [yellow]Guldmynt:[/] {player.Gold}   |   [blue]Plats:[/] {player.Location.Name}}}"));
        // headerContent.Expand();
        //headerLayout.Update(new Panel(Align.Center(new Markup($"[red]Kroppspoäng:[/] {health}/100   |   [yellow]Guldmynt:[/] {gold}   |   [blue]Plats:[/] Källare")))
        headerLayout.Update(headerText);

        // debugLayout.Update(new Panel(new Markup($"Lines: {storyMarkup.Lines} | Length: {storyMarkup.Length} | S_height: {storyHeight} | S_width: {storyWidth}  | allLines: {allLines.Count()}  | visLines: {visibleLines.Count()}  | currL: {currentLine}"))
        //     .Expand()
        //     .BorderColor(Color.Green).Header("[bold] DEBUG [/]"));
        Text enemyStats = new Text($"{UiText.Enemy(_language)}: {opponent.Name}\n{UiText.FightingSkill(_language)}: {opponent.FightingSkill}\n{UiText.Health(_language)}: {enemyHealth}");    
        enemyLayout.Update(new Panel(enemyStats).BorderColor(Color.Red).Header(UiText.Enemy(_language)).Expand());
        Text playerStats = new Text($"{UiText.Player(_language)}: {player.Name}\n{UiText.FightingSkill(_language)}: {player.FightingSkill}\n{UiText.Health(_language)}: {player.Health}");    
        playerLayout.Update(new Panel(playerStats).BorderColor(Color.Green).Header(UiText.Player(_language)).Expand());
    }
}