using CSEData.Domain;
using CSEData.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Application.Features.Scrapping.Repositories
{
    public interface ICompanyRepository : IRepository<Company, int>
    {

    }
}
