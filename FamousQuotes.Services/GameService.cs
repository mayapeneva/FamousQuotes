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

        public QuoteDto GetRandomUnansweredQuote(User user, bool isBinaryMode)
        {
            var unansweredQuotes = user.QuotesNotAnswered;
            var unansweredQuotesCount = unansweredQuotes.Count;

            if (unansweredQuotesCount == 0)
            {
                var quotesIds = DbContext.Quotes.Select(q => q.Id);
                user.AddQuotes(quotesIds.AsEnumerable());

                DbContext.Users.Update(user);
                DbContext.SaveChanges();

                unansweredQuotes = user.QuotesNotAnswered;
                unansweredQuotesCount = unansweredQuotes.Count;
            }            

            var randomQuote = new QuoteDto();
            //if (unansweredQuotesCount == 0)
            //{
            //    randomQuote.Score = user.Score;
            //    return randomQuote;
            //}

            var random = new Random();
            var randomQuoteIndex = random.Next(unansweredQuotesCount - 1);

            var quoteId = unansweredQuotes.ToArray()[randomQuoteIndex].QuoteId;
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
            var authorTwo = authors.ToArray()[randomAuthorOneIndex - 1];
            randomQuote.AuthorTwoName = authorTwo.Name;

            var randomAuthorThreeIndex = random.Next(authorsCount);
            var authorThree = authors.ToArray()[randomAuthorOneIndex - 1];
            randomQuote.AuthorThreeName = authorThree.Name;

            return randomQuote;
        }

        public ResultDto SaveAnswer(User user, AnswerDto answer, bool isBinaryMode)
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
                    user.AddNewQuoteAndAuthor(quoteId, answer.AuthorName, isAnswerTrue.Value);
                    result.AuthorName = answer.AuthorName;
                    result.IsAnswerTrue = true;

                    DbContext.Users.Update(user);
                    DbContext.SaveChanges();

                    return result;
                }

                result.AuthorName = correctAuthorName;
                result.IsAnswerTrue = false;
                return result;
            }

            if (answer.AuthorName == correctAuthorName)
            {
                user.AddNewQuoteAndAuthor(quoteId, answer.AuthorName, null);
                result.AuthorName = answer.AuthorName;
                result.IsAnswerTrue = true;

                DbContext.Users.Update(user);
                DbContext.SaveChanges();

                return result;
            }

            result.AuthorName = correctAuthorName;
            result.IsAnswerTrue = false;
            return result;
        }
    }
}
