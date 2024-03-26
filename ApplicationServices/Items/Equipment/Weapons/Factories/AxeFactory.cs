using ApplicationServices.Game.Helpers;
using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace ApplicationServices.Items.Equipment.Weapons.Factories;

public class AxeFactory : IAxeFactory
{
    public AxeEntity CreateNewAxe()
    {
        while (true)
        {
            var axe = new AxeEntity
            {
                Id = Guid.NewGuid(),
                WeaponType = WeaponTypeEntity.Axe,
                Rarity = ParseHelper.AskForEnum<RarityEntity>("What should the Rarity be?\n0. Common\n1. Uncommon\n2. Rare\n3. Epic\n4. Legendary\n"),
                Material = ParseHelper.AskForEnum<WeaponMaterialEntity>("What should the weapon material be?\n0. Wood\n1. Iron\n2. Copper\n3. Stone\n4. Steel\n5. Silver\n6. Gold\n7. Diamond\n"),
                Name = ParseHelper.AskForName("What should the name be? "),
                ArmorValue = ParseHelper.AskForInt("What should the armor value be? "),
                Durability = ParseHelper.AskForInt("What should the durability cap be? "),
                LevelRequirement = ParseHelper.AskForInt("What should the level requirement be?"),
                Weight = ParseHelper.AskForInt("How many kgs does it weigh? "),
                Value = ParseHelper.AskForInt("How much gps is it worth? "),
                MagicAttackValue = ParseHelper.AskForInt("What is the weapons magic attack value? "),
                MeleeAttackValue = ParseHelper.AskForInt("What is the weapons melee attack value?(Physical) "),
                MagicPower = ParseHelper.AskForInt("What is the weapons magic power? "),
                TwoHanded = ParseHelper.AskForBool("Is the weapon two-handed? (True/False) "),
                IsRanged = ParseHelper.AskForBool("Is the weapon ranged? (True/false)")
            };
            if (!axe.IsRanged) return axe;
            axe.Range = ParseHelper.AskForInt("What is the weapons range? ");
            axe.RangedAttackValue = ParseHelper.AskForInt("What is the weapon's ranged attack value? ");
            return axe;
        }
    }
}

