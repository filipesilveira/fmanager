namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.DayBooks;

    /// <summary>
    /// Day Book Service.
    /// </summary>
    public class DayBooksService : Service<DayBook>, IDayBooksService, IService<DayBook>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DayBooksService"/> class.
        /// </summary>
        public DayBooksService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}