using System.Reflection.Metadata;

namespace Nai.TextAdventure.Language.Concepts;

public class Predicate
{
    public required string Verb { get; init; }

    public required string Infinitiv { get; init; }

    public IEnumerable<string>? Synonyms { get; init; }

    public IEnumerable<string> GetAllMatchingAlternatives()
    {
        IList<string> all = new List<string>();
        all.Add(Verb);
        foreach (string syn in Synonyms ?? [])
        {
            all.Add(syn);
        }

        return all;
    }
    
}

