﻿namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.BindingModels;
    using FamousQuotes.Infrastructure.Models;
    using FamousQuotes.Infrastructure.ViewModels;
    using FamousQuotes.Services.Contracts;
    using FamousQuotes.Services.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class GameController : BaseController
    {
        private const string QuoteId = "QuoteId";
        private const string AuthorName = "AuthorName";
        private const string CorrectAuthorName = "CorrectAuthorName";

        private const string Message = "Message";
        private const string GameOverMessage = "The game is over. Your score is ";
        private const string CorrectAnswerMessage = "Correct!\n The right answer is:\n";
        private const string WrongAnswerMessage = "Sorry, you are wrong!\n The right answer is:\n";

        private readonly IGameService gameService;

        public GameController(
            UserManager<FamousQuotesUser> userManager,
            IMapper mapper,
            IGameService gameService)
            :base(userManager, mapper)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult> IndexBinaryMode()
        {
            var user = await GetUser();
            var quote = gameService.GetRandomUnansweredQuote(user, true);
            if (string.IsNullOrWhiteSpace(quote.QuoteText))
            {
                ViewData[Message] = GameOverMessage + quote.Score;
                return this.View();
            }

            HttpContext.Session.SetInt32(QuoteId, quote.QuoteId);
            HttpContext.Session.SetString(AuthorName, quote.AuthorOneName);
            HttpContext.Session.SetString(CorrectAuthorName, quote.CorrectAuthorName);

            var quoteViewModel = Mapper.Map<QuoteViewModel>(quote);
            return View(quoteViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> IndexMultipleChoicesMode()
        {
            FamousQuotesUser user = await GetUser();
            var quote = gameService.GetRandomUnansweredQuote(user, false);
            if (string.IsNullOrWhiteSpace(quote.QuoteText))
            {
                ViewData[Message] = GameOverMessage + quote.Score;
                return this.View();
            }

            var quoteViewModel = Mapper.Map<QuoteViewModel>(quote);
            return View(quoteViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PlayBinaryMode([FromForm]string answer)
        {
            var user = await GetUser();
            var answerDto = new AnswerDto
            {
                QuoteId = (int)HttpContext.Session.GetInt32(QuoteId),
                AuthorName = HttpContext.Session.GetString(AuthorName),
                CorrectAuthorName = HttpContext.Session.GetString(CorrectAuthorName),
                IsAnswerTrue = answer == "Yes"
            };

            var result = gameService.SaveAnswer(user, answerDto, true);
            var resultViewModel = new ResultViewModel
            {
                IsAnswerTrue = result.IsAnswerTrue
            };

            if (result.IsAnswerTrue)
            {
                resultViewModel.AuthorName = answerDto.CorrectAuthorName;
                return this.View(resultViewModel);
            }

            resultViewModel.AuthorName = result.AuthorName;
            return View(resultViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PlayMultipleChoicesMode(AnswerBindingModel answer)
        {
            var user = await GetUser();
            var answerDto = Mapper.Map<AnswerDto>(answer);
            var result = gameService.SaveAnswer(user, answerDto, false);
            if (result.IsAnswerTrue)
            {
                ViewData[Message] = CorrectAnswerMessage + result.AuthorName;
                return this.View();
            }

            ViewData[Message] = WrongAnswerMessage + result.AuthorName;
            return View();
        }
    }
}
