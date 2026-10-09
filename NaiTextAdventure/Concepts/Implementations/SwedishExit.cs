using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.Concepts.Implementations;

public sealed class SwedishExit: IExit
{
    public Guid Id { get; } = Guid.NewGuid();
    public required INoun Name { get; init; }
    public IEnumerable<INoun>? Synonyms { get; init; }
    public IEnumerable<string>? AllowedPrepositions {get; init;}
    public required string TargetRoomName { get; init; }
    public string? ExitMessage { get; init; }
    public required string Description {get; init;}
    public bool IsActivated {get; set;} = true;
    public bool IsFixed {get; set;} = true;
    public Func<Context, PlayerAction, ActionResult?> HandleAction {get; init; } = (c, a) => throw new NotImplementedException();

    public IRoom GetTargetRoom(World world)
    {
        try
        {
            IRoom room = world.Rooms.First(r => r.Name.Name.Equals(TargetRoomName, StringComparison.InvariantCultureIgnoreCase));
            return room;
        }
        catch(Exception e)
        {
            throw new KeyNotFoundException($"No room with name {TargetRoomName} exists in the world setup", e);
        }
    }

    public ActionResult? InterAct(Context context, PlayerAction action)
    {
        try
        {
            ActionResult? actionResult = HandleAction(context, action);
            if (actionResult != null)
            {
                return actionResult;
            }
        }
        catch(NotImplementedException)
        {}
        //If not implemented, or HandleAction returns null, do general handling:

        if (action.Predicate == Predicates.Gå && (action.MannerAdverbial == this || action.PlaceAdverbial == this))
        {
            return new ActionResult()
            {
                Message = $"{ExitMessage}",
                MoveToRoomId = TargetRoomName
            };
        
        }
        else if (action.Predicate == Predicates.Titta && action.PlaceAdverbial == this)
        {
            return new ActionResult()
            {
                Message = $"{Description}",
            };
        }
        else
        {
            return null;
        }
    }

    public bool IsMatch(string name)
    {
        return Utils.IsMatch(this, name);
    }


}