using Domain.Entities.Items.Models;

namespace Domain.Entities.Equipment.Models;
public class EquipmentBaseEntity : ItemsBaseEntity
{
    public int Durability { get; set; }
}
