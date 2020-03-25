

namespace FManager.Web.Extensions
{
    using FManager.Web.Controllers;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class ActionResultExtensions
    {
        public static string ToHtml(this ActionResult result, BaseController controller)
        {
            // Create memory writer.
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            // Create fake http context to render the view.
            var fakeResponse = new HttpResponse(memWriter);
            
            var routeData = new RouteData();
            routeData.Values.Add("controller", controller.GetType().Name.Replace("Controller", ""));
            routeData.Values.Add("action", "PrintBlank");

            var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request,
                fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                routeData,
                controller);
            var oldContext = System.Web.HttpContext.Current;
            System.Web.HttpContext.Current = fakeContext;
            // Render the view.
            result.ExecuteResult(fakeControllerContext);

            // Restore old context.
            System.Web.HttpContext.Current = oldContext;

            // Flush memory and return output.
            memWriter.Flush();
            return sb.ToString();
        }
    }
}