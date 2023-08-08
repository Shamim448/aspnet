using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Application.Services
{
    public interface IPriceService
    {
        void InsertPrice(int companyId, string ltp, string open, string high, string low, string volumn, DateTime time);
        void InsertPriceList(IList<Price> getPrices);
    }
}
