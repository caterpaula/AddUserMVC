using System.Security.Cryptography;
using System.Text;

namespace UNiDAYSHomework.Utilities
{
    public class EncryptionUtils
    {
        public static string Md5Hash(string stringToEncrypt)
        {
            byte[] bs = Encoding.UTF8.GetBytes(stringToEncrypt);
            StringBuilder s = new StringBuilder();
            using (MD5CryptoServiceProvider x = new MD5CryptoServiceProvider())
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