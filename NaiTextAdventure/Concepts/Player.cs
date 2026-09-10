using Nai.TextAdventure.Concepts;

namespace Nai.TextAdventure.Concepts;

public class Player(string name, byte health, byte maxHealth, byte fightingSkill, byte maxFightingSkill, byte gold, IRoom room)
{
    public string Name { get; } = name;
    public byte Health { get; set; } = health;

    public byte MaxHealth { get; set; } = maxHealth;

    public byte FightingSkill { get; set; } = fightingSkill;

    public byte MaxFightingSkill { get; set; } = maxFightingSkill;

    public byte Gold {get; set; } = gold;

    public List<IEntity> Inventory { get; set; } = new List<IEntity>();

    public IRoom Room { get; set; } = room;

    public IEnumerable<IEntity> GetAllEntities()
    {
        List<IEntity> entities = new List<IEntity>();
        entities.AddRange(Room.Items);
        entities.AddRange(Inventory);
        entities.AddRange(Room.Exits.Where(e => e.IsActivated));
        entities.Add(Room);

        return entities;
    }

}