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
                "Host=dpg-d76jh4adbo4c73blivj0-a.singapore-postgres.render.com; Port=5432; Database=runempire; Username=runempire_user; Password=j3qsgg2dKyDPGAQvyyRiM8a1VCxqowI1; SSL Mode=Require; Trust Server Certificate=true; Pooling=true; Timeout=15; Command Timeout=30;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
