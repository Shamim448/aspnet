using CSEData.Domain;
using CSEData.Worker.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.DataController
{

    public class ValueLoadToModel
    {
        private readonly DataGenerateModel _dataGenerate;
        private readonly CompanyCreateModel _companyCreate;
        private readonly PriceCreateModel _priceCreate;
        public ValueLoadToModel(DataGenerateModel dataGenerate, CompanyCreateModel companyCreate, PriceCreateModel priceCreate)
        {
            _dataGenerate = dataGenerate;
            _companyCreate = companyCreate;
            _priceCreate = priceCreate;

        }
        public void Load(string url)
        {
            _dataGenerate.GetNodsValue(url);
            var companyList = _companyCreate.GetCompany();          
            
                for (int i = 0; i < _dataGenerate.StockCode.Count; i++)
                {
                    if (companyList.Any(c => c.StockCodeName == _dataGenerate.StockCode[i].InnerText))
                    {
                        _companyCreate.StockCodeName = _dataGenerate.StockCode[i].InnerText;
                        _companyCreate.CreateCompany();
                    }
                   
                }
            
            
            
            //for (int i = 0; i < _dataGenerate.StockCode.Count; i++)
            //{
            //    if (_dataGenerate.StockCode[i] != null) { }
            //    _priceCreate.CompanyId = i;
            //    _priceCreate.LTP = _dataGenerate.LTP[i].InnerText;
            //    _priceCreate.Open = _dataGenerate.Opens[i].InnerText;
            //    _priceCreate.High = _dataGenerate.High[i].InnerText;
            //    _priceCreate.Low = _dataGenerate.Low[i].InnerText;
            //    _priceCreate.Volumn = _dataGenerate.Volume[i].InnerText;
            //    _priceCreate.Time = DateTime.Now;
            //    _priceCreate.CreatePrice();
            //}
            //HtmlDocument doc = _dataGenerate.GetDocument(url);
            //used when GetNodsValuereturn a list
            // var listOfCompany = _dataGenerate.GetNodsValue(url);

            //we can used it when GetNodsValue return a list
            //for (int i = 0; i < listOfCompany.Count; i++)
            //{
            //    _companyCreate.StockCodeName = listOfCompany[i].StockCodeName;

            //    _companyCreate.CreateCompany();

            //    _priceCreate.CompanyId = 1;
            //    //need to add price property value
            //}
        }

    }
}
