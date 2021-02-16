namespace FamousQuotes.Services
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;
    using System;
    using System.Linq;

    public class GameService : BaseService, IGameService
    {
        public GameService(FamousQuotesDbContext dbContext)
            : base(dbContext)
        {
        }

        public QuoteDto GetRandomUnansweredQuote(FamousQuotesUser user, bool isBinaryMode)
        {
            if (user.Answers is null || user.Answers.Count == 0)
            {
                var quoteIds = DbContext.Quotes.Select(q => q.Id);
                user.AddQuotesToBeAnswered(quoteIds.AsEnumerable());

                DbContext.FamousQuotesUsers.Update(user);
                DbContext.SaveChanges();
            }

            var unansweredQuoteIds = user.GetUnasweredQuotes();
            var unansweredQuoteIdsCount = unansweredQuoteIds.Count();

            var randomQuote = new QuoteDto();
            var random = new Random();
            var randomQuoteIndex = random.Next(unansweredQuoteIdsCount - 1);
            var quoteId = unansweredQuoteIds.ToArray()[randomQuoteIndex];

            var quote = DbContext.Quotes.FirstOrDefault(q => q.Id == quoteId);
            var correctAuthor = quote.Author;
            randomQuote.CorrectAuthorName = correctAuthor.Name;

            var authors = DbContext.Authors;
            var authorsCount = authors.Count();
            var randomAuthorOneIndex = random.Next(authorsCount);
            var authorOne = authors.ToArray()[randomAuthorOneIndex - 1];

            randomQuote.QuoteId = quote.Id;
            randomQuote.QuoteText = quote.Text;
            randomQuote.AuthorOneName = authorOne.Name;

            if (isBinaryMode)
            {
                return randomQuote;
            }

            var randomAuthorTwoIndex = random.Next(authorsCount);
            var authorTwo = authors.ToArray()[randomAuthorTwoIndex - 1];
            randomQuote.AuthorTwoName = authorTwo.Name;

            var randomAuthorThreeIndex = random.Next(authorsCount);
            var authorThree = authors.ToArray()[randomAuthorThreeIndex - 1];
            randomQuote.AuthorThreeName = authorThree.Name;

            return randomQuote;
        }

        public ResultDto SaveAnswer(FamousQuotesUser user, AnswerDto answer, bool isBinaryMode)
        {
            var result = new ResultDto
            {
                AuthorName = answer.AuthorName
            };

            var quoteId = answer.QuoteId;
            var correctAuthorName = answer.CorrectAuthorName;
            if (isBinaryMode)
            {
                var isAnswerTrue = answer.IsAnswerTrue != null ? answer.IsAnswerTrue : false;
                if ((answer.AuthorName == correctAuthorName && isAnswerTrue == true)
                    || (answer.AuthorName != correctAuthorName && isAnswerTrue == false))
                {
                    user.AddCorrectAnswer(quoteId, answer.AuthorName, isAnswerTrue.Value);
                    result.AuthorName = answer.AuthorName;
                    result.IsAnswerTrue = true;

                    DbContext.FamousQuotesUsers.Update(user);
                    DbContext.SaveChanges();

                    return result;
                }

                user.AddWrongAnswer(quoteId, answer.AuthorName, isAnswerTrue.Value);
                result.AuthorName = correctAuthorName;
                result.IsAnswerTrue = false;
                return result;
            }

            if (answer.AuthorName == correctAuthorName)
            {
                user.AddCorrectAnswer(quoteId, answer.AuthorName, null);
                result.AuthorName = answer.AuthorName;
                result.IsAnswerTrue = true;

                DbContext.FamousQuotesUsers.Update(user);
                DbContext.SaveChanges();

                return result;
            }

            user.AddWrongAnswer(quoteId, answer.AuthorName, null);
            result.AuthorName = correctAuthorName;
            result.IsAnswerTrue = false;
            return result;
        }
    }
}
