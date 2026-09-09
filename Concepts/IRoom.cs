using TextAdventure.Language;

namespace TextAdventure.Concepts;

public interface IRoom: IEntity
{
    List<IEntity> Items { get; }

    List<Exit> Exits { get; }

}