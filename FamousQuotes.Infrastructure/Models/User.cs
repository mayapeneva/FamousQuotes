namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class User : IdentityUser
    {
        private readonly ICollection<Answer> answers;
        public User()
        {
            answers = new HashSet<Answer>();
        }

        public virtual IReadOnlyCollection<Answer> Answers { get; private set; }

        public int Score { get; private set; }

        public bool AddNewQuoteAndAuthor(int quoteId, string author)
        {
            if (answers.Any(a => a.QuoteId == quoteId))
            {
                return false;
            }

            answers.Add(new Answer(quoteId, author));
            Score++;
            return true;
        }
    }
}
