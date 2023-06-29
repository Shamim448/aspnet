using Microsoft.AspNetCore.Mvc;

namespace Crud.web.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Roles()
        {
            return View();
        }
    }
}
