﻿using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
public class Axe : WeaponBase
{
    public Axe()
    {
        WeaponType = WeaponType.Axe;
    }

    public static implicit operator Axe(AxeEntity entity)
    {
        return new Axe
        {
            Name = entity.Name,
            Rarity = EnumMapper.MapToModel(entity.Rarity),
            Material = EnumMapper.MapToModel(entity.Material),
            WeaponType = EnumMapper.MapToModel(entity.WeaponType),
            ArmorValue = entity.ArmorValue,
            MeleeAttackValue = entity.MeleeAttackValue,
            RangedAttackValue = entity.RangedAttackValue,
            MagicAttackValue = entity.MagicAttackValue,
            IsRanged = entity.IsRanged,
            TwoHanded = entity.TwoHanded,
            Range = entity.Range,
            MagicPower = entity.MagicPower
        };
    }

    public static implicit operator AxeEntity(Axe model)
    {
        return new AxeEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Rarity = EnumMapper.MapToEntity(model.Rarity),
            Material = EnumMapper.MapToEntity(model.Material),
            WeaponType = EnumMapper.MapToEntity(model.WeaponType),
            ArmorValue = model.ArmorValue,
            MeleeAttackValue = model.MeleeAttackValue,
            RangedAttackValue = model.RangedAttackValue,
            MagicAttackValue = model.MagicAttackValue,
            IsRanged = model.IsRanged,
            TwoHanded = model.TwoHanded,
            Range = model.Range,
            MagicPower = model.MagicPower
        };
    }
}
