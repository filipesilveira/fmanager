using System.Web.Mvc;

namespace FManager.Web.Controllers
{
    public class ReviewsController : BaseController
    {
        public ActionResult ReviewsList()
        {
            return View();
        }
    }
}