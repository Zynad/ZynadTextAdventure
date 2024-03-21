using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities.Items.Models;
public class ItemsBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int LevelRequirement { get; set; }
    public RarityEntity Rarity { get; set; }
    public int Value { get; set; }
    public int Weight { get; set; }
}
