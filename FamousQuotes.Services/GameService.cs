namespace FamousQuotes.Services
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;

    internal class GameService : BaseService, IGameService
    {
        public GameService(FamousQuotesDbContext dbContext)
            : base(dbContext)
        {
        }

        public T GetRandomUnansweredQuote<T>(User user, bool isBinaryMode)
        {
            throw new System.NotImplementedException();
        }

        public ResultDto SaveAnswer(User user, AnswerDto answer, bool isBinaryMode)
        {
            throw new System.NotImplementedException();
        }
    }
}
