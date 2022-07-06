using System.Security.Cryptography;
using System.Text;

namespace BWT.Infrastructure.Cryptography
{
    public class Encode
    {
        public static string MD5(string stringEnconde)
        {
            MD5 formatMD5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder builder = new StringBuilder();
            stream = formatMD5.ComputeHash(encoding.GetBytes(stringEnconde));
            for (int i = 0; i < stream.Length; i++)
                builder.AppendFormat("{0:x2}", stream[i]);
            return builder.ToString();
        }

        public static string SHA256(string token)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(token));
            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
