using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IGameSetup
{
    string Title {get;}

    string SubTitle {get;}

    string About { get; }

    string? QuitPhrase { get; }

    string? Creator { get; }

    string? DeathComment { get; }

    string? SuccessComment { get; }

    string? TitleMusic { get; }

    string? MainMusic {get;}

    string? DeathMusic {get;}

    string? SuccessMusic {get;}

    string? BattleMusic {get;}

    World World { get; }

    IEnumerable<IEntity> InitialPlayerInventory {get;}

    string StartingRoomName { get; }

    IInterpreter MainInterpreter { get;}
}