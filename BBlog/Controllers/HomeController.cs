using BBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BBlogContext _context;

        public HomeController(BBlogContext context)
        {
            _context = context;
        }

        // Home page
        public IActionResult Index()
        {
            return View();
        }

        [Route("Blog")]
        public async Task<IActionResult> Blog(string blogCategory)
        {
            // All blogs
            var blogs = from b in _context.Blog
                        select b;

            if (!String.IsNullOrEmpty(blogCategory))
            {
                blogs = blogs.Where(s => s.Category == blogCategory);
            }

            return View(await blogs.OrderByDescending(x => x.PostDate).ToListAsync());
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("{category}/{id}")]
        public async Task<IActionResult> Post(string category, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
