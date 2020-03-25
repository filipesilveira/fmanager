namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.ManageBO;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Sections Service.
    /// </summary>
    public class SectionsService : Service<Section>, ISectionsService, IService<Section>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SectionsService"/> class.
        /// </summary>
        public SectionsService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<Section> CreateNewSection(Guid accountId)
        {
            var account = await this.UnitOfWork.Repository<Account>().GetByCriteriaAsync(w => w.AccountId == accountId);

            var section = new Section() {
                SectionId = Guid.NewGuid(),
                AccountId = accountId,
                CreationDate = DateTime.Now,
                Change = DateTime.Now
            };

            section = await this.AddAsync(section);
            section.Account = account;

            return section;
        }
    }
}