using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Models
{
    public class WebHeading
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string HeadingText { get; set; }
        public string HeadingType { get; set; }
    }
}
