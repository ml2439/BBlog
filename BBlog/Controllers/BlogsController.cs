using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBlog.Models;
using Microsoft.AspNetCore.Authorization;

namespace BBlog.Controllers
{
    //[Authorize]
    public class BlogsController : Controller
    {
        private readonly BBlogContext _context;

        public BlogsController(BBlogContext context)
        {
            _context = context;  
        }

        // GET: Blogs
        // can search blog by one keyword or category
        public async Task<IActionResult> Index(string blogCategory, string searchString)
        {
            // Use LINQ to get a list of categories that exist in the database
            IQueryable<string> categoryQuery = from b in _context.Blog
                                               orderby b.Category
                                               select b.Category;

            var blogs = from b in _context.Blog
                        select b;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(searchString)||s.Body.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(blogCategory))
            {
                blogs = blogs.Where(s => s.Category == blogCategory);
            }

            var blogCategoryVM = new BlogCategoryViewModel();
            blogCategoryVM.categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            blogCategoryVM.blogs = await blogs.ToListAsync();

            return View(blogCategoryVM);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Blogs/Create
        [HttpGet("[controller]/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Blogs/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[controller]/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            blog.PostDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Blogs", new { id = blog.Id });
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            blog.PostDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Blogs", new { id = blog.Id }); ;
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.SingleOrDefaultAsync(m => m.Id == id);
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
