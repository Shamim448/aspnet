using Crud.Domain.Entities;
using Crud.Persistance.DataSeeding;
using Crud.Persistance.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>, IApplicationDbContext
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(UserSeed.Users);
           // modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            // Add other entity configurations if needed
            modelBuilder.Entity<Course>().HasData(
                new Course() { Id = new Guid("D7B073A0-9E86-4AFF-B611-44BA4198EB4E"), Name="Asp.Net", Fees=30000 },
                new Course() { Id = new Guid("0C6C3539-F545-4E80-9B88-076F864177CB"), Name="C#", Fees=9000 }
                );
            //Primary key for pi board table using composite key
            modelBuilder.Entity<UserCourse>().HasKey((x) => new {x.UserId, x.CourseId});

            //one course many course
            modelBuilder.Entity<UserCourse>()
                .HasOne(c => c.Course)
                .WithMany(s => s.Users)
                .HasForeignKey(c => c.CourseId);

            //one users many course
            modelBuilder.Entity<UserCourse>()
                .HasOne(s => s.User)
                .WithMany(c => c.Courses)
                .HasForeignKey(s => s.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

    }
}