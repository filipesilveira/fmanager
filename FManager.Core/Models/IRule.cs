using System.Threading.Tasks;
using System.Xml.Linq;

namespace FManager.Core.Models
{
    /// <summary>
    /// This interface define a Rule
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Gets the rule name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Apply the rule to a xml element
        /// </summary>
        /// <param name="currentHtml"></param>
        /// <param name="element"></param>
        /// <returns>A string that contains the html code for the element</returns>
        Task<string> ApplyAsync(string currentHtml, XElement element = null);
    }
}

