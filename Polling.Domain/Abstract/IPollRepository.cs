using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polling.Domain.Abstract
{
    public interface IPollRepository
    {
        // IQueryable allows a sequence of Poll
        // objects to be obtained
        IQueryable<Poll> Polls { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Vote> Votes { get; }

        // Get poll details of one poll
        Poll Poll(int pollID);
        void AddPoll(Poll poll);

        IDictionary<int, string> AllCategories();

        void AddVote(Vote vote);
        int GetVotes(int pollId, int optionId);

        // Get the tags of a specific poll
        IList<Tag> Tags();

        // List and count of polls by category or default searches
        IEnumerable<Poll> PollsByCategory(string category, int pageNo, int pageSize);
        int TotalPollsByCategory(string category);

        // Methods to allow user to search repository
        IEnumerable<Poll> PollsForSearch(string search, int pageNo, int pageSize);
        int TotalPollsForSearch(string search);
    }
}
