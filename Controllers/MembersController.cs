using ExpertDirectory.Models;
using ExpertDirectory.Repositories;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpertDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Get all members.
        /// </summary>
        [HttpGet]
        public List<Member> GetMembers()
        {
            return _memberRepository.Get();
        }

        /// <summary>
        /// Get a specific member details.
        /// </summary>
        [HttpGet("{id}")]
        public List<Member> GetMembers(int id)
        {
            return _memberRepository.Get(id);
        }

        /// <summary>
        /// Search a key word for member.
        /// </summary>
        [HttpGet("SearchExpert/{id}/{text}")]
        public List<string> SearchExpert(int id, string text)
        {
            return _memberRepository.SearchExpert(id, text);
        }

        /// <summary>
        /// Add a member, fetch headings from website and store them.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Member>> PostMembers([FromBody] Member member)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://" + member.WebAddress);
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            var h1 = pageDocument.DocumentNode.SelectNodes("(//body//h1)");
            var h2 = pageDocument.DocumentNode.SelectNodes("(//body//h2)");
            var h3 = pageDocument.DocumentNode.SelectNodes("(//body//h3)");

            string HeadingVal;
            var newMemberHeading = new { };

            if (h1 != null)
            {
                HeadingVal = "h1";

                for (int i = 0; i < h1.Count; i++)
                {
                    var headings = new WebHeading();
                    headings.HeadingText = Regex.Replace(h1[i].InnerHtml, "<.*?>", String.Empty);
                    headings.HeadingType = HeadingVal;

                    member.WebHeadings.Add(headings);
                }
            }

            if (h2 != null)
            {
                HeadingVal = "h2";

                for (int i = 0; i < h2.Count; i++)
                {
                    var headings = new WebHeading();
                    headings.HeadingText = Regex.Replace(h2[i].InnerHtml, "<.*?>", String.Empty);
                    headings.HeadingType = HeadingVal;

                    member.WebHeadings.Add(headings);
                }
            }

            if (h3 != null)
            {
                HeadingVal = "h3";

                for (int i = 0; i < h3.Count; i++)
                {
                    var headings = new WebHeading();
                    headings.HeadingText = Regex.Replace(h3[i].InnerHtml, "<.*?>", String.Empty);
                    headings.HeadingType = HeadingVal;

                    member.WebHeadings.Add(headings);
                }
            }
            var newMember = await _memberRepository.Create(member);

            return CreatedAtAction(nameof(GetMembers), new { id = newMember.Id }, newMember);
        }
    }
}
