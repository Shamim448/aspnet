﻿using CSEData.Web.Data;
using CSEData.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSEData.Web.Controllers
{
    public class DataController : Controller
    {
        public  ApplicationDbContext _context;
        private  IDataScraper _scraper;
  

        public DataController(ApplicationDbContext context, IDataScraper scraper)
        {
            _context = context;
            _scraper = scraper;    
        }
        public IActionResult Index()
        {
            int dbCompanycount = _context.Companys.Count();
            var company = _scraper.InsertCompany("https://www.cse.com.bd/market/current_price");
            if (company != null && company.Count > dbCompanycount)
            {
                _context.Companys.AddRange(company);
                _context.SaveChanges();
            }


            var result = _scraper.InsertPrice("https://www.cse.com.bd/market/current_price");
            _context.Prices.AddRange(result);
            _context.SaveChanges();
            List<Price> getAllPrice = _context.Prices.ToList();
            return View(getAllPrice);
        }

       

    }
}
