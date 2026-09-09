namespace Nai.TextAdventure.Concepts;
public class World
{
    public required List<IRoom> Rooms {get; init;}

    public required List<IEntity> UnassignedItems {get; init;}

    public IRoom? GetRoom(string name)
    {
        return Rooms.FirstOrDefault(r => r.Name.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public IEntity GetAndAssignItem(string name)
    {
        IEntity? item = UnassignedItems.FirstOrDefault(i => i.Name.Name == name);
        if (item == null)
        {
            throw new ArgumentException($"An unassigned item with name '{name}' does not exist in the world.");
        }
        
        UnassignedItems.Remove(item);
        
        return item;
    }
}
