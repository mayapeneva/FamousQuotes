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
        private const string Message = "Message";
        private const string GameOverMessage = "The game is over. Your score is ";
        private const string CorrectAnswerMessage = "Correct! The right answer is: ";
        private const string WrongAnswerMessage = "Sorry, you are wrong! The right answer is: ";

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
            var user = await GetUser();
            var question = gameService.GetRandomUnansweredQuote(user, true);
            if (string.IsNullOrWhiteSpace(question.QuoteText))
            {
                this.ViewData[Message] = GameOverMessage + question.Score;
                return this.View();
            }

            // TODO: quote should be mapped to a viewModel and passed to the view below
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> PlayMultipleChoicesMode()
        {
            User user = await GetUser();
            var question = gameService.GetRandomUnansweredQuote(user, false);
            if (string.IsNullOrWhiteSpace(question.QuoteText))
            {
                this.ViewData[Message] = GameOverMessage + question.Score;
                return this.View();
            }

            // TODO: quote should be mapped to a viewModel and passed to the view below
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlayBinaryMode(AnswerBindingModel answer)
        {
            var user = await GetUser();
            //TODO: map the binding model to dto with mapper
            var answerDto = new AnswerDto
            {
                QuoteText = answer.QuoteText,
                AuthorName = answer.AuthorName,
                IsAnswerTrue = answer.IsAnswerTrue
            };

            var result = gameService.SaveAnswer(user, answerDto, true);
            if (result.IsAnswerTrue)
            {
                this.ViewData[Message] = CorrectAnswerMessage + result.AuthorName;
                return this.View();
            }

            this.ViewData[Message] = WrongAnswerMessage + result.AuthorName;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlayMultipleChoicesMode(AnswerBindingModel answer)
        {
            var user = await GetUser();
            //TODO: map the binding model to dto with mapper
            var answerDto = new AnswerDto
            {
                QuoteText = answer.QuoteText,
                AuthorName = answer.AuthorName
            };

            var result = gameService.SaveAnswer(user, answerDto, false);
            if (result.IsAnswerTrue)
            {
                this.ViewData[Message] = CorrectAnswerMessage + result.AuthorName;
                return this.View();
            }

            this.ViewData[Message] = WrongAnswerMessage + result.AuthorName;
            return View();
        }

        private async Task<User> GetUser()
        {
            return await userManager.GetUserAsync(this.User);
        }
    }
}
