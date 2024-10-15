using Kutil.G2p;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutil
{
    
    public static class G2P
    {

        public static string Convert(string sentence)
        {
            List<string> separated = new List<string>();
            string toAdd = string.Empty;
            StringBuilder sb = new StringBuilder();
            bool lastWasBlank = false;
            int i = -1;
            foreach (char character in sentence)
            {
                ++i; 
                if (i == sentence.Length - 1)
                {
                    sb.Append(character);
                    separated.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }

                if (character == ' ')
                {
                    separated.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(character);
                    lastWasBlank = true;
                    separated.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }
                sb.Append(character);
            }
            List<string> results = new List<string>();
            foreach (string word in separated)
            {
                if (word.Equals(" "))
                {
                    results.Add(" ");
                    continue;
                }
                results.Add(Variator.Variate(word));
            }
            return string.Join("", results);
        }
    }
}
