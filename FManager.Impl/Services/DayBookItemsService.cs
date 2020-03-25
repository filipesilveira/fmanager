namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.DayBooks;

    /// <summary>
    /// Day Book Items Service.
    /// </summary>
    public class DayBookItemsService : Service<DayBookItem>, IDayBookItemsService, IService<DayBookItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DayBookItemsService"/> class.
        /// </summary>
        public DayBookItemsService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}