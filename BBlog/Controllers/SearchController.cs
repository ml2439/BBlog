using BBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly BBlogContext _context;

        public SearchController (BBlogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var blogs = from b in _context.Blog
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(searchString) || s.Body.Contains(searchString));
            }

            var blogCategoryVM = new BlogCategoryViewModel();
            blogCategoryVM.blogs = await blogs.ToListAsync();

            return View(blogCategoryVM);
        }

    }
}
