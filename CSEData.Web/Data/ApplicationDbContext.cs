using CSEData.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Company>().HasData(
            //    new Company { Id = 1, StockCodeName = "1JANATAMF" }
            //    );
            //modelBuilder.Entity<Price>().HasData(
            //   new Price { Id = 1, CompanyID = 1, LTP = "6.0", Open = "6.3", High = "2.5", Low = "2.5", Volume="6534",  Time = DateTime.Today, }
            //   );
        }

        public DbSet<Price> Prices { get; set; }
        public DbSet<Company> Companys { get; set; }
    }
}
