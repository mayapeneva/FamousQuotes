namespace FamousQuotes.Services.Contracts
{
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.DTOs;

    public interface IGameService
    {
        RandomQuoteDto GetRandomUnansweredQuote(User user, bool isBinaryMode);

        ResultDto SaveAnswer(User user, AnswerDto answer, bool isBinaryMode);
    }
}
