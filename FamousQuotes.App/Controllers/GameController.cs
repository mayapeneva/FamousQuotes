namespace FamousQuotes.App.Controllers
{
    using FamousQuotes.App.Models.BindingModels;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class GameController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IGameService gameService;

        public GameController(
            UserManager<User> userManager,
            IGameService gameService)
        {
            this.userManager = userManager;
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult> PlayBinaryMode()
        {
            var user = await userManager.GetUserAsync(this.User);
            var question = gameService.GetRandomUnansweredQuote<RandomQuoteBinaryModeDto>(user, true);
            if (question.Score != null)
            {
                this.ViewData["Error"] = $"The game is over. Your score is {question.Score}.";
                return this.View();
            }

            // TODO: quote should be mapped to a viewModel and passed to the relevant view
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> PlayMultipleChoicesMode()
        {
            var user = await userManager.GetUserAsync(this.User);
            var question = gameService.GetRandomUnansweredQuote<RandomQuoteMultipleChoicesModeDto>(user, false);
            if (question.Score != null)
            {
                this.ViewData["Message"] = $"The game is over. Your score is {question.Score}.";
                return this.View();
            }

            // TODO: quote should be mapped to a viewModel and passed to the relevant view
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlayBinaryMode(AnswerBindingModel answer)
        {
            var user = await userManager.GetUserAsync(this.User);
            //TODO: map the binding model to dto
            var answerDto = new AnswerDto();
            var result = gameService.SaveAnswer(user, answerDto, true);
            if (result.IsAnswerTrue)
            {
                this.ViewData["Message"] = $"Correct! The right answer is: {result.AuthorName}.";
                return this.View();
            }

            this.ViewData["Message"] = $"Sorry, you are wrong! The right answer is: {result.AuthorName}.";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlayMultipleChoicesMode(AnswerBindingModel answer)
        {
            var user = await userManager.GetUserAsync(this.User);
            //TODO: map the binding model to dto
            var answerDto = new AnswerDto();
            var result = gameService.SaveAnswer(user, answerDto, false);
            if (result.IsAnswerTrue)
            {
                this.ViewData["Message"] = $"Correct! The right answer is: {result.AuthorName}.";
                return this.View();
            }

            this.ViewData["Message"] = $"Sorry, you are wrong! The right answer is: {result.AuthorName}.";
            return View();
        }
    }
}
