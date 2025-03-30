using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.Interface;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        #region ВариантГПТ
        /*
         * private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Головна сторінка адмін-панелі
        [HttpGet("")] // Вказуємо, що це кореневий маршрут "/admin"
        public IActionResult Index()
        {
            return View();
        }

        // ========== ДОДАВАННЯ СТРАВ ========== //
        [HttpGet("add-dish")] // GET: /admin/add-dish
        public IActionResult AddDish()
        {
            return View();
        }

        [HttpPost("add-dish")] // POST: /admin/add-dish
        public async Task<IActionResult> AddDish(MenuItemDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var dish = new MenuItem
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                UrlImageAdress = model.ImageUrl
            };

            _context.MenuItems.Add(dish);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // ========== ДОДАВАННЯ СТОЛИКІВ ========== //
        [HttpGet("add-table")] // GET: /admin/add-table
        public IActionResult AddTable()
        {
            return View();
        }

        [HttpPost("add-table")] // POST: /admin/add-table
        public async Task<IActionResult> AddTable(TableDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var table = new Table
            {
                Id = Guid.NewGuid(),
                Number = model.TableNumber,
                Capacity = model.Capacity
            };

            _context.Tables.Add(table);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
         */
        #endregion

        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("add-dish")]
        public IActionResult AddDish()
        {
            return View();
        }

        [HttpPost("add-dish")]
        public async Task<IActionResult> AddDish(MenuItemDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var menuItem = await adminService.CreateMenuItemAsync(model);
            if (menuItem == null)
                return BadRequest("Menu item not create");

            return RedirectToAction("Index");
        }

        [HttpGet("add-table")]
        public IActionResult AddTable()
        {
            return View();
        }

        [HttpPost("add-table")]
        public async Task<IActionResult> AddTable(TableDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var table = await adminService.CreateTableAsync(model);
            if (table == null)
                return BadRequest("Table not create");

            return RedirectToAction("Index");
        }
    }
}
