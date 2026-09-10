using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.Concepts.Implementations;

public sealed class SwedishExit: IExit
{
    public Guid Id { get; } = Guid.NewGuid();
    public required INoun Name { get; init; }
    public IEnumerable<INoun>? Synonyms { get; init; }
    public required string TargetRoom { get; init; }
    public string? ExitMessage { get; init; }
    public required string Description {get; init;}
    public bool IsActivated {get; set;} = true;
    public Func<Context, PlayerAction, ActionResult?> HandleAction {get; init; } = (c, a) => throw new NotImplementedException();

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

        if (action.Predicate.Verb == Verbs.Gå && (action.MannerAdverbial == this || action.PlaceAdverbial == this))
        {
            return new ActionResult()
            {
                Message = $"{ExitMessage}",
                MoveToRoomId = TargetRoom
            };
        
        }
        else if (action.Predicate.Verb == Verbs.Titta && action.PlaceAdverbial == this)
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