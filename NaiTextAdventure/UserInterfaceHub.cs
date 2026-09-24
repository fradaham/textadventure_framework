using Spectre.Console;
using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.UserInterface;

namespace Nai.TextAdventure;

public class UserInterfaceHub(IGameSetup setup)
{
    private World _world = setup.World;

    private Player? _player;

    private IInterpreter _interpreter = setup.MainInterpreter;

    public void Run()
    {
        TuiResult tuiResult = new TuiResult(GameState.Title);
        TitleUI titleView = new TitleUI(setup.Title, setup.SubTitle, setup.Creator);
        ITextUserInterface? mainView = null;
        Player dummyPlayer = new Player("DUMMY", 25, 25, 15, 15, 3, setup.World.GetRoom(setup.StartingRoomName)!); //TODO: Think through messy context making this unnecessary
        
        while(true)
        {
            if (tuiResult.TargetView == GameState.Title)
            {
                tuiResult = titleView.Execute(new Context(setup.World, dummyPlayer));
                if (tuiResult.TargetView == GameState.Main)
                {
                    _world = setup.World; //Important to get a new each time a new game is started (important that the GameSetup impl is implementing a get method tha provides a new world object each time)
                    IRoom startingRoom = _world.GetRoom(setup.StartingRoomName)!;
                    _player = new Player("Torleif", 25, 25, 15, 15, 3, startingRoom); //TODO: An input UI for this
                    //TODO: Think through messy context making this ugly thing unnecessary
                    tuiResult = new TuiResult(GameState.Main, new Context(_world, _player, startingRoom.Enter(new Context(_world, _player))));
                    mainView = new MainUserInterface(_world, _player!, _interpreter);
                }
            }
            else if (tuiResult.TargetView == GameState.Main)
            {
                tuiResult = mainView!.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Quit)
            {
                Quit();
            }
            else if (tuiResult.TargetView == GameState.Completed)
            {
                SuccessUI successUI = new(setup.SuccessComment);
                tuiResult = successUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Death)
            {
                DeathUI deathUI = new(setup.DeathComment);
                tuiResult = deathUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
            else if (tuiResult.TargetView == GameState.Fight)
            {
                BattleUserInterface battleUI = new();
                tuiResult = battleUI.Execute(new Context(_world, _player!, tuiResult.Context?.ActionResult));
            }
        }
    }

    private void Quit()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold yellow] {setup.QuitPhrase ?? "--------------"}[/]");
        Environment.Exit(0);
    }

    // public void Run()
    // {
    //     IView currentView = new DefaultView(_player);
    //     FinishCode? finishCode = null;

    //     AnsiConsole.Live(currentView.Layout).Start(ctx =>
    //     {
    //         currentView.Update();
    //         ctx.Refresh();
    //         while (finishCode == null)
    //         {
                

    //             // string command = AnsiConsole.Ask<string>("[green]>[/]");
    //             ConsoleKeyInfo input = Console.ReadKey(intercept: true);

                
    //             //ConsoleKey input = Console.ReadKey(intercept: true).Key;
    //             if (input.Key == ConsoleKey.F12)
    //             {
    //                 finishCode = FinishCode.Quit;
    //             }
    //             else
    //             {
    //                 currentView.UserInput(input);
    //             }

    //             // if (command == "quit")
    //             // {
    //             //     quit = true;
    //             // }

    //             if (input.Key == ConsoleKey.Enter && currentView is MainUserInterface defaultView)
    //             {
    //                 string userInput = defaultView.UserCommand;
    //                 ParsingResult parsingResult = _interpreter.Parse(userInput);
    //                 if (parsingResult.ErrorMessage is not null)
    //                 {
    //                    defaultView.AppendToLog(parsingResult.ErrorMessage); 
    //                 }
    //                 else
    //                 {
    //                     PlayerAction action;
    //                     try
    //                     {
    //                         action = CreatePlayerAction(parsingResult);
    //                         ActionResult? result = _player.Room.InterAct(new Context(_world, _player), action);
    //                         defaultView.AppendToLog(result != null? result.Message: "Det går inte.");
    //                         if (result?.MoveToRoomId != null)
    //                         {
    //                             IRoom? newRoom =_world.GetRoom(result.MoveToRoomId);
    //                             _player.Room = newRoom ?? throw new Exception($"Felkonfigurerat namn på rum: {result.MoveToRoomId}");
    //                             defaultView.AppendToLog(_player.Room.ToString() ?? "");
    //                         }
    //                         else if (result?.GameFinished != null)
    //                         {
    //                             finishCode = result?.GameFinished;
    //                         }

    //                     }
    //                     catch(InputException e)
    //                     {
    //                         defaultView.AppendToLog(e.Message);
    //                     }
    //                     catch(Exception e)
    //                     {
    //                         defaultView.AppendToLog(Markup.Escape($"Tekniskt fel: {e}"));
    //                     }
    //                 }
    //             }
    //             currentView.Update();
    //             ctx.Refresh();
    //         }
    //     });
        
    //     return finishCode!.Value;
    // }

    // private PlayerAction CreatePlayerAction(ParsingResult parsingResult)
    // {
    //     if (parsingResult.Command == null)
    //     {
    //         throw new ArgumentException("ParsingResult.Command is null, cannot construct a PlayerAction from this object.");
    //     }

    //     Predicate predicate = parsingResult.Command.Predicate;
    //     string? directObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.DirectObject.ToString())?.Value;
    //     string? indirectObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.IndirectObject.ToString())?.Value;
    //     string? placeAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbial.ToString())?.Value;
    //     string? mannerAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbial.ToString())?.Value;
    //     string? placeAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbialInit.ToString())?.Value;
    //     string? mannerAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbialInit.ToString())?.Value;
    //     //throw new Exception($"{directObjectStr}, {indirectObjectStr}, {placeAdverbialStr}, {placeAdverbialInitStr}, {mannerAdverbialStr}, {mannerAdverbialInitStr}");
    //     IEntity? directObject, indirectObject, placeAdverbial, mannerAdverbial;
    //     directObject = indirectObject = placeAdverbial = mannerAdverbial = null;

    //     List<IEntity> entities = _player.GetAllEntities().ToList();
    //     List<string> notFound = new();
    //     if (directObjectStr != null)
    //     {
    //         directObject = entities.FirstOrDefault(d => d.IsMatch(directObjectStr));
    //         if (directObject == null)
    //         {
    //             notFound.Add(directObjectStr);
    //         }
    //     }
    //     if (indirectObjectStr != null)
    //     {
    //         indirectObject = entities.FirstOrDefault(d => d.IsMatch(indirectObjectStr));
    //         if (indirectObject == null)
    //         {
    //             notFound.Add(indirectObjectStr);
    //         }
    //     }
    //     if (mannerAdverbialStr != null)
    //     {
    //         mannerAdverbial = entities.FirstOrDefault(d => d.IsMatch(mannerAdverbialStr));
    //         if (mannerAdverbial == null)
    //         {
    //             notFound.Add(mannerAdverbialStr);
    //         }
    //     }
    //     if (placeAdverbialStr != null)
    //     {
    //         placeAdverbial = entities.FirstOrDefault(d => d.IsMatch(placeAdverbialStr));
    //         if (placeAdverbial == null)
    //         {
    //             notFound.Add(placeAdverbialStr);
    //         }
    //     }
    //     if (notFound.Count() > 0)
    //     {
    //         throw new InputException($"{String.Join(", ", notFound)} finns inte här.");
    //     }
    //     return new PlayerAction()
    //     {
    //         IndirectObject = indirectObject,
    //         Predicate = predicate,
    //         DirectObject = directObject,
    //         PlaceAdverbial = placeAdverbial,
    //         MannerAdverbial  = mannerAdverbial,
    //         MannerAdverbialInit = mannerAdverbialInitStr,
    //         PlaceAdverbialInit = placeAdverbialInitStr    
    //     };
    // }
}

public class InputException: Exception
{
    public InputException(string message): base(message)
    {}
}