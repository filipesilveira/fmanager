using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using FManager.Data.Entities;
using FManager.Data.Entities.ManageBO;

/// <summary>
/// Accounts service.
/// </summary>
namespace FManager.Core.Services
{
    public interface IAccountsService : IService<Account>
    {
    }
}