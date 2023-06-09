using Fiorello.Data;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Text.Json;

namespace Fiorello.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public ShopController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include("Category").Include("Images").Where(m => !m.SoftDelete).Take(4).ToListAsync();
            int count = await _context.Products.Where(m=> !m.SoftDelete).CountAsync();
            ViewBag.count = count;
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Where(m => !m.SoftDelete).Include(m=> m.Images).Skip(skip).Take(4).ToListAsync();
            return PartialView("_ProductsPartial", products);
        }
    }
}
