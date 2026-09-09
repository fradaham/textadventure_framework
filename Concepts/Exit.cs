using TextAdventure.Language.Concepts;
using TextAdventure.Language.Swedish;

namespace TextAdventure.Concepts;

public class Exit: IDenominational
{
    public required INoun Name { get; init; }

    public IEnumerable<INoun>? Synonyms { get; init; }

    public required string TargetRoom { get; init; }

    public string? ExitMessage {get; init; }

    public bool IsMatch(string name)
    {
        return Utils.IsMatch(this, name);
    }

}