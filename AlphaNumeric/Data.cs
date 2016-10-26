using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNumeric
{
    public class Data
    {
        public static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            string str = new string(chars);
            return str;
        }

        public static string EncodeString(string raw)
        {
            var bytes = GetBytes(raw);
            return EncodeBytes(bytes);
        }

        public static string EncodeBytes(byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            var encodedString = base64
                .Replace("0", "01")
                .Replace("+", "02")
                .Replace("/", "03")
                .Replace("=", "04");
            return encodedString;
        }

        public static string DecodeString(string encodedString)
        {
            byte[] bytes = DecodeBytes(encodedString);
            var raw = GetString(bytes);
            return raw;
        }

        public static byte[] DecodeBytes(string encodedString)
        {
            var base64 = encodedString
                            .Replace("02", "+")
                            .Replace("03", "/")
                            .Replace("04", "=")
                            .Replace("01", "0");
            var bytes = Convert.FromBase64String(base64);
            return bytes;
        }
    }
}
