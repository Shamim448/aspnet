using CSEData.Application.Services;
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

        private IPriceService _priceService;
        //public CompanyCreateModel()
        //{ }
        public PriceCreateModel(IPriceService priceService)
        {
            _priceService = priceService;
        }
        public void CreatePrice()
        {
            _priceService.InsertPrice(CompanyId, LTP, Open, High, Low, Volumn, Time );
        }
    }
}
