using System.Security.Cryptography;
using System.Text;

namespace gdtPublisher.Utils
{
    public static class Hash
    {
        public static string Get(string value)
        {
            StringBuilder str = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                {
                    str.Append(b.ToString("X2"));
                }
            }

            return str.ToString();
        }
    }
}
