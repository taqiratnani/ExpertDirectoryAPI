using ExpertDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Repositories
{
    public interface IFriendRepository
    {
        List<FriendsDetails> Get();
        List<FriendsDetails> Get(int id);
        Task<Friends> Create(Friends friend);
    }
}
