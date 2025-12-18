using ApplicationServices.Items.Equipment.Armor.Factories;
using ApplicationServices.Items.Equipment.Weapons.Factories;
using Domain.Entities.Armor.Models;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Armor;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;

public class DbHandler(
    IWandRepository wandRepository,
    IStaffRepository staffRepository,
    ISwordRepository swordRepository,
    IAxeRepository axeRepository,
    IBootsRepository bootsRepository,
    IChestRepository chestRepository,
    IGlovesRepository glovesRepository,
    IHelmetRepository helmetRepository,
    ILegsRepository legsRepository,
    IWandFactory wandFactory,
    IStaffFactory staffFactory,
    ISwordFactory swordFactory,
    IAxeFactory axeFactory,
    IBootsFactory bootsFactory,
    IChestFactory chestFactory,
    IGlovesFactory glovesFactory,
    IHelmetFactory helmetFactory,
    ILegsFactory legsFactory)
    : IDbHandler
{
    #region Wand

    public async Task AddWand(WandEntity? entity = null)
    {
        if (entity != null)
        {
            await wandRepository.AddAsync(entity);
            return;
        }

        var wand = wandFactory.CreateNewWand();
        var createdItem = await wandRepository.AddAsync(wand);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }

    public async Task UpdateWand(WandEntity entity)
    {
        await wandRepository.UpdateAsync(entity);
    }

    public async Task DeleteWand(WandEntity entity)
    {
        await wandRepository.DeleteAsync(entity);
    }

    public async Task GetWand()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Staff

    public Task GetStaff()
    {
        throw new NotImplementedException();
    }

    public async Task AddStaff(StaffEntity? entity = null)
    {
        if (entity != null)
        {
            await staffRepository.AddAsync(entity);
            return;
        }

        var staff = staffFactory.CreateNewStaff();
        var createdItem = await staffRepository.AddAsync(staff);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }

    public async Task UpdateStaff(StaffEntity entity)
    {
        await staffRepository.UpdateAsync(entity);
    }

    public async Task DeleteStaff(StaffEntity entity)
    {
        await staffRepository.DeleteAsync(entity);
    }

    #endregion

    #region Sword

    public async Task AddSword(SwordEntity? entity = null)
    {
        if (entity != null)
        {
            await swordRepository.AddAsync(entity);
            return;
        }

        var sword = swordFactory.CreateNewSword();
        var createdItem = await swordRepository.AddAsync(sword);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }

    public async Task UpdateSword(SwordEntity entity)
    {
        await swordRepository.UpdateAsync(entity);
    }

    public async Task DeleteSword(SwordEntity entity)
    {
        await swordRepository.DeleteAsync(entity);
    }

    public async Task GetSword()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Axe

    public async Task AddAxe(AxeEntity? entity = null)
    {
        if (entity != null)
        {
            await axeRepository.AddAsync(entity);
            return;
        }
        var axe = axeFactory.CreateNewAxe();
        var createdItem = await axeRepository.AddAsync(axe);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }
    public async Task UpdateAxe(AxeEntity entity)
    {
        await axeRepository.UpdateAsync(entity);
    }
    public async Task DeleteAxe(AxeEntity entity)
    {
        await axeRepository.DeleteAsync(entity);
    }
    public async Task GetAxe()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Boots
    public async Task AddBoots(BootsEntity? entity = null)
    {
        if (entity != null)
        {
            await bootsRepository.AddAsync(entity);
            return;
        }
        var boots = bootsFactory.CreateNewBoots();
        var createdItem = await bootsRepository.AddAsync(boots);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }
    public async Task UpdateBoots(BootsEntity entity)
    {
        await bootsRepository.UpdateAsync(entity);
    }
    public async Task DeleteBoots(BootsEntity entity)
    {
        await bootsRepository.DeleteAsync(entity);
    }
    public async Task GetBoots()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Chest
    public async Task AddChest(ChestEntity? entity = null)
    {
        if (entity != null)
        {
            await chestRepository.AddAsync(entity);
            return;
        }
        var chest = chestFactory.CreateNewChest();
        var createdItem = await chestRepository.AddAsync(chest);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }
    public async Task UpdateChest(ChestEntity entity)
    {
        await chestRepository.UpdateAsync(entity);
    }
    public async Task DeleteChest(ChestEntity entity)
    {
        await chestRepository.DeleteAsync(entity);
    }
    public async Task GetChest()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Gloves
    public async Task AddGloves(GlovesEntity? entity = null)
    {
        if (entity != null)
        {
            await glovesRepository.AddAsync(entity);
            return;
        }
        var gloves = glovesFactory.CreateNewGloves();
        var createdItem = await glovesRepository.AddAsync(gloves);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }
    public async Task UpdateGloves(GlovesEntity entity)
    {
        await glovesRepository.UpdateAsync(entity);
    }
    public async Task DeleteGloves(GlovesEntity entity)
    {
        await glovesRepository.DeleteAsync(entity);
    }
    public async Task GetGloves()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Helmet
    public async Task AddHelmet(HelmetEntity? entity = null)
    {
        if (entity != null)
        {
            await helmetRepository.AddAsync(entity);
            return;
        }
        var helmet = helmetFactory.CreateNewHelmet();
        var createdItem = await helmetRepository.AddAsync(helmet);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
    }
    public async Task UpdateHelmet(HelmetEntity entity)
    {
        await helmetRepository.UpdateAsync(entity);
    }
    public async Task DeleteHelmet(HelmetEntity entity)
    {
        await helmetRepository.DeleteAsync(entity);
    }
    public async Task GetHelmet()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Legs
    public async Task AddLegs(LegsEntity? entity = null)
    {
        if (entity != null)
        {
            await legsRepository.AddAsync(entity);
            return;
        }
        var legs = legsFactory.CreateNewLegs();
        var createdItem = await legsRepository.AddAsync(legs);
        Console.WriteLine($"{createdItem.Name} was added in the database!");
        
    }
    public async Task UpdateLegs(LegsEntity entity)
    {
        await legsRepository.UpdateAsync(entity);
    }
    public async Task DeleteLegs(LegsEntity entity)
    {
        await legsRepository.DeleteAsync(entity);
    }
    public async Task GetLegs()
    {
        throw new NotImplementedException();
    }
    #endregion

}