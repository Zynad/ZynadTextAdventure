using TextAdventure.Repos.Items.Models;

namespace TextAdventure.Repos.Equipment.Models;
public class EquipmentBaseEntity : ItemsBaseEntity
{
    public int Durability { get; set; }
}
