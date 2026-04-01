using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RunEmpire.Entities;

namespace RunEmpire.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Run> Runs { get; set; }

        public DbSet<RunPoint> RunPoints { get; set; }

        public DbSet<Territory> Territories { get; set; }

        public DbSet<TerritoryPoint> TerritoryPoints { get; set; }
    }
}
