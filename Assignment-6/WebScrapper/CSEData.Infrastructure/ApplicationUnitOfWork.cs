using CSEData.Application;
using CSEData.Application.Features.Scrapping.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure
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
