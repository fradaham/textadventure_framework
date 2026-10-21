using System.Reflection.Metadata;

namespace Nai.TextAdventure.Language.Concepts;

public interface IPredicate
{
    string Imperativ { get; }

    string Infinitiv { get; }

    IEnumerable<string>? Synonyms { get; } 

    PredicateType Category { get; } 
}

public class Predicate: IPredicate
{
    public required string Imperativ { get; init; }

    public required string Infinitiv { get; init; }

    public IEnumerable<string>? Synonyms { get; init; }

    public PredicateType Category { get; init; } = PredicateType.Other; //Default
}

public enum PredicateType
{
    Other,
    Movement,
    Examination,
    Retrieval, 
    Discard

}

