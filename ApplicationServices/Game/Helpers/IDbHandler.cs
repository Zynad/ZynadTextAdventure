using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Game.Helpers;

public interface IDbHandler
{
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
}

