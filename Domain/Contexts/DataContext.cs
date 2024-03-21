using Domain.Entities.Items.Models;
using Domain.Entities.Weapons.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Contexts;
// Use Add-Migration MigrationName -StartupProject TextAdventure to migrate
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    DbSet<WandEntity> Wand { get; set; }
    DbSet<StaffEntity> Staff { get; set; }
    DbSet<SwordEntity> Sword { get; set; }
    DbSet<AxeEntity> Axe { get; set; }
}
