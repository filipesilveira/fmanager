namespace FManager.Impl.Services
{
    using Core;
    using FManager.Core.Services;
    using FManager.Data.Entities.Users;

    /// <summary>
    /// Payment Status Service.
    /// </summary>
    public class PaymentStatusService : Service<PaymentStatus>, IPaymentStatusService, IService<PaymentStatus>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentStatusService"/> class.
        /// </summary>
        public PaymentStatusService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}