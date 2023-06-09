using Fiorello.Data;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _context;

        public BlogDetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Blog blog = await _context.Blogs.Where(m=> !m.SoftDelete).FirstOrDefaultAsync(m=> m.Id == id);

            if (blog is null) return NotFound();

            return View(blog);
        }
    }
}
