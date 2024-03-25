using ApplicationServices.Game.Helpers;
using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace ApplicationServices.Items.Equipment.Weapons.Factories;

public class StaffFactory : IStaffFactory
{
    public StaffEntity CreateNewStaff()
    {
        while (true)
        {
            var staff = new StaffEntity
            {
                Id = Guid.NewGuid(),
                WeaponType = WeaponTypeEntity.Staff,
            };
            staff.Rarity = ParseHelper.AskForEnum<RarityEntity>("What should the Rarity be?\n0. Common\n1. Uncommon\n2. Rare\n3. Epic\n4. Legendary\n");
            staff.Material = ParseHelper.AskForEnum<WeaponMaterialEntity>("What should the weapon material be?\n0. Wood\n1. Iron\n2. Copper\n3. Stone\n4. Steel\n5. Silver\n6. Gold\n7. Diamond\n");
            staff.Name = ParseHelper.AskForName("What should the name be? ");
            staff.ArmorValue = ParseHelper.AskForInt("What should the armor value be? ");
            staff.Durability = ParseHelper.AskForInt("What should the durability cap be? ");
            staff.LevelRequirement = ParseHelper.AskForInt("What should the level requirement be?");
            staff.Weight = ParseHelper.AskForInt("How many kgs does it weigh? ");
            staff.Value = ParseHelper.AskForInt("How much gps is it worth? ");
            staff.MagicAttackValue = ParseHelper.AskForInt("What is the weapons magic attack value? ");
            staff.MeleeAttackValue = ParseHelper.AskForInt("What is the weapons melee attack value?(Physical) ");
            staff.MagicPower = ParseHelper.AskForInt("What is the weapons magic power? ");
            staff.TwoHanded = ParseHelper.AskForBool("Is the weapon two-handed? (True/False) ");
            staff.IsRanged = ParseHelper.AskForBool("Is the weapon ranged? (True/false)");
            if (!staff.IsRanged) return staff;
            staff.Range = ParseHelper.AskForInt("What is the weapons range? ");
            staff.RangedAttackValue = ParseHelper.AskForInt("What is the weapon's ranged attack value? ");
            return staff;
        }
    }
}

