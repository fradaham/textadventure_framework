using Nai.TextAdventure.Concepts;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.English;
using Nai.TextAdventure.Language.General;
using Nai.TextAdventure.Language.Swedish;
using Nai.TextAdventure.UserInterface;
using Spectre.Console;

namespace Nai.TextAdventure;

public class TextGameEngine
{   
    private IInterpreter _interpreter;

    private World _world;

    private UserInterfaceHub _uiHub;

    private string? _quitPhrase;

    public TextGameEngine(IGameSetup setup)
    {
        _interpreter = setup.Language switch 
        {
            GameLanguage.Swedish => new GeneralInterpreter(new SwedishInterpreterSettings()),
            GameLanguage.English => new GeneralInterpreter(new EnglishInterpreterSettings()),
            _ => throw new NotSupportedException("The language setting is not supported")
        };
        _world = setup.World;
        _quitPhrase = setup.QuitPhrase;
        _uiHub = new UserInterfaceHub(setup, this);
    }

    public void Run()
    {
        _uiHub.Run(GameState.Title);
        Quit();
    }

    private void Quit()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold yellow]{_quitPhrase ?? "--------------"}[/]");
        Environment.Exit(0);
    }
    
    internal TuiResult? ProcessUserCommand(string command, Player player, IPrintToUser userUI)
    {
        ParsingResult parsingResult = _interpreter.Parse(command);
        if (parsingResult.ErrorMessage is not null)
        {
            userUI.PrintMessage(parsingResult.ErrorMessage); 
        }
        else
        {
            if (parsingResult.Command == null)
            {
                throw new ArgumentException("ParsingResult.Command is null, cannot construct a PlayerAction from this object.");
            }
            PlayerAction action;
            try
            {
                if (!HelpRequestProcessing(userUI, parsingResult))
                {
                    action = CreatePlayerAction(parsingResult, player, _world);
                    ActionResult? result = player.Room.InterAct(new Context(_world, player), action);
                    return ProcessActionResult(result, player, userUI);
                }
            }
            catch(TextInputException e)
            {
                userUI.PrintMessage(e.Message);
            }
            catch(Exception e)
            {
                userUI.PrintMessage(Markup.Escape($"Technical error: {e}"));
            }
        }

        return null;
    }

    private bool HelpRequestProcessing(IPrintToUser userUI, ParsingResult parsingResult)
    {
        IPredicate predicate = parsingResult.Command!.Predicate;
        string? directObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.DirectObject.ToString())?.Value;

        if (parsingResult.Command.Predicate == Language.Swedish.Predicates.Hjälp || parsingResult.Command.Predicate == Language.English.Predicates.Help) //Needs to be more general?
        {
            if (directObjectStr == null)
            {
                userUI.PrintMessage(Markup.Escape(_interpreter.Help()));
                return true;
            }
            else
            {
                string? predicateHelpText = _interpreter.Help(directObjectStr);
                if (predicateHelpText != null)
                {
                    userUI.PrintMessage(Markup.Escape(predicateHelpText));
                    return true;
                }
            } 
        }

        return false;
    }

    internal TuiResult? ProcessActionResult(ActionResult? result, Player player, IPrintToUser userUI)
    {
        try
        {
            if (result?.Message != null)
            {
                userUI.PrintMessage(result.Message);
            }
            if (result?.Custom != null)
            {
                result.Custom.Invoke(new Context(_world, player));
            }
            if (result?.MoveToRoomId != null)
            {
                IRoom? newRoom = _world.GetRoom(result.MoveToRoomId);
                player.Room = newRoom ?? throw new Exception($"Cannot find room with name '{result.MoveToRoomId}' in world. Check game setup.");
                return ProcessActionResult(player.Room.Enter(new Context(_world, player)), player, userUI);
            }
            else if(result?.Fight != null)
            {
                return new TuiResult(GameState.Fight, result);
            }
            else if (result?.GameResult != null) 
            {
                if (result.GameResult == GameResult.Success)
                {
                    return new TuiResult(GameState.Completed);
                }
                else if (result.GameResult == GameResult.Fail)
                {
                    return new TuiResult(GameState.Death);
                }
            }
        }
        catch(TextInputException e)
        {
            userUI.PrintMessage(e.Message);
        }
        catch(Exception e)
        {
            userUI.PrintMessage(Markup.Escape($"Technical error: {e}"));
        }

        return null;
    }

    private PlayerAction CreatePlayerAction(ParsingResult parsingResult, Player player, World world)
    {
        IPredicate predicate = parsingResult.Command!.Predicate;
        string? directObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.DirectObject.ToString())?.Value;
        string? indirectObjectStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.IndirectObject.ToString())?.Value;
        string? placeAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbial.ToString())?.Value;
        string? mannerAdverbialStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbial.ToString())?.Value;
        string? placeAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.PlaceAdverbialInit.ToString())?.Value;
        string? mannerAdverbialInitStr = parsingResult.SentenceParts?.GetValueOrDefault(SentenceParts.MannerAdverbialInit.ToString())?.Value;
        //throw new Exception($"{directObjectStr}, {indirectObjectStr}, {placeAdverbialStr}, {placeAdverbialInitStr}, {mannerAdverbialStr}, {mannerAdverbialInitStr}");
        IEntity? directObject, indirectObject, placeAdverbial, mannerAdverbial;
        directObject = indirectObject = placeAdverbial = mannerAdverbial = null;

        List<IEntity> entities = player.GetAllEntities().ToList();
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
            if (placeAdverbial == null && predicate.IsMovement) //Special case for movement commands (like go/gå, then you should be able to go the adjacent rooms, even if they are not here in this room)
            {
                IEnumerable<IRoom> adjacentRooms = player.Room.Exits.Where(e => e.IsActivated).Select(e => e.GetTargetRoom(world));
                IRoom? matchingRoom = adjacentRooms.FirstOrDefault(r => r.IsMatch(placeAdverbialStr));
                placeAdverbial = matchingRoom != null?player.Room.Exits.FirstOrDefault(e => e.TargetRoomName == matchingRoom.Name.Name) : null;
            }
            if (placeAdverbial == null)
            {
                notFound.Add(placeAdverbialStr);
            }
        }
        if (notFound.Count() > 0)
        {
            throw new TextInputException($"{String.Join(", ", notFound)} finns inte här.");
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
}