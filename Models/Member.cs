using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public ICollection<WebHeading> WebHeadings { get; } = new List<WebHeading>();
        public ICollection<Friends> Friends { get; } = new List<Friends>();
    }
}
