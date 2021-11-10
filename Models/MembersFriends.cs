using System.Collections.Generic;

namespace ExpertDirectory.Models
{
    public class MembersFriends
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public WebHeading WebHeadings { get; set; } = new WebHeading();
        //public ICollection<MembersFriends> MembersFriends { get; } = new List<MembersFriends>();
        public Friends Friends { get; set; } = new Friends();

        //public int Id { get; set; }
        //public int MemberId { get; set; }
        //public int FriendId { get; set; }
        //public string WebAddress { get; set; }

        /*public int Id { get; set; }
        public string Name { get; set; }
        public string WebAddress { get; set; }
        public ICollection<WebHeading> WebHeadings { get; set; } = new List<WebHeading>();
        public ICollection<FriendsDetails> FriendsDetails { get; set; } = new List<FriendsDetails>();*/

        //public WebHeading WebHeadings { get; set; }
        //public string FriendName { get; set; }
        //public string FriendWebAddress { get; set; }
    }
}
