using CSEData.Application.Services;
using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Models
{
    public class CompanyCreateModel
    {
        public string StockCodeName { get; set; }

        private ICompanyService _companyService;
        //public CompanyCreateModel()
        //{ }
        public CompanyCreateModel(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task CreateCompany()
        {
           await _companyService.InsertCompany(StockCodeName);
        }
        public async Task <IList<Company>> GetCompany()
        {
            return await _companyService.GetAllCompany();
        }
    }
}
