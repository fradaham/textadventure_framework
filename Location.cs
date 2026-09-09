namespace TextAdventure;

public class Location(string name, string text)
{
    public string Description { get; } = text;

    public string Name { get; } = name;
}