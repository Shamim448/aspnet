using CSEData.Web.Data;
using CSEData.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSEData.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Price> getAllPrice = _context.Prices.ToList();
            return View(getAllPrice);
        }
    }
}
