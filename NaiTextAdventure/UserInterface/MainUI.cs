using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.UserInterface;
internal sealed class MainUserInterface : ITextUserInterface
{
    private readonly Layout rootLayout;
    private readonly Layout storyLayout;
    private readonly Layout bodyLayout;
    private readonly Layout headerLayout;
    private readonly Layout inventoryLayout;

    private readonly Layout footerLayout;

    public Layout Layout { get { return rootLayout; }}

    public void AppendToLog(string text)
    {
        textPanel.Append($"\n{text}\n");
    }

    public void AppendToLog(string text, string style)
    {
        textPanel.Append(text, style);
    }

    private readonly Player _player;

    private bool inventoryActive = false;
    private int invIndex = 0;

    private RollingTextPanel textPanel;

    private string promptInput = "";

    public string UserCommand { get; internal set;} = "";

    private IInterpreter _interpreter;

    private World _world;
    
    public MainUserInterface(World world, Player player, IInterpreter interpreter)
    {
        _player = player;
        _interpreter = interpreter;
        _world = world;
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
        textPanel = new(player.Room.ToString()!, "[bold] LOGGBOK [/]", (AnsiConsole.Profile.Width - inventoryLayout.Size - 4).Value, (AnsiConsole.Profile.Height - headerLayout.Size - footerLayout.Size - 2).Value);
    }

    public GameState Execute()
    {
        GameState? exitState = null;
        AnsiConsole.Live(rootLayout).Start(ctx =>
        {
            Update();
            ctx.Refresh();
            while (exitState == null)
            {
                ConsoleKeyInfo input = Console.ReadKey(intercept: true);
                if (input.Key == ConsoleKey.F12)
                {
                    exitState = GameState.Quit;
                }
                else if(input.Key == ConsoleKey.F11)
                {
                    exitState = GameState.Title;
                }
                else
                {
                    UserInput(input);
                }

                if (input.Key == ConsoleKey.Enter)
                {
                    string userInput = UserCommand;
                    ParsingResult parsingResult = _interpreter.Parse(userInput);
                    if (parsingResult.ErrorMessage is not null)
                    {
                       AppendToLog(parsingResult.ErrorMessage); 
                    }
                    else
                    {
                        PlayerAction action;
                        try
                        {
                            action = CreatePlayerAction(parsingResult);
                            ActionResult? result = _player.Room.InterAct(new Context(_world, _player), action);
                            AppendToLog(result?.Message?? "");
                            if (result?.MoveToRoomId != null)
                            {
                                IRoom? newRoom =_world.GetRoom(result.MoveToRoomId);
                                _player.Room = newRoom ?? throw new Exception($"Cannot find room with name '{result.MoveToRoomId}' in world. Check game setup.");
                                AppendToLog(_player.Room.ToString() ?? "");
                            }
                            else if (result?.StoryEvent != null) 
                            {
                                if (result.StoryEvent == StoryEvent.Success)
                                {
                                    exitState = GameState.Completed;
                                }
                                else if (result.StoryEvent == StoryEvent.Fail)
                                {
                                    exitState = GameState.Death;
                                }
                            }

                        }
                        catch(InputException e)
                        {
                            AppendToLog(e.Message);
                        }
                        catch(Exception e)
                        {
                            AppendToLog(Markup.Escape($"Technical error: {e}"));
                        }
                    }
                }
                Update();
                ctx.Refresh();
            }
        });
        
        return exitState!.Value;
    }

