using CSEData.Application.Features.Scrapping.Repositories;
using CSEData.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Application
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        ICompanyRepository Companys { get; }
        IPriceRepository Prices { get; }
    }
}
