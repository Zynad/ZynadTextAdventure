using ApplicationServices.Game.Helpers;

namespace ApplicationServices.Admin;

public class AdminManager : IAdminManager
{
    private readonly IDbHandler _dbHandler;

    public AdminManager(IDbHandler dbHandler)
    {
        _dbHandler = dbHandler ?? throw new ArgumentNullException(nameof(dbHandler));
    }

    public async Task AdminLogin()
    {
        string choice = ParseHelper.AskForString("Would you like to login as an admin? \n1. Yes\n2. No\n");
        if (choice is "no" or "2")
        {
            return;
        }

        string tryPassword = ParseHelper.AskForString("Enter the password :");
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
            switch (choice)
            {
                case "1" or "create":
                    await ChooseEntityType("create");
                    break;
                case "2" or "get":
                    await ChooseEntityType("get");
                    break;
                case "3" or "delete":
                    await ChooseEntityType("delete");
                    break;
                case "4" or "update":
                    await ChooseEntityType("update");
                    break;
                case "5" or "exit":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    private async Task ChooseEntityType(string crudMethod)
    {
        while (true)
        {
            string entityType = ParseHelper.AskForString("Choose the entity type: \n1. Armor\n2. Weapon\n3. Item\n4. Go back\n");
            switch (entityType)
            {
                case "armor" or "1":
                    string armorType = ParseHelper.AskForString("Choose the entity type: \n1. Boots\n2. Chest\n3. Gloves\n4. Helmet\n5. Legs\n6. Go back\n");
                    if (CheckGoBack(armorType)) return;
                    await ArmorCrud(crudMethod, armorType);
                    break;
                case "weapon" or "2":
                    string weaponType = ParseHelper.AskForString("Choose the entity type: \n1. Staff\n2. Wand\n3. Axe\n4. Sword\n5. Bow\n6. Go back\n");
                    if(CheckGoBack(weaponType)) return;
                    await WeaponCrud(crudMethod, weaponType);
                    break;
                case "item" or "3":
                    break;
                case "go back" or "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    private async Task ArmorCrud(string crudMethod, string armorType)
    {
        
    }

    private async Task WeaponCrud(string crudMethod, string weaponType)
    {
        while (true)
        {
            switch (crudMethod)
            {
                case "create":
                    switch (weaponType)
                    {
                        case "staff" or "1":
                            await _dbHandler.AddStaff();
                            break;
                        case "wand" or "2":
                            await _dbHandler.AddWand();
                            break;
                        default:
                            Console.WriteLine("Invalid weapon type, please try again.");
                            break;
                    }
                    break;
                case "get":
                    switch (weaponType)
                    {
                        case "staff" or "1":
                            await _dbHandler.GetStaff();
                            break;
                        case "wand" or "2":
                            await _dbHandler.GetWand();
                            break;
                        default:
                            Console.WriteLine("Invalid weapon type, please try again.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid CRUD method, please try again.");
                    break;
            }
        }
    }

    private async Task ItemCrud(string crudMethod, string itemType)
    {

    }

    private bool CheckGoBack(string input)
    {
        return input == "go back";
    }
}

