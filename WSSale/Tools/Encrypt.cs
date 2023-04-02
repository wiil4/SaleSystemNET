using System.Security.Cryptography;
using System.Text;

namespace WSSale.Tools
{
    public class Encrypt
    {
        public static string GetSha256(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert input string to a byte array using ASCII encoding and compute the hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(input));
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
