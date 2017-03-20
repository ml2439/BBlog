using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BBlog.Models
{
    public class BBlogContext : DbContext
    {
        public BBlogContext (DbContextOptions<BBlogContext> options)
            : base(options)
        {
        }

        public DbSet<BBlog.Models.Blog> Blog { get; set; }
    }
}
