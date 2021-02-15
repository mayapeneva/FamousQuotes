namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class User : IdentityUser
    {
        private ICollection<Answer> answers;

        public User()
        {
            answers = new HashSet<Answer>();
        }

        public virtual ICollection<Answer> Answers => answers.ToList().AsReadOnly();

        public int Score { get; private set; }

        public void AddQuotesToBeAnswered(IEnumerable<int> quoteIds)
        {
            quoteIds.ToList().ForEach(qi => answers.Add(new Answer(qi, Id)));
        }

        public IEnumerable<int> GetUnasweredQuotes()
        {
            return answers.Where(a => a.IsAnswered == false).Select(a => a.QuoteId);
        }

        public void AddCorrectAnswer(int quoteId, string author, bool? isAnswerTrue)
        {
            AddAnswer(quoteId, author, isAnswerTrue);
            Score++;
        }
        public void AddWrongAnswer(int quoteId, string author, bool? isAnswerTrue)
        {
            AddAnswer(quoteId, author, isAnswerTrue);
        }

        private void AddAnswer(int quoteId, string author, bool? isAnswerTrue)
        {
            var answer = answers.FirstOrDefault(a => a.QuoteId == quoteId);
            answer.AddAnswerInfo(author, isAnswerTrue);
        }
    }
}
