namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class User : IdentityUser
    {
        private readonly ICollection<Answer> answers;
        private readonly ICollection<int> quotesNotAnswered;
        public User()
        {
            answers = new HashSet<Answer>();
            quotesNotAnswered = new HashSet<int>();
        }

        public virtual IReadOnlyCollection<Answer> Answers => answers.ToList().AsReadOnly();

        public int Score { get; private set; }

        public virtual IReadOnlyCollection<int> QuotesNotAnswered => quotesNotAnswered.ToList().AsReadOnly();

        public void AddNewQuoteAndAuthor(int quoteId, string author)
        {
            answers.Add(new Answer(quoteId, author));
            quotesNotAnswered.Remove(quoteId);
            Score++;
        }
    }
}
