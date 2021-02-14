namespace FamousQuotes.Services
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;

    public class ReviewService : BaseService, IReviewService
    {
        public ReviewService(FamousQuotesDbContext dbContext)
            : base(dbContext)
        {
        }

        public AchievementsDto GetUserAchievements(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
