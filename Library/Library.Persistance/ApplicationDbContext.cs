using Crud.Persistance.Features.Membership;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
    ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole,
    ApplicationUserLogin, ApplicationRoleClaim,
    ApplicationUserToken>,IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    (x) => x.MigrationsAssembly(_migrationAssembly) );
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //// Add other entity configurations if needed

            //modelBuilder.Entity<Book>().HasData(new Book
            //{
            //    Id = new Guid("7C69C50D-F8E0-46C5-96BB-9C063501980E"),
            //    Name = "C++",
            //    Type = "kg",
            //    Author = "Shamim",
            //    Price = 100
            //});


        }
        public DbSet<Book> Books { get; set; }
    }
}