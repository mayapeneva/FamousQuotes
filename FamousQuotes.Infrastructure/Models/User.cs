namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class User : IdentityUser
    {
        private readonly ICollection<QuoteNotAnswered> quotesNotAnswered;
        public User()
        {
            Answers = new HashSet<Answer>();
            quotesNotAnswered = new HashSet<QuoteNotAnswered>();
        }

        public virtual ICollection<Answer> Answers { get; private set; }

        public int Score { get; private set; }

        public virtual IReadOnlyCollection<QuoteNotAnswered> QuotesNotAnswered => quotesNotAnswered.ToList().AsReadOnly();

        public void AddNewQuoteAndAuthor(int quoteId, string author, bool? isAnswerTrue)
        {
            Answers.Add(new Answer(quoteId, author, isAnswerTrue));
            quotesNotAnswered.Remove(new QuoteNotAnswered { QuoteId = quoteId });
            Score++;
        }

        public void AddQuotes(IEnumerable<int> quotesIds)
        {
            quotesIds.ToList().ForEach(q => quotesNotAnswered.Add(new QuoteNotAnswered { QuoteId = q }));
        }
    }
}
