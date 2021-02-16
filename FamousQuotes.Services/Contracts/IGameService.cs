namespace FamousQuotes.Services.Contracts
{
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.DTOs;

    public interface IGameService
    {
        QuoteDto GetRandomUnansweredQuote(FamousQuotesUser user, bool isBinaryMode);

        ResultDto SaveAnswer(FamousQuotesUser user, AnswerDto answer, bool isBinaryMode);
    }
}
