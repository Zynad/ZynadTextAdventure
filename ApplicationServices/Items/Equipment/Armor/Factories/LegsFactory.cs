using ApplicationServices.Game.Helpers;
using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace ApplicationServices.Items.Equipment.Armor.Factories;

public class LegsFactory : ILegsFactory
{
    public LegsEntity CreateNewLegs()
    {
        while (true)
        {
            var legs = new LegsEntity
            {
                Id = Guid.NewGuid(),
                Rarity = ParseHelper.AskForEnum<RarityEntity>("What should the Rarity be?\n0. Common\n1. Uncommon\n2. Rare\n3. Epic\n4. Legendary\n"),
                Material = ParseHelper.AskForEnum<ArmorMaterialEntity>("What should the armor material be?\n0. Leather\n1. Iron\n2. Steel\n3. Silver\n4. Gold\n5. Diamond\n"),
                Name = ParseHelper.AskForName("What should the name be? "),
                ArmorValue = ParseHelper.AskForInt("What should the armor value be? "),
                Durability = ParseHelper.AskForInt("What should the durability cap be? "),
                LevelRequirement = ParseHelper.AskForInt("What should the level requirement be?"),
                Weight = ParseHelper.AskForInt("How many kgs does it weigh? "),
                Value = ParseHelper.AskForInt("How much gps is it worth? "),
            };
            return legs;
        }
    }
}

