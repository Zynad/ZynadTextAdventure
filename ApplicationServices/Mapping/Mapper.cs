using ApplicationServices.Characters;
using ApplicationServices.Game;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using Domain.Enums;

namespace ApplicationServices.Mapping;

public static class EnumMapper
{
    public static WeaponMaterial MapToModel(WeaponMaterialEntity entity)
    {
        return (WeaponMaterial)entity;
    }

    public static WeaponMaterialEntity MapToEntity(WeaponMaterial model)
    {
        return (WeaponMaterialEntity)model;
    }

    public static ArmorMaterial MapToModel(ArmorMaterialEntity entity)
    {
        return (ArmorMaterial)entity;
    }

    public static ArmorMaterialEntity MapToEntity(ArmorMaterial model)
    {
        return (ArmorMaterialEntity)model;
    }

    public static Rarity MapToModel(RarityEntity entity)
    {
        return (Rarity)entity;
    }
    
    public static RarityEntity MapToEntity(Rarity model)
    {
        return (RarityEntity)model;
    }
    
    public static WeaponType MapToModel(WeaponTypeEntity entity)
    {
        return (WeaponType)entity;
    }
    
    public static WeaponTypeEntity MapToEntity(WeaponType model)
    {
        return (WeaponTypeEntity)model;
    }

    public static GenderEntity MapToEntity(Gender model)
    {
        return (GenderEntity)model;
    }
    
    public static Gender MapToModel(GenderEntity entity)
    {
        return (Gender)entity;
    }
}

