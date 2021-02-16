namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BaseController : Controller
    {
        protected BaseController(UserManager<FamousQuotesUser> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        protected UserManager<FamousQuotesUser> UserManager { get; set; }

        protected IMapper Mapper { get; set; }

        protected async Task<FamousQuotesUser> GetUser()
        {
            return await UserManager.GetUserAsync(this.User);
        }
    }
}
