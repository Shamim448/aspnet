using CSEData.Application;
using CSEData.Application.Services;
using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public CompanyService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public async Task<IList<Company>> GetAllCompany()
        {
           return await _unitOfWork.Companys.GetAllAsync();
        }

        public async Task InsertCompany(string stockCodeName)
        {
            Company company = new Company() { StockCodeName = stockCodeName};
           // _unitOfWork.Companys.Add(company);
            await _unitOfWork.Companys.AddAsync(company);
            _unitOfWork.Save();

        }
    }
}
