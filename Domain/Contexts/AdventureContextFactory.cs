using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Domain.Contexts;

public class AdventureContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../TextAdventure/Config");
        Console.WriteLine($"Base path: {basePath}");
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = config.GetConnectionString("LaptopDB");
        //var connectionString = config.GetConnectionString("Database1");
        Console.WriteLine($"Connection string: {connectionString}");
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new DataContext(optionsBuilder.Options);
    }
}

