namespace TextAdventure.Concepts;

public class Context(World world, Player player)
{
    public Player Player { get; } = player;

    public World World { get; } = world;

}