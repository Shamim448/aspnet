using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Domain;
using Microsoft.EntityFrameworkCore;


namespace CSEData.Persistance.Features.Scrapping.Repositories
{
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext dbContext) 
            : base((DbContext)dbContext)
        {
        }
    }
}
