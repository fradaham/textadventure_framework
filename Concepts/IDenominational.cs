using TextAdventure.Language.Concepts;

namespace TextAdventure.Concepts;

public interface IDenominational
{
    INoun Name { get; }

    IEnumerable<INoun>? Synonyms { get;}

    bool IsMatch(string name);
}