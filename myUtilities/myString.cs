using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS.myUtilities.myString
{
    /// <summary>
    /// Inkrementieren eines Strings
    /// </summary>
    public enum mode
    {
        AlphaNumeric = 1,
        Alpha = 2,
        Numeric = 3
    }

    public class myString
    {
        public static string Increment(string text, mode mode)
        {
            int zahl;
            bool isDigit = int.TryParse(text, out zahl);

            if (isDigit)
            {
                zahl += 1;
                return zahl.ToString();
            }
            else
            {
                var textArr = text.ToCharArray();

                // Add legal characters
                var characters = new List<char>();

                if (mode == mode.AlphaNumeric || mode == mode.Numeric)
                    for (char c = '0'; c <= '9'; c++)
                        characters.Add(c);

                if (mode == mode.AlphaNumeric || mode == mode.Alpha)
                    for (char c = 'a'; c <= 'z'; c++)
                        characters.Add(c);

                // Loop from end to beginning
                for (int i = textArr.Length - 1; i >= 0; i--)
                {
                    if (textArr[i] == characters.Last())
                    {
                        textArr[i] = characters.First();
                    }
                    else
                    {
                        textArr[i] = characters[characters.IndexOf(textArr[i]) + 1];
                        break;
                    }
                }

                return new string(textArr);
            }
        }
    }
}

