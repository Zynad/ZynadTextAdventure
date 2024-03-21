using Microsoft.EntityFrameworkCore;

namespace Domain.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

}
