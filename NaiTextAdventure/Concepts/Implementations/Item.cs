using Nai.TextAdventure.Language.Concepts;
using Nai.TextAdventure.Language.General;

namespace Nai.TextAdventure.Concepts.Implementations;

/// <summary>
/// A default implementation for items with support for basic interaction in both swedish and english.
/// Use this class to implement new items, especially items that need to hold some kind of state in the game.
/// Override the HandleAction method to implement custom interaction for the item.
/// </summary>
public abstract class Item: IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public abstract INoun Name { get; }
    public abstract IEnumerable<INoun>? Synonyms { get; }

    public abstract string Description {get; }

    public virtual bool IsFixed => false;

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

        if (action.Predicate.Category == PredicateType.Retrieval && action.DirectObject == this && action.MannerAdverbial == null)
        {
            if (context.Player.Inventory.Contains(this))
            {
                return new ActionResult()
                {
                    Message = UserText.ItemAlreadyInInventory(context.Language, Name)
                };
            }
            else if (IsFixed)
            {
                return new ActionResult()
                {
                    Message = UserText.CannotPickUpItem(context.Language, Name)
                };
            }
            else
            {
                context.Player.Inventory.Add(this);
                context.Player.Room.Items.Remove(this);
                return new ActionResult()
                {
                    Message = UserText.ItemPickedUp(context.Language, Name)
                };
            }
        }
        else if (action.Predicate.Category == PredicateType.Discard && action.DirectObject == this)
        {
            if (context.Player.Inventory.Contains(this))
            {
                context.Player.Inventory.Remove(this);
                context.Player.Room.Items.Add(this);
                return new ActionResult()
                {
                    Message = UserText.ItemDiscarded(context.Language, Name)
                };
            }
            else
            {
                return new ActionResult()
                {
                    Message = UserText.CantDiscardItemNotInInventory(context.Language, Name)
                };
            }
        }
        else if ((action.Predicate.Category == PredicateType.Examination && action.DirectObject == this && action.MannerAdverbial == null) || (action.Predicate.Category == PredicateType.Examination && action.PlaceAdverbial == this && (action.PlaceAdverbialInit == null || action.PlaceAdverbialInit == "på")))
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