
using Nai.TextAdventure.Concepts;

namespace Nai.TextAdventure.Language.Concepts;

public class PlayerAction
{
    public IEntity? DirectObject {get; init;}

    public IEntity? IndirectObject {get; init;}

    public required Predicate Predicate {get; init;}

    public IEntity? PlaceAdverbial {get; init;}

    public IEntity? MannerAdverbial {get; init;}

    public string? MannerAdverbialInit {get; init;}

    public string? PlaceAdverbialInit {get; init;}

    
}