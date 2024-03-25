using ApplicationServices.Game.Helpers;
using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace ApplicationServices.Items.Equipment.Weapons.Factories;

public class WandFactory : IWandFactory
{
    public WandEntity CreateNewWand()
    {
        while (true)
        {
            var wand = new WandEntity
            {
                Id = Guid.NewGuid(),
                WeaponType = WeaponTypeEntity.Wand,
            };
            wand.Rarity = ParseHelper.AskForEnum<RarityEntity>("What should the Rarity be?\n0. Common\n1. Uncommon\n2. Rare\n3. Epic\n4. Legendary\n");
            wand.Material = ParseHelper.AskForEnum<WeaponMaterialEntity>("What should the weapon material be?\n0. Wood\n1. Iron\n2. Copper\n3. Stone\n4. Steel\n5. Silver\n6. Gold\n7. Diamond\n");
            wand.Name = ParseHelper.AskForName("What should the name be? ");
            wand.ArmorValue = ParseHelper.AskForInt("Shat should the armor value be? ");
            wand.Durability = ParseHelper.AskForInt("What should the durability cap be? ");
            wand.LevelRequirement = ParseHelper.AskForInt("What should the level requirement be?");
            wand.Weight = ParseHelper.AskForInt("How many kgs does it weigh? ");
            wand.Value = ParseHelper.AskForInt("How much gps is it worth? ");
            wand.MagicAttackValue = ParseHelper.AskForInt("What is the weapons magic attack value? ");
            wand.MeleeAttackValue = ParseHelper.AskForInt("What is the weapons melee atack value?(Physical) ");
            wand.MagicPower = ParseHelper.AskForInt("What is the weapons magic power? ");
            wand.TwoHanded = ParseHelper.AskForBool("Is the weapon two-handed? (True/False) ");
            wand.IsRanged = ParseHelper.AskForBool("Is the weapon ranged? (True/false)");
            if (!wand.IsRanged) return wand;
            wand.Range = ParseHelper.AskForInt("What is the weapons range? ");
            wand.RangedAttackValue = ParseHelper.AskForInt("What is the weapon's ranged attack value? ");
            return wand;
        }
    }
}

