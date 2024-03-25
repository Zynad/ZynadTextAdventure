using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.Factories;

public class WandFactory : IWandFactory
{
    public async Task<WandEntity> CreateNewWand()
    {
        return new WandEntity();
    }
}

