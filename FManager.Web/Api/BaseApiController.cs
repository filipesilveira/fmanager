using FManager.Web.Filters;
using System.Web.Http;

namespace FManager.Web.Api
{
    [SessionToken]
    public class BaseApiController : ApiController
    {
    }
}