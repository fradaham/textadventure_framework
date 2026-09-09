using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public interface IDenominational
{
    INoun Name { get; }

    IEnumerable<INoun>? Synonyms { get;}

    bool IsMatch(string name);
}