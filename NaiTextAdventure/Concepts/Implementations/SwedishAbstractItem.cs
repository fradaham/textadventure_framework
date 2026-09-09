using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.Concepts.Implementations;

public abstract class SwedishAbstractItem: IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public abstract INoun Name { get; }
    public abstract IEnumerable<INoun>? Synonyms { get; }

    public abstract string Description {get; }

    public virtual ActionResult? InterAct(Context context, PlayerAction action)
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
        else if ((action.Predicate.Verb == Verbs.Undersök && action.DirectObject == this) || (action.Predicate.Verb == Verbs.Titta && action.PlaceAdverbial == this && (action.PlaceAdverbialInit == null || action.PlaceAdverbialInit == "på")))
        {
            return new ActionResult()
            {
                Message = this.ToString()?? $"Du ser inget speciellt när du tittar på {Name.DefiniteForm}"
            };
        }
        else
        {
            return null;
        }
    }

    public override string ToString()
    {
        return Description;
    }

    public bool IsMatch(string name)
    {
        return Utils.IsMatch(this, name);
    }

    
    public virtual ActionResult? HandleAction(Context context, PlayerAction action)
    {
        throw new NotImplementedException();
    }

}