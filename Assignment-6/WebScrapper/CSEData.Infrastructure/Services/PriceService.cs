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
    public class PriceService:IPriceService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public PriceService(IApplicationUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public void InsertPrice(int companyId, string ltp, string open, string high, string low, string volumn, DateTime time)
        {
            Price price = new Price() { CompanyId = companyId, LTP = ltp, Open = open, High = high, Low = low, Volume = volumn, Time = time };
            _unitOfWork.Prices.Add(price);
            
            _unitOfWork.Save();

        }      
        public void InsertPriceList(IList<Price> getPrices)
        {
            List<Price> priceList = new List<Price>();
            foreach (Price price in getPrices)
            {
                priceList.Add(price);
            }
            _unitOfWork.Prices.AddRange(priceList);
            _unitOfWork.Save();
        }
    }
}
