namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class User : IdentityUser
    {
        private readonly ICollection<int> quotesNotAnswered;
        public User()
        {
            Answers = new HashSet<Answer>();
            quotesNotAnswered = new HashSet<int>();
        }

        public virtual ICollection<Answer> Answers { get; private set; }

        public int Score { get; private set; }

        public virtual IReadOnlyCollection<int> QuotesNotAnswered => quotesNotAnswered.ToList().AsReadOnly();

        public void AddNewQuoteAndAuthor(int quoteId, string author)
        {
            Answers.Add(new Answer(quoteId, author));
            quotesNotAnswered.Remove(quoteId);
            Score++;
        }

        public void AddQuotes(IEnumerable<int> quotesIds)
        {
            quotesIds.ToList().ForEach(q => quotesNotAnswered.Add(q));
        }
    }
}
