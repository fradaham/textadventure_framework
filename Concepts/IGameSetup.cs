using TextAdventure.Language.Concepts;

namespace TextAdventure.Concepts;

public interface IGameSetup
{
    string Title {get;}

    string SubTitle {get;}

    string? QuitPhrase { get; }

    string? Creator { get; }

    string? DeathComment { get; }

    string? SuccessComment { get; }

    World World { get; }

    IEnumerable<IEntity> InitialPlayerInventory {get;}

    string StartingRoomName { get; }

    IInterpreter MainInterpreter { get;}
}