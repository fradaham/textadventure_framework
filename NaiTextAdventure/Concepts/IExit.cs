using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IExit: IEntity
{
    IEnumerable<string>? AllowedPrepositions {get; init;}

    string TargetRoomName { get; init; }

    string? ExitMessage {get; init; }

    bool IsActivated {get; set;}

    IRoom GetTargetRoom(World world); 
}