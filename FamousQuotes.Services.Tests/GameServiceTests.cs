namespace FamousQuotes.Services.Tests
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class GameServiceTests
    {
        private readonly List<string> Authors = new List<string>{ "Confucius", "Sigmund Freud", "Benjamin Franklin" };
        private readonly List<string> Quotes = new List<string>{ "Study the past, if you would divine the future.", "Silence is a true friend who never betrays.", "From error to error one discovers the entire truth.", "The most complicated achievements of thought are possible without the assistance of consciousness.", "Well done is better than well said.", "Watch the little things; a small leak will sink a great ship." };

        private readonly FamousQuotesDbContext dbContext;
        private readonly IGameService gameService;
        private readonly Mock<FamousQuotesUser> moqUser;

        public GameServiceTests()
        {
            var options = new DbContextOptionsBuilder<FamousQuotesDbContext>().UseInMemoryDatabase("FQDB")
                .Options;
            dbContext = new FamousQuotesDbContext(options);

            gameService = new GameService(dbContext);

            var index = 0;
            foreach (var authorName in Authors)
            {
                var author = new Author(authorName);
                dbContext.Authors.Add(author);
                dbContext.SaveChanges();

                var quoteOne = new Quote(author.Id, Quotes[index++]);
                var quoteTwo = new Quote(author.Id, Quotes[index++]);
                dbContext.Quotes.Add(quoteOne);
                dbContext.Quotes.Add(quoteTwo);
                dbContext.SaveChanges();
            }

            moqUser = new Mock<FamousQuotesUser>();
            moqUser.Setup(u => u.Id).Returns("b3b306c4-a07e-4e67-8570-52cc9957d311");
            moqUser.Setup(u => u.Answers).Returns(new List<Answer> { new Answer(1, moqUser.Object.Id), new Answer(2, moqUser.Object.Id), new Answer(3, moqUser.Object.Id), new Answer(4, moqUser.Object.Id), new Answer(5, moqUser.Object.Id), new Answer(6, moqUser.Object.Id) });
        }

        [Fact]
        public void GetRandomUnansweredQuote_ShouldReturnUnansweredQuoteWhenAllUnansweredForBinaryMode()
        {
            //Arrange
            var low = 1;
            var high = 6;

            //Act
            var unansweredQuote = gameService.GetRandomUnansweredQuote(moqUser.Object, true);

            //Assert
            Assert.InRange(unansweredQuote.QuoteId, low, high);
        }

        [Fact]
        public void GetRandomUnansweredQuote_ShouldReturnUnansweredQuoteWhenNotAllUnansweredForBinaryMode()
        {
            //Arrange
            moqUser.Object.AddCorrectAnswer(1, "Confucius", true);
            moqUser.Object.AddCorrectAnswer(6, "Benjamin Franklin", true);
            dbContext.SaveChanges();
            var low = 2;
            var high = 5;

            //Act
            var unansweredQuote = gameService.GetRandomUnansweredQuote(moqUser.Object, true);

            //Assert
            Assert.InRange(unansweredQuote.QuoteId, low, high);
        }
    }
}
