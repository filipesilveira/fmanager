namespace FManager.Core.Providers
{
    /// <summary>
    /// Interface to handle cryptography.
    /// </summary>
    public interface ICriptographyProvider
    {
        /// <summary>
        /// Apply criptography to a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Apply(string input);
    }
}