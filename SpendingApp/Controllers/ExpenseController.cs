using Microsoft.AspNetCore.Mvc;

namespace SpendingApp.Controllers
{
    public class ExpenseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}
