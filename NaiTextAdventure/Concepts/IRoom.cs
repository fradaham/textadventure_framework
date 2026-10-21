using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IRoom: IEntity
{
    List<IEntity> Items { get; }

    List<IExit> Exits { get; }

    ActionResult? Enter(Context context);

    string ToString(GameLanguage language);

}