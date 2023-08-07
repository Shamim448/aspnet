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
        public ValueLoadToModel(DataGenerateModel dataGenerate, CompanyCreateModel companyCreate)
        {
            _dataGenerate = dataGenerate;
            _companyCreate = companyCreate;

        }
        public void Load(string url)
        {
            //HtmlDocument doc = _dataGenerate.GetDocument(url);
            var listOfCompany = _dataGenerate.GetNodsValue(url);
            for (int i = 0; i < listOfCompany.Count; i++)
            {
                _companyCreate.StockCodeName = listOfCompany[i].StockCodeName;
                _companyCreate.CreateCompany();
            }
        }
        
    }
}
