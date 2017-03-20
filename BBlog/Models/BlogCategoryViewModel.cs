using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BBlog.Models
{
    public class BlogCategoryViewModel
    {
        public List<Blog> blogs;
        public SelectList categories;

        // contains the selected category
        public string blogCategory { get; set; }
    }
}
