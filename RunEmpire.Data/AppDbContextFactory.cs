using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RunEmpire.Data
{
    public class AppDbContextFactory
       : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=RunEmpire;Username=postgres;Password=Darshan@7093");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
