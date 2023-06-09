using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public HomeController(AppDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Product> products = await _productService.GetAllAsync();

            About about = await _context.About.Include("Lists").Where(m => !m.SoftDelete).FirstOrDefaultAsync();

            IEnumerable<Expert> experts = await _context.Experts.Where(m => !m.SoftDelete).ToListAsync();

            BlogHeader blogHeader = await _context.BlogHeader.FirstOrDefaultAsync();

            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Comment> comments = await _context.Comments.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Instagram> instagram = await _context.Instagram.Where(m => !m.SoftDelete).ToListAsync();

            HomeVM model = new() 
            { 
                Categories = categories,
                Products  = products,
                About = about,
                Experts = experts,
                BlogHeader = blogHeader,
                Blogs = blogs,
                Comments = comments,
                Instagram = instagram
            };

            return View(model);
        }
    }
}