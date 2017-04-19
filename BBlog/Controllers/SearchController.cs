using BBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BBlog.Controllers
{
    public class SearchController : Controller
    {
        private readonly BBlogContext _context;

        public SearchController (BBlogContext context)
        {
            _context = context;
        }

        [Route("[controller]")]
        public async Task<IActionResult> Index(string searchString)
        {
            var blogs = from b in _context.Blog
                        select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(searchString) || s.Body.Contains(searchString));
            }

            var blogCategoryVM = new BlogSelectViewModel();
            blogCategoryVM.blogs = await blogs.ToListAsync();

            return View(blogCategoryVM);
        }

        [Route("Archive")]
        public async Task<IActionResult> Archive(string monthyear)
        {
            var blogs = from b in _context.Blog
                        select b;

            if (!String.IsNullOrEmpty(monthyear))
            {
                blogs = blogs.Where(s => s.PostDate.ToString("MMM yyyy").Equals(monthyear));
            }

            var blogVM = new BlogSelectViewModel();
            blogVM.blogs = await blogs.ToListAsync();

            return View(blogVM);
        }
    }
}
