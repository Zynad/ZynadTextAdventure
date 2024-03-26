using Domain.Entities.Armor.Models;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Game.Helpers;

public interface IDbHandler
{
    // Weapon related methods
    Task AddWand(WandEntity entity = null);
    Task UpdateWand(WandEntity entity);
    Task DeleteWand(WandEntity entity);
    Task GetWand();
    Task GetStaff();
    Task AddStaff(StaffEntity entity = null);
    Task UpdateStaff(StaffEntity entity);
    Task DeleteStaff(StaffEntity entity);
    Task AddSword(SwordEntity entity = null);
    Task UpdateSword(SwordEntity entity);
    Task DeleteSword(SwordEntity entity);
    Task GetSword();
    Task AddAxe(AxeEntity entity = null);
    Task UpdateAxe(AxeEntity entity);
    Task DeleteAxe(AxeEntity entity);
    Task GetAxe();
    // Armor related methods
    Task AddBoots(BootsEntity entity = null);
    Task UpdateBoots(BootsEntity entity);
    Task DeleteBoots(BootsEntity entity);
    Task GetBoots();
    Task AddChest(ChestEntity entity = null);
    Task UpdateChest(ChestEntity entity);
    Task DeleteChest(ChestEntity entity);
    Task GetChest();
    Task AddGloves(GlovesEntity entity = null);
    Task UpdateGloves(GlovesEntity entity);
    Task DeleteGloves(GlovesEntity entity);
    Task GetGloves();
    Task AddHelmet(HelmetEntity entity = null);
    Task UpdateHelmet(HelmetEntity entity);
    Task DeleteHelmet(HelmetEntity entity);
    Task GetHelmet();
    Task AddLegs(LegsEntity entity = null);
    Task UpdateLegs(LegsEntity entity);
    Task DeleteLegs(LegsEntity entity);
    Task GetLegs();
}

