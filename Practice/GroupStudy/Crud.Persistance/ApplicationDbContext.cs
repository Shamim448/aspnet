using Crud.Domain.Entities;
using Crud.Persistance.DataSeeding;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>,
        IApplicationDbContext
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
            //modelBuilder.Entity<User>().HasData(UserSeed.Users);
            //modelBuilder.Entity<ApplicationUserLogin>().HasNoKey();
            //modelBuilder.Entity<ApplicationUserRole>().HasNoKey();
            //modelBuilder.Entity<ApplicationUserToken>().HasNoKey();
            //modelBuilder.Entity<ApplicationUser>().HasNoKey();
            //// Add foreign key relationship between User and ApplicationUser
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.ApplicationUser)
            //    .WithOne()
            //    .HasForeignKey<ApplicationUser>(au => au.Id)
            //    .IsRequired();

        }
        
        public DbSet<User> Users { get; set; }

    }
}