    private PlayerAction CreatePlayerAction(ParsingResult parsingResult)
    {
        if (parsingResult.Command == null)
        {
            throw new ArgumentException("ParsingResult.Command is null, cannot construct a PlayerAction from this object.");
        }

        Predicate predicate = parsingResult.Command.Predicate;
        string? directObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.DirectObject.ToString())?.Value;
        string? indirectObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.IndirectObject.ToString())?.Value;
        string? placeAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbial.ToString())?.Value;
        string? mannerAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbial.ToString())?.Value;
        string? placeAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbialInit.ToString())?.Value;
        string? mannerAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbialInit.ToString())?.Value;
        //throw new Exception($"{directObjectStr}, {indirectObjectStr}, {placeAdverbialStr}, {placeAdverbialInitStr}, {mannerAdverbialStr}, {mannerAdverbialInitStr}");
        IEntity? directObject, indirectObject, placeAdverbial, mannerAdverbial;
        directObject = indirectObject = placeAdverbial = mannerAdverbial = null;

        List<IEntity> entities = _player.GetAllEntities().ToList();
        List<string> notFound = new();
        if (directObjectStr != null)
        {
            directObject = entities.FirstOrDefault(d => d.IsMatch(directObjectStr));
            if (directObject == null)
            {
                notFound.Add(directObjectStr);
            }
        }
        if (indirectObjectStr != null)
        {
            indirectObject = entities.FirstOrDefault(d => d.IsMatch(indirectObjectStr));
            if (indirectObject == null)
            {
                notFound.Add(indirectObjectStr);
            }
        }
        if (mannerAdverbialStr != null)
        {
            mannerAdverbial = entities.FirstOrDefault(d => d.IsMatch(mannerAdverbialStr));
            if (mannerAdverbial == null)
            {
                notFound.Add(mannerAdverbialStr);
            }
        }
        if (placeAdverbialStr != null)
        {
            placeAdverbial = entities.FirstOrDefault(d => d.IsMatch(placeAdverbialStr));
            if (placeAdverbial == null)
            {
                notFound.Add(placeAdverbialStr);
            }
        }
        if (notFound.Count() > 0)
        {
            throw new InputException($"{String.Join(", ", notFound)} finns inte här.");
        }
        return new PlayerAction()
        {
            IndirectObject = indirectObject,
            Predicate = predicate,
            DirectObject = directObject,
            PlaceAdverbial = placeAdverbial,
            MannerAdverbial  = mannerAdverbial,
            MannerAdverbialInit = mannerAdverbialInitStr,
            PlaceAdverbialInit = placeAdverbialInitStr    
        };
    }

    public void Update()
    {
        int consoleWidth = AnsiConsole.Profile.Width;
        int consoleHeight = AnsiConsole.Profile.Height;
        int storyHeight = (consoleHeight - headerLayout.Size! - footerLayout.Size! - 2).Value;
        int storyWidth = (consoleWidth - inventoryLayout.Size! - 4).Value;

        textPanel.Height = storyHeight;
        textPanel.Width = storyWidth;
        
        storyLayout.Update(textPanel.InnerPanel);

        BarChart healthBar = new BarChart()
            .WithMaxValue(_player.MaxHealth)
            .AddItem(   $"Hälsa ({_player.MaxHealth})"
                        ,_player.Health
                        ,(((double)_player.Health)/_player.MaxHealth) switch 
                        {
                            > 0.75 => Color.Green,
                            > 0.25 and <= 0.75 => Color.Yellow,
                            _ => Color.Red
                        });
            

        BarChart spBar = new BarChart()
            .WithMaxValue(15)
            .AddItem($"Stridsförmåga ({_player.MaxFightingSkill})", _player.FightingSkill, Color.LightCyan3);

        Grid grid = new();
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Left, NoWrap = true });
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Center, NoWrap = true });
        grid.AddColumn(new GridColumn() { Width = (consoleWidth - 4)/3, Alignment = Justify.Right, NoWrap = true });
        // grid.AddRow(healthBar, new Markup($"[yellow]Guldmynt:[/] {player.Gold}"), new Markup($"[blue]Plats:[/] Källare"));
        // grid.AddRow(spBar);
        grid.AddRow(new Markup($"Hälsa: {_player.Health}"), new Markup($"Stridsförmåga: {_player.FightingSkill}"), new Markup($"[blue]Plats:[/] Källare"));

        // Columns headerContent = new(healthBar, spBar, new Markup($"[red]Kroppspoäng:[/] {player.Health}/100   |   [yellow]Guldmynt:[/] {player.Gold}   |   [blue]Plats:[/] {player.Location.Name}}}"));
        // headerContent.Expand();

        //headerLayout.Update(new Panel(Align.Center(new Markup($"[red]Kroppspoäng:[/] {health}/100   |   [yellow]Guldmynt:[/] {gold}   |   [blue]Plats:[/] Källare")))
        headerLayout.Update(new Panel(grid)
        .BorderColor(Color.Red)
        .Header("[b] SPELARSTATUS (S) [/]"));


        // debugLayout.Update(new Panel(new Markup($"Lines: {storyMarkup.Lines} | Length: {storyMarkup.Length} | S_height: {storyHeight} | S_width: {storyWidth}  | allLines: {allLines.Count()}  | visLines: {visibleLines.Count()}  | currL: {currentLine}"))
        //     .Expand()
        //     .BorderColor(Color.Green).Header("[bold] DEBUG [/]"));
            
        string inventoryMarkup = string.Join("\n", _player.Inventory.Select((item, index) => $"{((inventoryActive && index == invIndex)? "[bold black on cyan]":"")}-{item.Name.Name}{((inventoryActive && index == invIndex)? "[/]" : "")}"));
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

        footerLayout.Update(
            new Panel(Align.Left(new Markup($"> {promptInput}")))
            .BorderColor(Color.Grey)
        );
    }

    public void UserInput(ConsoleKeyInfo input)
    {
        if (input.Key == ConsoleKey.Enter)
        {
            UserCommand = new string(promptInput);
            textPanel.Append($"\n> {promptInput}\n"); 
            promptInput = "";           
        }
        else if ("abcdefghijklmnopqrstuvxyzåäöABCDEFGHIJKLMNOPQRSTUVXYZÅÄÖ ".Contains(input.KeyChar))
        {
            promptInput += input.KeyChar;
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
                    invIndex = _player.Inventory.Count() - 1;
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
                if (invIndex >= _player.Inventory.Count())
                {
                    invIndex = 0;
                }
            }
            else
            {
                textPanel.ScrollDown();
            }
        }
        else if(input.Key == ConsoleKey.D1)
        {
            textPanel.Text += "\n\n Blajan smakar chokladkräm, men luktar underligt likt bajs. Mums mums!";
        }    

    }
}