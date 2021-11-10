using ExpertDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Repositories
{
    public interface IMemberRepository
    {
        List<Member> Get();
        List<Member> Get(int id);
        List<string> SearchExpert(int id, string text);
        Task<Member> Create(Member member);
    }
}
