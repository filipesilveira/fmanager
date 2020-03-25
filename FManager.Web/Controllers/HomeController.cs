using FManager.Core.Helpers;
using FManager.Core.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace FManager.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISessionHelper sessionHelper;
        private readonly IUsersService usersService;

        public HomeController(
            ISessionHelper sessionHelper,
            IUsersService usersService)
        {
            this.sessionHelper = sessionHelper;
            this.usersService = usersService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "FManager";

            var tokenString = (string)this.sessionHelper.GetFromSession("Token");

            if (!Guid.TryParse(tokenString, out var token) || token == Guid.Empty)
            {
                return Redirect("/Login");
            }

            var user = await this.usersService.GetUserByCurrentSessionToken();

            if (user != null /*&& user.PaymentStatus.Code == 3*/)
            {
                return View();
            }

            return Redirect("/Login");
        }
    }
}
