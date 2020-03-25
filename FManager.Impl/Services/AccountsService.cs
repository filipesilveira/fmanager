namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.ManageBO;

    /// <summary>
    /// Accounts Service.
    /// </summary>
    public class AccountsService : Service<Account>, IAccountsService, IService<Account>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsService"/> class.
        /// </summary>
        public AccountsService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}