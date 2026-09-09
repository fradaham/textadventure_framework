using TextAdventure.Language.Concepts;

namespace TextAdventure.Concepts;

public interface IInteractable
{
    ActionResult? InterAct(Context context, PlayerAction action);

}