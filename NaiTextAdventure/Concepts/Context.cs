using Nai.TextAdventure.Language.Concepts;

namespace Nai.TextAdventure.Concepts;

public class Context(World world, Player player, GameLanguage language)
{
    public Player Player { get; } = player;

    public World World { get; } = world;

    public GameLanguage Language { get; } = language;
}