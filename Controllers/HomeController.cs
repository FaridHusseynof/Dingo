using Dingo.Data;
using Microsoft.AspNetCore.Mvc;

namespace Dingo.Controllers
{
    public class HomeController : Controller
    {
        private DingoDbContext _context { get; }
        public HomeController(DingoDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c=>!c.IsDeleted));
        }
    }
}
