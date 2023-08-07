using CSEData.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure
{
    public interface IApplicationDbContext
    {
        public DbSet<Company> Companys { get; }
        public DbSet<Price> Prices { get; }
    }
}
