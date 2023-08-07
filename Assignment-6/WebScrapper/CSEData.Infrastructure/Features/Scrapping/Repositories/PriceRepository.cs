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
    public class PriceRepository : Repository<Price, int>, IPriceRepository
    {
        public PriceRepository(IApplicationDbContext dbContext) 
            : base((DbContext)dbContext)
        {
        }
    }
}
