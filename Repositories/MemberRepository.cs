using ExpertDirectory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertDirectory.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ExpertDirectoryContext _context;

        public MemberRepository(ExpertDirectoryContext context)
        {
            _context = context;
        }

        public async Task<Member> Create(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public List<Member> Get()
        {
            return _context.Members.Include(p => p.WebHeadings).Include(q => q.Friends).ToList();
        }

        public List<Member> Get(int id)
        {
            List<Member> list = _context.Members.Where(a => a.Id == id).Include(p => p.WebHeadings).Include(q => q.Friends).ToList();

            return list;
        }

        public List<string> SearchExpert(int id, string text)
        {
            List<int> connection = new List<int>();
            List<ConnectedMembers> connectionNames = new List<ConnectedMembers>();
            List<string> connectionsList = new List<string>();

            // Check if searched key word is present in any member's headings
            List<ConnectedMembers> list = (from c1 in _context.Members
                                join c2 in _context.WebHeadings on c1.Id equals c2.MemberId
                                 where c2.HeadingText.Contains(text)
                                 && c1.Id != id
                                select new ConnectedMembers
                                {
                                    Id = c1.Id,
                                    Name = c1.Name
                                }).ToList();

            if(list.Count > 0)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    connection.Clear();
                    connection.Add(id);

                    // If searched heading is present in member's direct friend
                    List<ConnectedMembers> checkList = (from c1 in _context.Friendships
                                                        where c1.MemberId == id && c1.FriendId == list[j].Id
                                                        select new ConnectedMembers
                                                        {
                                                            Id = c1.Id
                                                        }).ToList();

                    List<ConnectedMembers> finalList = new List<ConnectedMembers>();

                    if (checkList.Count == 0)
                    {
                        // If searched heading is present in member's friend of friend
                        finalList = (from c1 in _context.Friendships
                                     join c2 in _context.Friendships on c1.MemberId equals list[j].Id
                                     join c3 in _context.Members on c2.MemberId equals c3.Id
                                     where c1.MemberId == c2.MemberId
                                     select new ConnectedMembers
                                     {
                                         Id = c1.Id,
                                         Name = c3.Name
                                     }).ToList();

                        if (finalList.Count > 0)
                        {
                            connection.Add(finalList[0].Id);
                        }
                        else
                        {
                            connection.Clear();
                            continue;
                        }

                        connection.Add(list[j].Id);
                    }
                    else
                    {
                        connection.Add(list[j].Id);
                    }

                    // Convert find members to format
                    // Member1 -> Member2
                    // or
                    // Member1 -> Member2 -> Member3
                    if (connection.Count > 1)
                    {
                        string map = string.Empty;

                        for (int i = 0; i < connection.Count; i++)
                        {
                            connectionNames = (from c1 in _context.Members
                                               where c1.Id == connection[i]
                                               select new ConnectedMembers
                                               {
                                                   Name = c1.Name
                                               }).ToList();

                            if (i > 0)
                            {
                                map += " -> ";
                                map += connectionNames[0].Name;
                            }
                            else
                            {
                                map += connectionNames[0].Name;
                            }
                        }

                        connectionsList.Add(map);
                    }
                }
            }

            return connectionsList;
        }
    }
}
