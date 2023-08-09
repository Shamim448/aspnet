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
    public class WebScraperService
    {
        public string StockCodeName { get; set; }

        public int CompanyId { get; set; }
        public string LTP { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Volumn { get; set; }
        public DateTime Time { get; set; }


        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly NodeGenaratorService _nodes;
        public WebScraperService(IApplicationUnitOfWork unitOfWork, NodeGenaratorService nodes)
        {
            _unitOfWork = unitOfWork;
            _nodes = nodes;
        }


        public async Task LoadAsunc(string url)
        {
            await _nodes.GetNodsValue(url);
            //get all company from db
            var companyList = await GetAllCompanysAsync();
            int companyCount = companyList.Count;
            int urldataCount = _nodes.StockCode.Count;
            //insert company 1st time
            if (companyCount <= 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    StockCodeName = _nodes.StockCode[i].InnerText;
                    //CreateCompany();
                    InsertCompanyAsync(StockCodeName);
                    var st = companyList.Where(company => company.StockCodeName == _nodes.StockCode[i].InnerText).FirstOrDefault();
                    //First Time st was null so insert all company
                    //After that check StockCodeName available or not, if not found StockCodeName then insert StockCodeName
                    if (st == null)
                    {
                        await Console.Out.WriteLineAsync(i + " " + _nodes.StockCode[i].InnerText);
                    }
                }
                await AddPriceAsync();
            }
            //if new company found insert
            else if (urldataCount > companyCount)
            {
                for (int i = 0; i < urldataCount; i++)
                {
                    var st = companyList.Where(company => company.StockCodeName == _nodes.StockCode[i].InnerText).FirstOrDefault();
                    //check StockCodeName available or not, if not found StockCodeName then insert StockCodeName
                    if (st == null)
                    {
                        StockCodeName = _nodes.StockCode[i].InnerText;
                        //CreateCompany();
                        InsertCompanyAsync(StockCodeName);
                        await Console.Out.WriteLineAsync(i + " " + _nodes.StockCode[i].InnerText);
                    }
                }
                await AddPriceAsync();
            }
            else
            {
                await AddPriceAsync();
            }
        }





        //Price Add to db
        private async Task AddPriceAsync()
        {
            var companyListafterLoad = await GetAllCompanysAsync();
            if (companyListafterLoad != null)
            {
                //Add price table value
                for (int i = 0; i < _nodes.StockCode.Count; i++)
                {
                    //check StockCodeName available or not, if found StockCodeName then insert price value
                    var st = companyListafterLoad.Where(company => company.StockCodeName == _nodes.StockCode[i].InnerText).FirstOrDefault();
                    if (st != null)
                    {
                        CompanyId = st.Id;
                        LTP = _nodes.LTP[i].InnerText;
                        Open = _nodes.Opens[i].InnerText;
                        High = _nodes.High[i].InnerText;
                        Low = _nodes.Low[i].InnerText;
                        Volumn = _nodes.Volume[i].InnerText;
                        Time = DateTime.Now;
                       await InsertPriceAsync(CompanyId, LTP, Open, High, Low, Volumn, Time);
                        //await Console.Out.WriteLineAsync(i + " " + "Price Added");
                    }
                }
            }
        }

        //public async Task<IList<Company>> GetAllCompany()
        //{
        //    return await _unitOfWork.Companys.GetAllAsync();
        //}
        private async Task<IList<Company>> GetAllCompanysAsync()
        {
            return await _unitOfWork.Companys.GetAllAsync();
        }

        //Company Handle
        private async Task InsertCompanyAsync(string stockCodeName)
        {
            Company company = new Company() { StockCodeName = stockCodeName };
            // _unitOfWork.Companys.Add(company);
            await _unitOfWork.Companys.AddAsync(company);
            _unitOfWork.Save();

        }
        //public async Task CreateCompany()
        //{
        //    await InsertCompany(StockCodeName);
        //}
        
        //Price handel
        private async Task InsertPriceAsync(int companyId, string ltp, string open, string high, string low, string volumn, DateTime time)
        {
            Price price = new Price() { CompanyId = companyId, LTP = ltp, Open = open, High = high, Low = low, Volume = volumn, Time = time };       
            await _unitOfWork.Prices.AddAsync(price);
            //await _unitOfWork.SaveAsync();
            _unitOfWork.Save();
        }
        //public async Task CreatePrice()
        //{
        //    await InsertPrice(CompanyId, LTP, Open, High, Low, Volumn, Time);
        //}
    }
}
