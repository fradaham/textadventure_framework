using Nai.TextAdventure.Language;

namespace Nai.TextAdventure.Concepts;

public interface IRoom: IEntity
{
    List<IEntity> Items { get; }

    List<IExit> Exits { get; }

}