namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;

        protected ReviewController(
            UserManager<Infrastructure.Models.FamousQuotesUser> userManager,
            IMapper mapper,
            IReviewService reviewService)
            : base(userManager, mapper)
        {
            this.reviewService = reviewService;
        }

        public async Task<ActionResult> PersonalAchievemnts()
        {
            var user = await GetUser();
            var achievements = reviewService.GetUserAchievements(user);
            return View();
        }
    }
}
