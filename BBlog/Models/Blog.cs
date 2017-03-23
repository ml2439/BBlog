using System;
using System.ComponentModel.DataAnnotations;

namespace BBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        public string Tags { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "Post Date")]
        [DataType(DataType.DateTime)] 
        public DateTime PostDate { get; set; }
    }
}
