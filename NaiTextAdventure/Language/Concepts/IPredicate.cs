using System.Reflection.Metadata;

namespace Nai.TextAdventure.Language.Concepts;

public interface IPredicate
{
    string Imperativ { get; }

    string Infinitiv { get; }

    IEnumerable<string>? Synonyms { get; } 

    bool IsMovement { get; } //Used to tag predicates indicating that they are used to move the player (like go/gå, run/spring)
}

public class Predicate: IPredicate
{
    public required string Imperativ { get; init; }

    public required string Infinitiv { get; init; }

    public IEnumerable<string>? Synonyms { get; init; }

    public bool IsMovement { get; init; } = false; 
    
}

