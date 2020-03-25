namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Helpers;
    using FManager.Core.Services;
    using FManager.Data.Entities.Users;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Users Service.
    /// </summary>
    public class UsersService : Service<User>, IUsersService, IService<User>
    {
        private readonly ISessionHelper sessionHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/> class.
        /// </summary>
        public UsersService(
            IUnitOfWork unitOfWork,
            ISessionHelper sessionHelper)
            : base(unitOfWork)
        {
            this.sessionHelper = sessionHelper;
        }

        public Task<User> GetUserByCurrentSessionToken()
        {
            var sessionToken = this.sessionHelper?.GetSessionToken() ?? Guid.Empty;

            if (sessionToken == Guid.Empty)
            {
                return null;
            }

            return this.GetByCriteriaAsync(w => w.SessionToken == sessionToken && !w.Deleted);
        }
    }
}