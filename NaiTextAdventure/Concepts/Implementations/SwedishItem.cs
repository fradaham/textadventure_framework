using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.Concepts.Implementations;

public sealed class SwedishItem: IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public required INoun Name { get; init; }
    public IEnumerable<INoun>? Synonyms { get; init; }
    public Func<Context, PlayerAction, ActionResult?> HandleAction {get; init; } = (c, a) => throw new NotImplementedException();

    public required string Description {get; init;}

    public override string ToString()
    {
        return Description;
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

        if (action.Predicate.Verb == Verbs.Ta && action.DirectObject == this)
        {
            if (context.Player.Inventory.Contains(this))
            {
                return new ActionResult()
                {
                    Message = $"Du har redan tagit upp {Name.DefiniteForm}"
                };
            }
            else
            {
                context.Player.Inventory.Add(this);
                context.Player.Room.Items.Remove(this);
                return new ActionResult()
                {
                    Message = $"Du tog upp {Name.DefiniteForm}."
                };
            }
        }
        else if (action.Predicate.Verb == Verbs.Släpp && action.DirectObject == this)
        {
            if (context.Player.Inventory.Contains(this))
            {
                context.Player.Inventory.Remove(this);
                context.Player.Room.Items.Add(this);
                return new ActionResult()
                {
                    Message = $"Du släppte {Name.DefiniteForm}."
                };
            }
            else
            {
                return new ActionResult()
                {
                    Message = $"Du kan inte släppa {Name.DefiniteForm},  eftersom du inte har {Name.Pronomen} på dig."
                };
            }
        }
        else if(action.Predicate.Verb == Verbs.Titta)
        {
            return new ActionResult()
            {
                Message = this.ToString()
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