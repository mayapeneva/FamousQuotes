namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class BaseController : Controller
    {
        protected BaseController(UserManager<User> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        protected UserManager<User> UserManager { get; set; }

        protected IMapper Mapper { get; set; }

        protected async Task<User> GetUser()
        {
            return await UserManager.GetUserAsync(this.User);
        }
    }
}
