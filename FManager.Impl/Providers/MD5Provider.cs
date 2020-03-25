namespace FManager.Impl.Services
{
    using FManager.Core.Providers;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// MD5 Criptography Provider
    /// </summary>
    public class MD5Provider : ICriptographyProvider
    {
        public string Apply(string input) {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}