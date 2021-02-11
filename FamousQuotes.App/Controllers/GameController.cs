namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.BindingModels;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class GameController : BaseController
    {
        private const string Message = "Message";
        private const string GameOverMessage = "The game is over. Your score is ";
        private const string CorrectAnswerMessage = "Correct! The right answer is: ";
        private const string WrongAnswerMessage = "Sorry, you are wrong! The right answer is: ";

        private readonly IGameService gameService;

        public GameController(
            UserManager<User> userManager,
            IMapper mapper,
            IGameService gameService)
            :base(userManager, mapper)
        {
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
            var answerDto = Mapper.Map<AnswerDto>(answer);
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
            var answerDto = Mapper.Map<AnswerDto>(answer);
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
            return await UserManager.GetUserAsync(this.User);
        }
    }
}
