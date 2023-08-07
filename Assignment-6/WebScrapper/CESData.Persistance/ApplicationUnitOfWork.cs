using CSEData.Application;
using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistance
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository Companys { get; private set; }
        public IPriceRepository Prices { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, 
            ICompanyRepository companyRepository, IPriceRepository priceRepository) 
            : base((DbContext)dbContext)
        {
            Companys = companyRepository;
            Prices = priceRepository;
        }
    }
}
