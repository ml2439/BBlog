using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BBlog.Models
{
    public class BlogSelectViewModel
    {
        public List<Blog> blogs;
        public SelectList categories;

        // the selected category
        public string blogCategory { get; set; }
    }
}
