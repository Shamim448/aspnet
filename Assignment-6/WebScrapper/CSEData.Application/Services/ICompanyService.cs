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
        public Task<IList<Company>> GetAllCompany();
        Task InsertCompany(string stockCodeName);
    }
}
