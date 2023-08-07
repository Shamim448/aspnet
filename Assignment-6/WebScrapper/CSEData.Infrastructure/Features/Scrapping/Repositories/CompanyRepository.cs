using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Domain;
using CSEData.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure.Features.Scrapping.Repositories
{
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext dbContext) 
            : base((DbContext)dbContext)
        {
        }
    }
}
