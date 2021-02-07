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

        public RandomQuoteDto GetRandomUnansweredQuote(User user, bool isBinaryMode)
        {
            var unansweredQuotes = user.QuotesNotAnswered;
            var unansweredQuotesCount = unansweredQuotes.Count;

            if (user.Score == 0 && unansweredQuotesCount == 0)
            {
                var quotesIds = DbContext.Quotes.Select(q => q.Id);
                user.AddQuotes(quotesIds.AsEnumerable());

                DbContext.Users.Update(user);
                DbContext.SaveChanges();

                unansweredQuotes = user.QuotesNotAnswered;
                unansweredQuotesCount = unansweredQuotes.Count;
            }            

            var randomQuote = new RandomQuoteDto();
            if (unansweredQuotesCount == 0)
            {
                randomQuote.Score = user.Score;
                return randomQuote;
            }

            var random = new Random();
            var randomQuoteIndex = random.Next(unansweredQuotesCount - 1);

            var quoteId = unansweredQuotes.ToArray()[randomQuoteIndex];
            var quote = DbContext.Quotes.FirstOrDefault(q => q.Id == quoteId);

            var correctAuthor = quote.Author;
            var authors = DbContext.Authors;
            var authorsCount = authors.Count();

            var randomAuthorOneIndex = random.Next(authorsCount);
            var authorOne = authors.ToArray()[randomAuthorOneIndex - 1];

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

            var quote = DbContext.Quotes.FirstOrDefault(q => q.Text == answer.QuoteText);
            var rightAuthorName = quote.Author.Name;
            if (isBinaryMode)
            {
                var isAnswerTrue = answer.IsAnswerTrue != null ? answer.IsAnswerTrue : false;
                if (answer.AuthorName == rightAuthorName && isAnswerTrue == true)
                {
                    user.AddNewQuoteAndAuthor(quote.Id, answer.AuthorName);
                    result.AuthorName = answer.AuthorName;
                    result.IsAnswerTrue = true;

                    DbContext.Users.Update(user);
                    DbContext.SaveChanges();

                    return result;
                }

                result.AuthorName = rightAuthorName;
                result.IsAnswerTrue = false;
                return result;
            }

            if (answer.AuthorName == rightAuthorName)
            {
                user.AddNewQuoteAndAuthor(quote.Id, answer.AuthorName);
                result.AuthorName = answer.AuthorName;
                result.IsAnswerTrue = true;

                DbContext.Users.Update(user);
                DbContext.SaveChanges();

                return result;
            }

            result.AuthorName = rightAuthorName;
            result.IsAnswerTrue = false;
            return result;
        }
    }
}
