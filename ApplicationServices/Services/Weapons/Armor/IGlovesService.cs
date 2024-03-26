﻿using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public interface IGlovesService
{
    Task<List<Gloves>> GetGloves();
    Task<Gloves> GetGlove(Expression<Func<GlovesEntity, bool>> predicate);
    Task<bool> AddGloves(Gloves gloves);
    Task<bool> UpdateGloves(Gloves gloves);
    Task<bool> DeleteGloves(Guid id);
}

