using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IExit: IEntity
{
    IEnumerable<string>? AllowedPrepositions {get; init;}

    string TargetRoom { get; init; }

    string? ExitMessage {get; init; }

    bool IsActivated {get; set;}
}