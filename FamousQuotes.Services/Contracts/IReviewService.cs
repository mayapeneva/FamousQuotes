namespace FamousQuotes.Services.Contracts
{
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.DTOs;

    public interface IReviewService
    {
        AchievementsDto GetUserAchievements(FamousQuotesUser user);
    }
}
