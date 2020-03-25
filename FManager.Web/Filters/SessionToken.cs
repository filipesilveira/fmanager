using FManager.Core.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace FManager.Web.Filters
{
    public class SessionToken : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            try
            {
                if (DependencyResolver.Current.GetService<ISessionHelper>().GetSessionToken() != Guid.Empty)
                {
                    return;
                }

                context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}