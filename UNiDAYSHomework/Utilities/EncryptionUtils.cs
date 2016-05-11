using System.Security.Cryptography;
using System.Text;

namespace UNiDAYSHomework.Utilities
{
    public class EncryptionUtils
    {
        public static string Md5Hash(string stringToEncrypt)
        {
            var bs = Encoding.UTF8.GetBytes(stringToEncrypt);
            var s = new StringBuilder();
            using (var x = new MD5CryptoServiceProvider())
            {
                bs = x.ComputeHash(bs);
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
            }
            return s.ToString();
        }
    }
}