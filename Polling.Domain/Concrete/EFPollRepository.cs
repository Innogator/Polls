using Polling.Domain.Abstract;
using Polling.Domain.Context;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Concrete
{
    public class EFPollRepository : IPollRepository
    {
        private PollContext context = new PollContext();

        public IQueryable<Poll> Polls
        {
            get { return context.Polls; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IQueryable<Vote> Votes
        {
            get { return context.Votes; }
        }

        public Poll Poll(int pollID)
        {
            Poll poll = context
                        .Polls
                        .Where(p => p.PollID == pollID)
                        .FirstOrDefault();
            return poll;
        }

        // Add a poll to the database
        public void AddPoll(Poll poll)
        {
            context.Polls.Add(poll);
            context.SaveChanges();
        }

        public void AddVote(Vote vote)
        {
            context.Votes.Add(vote);
            context.SaveChanges();
        }

        public int GetVotes(int pollId, int optionId)
        {
            return context.Votes
                        .Where(x => x.PollID == pollId)
                        .Where(x => x.ChoiceID == optionId)
                        .Count();
        }

        public IDictionary<int, string> AllCategories()
        {
            var query = context
                .Categories
                .Select(x => new { x.CategoryID, x.Name })
                .Distinct()
                .OrderBy(x => x)
                .ToDictionary(o => o.CategoryID, o => o.Name);

            return query;
        }

        public IList<Tag> Tags()
        {
            return context.Tags.ToList();

            //var tags = context.Tags.Where(x => );        



            //    context
            //            .PollTags
            //            .Join(context.PollTags,
            //                fk => fk.PollID,
            //                pk => pk.PollID,
            //                (fk, pk) => new { PollTag = fk, Poll = pk })
            //            .Select(x => x.PollTag)
            //            .Where(y => y.PollID == pollID).ToList();

            //// now we have the list of tags for the poll, 
            //// join to the tag table to get the name of the tag
            //var query2 = context
            //            .Tags
            //            .Where(p => p.TagID in query)
        }

        public IEnumerable<Poll> PollsByCategory(string category, int pageNo, int pageSize)
        {
            var query = context
                        .Polls
                        .Where(p => category == null || String.Equals(p.Category.Name, category))
                        .OrderBy(p => p.PollQuestion)
                        .Skip((pageNo) * pageSize)
                        .Take(pageSize);
            return query;
        }

        public int TotalPollsByCategory(string text)
        {
            int count;

            count = text == null ?
                            context.Polls.Count() :
                            context.Polls.Where(e => String.Equals(e.Category.Name, text)).Count();
            return count;
        }

        public IEnumerable<Poll> PollsForSearch(string search, int pageNo, int pageSize)
        {
            var query = context.Polls
                                .Where(p => p.PollQuestion.Contains(search)
                                    || p.Category.Name.Contains(search))
                                .OrderByDescending(p => p.PollQuestion)
                                .Skip(pageNo * pageSize)
                                .Take(pageSize);
            return query;
        }

        public int TotalPollsForSearch(string search)
        {
            return context.Polls
                    .Where(p => p.PollQuestion.Contains(search) || p.Category.Name.Contains(search))
                    .Count();
        }
    }
}
