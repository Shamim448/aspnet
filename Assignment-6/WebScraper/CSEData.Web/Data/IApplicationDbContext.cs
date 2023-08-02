using CSEData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Web.Data
{
    public interface IApplicationDbContext
    {
        public DbSet <Company> Companys { get; set; }
        public DbSet <Price> Prices { get; set; }
    }
}
