using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IExit: IEntity
{
    public string TargetRoom { get; init; }

    public string? ExitMessage {get; init; }

    public bool IsActivated {get; set;}
}