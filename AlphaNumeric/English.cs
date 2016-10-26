using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNumeric
{
    public static class English
    {
        public static char[] validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ3456789".ToCharArray();

        private static Dictionary<char, int> CharValues = new Dictionary<char, int>();

        static English()
        {
            for(int i = 0; i < validChars.Length; i++)
            {
                CharValues.Add(validChars[i], i);
            }
        }

        public static string Encode(string rawString)
        {
            char[] allChars = rawString.ToCharArray();
            StringBuilder builder = new StringBuilder(rawString.Length * 2);
            for(int i = 0; i < allChars.Length; i++)
            {
                int c = allChars[i];
                if((c >= 51 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122))
                {
                    builder.Append(allChars[i]);
                } 
                else
                {
                    int index = builder.Length;
                    int count = 0;
                    do
                    {
                        builder.Append(validChars[c % 59]);
                        c /= 59;
                        count++;
                    } while (c > 0);

                    if (count == 1) builder.Insert(index, '0');
                    else if (count == 2) builder.Insert(index, '1');
                    else if (count == 3) builder.Insert(index, '2');
                    else throw new Exception("Base59 has invalid count, method must be wrong Count is: " + count);
                }
            }

            return builder.ToString();   
        }

        public static string Decode(string encodedString)
        {
            StringBuilder builder = new StringBuilder(encodedString.Length * 2);

            char[] allChars = encodedString.ToCharArray();
            int index = 0;
            while(index < allChars.Length)
            {
                char c0 = allChars[index++];
                if ((c0 >= 51 && c0 <= 57) || (c0 >= 65 && c0 <= 90) || (c0 >= 97 && c0 <= 122))
                {
                    builder.Append(c0);
                }
                else if(c0 >= 48 && c0 <= 50)
                {
                    int value = CharValues[allChars[index++]];
                    
                    if(c0 >= 49)
                    {
                        value += CharValues[allChars[index++]] * 59;
                        if(c0 >= 50)
                        {
                            value += CharValues[allChars[index++]] * 59 * 59;
                        }
                    }
                    
                    builder.Append((char)value);
                }
                else
                {
                    throw new Exception("Not a correctly encoded string");
                }
            }

            return builder.ToString();
        }
    }
}
