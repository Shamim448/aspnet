using Crud.Domain.Entities;
using Crud.Persistance;
using Crud.Persistance.DataSeeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationsAssembly;
        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationAssembly;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, (x) => x.MigrationsAssembly(_migrationsAssembly));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(StudentSeed.Students );
        }
        public DbSet<Student> Students { get; set; }
    }
}