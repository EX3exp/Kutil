using Kutil.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetKiwi;

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
                Word kWord = new Word(text);
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
