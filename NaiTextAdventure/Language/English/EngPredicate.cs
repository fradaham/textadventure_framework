using System.Dynamic;
using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.English;

public class EngPredicate : IPredicate
{
    public required string Imperativ { get; init;}

    public string Infinitiv { get => Imperativ;} //Infinitiv and imperativ forms are the same in english

    public IEnumerable<string>? Synonyms { get; init; }

    public bool IsMovement { get; init;} = false;
}