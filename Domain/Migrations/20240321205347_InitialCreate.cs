using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Axe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequirement = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Durability = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false),
                    ArmorValue = table.Column<int>(type: "int", nullable: false),
                    MeleeAttackValue = table.Column<int>(type: "int", nullable: false),
                    RangedAttackValue = table.Column<int>(type: "int", nullable: false),
                    MagicAttackValue = table.Column<int>(type: "int", nullable: false),
                    IsRanged = table.Column<bool>(type: "bit", nullable: false),
                    TwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    MagicPower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Axe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequirement = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Durability = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false),
                    ArmorValue = table.Column<int>(type: "int", nullable: false),
                    MeleeAttackValue = table.Column<int>(type: "int", nullable: false),
                    RangedAttackValue = table.Column<int>(type: "int", nullable: false),
                    MagicAttackValue = table.Column<int>(type: "int", nullable: false),
                    IsRanged = table.Column<bool>(type: "bit", nullable: false),
                    TwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    MagicPower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sword",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequirement = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Durability = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false),
                    ArmorValue = table.Column<int>(type: "int", nullable: false),
                    MeleeAttackValue = table.Column<int>(type: "int", nullable: false),
                    RangedAttackValue = table.Column<int>(type: "int", nullable: false),
                    MagicAttackValue = table.Column<int>(type: "int", nullable: false),
                    IsRanged = table.Column<bool>(type: "bit", nullable: false),
                    TwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    MagicPower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelRequirement = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Durability = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: false),
                    ArmorValue = table.Column<int>(type: "int", nullable: false),
                    MeleeAttackValue = table.Column<int>(type: "int", nullable: false),
                    RangedAttackValue = table.Column<int>(type: "int", nullable: false),
                    MagicAttackValue = table.Column<int>(type: "int", nullable: false),
                    IsRanged = table.Column<bool>(type: "bit", nullable: false),
                    TwoHanded = table.Column<bool>(type: "bit", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    MagicPower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wand", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Axe");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Sword");

            migrationBuilder.DropTable(
                name: "Wand");
        }
    }
}
