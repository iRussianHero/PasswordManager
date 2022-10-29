using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
            ViewBag.Types = new SelectList(_managerDbContext.Types, "Id", "Name");
            return View(_managerDbContext.Items.Include(x => x.Type).ToList());
            //.Items.Include(x => x.Type).ThenInclude() - для множества
        }


        public async Task<IActionResult> Delete(int id)
        {
            await DeleteById(id);
            return View("Index", _managerDbContext.Items.ToList());
        }

        public IActionResult Add()
        {
            ViewBag.Types = new SelectList(_managerDbContext.Types, "Id", "Name");
            return View();
        }

        public IActionResult Find(string name)
        {
            ViewBag.Name = FindByName(name);
            return View();
        }

        public IActionResult Edit(int id)
        {
            Item item = FindById(id);
            var list = _managerDbContext.Types;
            ViewBag.Types = new SelectList(list, "Id", "Name", list.FirstOrDefault(x => x.Id == id).Id); //4-ый оператор означает выбранные элементы
            return View(item);
        }

        public async Task<IActionResult> Update(Item item, int id)
        {
            Item oldItem = FindById(id);
            oldItem.Name = item.Name;
            oldItem.Description = item.Description;
            oldItem.Password = item.Password;
            oldItem.Email = item.Email;
            oldItem.TypeId = item.TypeId;
            oldItem.Login = item.Login;
            _managerDbContext.Update(oldItem);
            await _managerDbContext.SaveChangesAsync();
            TempData["Status"] = "Post was updated";
            return RedirectToAction("Index", _managerDbContext.Items.ToList());
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

        public async Task<IActionResult> DeleteById(int id)
        {
            Item item = FindById(id);

            _managerDbContext.Items.Remove(item);
            await _managerDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public Item FindById(int id)
        {
            Item item = new Item();
            return item = _managerDbContext.Items
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public List<Item> FindByName(string name)
        {
            var users = _managerDbContext.Items.Where(p => EF.Functions.Like(p.Name, name)).ToList();
            return users;
        }
    }
}