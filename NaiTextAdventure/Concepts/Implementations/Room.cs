using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.General;

namespace Nai.TextAdventure.Concepts.Implementations;

/// <summary>
/// A default implementation for rooms with support for basic interaction in both swedish and english.
/// Use this class to implement the rooms in the game.
/// </summary>
public abstract class Room: IRoom
{
    public Guid Id { get; } = Guid.NewGuid();
    public abstract List<IEntity> Items { get; }

    public abstract List<IExit> Exits { get; }

    public abstract INoun Name { get;}

    public virtual IEnumerable<INoun>? Synonyms { get; }

    public abstract string Description { get; }

    public bool IsFixed {get;} = true;

    public string ToString(GameLanguage language)
    {
        string items = Items.Count() > 0? $"\n\n{UserText.Items(language)}:\n\n {string.Join("\n", Items.Select(i => new string([i.Name.Name[0]]).ToUpper() + i.Name.Name[1..]))}" : String.Empty;
        string exits = Exits.Where(e => e.IsActivated).Count() > 0? $"\n\n{UserText.Exits(language)}:\n\n {string.Join("\n", Exits.Where(e => e.IsActivated).Select(i => new string([i.Name.Name[0]]).ToUpper() + i.Name.Name[1..]))}" : String.Empty;
        return $"{Description}{items}{exits}";
    }

    public virtual ActionResult? Enter(Context context)
    {
        return new ActionResult()
        {
            Message = this.ToString(context.Language)
        };
    }

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
        if (action.Predicate.Category == PredicateType.Examination && (action.DirectObject == this || action.PlaceAdverbial == this) || (action.DirectObject == null && action.PlaceAdverbial == null))
        {
            return new ActionResult()
            {
                Message = this.ToString(context.Language)
            };
        }

        if (action.DirectObject != this)
        {
            ActionResult? result = action.DirectObject?.InterAct(context, action);
            if (result != null)
            {
                return result;
            }
        }
        if (action.IndirectObject != this)
        {
            ActionResult? result = action.IndirectObject?.InterAct(context, action);
            if (result != null)
            {
                return result;
            }
        }
        if (action.PlaceAdverbial != this)
        {
            ActionResult? result = action.PlaceAdverbial?.InterAct(context, action);
            if (result != null)
            {
                return result;
            }
        }
        if (action.MannerAdverbial != this)
        {
            ActionResult? result = action.MannerAdverbial?.InterAct(context, action);
            if (result != null)
            {
                return result;
            }
        }

        return new ActionResult()
        {
            Message = UserText.CantDoThat(context.Language)
        };
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

