using Kutil.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using javax.xml.transform;
using javax.xml.soap;

namespace Kutil.G2p
{
    
    public static class Variator
    {
        /// <summary>
        /// Variates input word. e.g) "밥을" -> "바블" ,  "먹다" -> "먹따"
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Variate(string word)
        {
            try
            {
                string text = word;
                Hangeul[] hangeuls = new Hangeul[word.Length];
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == ' ')
                    {
                        continue;
                    }
                    hangeuls[i] = new Hangeul(word[i]);
                }

                return text;
                
            }
           
            catch (Exception e)
            {
                Console.WriteLine("Error in Variator.Variate()");
                Console.WriteLine("Input: " + word);
                
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return word;
            }
            

        }
    }
}
