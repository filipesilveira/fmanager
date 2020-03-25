namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.ManageBO;

    /// <summary>
    /// Entries Service.
    /// </summary>
    public class EntriesService : Service<Entry>, IEntriesService, IService<Entry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntriesService"/> class.
        /// </summary>
        public EntriesService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}