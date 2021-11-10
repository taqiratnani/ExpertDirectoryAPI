using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Models
{
    public class ExpertDirectoryContext : DbContext
    {
        public ExpertDirectoryContext(DbContextOptions<ExpertDirectoryContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<WebHeading> WebHeadings { get; set; }
        public DbSet<Friends> Friendships {  get; set; }
    }
}
