using TextAdventure.Language.Concepts;

namespace TextAdventure.Concepts;

public interface IExit: IEntity
{
    public string TargetRoom { get; init; }

    public string? ExitMessage {get; init; }
}