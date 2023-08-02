using CSEData.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Web.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //need EF.SqlServer package for working
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Company>().HasData(
            //    new Company { Id = 1, StockCodeName = "test" }
            //    );
            //modelBuilder.Entity<Price>().HasData(
            //   new Price { Id = 1, LTP = 2.7, High = 2.5, Low = 2.3, Open = 5.3, Time = DateTime.Today, }
            //   );
        }
        public DbSet<Company> Companys { get ; set ; }
        public DbSet<Price> Prices { get ; set ; }
    }
}