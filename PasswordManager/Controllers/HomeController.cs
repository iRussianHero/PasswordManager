using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PasswordManager.Models;
using System.Diagnostics;
using Type = PasswordManager.Models.Type;

namespace PasswordManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ManagerDbContext _managerDbContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ManagerDbContext managerDbContext, ILogger<HomeController> logger)
        {
            _managerDbContext = managerDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_managerDbContext.Items.ToList());
        }

        public IActionResult Add()
        {
            ViewBag.Types = new SelectList(_managerDbContext.Types, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> AddNew(Item item)
        {
            _managerDbContext.Items.AddAsync(item);
            await _managerDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}