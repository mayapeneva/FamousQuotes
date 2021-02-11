namespace FamousQuotes.App.Controllers
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        public UserManager<User> UserManager { get; set; }

        public IMapper Mapper { get; set; }
    }
}
