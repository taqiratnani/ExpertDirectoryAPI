using ExpertDirectory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ExpertDirectoryContext _context;

        public FriendRepository(ExpertDirectoryContext context)
        {
            _context = context;
        }

        public async Task<Friends> Create(Friends friend)
        {
            _context.Friendships.Add(friend);
            await _context.SaveChangesAsync();

            return friend;
        }

        public List<FriendsDetails> Get()
        {
            List<FriendsDetails> list = (from c1 in _context.Members
                                         join c2 in _context.Friendships on c1.Id equals c2.FriendId
                                  select new FriendsDetails
                                  {
                                      MemberId = c2.MemberId,
                                      FriendName = c1.Name,
                                      FriendWebAddress = c1.WebAddress
                                  }).Distinct().ToList();

            return list;
        }

        public List<FriendsDetails> Get(int id)
        {
            List<FriendsDetails> list = (from c1 in _context.Members
                                         join c2 in _context.Friendships on c1.Id equals c2.FriendId
                                         where c1.Id == id
                                         select new FriendsDetails
                                         {
                                             MemberId = c2.MemberId,
                                             FriendName = c1.Name,
                                             FriendWebAddress = c1.WebAddress
                                         }).Distinct().ToList();

            return list;
        }
    }
}
