using FManager.Core.Dtos.Users;
using FManager.Data.Entities.Users;
using System;

namespace FManager.Core.Providers
{
    /// <summary>
    /// Interface to handle the payments.
    /// </summary>
    public interface IPaymentsProvider
    {
        /// <summary>
        /// Returns a payment url based on itemId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="paymentPlanId"></param>
        /// <returns></returns>
        string Checkout(UserDto user, Guid paymentPlanId);

        /// <summary>
        /// Check out if the payment is completed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsPaymentCompleted(User user);

        /// <summary>
        /// Get user's payment url
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GetPaymentUrl(User user);
    }
}