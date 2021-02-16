namespace FamousQuotes.Infrastructure.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;

    public class FamousQuotesUser : IdentityUser
    {
        public FamousQuotesUser()
        {
            Answers = new HashSet<Answer>();
        }

        public virtual ICollection<Answer> Answers { get; private set; }

        public int Score { get; private set; }

        public void AddQuotesToBeAnswered(IEnumerable<int> quoteIds)
        {
            quoteIds.ToList().ForEach(qi => Answers.Add(new Answer(qi, Id)));
        }

        public IEnumerable<int> GetUnasweredQuotes()
        {
            return Answers.Where(a => a.IsAnswered == false).Select(a => a.QuoteId);
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
            var answer = Answers.FirstOrDefault(a => a.QuoteId == quoteId);
            answer.AddAnswerInfo(author, isAnswerTrue);
        }
    }
}
