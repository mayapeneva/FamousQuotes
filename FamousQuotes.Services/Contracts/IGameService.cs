namespace FamousQuotes.Services.Contracts
{
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.DTOs;

    public interface IGameService
    {
        T GetRandomUnansweredQuote<T>(User user, bool isBinaryMode);

        ResultDto SaveAnswer(User user, AnswerDto answer, bool isBinaryMode);
    }
}
