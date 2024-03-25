﻿using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.Factories;

public interface IWandFactory
{
    WandEntity CreateNewWand();
}

