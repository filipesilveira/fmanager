using FManager.Core.Dtos.Login;
using FManager.Core.Helpers;
using FManager.Core.Providers;
using FManager.Core.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FManager.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly ISessionHelper sessionHelper;
        private readonly IPaymentsProvider paymentsProvider;
        private readonly ICriptographyProvider criptographyProvider;
        private readonly IEmailsProvider emailsProvider;

        public LoginController(
            IUsersService usersService,
            ISessionHelper sessionHelper,
            IPaymentsProvider paymentsProvider,
            ICriptographyProvider criptographyProvider,
            IEmailsProvider emailsProvider)
        {
            this.usersService = usersService;
            this.sessionHelper = sessionHelper;
            this.paymentsProvider = paymentsProvider;
            this.criptographyProvider = criptographyProvider;
            this.emailsProvider = emailsProvider;
        }

        public ActionResult Index()
        {
            return View("~/Views/Login/LoginIndex.cshtml");
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public ActionResult Logout()
        {
            this.sessionHelper.ClearSession();

            return Redirect("/Login");
        }

        public async Task<ActionResult> ResetPassword()
        {
            this.sessionHelper.ClearSession();

            await this.emailsProvider.SendAsync("FManager: Recuperar Senha", "Teste", "filipe.silveira06@gmail.com");

            return Redirect("/Login");
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginDto loginDto)
        {
            this.sessionHelper.SetToSession("Token", null);

            var password = this.criptographyProvider.Apply(loginDto.Password);
            var user = await this.usersService.GetByCriteriaAsync(w => w.Email == loginDto.Email && w.Password == password && !w.Deleted);

            if (user == null)
            {
                return Json(new
                {
                    Login = false
                }, JsonRequestBehavior.AllowGet);
            }

            var isPaymentCompleted = this.paymentsProvider.IsPaymentCompleted(user);

            user.SessionToken = Guid.NewGuid();

            user = await this.usersService.UpdateAsync(user);

            this.sessionHelper.SetToSession("Token", user.SessionToken.ToString());
            this.sessionHelper.SetToSession("UserId", user.UserId);

            return Json(new
            {
                Login = true,
                Payment = isPaymentCompleted
            }, JsonRequestBehavior.AllowGet);
        }
    }
}