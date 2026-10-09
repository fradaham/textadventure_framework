using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Language.English;

 public static class Predicates
{
    public static readonly EngPredicate Go = new()
    {
        Imperativ = "go",
        IsMovement = true
    };

    public static readonly EngPredicate Put = new()
    {
        Imperativ = "put",
    };

    public static readonly EngPredicate Pick_up = new()
    {
        Synonyms = ["take", "grab", "get"],
        Imperativ = "pick up",
    };

    public static readonly EngPredicate Open = new()
    {
        Imperativ = "open",
    };

    public static readonly EngPredicate Close = new()
    {
        Imperativ = "close",
        Synonyms = ["shut"]
    };

    public static readonly EngPredicate Discard = new()
    {
        Imperativ = "discard"
    };

    public static readonly EngPredicate Unlock = new()
    {
        Imperativ = "unlock",
    };

    public static readonly EngPredicate Lock = new()
    {
        Imperativ = "lock",
    };

    public static readonly EngPredicate Look = new()
    {
        Imperativ = "look"
    };

    public static readonly EngPredicate Examine = new()
    {
        Imperativ = "examine"
    };

    public static readonly EngPredicate Throw = new()
    {
        Imperativ = "throw",        
    };

    public static readonly EngPredicate Stab = new()
    {
        Imperativ = "stab",
    };

    public static readonly EngPredicate Help = new()
    {
        Imperativ = "help",
    };

    public static readonly EngPredicate Eat = new()
    {
        Imperativ = "eat",
    };
}