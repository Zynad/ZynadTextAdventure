using ApplicationServices.Game.Helpers;

namespace ApplicationServices.Admin;

public class AdminManager(IDbHandler dbHandler) : IAdminManager
{
    private readonly IDbHandler _dbHandler = dbHandler ?? throw new ArgumentNullException(nameof(dbHandler));

    private enum CrudAction
    {
        Create = 1,
        Get = 2,
        Delete = 3,
        Update = 4,
        Exit = 5
    }

    private enum EntityCategory
    {
        Armor = 1,
        Weapon = 2,
        Item = 3,
        GoBack = 4,
        Exit = 5
    }

    private enum ArmorType
    {
        Boots = 1,
        Chest = 2,
        Gloves = 3,
        Helmet = 4,
        Legs = 5,
        GoBack = 6
    }

    private enum WeaponType
    {
        Staff = 1,
        Wand = 2,
        Axe = 3,
        Sword = 4,
        Bow = 5,
        GoBack = 6
    }

    public async Task AdminLogin()
    {
        string choice = ParseHelper.AskForString("Would you like to login as an admin? \n1. Yes\n2. No\n");
        if (choice != "yes" && choice != "1")
        {
            return;
        }

        string tryPassword = ParseHelper.AskForString("Enter the password : ");
        if (tryPassword is "bytmig123!")
        {
            Console.WriteLine("Admin login successful!");
            await AdminView();
        }
    }

    private async Task AdminView()
    {
        while (true)
        {
            string choice = ParseHelper.AskForString("What would you like to do? \n1. Create\n2. Get\n3. Delete\n4. Update\n5. Exit\n");
            if (string.IsNullOrWhiteSpace(choice))
            {
                return;
            }
            if (!TryParseSelection(choice, out CrudAction action))
            {
                Console.WriteLine("Invalid choice, please try again.");
                continue;
            }

            if (action == CrudAction.Exit)
            {
                return;
            }

            await ChooseEntityType(action);
        }
    }

    private async Task ChooseEntityType(CrudAction action)
    {
        while (true)
        {
            string entityType = ParseHelper.AskForString("Choose the entity type: \n1. Armor\n2. Weapon\n3. Item\n4. Go back\n5. Exit\n");
            if (string.IsNullOrWhiteSpace(entityType))
            {
                return;
            }
            if (!TryParseSelection(entityType, out EntityCategory category))
            {
                Console.WriteLine("Invalid choice, please try again.");
                continue;
            }

            switch (category)
            {
                case EntityCategory.Armor:
                    string armorType = ParseHelper.AskForString("Choose the entity type: \n1. Boots\n2. Chest\n3. Gloves\n4. Helmet\n5. Legs\n6. Go back\n");
                    if (!TryParseSelection(armorType, out ArmorType parsedArmor) || parsedArmor == ArmorType.GoBack)
                    {
                        return;
                    }

                    await ArmorCrud(action, parsedArmor);
                    break;
                case EntityCategory.Weapon:
                    string weaponType = ParseHelper.AskForString("Choose the entity type: \n1. Staff\n2. Wand\n3. Axe\n4. Sword\n5. Bow\n6. Go back\n");
                    if (!TryParseSelection(weaponType, out WeaponType parsedWeapon) || parsedWeapon == WeaponType.GoBack)
                    {
                        return;
                    }

                    await WeaponCrud(action, parsedWeapon);
                    break;
                case EntityCategory.Item:
                    return;
                case EntityCategory.GoBack:
                    return;
                case EntityCategory.Exit:
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    private async Task ArmorCrud(CrudAction action, ArmorType armorType)
    {
        switch (action)
        {
            case CrudAction.Create:
                await HandleArmorCreate(armorType);
                return;
            case CrudAction.Get:
                await HandleArmorGet(armorType);
                return;
            default:
                Console.WriteLine("Invalid CRUD method, please try again.");
                return;
        }
    }

    private async Task HandleArmorCreate(ArmorType armorType)
    {
        switch (armorType)
        {
            case ArmorType.Boots:
                await _dbHandler.AddBoots();
                break;
            case ArmorType.Chest:
                await _dbHandler.AddChest();
                break;
            case ArmorType.Gloves:
                await _dbHandler.AddGloves();
                break;
            case ArmorType.Helmet:
                await _dbHandler.AddHelmet();
                break;
            case ArmorType.Legs:
                await _dbHandler.AddLegs();
                break;
            default:
                Console.WriteLine("Invalid armor type, please try again.");
                break;
        }
    }

    private async Task HandleArmorGet(ArmorType armorType)
    {
        switch (armorType)
        {
            case ArmorType.Boots:
                await _dbHandler.GetBoots();
                break;
            case ArmorType.Chest:
                await _dbHandler.GetChest();
                break;
            case ArmorType.Gloves:
                await _dbHandler.GetGloves();
                break;
            case ArmorType.Helmet:
                await _dbHandler.GetHelmet();
                break;
            case ArmorType.Legs:
                await _dbHandler.GetLegs();
                break;
            default:
                Console.WriteLine("Invalid armor type, please try again.");
                break;
        }
    }

    private async Task WeaponCrud(CrudAction action, WeaponType weaponType)
    {
        switch (action)
        {
            case CrudAction.Create:
                await HandleWeaponCreate(weaponType);
                break;
            case CrudAction.Get:
                await HandleWeaponGet(weaponType);
                break;
            default:
                Console.WriteLine("Invalid CRUD method, please try again.");
                break;
        }
    }

    private async Task HandleWeaponCreate(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Staff:
                await _dbHandler.AddStaff();
                break;
            case WeaponType.Wand:
                await _dbHandler.AddWand();
                break;
            case WeaponType.Axe:
                await _dbHandler.AddAxe();
                break;
            case WeaponType.Sword:
                await _dbHandler.AddSword();
                break;
            default:
                Console.WriteLine("Invalid weapon type, please try again.");
                break;
        }
    }

    private async Task HandleWeaponGet(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Staff:
                await _dbHandler.GetStaff();
                break;
            case WeaponType.Wand:
                await _dbHandler.GetWand();
                break;
            case WeaponType.Axe:
                await _dbHandler.GetAxe();
                break;
            case WeaponType.Sword:
                await _dbHandler.GetSword();
                break;
            default:
                Console.WriteLine("Invalid weapon type, please try again.");
                break;
        }
    }

    private async Task ItemCrud(string crudMethod, string itemType)
    {

    }

    private static bool TryParseSelection<TEnum>(string input, out TEnum parsed) where TEnum : struct, Enum
    {
        string normalized = input.Replace(" ", string.Empty);

        if (Enum.TryParse(normalized, ignoreCase: true, out parsed) && Enum.IsDefined(typeof(TEnum), parsed))
        {
            return true;
        }

        if (int.TryParse(normalized, out int numeric) && Enum.IsDefined(typeof(TEnum), numeric))
        {
            parsed = (TEnum)Enum.ToObject(typeof(TEnum), numeric);
            return true;
        }

        parsed = default;
        return false;
    }
}

