namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.ManageBO;

    /// <summary>
    /// Parities Service.
    /// </summary>
    public class ParitiesService : Service<Parity>, IParitiesService, IService<Parity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParitiesService"/> class.
        /// </summary>
        public ParitiesService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}