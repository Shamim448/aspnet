using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Application.Services
{
    public interface ICompanyService
    {
        public IList<Company> GetAllCompany();
        void InsertCompany(string stockCodeName);
    }
}
