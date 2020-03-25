namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.ManageBO;

    /// <summary>
    /// Currencies Service.
    /// </summary>
    public class CurrenciesService : Service<Currency>, ICurrenciesService, IService<Currency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrenciesService"/> class.
        /// </summary>
        public CurrenciesService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}