using FManager.Core;
using FManager.Core.Services;
using FManager.Data.Entities.Shared;

namespace FManager.Impl.Services
{
    /// <summary>
    /// Payment Plan Service.
    /// </summary>
    public class PaymentPlansService : Service<PaymentPlan>, IPaymentPlansService, IService<PaymentPlan>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentPlansService"/> class.
        /// </summary>
        public PaymentPlansService(
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}