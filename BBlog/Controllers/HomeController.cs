using BBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Blog
        //        .OrderByDescending(x => x.PostDate).ToListAsync());
        //}

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

        [Route("Dev")]
        public async Task<IActionResult> Dev()
        {
            var DevBlogs = _context.Blog.Where(b => b.Category == "Dev");
            return View(await DevBlogs.OrderByDescending(x => x.PostDate).ToListAsync());
        }

        public async Task<IActionResult> Game()
        {
            var DevBlogs = _context.Blog.Where(b => b.Category == "Game");
            return View(await DevBlogs.OrderByDescending(x => x.PostDate).Take(5).ToListAsync());
        }

        public async Task<IActionResult> Recipe()
        {
            var DevBlogs = _context.Blog.Where(b => b.Category == "Recipe");
            return View(await DevBlogs.OrderByDescending(x => x.PostDate).Take(5).ToListAsync());
        }

        public async Task<IActionResult> Roam()
        {
            var DevBlogs = _context.Blog.Where(b => b.Category == "Roam");
            return View(await DevBlogs.OrderByDescending(x => x.PostDate).Take(5).ToListAsync());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
