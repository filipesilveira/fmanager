using System.Threading.Tasks;

namespace FManager.Core.Providers
{
    /// <summary>
    /// Emails Provider.
    /// </summary>
    public interface IEmailsProvider
    {
        /// <summary>
        /// Send emails async
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="destinations"></param>
        /// <returns></returns>
        Task SendAsync(string subject, string body, params string[] destinations);

        /// <summary>
        /// Check if the emails is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsValidEmail(string email);
    }
}