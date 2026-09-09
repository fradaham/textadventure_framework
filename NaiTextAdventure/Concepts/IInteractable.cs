using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IInteractable
{
    ActionResult? InterAct(Context context, PlayerAction action);

}