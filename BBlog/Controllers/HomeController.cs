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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Blog
                .OrderByDescending(x => x.PostDate).ToListAsync());
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

        [Route("Dev")]
        public async Task<IActionResult> Dev()
        {
            return await Grabber("Dev");
        }

        [Route("Game")]
        public async Task<IActionResult> Game()
        {
            return await Grabber("Game");
        }

        [Route("Recipe")]
        public async Task<IActionResult> Recipe()
        {
            return await Grabber("Recipe");
        }

        [Route("Roam")]
        public async Task<IActionResult> Roam()
        {
            return await Grabber("Roam");
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // helper methods only work for this class
        private async Task<IActionResult> Grabber(string s)
        {
            var Blogs = _context.Blog.Where(b => b.Category == s);
            return View(await Blogs.OrderByDescending(x => x.PostDate).ToListAsync());
        }
    }
}
