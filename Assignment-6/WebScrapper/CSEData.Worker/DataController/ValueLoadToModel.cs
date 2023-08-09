using CSEData.Domain;
using CSEData.Worker.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async void Load(string url)
        {
            await _dataGenerate.GetNodsValue(url);
            //get all company from db
            var companyList = await _companyCreate.GetCompany();
            int companyCount = companyList.Count;
            int urldataCount = _dataGenerate.StockCode.Count;
            //insert company 1st time
            if (companyCount <= 0 ) {
                for (int i = 0; i < 5; i++)
                {
                    _companyCreate.StockCodeName = _dataGenerate.StockCode[i].InnerText;
                    _companyCreate.CreateCompany();
                    var st = companyList.Where(company => company.StockCodeName == _dataGenerate.StockCode[i].InnerText).FirstOrDefault();
                    //First Time st was null so insert all company
                    //After that check StockCodeName available or not, if not found StockCodeName then insert StockCodeName
                    if (st == null)
                    {   
                        await Console.Out.WriteLineAsync(i + " " + _dataGenerate.StockCode[i].InnerText);
                    }
                }
                await AddPriceAsync();
            }
            //if new company found insert
            else if (urldataCount > companyCount) 
            {
                for (int i = 0; i < urldataCount; i++)
                {
                    var st = companyList.Where(company => company.StockCodeName == _dataGenerate.StockCode[i].InnerText).FirstOrDefault();
                    //check StockCodeName available or not, if not found StockCodeName then insert StockCodeName
                    if (st == null)
                    {
                        _companyCreate.StockCodeName = _dataGenerate.StockCode[i].InnerText;
                        _companyCreate.CreateCompany();
                        await Console.Out.WriteLineAsync(i + " " + _dataGenerate.StockCode[i].InnerText);
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
            var companyListafterLoad = await _companyCreate.GetCompany();
            if (companyListafterLoad != null)
            {
                //Add price table value
                for (int i = 0; i < _dataGenerate.StockCode.Count; i++)
                {
                    //check StockCodeName available or not, if found StockCodeName then insert price value
                    var st = companyListafterLoad.Where(company => company.StockCodeName == _dataGenerate.StockCode[i].InnerText).FirstOrDefault();
                    if (st != null)
                    {
                        _priceCreate.CompanyId = st.Id;
                        _priceCreate.LTP = _dataGenerate.LTP[i].InnerText;
                        _priceCreate.Open = _dataGenerate.Opens[i].InnerText;
                        _priceCreate.High = _dataGenerate.High[i].InnerText;
                        _priceCreate.Low = _dataGenerate.Low[i].InnerText;
                        _priceCreate.Volumn = _dataGenerate.Volume[i].InnerText;
                        _priceCreate.Time = DateTime.Now;
                        _priceCreate.CreatePrice();
                        //await Console.Out.WriteLineAsync(i + " " + "Price Added");
                    }
                }
            }
        }
    }
}
