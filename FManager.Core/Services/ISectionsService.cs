using FManager.Data.Entities.ManageBO;
using System;
using System.Threading.Tasks;

/// <summary>
/// Sections service.
/// </summary>
namespace FManager.Core.Services
{
    public interface ISectionsService : IService<Section>
    {
        /// <summary>
        /// Create new section for an account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Section> CreateNewSection(Guid accountId);
    }
}