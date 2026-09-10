using System.ComponentModel;
using System.Globalization;
using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.Swedish;

namespace Nai.TextAdventure.Concepts.Implementations;

public abstract class SwedishAbstractRoom: IRoom
{
    public Guid Id { get; } = Guid.NewGuid();
    public abstract List<IEntity> Items { get; }

    public abstract List<IExit> Exits { get; }

    public abstract INoun Name { get;}

    public virtual IEnumerable<INoun>? Synonyms { get; }

    public abstract string Description { get; }

    public override string ToString()
    {
        string items = Items.Count() > 0? $"\n\nFöremål:\n\n {string.Join("\n", Items.Select(i => new string([i.Name.Name[0]]).ToUpper() + i.Name.Name[1..]))}\n" : "";
        string exits = Exits.Where(e => e.IsActivated).Count() > 0? $"\n\nUtgångar:\n\n {string.Join("\n", Exits.Where(e => e.IsActivated).Select(i => new string([i.Name.Name[0]]).ToUpper() + i.Name.Name[1..]))}\n" : "";
        return Description + items + exits;
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
        if (action.Predicate.Verb == Verbs.Titta)
        {
            if (action.PlaceAdverbial == null || (action.PlaceAdverbial == this && (action.PlaceAdverbialInit == "på" || action.PlaceAdverbialInit == "i")))
            {
                return new ActionResult()
                {
                    Message = this.ToString() ?? "Beskrivning saknas. Av någon underlig anledning tittar du, men ser ingenting...är det mörkt? Har du en ögonbindel? Vem vet..."
                };
            }
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
            Message = "Det går inte."
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

