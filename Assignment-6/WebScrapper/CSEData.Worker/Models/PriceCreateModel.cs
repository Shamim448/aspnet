using CSEData.Application.Services;
using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Models
{
    public class PriceCreateModel
    {
        public int CompanyId { get; set; }
        public string LTP { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Volumn { get; set; }
        public DateTime Time { get; set; }
         
        public IList<Price> GetPrices { get; set; }

        private IPriceService _priceService;
        //public CompanyCreateModel()
        //{ }
        public PriceCreateModel(IPriceService priceService)
        {
            _priceService = priceService;
        }
        public async Task CreatePrice()
        {
           await _priceService.InsertPrice(CompanyId, LTP, Open, High, Low, Volumn, Time );
        }
        public async void CreatePriceList()
        {
            _priceService.InsertPriceList(GetPrices);
        }
    }
}
