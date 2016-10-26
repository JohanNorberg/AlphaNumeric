using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlphaNumeric;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AlphaNumeric.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static Regex alphaNumeric = new Regex(@"^[a-zA-Z0-9\s,]*$");

        [TestMethod]
        public void TestEnglish()
        {
            {
                string org = "one1.fake email@/just/to/test.com";
                TestEnglish(org);
            }

            {
                string org = "مرحبا، أنا التشفير تعلمون. من الفضاء الخارجي.";
                TestEnglish(org);
            }

            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < char.MaxValue; i++)
                {
                    char c = (char)i;
                    builder.Append(c);
                }
                string org = builder.ToString();
                TestEnglish(org);
            }
        }

        private static void TestEnglish(string org)
        {
            string encoded = English.Encode(org);
            Assert.IsTrue(alphaNumeric.IsMatch(encoded));
            string decoded = English.Decode(encoded);
            Assert.AreEqual(org, decoded);
        }

        [TestMethod]
        public void TestDataEncoding()
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int strSize = random.Next(1, 200);
                TestEncodingString(strSize);
            }

            for (int i = 0; i < 10000; i++)
            {
                var bytes = GenerateRandomBytes(50);
                string original = Convert.ToBase64String(bytes);
                string encoded = Data.EncodeString(original);
                string decoded = Data.DecodeString(encoded);
                Assert.AreEqual(original, decoded);
            }

            {
                string org = "";
                string enc = Data.EncodeString(org);
                string dec = Data.DecodeString(enc);
                Assert.AreEqual(org, dec);
            }

            {
                var bytes = GenerateRandomBytes(32);
                var str = Data.EncodeBytes(bytes);
                Assert.IsTrue(str.Length < 65);
            }
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var data = new byte[length];
                rng.GetBytes(data);
                return data;
            }
        }

        private static void TestEncodingString(int strSize)
        {
            strSize /= 2;
            strSize *= 2;
            var bytes = GenerateRandomBytes(strSize);
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            string original = new string(chars);

            //string original = System.Text.Encoding.UTF8.GetString(bytes); // new string(chars);

            string encoded = Data.EncodeString(original);
            Assert.IsTrue(alphaNumeric.IsMatch(encoded));
            string decoded = Data.DecodeString(encoded);
            Assert.AreEqual(original, decoded);
        }
    }
}
